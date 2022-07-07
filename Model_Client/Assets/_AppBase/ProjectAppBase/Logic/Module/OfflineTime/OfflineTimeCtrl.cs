/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp
{
    public class OfflineTimeCtrl : BaseCtrl
    {
        public static OfflineTimeCtrl Instance { get; private set; }

        private Timer loopTimer;
        private const float UpdateTimeInterval = 10f;

        #region 生命周期

        protected override void OnInit()
        {
            Instance = this;
        }

        protected override void OnDispose()
        {
            Instance = null;
        }

        #endregion 生命周期

        #region 消息

        protected override void AddListener()
        {
            AppDispatcher.Instance.AddListener(AppMsg.App_Pause_True, OnOffline);
            MainThreadDispatcher.Instance.AddListener(MainThreadMsg.App_Pause_False, OnOnline);
            AppDispatcher.Instance.AddListener(AppMsg.App_Quit, OnAppQuit);
            CtrlDispatcher.Instance.AddListener(CtrlMsg.Game_StartLater, OnGameStartLater);
        }

        protected override void RemoveListener()
        {
            AppDispatcher.Instance.RemoveListener(AppMsg.App_Pause_True, OnOffline);
            MainThreadDispatcher.Instance.RemoveListener(MainThreadMsg.App_Pause_False, OnOnline);
            AppDispatcher.Instance.RemoveListener(AppMsg.App_Quit, OnAppQuit);
            CtrlDispatcher.Instance.RemoveListener(CtrlMsg.Game_StartLater, OnGameStartLater);
        }

        protected override void AddServerListener()
        {
        }

        protected override void RemoveServerListener()
        {
        }

        #endregion 消息

        // 应用暂停
        private void OnOffline(object obj = null)
        {
            if (PreferencesMgr.Instance == null) return;
            if (PreferencesMgr.Instance.GetPreferences() == null) return;

            PreferencesMgr.Instance.Offline_timestamp = DateTimeMgr.Instance.GetServerCurrTimestamp();
            PreferencesMgr.Instance.ImmediateSendSave();
        }

        // 应用恢复
        private void OnOnline(object obj = null)
        {
            if (PreferencesMgr.Instance == null) return;
            if (PreferencesMgr.Instance.GetPreferences() == null) return;
            if (PreferencesMgr.Instance.Offline_timestamp == 0) return;

            long offlineTime = DateTimeMgr.Instance.GetServerCurrTimestamp() - PreferencesMgr.Instance.Offline_timestamp;
            CtrlDispatcher.Instance.Dispatch(CtrlMsg.OfflineTime_Inform, offlineTime);

            PreferencesMgr.Instance.Offline_timestamp = 0;
        }

        // 应用退出
        private void OnAppQuit(object obj)
        {
            OnOffline();
        }

        // 进入游戏
        private void OnGameStartLater(object obj)
        {
            OnOnline();
            if (loopTimer == null)
            {
                loopTimer = TimerMgr.Instance.CreateTimer("OfflineTimeCtrl", TimerTimeType.Time);
                loopTimer.AddLoopTimer(UpdateTimeInterval, LoopEvent);
                LoopEvent();
            }
        }

        private void LoopEvent(TimerTask timerTask = null)
        {
            if (PreferencesMgr.Instance == null) return;
            if (PreferencesMgr.Instance.GetPreferences() == null) return;
            PreferencesMgr.Instance.LastOnline_timestamp = DateTimeMgr.Instance.GetServerCurrTimestamp();
        }
    }
}