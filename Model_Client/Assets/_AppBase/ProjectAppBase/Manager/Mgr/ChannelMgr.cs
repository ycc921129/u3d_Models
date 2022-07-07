/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System;
using FutureCore;
using FutureCore.Data;
using ProjectApp.Data;
using ProjectApp.Protocol;
using System.Collections.Generic;

namespace ProjectApp
{
    public sealed partial class ChannelMgr : BaseMgr<ChannelMgr>
    {
        private const string VideoAdFailedInfo = "Watching video failed, Please try again.";
        private const string VideoAdLoadingInfo = "Watch video loading, Please try again.";
        private const float LoopCheckVideoAdTime = 5f;
        
        // 插屏广告回调
        private Action<bool> interstitialAdCallBack;
        // 视频广告回调
        private Action<bool> videoAdCallBack;
        // 时间事件统计
        private Dictionary<string, double> timeEventStatisticDict = new Dictionary<string, double>();

        // 视频广告状态检查计时器
        private TimerTask videoAdCheckTimer;
        private bool isVideoAdFinish = false;
        private Action<bool> onVideoAdStateFunc;

        // 是否GM系统跳过广告
        public bool IsGMPassAds = false;

        // 观看插屏广告消息数据
        private CommonMsgData watchInterstitialMsgData = new CommonMsgData(CtrlCommonMsg.WatchInterstitial, 1);
        // 观看视频广告消息数据
        private CommonMsgData watchVideoMsgData = new CommonMsgData(CtrlCommonMsg.WatchVideo, 1);

        public override void Init()
        {
            base.Init();
            AddListener();
        }

        public override void StartUp()
        {
            base.StartUp();
            InitTimer();
        }

        public override void Dispose()
        {
            base.Dispose();
            RemoveListener();
            DisposeTimer();
        }

        private void AddListener()
        {
            #region Channel调用
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.ReportError, ReportError);
            #endregion

