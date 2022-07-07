using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class ProjectDirectoryTool
    {
        [MenuItem(EditorAppConst.MenuItemAppName + "/ProjectDirectory/ProjectApp/AutoCreator", false, 0)]
        private static void ProjectApp_AutoCreator()
        {
            SelectOpenDirectory("Assets/_App/AutoCreator");
        }

        [MenuItem(EditorAppConst.MenuItemAppName + "/ProjectDirectory/ProjectApp/Manager", false, 0)]
        private static void ProjectApp_Manager()
        {
            SelectOpenDirectory("Assets/_App/ProjectApp/Manager");
        }

        [MenuItem(EditorAppConst.MenuItemAppName + "/ProjectDirectory/ProjectApp/Module", false, 0)]
        private static void ProjectApp_Logic()
        {
            SelectOpenDirectory("Assets/_App/ProjectApp/Logic/Module");
        }

        [MenuItem(EditorAppConst.MenuItemAppName + "/ProjectDirectory/_Tool(外部工具目录)", false, 0)]
        private static void ProjectApp__Tool_WB()
        {
            string path = AppEditorInfo.ProjectFolder + "/_Tool/";
            path = Path.GetFullPath(path);
            Process process = Process.Start(path);
            process.WaitForExit();
            process.Close();
            UnityEngine.Debug.Log("[ProjectDirectoryTool]" + path);
        }

        private static void SelectOpenDirectory(string path)
        {
            Object obj = AssetDatabase.LoadMainAssetAtPath(path);
            if (obj == null) return;

            Selection.activeObject = null;
            Selection.activeObject = obj;
            EditorGUIUtility.PingObject(Selection.activeObject);

            //string fullPath = Path.GetFullPath(path);
            //Process process = Process.Start(fullPath);
            //process.WaitForExit();
            //process.Close();

            UnityEngine.Debug.Log("[ProjectDirectoryTool]" + path);
        }
    }
}