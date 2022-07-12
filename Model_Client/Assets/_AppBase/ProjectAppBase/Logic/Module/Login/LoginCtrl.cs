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
        }  
        protected override void RemoveServerListener()
        {
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
     
        private void SendLoginReq(bool isWeakNetworkMode)
        {
            // 统计连接时长
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.networkavailable_connect_delay);
            // 设置弱联网状态
            WeakNetworkCtrl.Instance.IsInWeakNetworkMode = isWeakNetworkMode;

            // 登录成功
            OnLoginResp();
        }

        private void SendNetLoginReq()
        {
            SendLoginReq(false);
        }

        public void SendOfflineLoginReq()
        {
            SendLoginReq(true);
        }

        private void OnLoginResp()
        {  
            LoginProcess();
        }        
          
        private void LoginProcess()
        {
            hasUpdateApkUIDisplay = false;      
            // 初始化用户配置完成消息
            ctrlDispatcher.Dispatch(CtrlMsg.UserConfiguration_Init);

            LoginLogicComplete();
        }        

        private void LoginLogicComplete()
        {
            if (WeakNetworkCtrl.Instance.IsInWeakNetworkMode)
            {
                LogUtil.Log("[LoginCtrl]WeakNetworkLogin");
                CtrlDispatcher.Instance.Dispatch(CtrlMsg.WeakNetwork_LoginSucceed);
            }
            else
            {
                CtrlDispatcher.Instance.Dispatch(CtrlMsg.Login_Succeed);
            }  

            if (WeakNetworkCtrl.Instance.IsCanOfflineMode())
            {
                LogUtil.Log("[LoginCtrl]Can Offline Mode");
            }
        }
    }
}