/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
#if UNITY_IOS
using System.Runtime.InteropServices;
#endif

namespace FutureCore
{
    [Beebyte.Obfuscator.Skip]
    public static class iOSChannelApi
    {
#if UNITY_IOS && !UNITY_EDITOR
        #region 初始化
        [DllImport("__Internal")]
        public static extern void init();
        #endregion

        #region 硬件字段
        [DllImport("__Internal")]
        public static extern string lang();

        [DllImport("__Internal")]
        public static extern string dev_fac();

        [DllImport("__Internal")]
        public static extern int sdk_int();

        [DllImport("__Internal")]
        public static extern string dev_model();

        [DllImport("__Internal")]
        public static extern bool isRoot();

        [DllImport("__Internal")]
        public static extern bool isProxy();

        [DllImport("__Internal")]
        public static extern bool isVPN();

        [DllImport("__Internal")]
        public static extern string pg();

        [DllImport("__Internal")]
        public static extern int ver_code();

        [DllImport("__Internal")]
        public static extern string ver();

        [DllImport("__Internal")]
        public static extern string os_ver();

        [DllImport("__Internal")]
        public static extern string channel();

        [DllImport("__Internal")]
        public static extern string aid();

        [DllImport("__Internal")]
        public static extern string idfa();

        [DllImport("__Internal")]
        public static extern string ids();

        [DllImport("__Internal")]
        public static extern string umid();

        [DllImport("__Internal")]
        public static extern bool debug();

        [DllImport("__Internal")]
        public static extern int buildType();

        [DllImport("__Internal")]
        public static extern string network();

        [DllImport("__Internal")]
        public static extern bool isNetworkAvailable();
        #endregion

        #region 调用: 验证登录
        [DllImport("__Internal")]
        public static extern void sendUID(string uid);

        [DllImport("__Internal")]
        public static extern void directorInitSuccess();
        #endregion

        #region 调用: 平台相关
        [DllImport("__Internal")]
        public static extern string getAppName();

        [DllImport("__Internal")]
        public static extern void hideSplashPage();

        [DllImport("__Internal")]
        public static extern void feedback(string mailBox);

        [DllImport("__Internal")]
        public static extern void rate();

        [DllImport("__Internal")]
        public static extern void updateApp(string json);

        [DllImport("__Internal")]
        public static extern void toast(string s);
        #endregion

        #region 调用: 插屏广告
        [DllImport("__Internal")]
        public static extern void preLoadInterstitialAd(string placementKey);

        [DllImport("__Internal")]
        public static extern bool isPreLoadIntersititialAd(string placementKey);

        [DllImport("__Internal")]
        public static extern void showInterstitialAd(string placementKey);

        #endregion

        #region 调用: 视频广告
        [DllImport("__Internal")]
        public static extern void preLoadVideoAd(string placementKey);

        [DllImport("__Internal")]
        public static extern bool isPreLoadVideoAdSuccess(string placementKey);

        [DllImport("__Internal")]
        public static extern void showVideoAd(string placementKey, int viewId = 0);

        #endregion

        #region 调用: 横幅广告
        [DllImport("__Internal")]
        public static extern void showBannerAdView();

        [DllImport("__Internal")]
        public static extern void hideBannerAdView();

        [DllImport("__Internal")]
        public static extern void showCenterBannerAdView();

        [DllImport("__Internal")]
        public static extern void hideCenterBannerAdView();
        #endregion

        #region 调用: 友盟通用打点统计
        [DllImport("__Internal")]
        public static extern void onEvent(string key);

        [DllImport("__Internal")]
        public static extern void onEventParam(string key, string value);

        [DllImport("__Internal")]
        public static extern void onEventValue(string key, int value);
        #endregion

        #region 调用: 友盟打点统计
        [DllImport("__Internal")]
        public static extern void onProfileSignIn(string inviteCode);
        #endregion
        
        #region 事件统计上报

        [DllImport("__Internal")]
        public static extern void onLoginSuccess(long uid,string token, string url);

        [DllImport("__Internal")]
        public static extern void logAd(string sdk, string type, string id, string name, string eventname, double revenue);

        [DllImport("__Internal")]
        public static extern void logUserEvent(string e, int times);

        [DllImport("__Internal")]
        public static extern void logUserStatus(string name, string value);

