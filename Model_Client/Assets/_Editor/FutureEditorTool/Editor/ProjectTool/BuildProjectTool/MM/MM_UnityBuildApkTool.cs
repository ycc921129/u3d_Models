using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using FuturePlugin;

namespace FutureEditor
{
    public static class MM_UnityBuildApkTool
    {
        // 测试包名
        private const string TEST_PACKAGENAME = "com.nullpack.dev";

        //    private enum BuildType
        //    {
        //        Debug,
        //        DebugFormal,
        //        Release,
        //        AppBundle
        //    }

        //    public enum BuildType : int
        //    {
        //        Debug = 1,
        //        DebugFormal = 2,
        //        Release = 3,
        //        All = 4,
        //    }


        private const string TAG = "[MM_UnityBuildApkTool]";
        private const string UNITY_BUILD_DEBUG = "UNITY_BUILD_DEBUG";
        private const string UNITY_BUILD_DEBUGFORMAL = "UNITY_BUILD_DEBUGFORMAL";
        private const string UNITY_BUILD_RELEASE = "UNITY_BUILD_RELEASE";

        private static readonly string[] UNITY_BUILD_LIST = { UNITY_BUILD_DEBUG, UNITY_BUILD_DEBUGFORMAL, UNITY_BUILD_RELEASE };

        private static string mBuildPath = Path.GetFullPath(Application.dataPath + "/../../UnityBuild/");
        private static string mAndroidModulePath = Path.GetFullPath(Application.dataPath + "../../../AndroidModule/");
        private static string mAndroidModuleAppPath = Path.GetFullPath(Application.dataPath + "../../../AndroidModule/app/");
        private static BuildOptions mBuildOptions;

        [MenuItem("[FC Release]/MM_UnityBuild/Debug（debug-测服）", false, -13)]
        public static bool BuildDebugApk()
        {
            ApplyPlayerSettings(AppBuildType.Debug);
            return BuildApk(AppBuildType.Debug);
        }

        [MenuItem("[FC Release]/MM_UnityBuild/DebugFormal（debug-正服）", false, -13)]
        public static bool BuildDebugFormalApk()
        {
            ApplyPlayerSettings(AppBuildType.DebugFormal);
            return BuildApk(AppBuildType.DebugFormal);
        }

        [MenuItem("[FC Release]/MM_UnityBuild/Release（release-正服）", false, -13)]
        public static bool BuildReleaseApk()
        {
            ApplyPlayerSettings(AppBuildType.Release);
            return BuildApk(AppBuildType.Release);
        }

        //    [MenuItem("[FC Release]/MM_UnityBuild/AppBundle", false, -13)]
        //    public static bool BuildAppBundle()
        //    {
        //        return BuildApk(BuildType.AppBundle);
        //    }

        [MenuItem("[FC Release]/MM_UnityBuild/Debug|DebugFormal|Release", false, -13)]
        public static void BuildDebugAndReleaseApk()
        {
            if (!BuildDebugApk())
            {
                return;
            }
            if (!BuildDebugFormalApk())
            {
                return;
            }
            if (!BuildReleaseApk())
            {
                return;
            }
        }

        private static bool BuildApk(AppBuildType type)
        {
#if UNITY_ANDROID
            LogUtil.LogFormat("{0} start build {1} apk {2}", TAG, GetTypeStr(type), GetApkPath(type));
            string aabSaveDir = String.Format(mAndroidModuleAppPath + "build/outputs/bundle/{0}Release/",
                AppFacade_Editor.AppName);

            if (false == Directory.Exists(aabSaveDir))
            {
                //创建文件夹
                Directory.CreateDirectory(aabSaveDir);
            }

            SetKeystorePass();

            SetBuildConfig(type);

            string outputPath = GetApkPath(type);
            var buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, outputPath, BuildTarget.Android, mBuildOptions);
            CleanMarco();

            if (buildReport.summary.result == BuildResult.Succeeded)
            {
                LogUtil.LogFormat("{0} duration: {1}, size: {2}", TAG, buildReport.summary.totalTime, buildReport.summary.totalSize);
                LogUtil.LogFormat("{0} build apk Succeeded", TAG);
                //            if (type == BuildType.AppBundle)
                //            {// 复制aab到android的编译目录方便测试aab安装
                //                outputPath = outputPath.Replace(".apk", ".aab");
                //                string aabSavePath = aabSaveDir + Path.GetFileName(outputPath);
                //                DelectDir(aabSaveDir);
                //                File.Copy(outputPath, aabSavePath, true);
                //            }

                return true;
            }
#else
        LogUtil.LogErrorFormat("切换到Android平台再打包");
#endif

