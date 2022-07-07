using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class BuildApkClientTool
    {
        private static string BuildFolder = "_Build/APK/";
        private static string BuildPath = EditorPathConst.ProjectPath + BuildFolder;
        private const BuildOptions CompressBuildOption = BuildOptions.None;

        [MenuItem("[FC Release]/OtherBuild/APK/构建内网测试.apk", false, 1)]
        private static void BuildLocalDebugApk()
        {
            Hashtable appInfo = new Hashtable();
            appInfo["AppName"] = AppFacade_Editor.AppName + "_localdebug" + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".apk";
            appInfo["ChannelType"] = EditorChannelType.LocalDebug;
            appInfo["ChannelName"] = "内网测试";
            appInfo["IsRelease"] = false;
            appInfo["AppVersion"] = "1.0.1";
            BuildApkClient(appInfo);
            UnityEngine.Debug.Log("生成内网测试包成功");
        }

        [MenuItem("[FC Release]/OtherBuild/APK/构建外网审核.apk", false, 2)]
        private static void BuildNetCheckApk()
        {
            Hashtable appInfo = new Hashtable();
            appInfo["AppName"] = AppFacade_Editor.AppName + "_netcheck" + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".apk";
            appInfo["ChannelType"] = EditorChannelType.NetCheck;
            appInfo["ChannelName"] = "外网审核";
            appInfo["IsRelease"] = true;
            appInfo["AppVersion"] = "1.0.1";
            BuildApkClient(appInfo);
            UnityEngine.Debug.Log("生成外网审核包成功");
        }

        [MenuItem("[FC Release]/OtherBuild/APK/构建外网正式.apk", false, 3)]
        private static void BuildNetReleaseApk()
        {
            Hashtable appInfo = new Hashtable();
            appInfo["AppName"] = AppFacade_Editor.AppName + "_netrelease" + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".apk";
            appInfo["ChannelType"] = EditorChannelType.NetRelease;
            appInfo["ChannelName"] = "外网正式";
            appInfo["IsRelease"] = true;
            appInfo["AppVersion"] = "1.0.1";
            BuildApkClient(appInfo);
            UnityEngine.Debug.Log("生成外网正式包成功");
        }

        private static void BuildApkClient(Hashtable appInfo)
        {
            if (!Directory.Exists(BuildFolder))
                Directory.CreateDirectory(BuildFolder);
            if (!Directory.Exists(BuildClientTool.AppInfoFolder))
                Directory.CreateDirectory(BuildClientTool.AppInfoFolder);

            string buildPath = BuildFolder + appInfo["AppName"];

            bool isRelease = (bool)appInfo["IsRelease"];
            SetAndroidPlayerSetting();
            SetClientVersion(appInfo, isRelease);
            SetAndroidKeystore();

            BuildClientTool.CreateAppInfo(appInfo);
            BuildClientTool.BuildMonoPlayer(AppFacade_Editor.AppName, buildPath, BuildTarget.Android, CompressBuildOption);

            Process.Start(BuildPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void SetAndroidPlayerSetting()
        {
            // 有一定几率下 会在一些机型下引起崩溃 屏幕旋转设置由平台层实现
            // 屏幕旋转设置
            //Screen.orientation = ScreenOrientation.Portrait;
            //Screen.autorotateToPortrait = false;
            //Screen.autorotateToPortraitUpsideDown = false;
            //Screen.autorotateToLandscapeLeft = false;
            //Screen.autorotateToLandscapeRight = false;
        }

        private static void SetClientVersion(Hashtable appInfo, bool isRelease)
        {
            PlayerSettings.bundleVersion = (string)appInfo["AppVersion"];
            if (isRelease)
            {
                int bundleVersionCode = PlayerSettings.Android.bundleVersionCode;
                PlayerSettings.Android.bundleVersionCode = bundleVersionCode + 1;
            }
        }

        private static void SetAndroidKeystore()
        {
            PlayerSettings.Android.keystoreName = null;
            PlayerSettings.Android.keystorePass = null;
            PlayerSettings.Android.keyaliasName = null;
            PlayerSettings.Android.keyaliasPass = null;
            //PlayerSettings.Android.keystoreName = EditorPathConst.ProjectPath + "_Keystore/app.keystore";
            //PlayerSettings.Android.keystorePass = "123456";
            //PlayerSettings.Android.keyaliasName = "app";
            //PlayerSettings.Android.keyaliasPass = "123456";
        }
    }
}