            #region Channel回调
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnSdkChannel, OnSdkChannel);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnLoginBindToken, OnLoginBindToken);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnSendAdwords, OnSendAdwords);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnInterstitialAdClose, OnInterstitialAdClose);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnVideoAdLoadStart, OnVideoAdLoadStart);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnVideoAdLoaded, OnVideoAdLoaded);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnVideoAdFaiToLoaded, OnVideoAdFaiToLoaded);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnVideoAdOpen, OnVideoAdOpen);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnVideoAdClick, OnVideoAdClick);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnVideoAdRewarded, OnVideoAdRewarded);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnVideoAdClosed, OnVideoAdClosed);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnAdEvent, OnAdEvent);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnHandlePurchase, OnHandlePurchase);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnBillingSubscribePurchased, OnBillingSubscribePurchased);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnBillingIapPurchased, OnBillingIapPurchased);
            #endregion

            AppDispatcher.Instance.AddListener(AppMsg.UI_ShowPlatformTipsUI, OnShowPlatformTipsUI);
        }

        private void RemoveListener()
        {
            #region Channel调用
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.ReportError, ReportError);
            #endregion

            #region Channel回调
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnSdkChannel, OnSdkChannel);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnLoginBindToken, OnLoginBindToken);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnSendAdwords, OnSendAdwords);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnInterstitialAdClose, OnInterstitialAdClose);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnVideoAdLoadStart, OnVideoAdLoadStart);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnVideoAdLoaded, OnVideoAdLoaded);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnVideoAdFaiToLoaded, OnVideoAdFaiToLoaded);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnVideoAdOpen, OnVideoAdOpen);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnVideoAdClick, OnVideoAdClick);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnVideoAdRewarded, OnVideoAdRewarded);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnVideoAdClosed, OnVideoAdClosed);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnAdEvent, OnAdEvent);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnHandlePurchase, OnHandlePurchase);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnBillingSubscribePurchased, OnBillingSubscribePurchased);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnBillingIapPurchased, OnBillingIapPurchased);
            #endregion

            AppDispatcher.Instance.RemoveListener(AppMsg.UI_ShowPlatformTipsUI, OnShowPlatformTipsUI);
        }

        private void InitTimer()
        {
            videoAdCheckTimer = TimerUtil.General.AddLoopTimer(LoopCheckVideoAdTime, OnCheckVideoAd);
            videoAdCheckTimer.SetActive(false);
        }

        private void DisposeTimer()
        {
            videoAdCheckTimer.Dispose();
            videoAdCheckTimer = null;
        }

        #region 封装: 视频广告状态

        /// <summary>
        /// 开始绑定广告视频状态
        /// </summary>
        public void StartBindVideoAdState(Action<bool> onVideoAdStateFunc)
        {
            this.onVideoAdStateFunc = onVideoAdStateFunc;
            if (!isVideoAdFinish)
            {
                isVideoAdFinish = Channel.Current.isPreLoadVideoAdSuccess(AdsConst.Video_AdId);
                if (!isVideoAdFinish)
                {
                    if (onVideoAdStateFunc != null)
                    {
                        onVideoAdStateFunc(false);
                    }
                    videoAdCheckTimer.SetActive(true);
                    Channel.Current.preLoadVideoAd(AdsConst.Video_AdId);
                }
            }
            else
            {
                if (onVideoAdStateFunc != null)
                {
                    onVideoAdStateFunc(true);
                }
            }
        }

        /// <summary>
        /// 结束绑定广告视频状态
        /// </summary>
        public void EndBindVideoAdState()
        {
            onVideoAdStateFunc = null;
            videoAdCheckTimer.SetActive(false);
        }

        private void RefreshVideoAdState()
        {
            isVideoAdFinish = Channel.Current.isPreLoadVideoAdSuccess(AdsConst.Video_AdId);
            if (isVideoAdFinish) return;

            if (onVideoAdStateFunc != null)
            {
                onVideoAdStateFunc(false);
            }
            videoAdCheckTimer.SetActive(true);
            Channel.Current.preLoadVideoAd(AdsConst.Video_AdId);
        }

        private void OnCheckVideoAd(TimerTask timer)
        {
            if( videoAdCallBack != null )
                return;
            if (isVideoAdFinish)
            {
                videoAdCheckTimer.SetActive(false);
                return;
            }

            isVideoAdFinish = Channel.Current.isPreLoadVideoAdSuccess(AdsConst.Video_AdId);
            if (!isVideoAdFinish)
            {
                Channel.Current.preLoadVideoAd(AdsConst.Video_AdId);
            }
            else
            {
                if (onVideoAdStateFunc != null)
                {
                    onVideoAdStateFunc(true);
                    videoAdCheckTimer.SetActive(false);
                }
            }
        }

        #endregion 封装: 视频广告状态

        #region 调用

        #region 调用: SDK
        public void StatisticSdkChannel()
        {
            string sdkChannel = PrefsUtil.ReadString(PrefsKeyConst.ChannelMgr_sdkChannel);
            if (!string.IsNullOrEmpty(sdkChannel))
            {
                UserSessionCtrl.Instance.StatisticState("sdkChannel", sdkChannel);
            }
        }
        #endregion        

        #region 调用: 插屏广告
        /// <summary>
        /// 缓存关闭面板插屏广告
        /// </summary>
        public void preClosePanelLoadInterstitialAd()
        {
            Channel.Current.preLoadInterstitialAd(AdsConst.ClosePanel_InterstitialAdId);
        }

        /// <summary>
        /// 缓存获取奖励面板关闭时插屏广告
        /// </summary>
        public void preGetRewardLoadInterstitialAd()
        {
            Channel.Current.preLoadInterstitialAd(AdsConst.GetReward_InterstitialAdId);
        }

        /// <summary>
        /// 缓存游戏结束插屏广告
        /// </summary>
        public void preGetRewardInterstitialAd()
        {
            Channel.Current.preLoadInterstitialAd(AdsConst.GameOver_InterstitialAdId);
        }

        /// <summary>
        /// 显示关闭面板插屏广告
        /// </summary>
        public void ShowClosePanelInterstitialAd(Action<bool> callBack = null)
        {
            ShowInterstitialAd(AdsConst.ClosePanel_InterstitialAdId, callBack);
        }

        /// <summary>
        /// 显示获取奖励面板关闭时插屏广告
        /// </summary>
        public void ShowGetRewardInterstitialAd(Action<bool> callBack = null)
        {
            ShowInterstitialAd(AdsConst.GetReward_InterstitialAdId, callBack);
        }

        /// <summary>
        /// 显示游戏结束插屏广告
        /// </summary>
        public void ShowGameEndInterstitialAd(Action<bool> callBack = null)
        {
            ShowInterstitialAd(AdsConst.GameOver_InterstitialAdId, callBack);
        }

        /// <summary>
        /// 显示插屏
        /// </summary>
        public void ShowInterstitialAd(string adPlacement, Action<bool> callBack = null)
        {
            if (!IsPreLoadIntersititialAd()) 
            {
                if (callBack != null)
                {
                    callBack(false);
                }
                return;
            }
            ImmediatelyShowInterstitialAd(adPlacement, callBack);
        }

        public void ImmediatelyShowInterstitialAd(string adPlacement, Action<bool> callBack = null)
        {
            interstitialAdCallBack = callBack;
            // 插屏
            Channel.Current.showInterstitialAd(adPlacement);
            // 统计
            LoginModel loginModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;
            if (loginModel == null)
            {
                return;
            }
            if (loginModel.isNewUser)
            {
                SendStatisticEvent(StatisticConst.new_user_interstitial_ad_show);
            }
            else
            {
                SendStatisticEvent(StatisticConst.interstitial_ad_show);
            }
        }

        /// <summary>
        /// 获取客户端可否显示插屏
        /// </summary>
        public bool IsPreLoadIntersititialAd()
        {
            return Channel.Current.isPreLoadIntersititialAd(AdsConst.ClosePanel_InterstitialAdId);
        }
        #endregion

        #region 调用: 视频广告
        public void PreloadVideoAd()
        {
            PreloadVideoAd(AdsConst.Video_AdId);
        }
        /// <summary>
        /// 预加载视频广告
        /// (SDK层有做缓存逻辑, 业务层可以不调用)
        /// </summary>
        public void PreloadVideoAd(string videoAdId)
        {
            Channel.Current.preLoadVideoAd(videoAdId);
        }

        public void ShowVideoAd(Action<bool> callback)
        {
            ShowVideoAd(AdsConst.Video_AdId, callback);
        }

        /// <summary>
        /// 播放视频
        /// </summary>
        public void ShowVideoAd(string videoAdId, Action<bool> callback)
        {
            // 统计触摸广告按钮
            SendStatisticEvent(StatisticConst.video_sdk_touch_button);

            // 是否GM系统跳过广告
            if (!Channel.IsRelease)
            {
                if (IsGMPassAds)
                {
                    LogUtil.Log("[ChannelMgr]ShowVideoAd: Is GM Pass Ads.");
                    callback(true);
                    return;
                }
            }

            // 视频预加载失败
            if (!Channel.Current.isPreLoadVideoAdSuccess(videoAdId))
            {
                Channel.Current.preLoadVideoAd(videoAdId);
                App.ShowPlatformTipsUI(VideoAdLoadingInfo);
                callback(false);
                return;
            }

            // 视频预加载成功
            App.DisplayWaitTimeUI(5f, () => App.ShowPlatformTipsUI(VideoAdFailedInfo));
            videoAdCallBack = callback;
            // 视频广告
            Channel.Current.showVideoAd(videoAdId);
            // 统计
            LoginModel loginModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;
            if (loginModel == null)
            {
                return;
            }

            if (loginModel.isNewUser)
            {
                SendStatisticEvent(StatisticConst.new_user_video_ad_play);
            }
            else
            {
                SendStatisticEvent(StatisticConst.video_ad_play);
            }
        }
        #endregion

        #region 调用: 视频广告打点     
        /// <summary>
        /// 视频按钮展示（曝光）
        /// </summary>
        public void LogRewardedAdBtnExposure(string placementEntry)
        {
            Channel.Current.logRewardedAdBtnExposure(placementEntry);
        }

        /// <summary>
        /// 点击视频按钮触发奖励广告 
        /// </summary>
        public void TouchAndShowRewardedAd(string placementEntry, Action<bool> callback)
        {
            // 统计触摸广告按钮
            SendStatisticEvent(StatisticConst.video_sdk_touch_button);

            videoAdCallBack = callback;  

            // 视频广告
            bool isShowSuccess = Channel.Current.touchAndShowRewardedAd(placementEntry);            
              
            // 视频预加载失败  
            if (!isShowSuccess)
            {
                App.ShowPlatformTipsUI(VideoAdLoadingInfo);
                videoAdCallBack(false);
                videoAdCallBack = null;
                return;
            }

            // 统计
            LoginModel loginModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;
            if (loginModel == null)
            {
                return;
            }

            if (loginModel.isNewUser)
            {
                SendStatisticEvent(StatisticConst.new_user_video_ad_play);
            }
            else
            {
                SendStatisticEvent(StatisticConst.video_ad_play);
            }
        }
        #endregion

        #region 调用: 分享
        /// <summary>
        /// 分享
        /// </summary>
        public void Share() 
        {
            string url = InviteCtrl.Instance.Invite_url;
            string code = InviteCtrl.Instance.Invite_Code;
            LogUtil.Log("[ChannelMgr]分享 url:" + url);
            LoginModel userModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;

            ShareContentData shareContent = new ShareContentData();
            string t1 =
                StringUtil.Concat("I am so lucky to get gift cards by just few taps! Want to be lucky too? Use my referral:", url, "\n");

            shareContent.more =
                StringUtil.Concat(t1, "Won't you like to get gifts every time you're bored? Use my referral code: ", code.ToString());
            Channel.Current.share(SerializeUtil.ToJson(shareContent));

            // 统计 
            if (userModel.isNewUser)
            {
                SendStatisticEvent(StatisticConst.new_user_share);
            }
            else
            {
                SendStatisticEvent(StatisticConst.share);
            }
        }
        #endregion

        #region 调用: 友盟统计事件
        /// <summary>
        /// 统计事件，不带参数
        /// </summary>
        public void SendStatisticEvent(string eventKey)
        {
            Channel.Current.onEvent(eventKey);
        }

        /// <summary>
        /// 统计事件，带参数
        /// </summary>
        public void SendStatisticEventWithParam(string eventKey, string param)
        {
            Channel.Current.onEventParam(eventKey, param);
        }

        /// <summary>
        /// 统计事件，带参数
        /// </summary>
        public void SendStatisticEventWithTime(string eventKey, int time, bool isLog = true)
        {
            Channel.Current.onEventValue(eventKey, time, isLog);
        }

        /// <summary>
        /// 开始统计时长事件
        /// </summary>
        public void StartStatisticTimeEvent(string eventKey)
        {
            if (!timeEventStatisticDict.ContainsKey(eventKey))
            {
                timeEventStatisticDict.Add(eventKey, DateTimeMgr.Instance.GetTimestampInMilliSecond(DateTime.Now));
            }
            else
            {
                timeEventStatisticDict[eventKey] = DateTimeMgr.Instance.GetTimestampInMilliSecond(DateTime.Now);
            }
        }

        /// <summary>
        /// 结束统计时长事件
        /// 毫秒
        /// </summary>
        public void EndStatisticTimeEvent(string eventKey, double maxMillSecond = 4294967295, bool isLog = true)
        {
            if (timeEventStatisticDict.ContainsKey(eventKey))
            {
                double startTime = timeEventStatisticDict[eventKey];
                double endTime = DateTimeMgr.Instance.GetTimestampInMilliSecond(DateTime.Now);
                double offsetMillSecond = endTime - startTime;
                timeEventStatisticDict.Remove(eventKey);
                if (offsetMillSecond < 0 || offsetMillSecond > maxMillSecond) return;

                Channel.Current.onEventValue(eventKey, (int)offsetMillSecond, isLog);
            }
        }

        /// <summary>
        /// 删除统计时长事件
        /// </summary>
        public void DeleteStatisticTimeEvent(string eventKey)
        {
            if (timeEventStatisticDict.ContainsKey(eventKey))
            {
                timeEventStatisticDict.Remove(eventKey);
            }
        }
        #endregion

        #region 调用: FaceBook统计事件
        public void SendFBEvent(string key)
        {
            Channel.Current.onFBEvent(key);
        }

        public void SendFBEvent(string key, double value)
        {
            Channel.Current.onFBEvent(key, value);
        }

        public void SendFBEvent(string key, string value)
        {
            Channel.Current.onFBEvent(key, value);
        }

        public void SendFBEvent(string key, Dictionary<string, object> parameters)
        {
            SendFBEvent(key, SerializeUtil.ToJson<Dictionary<string, object>>(parameters));
        }

        public void SendFBEvent(string key, double valueToSum, string parametersJson)
        {
            Channel.Current.onFBEvent(key, valueToSum, parametersJson);
        }

        public void SendFBEvent(string key, double valueToSum, Dictionary<string, object> parameters)
        {
            SendFBEvent(key, valueToSum, SerializeUtil.ToJson<Dictionary<string, object>>(parameters));
        }
        #endregion

        #region 调用: (旧)内购/订阅
        private void OnHandlePurchase(object args)
        {
            PurchaseData purchaseData = SerializeUtil.ToObject<PurchaseData>((string)args);
            ChannelDispatcher.Instance.Dispatch(ChannelMsg.OnHandlePurchase, purchaseData);
        }
        #endregion

        #region 调用: (新)内购/订阅
        private void OnBillingSubscribePurchased(object args)
        {
            PurchaseData purchaseData = SerializeUtil.ToObject<PurchaseData>((string)args);
            ChannelDispatcher.Instance.Dispatch(ChannelMsg.OnBillingSubscribePurchased, purchaseData);
        }

        private void OnBillingIapPurchased(object args)
        {
            IapPurchaseData iapPurchaseData = SerializeUtil.ToObject<IapPurchaseData>((string)args);
            ChannelDispatcher.Instance.Dispatch(ChannelMsg.OnBillingIapPurchased, iapPurchaseData);
        }
        #endregion

        #region 调用: 上报异常
        /// <summary>
        /// 上报异常
        /// </summary>
        private void ReportError(object param)
        {
            string info = param as string;
            Channel.Current.reportError(info);
        }
        #endregion

        #endregion

        #region 回调

        #region 回调: SDK
        /// <summary>
        /// 响应SDK渠道回调
        /// </summary>
        public void OnSdkChannel(object param)
        {
            string sdkChannel = (string)param;
            if (!string.IsNullOrEmpty(sdkChannel) && sdkChannel.ToLower() != "organic")
            {
                PrefsUtil.WriteString(PrefsKeyConst.ChannelMgr_sdkChannel, sdkChannel);
                StatisticSdkChannel();
            }
        }
        #endregion

        #region 回调: 验证登录
        private void OnLoginBindToken(object param)
        {
            string info = param as string;
            FirebaseUserInfo userInfo = SerializeUtil.ToObject<FirebaseUserInfo>(info);
            ChannelDispatcher.Instance.Dispatch(ChannelMsg.OnLoginBindToken, userInfo);
        }
        #endregion

        #region 回调: 平台相关
        public void OnSendAdwords(object param = null)
        {
            string adwords = Channel.Current.getAdWords();
            if (string.IsNullOrEmpty(adwords)) return;

            C2S_adwords req = new C2S_adwords();
            req.data = new C2S_adwords_data();
            req.data.referrer = adwords;

            // 登录成功才能发送Adwords
            // 要放在登录成功之后才发送
            if (WSNetMgr.Instance.Send(req))
            {
                //Channel.Current.clearAdWords();
                LogUtil.Log("[ChannelMgr]OnSendAdwords adwords:" + adwords);
            }
        }
        #endregion

        #region 回调: 插屏广告
        private void OnInterstitialAdClose(object param)
        {
            string sdk = param as string;
            if (interstitialAdCallBack != null)
            {
                interstitialAdCallBack(true);
                interstitialAdCallBack = null;
            }

            CtrlDispatcher.Instance.Dispatch(CtrlMsg.CommonMsg, watchInterstitialMsgData);
        }
        #endregion

        #region 回调: 视频广告
        private void OnVideoAdLoadStart(object param)
        {
            string sdk = param as string;
        }

        private void OnVideoAdLoaded(object param)
        {
            string sdk = param as string;
        }

        private void OnVideoAdFaiToLoaded(object param)
        {
            string sdk = param as string;
            if (videoAdCallBack != null)
            {
                videoAdCallBack(false);
                videoAdCallBack = null;
            }
            App.HideWaitUI();  
        }

        private void OnVideoAdOpen(object param)
        {
            string sdk = param as string;
            App.HideWaitUI();
        }

        private void OnVideoAdClick(object param)
        {
            string sdk = param as string;
        }

        private void OnVideoAdRewarded(object param)
        {
            string sdk = param as string;
            if (videoAdCallBack != null)
            {
                videoAdCallBack(true);
                videoAdCallBack = null;
            }
            RefreshVideoAdState();
            App.HideWaitUI();

            // 统计
            LoginModel userModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;
            if (userModel.isNewUser)
            {
                SendStatisticEvent(StatisticConst.new_user_video_ad_complete);
            }
            else
            {
                SendStatisticEvent(StatisticConst.video_ad_complete);
            }

            CtrlDispatcher.Instance.Dispatch(CtrlMsg.CommonMsg, watchVideoMsgData);
        }

        private void OnVideoAdClosed(object param)
        {
            string sdk = param as string;
            if (videoAdCallBack != null)
            {
                videoAdCallBack(false);
                videoAdCallBack = null;
            }
            RefreshVideoAdState();
            App.HideWaitUI();
        }
        #endregion

        #region 回调: 广告统计
        /// <summary>
        /// 发送广告统计
        /// </summary>
        /// <param name="adType">广告类型 "interstitial", "video",</param>
        /// <param name="adEvent">事件 "request", "click", "impression"</param>
        /// <param name="gameEntry">广告入口</param>
        public void SendAdEvent(string sdk, string type, string id, string name, string eventname, double revenue)
        {
            // 上报
            Channel.Current.logAd(sdk, type, id, name, eventname, revenue);
            // 统计
            string key = type + "_sdk_" + eventname;
            Channel.Current.onEventParam(key, sdk);
            LogUtil.Log("[ChannelMgr]SendAdEvent:" + key + " | " + sdk);
        }

        private void OnAdEvent(object param)
        {
            // 走CoreSdk统计, Channel不再回调
            return;

            //string eventStr = param as string;
            //C2S_ad_event req = new C2S_ad_event();
            //req.data = SerializeUtil.ToObject<C2S_ad_event_data>(eventStr);
            //WSNetMgr.Instance.ImmediateSend(req);
            //// LogUtil.Log("[ChannelMgr]OnAdEvent:" + eventStr);
        }
        #endregion

        #region 回调: 应用消息
        private void OnShowPlatformTipsUI(object param)
        {
            Channel.Current.toast(param as string);
        }
        #endregion

        #endregion
    }
}