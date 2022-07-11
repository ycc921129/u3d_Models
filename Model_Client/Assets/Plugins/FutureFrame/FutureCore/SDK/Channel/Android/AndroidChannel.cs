/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using FutureCore.Data;
using FuturePlugin;
using System.Collections.Generic;
using UnityEngine;

namespace FutureCore
{
    public partial class AndroidChannel : BaseChannel
    {
        private string className;
        private string apiPrefix;

        private AndroidJavaClass androidClass;
        private AndroidJavaObject androidObject;

        private Dictionary<string, string> funcNamesDict = new Dictionary<string, string>();

        protected override void SetChannelDefine()
        {
            channelDefine = ChannelDefine.Android;
        }

        protected override void InitChannel()
        {
            InitAndroidObject();
        }

        private void InitAndroidObject()
        {
            className = AppConst.AndroidSDKClassName;
            apiPrefix = AppFacade_Frame.SDKApiPrefix;

            if (Application.platform == RuntimePlatform.Android)
            {
                androidClass = new AndroidJavaClass(className);
                string sdkClassName = "com.unity3d.player.UnityPlayer";
                if (className.Contains(sdkClassName))
                {
                    androidObject = androidClass.GetStatic<AndroidJavaObject>("currentActivity");
                }
                else
                {
                    androidObject = androidClass.GetStatic<AndroidJavaObject>("Instance");
                    if (androidObject != null)
                    {
                        androidObject.Call("init");
                    }
                }
            }
        }

        private void CallVoidApi(string apiFunc, params object[] args)
        {
            try
            {
                apiFunc = GetCallApiName(apiFunc);
                androidObject.Call(apiFunc, args);
            }
            catch (System.Exception e)
            {
                LogUtil.LogError("[AndroidChannel]CallVoidApi Exception: " + e.ToString());
            }
        }

        private T CallReturnApi<T>(string apiFunc, params object[] args)
        {
            try
            {
                apiFunc = GetCallApiName(apiFunc);
                return androidObject.Call<T>(apiFunc, args);
            }
            catch (System.Exception e)
            {
                LogUtil.LogError("[AndroidChannel]CallReturnApi Exception: " + e.ToString());
                return default;
            }
        }

        private string GetCallApiName(string apiFunc)
        {
            string fullApiFunc = null;
            if (!funcNamesDict.TryGetValue(apiFunc, out fullApiFunc))
            {
                fullApiFunc = string.Format("{0}_{1}", apiPrefix, apiFunc);
                funcNamesDict.Add(apiFunc, fullApiFunc);
            }
            return fullApiFunc;
        }

        #region 调用: 硬件字段
        /// <summary>
        /// 语言
        /// </summary>
        public override string lang
        {
            get { return CallReturnApi<string>("lang"); }
        }

        /// <summary>
        /// 设备生产商
        /// </summary>
        public override string dev_fac
        {
            get { return CallReturnApi<string>("dev_fac"); }
        }

        /// <summary>
        /// 系统版本值(Android版本)
        /// </summary>
        public override int sdk_int
        {
            get { return CallReturnApi<int>("sdk_int"); }
        }

        /// <summary>
        /// 设备型号
        /// </summary>
        public override string dev_model
        {
            get { return CallReturnApi<string>("dev_model"); }
        }

        /// <summary>
        /// 是否root,默认为false
        /// </summary>
        public override bool isRoot
        {
            get { return CallReturnApi<bool>("isRoot"); }
        }

        /// <summary>
        /// 是否通过代理方式
        /// </summary>
        public override bool isProxy
        {
            get { return CallReturnApi<bool>("isProxy"); }
        }

        /// <summary>
        /// 是否通过VPN方式
        /// </summary>
        public override bool isVPN
        {
            get { return CallReturnApi<bool>("isVPN"); }
        }
        #endregion 字段

        #region 调用: 业务字段
        /// <summary>
        /// 包名
        /// </summary>
        public override string pg
        {
            get
            {
                var oriPg = CallReturnApi<string>("pg");
                if (oriPg.EndsWith(".dev"))
                {
                    return AppFacade_Frame.PackageName;
                }

                return oriPg;
            }
        }

        /// <summary>
        /// 客户端版本值, 用于判定升级
        /// </summary>
        public override int ver_code
        {
            get { return CallReturnApi<int>("ver_code"); }
        }

        /// <summary>
        /// 客户端版本
        /// </summary>
        public override string ver
        {
            get { return CallReturnApi<string>("ver"); }
        }