            return false;
        }

        /**
         * 设置编译变量
         */
        private static void ApplyPlayerSettings(AppBuildType type)
        {
            switch (type)
            {
                case AppBuildType.Debug:
                    {
                        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, TEST_PACKAGENAME);
                        AddMarco(UNITY_BUILD_DEBUG);
                        break;
                    }
                case AppBuildType.DebugFormal:
                    {
                        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, TEST_PACKAGENAME);
                        AddMarco(UNITY_BUILD_DEBUGFORMAL);
                        break;
                    }

                case AppBuildType.Release:
                    {
                        AddMarco(UNITY_BUILD_RELEASE);
                        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, AppFacade_Editor.PackageName);
                        break;
                    }
            }
        }

        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        //如果 使用了 streamreader 在删除前 必须先关闭流 ，否则无法删除 sr.close();
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        private static void SetKeystorePass()
        {
            //        PlayerSettings.Android.keystorePass = AppProjectInfo.AppName + "123456";
            //        PlayerSettings.Android.keyaliasPass = AppProjectInfo.AppName + "123456";
            PlayerSettings.Android.keyaliasName = readKeystoreConfig("keyAlias").Trim();
            PlayerSettings.Android.keyaliasPass = readKeystoreConfig("keyPassword").Trim();
            string storeFile = Path.GetFullPath(mAndroidModuleAppPath + readKeystoreConfig("storeFile").Trim());
            PlayerSettings.Android.keystorePass = readKeystoreConfig("storePassword").Trim();
        }

        private static string readKeystoreConfig(string fieldName)
        {
            var keystoreConfigPath = String.Format("{0}config_{1}.gradle", mAndroidModulePath, AppFacade_Editor.AppName);
            using (StreamReader sr = new StreamReader(keystoreConfigPath, Encoding.UTF8))
            {
                string line;

                // 从文件读取并显示行，直到文件的末尾 
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (!line.Contains(":"))
                        continue;
                    string field = line.Substring(0, line.IndexOf(":"));
                    field = field.Trim();
                    if (field == fieldName)
                    {
                        return line.Substring(line.IndexOf("'") + 1, line.LastIndexOf("'") - line.IndexOf("'") - 1);
                    }
                }
            }

            return "";
        }

        private static void SetBuildConfig(AppBuildType type)
        {
            switch (type)
            {
                case AppBuildType.Debug:
                    EditorUserBuildSettings.buildAppBundle = false;
                    SetDebugBuildConfig();
                    break;
                case AppBuildType.Release:
                case AppBuildType.DebugFormal:
                    EditorUserBuildSettings.buildAppBundle = false;
                    SetReleaseBuildConfig();
                    break;
                    //            case BuildType.AppBundle:
                    //                EditorUserBuildSettings.buildAppBundle = true;
                    //                SetReleaseBuildConfig();
                    //                break;
            }
        }

        private static void SetDebugBuildConfig()
        {
            mBuildOptions = BuildOptions.CompressWithLz4 | BuildOptions.Development;

            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);


        }

        private static void SetReleaseBuildConfig()
        {
            mBuildOptions = BuildOptions.None;

            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;


        }

        private static void AddMarco(string marco)
        {
            CleanMarco();
            var marcos = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
            if (!marcos.Contains(marco))
            {
                if (!marcos.Trim().EndsWith(";")) marcos += ";";
                marcos += marco;
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, marcos);
            }
        }

        private static void CleanMarco()
        {
            var marcos = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
            foreach (var marco in UNITY_BUILD_LIST)
            {
                if (marcos.Contains(marco))
                {
                    marcos = marcos.Replace(marco, "");
                    marcos = marcos.Replace(";;", ";");
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, marcos);
                }
            }
        }

        private static string GetApkPath(AppBuildType type)
        {
            var timeStr = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
            return mBuildPath + AppFacade_Editor.AppName + "_v" + PlayerSettings.Android.bundleVersionCode + "_" +
                   PlayerSettings.bundleVersion + "-" + GetTypeStr(type) + "_" + timeStr + ".apk";
        }

        private static string GetTypeStr(AppBuildType type)
        {
            return type.ToString().ToLower();
        }
    }
}