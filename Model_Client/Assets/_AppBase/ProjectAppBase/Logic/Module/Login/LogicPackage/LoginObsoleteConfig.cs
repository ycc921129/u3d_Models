/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using ProjectApp.Protocol;
using System;
using System.IO;
using UnityEngine.Networking;

namespace ProjectApp
{
    /// <summary>
    /// 使用Http游戏配置
    /// (服务器没压缩, 只加密了数据)
    /// </summary>
    public static class LoginObsoleteConfig
    {
        private static string localConfigUrl;
        private static string serverConfigUrl;

        private static int maxSendConfigUrlTimes = 2;
        private static int sendConfigUrlTimes;

        private static Action completeFunc;

        #region 旧版游戏配置
        public static string ReadObsoleteConfigLocalUrl()
        {
            return PrefsUtil.ReadString(PrefsKeyConst.LoginCtrl_configLocalUrl);
        }

        public static bool IsExistObsoleteConfig()
        {
            if (!Directory.Exists(PathConst.ObsoleteConfigDir))
            {
                return false;
            }
            string[] files = Directory.GetFiles(PathConst.ObsoleteConfigDir);
            if(PrefsUtil.HasKey(PrefsKeyConst.LoginCtrl_configLocalUrl) && files.Length >= 1)
            {
                return true;
            }
            return false;
        }

        public static bool IsCanReadObsoleteConfig()
        {
            string url = ReadObsoleteConfigLocalUrl();
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            string serverVersion = url.Substring(url.LastIndexOf("/") + 1).Replace(".json", string.Empty);
            string path = PathConst.ObsoleteConfigDir + serverVersion + AppConst.ABExtName;
            if (!File.Exists(path))
            {
                return false;
            }

            object configObject = JsonEncryptUtil.ReadFormLocalFile<object>(path, url);
            bool isCan = configObject != null;
            return isCan;
        }
        #endregion

        public static void UpdateGameConfig(S2C_reg_login resp, Action completeFunc)
        {
            LoginObsoleteConfig.completeFunc = completeFunc;            
            //HACK mm1服务器 弃用
            //if (resp.data.pg_settingex == null)
            //{
            //    LogUtil.Log("[LoginObsoleteConfig]Login to the server and cancel the offline login");
            //    LoginObsoleteConfig.completeFunc();
            //    return;
            //}

            //serverConfigUrl = resp.data.game_settings_url;
            //localConfigUrl = ReadObsoleteConfigLocalUrl();

            //string serverVersion = serverConfigUrl.Substring(serverConfigUrl.LastIndexOf("/") + 1).Replace(".json", string.Empty);

            //AppConst.ConfigServerHash = serverVersion;
            //AppConst.ConfigServerVersion = serverVersion;

            //if (AppConst.ConfigInternalVersion == AppConst.ConfigServerVersion)
            //{
            //    LoginObsoleteConfig.completeFunc();
            //    return;
            //}
            //else if (!string.IsNullOrEmpty(localConfigUrl) && localConfigUrl == serverConfigUrl)
            //{
            //    LogUtil.Log("[LoginObsoleteConfig]Curr LocalConfigUrl: " + localConfigUrl);
            //    string serverConfigSaveFile = PathConst.ObsoleteConfigDir + AppConst.ConfigServerVersion + AppConst.ABExtName;
            //    if (File.Exists(serverConfigSaveFile))
            //    {
            //        LoginObsoleteConfig.completeFunc();
            //        return;
            //    }
            //}
            SendDownConfig();
        }

        private static void SendDownConfig()
        {
            AppDispatcher.Instance.Dispatch(AppMsg.Http_RequestConfiging);

            sendConfigUrlTimes++;
            HttpMgr.Instance.Send(serverConfigUrl, OnDownUpdateConfig, 5);

            // 统计
            ChannelMgr.Instance.SendStatisticEventWithParam(StatisticConst.game_settings_url_request, sendConfigUrlTimes.ToString());
            ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.game_settings_url_success);
            ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.game_settings_url_fail);

            LogUtil.Log("[LoginObsoleteConfig]SendDownConfig: " + serverConfigUrl);
        }

        private static void OnDownUpdateConfig(bool isError, DownloadHandler downloadHandler)
        {
            if (isError && sendConfigUrlTimes < maxSendConfigUrlTimes)
            {
                // 统计
                ChannelMgr.Instance.DeleteStatisticTimeEvent(StatisticConst.game_settings_url_success);
                ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.game_settings_url_fail);

                SendDownConfig();
                return;
            }
            if (isError && sendConfigUrlTimes == maxSendConfigUrlTimes)
            {
                // 统计
                ChannelMgr.Instance.DeleteStatisticTimeEvent(StatisticConst.game_settings_url_success);
                ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.game_settings_url_fail);

                if (NetConst.IsNetAvailable)
                {
                    completeFunc();
                    TimerUtil.Simple.AddTimer(10f, () =>
                    {
                        HttpMgr.Instance.Send(serverConfigUrl, OnDownSaveConfig, 10);
                    });
                }
                LogUtil.Log("[LoginObsoleteConfig]Update Config Failed");
                return;
            }

            // 统计
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.game_settings_url_success);
            ChannelMgr.Instance.DeleteStatisticTimeEvent(StatisticConst.game_settings_url_fail);

            SaveObsoleteConfig(serverConfigUrl, downloadHandler.data);
            LogUtil.Log("[LoginObsoleteConfig]Update Config Succeed");  

            completeFunc();
        }

        private static void OnDownSaveConfig(bool isError, DownloadHandler downloadHandler)
        {
            if (isError) return;

            SaveObsoleteConfig(serverConfigUrl, downloadHandler.data);
            LogUtil.Log("[LoginObsoleteConfig]Save Update Config Succeed");
        }

        private static void SaveObsoleteConfig(string url, byte[] serverConfig)
        {
            PathUtil.ClearDir(PathConst.ObsoleteConfigDir);
            string serverConfigSaveFile = PathConst.ObsoleteConfigDir + AppConst.ConfigServerVersion + AppConst.ABExtName;
            File.WriteAllBytes(serverConfigSaveFile, serverConfig);
            PrefsUtil.WriteString(PrefsKeyConst.LoginCtrl_configLocalUrl, url);
        }
    }
}