        /// <summary>
        /// 系统+版本
        /// </summary>
        public override string os_ver
        {
            get { return CallReturnApi<string>("os_ver"); }
        }

        /// <summary>
        /// 渠道名
        /// </summary>
        public override string channel
        {
            get { return CallReturnApi<string>("channel"); }
        }

        /// <summary>
        /// sim卡运营商标识码
        /// </summary>
        public override string imsi
        {
            get { return CallReturnApi<string>("imsi"); }
        }

        /// <summary>
        /// AndroidID
        /// </summary>
        public override string aid
        {
            get { return CallReturnApi<string>("aid"); }
        }

        /// <summary>
        /// 友盟id
        /// </summary>
        public override string umid
        {
            get { return CallReturnApi<string>("umid"); }
        }

        /// <summary>
        /// 广告id
        /// </summary>
        public override string idfa
        {
            get { return CallReturnApi<string>("idfa"); }
        }

        /// <summary>
        /// id组
        /// </summary>
        public override string ids
        {
            get { return CallReturnApi<string>("ids"); }
        }

        /// <summary>
        /// 客户端哈希, 用于检测包是否被修改过
        /// </summary>
        public override string hash
        {
            get
            {
                if (AppFacade_Frame.IsUseUGameAndroid)
                {
                    return CallReturnApi<string>("hash");
                }
                else
                {
                    // 卓动和英动等外部项目打包方式
#if UNITY_BUILD_DEBUG || UNITY_BUILD_DEBUGFORMAL
                    return string.Empty;
#endif
                    return CallReturnApi<string>("hash");
                }
            }
        }

        /// <summary>
        /// 客户端是否是Debug版本
        /// </summary>
        public override bool debug
        {
            get
            {
                if (AppFacade_Frame.IsUseUGameAndroid)
                {
                    return CallReturnApi<bool>("debug");
                }
                else
                {
                    // 卓动和英动等外部项目打包方式
#if UNITY_BUILD_DEBUG
                    return true;
#elif UNITY_BUILD_DEBUGFORMAL
                    return false;
#else
                    return CallReturnApi<bool>("debug");
#endif
                }
            }
        }

        public override AppBuildType buildType
        {
            get
            {
                if (AppFacade_Frame.IsUseUGameAndroid)
                {
                    return (AppBuildType)CallReturnApi<int>("buildType");
                }
                else
                {
#if UNITY_BUILD_DEBUG
                    return AppBuildType.Debug;
#elif UNITY_BUILD_DEBUGFORMAL
                    return AppBuildType.DebugFormal;
#else
                    return AppBuildType.Release;
#endif
                }
            }
        }

        /// <summary>
        /// 网络类型
        /// </summary>
        public override string network
        {
            get { return CallReturnApi<string>("network"); }
        }

        /// <summary>
        /// 网络是否可用
        /// </summary>
        public override bool isNetworkAvailable
        {
            get { return CallReturnApi<bool>("isNetworkAvailable"); }
        }
        #endregion

        #region 调用: 验证登录
        /// <summary>
        /// 获取国家/地区代码
        /// </summary>
        public override string getCountry()
        {
            string country = CallReturnApi<string>("getCountry");
            LogUtil.Log("[AndroidChannel]getCountry: " + country);
            return country;
        }

        /// <summary>
        /// 获取FirebaseToken
        /// </summary>
        public override string getFirebaseToken()
        {
            LogUtil.Log("[AndroidChannel]获取FirebaseToken");
            return CallReturnApi<string>("getFirebaseToken");
        }

        /// <summary>
        /// 验证是否登入firebase
        /// </summary>
        public override bool firebaseIsLogin()
        {
            LogUtil.Log("[AndroidChannel]验证是否登入firebase");
            return CallReturnApi<bool>("firebaseIsLogin");
        }

        /// <summary>
        /// 获取AdWords (安装来源)
        /// </summary>
        public override string getAdWords()
        {
            LogUtil.Log("[AndroidChannel]获取AdWords (安装来源)");
            return CallReturnApi<string>("getAdWords");
        }

        /// <summary>
        /// 清除AdWords (安装来源)
        /// </summary>
        public override void clearAdWords()
        {
            LogUtil.Log("[AndroidChannel]清除AdWords (安装来源)");
            CallVoidApi("clearAdWords");
        }

        /// <summary>
        /// 登出firebase
        /// </summary>
        public override void firebaseSignOut()
        {
            LogUtil.Log("[AndroidChannel]登出firebase");
            CallVoidApi("firebaseSignOut");
        }

