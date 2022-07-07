/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using FuturePlugin;
using FutureCore.Data;
using UnityEngine;

namespace FutureCore
{
    public partial class BaseChannel : ISDK
    {
        protected string channelDefine;
        private GameObject apiGo;
        protected ChannelCallBackApi cbApi;

        public void Init(GameObject apiGo)
        {
            SetChannelDefine();
            InitChannel();
            InitCallBack(apiGo);
        }

        protected virtual void SetChannelDefine()
        {
            channelDefine = ChannelDefine.Base;
        }

        protected virtual void InitChannel() { }

        private void InitCallBack(GameObject apiGo)
        {
            this.apiGo = apiGo;
            cbApi = apiGo.AddComponent<ChannelCallBackApi>();
            cbApi.InitCallBack(channelDefine);
        }

        public string GetChannelDefine()
        {
            return channelDefine;
        }

        public ChannelCallBackApi GetCBApi()
        {
            return cbApi;
        }

        #region 调用: 硬件字段

        /// <summary>
        /// 语言码
        /// </summary>
        private string _langCode = null;
        public string langCode
        {
            get
            {
                // 计算语言码
                if (_langCode == null)
                {
                    string _lang = lang;
                    if (_lang != null)
                    {
                        _lang = _lang.Replace('-', '_');
                        _langCode = _lang;
                        int splitIdx = _lang.IndexOf('_');
                        if (splitIdx > 0)
                        {
                            _langCode = _lang.Substring(0, splitIdx);
                        }
                    }
                }
                return _langCode;
            }
        }

        /// <summary>
        /// 语言_国家
        /// </summary>
        public virtual string lang { get { return AppConst.DefaultLangue; } }

        /// <summary>
        /// 设备生产商
        /// </summary>
        public virtual string dev_fac { get { return "dell"; } }

        /// <summary>
        /// 系统版本值(Android版本)
        /// </summary>
        public virtual int sdk_int { get { return 26; } }

        /// <summary>
        /// 设备型号
        /// </summary>
        public virtual string dev_model { get { return "alien"; } }

        /// <summary>
        /// 是否root,默认为false
        /// </summary>
        public virtual bool isRoot { get { return false; } }

        /// <summary>
        /// 通过代理方式
        /// </summary>
        public virtual bool isProxy { get { return true; } }

        /// <summary>
        /// 通过VPN方式
        /// </summary>
        public virtual bool isVPN { get { return true; } }
        #endregion

        #region 调用: 业务字段
        /// <summary>
        /// 包名
        /// </summary>
        public virtual string pg { get { return AppConst.PackageName; } }

        /// <summary>
        /// 客户端版本值, 用于判定升级
        /// </summary>
        public virtual int ver_code { get { return AppConst.ChannelTest_VerCode; } }

        /// <summary>
        /// 客户端版本
        /// </summary>
        public virtual string ver { get { return AppConst.PackageVersion; } }

        /// <summary>
        /// 系统+版本
        /// </summary>
        public virtual string os_ver { get { return AppConst.Package_os_ver; } }

        /// <summary>
        /// 渠道名
        /// </summary>
        public virtual string channel { get { return string.Empty; } }

        /// <summary>
        /// sim卡运营商标识码
        /// </summary>
        public virtual string imsi { get { return AppConst.Package_imsi; } }

        /// <summary>
        /// AndroidID
        /// </summary>
        public virtual string aid
        {
            get
            {
                string newAidTest = PrefsUtil.ReadString(PrefsKeyConst.Channel_newAidTest);
                if (!string.IsNullOrEmpty(newAidTest))
                {
                    return newAidTest;
                }
                else
                {
                    newAidTest = DateTimeMgr.Instance.GetCurrTimestampInfo();
                    PlayerPrefs.SetString(PrefsKeyConst.Channel_newAidTest, newAidTest);
                    return newAidTest;
                }
                return DeviceConst.DeviceIdentifier;
            }
        }

        /// <summary>
        /// 友盟id
        /// </summary>
        public virtual string umid { get { return aid; } }

        /// <summary>
        /// 广告id
        /// </summary>
        public virtual string idfa { get { return string.Empty; } }

        /// <summary>
        /// id组
        /// </summary>
        public virtual string ids { get { return string.Empty; } }

