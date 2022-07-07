using System;
using FutureCore;

namespace ProjectApp
{
    public class ReconnectUICtrl : BaseUICtrl
    {
        ReconnectUI ui;
        bool openStatus = true;

        public override void Init()
        {
            if (CommonConfig.ReconnectUI)
            {
                base.Init();
            }
        }

        public override void Dispose()
        {
            if (CommonConfig.LoginUI)
            {
                base.Dispose();
            }
        }

        #region 生命周期
        protected override void OnInit()
        {
        }

        protected override void OnDispose()
        {
        }

        public override void OpenUI(object args = null)
        {
            if (!WeakNetworkCtrl.Instance.IsCanShowReconnectUI()) return;

            if (!AppGlobal.IsShowDisconnectionTips) return;
            if (!openStatus || CommonConst.UpdataStatus) return;
  
            if (this.ui == null)
            {
                ui = new ReconnectUI(this);
                ui.Open(args);
            }
        }

        public override void CloseUI(object args = null)
        {
            if (ui != null)
            {
                ui.Close();
                ui = null;
                uiCtrlDispatcher.Dispatch(UICtrlMsg.GameLoadingUI_Close);
            }
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            uiCtrlDispatcher.AddListener(UICtrlMsg.ReconnectUI_Open, OpenUI);
            uiCtrlDispatcher.AddListener(UICtrlMsg.C_OpenReconnectUI, OpenUI);
            uiCtrlDispatcher.AddListener(UICtrlMsg.C_CloseReconnectUI, CloseUI);
            uiCtrlDispatcher.AddListener(UICtrlMsg.OpenReconnectFunc, SetReconnectFuncOpen);
            uiCtrlDispatcher.AddListener(UICtrlMsg.CloseReconnectFunc, SetReconnectFuncClose);

            ctrlDispatcher.AddListener(CtrlMsg.WeakNetwork_LoginSucceed, CloseUI);
            ctrlDispatcher.AddListener(CtrlMsg.Login_Succeed, CloseUI);
            ctrlDispatcher.AddListener(CtrlMsg.Login_ReloginSucceed, CloseUI);
            ctrlDispatcher.AddListener(CtrlMsg.Login_ReloginWaitDelay, ShowWaitTextUI);//展示loading
            ctrlDispatcher.AddListener(CtrlMsg.Login_ReloginWaitDelayEnd, CloseWaitTextUI);//关闭loading
            ctrlDispatcher.AddListener(CtrlMsg.EnableReconect, EnableReconnect);//激活断线

            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.NetworkStatusChanged_False, SetReconnectFalseStatue);
            
            AppDispatcher.Instance.AddListener(AppMsg.WebSocketServer_ConnectSucceed, ConnectSuccess);
            AppDispatcher.Instance.AddListener(AppMsg.WebSocketServer_ConnectNoNetwork, ReconnectStatusChange);
            AppDispatcher.Instance.AddListener(AppMsg.WebSocketServer_ConnectStart, DelayOpen);
            AppDispatcher.Instance.AddListener(AppMsg.WebSocketServer_ConnectFailed, ReconnectStatusChange);
            AppDispatcher.Instance.AddListener(AppMsg.WebSocketServer_Disconnect, ReconnectStatusChange);
            AppDispatcher.Instance.AddListener(AppMsg.WebSocketServer_Exception, ReconnectStatusChange);
            AppDispatcher.Instance.AddListener(AppMsg.WebSocketServer_HeatBeatOverTime, ReconnectStatusChange);
        }