        /// <summary>
        /// 验证手机号
        /// </summary>
        public override void accountKitTokenCheck(string mobile)
        {
            LogUtil.Log("[AndroidChannel]验证手机号");
            CallVoidApi("accountKitTokenCheck", mobile);
        }

        /// <summary>
        /// 登入facebook
        /// </summary>
        public override void facebookLogin()
        {
            LogUtil.Log("[AndroidChannel]登入facebook");
            CallVoidApi("facebookLogin");
        }

        /// <summary>
        /// 登入google
        /// </summary>
        public override void googleLogin()
        {
            LogUtil.Log("[AndroidChannel]登入google");
            CallVoidApi("googleLogin");
        }

        /// <summary>
        /// 发送UID
        /// </summary>
        public override void sendUID(string uid)
        {
            LogUtil.Log("[AndroidChannel]发送UID: " + uid);
            CallVoidApi("sendUID", uid);
        }

        /// <summary>
        /// 通知客户端场景已经初始化
        /// </summary>
        public override void directorInitSuccess()
        {
            LogUtil.Log("[AndroidChannel]通知客户端场景已经初始化");
            CallVoidApi("directorInitSuccess");
        }
        #endregion

        #region 调用: 平台相关
        /// <summary>
        /// 获取应用名
        /// </summary>
        public override string getAppName()
        {
            string appName = CallReturnApi<string>("getAppName");
            LogUtil.Log("[AndroidChannel]getAppName: " + appName);
            return appName;
        }

        /// <summary>
        /// 隐藏闪屏
        /// </summary>
        public override void hideSplashPage()
        {
            LogUtil.Log("[AndroidChannel]隐藏闪屏");
            CallVoidApi("hideSplashPage");
        }

        /// <summary>
        /// 邮箱反馈 (只传邮箱)
        /// </summary>
        public override void feedback(string mailbox)
        {
            LogUtil.Log("[AndroidChannel]邮箱反馈 (只传邮箱)" + mailbox);
            CallVoidApi("feedback", mailbox);
        }

        /// <summary>
        /// 拷贝到剪切板
        /// </summary>
        /// <param name="copy">拷贝内容</param>
        /// <param name="hint">拷贝成功后的提示</param>
        public override void copy(string copy, string hint)
        {
            LogUtil.Log("[AndroidChannel]拷贝到剪切板 copy:" + copy + " hint：" + hint);
            CallVoidApi("copy", copy, hint);
        }

        /// <summary>
        /// 分享
        /// </summary>
        public override void share(string jsonShareContent)
        {
            LogUtil.Log("[AndroidChannel]分享 jsonShareContent:" + jsonShareContent);
            CallVoidApi("share", jsonShareContent);
        }

        /// <summary>
        /// 消息框
        /// </summary>
        public override void toast(string s)
        {
            LogUtil.Log("[AndroidChannel]消息框:" + s);
            CallVoidApi("toast", s);
        }

        /// <summary>
        /// 跳转商店App界面
        /// </summary>
        public override void openAppInGooglePlay()
        {
            LogUtil.Log("[AndroidChannel]跳转商店App界面");
            CallVoidApi("openAppInGooglePlay");
        }

        /// <summary>
        /// 跳转到GooglePlay更新App
        /// </summary>
        public override void updateApp(string json)
        {
            LogUtil.Log("[AndroidChannel]跳转到GooglePlay更新App json:" + json);
            CallVoidApi("updateApp", json);
        }
        #endregion

        #region 调用: 广告配置
        /// <summary>
        /// 设置广告配置
        /// </summary>
        public override void setAdConfig(string adsConfig)
        {
            if (string.IsNullOrEmpty(adsConfig))
            {
                LogUtil.Log("[AndroidChannel]设置广告配置 配置为空");
                return;
            }
            LogUtil.Log("[AndroidChannel]设置广告配置 adsConfig:" + adsConfig);
            CallVoidApi("setAdConfig", adsConfig);
        }

        /// <summary>
        /// 设置广告限制配置
        /// </summary>
        public override void setAdLimitConfig(string adLimitJson)
        {
            if (string.IsNullOrEmpty(adLimitJson))
            {
                LogUtil.Log("[AndroidChannel]设置广告限制配置 配置为空");
                return;
            }
            LogUtil.Log("[AndroidChannel]设置广告限制配置 adLimitJson:" + adLimitJson);
            CallVoidApi("setAdLimitConfig", adLimitJson);
        }