        /// <summary>
        /// 客户端哈希, 用于检测包是否被修改过
        /// </summary>
        public virtual string hash { get { return string.Empty; } }

        /// <summary>
        /// 客户端是否是Debug版本
        /// </summary>
        public virtual bool debug { get { return AppConst.ChannelTest_Debug; } }

        /// <summary>
        /// 编译类型
        /// </summary>
        public virtual AppBuildType buildType { get { return AppConst.ChannelTest_BuildType; } }

        /// <summary>
        /// 网络类型
        /// </summary>
        public virtual string network { get { return "wifi"; } }

        /// <summary>
        /// 网络是否可用
        /// </summary>
        public virtual bool isNetworkAvailable
        {
            get
            {
                return Application.internetReachability != NetworkReachability.NotReachable;
            }
        }
        #endregion

        #region 调用: 验证登录
        /// <summary>
        /// 获取国家/地区代码
        /// </summary>
        public virtual string getCountry()
        {
            LogUtil.Log("[BaseChannel]getCountry test");
            return "test";
        }

        /// <summary>
        /// 获取FirebaseToken
        /// </summary>
        public virtual string getFirebaseToken()
        {
            LogUtil.Log("[BaseChannel]获取FirebaseToken");
            return "testfirebaseToken";
        }

        /// <summary>
        /// 验证是否登入firebase
        /// </summary>
        public virtual bool firebaseIsLogin()
        {
            LogUtil.Log("[BaseChannel]验证是否登入firebase");
            return true;
        }

        /// <summary>
        /// 获取AdWords (安装来源)
        /// </summary>
        public virtual string getAdWords()
        {
            LogUtil.Log("[BaseChannel]获取AdWords (安装来源)");
            return "PC_Debug";
        }

        /// <summary>
        /// 清除AdWords (安装来源)
        /// </summary>
        public virtual void clearAdWords()
        {
            LogUtil.Log("[BaseChannel]清除AdWords (安装来源)");
        }

        /// <summary>
        /// 登出firebase
        /// </summary>
        public virtual void firebaseSignOut()
        {
            LogUtil.Log("[BaseChannel]登出firebase");
        }

        /// <summary>
        /// 验证手机号
        /// </summary>
        public virtual void accountKitTokenCheck(string mobile)
        {
            LogUtil.Log("[BaseChannel]验证手机号");
            cbApi.onAccountKitMobile("13888888888");
        }

        /// <summary>
        /// 登入facebook
        /// </summary>
        public virtual void facebookLogin()
        {
            LogUtil.Log("[BaseChannel]登入facebook");
            FirebaseUserInfo info = new FirebaseUserInfo();
            info.isSuccess = true;
            info.type = "facebook";
            info.id = "TestID";
            info.sex = "2";
            info.nick = "AOE";
            info.mobile = "13888888888";
            info.email = "test@aoe.com";
            info.firebase_token = "testfirebase_token";
            cbApi.onLoginBindToken(SerializeUtil.ToJson(info));
        }

        /// <summary>
        /// 登入google
        /// </summary>
        public virtual void googleLogin()
        {
            LogUtil.Log("[BaseChannel]登入google");
            FirebaseUserInfo info = new FirebaseUserInfo();
            info.isSuccess = true;
            info.type = "google";
            info.id = "TestID";
            info.sex = "2";
            info.nick = "AOE";
            info.mobile = "13888888888";
            info.email = "test@aoe.com";
            info.firebase_token = "testfirebase_token";
            cbApi.onLoginBindToken(SerializeUtil.ToJson(info));
        }

        /// <summary>
        /// 发送UID
        /// </summary>
        public virtual void sendUID(string uid)
        {
            LogUtil.Log("[BaseChannel]发送UID");
        }

        /// <summary>
        /// 通知客户端场景已经初始化
        /// </summary>
        public virtual void directorInitSuccess()
        {
            LogUtil.Log("[BaseChannel]通知客户端场景已经初始化");
        }
        #endregion

        #region 调用: 平台相关
        /// <summary>
        /// 获取应用名
        /// </summary>
        public virtual string getAppName()
        {
            LogUtil.Log("[BaseChannel]getAppName: " + Application.productName);
            return Application.productName;
        }

        /// <summary>
        /// 隐藏闪屏
        /// </summary>
        public virtual void hideSplashPage()
        {
            LogUtil.Log("[BaseChannel]隐藏闪屏");
        }

