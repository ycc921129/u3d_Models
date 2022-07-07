using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class FGUIContollerCreateTool_v2
    {
        private static string ToolDir = Path.GetFullPath(Application.dataPath + @"\..\..\_Tool\FGUICtrlConstTool\");
        private static string ToolExe = "FGUICtrlConstTool.exe";
        private static string FguiFromSVNPath = Path.GetFullPath(AppEditorInfo.UISVNPath);
        private static string Unity3dPath = Path.GetFullPath(Application.dataPath + "/../");

        private static string Cmd_All = "All";
        private static string Cmd_Common = "Common";
        private static string Cmd_Project = "Project";

        [MenuItem("[FC Project]/FGUI/ContollerCreate/1) 生成所有界面控制器常量", false, 0)]
        public static void CreateAllControllerScripts()
        {
            CreateControllerScripts(Cmd_All);
        }

        [MenuItem("[FC Project]/FGUI/ContollerCreate/2) 生成通用界面控制器常量", false, 1)]
        public static void CreateController_ScriptsCommon()
        {
            CreateControllerScripts(Cmd_Common);
        }

        [MenuItem("[FC Project]/FGUI/ContollerCreate/3) 生成项目界面控制器常量", false, 2)]
        public static void CreateController_ScriptsProject()
        {
            CreateControllerScripts(Cmd_Project);
        }

        public static void OpenTool()
        {
            Process process = Process.Start(ToolDir + ToolExe);
            process.Close();
            process.Dispose();
            UnityEngine.Debug.Log("[FGUIContollerCreateTool]OpenTool");
        }

        private static void CreateControllerScripts(string mode)
        {
            Process process = Process.Start(ToolDir + ToolExe, FguiFromSVNPath + " " + Unity3dPath + " " + mode);
            process.WaitForExit();
            process.Close();
            process.Dispose();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            UnityEngine.Debug.Log("[FGUIContollerCreateTool]CreateControllerScripts: " + mode);
        }
    }
}