        /// <summary>
        /// 设置插屏时间间隔
        /// </summary>
        public override void setIntstitlAdInterval(int timeInterval)
        {
            LogUtil.Log("[AndroidChannel]设置插屏时间间隔 timeInterval:" + timeInterval);
            CallVoidApi("setIntstitlAdInterval", timeInterval);
        }
        #endregion

        #region 调用: 插屏广告
        /// <summary>
        /// 缓存插屏广告
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public override void preLoadInterstitialAd(string placementKey)
        {
            LogUtil.Log("[AndroidChannel]缓存插屏广告 placementKey:" + placementKey);
            CallVoidApi("preLoadInterstitialAd", placementKey);
        }

        /// <summary>
        /// 是否缓存插屏广告完成
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public override bool isPreLoadIntersititialAd(string placementKey)
        {
            bool isPreLoad = CallReturnApi<bool>("isPreLoadIntersititialAd", placementKey);
            LogUtil.LogFormat("[AndroidChannel]是否缓存插屏广告完成 placementKey:{0} isPreLoad:{1}", placementKey, isPreLoad);
            return isPreLoad;
        }

        /// <summary>
        /// 显示插屏广告
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public override void showInterstitialAd(string placementKey)
        {
            LogUtil.Log("[AndroidChannel]显示插屏广告 placementKey:" + placementKey);
            CallVoidApi("showInterstitialAd", placementKey);
        }
        #endregion

        #region 调用: 视频广告
        /// <summary>
        /// 缓存视频广告
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public override void preLoadVideoAd(string placementKey)
        {
            LogUtil.Log("[AndroidChannel]缓存视频广告 placementKey:" + placementKey);
            CallVoidApi("preLoadVideoAd", placementKey);
        }

        /// <summary>
        /// 视频广告是否缓存成功
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public override bool isPreLoadVideoAdSuccess(string placementKey)
        {
            bool isPreLoad = CallReturnApi<bool>("isPreLoadVideoAdSuccess", placementKey);
            LogUtil.LogFormat("[AndroidChannel]视频广告是否缓存成功 placementKey:{0} {1}", placementKey, isPreLoad);
            return isPreLoad;
        }

        /// <summary>
        /// 展示视频广告
        /// </summary>
        /// <param name="placementKey">广告位</param>
        /// <param name="viewId">viewId 控件id(与Android层无关，不往上层传)</param>
        public override void showVideoAd(string placementKey, int viewId = 0)
        {
            LogUtil.Log("[AndroidChannel]展示视频广告 placementKey:" + placementKey);
            CallVoidApi("showVideoAd", placementKey);
        }
        #endregion

        #region 调用: 视频广告（带打点）
        /// <summary>
        /// 视频按钮展示（曝光）
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public override void logRewardedAdBtnExposure(string placementKey)
        {
            CallVoidApi("logRewardedAdBtnExposure", placementKey);
            LogUtil.LogFormat("[AndroidChannel] logRewardedAdBtnExposure 视频按钮展示 placementKey:{0}", placementKey);
        }

        /// <summary>
        /// 点击视频按钮触发奖励广告 
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public override bool touchAndShowRewardedAd(string placementKey)
        {
            bool isShowSuccess = CallReturnApi<bool>("touchAndShowRewardedAd", placementKey);
            LogUtil.LogFormat("[AndroidChannel] touchAndShowRewardedAd 展示打点视频广告 placementKey:{0},{1}", placementKey, isShowSuccess);
            return isShowSuccess;
        }
        #endregion

        #region 调用：新版广告
        public override void adFlyShowIconAd(string placementId, int width, int height, int left, int top, bool isMoveType = false)
        {
            LogUtil.Log("[AndroidChannel]小布科技 显示 placementId:" + placementId + " width:" + width
                + " height:" + height + " left:" + left + " top:" + top + " isMoveType: " + isMoveType);

            CallVoidApi("adFlyShowIconAd", placementId, width, height, left, top, isMoveType);
        }

        public override void adFlyHideIconAd(string placementId)
        {
            LogUtil.Log("[AndroidChannel]小布科技 关闭 placementId:" + placementId);

            CallVoidApi("adFlyHideIconAd", placementId);
        }

        public override void okSpinLoadIconAd(string placementId)
        {
            LogUtil.Log("[AndroidChannel]adt 读取icon placementId:" + placementId);

            CallVoidApi("okSpinLoadIconAd", placementId);
        }

        public override void okSpinShowIconAd(string placementId, int width, int height, int left, int top)
        {
            LogUtil.Log("[AndroidChannel]adt 显示banner placementId:" + placementId + " width:" + width
                + " height:" + height + " left:" + left + " top:" + top);

            CallVoidApi("okSpinShowIconAd", placementId, width, height, left, top);
        }