        /// <summary>
        /// 邮箱反馈 (只传邮箱)
        /// </summary>
        public virtual void feedback(string mailbox)
        {
            LogUtil.Log("[BaseChannel]邮箱反馈 (只传邮箱)" + mailbox);
        }

        /// <summary>
        /// 拷贝到剪切板
        /// </summary>
        /// <param name="copy">拷贝内容</param>
        /// <param name="hint">拷贝成功后的提示</param>
        public virtual void copy(string copy, string hint)
        {
            LogUtil.Log("[BaseChannel]拷贝到剪切板 copy:" + copy + " hint：" + hint);
            GUIUtility.systemCopyBuffer = copy;
        }

        /// <summary>
        /// 分享
        /// </summary>
        public virtual void share(string jsonShareContent)
        {
            LogUtil.Log("[BaseChannel]分享 jsonShareContent:" + jsonShareContent);
            cbApi.shareSuccess("true");
        }

        /// <summary>
        /// 消息框
        /// </summary>
        public virtual void toast(string s)
        {
            LogUtil.Log("[BaseChannel]消息框:" + s);
            App.ShowTipsUI(s);
        }

        /// <summary>
        /// 评星
        /// </summary>
        public virtual void rate()
        {
            openAppInGooglePlayByPg(pg);
        }

        /// <summary>
        /// 跳转商店App界面
        /// </summary>
        public virtual void openAppInGooglePlay()
        {
            LogUtil.Log("[BaseChannel]跳转商店App界面");
            Application.OpenURL("www.baidu.com");
        }

        /// <summary>
        /// 跳转到GooglePlay更新App更新App
        /// </summary>
        public virtual void updateApp(string json)
        {
            LogUtil.Log("[BaseChannel]跳转到GooglePlay更新App json:" + json);
            Application.OpenURL("www.baidu.com");
        }
        #endregion

        #region 调用: 广告配置
        /// <summary>
        /// 设置广告配置
        /// </summary>
        public virtual void setAdConfig(string adsConfig)
        {
            if (string.IsNullOrEmpty(adsConfig))
            {
                LogUtil.Log("[BaseChannel]设置广告配置 配置为空");
                return;
            }
            LogUtil.Log("[BaseChannel]设置广告配置 adsConfig:" + adsConfig);

        }

        /// <summary>
        /// 设置广告限制配置
        /// </summary>
        public virtual void setAdLimitConfig(string adLimitJson)
        {
            if (string.IsNullOrEmpty(adLimitJson))
            {
                LogUtil.Log("[BaseChannel]设置广告限制配置 配置为空");
                return;
            }
            LogUtil.Log("[BaseChannel]设置广告限制配置 adLimitJson:" + adLimitJson);
        }

        /// <summary>
        /// 设置插屏时间间隔
        /// </summary>
        public virtual void setIntstitlAdInterval(int timeInterval)
        {
            LogUtil.Log("[BaseChannel]设置插屏时间间隔 timeInterval:" + timeInterval);
        }
        #endregion

        #region 调用: 插屏广告
        /// <summary>
        /// 缓存插屏广告
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public virtual void preLoadInterstitialAd(string placementKey)
        {
            LogUtil.Log("[BaseChannel]缓存插屏广告 placementKey:" + placementKey);
        }

        /// <summary>
        /// 是否缓存插屏广告完成
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public virtual bool isPreLoadIntersititialAd(string placementKey)
        {
            LogUtil.LogFormat("[BaseChannel]是否缓存插屏广告完成 placementKey:{0} isPreLoad:{1}", placementKey, AppConst.ChannelTest_IsPreLoadIntersititialAd);
            return AppConst.ChannelTest_IsPreLoadIntersititialAd;
        }

        /// <summary>
        /// 显示插屏广告
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public virtual void showInterstitialAd(string placementKey)
        {
            LogUtil.Log("[BaseChannel]显示插屏广告 placementKey:" + placementKey);
            cbApi.onInterstitialAdClose(placementKey);
        }
        #endregion

        #region 调用: 视频广告（带打点）
        /// <summary>
        /// 视频按钮展示（曝光）
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public virtual void logRewardedAdBtnExposure(string placementKey)
        {
            LogUtil.LogFormat("[BaseChannel] logRewardedAdBtnExposure 视频按钮展示 placementKey:{0}", placementKey);
        }

