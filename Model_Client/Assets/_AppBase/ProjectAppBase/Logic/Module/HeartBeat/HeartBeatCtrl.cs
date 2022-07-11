/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using Cysharp.Threading.Tasks;
using FutureCore;
using ProjectApp.Protocol;
using UnityEngine;

namespace ProjectApp
{
    public class HeartBeatCtrl : BaseCtrl
    {
        public const string TAG = "[HeartBeatCtrl] ";
        public static HeartBeatCtrl Instance { get; private set; }

        private LoginModel userModel = null;
        private bool hasServerHeartBeat = true;
        private C2S_heartbeat heartBeatMsg = new C2S_heartbeat();
        private TimerTask heartBeatTimer = null;

        protected override void OnInit()
        {
            Instance = this;
            userModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;
        }

        protected override void OnDispose()
        {
            Instance = null;
            if (heartBeatTimer != null)
            {
                heartBeatTimer.Dispose();
            }
        }

        protected override void AddListener()
        {
            AppDispatcher.Instance.AddFinallyListener(AppMsg.WebSocketServer_ConnectSucceed, OnNetConnectSucceed);
            AppDispatcher.Instance.AddFinallyListener(AppMsg.WebSocketServer_Disconnect, OnNetDisconnect);
            ctrlDispatcher.AddFinallyListener(CtrlMsg.Login_Succeed, OnLoginSucceed);
            ctrlDispatcher.AddFinallyListener(CtrlMsg.Login_ReloginSucceed, OnLoginSucceed);            
        }

        protected override void RemoveListener()
        {
            AppDispatcher.Instance.RemoveFinallyListener(AppMsg.WebSocketServer_ConnectSucceed, OnNetConnectSucceed);
            AppDispatcher.Instance.RemoveFinallyListener(AppMsg.WebSocketServer_Disconnect, OnNetDisconnect);
            ctrlDispatcher.RemoveFinallyListener(CtrlMsg.Login_Succeed, OnLoginSucceed);
            ctrlDispatcher.RemoveFinallyListener(CtrlMsg.Login_ReloginSucceed, OnLoginSucceed);
        }

        protected override void AddServerListener()
        {

        }

        protected override void RemoveServerListener()
        {

        }        

        private void OnNetConnectSucceed(object param)
        {
            if (userModel != null && userModel.loginData != null)
            {
                UpdateServerCurrTime(userModel.loginData.info.login_time);
            }
        }

        private void OnHeartSuccess(object param)
        {
            if (heartBeatTimer != null)
            {
                heartBeatTimer.Dispose();
            }

            hasServerHeartBeat = true;            
        }

        private void OnLoginSucceed(object param)
        {
            if (userModel != null && userModel.loginData != null)
            {
                //UpdateServerCurrTime(userModel.loginData.info.login_time);
            }
        }

        private void OnS2C_heartbeat(object param = null)
        {
            if (param == null)
            {
                LogUtil.LogError(StringUtil.Concat(TAG, "S2C_heartbeat_data is null"));
                return;
            }

            // 统计
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.networkavailable_delay, 60 * 1000, false);

            hasServerHeartBeat = true;
            S2C_heartbeat_data data = SerializeUtil.ToObject<S2C_heartbeat_data>(param.ToString());
            if (data == null)
            {
                LogUtil.LogError(StringUtil.Concat(TAG, "heartbeat data is null"));
                return;
            }            

            UpdateServerCurrTime(data.ts);
        }

        private void UpdateServerCurrTime(long timestamp)
        {
            if (PreferencesMgr.Instance.GetPreferences() != null)
            {
                DateTimeMgr.Instance.SetServerCurrTimestamp(timestamp);
                ctrlDispatcher.Dispatch(CtrlMsg.HeartBeat_UpdateServerCurrTime);
                PreferencesMgr.Instance.LastOnline_timestamp = timestamp;
            }
        }

        private void OnNetDisconnect(object param)
        {
            if (heartBeatTimer != null)
            {
                heartBeatTimer.Dispose();
            }
        }
    }
}