        public override void okSpinHideIconAd(string placementId)
        {
            LogUtil.Log("[AndroidChannel]adt 关闭banner placementId:" + placementId);

            CallVoidApi("okSpinHideIconAd", placementId);
        }

        public override void okSpinOpenInteractive(string placementId)
        {
            LogUtil.Log("[AndroidChannel]adt 跳转活动面 placementId:" + placementId);

            CallVoidApi("okSpinOpenInteractive", placementId);
        }

        public override void flatAdsShowIconAd(string placementId, int width, int height, int left, int top)
        {
            LogUtil.Log("[AndroidChannel]显示flatAdsShowIconAd placementId:" + placementId + " width:" + width
                + " height:" + height + " left:" + left + " top:" + top);

            CallVoidApi("flatAdsShowIconAd", placementId, width, height, left, top);
        }

        public override void flatAdsHideIconAd(string placementId)
        {
            LogUtil.Log("[AndroidChannel]flatAdsShowIconAd 关闭 placementId:" + placementId);

            CallVoidApi("flatAdsHideIconAd", placementId);
        }
        #endregion

        #region 调用: 横幅广告
        /// <summary>
        /// 显示底部横幅广告
        /// </summary>
        public override void showBannerAdView()
        {
            LogUtil.Log("[AndroidChannel]显示底部横幅广告");
            CallVoidApi("showBannerAdView");
        }

        /// <summary>
        /// 隐藏底部横幅广告
        /// </summary>
        public override void hideBannerAdView()
        {
            LogUtil.Log("[AndroidChannel]隐藏底部横幅广告");
            CallVoidApi("hideBannerAdView");
        }

        /// <summary>
        /// 显示中部横幅广告
        /// </summary>
        public override void showCenterBannerAdView()
        {
            LogUtil.Log("[AndroidChannel]显示中部横幅广告");
            CallVoidApi("showCenterBannerAd");
        }

        /// <summary>
        /// 隐藏中部横幅广告
        /// </summary>
        public override void hideCenterBannerAdView()
        {
            LogUtil.Log("[AndroidChannel]隐藏中部横幅广告");
            CallVoidApi("hideCenterBannerAd");
        }
        #endregion

        #region 调用: 积分墙
        /// <summary>
        /// 打开Fyber积分墙
        /// </summary>
        public override void openFyber()
        {
            LogUtil.Log("[AndroidChannel]打开Fyber积分墙");
            CallVoidApi("openFyber");
        }

        /// <summary>
        /// 打开Ironsource积分墙
        /// </summary>
        public override void openIronsource()
        {
            LogUtil.Log("[AndroidChannel]打开Ironsource积分墙");
            CallVoidApi("openIronsource");
        }

        /// <summary>
        /// 打开Tayjor积分墙
        /// </summary>
        public override void openTayjor()
        {
            LogUtil.Log("[AndroidChannel]打开Tayjor积分墙");
            CallVoidApi("openTayjor");
        }
        #endregion

        #region 调用: 导流位
        /// <summary>
        /// 跳转Google Play商店
        /// </summary>
        /// <param name="pg">包名</param>
        public override void openAppInGooglePlayByPg(string pg)
        {
            LogUtil.Log("[AndroidChannel]跳转Google Play商店 pg:" + pg);
            CallVoidApi("openAppInGooglePlayByPg", pg);
        }

        /// <summary>
        /// 跳转网页
        /// </summary>
        /// <param name="url">地址</param>
        public override void openWebPage(string url)
        {
            LogUtil.Log("[AndroidChannel]跳转网页 url:" + url);
            CallVoidApi("openWebPage", url);
        }

        /// <summary>
        /// 打开apk应用
        /// </summary>  
        public override void openApp(string pg)
        {
            LogUtil.Log("[AndroidChannel]打开apk应用 pg:" + pg);
            CallVoidApi("openApp", pg);
        }

        /// <summary>
        /// 检查pg是否已经安装
        /// </summary>
        public override bool checkAppInstalled(string pg)
        {
            LogUtil.Log("[AndroidChannel]检查是否已经安装 pg:" + pg);
            return CallReturnApi<bool>("checkAppInstalled", pg);
        }

        /// <summary>
        /// 打开app关联平台
        /// </summary>
        public override void openMarket(string pg, string referrer)
        {
            LogUtil.Log("[AndroidChannel]检查是否已经安装 pg:" + pg + ", referrer: " + referrer);
            CallVoidApi("openMarket", pg, referrer);
        }