        /// <summary>
        /// 点击视频按钮触发奖励广告
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public virtual bool touchAndShowRewardedAd(string placementKey)
        {
            LogUtil.LogFormat("[BaseChannel] touchAndShowRewardedAd 展示打点视频广告 placementKey:{0},{1}", placementKey, true);
            return true;
        }
        #endregion

        #region 调用: 视频广告
        /// <summary>
        /// 缓存视频广告
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public virtual void preLoadVideoAd(string placementKey)
        {
            LogUtil.Log("[BaseChannel]缓存视频广告 placementKey:" + placementKey);
        }

        /// <summary>
        /// 视频广告是否缓存成功
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public virtual bool isPreLoadVideoAdSuccess(string placementKey)
        {
            LogUtil.LogFormat("[BaseChannel]视频广告是否缓存成功 placementKey:{0} {1}", placementKey, AppConst.ChannelTest_isPreLoadVideoAdSuccess);
            return AppConst.ChannelTest_isPreLoadVideoAdSuccess;
        }

        /// <summary>
        /// 展示视频广告
        /// </summary>
        /// <param name="placementKey">广告位</param>
        /// <param name="viewId">viewId 控件id(与 Android 层无关，不往上层传)</param>
        public virtual void showVideoAd(string placementKey, int viewId = 0)
        {
            LogUtil.Log("[BaseChannel]展示视频广告 placementKey:" + placementKey);
            if (TestConst.isTestWatchVideoAdSuccess)
            {
                cbApi.onVideoAdRewarded(placementKey);
            }
            else
            {
                cbApi.onVideoAdFaiToLoaded(placementKey);
            }
        }
        #endregion

        #region 调用：新版广告
        public virtual void adFlyShowIconAd(string placementId, int width, int height, int left, int top, bool isMoveType = false)
        {
            LogUtil.Log("[BaseChannel]小布科技 显示 placementId:" + placementId + " width:" + width
                + " height:" + height + " left:" + left + " top:" + top + " isMoveType: " + isMoveType);
        }

        public virtual void adFlyHideIconAd(string placementId)
        {
            LogUtil.Log("[BaseChannel]小布科技 关闭 placementId:" + placementId);
        }

        public virtual void okSpinLoadIconAd(string placementId)
        {
            LogUtil.Log("[BaseChannel]adt 读取icon placementId:" + placementId);
        }

        public virtual void okSpinShowIconAd(string placementId, int width, int height, int left, int top)
        {
            LogUtil.Log("[BaseChannel]adt 显示banner placementId:" + placementId + " width:" + width
                + " height:" + height + " left:" + left + " top:" + top);
        }

        public virtual void okSpinHideIconAd(string placementId)
        {
            LogUtil.Log("[BaseChannel]adt 关闭banner placementId:" + placementId);
        }

        public virtual void okSpinOpenInteractive(string placementId)
        {
            LogUtil.Log("[BaseChannel]adt 跳转活动面 placementId:" + placementId);
        }

        public virtual void flatAdsShowIconAd(string placementId, int width, int height, int left, int top)
        {
            LogUtil.Log("[BaseChannel]显示flatAdsShowIconAd placementId:" + placementId + " width:" + width
                + " height:" + height + " left:" + left + " top:" + top);
        }

        public virtual void flatAdsHideIconAd(string placementId)
        {
            LogUtil.Log("[BaseChannel]flatAdsShowIconAd 关闭 placementId:" + placementId);
        }
        #endregion

        #region 调用: 横幅广告
        /// <summary>
        /// 显示底部横幅广告
        /// </summary>
        public virtual void showBannerAdView()
        {
            LogUtil.Log("[BaseChannel]显示底部横幅广告");
        }

        /// <summary>
        /// 隐藏底部横幅广告
        /// </summary>
        public virtual void hideBannerAdView()
        {
            LogUtil.Log("[BaseChannel]隐藏底部横幅广告");
        }

        /// <summary>
        /// 显示中部横幅广告
        /// </summary>
        public virtual void showCenterBannerAdView()
        {
            LogUtil.Log("[BaseChannel]显示中部横幅广告");
        }

