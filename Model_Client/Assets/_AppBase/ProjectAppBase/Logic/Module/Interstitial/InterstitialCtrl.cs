/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using ProjectApp.Data;
using System;

namespace ProjectApp
{
    /// <summary>
    /// 插屏间隔控制器
    /// </summary>
    public class InterstitialCtrl : BaseCtrl
    {
        public static InterstitialCtrl Instance;
        private InsterstitialVO config;

        private int TodayShowCount
        {
            get
            {
                return PreferencesMgr.Instance.Interstitial_TodayShowCount;
            }
            set
            {
                PreferencesMgr.Instance.Interstitial_TodayShowCount = value;
            }
        }

        private int TodayClickCount
        {
            get
            {
                return PreferencesMgr.Instance.Interstitial_TodayClickCount;
            }
            set
            {
                PreferencesMgr.Instance.Interstitial_TodayClickCount = value;
            }
        }

        private int ActiveDay
        {
            get
            {
                return PreferencesMgr.Instance.Interstitial_ActiveDay;
            }
            set
            {
                PreferencesMgr.Instance.Interstitial_ActiveDay = value;
            }
        }

        private int TimeStamp
        {
            get
            {
                return PreferencesMgr.Instance.Interstitial_TimeStamp;
            }
            set
            {
                PreferencesMgr.Instance.Interstitial_TimeStamp = value;
            }
        }

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
            ctrlDispatcher.AddListener(CtrlMsg.ServerNewDays, OnNewDay);
            ctrlDispatcher.AddListener(CtrlMsg.Game_StartBefore, OnGameStart);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnInterstitialAdOpen, OnInterstitialAdOpen);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnInterstitialAdClick, OnInterstitialClick);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnInterstitialAdClose, OnInterstitialClose);
        }

        protected override void RemoveListener()
        {
            ctrlDispatcher.RemoveListener(CtrlMsg.ServerNewDays, OnNewDay);
            ctrlDispatcher.RemoveListener(CtrlMsg.Game_StartBefore, OnGameStart);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnInterstitialAdOpen, OnInterstitialAdOpen);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnInterstitialAdClick, OnInterstitialClick);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnInterstitialAdClose, OnInterstitialClose);
        }  

        protected override void AddServerListener()
        {
        }

        protected override void RemoveServerListener()
        {
        }

        #endregion 消息

        #region 回调

        private void OnGameStart(object param)
        {  
            // 获取配置
            config = InsterstitialVOModel.Instance.GetFirstVO();
        }

        private void OnNewDay(object param)
        {
            // 重置插屏天数
            TodayShowCount = 0;
            // 激活天数增加
            ActiveDay++;
            // 重置插屏点击次数
            TodayClickCount = 0;
        }

        #endregion 回调

        public void ShowInterstitial(Action<bool> callback = null)
        {
            // 激活天数是否满足
            if (ActiveDay <= config.activation)
            {
                LogUtil.LogWarning("[InterstitialCtrl] ShowInterstitial() return ActiveDay: " + ActiveDay);
                return;
            }
            // 每日点击次数上限
            if (TodayClickCount >= config.intervalInvalid)
            {
                LogUtil.LogWarning("[InterstitialCtrl] ShowInterstitial() return TodayClickCount: " + TodayClickCount);
                return;
            }
            // 每日展示次数上限
            if (TodayShowCount >= config.insertMax)
            {
                LogUtil.LogWarning("[InterstitialCtrl] ShowInterstitial() return TodayShowCount: " + TodayShowCount);
                return;
            }
            // 冷却时间戳是否满足
            int timeStampGap = TimeStamp - (int)DateTimeMgr.Instance.GetCurrTimestamp();
            if (timeStampGap > 0)
            {
                LogUtil.LogWarning("[InterstitialCtrl] ShowInterstitial() return timeStampGap: " + timeStampGap);
                return;
            }
            // 调用展示插屏
            AdsHelper.ShowClosePanelInterstitialAd(callback);
        }

        public void OnInterstitialAdOpen(object param = null)
        {
            string sdk = param as string;

#if UNITY_IOS
            // 安卓播放视频广告的时候会自动挂起游戏，iOS不能，开启UNITY_STANDALONE是为了方便调试
            GameMgr.Instance.Pause();
#endif
        }

        public void OnInterstitialClick(object param = null)
        {
            LogUtil.LogWarning("[InterstitialCtrl] OnInterstitialClick() ");
            // 累计点击次数
            TodayClickCount++;
        }

        public void OnInterstitialClose(object param = null)
        {
            LogUtil.LogWarning("[InterstitialCtrl] OnInterstitialClose() ");
            // 累计展示次数
            TodayShowCount++;
            // 累加时间戳
            TimeStamp = (int)DateTimeMgr.Instance.GetCurrTimestamp() + config.insertInterval;

#if UNITY_IOS
            // 安卓播放视频广告的时候会自动挂起游戏，iOS不能，开启UNITY_STANDALONE是为了方便调试
            GameMgr.Instance.Resume();
#endif
        }
    }
}