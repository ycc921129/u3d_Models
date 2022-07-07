using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

/// <summary>
/// 适配旧版本Ipa打包接口
/// </summary>
public static class BuildProjectIpaClientTool
{
    public static void CopyToXcode()
    {
        FutureEditor.BuildProjectIpaClientTool.CopyToXcode();
    }
}

namespace FutureEditor
{
    // ios ipa包编译
    public static class BuildProjectIpaClientTool
    {
        private const string TAG = "[iOS Build]";
        private static string XCODE_EXPORT_FOLDER = Path.GetFullPath(
            string.Format(Application.dataPath + "/../../UClientBuild/{0}_xcode_project.ipa/", AppFacade_Editor.AppName));

        /**
         * 真正的编译路径
         */
        private static string XCODE_BUILD_PATH = Path.GetFullPath(Application.dataPath + "../../../../UGameIOS/");

        private static string XCODE_AUTO_BUILD_IOS_PATH = XCODE_BUILD_PATH + "../../_autobuild_ios/";

        [MenuItem("[FC Release]/iOS编译/导出Xcode工程", false, -15)]
        public static void CopyToXcode()
        {
#if UNITY_IOS
            Unity3dScriptTool.SetReleaseDefineSymbol();

            if (!Directory.Exists(XCODE_BUILD_PATH))
            {
                LogUtil.LogErrorFormat(string.Format( "找不到xcode编译目录：{0}", XCODE_BUILD_PATH ));
                return;
            }

            BuildOptions buildOptions = BuildOptions.None;

            var buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, XCODE_EXPORT_FOLDER, BuildTarget.iOS, buildOptions);

            if (buildReport.summary.result != BuildResult.Succeeded)
            {
                ThrowException("[BuildProjectIpaClientTool]Unity编译ipa错误");
                return;
            }

            LogUtil.LogFormat("[BuildProjectIpaClientTool]导出成功");

            // 从导出项目复制三个文件夹到编译项目
            var exportCopyFolders = new []{"Classes", "Data", "Libraries"};
            foreach (var folder in exportCopyFolders)
            {
                var destPath = XCODE_BUILD_PATH + folder;
                if (Directory.Exists(destPath)) FileUtil.DeleteFileOrDirectory(destPath);
                FileUtil.CopyFileOrDirectory(XCODE_EXPORT_FOLDER + "/" + folder, destPath);
            }
            LogUtil.LogFormat("[BuildProjectIpaClientTool]成功复制编译文件");
            LogSuccess(TAG + "成功 导出Xcode工程");
            syncClasses();
#else
            LogUtil.LogError("[BuildProjectIpaClientTool]切换到iOS平台再打包");
#endif
        }

        private static void LogSuccess(string log)
        {
            UnityEngine.Debug.Log("<color=#0000ffff>" + log + "</color>");
        }

        // [MenuItem("[FC Release]/iOS编译/同步Classes文件夹", false, -15)]
        private static void syncClasses()
        {
            // 1
            UnityEngine.Debug.Log(TAG + "开始同步Classes文件夹");
            if (!buildCmd("-r"))
            {
                ThrowException(TAG + "同步Classes文件夹 失败");
                return;
            }
            else
            {
                LogSuccess(TAG + "成功 同步Classes文件夹");
            }

            // 2
            UnityEngine.Debug.Log(TAG + "开始清理");
            if (!buildCmd("-c"))
            {
                ThrowException(TAG + "清理 失败");
                return;
            }
            else
            {
                LogSuccess(TAG + "成功 清理");
            }
        }

        [MenuItem("[FC Release]/iOS编译/测试", false, -15)]
        private static void buildCmdTest()
        {
            buildCmd("-c");
        }

        private static bool buildCmd(string args)
        {
            var exitCode = 0;
            var cmd = XCODE_AUTO_BUILD_IOS_PATH + "build.py " + args + " -p" + XCODE_BUILD_PATH;
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.ErrorDialog = true;

            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.StartInfo.FileName = "python3";
            process.StartInfo.Arguments = cmd; //string.Format("{0}/{1}", XCODE_AUTO_BUILD_IOS_PATH, cmd);

            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            process.WaitForExit();
            exitCode = process.ExitCode;
            process.Close();

            if (exitCode == 0)
            {
                // 成功
                string logInfo = string.Format("[BuildProjectApkClientTool]Assemble {0} Succeed with code {1}", cmd, exitCode);
                UnityEngine.Debug.LogFormat("<color=green>{0}</color>\n ", logInfo);
                return true;
            }
            else
            {
                // 失败
                UnityEngine.Debug.LogErrorFormat("[BuildProjectApkClientTool]Assemble {0} Failed with code {1}", cmd, exitCode);
                UnityEngine.Debug.LogError(error);
                UnityEngine.Debug.Log(output);
                return false;
            }
            // process.
        }

        private static void ThrowException(string errorMsg)
        {
            UnityEngine.Debug.LogError(errorMsg);
            if (!Application.isBatchMode)
            {
                return;
            }
            throw new Exception(errorMsg);
        }

        //    [MenuItem("[FC Release]/iOS编译/编译", false, -15)]
        //    public static void BuildXcode()
        //    {
        //        Process process = new Process();
        //        process.StartInfo.UseShellExecute = true;
        //        process.StartInfo.CreateNoWindow = false;
        //        process.StartInfo.ErrorDialog = true;

        //        process.StartInfo.WorkingDirectory = XCODE_AUTO_BUILD_IOS_PATH;
        //#if UNITY_EDITOR_WIN
        //        //process.StartInfo.FileName = GetBatFile();
        //        //process.StartInfo.Arguments = cmd;
        //#elif UNITY_EDITOR_OSX
        //                 process.StartInfo.FileName = "/bin/bash";
        //                 process.StartInfo.Arguments = string.Format("touch {0}testtttt", XCODE_AUTO_BUILD_IOS_PATH);
        //#endif

        //        process.Start();
        //        process.WaitForExit();
        //        process.Close();
        //    }
    }
}