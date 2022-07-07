/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using Newtonsoft.Json.Linq;
using ProjectApp.Protocol;
using System;
using System.Collections.Generic;

namespace ProjectApp
{
    public class LoginCtrl : BaseCtrl
    {
        public static LoginCtrl Instance { get; private set; }

        private LoginModel loginModel;

        public bool hasUpdateApkUIDisplay = false;
        public bool isFullLoginSucceed = false;
        public int loginCompleteTimes = 0;
        public List<string> c2s_infoExtraFields = null;
        public bool isDelayLogining = false;

        #region 生命周期
        protected override void OnInit()
        {
            Instance = this;
            loginModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;
        }
        protected override void OnDispose()
        {
            Instance = null;
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            AppDispatcher.Instance.AddListener(AppMsg.WebSocketServer_ConnectSucceed, OnWebSocketConnected);
        }

        protected override void RemoveListener()
        {
            AppDispatcher.Instance.RemoveListener(AppMsg.WebSocketServer_ConnectSucceed, OnWebSocketConnected);
        }

        protected override void AddServerListener()
        {
            wsNetDispatcher.AddPriorityListener(WSNetMsg.S2C_reg_login, OnLoginResp);
        }
        protected override void RemoveServerListener()
        {
            wsNetDispatcher.RemovePriorityListener(WSNetMsg.S2C_reg_login, OnLoginResp);
        }
        #endregion

        public void InitLogin()
        {
            // 检查安装情况
            LoginLocalCache.CheckApkInstall();
        }

