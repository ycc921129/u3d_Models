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
        }

        private void OnAppPauseFalse(object param)
        {           
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
            
        }

        private void AutoReConnectLogin()
        {

        }
        #endregion
    }
}