        /// <summary>
        /// 隐藏中部横幅广告
        /// </summary>
        public virtual void hideCenterBannerAdView()
        {
            LogUtil.Log("[BaseChannel]隐藏中部横幅广告");
        }
        #endregion

        #region 调用: 积分墙
        /// <summary>
        /// 打开Fyber积分墙
        /// </summary>
        public virtual void openFyber()
        {
            LogUtil.Log("[BaseChannel]打开Fyber积分墙");
        }

        /// <summary>
        /// 打开Ironsource积分墙
        /// </summary>
        public virtual void openIronsource()
        {
            LogUtil.Log("[BaseChannel]打开Ironsource积分墙");
        }

        /// <summary>
        /// 打开Tayjor积分墙
        /// </summary>
        public virtual void openTayjor()
        {
            LogUtil.Log("[BaseChannel]打开Tayjor积分墙");
        }
        #endregion

        #region 调用: 导流位
        /// <summary>
        /// 跳转Google Play商店
        /// </summary>
        /// <param name="pg">包名</param>
        public virtual void openAppInGooglePlayByPg(string pg)
        {
            LogUtil.Log("[BaseChannel]跳转Google Play商店 pg:" + pg);
        }

        /// <summary>
        /// 跳转网页
        /// </summary>
        /// <param name="url">地址</param>
        public virtual void openWebPage(string url)
        {
            LogUtil.Log("[BaseChannel]跳转网页 url:" + url);
            Application.OpenURL(url);
        }

        /// <summary>
        /// 打开apk应用
        /// </summary>
        public virtual void openApp(string pg)
        {
            LogUtil.Log("[BaseChannel]打开apk应用 pg:" + pg);
        }

        /// <summary>
        /// 检查pg是否已经安装
        /// </summary>
        public virtual bool checkAppInstalled(string pg)
        {
            LogUtil.Log("[BaseChannel]检查是否已经安装 pg:" + pg);
            return true;
        }

        /// <summary>
        /// 打开app关联平台
        /// </summary>
        public virtual void openMarket(string pg, string referrer)
        {
            LogUtil.Log("[BaseChannel]检查是否已经安装 pg:" + pg + ", referrer: " + referrer);
        }

        /// <summary>
        /// 分享有效用户打点
        /// </summary>
        public virtual void logEffective()
        {
            LogUtil.Log("[BaseChannel]分享有效用户打点 ");
        }

        #endregion

        #region 调用: 友盟通用打点统计
        /// <summary>
        /// 不带参数统计
        /// </summary>
        public virtual void onEvent(string key)
        {
            LogUtil.Log("[BaseChannel]友盟不带参数统计 key:" + key);
        }

        /// <summary>
        /// 带参数统计
        /// </summary>
        public virtual void onEventParam(string key, string value)
        {
            LogUtil.Log("[BaseChannel]友盟带参数统计 key:" + key + " value:" + value);
        }

        /// <summary>
        /// 时长统计
        /// </summary>
        public virtual void onEventValue(string key, int value, bool isLog = true)
        {
            if (isLog)
            {
                LogUtil.Log("[BaseChannel]友盟时长统计 key:" + key + " value:" + value);
            }
        }
        #endregion

        #region 调用: 友盟打点统计
        /// <summary>
        /// 统计玩家登录
        /// </summary>
        /// <param name="id">传邀请码</param>
        public virtual void onProfileSignIn(string id)
        {
            LogUtil.Log("[BaseChannel]统计玩家登录 id:" + id);
        }

        /// <summary>
        /// 统计玩家等级
        /// </summary>
        public virtual void setPlayerLevel(int level)
        {
            LogUtil.Log("[BaseChannel]统计玩家等级 level:" + level);
        }

        /// <summary>
        /// 统计开始关卡
        /// </summary>
        public virtual void startLevel(string level)
        {
            LogUtil.Log("[BaseChannel]统计开始关卡 level:" + level);
        }

        /// <summary>
        /// 统计关卡通关成功
        /// </summary>
        public virtual void finishLevel(string level)
        {
            LogUtil.Log("[BaseChannel]统计关卡通关成功 level:" + level);
        }

        /// <summary>
        /// 统计关卡通关失败
        /// </summary>
        /// <param name="level"></param>
        public virtual void failLevel(string level)
        {
            LogUtil.Log("[BaseChannel]统计关卡通关失败 level:" + level);
        }
        #endregion

