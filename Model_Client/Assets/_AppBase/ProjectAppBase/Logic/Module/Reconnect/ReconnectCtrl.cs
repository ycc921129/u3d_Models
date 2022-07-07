/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp
{
    public class ReconnectCtrl : BaseCtrl
    {
        public static ReconnectCtrl Instance { get; private set; }

        private bool isAutoReConnecting = false;
        private int autoReConnectTimes = 0;
        private float autoReConnectDelayTime = 1;

        private float checkConnectTime = 10f;
        private TimerTask checkConnectTimer = null;

        protected override void OnInit()
        {
            Instance = this;
        }
        protected override void OnDispose()
        {
            Instance = null;

            if (checkConnectTimer != null)
            {
                checkConnectTimer.Dispose();
            }
        }

        protected override void AddListener()
        {
            ChannelDispatcher.Instance.AddPriorityListener(ChannelRawMsg.NetworkStatusChanged_True, OnNetworkStatusChangedTrue);
            MainThreadDispatcher.Instance.AddPriorityListener(MainThreadMsg.App_Pause_False, OnAppPauseFalse);

            AppDispatcher.Instance.AddPriorityListener(AppMsg.WebSocketServer_ConnectFailed, OnAutoReConnectLogin);
            AppDispatcher.Instance.AddPriorityListener(AppMsg.WebSocketServer_Disconnect, OnAutoReConnectLogin);
            AppDispatcher.Instance.AddPriorityListener(AppMsg.WebSocketServer_Exception, OnConnectWebSocketException);

            CtrlDispatcher.Instance.AddListener(CtrlMsg.Game_Start, OnGameStart);
        }

        protected override void RemoveListener()
        {
            ChannelDispatcher.Instance.RemovePriorityListener(ChannelRawMsg.NetworkStatusChanged_True, OnNetworkStatusChangedTrue);
            MainThreadDispatcher.Instance.RemovePriorityListener(MainThreadMsg.App_Pause_False, OnAppPauseFalse);

            AppDispatcher.Instance.RemovePriorityListener(AppMsg.WebSocketServer_ConnectFailed, OnAutoReConnectLogin);
            AppDispatcher.Instance.RemovePriorityListener(AppMsg.WebSocketServer_Disconnect, OnAutoReConnectLogin);
            AppDispatcher.Instance.RemovePriorityListener(AppMsg.WebSocketServer_Exception, OnConnectWebSocketException);

            CtrlDispatcher.Instance.RemoveListener(CtrlMsg.Game_Start, OnGameStart);
        }

        protected override void AddServerListener()
        {
        }
        protected override void RemoveServerListener()
        {
        }

        #region 逻辑
        private void OnNetworkStatusChangedTrue(object param)
        {
            if (!WSNetMgr.Instance.hasFirstConnect) return;

            // 立即重连
            ResumeAutoReConnectState();
            AutoReConnectLogin();
        }

        private void OnAppPauseFalse(object param)
        {
            if (!WSNetMgr.Instance.hasFirstConnect) return;

            // 立即重连
            if (App.GetCurrProgressStateValue() < (int)ProgressState.ConnectLogin_30)
                return;
            if (WSNetMgr.Instance.isConnected)
                return;
            if (!NetConst.IsNetAvailable)
                return;
            ResumeAutoReConnectState();
            AutoReConnectLogin();
        }

        private void OnConnectWebSocketException(object param)
        {
            // 统计连接错误
            ChannelMgr.Instance.SendStatisticEventWithParam(StatisticConst.networkavailable_connect_error, Channel.Current.network);
            // 尝试重连
            AutoReConnectLogin();
        }

        public void ResumeAutoReConnectState()
        {
            isAutoReConnecting = false;
            autoReConnectTimes = 0;
            autoReConnectDelayTime = 1;
        }

        private void OnAutoReConnectLogin(object param)
        {
            AutoReConnectLogin();
        }

        private void OnGameStart(object obj)
        {
            checkConnectTimer = TimerUtil.General.AddLoopTimer(checkConnectTime, (timer) =>
            {
                if (WSNetMgr.Instance.isConnected)
                    return;
                if (WSNetMgr.Instance.isConnecting)
                    return;
                if (!NetConst.IsNetAvailable)
                    return;

                ResumeAutoReConnectState();
                AutoReConnectLogin();
            });
        }

        private void AutoReConnectLogin()
        {
            LogUtil.LogFormat("[LoginCtrl]AutoReConnectLogin IsAppPause:{0} IsConnected:{1} IsNetAvailable:{2} isDelayLogin:{3} isAutoReConnecting:{4}",
                ProjectApplication.Instance.IsAppPause, WSNetMgr.Instance.isConnected, NetConst.IsNetAvailable, LoginCtrl.Instance.isDelayLogining, isAutoReConnecting);

            // 断线设置状态
            AppGlobal.IsLoginSucceed = false;

            if (ProjectApplication.Instance.IsAppPause)
                return;
            if (WSNetMgr.Instance.isConnected)
                return;
            if (!NetConst.IsNetAvailable)
                return;
            if (LoginCtrl.Instance.isDelayLogining)
                return;
            if (isAutoReConnecting)
                return;

            isAutoReConnecting = true;
            autoReConnectTimes++;
            if (autoReConnectTimes > 1)
            {
                autoReConnectDelayTime *= 2;
                if (autoReConnectDelayTime > 60)
                {
                    autoReConnectDelayTime = 60;
                }
            }
            TimerUtil.Simple.AddTimer(autoReConnectDelayTime, () =>
            {
                if (WSNetMgr.Instance.isConnected)
                    return;
                if (LoginCtrl.Instance.isDelayLogining)
                    return;
                bool result = LoginCtrl.Instance.ConnectLogin();
                if (!result && !WSNetMgr.Instance.isConnected && NetConst.IsNetAvailable)
                {
                    isAutoReConnecting = false;
                    AutoReConnectLogin();
                }
            });
        }
        #endregion
    }
}