        protected override void RemoveListener()
        {
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.ReconnectUI_Open, OpenUI);
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.C_OpenReconnectUI, OpenUI);
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.C_CloseReconnectUI, CloseUI);
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.OpenReconnectFunc, SetReconnectFuncOpen);
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.CloseReconnectFunc, SetReconnectFuncClose);

            ctrlDispatcher.RemoveListener(CtrlMsg.WeakNetwork_LoginSucceed, CloseUI);
            ctrlDispatcher.RemoveListener(CtrlMsg.Login_Succeed, CloseUI);
            ctrlDispatcher.RemoveListener(CtrlMsg.Login_ReloginSucceed, CloseUI);
            ctrlDispatcher.RemoveListener(CtrlMsg.Login_ReloginWaitDelay, ShowWaitTextUI);
            ctrlDispatcher.RemoveListener(CtrlMsg.Login_ReloginWaitDelayEnd, CloseWaitTextUI);
            ctrlDispatcher.RemoveListener(CtrlMsg.EnableReconect, EnableReconnect);//激活断线


            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.NetworkStatusChanged_False, SetReconnectFalseStatue);

            // WebSocket服务器连接无网络
            AppDispatcher.Instance.RemoveListener(AppMsg.WebSocketServer_ConnectSucceed, ConnectSuccess);
            AppDispatcher.Instance.RemoveListener(AppMsg.WebSocketServer_ConnectNoNetwork, ReconnectStatusChange);
            AppDispatcher.Instance.RemoveListener(AppMsg.WebSocketServer_ConnectStart, DelayOpen);
            AppDispatcher.Instance.RemoveListener(AppMsg.WebSocketServer_ConnectFailed, ReconnectStatusChange);
            AppDispatcher.Instance.RemoveListener(AppMsg.WebSocketServer_Disconnect, ReconnectStatusChange);
            AppDispatcher.Instance.RemoveListener(AppMsg.WebSocketServer_Exception, ReconnectStatusChange);
            AppDispatcher.Instance.RemoveListener(AppMsg.WebSocketServer_HeatBeatOverTime, ReconnectStatusChange);
        }

        protected override void AddServerListener()
        {

        }

        protected override void RemoveServerListener()
        {

        }
        #endregion

        #region Event
        public void SetReconnectFalseStatue(object param)
        {
            OpenUI();
        }

        private void ConnectSuccess(object param)
        {
            TimerUtil.Simple.RemoveTimer(OpenReconnectFunc);
        }

        public void DelayOpen(object param = null)
        {
            TimerUtil.Simple.AddTimer(10, OpenReconnectFunc);
        }

        public void OpenReconnectFunc()
        {
            SetReconnectFuncOpen(null);
        }

        public void ReconnectStatusChange(object args = null)
        {
            uiCtrlDispatcher.Dispatch(UICtrlMsg.GameLoadingUI_Close);
            OpenUI();
        }

        public void ShowWaitTextUI(object args = null)
        {
            OpenUI();
            if (ui != null)
            {
                ui.ShowWait(args);
            }
        }

        public void CloseWaitTextUI(object args = null)
        {
            OpenUI();
          if (ui != null)
            {
                ui.CloseWait(args);
            }
        }

        public void SetReconnectFuncOpen(object param = null)
        {
            if (WSNetMgr.Instance.State != WSNetState.LoginSuccess)
            {
                this.openStatus = true;
                OpenUI();
            }
        }

        public void SetReconnectFuncClose(object param)
        {
            this.openStatus = false;
        }

        public void OnRetry()
        {
            if (WSNetMgr.Instance.State == WSNetState.LoginFailed_MustDelay
                || WSNetMgr.Instance.State == WSNetState.PreferencesParseError
                || WSNetMgr.Instance.State == WSNetState.ConfigParseError
                || WSNetMgr.Instance.State == WSNetState.ConfigSerializeError) return;

            LoginCtrl.Instance.ConnectLogin();
            uiCtrlDispatcher.Dispatch(UICtrlMsg.GameLoadingUI_Open);
        }

        public void OnHelp()
        {
            if (ui != null)
            {
                Channel.Current.feedback(this.ui.GetErrStatus);
            }
        }

        private void EnableReconnect(object param)
        {
            if (!NetConst.IsNetAvailable && !WSNetMgr.Instance.isConnected)
            {
                OpenUI();
            }
        }
        #endregion
    }
}