        /// <summary>
        /// 分享有效用户打点
        /// </summary>
        public override void logEffective()
        {
            LogUtil.Log("[AndroidChannel]分享有效用户打点 ");
            CallVoidApi("logEffective");
        }
        #endregion

        #region 调用: 友盟打点统计
        /// <summary>
        /// 不带参数统计
        /// </summary>
        public override void onEvent(string key)
        {
            LogUtil.Log("[AndroidChannel]友盟不带参数统计 key:" + key);
            CallVoidApi("onEvent", key);
        }

        /// <summary>
        /// 带参数统计
        /// </summary>
        public override void onEventParam(string key, string value)
        {
            LogUtil.Log("[AndroidChannel]友盟带参数统计 key:" + key + " value:" + value);
            CallVoidApi("onEventParam", key, value);
        }

        /// <summary>
        /// 时长统计
        /// </summary>
        public override void onEventValue(string key, int value, bool isLog = true)
        {
            if (isLog)
            {
                LogUtil.Log("[AndroidChannel]友盟时长统计 key:" + key + " value:" + value);
            }
            CallVoidApi("onEventValue", key, value);
        }
        #endregion

        #region 调用: FaceBook打点统计
        public override void onFBEvent(string key)
        {
            LogUtil.Log("[AndroidChannel]FB统计 key:" + key);
            CallVoidApi("onFBEvent", key);
        }

        public override void onFBEvent(string key, double value)
        {
            LogUtil.Log("[AndroidChannel]FB统计 key:" + key + " value:" + value);
            CallVoidApi("onFBEvent", key, value);
        }

        public override void onFBEvent(string key, string value)
        {
            LogUtil.Log("[AndroidChannel]FB统计 key:" + key + " value:" + value);
            CallVoidApi("onFBEvent", key, value);
        }

        public override void onFBEvent(string key, double valueToSum, string parametersJson)
        {
            LogUtil.Log("[AndroidChannel]FB统计 key:" + key + " valueToSum:" + valueToSum + "parametersJson:" + parametersJson);
            CallVoidApi("onFBEvent", key, valueToSum, parametersJson);
        }
        #endregion

        #region 调用: 友盟打点统计
        /// <summary>
        /// 统计玩家登录
        /// </summary>
        /// <param name="id">传邀请码</param>
        public override void onProfileSignIn(string id)
        {
            LogUtil.Log("[AndroidChannel]统计玩家登录 id : " + id);
            CallVoidApi("onProfileSignIn", id);
        }

        /// <summary>
        /// 统计玩家等级
        /// </summary>
        public override void setPlayerLevel(int level)
        {
            LogUtil.Log("[AndroidChannel]统计玩家等级 level : " + level);
            CallVoidApi("setPlayerLevel", level);
        }

        /// <summary>
        /// 统计开始关卡
        /// </summary>
        public override void startLevel(string level)
        {
            LogUtil.Log("[AndroidChannel]统计开始关卡 level : " + level);
            CallVoidApi("startLevel", level);
        }

        /// <summary>
        /// 统计关卡通关成功
        /// </summary>
        public override void finishLevel(string level)
        {
            LogUtil.Log("[AndroidChannel]统计关卡通关成功 level : " + level);
            CallVoidApi("finishLevel", level);
        }

        /// <summary>
        /// 统计关卡通关失败
        /// </summary>
        /// <param name="level"></param>
        public override void failLevel(string level)
        {
            LogUtil.Log("[AndroidChannel]统计关卡通关失败 level : " + level);
            CallVoidApi("failLevel", level);
        }
        #endregion

        #region 调用: 硬件
        /// <summary>
        /// 开始震动
        /// </summary>
        /// <param name="milliseconds">震动的持续时间</param>
        /// <param name="amplitude">震动强度 1-255</param>
        public override void vibrate(long milliseconds, int amplitude)
        {
            CallVoidApi("vibrate", milliseconds, amplitude);
        }

        /// <summary>
        /// 取消震动
        /// </summary>
        public override void virateCancel()
        {
            CallVoidApi("virateCancel");
        }
        #endregion

        #region 调用: 加密
        /// <summary>
        /// 加密
        /// </summary>
        public override string encryptedString(string name, string raw, string associatedData)
        {
            return base.encryptedString(name, raw, associatedData);

#if UNITY_IOS
            return base.encryptedString(name, raw, associatedData);
#elif UNITY_ANDROID
            // 安卓6
            if (sdk_int >= 23)
            {
                return CallReturnApi<string>("encryptedString", name, raw, associatedData);
            }
#endif
            return base.encryptedString(name, raw, associatedData);
        }

