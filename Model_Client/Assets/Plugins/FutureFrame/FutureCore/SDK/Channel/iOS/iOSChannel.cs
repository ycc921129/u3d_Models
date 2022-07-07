/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using FuturePlugin;
using UnityEngine;

namespace FutureCore
{
    public class iOSChannel : BaseChannel
    {
        protected override void SetChannelDefine()
        {
            channelDefine = ChannelDefine.iOS;
        }

        #region 硬件字段

        /// <summary>
        /// 语言
        /// </summary>
        public override string lang
        {
            get { return iOSChannelApi.lang(); }
        }

        /// <summary>
        /// 设备生产商
        /// </summary>
        public override string dev_fac
        {
            get { return iOSChannelApi.dev_fac(); }
        }

        /// <summary>
        /// 系统版本值(Android版本)
        /// </summary>
        public override int sdk_int
        {
            get { return iOSChannelApi.sdk_int(); }
        }

        /// <summary>
        /// 设备型号
        /// </summary>
        public override string dev_model
        {
            get { return iOSChannelApi.dev_model(); }
        }

        /// <summary>
        /// 是否root,默认为false
        /// </summary>
        public override bool isRoot
        {
            get { return iOSChannelApi.isRoot(); }
        }

        /// <summary>
        /// 是否通过代理方式
        /// </summary>
        public override bool isProxy
        {
            get { return iOSChannelApi.isProxy(); }
        }

        /// <summary>
        /// 是否通过VPN方式
        /// </summary>
        public override bool isVPN
        {
            get { return iOSChannelApi.isVPN(); }
        }

        #endregion 硬件字段

        #region 业务字段

        /// <summary>
        /// 包名
        /// </summary>
        public override string pg
        {
            get
            {
                string tmp_pg = iOSChannelApi.pg();
                if (tmp_pg.EndsWith(".dev"))
                {
                    return AppFacade_Frame.PackageName;
                }
                return tmp_pg;
            }
        }

        /// <summary>
        /// 客户端版本值, 用于判定升级
        /// </summary>
        public override int ver_code
        {
            get { return iOSChannelApi.ver_code(); }
        }

        /// <summary>
        /// 客户端版本
        /// </summary>
        public override string ver
        {
            get { return iOSChannelApi.ver(); }
        }

        /// <summary>
        /// 系统+版本
        /// </summary>
        public override string os_ver
        {
            get { return iOSChannelApi.os_ver(); }
        }

        /// <summary>
        /// 渠道名
        /// </summary>
        public override string channel
        {
            get { return iOSChannelApi.channel(); }
        }

        /// <summary>
        /// AndroidID
        /// </summary>
        public override string aid
        {
            get { return iOSChannelApi.aid(); }
        }

        /// <summary>
        /// 广告id
        /// </summary>
        public override string idfa
        {
            get { return iOSChannelApi.idfa(); }
        }

        /// <summary>
        /// id组
        /// </summary>
        public override string ids
        {
            get { return iOSChannelApi.ids(); }
        }

        /// <summary>
        /// 友盟id
        /// </summary>
        public override string umid
        {
            get { return iOSChannelApi.umid(); }
        }

        /// <summary>
        /// 客户端是否是Debug版本
        /// </summary>
        public override bool debug
        {
            get
            {
                return iOSChannelApi.debug();
            }
        }

        /// <summary>
        /// 编译类型
        /// </summary>
        public override AppBuildType buildType
        {
            get
            {
                return (AppBuildType)iOSChannelApi.buildType();
            }
        }

        /// <summary>
        /// 网络类型
        /// </summary>
        public override string network
        {
            get { return iOSChannelApi.network(); }
        }

        /// <summary>
        /// 网络是否可用
        /// </summary>
        public override bool isNetworkAvailable
        {
            get { return iOSChannelApi.isNetworkAvailable(); }
        }

        #endregion 业务字段

        #region 调用: 验证登录
        /// <summary>
        /// 发送UID
        /// </summary>
        public override void sendUID(string uid)
        {
            LogUtil.Log("[iOSChannel]发送UID: " + uid);
            iOSChannelApi.sendUID(uid);
        }

