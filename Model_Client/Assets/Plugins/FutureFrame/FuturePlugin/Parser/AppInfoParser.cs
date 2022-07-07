/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using Newtonsoft.Json;
using System.Collections;
using UnityEngine;

namespace FuturePlugin
{
    public static class AppInfoParser
    {
        public static void Init()
        {
            InitDefine();
            AfterInitDefine();
        }

        private static void InitDefine()
        {
            InitAppInfoConst();
        }

        private static void InitAppInfoConst()
        {
#if !UNITY_EDITOR
            TextAsset appInfoAsset = Resources.Load<TextAsset>("Preset/VersionMgr/AppInfo");
            if (appInfoAsset && string.IsNullOrEmpty(appInfoAsset.text))
            {
                string appInfoText = appInfoAsset.text;
                Hashtable appInfo = JsonConvert.DeserializeObject<Hashtable>(appInfoText);

                AppInfoConst.AppName = (string)appInfo["AppName"];
                long channelType = (long)appInfo["ChannelType"];
                AppInfoConst.ChannelType = (ChannelType)channelType;
                AppInfoConst.ChannelName = (string)appInfo["ChannelName"];
                AppInfoConst.IsRelease = (bool)appInfo["IsRelease"];
                AppInfoConst.AppVersion = (string)appInfo["AppVersion"];
                AppInfoConst.BundleVersionCode = (long)appInfo["BundleVersionCode"];

                LogUtil.LogFormat("[AppInfo]AppInfo:\n{0}", appInfoText);
            }
            else
            {
                AppInfoConst.AppName = "开发环境";
                AppInfoConst.ChannelType = ChannelType.LocalDebug;
                AppInfoConst.ChannelName = "内网测试";
                AppInfoConst.IsRelease = false;
                AppInfoConst.AppVersion = "0.0.0";
                AppInfoConst.BundleVersionCode = 0;
            }
#else
            AppInfoConst.AppName = "开发环境";
            AppInfoConst.ChannelType = ChannelType.LocalDebug;
            AppInfoConst.ChannelName = "内网测试";
            AppInfoConst.IsRelease = false;
            AppInfoConst.AppVersion = "0.0.0";
            AppInfoConst.BundleVersionCode = 0;
#endif
        }

        private static void AfterInitDefine()
        {
        }
    }
}