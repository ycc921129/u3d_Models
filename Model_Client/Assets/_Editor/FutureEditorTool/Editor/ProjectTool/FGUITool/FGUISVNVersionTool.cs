using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class FGUISVNVersionTool
    {
        [MenuItem("[FC Project]/FGUI/SVNVersion/1) 打开界面目录", false, 0)]
        public static void OpenFolder()
        {
            if (Directory.Exists(AppEditorInfo.UISVNPath))
            {
                DirectoryTool.OpenDirectory(AppEditorInfo.UISVNPath);
            }
            else
            {
                DirectoryTool.OpenDirectory(Application.dataPath + @"\..\..\");
                UnityEngine.Debug.Log("[FGUISVNVersionTool]没有找到界面目录");
            }
        }

        [MenuItem("[FC Project]/FGUI/SVNVersion/2) 更新SVN界面", false, 1)]
        public static void UpdateSVN()
        {
            string uiAssetsPathFromSVNPath = AppEditorInfo.UISVNPath + @"assets\";
            if (!Directory.Exists(uiAssetsPathFromSVNPath))
            {
                UnityEngine.Debug.Log("[FGUISVNVersionTool]更新SVN界面失败, 没有找到SVN界面目录。");
                return;
            }

            Process process = Process.Start("TortoiseProc.exe", "/command:update /path:" + uiAssetsPathFromSVNPath + " /closeonend:0");
            process.WaitForExit();
            process.Close();

            UnityEngine.Debug.Log("[FGUISVNVersionTool]更新SVN界面");
        }

        [MenuItem("[FC Project]/FGUI/SVNVersion/3) 提交SVN界面", false, 2)]
        public static void CommitSVN()
        {
            string uiAssetsPathFromSVNPath = AppEditorInfo.UISVNPath + @"assets\";
            if (!Directory.Exists(uiAssetsPathFromSVNPath))
            {
                UnityEngine.Debug.Log("[FGUISVNVersionTool]提交SVN界面失败, 没有找到SVN界面目录。");
                return;
            }

            Process process = Process.Start("TortoiseProc.exe", "/command:commit /path:" + uiAssetsPathFromSVNPath + " /closeonend:0");
            process.WaitForExit();
            process.Close();

            UnityEngine.Debug.Log("[FGUISVNVersionTool]提交SVN界面");
        }
    }
}