        /// <summary>
        /// 解密
        /// </summary>
        public override string decryptedString(string name, string encryptedValue, string associatedData)
        {
            return base.decryptedString(name, encryptedValue, associatedData);

#if UNITY_IOS
            return base.decryptedString(name, encryptedValue, associatedData);
#elif UNITY_ANDROID
            // 安卓6
            if (sdk_int >= 23)
            {
                return CallReturnApi<string>("decryptedString", name, encryptedValue, associatedData);
            }
#endif
        }
        #endregion

        #region 调用: 事件
        /// <summary>
        /// appsFlyer 应用内事件
        /// </summary>
        public override void onAppsFlyerEvent(string key, string value)
        {
            LogUtil.Log("[AndroidChannel]appsFlyer 应用内事件:" + key + " " + value);
            CallVoidApi("onAppsFlyerEvent", key, value);
        }
        #endregion

        #region 调用: 事件统计上报
        /// <summary>
        /// 登录成功
        /// </summary>
        public override void onLoginSuccess(long uid, string token, string url)
        {
            CallVoidApi("onLoginSuccess", uid, token, url);
            LogUtil.Log("[AndroidChannel]onLoginSuccess");
        }

        /// <summary>
        /// AD_EVENT 广告事件
        /// </summary>
        /// <param name="sdk">广告SDK接入商</param>
        /// <param name="type">广告类型</param>
        /// <param name="id">广告位ID</param>
        /// <param name="name">广告位名称</param>
        /// <param name="eventname">事件</param>
        /// <param name="revenue">广告收益</param>
        public override void logAd(string sdk, string type, string id, string name, string eventname, double revenue)
        {
            CallVoidApi("logAd", sdk, type, id, name, eventname, revenue);
            LogUtil.LogFormat("[AndroidChannel]logAd sdk:{0} type:{1} name:{2} eventname:{3} revenue:{4}", sdk, type, name, eventname, revenue);
        }

        /// <summary>
        /// USER_EVENT 用户事件
        /// </summary>
        /// <param name="e">事件名称</param>
        /// <param name="times">事件发生次数</param>
        public override void logUserEvent(string e, int times)
        {
            CallVoidApi("logUserEvent", e, times);
            LogUtil.LogFormat("[AndroidChannel]logUserEvent e:{0} times:{1}", e, times);
        }

        /// <summary>
        /// USER_STATUS 用户状态
        /// </summary>
        /// <param name="name">状态名</param>
        /// <param name="value">状态值</param>
        public override void logUserStatus(string name, string value)
        {
            CallVoidApi("logUserStatus", name, value);
            LogUtil.LogFormat("[AndroidChannel]logUserStatus name:{0} value:{1}", name, value);
        }

        /// <summary>
        /// 普通统计上报
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="value">事件参数</param>
        public override void logNormal(string name, string value)
        {
            CallVoidApi("logNormal", name, value);
            LogUtil.LogFormat("[AndroidChannel]logNormal name:{0} value:{1}", name, value);
        }

        /// <summary>
        /// Json普通统计上报
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="mapJsonStr">事件参数</param>
        public override void logNormalForJson(string name, string mapJsonStr)
        {
            CallVoidApi("logNormalForJson", name, mapJsonStr);
            LogUtil.LogFormat("[AndroidChannel]logNormalForJson name:{0} mapJsonStr:{1}", name, mapJsonStr);
        }

        #endregion

        #region 调用: firebase事件投放使用
        public override void onUpdateLtv24HJson(string configJson)
        {
            LogUtil.Log("[AndroidChannel]firebase事件投放使用 key:" + configJson);
            CallVoidApi("onUpdateLtv24HJson", configJson);
        }
        #endregion

        #region 调用: (旧)内购/订阅
        /// <summary>
        /// 点击内购按钮
        /// </summary>
        /// <param name="skuId">商品Id</param>
        public override void clickInAppBuyBtn(string skuId)
        {
            LogUtil.Log("[AndroidChannel]点击内购按钮 skuId:" + skuId);
            CallVoidApi("clickInAppBtn", skuId);
        }

        /// <summary>
        /// 点击订阅按钮
        /// </summary>
        /// <param name="skuId">商品Id</param>
        public override void clickSubscribeBtn(string skuId)
        {
            LogUtil.Log("[AndroidChannel]点击订阅按钮 skuId:" + skuId);
            CallVoidApi("clickSubscribeBtn", skuId);
        }

