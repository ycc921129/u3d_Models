/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    /// <summary>
    /// 渠道原生消息
    /// Frame_0-999
    /// </summary>
    public static class ChannelRawMsg
    {
        public const string NAME = "ChannelRawMsg";
        public const uint BASE = 0;
        private static uint Cursor_BASE = BASE;

        #region 调用
        /// SDK 调用
        // 统计异常上报
        public static readonly uint ReportError = ++Cursor_BASE;
        #endregion

        #region 回调
        /// SDK 回调消息
        // 响应SDK渠道回调
        public static readonly uint OnSdkChannel = ++Cursor_BASE;

        /// 验证登录 回调消息
        // 响应验证手机号
        public static readonly uint OnAccountKitMobile = ++Cursor_BASE;
        // 响应登入后绑定Token
        public static readonly uint OnLoginBindToken = ++Cursor_BASE;

        /// 平台相关 回调消息
        // 响应是否分享成功
        public static readonly uint ShareSuccess = ++Cursor_BASE;
        // 响应网络连接状态变化已连接
        public static readonly uint NetworkStatusChanged_True = ++Cursor_BASE;
        // 响应网络连接状态变化已断开
        public static readonly uint NetworkStatusChanged_False = ++Cursor_BASE;
        // 响应发送Adwords
        public static readonly uint OnSendAdwords = ++Cursor_BASE;

        /// 广告 回调消息
        // 响应插屏广告开始加载
        public static readonly uint OnInterstitialAdLoadStart = ++Cursor_BASE;
        // 响应插屏广告开始成功
        public static readonly uint onInterstitialAdLoaded = ++Cursor_BASE;
        // 响应插屏广告加载失败
        public static readonly uint OnInterstitialAdFailedToLoad = ++Cursor_BASE;
        // 响应插屏广告展示
        public static readonly uint OnInterstitialAdOpen = ++Cursor_BASE;
        // 响应插屏广告点击
        public static readonly uint OnInterstitialAdClick = ++Cursor_BASE;
        // 响应插屏广告结束
        public static readonly uint OnInterstitialAdClose = ++Cursor_BASE;

        // 响应视频广告开始加载
        public static readonly uint OnVideoAdLoadStart = ++Cursor_BASE;
        // 响应视频广告加载成功
        public static readonly uint OnVideoAdLoaded = ++Cursor_BASE;
        // 响应视频广告加载失败
        public static readonly uint OnVideoAdFaiToLoaded = ++Cursor_BASE;
        // 响应开始播放视频广告
        public static readonly uint OnVideoAdOpen = ++Cursor_BASE;
        // 响应点击了视频广告
        public static readonly uint OnVideoAdClick = ++Cursor_BASE;
        // 响应视频广告看完得到奖励
        public static readonly uint OnVideoAdRewarded = ++Cursor_BASE;
        // 响应视频广告关闭
        public static readonly uint OnVideoAdClosed = ++Cursor_BASE;

        // 响应广告统计事件
        public static readonly uint OnAdEvent = ++Cursor_BASE;

        /// 订阅内购 回调消息
        // (旧)响应订阅回调
        public static readonly uint OnHandlePurchase = ++Cursor_BASE;
        // 响应IAP结果回调
        public static readonly uint OnBillingPurchasesResult = ++Cursor_BASE;
        // (新)响应订阅回调
        public static readonly uint OnBillingSubscribePurchased = ++Cursor_BASE;
        // 响应订阅回调
        public static readonly uint OnBillingIapPurchased = ++Cursor_BASE;
        #endregion
    }
}