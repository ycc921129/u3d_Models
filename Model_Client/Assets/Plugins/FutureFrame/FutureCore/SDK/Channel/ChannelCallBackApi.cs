/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using Beebyte.Obfuscator;
using UnityEngine;

namespace FutureCore
{
    [Skip]
    public partial class ChannelCallBackApi : MonoBehaviour
    {
        private string channelDefine;

        public void InitCallBack(string channelDefine)
        {
            this.channelDefine = channelDefine;
            InitUnity3dNetState();
        }

        #region 编辑器测试模式回调: 网络
        private DataBind<NetworkReachability> currUnity3dNetState;

        private void InitUnity3dNetState()
        {
            if (channelDefine != ChannelDefine.Base) return;

            currUnity3dNetState = new DataBind<NetworkReachability>();
            currUnity3dNetState.QuietlySetData(Application.internetReachability);
            currUnity3dNetState.onDataChanged = OnUnity3dNetChange;
            currUnity3dNetState.Data = Application.internetReachability;
        }

        private void OnUnity3dNetChange(NetworkReachability oldValue, NetworkReachability newValue)
        {
            if (newValue == NetworkReachability.NotReachable)
            {
                networkStatusChanged("false");
            }
            else
            {
                networkStatusChanged("true");
            }
        }

        private void Update()
        {
            if (channelDefine != ChannelDefine.Base) return;

            if (Time.frameCount % 15 == 0)
            {
                currUnity3dNetState.Data = Application.internetReachability;
            }
        }
        #endregion

        #region 回调: SDK
        /// <summary>
        /// 响应SDK渠道回调
        /// </summary>
        public void onSdkChannel(string sdkChannel)
        {
            LogUtil.Log("[ChannelCB]响应SDK渠道回调 sdkChannel: " + sdkChannel);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnSdkChannel, sdkChannel);
        }
        #endregion