        /// <summary>
        /// 查询订阅购买历史
        /// </summary>
        public override void queryPurchaseHistory()
        {
            LogUtil.Log("[AndroidChannel]查询订阅购买历史");
            CallVoidApi("queryPurchaseHistory");
        }
        #endregion

        #region 调用: (新)内购/订阅
        /// <summary>
        /// 登录后查询商品属性
        /// (获取价格)
        /// </summary>
        public override void queryBillingProductDetails(ProductDetailsData productDetailsData)
        {
            LogUtil.Log("[AndroidChannel]登录后查询商品属性");
            string json = SerializeUtil.ToJson(productDetailsData);
            CallVoidApi("queryBillingProductDetails", json);
        }

        /// <summary>
        /// 查询订阅购买历史
        /// (获取购买订单)
        /// </summary>
        public override void queryBillingPurchaseHistory()
        {
            LogUtil.Log("[AndroidChannel]查询订阅购买历史");
            CallVoidApi("queryBillingPurchaseHistory");
        }


        /// <summary>
        /// 内购/订阅是否初始化成功
        /// </summary>
        public override bool isBillingInitSuccess()
        {
            bool state = CallReturnApi<bool>("isBillingInitSuccess");
            LogUtil.Log("[AndroidChannel]内购/订阅是否初始化状态: " + state);
            return state;
        }

        /// <summary>
        /// 获取现在的购买价格
        /// </summary>
        public override string getBillingProductPrice(string productID)
        {
            string price = CallReturnApi<string>("getBillingProductPrice", productID);
            LogUtil.LogFormat("[AndroidChannel]获取现在的购买价格: productID:{0} price:{1}", productID, price);
            return price;
        }

        /// <summary>
        /// 获取原来的购买价格
        /// </summary>
        public override string getBillingProductOriginalPrice(string productID)
        {
            string price = CallReturnApi<string>("getBillingProductOriginalPrice", productID);
            LogUtil.LogFormat("[AndroidChannel]获取原来的购买价格: productID:{0} price:{1}", productID, price);
            return price;
        }

        /// <summary>
        /// 调起IAP
        /// </summary>
        public override void launchBillingFlow(string productID)
        {
            LogUtil.LogFormat("[AndroidChannel]调起IAP: productID:{0}", productID);
            CallVoidApi("launchBillingFlow", productID);
        }
        #endregion

        #region 调用: 上报异常
        /// <summary>
        /// 上报异常
        /// </summary>
        public override void reportError(string errorMsg)
        {
            CallVoidApi("reportError", errorMsg);
        }
        #endregion

        #region 调用: Adjust打点
        /// <summary>
        /// 通过事件名打点
        /// </summary>
        public override void trackEventByEventKey(string adjustEventKey)
        {
            CallVoidApi("trackEventByEventKey", adjustEventKey);
        }

        /// <summary>
        /// 设置统计事件 
        /// </summary>
        public override void setTrackEventTokenMap(string adjust_event_json)
        {
            LogUtil.Log("[AndroidChannel]adjust_event_json 设置统计事件:" + adjust_event_json);
            CallVoidApi("setTrackEventTokenMap", adjust_event_json);
        }

        /// <summary>
        /// 设置统计事件 
        /// </summary>
        public override void setAdsNewConfig(string adsNewJson)
        {
            LogUtil.Log("[AndroidChannel]adsNewJson 设置统计事件:" + adsNewJson);
            CallVoidApi("setAdsNewConfig", adsNewJson);
        }

        /// <summary>
        /// 设置统计事件 
        /// </summary>
        public override void setAdsIconConfig(string configJson)
        {
            LogUtil.Log("[AndroidChannel]configJson 设置统计事件:" + configJson);
            CallVoidApi("setAdsIconConfig", configJson);
        }

        /// <summary>
        /// 网络优化配置事件 
        /// </summary>
        public override void setNetOptConfig(string configJson, string uid)
        {
            LogUtil.Log("[AndroidChannel]setNetOptConfig 网络优化配置事件 :" + configJson + ", uid = " + uid);
            CallVoidApi("setNetOptConfig", configJson, uid);
        }

        /// <summary>
        /// 插屏广告配置事件   
        /// </summary>
        public override void setAdsInterstitialConfig(string configJson)
        {
            LogUtil.Log("[AndroidChannel] setAdsInterstitialConfig 插屏广告配置 :" + configJson);
            CallVoidApi("setAdsInterstitialConfig", configJson);
        }
        #endregion
    }
}