        #region 调用: FaceBook打点统计
        public virtual void onFBEvent(string key)
        {
            LogUtil.Log("[BaseChannel]FB统计 key:" + key);
        }

        public virtual void onFBEvent(string key, double value)
        {
            LogUtil.Log("[BaseChannel]FB统计 key:" + key + " value:" + value);
        }

        public virtual void onFBEvent(string key, string value)
        {
            LogUtil.Log("[BaseChannel]FB统计 key:" + key + " value:" + value);
        }

        public virtual void onFBEvent(string key, double valueToSum, string parametersJson)
        {
            LogUtil.Log("[BaseChannel]FB统计 key:" + key + " valueToSum:" + valueToSum + "parametersJson:" + parametersJson);
        }
        #endregion

        #region 调用: firebase事件投放使用
        public virtual void onUpdateLtv24HJson(string configJson)
        {
            LogUtil.Log("[BaseChannel]firebase事件投放使用 configJson:" + configJson);
        }
        #endregion

        #region 调用: 硬件
        /// <summary>
        /// 开启震动
        /// </summary>
        /// <param name="milliseconds">震动的持续时间</param>
        /// <param name="amplitude">震动强度 1-255</param>
        public virtual void vibrate(long milliseconds, int amplitude)
        {
            VibrationHelper.Vibrate();
        }

        /// <summary>
        /// 取消震动
        /// </summary>
        public virtual void virateCancel()
        {
            LogUtil.Log("[BaseChannel]取消震动");
        }
        #endregion

        #region 调用: 加密
        /// <summary>
        /// 加密
        /// </summary>
        public virtual string encryptedString(string name, string raw, string associatedData)
        {
            return JsonEncryptUtil.EncryptString(name, raw, associatedData);
        }

        /// <summary>
        /// 解密
        /// </summary>
        public virtual string decryptedString(string name, string encryptedValue, string associatedData)
        {
            return JsonEncryptUtil.DecryptString(name, encryptedValue, associatedData);
        }
        #endregion

        #region 调用: 事件
        /// <summary>
        /// appsFlyer 应用内事件
        /// </summary>
        public virtual void onAppsFlyerEvent(string key, string value)
        {
            LogUtil.Log("[BaseChannel]appsFlyer 应用内事件:" + key + " " + value);
        }
        #endregion