        /// <summary>
        /// 通知客户端场景已经初始化
        /// </summary>
        public override void directorInitSuccess()
        {
            LogUtil.Log("[iOSChannel]通知客户端场景已经初始化");
            iOSChannelApi.directorInitSuccess();
        }
        #endregion

        #region 调用: 平台相关
        /// <summary>
        /// 获取应用名
        /// </summary>
        public override string getAppName()
        {
            string appName = iOSChannelApi.getAppName();
            LogUtil.Log("[iOSChannel]getAppName: " + appName);
            return appName;
        }

        /// <summary>
        /// 隐藏闪屏
        /// </summary>
        public override void hideSplashPage()
        {
            LogUtil.Log("[iOSChannel]隐藏闪屏");
            iOSChannelApi.hideSplashPage();
        }

        /// <summary>
        /// 反馈
        /// </summary>
        public override void feedback(string mailbox)
        {
            LogUtil.Log("[iOSChannel]feedback");
            iOSChannelApi.feedback(mailbox);
        }

        /// <summary>
        /// 跳转商店App界面
        /// </summary>
        public override void rate()
        {
            LogUtil.Log("[iOSChannel]跳转商店App界面");
            iOSChannelApi.rate();
        }
        /// <summary>
        /// 跳转到GooglePlay更新App更新App
        /// </summary>
        public override void updateApp(string json)
        {
            LogUtil.Log("[iOSChannel]updateApp");
            iOSChannelApi.updateApp(json);
        }
        /// <summary>
        /// toast
        /// </summary>
        public override void toast(string s)
        {
            LogUtil.Log("[iOSChannel]toast");
            iOSChannelApi.toast(s);
        }
        #endregion

        #region 调用: 插屏广告
        /// <summary>
        /// 缓存插屏广告
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public override void preLoadInterstitialAd(string placementKey)
        {
            LogUtil.Log("[iOSChannel]缓存插屏广告 placementKey:" + placementKey);
            iOSChannelApi.preLoadInterstitialAd(placementKey);
        }

        /// <summary>
        /// 是否缓存插屏广告完成
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public override bool isPreLoadIntersititialAd(string placementKey)
        {
            bool isPreLoad = iOSChannelApi.isPreLoadIntersititialAd(placementKey);
            LogUtil.LogFormat("[iOSChannel]是否缓存插屏广告完成 placementKey:{0} isPreLoad:{1}", placementKey, isPreLoad);
            return isPreLoad;
        }

        /// <summary>
        /// 显示插屏广告
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public override void showInterstitialAd(string placementKey)
        {
            LogUtil.Log("[iOSChannel]显示插屏广告 placementKey:" + placementKey);
            iOSChannelApi.showInterstitialAd(placementKey);
        }
        #endregion

        #region 调用: 视频广告
        /// <summary>
        /// 缓存视频广告
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public override void preLoadVideoAd(string placementKey)
        {
            LogUtil.Log("[iOSChannel]缓存视频广告 placementKey:" + placementKey);
            iOSChannelApi.preLoadVideoAd(placementKey);
        }

        /// <summary>
        /// 视频广告是否缓存成功
        /// </summary>
        /// <param name="placementKey">广告位</param>
        public override bool isPreLoadVideoAdSuccess(string placementKey)
        {
            bool isPreLoad = iOSChannelApi.isPreLoadVideoAdSuccess(placementKey);
            LogUtil.LogFormat("[iOSChannel]视频广告是否缓存成功 placementKey: {0} {1}", placementKey, isPreLoad);
            return isPreLoad;
        }

        /// <summary>
        /// 展示视频广告
        /// </summary>
        /// <param name="placementKey">广告位</param>
        /// <param name="viewId">viewId 控件id(与Android层无关，不往上层传)</param>
        public override void showVideoAd(string placementKey, int viewId = 0)
        {
            LogUtil.Log("[iOSChannel]展示视频广告 placementKey:" + placementKey);
            iOSChannelApi.showVideoAd(placementKey);
        }
        #endregion

        #region 调用: 横幅广告
        /// <summary>
        /// 显示底部横幅广告
        /// </summary>
        public override void showBannerAdView()
        {
            LogUtil.Log("[iOSChannel]显示底部横幅广告");
            iOSChannelApi.showBannerAdView();
        }

