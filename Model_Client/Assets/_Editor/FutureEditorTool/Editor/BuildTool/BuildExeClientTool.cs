using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEditor;

namespace FutureEditor
{
    public static class BuildExeClientTool
    {
        private static string BuildFolder = "_Build/EXE/";
        private static string BuildPath = EditorPathConst.ProjectPath + BuildFolder;
        private const BuildOptions CompressBuildOption = BuildOptions.None;

        [MenuItem("[FC Release]/OtherBuild/EXE/构建内网测试.exe", false, 0)]
        public static void BuildLocalDebugExe()
        {
            Hashtable appInfo = new Hashtable();
            appInfo["AppName"] = AppFacade_Editor.AppName.ToLower() + "_localdebug" + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".exe";
            appInfo["ChannelType"] = EditorChannelType.LocalDebug;
            appInfo["ChannelName"] = "内网测试";
            appInfo["IsRelease"] = false;
            appInfo["AppVersion"] = "1.0.0";
            BuildExeClient(appInfo);
            UnityEngine.Debug.Log("生成内网测试包成功");
        }

        private static void BuildExeClient(Hashtable appInfo)
        {
            if (!Directory.Exists(BuildFolder))
                Directory.CreateDirectory(BuildFolder);
            if (!Directory.Exists(BuildClientTool.AppInfoFolder))
                Directory.CreateDirectory(BuildClientTool.AppInfoFolder);

            string buildPath = BuildFolder + appInfo["AppName"];

            BuildClientTool.CreateAppInfo(appInfo);
            BuildClientTool.BuildMonoPlayer(AppFacade_Editor.AppName, buildPath, BuildTarget.StandaloneWindows64, CompressBuildOption);

            Process.Start(BuildPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}