        #region 调用: 事件统计上报
        /// <summary>
        /// 登录成功
        /// </summary>
        public virtual void onLoginSuccess(long uid, string token, string url)
        {
            LogUtil.Log("[BaseChannel]onLoginSuccess");
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
        public virtual void logAd(string sdk, string type, string id, string name, string eventname, double revenue)
        {
            LogUtil.LogFormat("[BaseChannel]logAd sdk:{0} type:{1} name:{2} eventname:{3} revenue:{4}", sdk, type, name, eventname, revenue);
        }

        /// <summary>
        /// USER_EVENT 用户事件
        /// </summary>
        /// <param name="e">事件名称</param>
        /// <param name="times">事件发生次数</param>
        public virtual void logUserEvent(string e, int times)
        {
            LogUtil.LogFormat("[BaseChannel]logUserEvent e:{0} times:{1}", e, times);
        }

        /// <summary>
        /// USER_STATUS 用户状态
        /// </summary>
        /// <param name="name">状态名</param>
        /// <param name="value">状态值</param>
        public virtual void logUserStatus(string name, string value)
        {
            LogUtil.LogFormat("[BaseChannel]logUserStatus name:{0} value:{1}", name, value);
        }

        /// <summary>
        /// 普通统计上报
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="value">事件参数</param>
        public virtual void logNormal(string name, string value)
        {
            LogUtil.LogFormat("[BaseChannel]logNormal name:{0} value:{1}", name, value);
        }

        /// <summary>
        /// Json普通统计上报
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="mapJsonStr">事件参数</param>
        public virtual void logNormalForJson(string name, string mapJsonStr)
        {
            LogUtil.LogFormat("[BaseChannel]logNormalForJson name:{0} mapJsonStr:{1}", name, mapJsonStr);
        }
        #endregion

        #region 调用: (旧)内购/订阅
        /// <summary>
        /// 点击内购按钮
        /// </summary>
        /// <param name="skuId">商品Id</param>
        public virtual void clickInAppBuyBtn(string skuId)
        {
            LogUtil.Log("[BaseChannel]点击内购按钮 skuId:" + skuId);
        }

        /// <summary>
        /// 点击订阅按钮
        /// </summary>
        /// <param name="skuId">商品Id</param>
        public virtual void clickSubscribeBtn(string skuId)
        {
            LogUtil.Log("[BaseChannel]点击订阅按钮 skuId:" + skuId);
        }

        /// <summary>
        /// 查询订阅购买历史
        /// </summary>
        public virtual void queryPurchaseHistory()
        {
            LogUtil.Log("[BaseChannel]查询订阅购买历史");
        }
        #endregion

        #region 调用: (新)内购/订阅
        /// <summary>
        /// 登录后查询商品属性
        /// (获取价格)
        /// </summary>
        public virtual void queryBillingProductDetails(ProductDetailsData productDetailsData)
        {
            string json = SerializeUtil.ToJson(productDetailsData);
            LogUtil.Log("[BaseChannel]登录后查询商品属性, json: " + json);
        }

        /// <summary>
        /// 查询订阅购买历史
        /// (获取购买订单)
        /// </summary>
        public virtual void queryBillingPurchaseHistory()
        {
            LogUtil.Log("[BaseChannel]查询订阅购买历史");
        }

        /// <summary>
        /// 内购/订阅是否初始化成功
        /// </summary>
        public virtual bool isBillingInitSuccess()
        {
            bool state = true;
            LogUtil.Log("[BaseChannel]内购/订阅是否初始化状态: " + state);
            return state;
        }

        /// <summary>
        /// 获取现在的购买价格
        /// </summary>
        public virtual string getBillingProductPrice(string productID)
        {
            string price = "US $0";
            LogUtil.LogFormat("[BaseChannel]获取现在的购买价格: productID:{0} price:{1}", productID, price);
            return price;
        }

        /// <summary>
        /// 获取原来的购买价格
        /// </summary>
        public virtual string getBillingProductOriginalPrice(string productID)
        {
            string price = "US $0";
            LogUtil.LogFormat("[BaseChannel]获取原来的购买价格: productID:{0} price:{1}", productID, price);
            return price;
        }

        /// <summary>
        /// 调起IAP
        /// </summary>
        public virtual void launchBillingFlow(string productID)
        {
            LogUtil.LogFormat("[BaseChannel]调起IAP: productID:{0}", productID);

            App.HideWaitUI();
            // 逻辑层自己模拟
            if (productID.Contains("subs_"))
            {

            }
            else if (productID.Contains("iap_"))
            {

            }
        }
        #endregion

        #region 调用: 上报异常
        /// <summary>
        /// 上报异常
        /// </summary>
        public virtual void reportError(string errorMsg)
        {
        }
        #endregion

        #region 调用: adjust打点
        /// <summary>
        /// 通过事件名打点
        /// </summary>
        public virtual void trackEventByEventKey(string adjustEventKey)
        {
        }

        /// <summary>
        /// adjust事件 
        /// </summary>
        public virtual void setTrackEventTokenMap(string adjust_event_json)
        {
            LogUtil.Log("[BaseChannel]adjust_event_json adjust事件:" + adjust_event_json);
        }

        /// <summary>
        /// ads_video事件 
        /// </summary>
        public virtual void setAdsNewConfig(string adsNewJson)
        {
            LogUtil.Log("[BaseChannel]setAdsNewConfig ads_video事件:" + adsNewJson);
        }

        /// <summary>
        /// ads_icon事件 
        /// </summary>
        public virtual void setAdsIconConfig(string configJson)
        {
            LogUtil.Log("[BaseChannel]setAdsIconConfig ads_icon事件:" + configJson);
        }

        /// <summary>
        /// 网络优化配置事件 
        /// </summary>
        public virtual void setNetOptConfig(string configJson, string uid)
        {
            LogUtil.Log("[BaseChannel]setNetOptConfig 网络优化配置事件 :" + configJson + ", uid = " + uid);
        }

        /// <summary>
        /// 插屏广告配置事件 
        /// </summary>
        public virtual void setAdsInterstitialConfig(string configJson)
        {
            LogUtil.Log("[BaseChannel] setAdsInterstitialConfig 插屏广告配置 :" + configJson );
        }
        
        #endregion
    }
}