        /// <summary>
        /// 隐藏底部横幅广告
        /// </summary>
        public override void hideBannerAdView()
        {
            LogUtil.Log("[iOSChannel]隐藏底部横幅广告");
            iOSChannelApi.hideBannerAdView();
        }

        /// <summary>
        /// 显示中部横幅广告
        /// </summary>
        public override void showCenterBannerAdView()
        {
            LogUtil.Log("[iOSChannel]显示中部横幅广告");
            iOSChannelApi.showCenterBannerAdView();
        }

        /// <summary>
        /// 隐藏中部横幅广告
        /// </summary>
        public override void hideCenterBannerAdView()
        {
            LogUtil.Log("[iOSChannel]隐藏中部横幅广告");
            iOSChannelApi.hideCenterBannerAdView();
        }
        #endregion

        #region 调用: 友盟通用打点统计
        /// <summary>
        /// 不带参数统计
        /// </summary>
        public override void onEvent(string key)
        {
            LogUtil.Log("[iOSChannel]友盟不带参数统计 key:" + key);
            iOSChannelApi.onEvent(key);
        }

        /// <summary>
        /// 带参数统计
        /// </summary>
        public override void onEventParam(string key, string value)
        {
            LogUtil.Log("[iOSChannel]友盟带参数统计 key:" + key + " value:" + value);
            iOSChannelApi.onEventParam(key, value);
        }

        /// <summary>
        /// 时长统计
        /// </summary>
        public override void onEventValue(string key, int value, bool isLog = true)
        {
            if (isLog)
            {
                LogUtil.Log("[iOSChannel]友盟时长统计 key:" + key + " value:" + value);
            }
            iOSChannelApi.onEventValue(key, value);
        }
        #endregion

        #region 调用: 友盟打点统计
        /// <summary>
        /// 传递邀请码
        /// </summary>
        public override void onProfileSignIn(string inviteCode)
        {
            LogUtil.Log("[iOSChannel]onProfileSignIn");
            iOSChannelApi.onProfileSignIn(inviteCode);
        }
        #endregion

        #region 事件统计上报

        /// 登录成功
        /// </summary>
        public override void onLoginSuccess(long uid, string token, string url)
        {
            iOSChannelApi.onLoginSuccess(uid, token, url);
            LogUtil.Log("[iOSChannel]onLoginSuccess");
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
            iOSChannelApi.logAd(sdk, type, id, name, eventname, revenue);
            LogUtil.LogFormat("[iOSChannel]logAd sdk:{0} type:{1} name:{2} eventname:{3} revenue:{4}", sdk, type, name, eventname, revenue);
        }

        /// <summary>
        /// USER_EVENT 用户事件
        /// </summary>
        /// <param name="e">事件名称</param>
        /// <param name="times">事件发生次数</param>
        public override void logUserEvent(string e, int times)
        {
            iOSChannelApi.logUserEvent(e, times);
            LogUtil.LogFormat("[iOSChannel]logUserEvent e:{0} times:{1}", e, times);
        }

        /// <summary>
        /// USER_STATUS 用户状态
        /// </summary>
        /// <param name="name">状态名</param>
        /// <param name="value">状态值</param>
        public override void logUserStatus(string name, string value)
        {
            iOSChannelApi.logUserStatus(name, value);
            LogUtil.LogFormat("[iOSChannel]logUserStatus name:{0} value:{1}", name, value);
        }

        /// <summary>
        /// 普通统计上报
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="value">事件参数</param>
        public override void logNormal(string name, string value)
        {
            iOSChannelApi.logNormal(name, value);
            LogUtil.LogFormat("[iOSChannel]logNormal name:{0} value:{1}", name, value);
        }

        /// <summary>
        /// Json普通统计上报
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="mapJsonStr">事件参数</param>
        public override void logNormalForJson(string name, string mapJsonStr)
        {
            iOSChannelApi.logNormalForJson(name, mapJsonStr);
            LogUtil.LogFormat("[iOSChannel]logNormalForJson name:{0} mapJsonStr:{1}", name, mapJsonStr);
        }

        #endregion
    }
}