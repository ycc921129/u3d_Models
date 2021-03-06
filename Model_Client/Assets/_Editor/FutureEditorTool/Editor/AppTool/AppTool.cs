using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FutureEditor
{
    public static class AppTool
    {
        [MenuItem(EditorAppConst.MenuSubItemAppName, false, -1001)]
        private static void AppName()
        {
            OpenProjectFolder();
            UnityEngine.Debug.Log("[AppTool]appPath: " + AppEditorInfo.ProjectFolder);
        }

        [MenuItem(EditorAppConst.MenuSubItemAppDesc, false, -1000)]
        private static void AppDesc()
        {
            AppName();
        }

        [MenuItem(EditorAppConst.MenuItemAppName + "/刷新工程", false, -2)]
        public static void RefreshProject()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            Resources.UnloadUnusedAssets();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            Resources.UnloadUnusedAssets();

            UnityEngine.Debug.Log("<color=green>[AppTool]刷新工程完成</color>");
        }

        [MenuItem(EditorAppConst.MenuSubItemOpenAppCodeVSIDE, false, -1)]
        private static void OpenAppCodeVSIDE()
        {
            OpenCodeVSProject();
        }

        public static void OpenProjectFolder()
        {
            string appPath = AppEditorInfo.ProjectFolder;
            appPath = Path.GetFullPath(appPath);
            Process process = Process.Start(appPath);
            process.WaitForExit();
            process.Close();
        }

        public static void OpenProjectSettings()
        {
            // 打表配置
            string configPath = "../_Tool/ExcelTool/Setting/Setting.json";
            configPath = Path.GetFullPath(configPath);
            if (File.Exists(configPath))
            {
                Process.Start(configPath);
            }
            // 项目配置
            string projectPath = "Assets/_App/ProjectApp/App/AppFacade.cs";
            Object asset = AssetDatabase.LoadAssetAtPath(projectPath, typeof(TextAsset));
            if (asset != null)
            {
                AssetDatabase.OpenAsset(asset as TextAsset);
            }
        }

        private static void OpenCodeVSProject()
        {
            // 项目代码工程
            //string projectPath = "Assets/_App/ProjectApp/App/AppFacade.cs";
            //Object asset = AssetDatabase.LoadAssetAtPath(projectPath, typeof(TextAsset));
            //if (asset != null)
            //{
            //    AssetDatabase.OpenAsset(asset as TextAsset);
            //}

            // 反射调用引擎内部方法
            Assembly asm = Assembly.GetAssembly(typeof(Editor));
            Type type = asm.GetType("UnityEditor.CodeEditorProjectSync");
            if (type == null) return;
            MethodInfo func = type.GetMethod("SyncAndOpenSolution", BindingFlags.Static | BindingFlags.NonPublic);
            func.Invoke(null, null);
        }

        public static void OpenSVNFolder()
        {
            string svnPath = AppEditorInfo.SVNPath;
            Process process = Process.Start(svnPath);
            process.WaitForExit();
            process.Close();
            UnityEngine.Debug.Log("[AppTool]svnPath: " + svnPath);
        }

        public static void SetApplicationIdentifier()
        {
            // 设置项目唯一标识
            PlayerSettings.companyName = "games" + AppFacade_Editor.AppName;
            PlayerSettings.productName = AppFacade_Editor.AppName;
            string applicationIdentifier = string.Format("com.{0}.{1}{2}", PlayerSettings.companyName, PlayerSettings.productName, EditorUserBuildSettings.activeBuildTarget.ToString());
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, applicationIdentifier);
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, applicationIdentifier);
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, applicationIdentifier);
            PlayerSettings.allowUnsafeCode = true;
            //PlayerSettings.enableFrameTimingStats = true;
        }

        public static BuildTarget GetEditorPlatform()
        {
            return EditorUserBuildSettings.activeBuildTarget;
        }

        public static Process[] GetProcessesByName(string processName)
        {
            return Process.GetProcessesByName(processName);
        }

        public static bool IsDebugBuild()
        {
            return UnityEngine.Debug.isDebugBuild;
        }
    }
}