        #region 回调: 验证登录
        /// <summary>
        /// 响应验证手机号
        /// </summary>
        public void onAccountKitMobile(string mobile)
        {
            LogUtil.Log("[ChannelCB]响应验证手机号 mobile: " + mobile);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnAccountKitMobile, mobile);
        }

        /// <summary>
        /// 响应登入后绑定Token
        /// </summary>
        public void onLoginBindToken(string info)
        {
            LogUtil.Log("[ChannelCB]响应登入后绑定Token info: " + info);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnLoginBindToken, info);
        }

        /// <summary>
        /// Android 生命周期监听
        /// </summary>
        public void onAndroidLifecycleListener(string name) { }
        #endregion 回调: 验证登录

        #region 回调: 平台相关
        /// <summary>
        /// 响应是否分享成功
        /// </summary>
        public void shareSuccess(string s)
        {
            bool isSuccess = bool.Parse(s);
            LogUtil.Log("[ChannelCB]响应是否分享成功 isSuccess:" + isSuccess);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.ShareSuccess, isSuccess);
        }

        /// <summary>
        /// 响应网络连接状态变化
        /// </summary>
        public void networkStatusChanged(string param)
        {
            bool hasNet = bool.Parse(param);
            LogUtil.Log("[ChannelCB]响应网络连接状态变化:" + (hasNet ? "已连接" : "已断开"));
            if (hasNet)
            {
                ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.NetworkStatusChanged_True, NetConst.IsNetAvailable);
            }
            else
            {
                ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.NetworkStatusChanged_False, NetConst.IsNetAvailable);
            }
        }

        /// <summary>
        /// 响应FirebaseToken
        /// 响应时游戏客户端还未启动,等游戏客户端启动后主动获取
        /// </summary>
        public void onFirebaseToken(string s)
        {
            LogUtil.Log("[ChannelCB]响应FirebaseToken s:" + s);
        }

        /// <summary>
        /// 响应更新用户信息
        /// </summary>
        public void updatePersonInfo(string s)
        {
            LogUtil.Log("[ChannelCB]响应更新用户信息 s:" + s);
        }

        /// <summary>
        /// 响应更新应用列表
        /// </summary>
        public void updateAppLists(string s)
        {
            LogUtil.Log("[ChannelCB]响应更新应用列表 s:" + s);
        }

        /// <summary>
        /// 响应发送Adwords
        /// </summary>
        public void sendAdwords()
        {
            LogUtil.Log("[ChannelCB]响应发送Adwords");
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnSendAdwords);
        }
        #endregion 回调: 平台相关

        #region 回调: 插屏广告
        /// <summary>
        /// 响应插屏广告开始加载
        /// </summary>
        public void onInterstitialAdLoadStart(string sdk)
        {
            LogUtil.Log("[ChannelCB]响应插屏广告开始加载 sdk:" + sdk);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnInterstitialAdLoadStart, sdk);
        }

        /// <summary>
        /// 响应插屏广告开始成功
        /// </summary>
        public void onInterstitialAdLoaded(string sdk)
        {
            LogUtil.Log("[ChannelCB]响应插屏广告开始成功 sdk:" + sdk);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.onInterstitialAdLoaded, sdk);
        }

        /// <summary>
        /// 响应插屏广告加载失败
        /// </summary>
        public void onInterstitialAdFailedToLoad(string sdk)
        {
            LogUtil.Log("[ChannelCB]响应插屏广告加载失败 sdk:" + sdk);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnInterstitialAdFailedToLoad, sdk);
        }

        /// <summary>
        /// 响应插屏广告展示
        /// </summary>
        public void onInterstitialAdOpen(string sdk)
        {
            LogUtil.Log("[ChannelCB]响应插屏广告展示 sdk:" + sdk);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnInterstitialAdOpen, sdk);
        }

        /// <summary>
        /// 响应插屏广告点击
        /// </summary>
        public void onInterstitialAdClick(string sdk)
        {
            LogUtil.Log("[ChannelCB]响应插屏广告点击 sdk:" + sdk);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnInterstitialAdClick, sdk);
        }

        /// <summary>
        /// 响应插屏广告结束 结束得到奖励
        /// </summary>
        public void onInterstitialAdClose(string sdk)
        {
            LogUtil.Log("[ChannelCB]响应插屏广告结束 sdk:" + sdk);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnInterstitialAdClose, sdk);
        }
        #endregion 回调: 插屏广告

        #region 回调: 视频广告
        /// <summary>
        /// 响应视频广告开始加载
        /// </summary>
        public void onVideoAdLoadStart(string sdk)
        {
            LogUtil.Log("[ChannelCB]响应视频广告开始加载 sdk:" + sdk);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnVideoAdLoadStart, sdk);
        }

        /// <summary>
        /// 响应视频广告加载成功
        /// </summary>
        public void onVideoAdLoaded(string sdk)
        {
            LogUtil.Log("[ChannelCB]响应视频广告加载成功 sdk:" + sdk);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnVideoAdLoaded, sdk);
        }

        /// <summary>
        /// 响应视频广告加载失败
        /// </summary>
        public void onVideoAdFaiToLoaded(string sdk)
        {
            LogUtil.Log("[ChannelCB]响应视频广告加载失败 sdk:" + sdk);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnVideoAdFaiToLoaded, sdk);
        }

        /// <summary>
        /// 响应开始播放视频广告
        /// </summary>
        public void onVideoAdOpen(string sdk)
        {
            LogUtil.Log("[ChannelCB]响应开始播放视频广告 sdk:" + sdk);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnVideoAdOpen, sdk);
        }

        /// <summary>
        /// 响应点击了视频广告
        /// </summary>
        public void onVideoAdClick(string sdk)
        {
            LogUtil.Log("[ChannelCB]响应点击了视频广告 sdk:" + sdk);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnVideoAdClick, sdk);
        }

        /// <summary>
        /// 响应视频广告看完得到奖励
        /// </summary>
        public void onVideoAdRewarded(string sdk)
        {
            LogUtil.Log("[ChannelCB]响应视频广告看完得到奖励 sdk:" + sdk);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnVideoAdRewarded, sdk);
        }

        /// <summary>
        /// 响应视频广告关闭
        /// </summary>
        public void onVideoAdClosed(string sdk)
        {
            LogUtil.Log("[ChannelCB]响应视频广告关闭 sdk:" + sdk);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnVideoAdClosed, sdk);
        }
        #endregion 回调: 视频广告

        #region 回调: 广告统计
        /// <summary>
        /// 响应广告统计事件
        /// Android端调用：cc.Channel.adEvent(" + "'" + {"id":"107470321109445336459","type":"google"} + "'" + "
        /// </summary>
        /// <param name="s">s的内容为 '{"id":"107470321109445336459","type":"google"}'</param>
        public void onAdEvent(string s)
        {
            // LogUtil.Log("[ChannelCB]响应广告统计事件 s:" + s);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnAdEvent, s);
        }
        #endregion 回调: 广告统计

        #region 回调: (旧)订阅
        /// <summary>
        /// 响应订阅回调
        /// </summary>
        public void onHandlePurchase(string originalJson)
        {
            LogUtil.Log("[ChannelCB]订阅回调 onHandlePurchase originalJson:" + originalJson);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnHandlePurchase, originalJson);
        }
        #endregion

        #region 回调: (新)内购与订阅
        /// <summary>
        /// 响应IAP结果回调
        /// </summary>
        public void onBillingPurchasesResult(string resultCode)
        {
            LogUtil.Log("[ChannelCB]IAP结果回调 onBillingPurchasesResult:" + resultCode);
            int result = int.Parse(resultCode);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnBillingPurchasesResult, result);
        }

        /// <summary>
        /// 响应查询已订阅与新增订阅
        /// </summary>
        public void onBillingSubscribePurchased(string originalJson)
        {
            LogUtil.Log("[ChannelCB]订阅回调 onBillingSubscribePurchased originalJson:" + originalJson);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnBillingSubscribePurchased, originalJson);
        }

        /// <summary>
        /// 响应内购回调
        /// </summary>
        public void onBillingIapPurchased(string iapJson)
        {
            LogUtil.Log("[ChannelCB]内购回调 onBillingIapPurchased:" + iapJson);
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.OnBillingIapPurchased, iapJson);
        }
        #endregion
    }
}