        [DllImport("__Internal")]
        public static extern void logNormal(string name, string value);

        [DllImport("__Internal")]
        public static extern void logNormalForJson(string name, string mapJsonStr);

        #endregion
#else
        #region 初始化
        public static void init() { throw new NotImplementedException(); }
        #endregion

        #region 硬件字段
        public static string lang() { throw new NotImplementedException(); }

        public static string dev_fac() { throw new NotImplementedException(); }

        public static int sdk_int() { throw new NotImplementedException(); }

        public static string dev_model() { throw new NotImplementedException(); }

        public static bool isRoot() { throw new NotImplementedException(); }

        public static bool isProxy() { throw new NotImplementedException(); }

        public static bool isVPN() { throw new NotImplementedException(); }

        public static string pg() { throw new NotImplementedException(); }

        public static int ver_code() { throw new NotImplementedException(); }

        public static string ver() { throw new NotImplementedException(); }

        public static string os_ver() { throw new NotImplementedException(); }

        public static string channel() { throw new NotImplementedException(); }

        public static string aid() { throw new NotImplementedException(); }

        public static string umid() { throw new NotImplementedException(); }

        public static string idfa() { throw new NotImplementedException(); }

        public static string ids() { throw new NotImplementedException(); }

        public static bool debug() { throw new NotImplementedException(); }

        public static int buildType() { throw new NotImplementedException(); }

        public static string network() { throw new NotImplementedException(); }

        public static bool isNetworkAvailable() { throw new NotImplementedException(); }
        #endregion

        #region 调用: 验证登录
        public static void sendUID(string uid) { throw new NotImplementedException(); }

        public static void directorInitSuccess() { throw new NotImplementedException(); }
        #endregion

        #region 调用: 平台相关
        public static string getAppName() { throw new NotImplementedException(); }

        public static void hideSplashPage() { throw new NotImplementedException(); }

        public static void feedback(string mailBox) { throw new NotImplementedException(); }

        public static void rate() { throw new NotImplementedException(); }

        public static void updateApp(string json) { throw new NotImplementedException(); }

        public static void toast(string s) { throw new NotImplementedException(); }
        #endregion

        #region 调用: 插屏广告
        public static void preLoadInterstitialAd(string placementKey) { throw new NotImplementedException(); }

        public static bool isPreLoadIntersititialAd(string placementKey) { throw new NotImplementedException(); }

        public static void showInterstitialAd(string placementKey) { throw new NotImplementedException(); }

        #endregion

        #region 调用: 视频广告
        public static void preLoadVideoAd(string placementKey) { throw new NotImplementedException(); }

        public static bool isPreLoadVideoAdSuccess(string placementKey) { throw new NotImplementedException(); }

        public static void showVideoAd(string placementKey, int viewId = 0) { throw new NotImplementedException(); }

        #endregion

        #region 调用: 横幅广告
        public static void showBannerAdView() { throw new NotImplementedException(); }

        public static void hideBannerAdView() { throw new NotImplementedException(); }

        public static void showCenterBannerAdView() { throw new NotImplementedException(); }

        public static void hideCenterBannerAdView() { throw new NotImplementedException(); }
        #endregion

        #region 调用: 友盟通用打点统计
        public static void onEvent(string key) { throw new NotImplementedException(); }

        public static void onEventParam(string key, string value) { throw new NotImplementedException(); }

        public static void onEventValue(string key, int value) { throw new NotImplementedException(); }
        #endregion

        #region 调用: 友盟打点统计
        public static void onProfileSignIn(string inviteCode) { throw new NotImplementedException(); }
        #endregion

        #region 事件统计上报

        public static void onLoginSuccess(long uid, string token, string url)
        {
            throw new NotImplementedException();
        }

        public static void logAd(string sdk, string type, string id, string name, string eventname, double revenue)
        {
            throw new NotImplementedException();
        }

        public static void logUserEvent(string e, int times)
        {
            throw new NotImplementedException();
        }

        public static void logUserStatus(string name, string value)
        {
            throw new NotImplementedException();
        }

        public static void logNormal(string name, string value)
        {
            throw new NotImplementedException();
        }

        public static void logNormalForJson(string name, string mapJsonStr)
        {
            throw new NotImplementedException();
        }

        #endregion
#endif
    }
}