        private void OnWebSocketConnected(object obj)
        {
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.socket_connect);
            if (ProjectApplication.Instance.IsNewInstall && isFirstWebSocketConnectFinish)
            {
                isFirstWebSocketConnectFinish = false;
                ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.new_user_socket_connected);
            }
        }
        /// <summary>
        /// websocket会多次连接，所以新用户只能调用一次
        /// </summary>
        private bool isFirstWebSocketConnectStart = true;

        /// <summary>
        /// websocket连接成功,新用户只能调用一次
        /// </summary>
        private bool isFirstWebSocketConnectFinish = true;
        public bool ConnectLogin()
        {
            if (isDelayLogining)
            {
                LogUtil.Log("[LoginCtrl]ConnectLogin Fail: Need DelayLogin");
                return false;
            }

            LogUtil.Log("[LoginCtrl]ConnectLogin");
            AppGlobal.IsLoginSucceed = false;

            bool result = WSNetMgr.Instance.Connect(SendNetLoginReq);

            //开始统计连接websocket
            ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.socket_connect);
            if (ProjectApplication.Instance.IsNewInstall && isFirstWebSocketConnectStart)
            {
                isFirstWebSocketConnectStart = false;
                ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.new_user_socket_connected);
            }

            if (!result && !NetConst.IsNetAvailable && !WSNetMgr.Instance.isConnected)
            {
                if (!App.GetIsWeakNetwork())
                {
                    AppDispatcher.Instance.Dispatch(AppMsg.WebSocketServer_ConnectNoNetwork);
                }
            }
            else
            {
                // 统计连接时长
                ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.networkavailable_connect_delay);
            }
            return result;
        }

        private void SendLoginReq(bool isWeakNetworkMode)
        {
            // 统计连接Url
            ChannelMgr.Instance.SendStatisticEventWithParam(StatisticConst.ws_best_url, WSNetMgr.Instance.GetNewestConnectionUrl());
            // 统计连接时长
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.networkavailable_connect_delay);
            // 设置弱联网状态
            WeakNetworkCtrl.Instance.IsInWeakNetworkMode = isWeakNetworkMode;

            C2S_reg_login loginReq = new C2S_reg_login();
            loginReq.data = new C2S_reg_login_data();
            // 初始化登录协议  
            LoginRequestMsgData.Init(loginReq.data, c2s_infoExtraFields);
            LoginLocalCache.ReadOperationConfigLocalCache(loginReq.data);

            // 登录  
            if (!isWeakNetworkMode)
            {
                // 回置状态
                ReconnectCtrl.Instance.ResumeAutoReConnectState();

                bool isReLogin = loginCompleteTimes > 0;
                loginReq.data.is_reconnect = isReLogin ? 1 : 0;

                WSNetMgr.Instance.State = WSNetState.Logining;
                WSNetMgr.Instance.ImmediateSend(loginReq);
            }
            else
            {
                // 登录成功
                S2C_reg_login loginResp = WeakNetworkCtrl.Instance.ReadLocalCacheS2CRegLoginMsg();
                OnLoginResp(loginResp);
            }
        }

        private void SendNetLoginReq()
        {
            SendLoginReq(false);
        }

        public void SendOfflineLoginReq()
        {
            SendLoginReq(true);
        }

        private void OnLoginResp(BaseS2CJsonProto respMsg)
        {
            S2C_reg_login loginResp = respMsg as S2C_reg_login;
            // 异常处理
            if (!string.IsNullOrEmpty(loginResp.err))
            {
                LogUtil.LogError("[LoginCtrl]Login Resp Error: " + loginResp.err);
                App.ShowTipsUI("Login server error!");
                return;

                switch (loginResp.err)
                {
                    // 登录需要延迟
                    case "RELOGIN MUST DELAY":
                        isDelayLogining = true;
                        WSNetMgr.Instance.State = WSNetState.LoginFailed_MustDelay;
                        int delayTime = 0;
                        CtrlDispatcher.Instance.Dispatch(CtrlMsg.Login_ReloginWaitDelay, delayTime);
                        TimerUtil.Simple.AddTimer(delayTime, () =>
                        {
                            isDelayLogining = false;
                            ConnectLogin();
                            CtrlDispatcher.Instance.Dispatch(CtrlMsg.Login_ReloginWaitDelayEnd);
                        });
                        return;
                    // 强制更新
                    case "MUST UPDATE":
                        if (AppConst.IsNeedPromptAppUpdate)
                        {
                            CheckApkUpdate(loginResp);
                            return;
                        }
                        break;
                    // 其他登录错误
                    default:
                        LogUtil.LogError("[LoginCtrl]Login Resp Error: " + loginResp.err);
                        App.ShowTipsUI("Login server error!");
                        return;
                }
            }

            // 登录成功
            LogUtil.Log("[LoginCtrl]Login Succeed");
            AppGlobal.IsLoginSucceed = true;
            WSNetMgr.Instance.State = WSNetState.LoginSuccess;

            loginCompleteTimes++;
            loginResp.sequenceNumber = loginCompleteTimes;
            if (!isFullLoginSucceed && loginResp.sequenceNumber != 1)
            {
                // 未完成完整登录游戏，仅做恢复连接  
                return;
            }

            // 统计链接Url
            if (WSNetMgr.Instance.isConnected)
            {
                // SDK统计
                Channel.Current.onLoginSuccess(loginResp.data.uid, loginResp.data.token, WSNetMgr.Instance.GetNewestConnectionUrl());
            }
            // 统计统计登录时长
            if (loginResp.sequenceNumber == 1)
            {
                // 登录漏斗统计3
                LoginStatistics.AddFunnelData_loginsucceed_3();

                int upperLimit = 120 * 1000;
                ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.login_succeed, upperLimit);
                if (ProjectApplication.Instance.IsNewInstall)
                {
                    ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.new_user_login_succeed, upperLimit);
                }
            }

            // Apk升级弹窗
            if (AppConst.IsNeedPromptAppUpdate)
            {
                if (!CheckApkUpdate(loginResp))
                {
                    LoginProcess(loginResp);
                }
            }
            else
            {
                LoginProcess(loginResp);
            }
        }

        private bool CheckApkUpdate(S2C_reg_login loginResp)
        {
            if (loginResp.data.upgrade != null)
            {
                Update update = loginResp.data.upgrade;
                if (update.mode != 3)
                {
                    if (update.mode == 1) return false;

                    if (AppGlobal.IsGameStart)
                    {
                        return false;
                    }
                    if (loginResp.sequenceNumber == 1)
                    {
                        // 需要更新
                        LogUtil.Log("[LoginCtrl]Need Update Apk");
                        hasUpdateApkUIDisplay = true;
                        AppHelper.ShowNeedUpdateUI(update, () => LoginProcess(loginResp));
                        return true;
                    }
                }
                else
                {
                    // 强制更新
                    LogUtil.Log("[LoginCtrl]Must Need Update Apk");
                    hasUpdateApkUIDisplay = true;
                    AppHelper.ShowMustUpdateUI(update);
                    return true;
                }
            }
            return false;
        }

        private void LoginProcess(S2C_reg_login loginResp)
        {
            // 登录流程
            LogUtil.Log("[LoginCtrl]LoginProcess SequenceNumber: " + loginResp.sequenceNumber);
            // 邀请码
            LogUtil.Log("[LoginCtrl]InvitedCode: " + loginResp.data.info.invite_code);

            hasUpdateApkUIDisplay = false;

            if (App.GetIsWeakNetwork())
            {
                WeakNetworkCtrl.Instance.InitCache(loginResp);
            }

            App.UserInfo = new UserInfo();
            App.UserInfo.userId = loginResp.data.uid.ToString();

            // 保存本地Uid
            LoginLocalCache.SetLocalUid(loginResp.data.uid);
            // 保存Token
            LoginLocalCache.SetToken(loginResp.data.token);
            // 设置运营配置
            LoginLocalCache.SetOperationConfigLocalCache(loginResp);

            // 初始化SDK
            InitSDK(loginResp);
            // 初始化模块数据
            InitModelData(loginResp);
            // 初始化用户配置完成消息
            ctrlDispatcher.Dispatch(CtrlMsg.UserConfiguration_Init);

            if (WeakNetworkCtrl.Instance.IsInWeakNetworkMode)
            {
                LoginLogicComplete(loginResp);
            }
            else
            {
                // 设置游戏配置表  
                if (WSNetMgr.Instance.IsAppWssUrl())
                {
                    LoginWssConfig.UpdateWssGameConfig(loginResp, () => LoginLogicComplete(loginResp));
                }
                else
                {
                    LoginObsoleteConfig.UpdateGameConfig(loginResp, () => LoginLogicComplete(loginResp));
                }
            }
        }

        private void InitSDK(S2C_reg_login loginResp)
        {
            try
            {
                // 发送UID给安卓 -> (因为需求改变，发送邀请码给安卓) -> (因为积分墙需求，修改为发送UID给安卓)
                Channel.Current.sendUID(loginResp.data.uid.ToString());

                //HACK Adwords广告来源-暂时不需要用到 
                //ChannelMgr.Instance.OnSendAdwords();   

                // 统计玩家登陆 发送邀请码 （注: SDK存在问题，需要注释SDK实现)
                Channel.Current.onProfileSignIn(loginResp.data.info.invite_code);

                if (loginResp.data.ltv_24h != null)
                {
                    Channel.Current.onUpdateLtv24HJson(loginResp.data.ltv_24h.ToString());
                }

                if (loginResp.data.pg_setting != null)
                {
                    JObject jobj = SerializeUtil.GetJObjectByObject(loginResp.data.pg_setting);
                    if (jobj == null) return;
                    if (jobj["ads_video"] != null)
                    {
                        Channel.Current.setAdsNewConfig(jobj["ads_video"].ToString());
                    }
                    if (jobj["ads_icon"] != null)
                    {
                        Channel.Current.setAdsIconConfig(jobj["ads_icon"].ToString());
                    }
                    if (jobj["adjust_event"] != null)
                    {
                        Channel.Current.setTrackEventTokenMap(jobj["adjust_event"].ToString());
                    }
                    if (jobj["ads_interstitial"] != null)
                    {
                        Channel.Current.setAdsInterstitialConfig(jobj["ads_interstitial"].ToString());
                    }
                    if (loginResp.data.info != null
                        && loginResp.data.info.is_open_exchange)
                    {
                        if (jobj["net_opt_v1"] != null)
                            Channel.Current.setNetOptConfig(jobj["net_opt_v1"].ToString(), loginResp.data.uid.ToString());
                        else if (jobj["net_opt"] != null)
                            Channel.Current.setNetOptConfig(jobj["net_opt"].ToString(), loginResp.data.uid.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError("[LoginCtrl]InitSDK Exception: " + e.ToString());
            }
        }

        private void InitModelData(S2C_reg_login loginResp)
        {
            try
            {
                loginModel.loginData = loginResp.data;
                loginModel.launchAppTime = (int)DateTimeMgr.Instance.GetCurrTimestamp();
                loginModel.loginDays = loginResp.data.statis.online_day;
                loginModel.loginCount = loginResp.data.statis.online_count;
                loginModel.isNewUser = (loginResp.data.statis.online_day == 1) && (loginResp.data.statis.online_count == 1);
                InfoCtrl.Instance.InitInfoData(loginResp);
            }
            catch (Exception e)
            {
                LogUtil.LogError("[LoginCtrl]InitModelData Exception: " + e.ToString());
            }
        }

        private void LoginLogicComplete(S2C_reg_login loginResp)
        {
            if (WeakNetworkCtrl.Instance.IsInWeakNetworkMode)
            {
                LogUtil.Log("[LoginCtrl]WeakNetworkLogin");
                CtrlDispatcher.Instance.Dispatch(CtrlMsg.WeakNetwork_LoginSucceed);
            }
            else
            {
                if (loginResp.sequenceNumber == 1)
                {
                    CtrlDispatcher.Instance.Dispatch(CtrlMsg.Login_Succeed);
                    isFullLoginSucceed = true;
                }
                else
                {
                    CtrlDispatcher.Instance.Dispatch(CtrlMsg.Login_ReloginSucceed, loginCompleteTimes);
                }
            }

            if (WeakNetworkCtrl.Instance.IsCanOfflineMode())
            {
                LogUtil.Log("[LoginCtrl]Can Offline Mode");
            }
        }
    }
}