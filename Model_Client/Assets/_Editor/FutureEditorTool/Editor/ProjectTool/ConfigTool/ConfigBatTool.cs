using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class ConfigBatTool
    {
        private static string ToolDir = Path.GetFullPath(Application.dataPath + @"\..\..\_Tool\ExcelTool\");
        private static string ExcelFromSVNPath = AppEditorInfo.ExcelSVNPath;

        private static string Cmd_Test = "1.配置表生成同步-测试服{0}.bat";
        private static string Cmd_Release = "2.配置表生成同步-正式服{0}.bat";

        [MenuItem("[FC Project]/Config/1) 生成打表工具Setting.json", false, 0)]
        public static void GenerateConfigSetting()
        {
            ConfigSettingTool.Generate();
            // 打开配置
            string configSettingPath = "../_Tool/ExcelTool/Setting/Setting.json";
            configSettingPath = Path.GetFullPath(configSettingPath);
            if (File.Exists(configSettingPath))
            {
                Process.Start(configSettingPath);
            }
        }

        [MenuItem("[FC Project]/Config/2) 打开配置表目录", false, 1)]
        public static void OpenConfigDir()
        {
            string existsExcelProjectToPath = GetExistsExcelProjectToPath();
            if (Directory.Exists(ExcelFromSVNPath))
            {
                DirectoryTool.OpenDirectory(ExcelFromSVNPath);
            }
            else if (Directory.Exists(existsExcelProjectToPath))
            {
                DirectoryTool.OpenDirectory(existsExcelProjectToPath);
            }
            else
            {
                DirectoryTool.OpenDirectory(Application.dataPath + @"\..\..\");
                UnityEngine.Debug.Log("[ConfigBatTool]没有找到配置表目录");
            }
        }

        [MenuItem("[FC Project]/Config/3) 更新SVN配置表", false, 2)]
        public static void UpdateSVNConfig()
        {
            if (!Directory.Exists(ExcelFromSVNPath))
            {
                UnityEngine.Debug.Log("[ConfigBatTool]更新SVN配置表失败, 没有找到SVN配置目录");
                return;
            }

            Process process = Process.Start("TortoiseProc.exe", "/command:update /path:" + ExcelFromSVNPath + " /closeonend:0");
            process.WaitForExit();
            process.Close();
            process.Dispose();

            UnityEngine.Debug.Log("[ConfigBatTool]更新SVN配置表");
        }

        [MenuItem("[FC Project]/Config/4) 提交SVN配置表", false, 3)]
        public static void CommitSVNConfig()
        {
            if (!Directory.Exists(ExcelFromSVNPath))
            {
                UnityEngine.Debug.Log("[ConfigBatTool]提交SVN配置表失败, 没有找到SVN配置目录");
                return;
            }

            Process process = Process.Start("TortoiseProc.exe", "/command:commit /path:" + ExcelFromSVNPath + " /closeonend:0");
            process.WaitForExit();
            process.Close();
            process.Dispose();

            UnityEngine.Debug.Log("[ConfigBatTool]提交SVN配置表");
        }

        [MenuItem("[FC Project]/Config/5) 同步Project配置表", false, 4)]
        public static void SyncProjectConfigFromSVN()
        {
            string excelFromSVNPath = ExcelFromSVNPath;
            if (!Directory.Exists(excelFromSVNPath))
            {
                UnityEngine.Debug.Log("[ConfigBatTool]同步Project配置表失败, 没有找到SVN配置目录");
                return;
            }

            string excelProjectToPath = GetSyncExcelProjectToPath();
            if (Directory.Exists(excelProjectToPath))
            {
                Directory.Delete(excelProjectToPath, true);
            }

            FileTool.ConditionCopyDirectoryFile(excelFromSVNPath, excelProjectToPath, (path) =>
            {
                string fileName = Path.GetFileName(path);
                return !fileName.StartsWith("~$");
            });

            UnityEngine.Debug.Log("[ConfigBatTool]同步Project配置表完成");
        }

        [MenuItem("[FC Project]/Config/6) 测试服打表", false, 5)]
        public static void DoTestConfigBat()
        {
            string appName = EditorAppConst.AppName;
            string cmd = string.Format(Cmd_Test, "_" + appName);
            string cmdFile = Path.Combine(ToolDir, cmd);
            if (!File.Exists(cmdFile))
            {
                cmd = string.Format(Cmd_Test, string.Empty);
                cmdFile = Path.Combine(ToolDir, cmd);
            }

            // 动态参数传入项目代号
            Process process = Process.Start(cmdFile, appName);
            process.WaitForExit();
            process.Close();
            process.Dispose();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            UnityEngine.Debug.Log("[ConfigBatTool]测试服打表完成");
        }

        [MenuItem("[FC Project]/Config/7) 正式服打表", false, 8)]
        public static void DoReleaseConfigBat()
        {
            string appName = EditorAppConst.AppName;
            string cmd = string.Format(Cmd_Release, "_" + appName);
            string cmdFile = Path.Combine(ToolDir, cmd);
            if (!File.Exists(cmdFile))
            {
                cmd = string.Format(Cmd_Release, string.Empty);
                cmdFile = Path.Combine(ToolDir, cmd);
            }

            // 动态参数传入项目代号
            Process process = Process.Start(cmdFile, appName);
            process.WaitForExit();
            process.Close();
            process.Dispose();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            UnityEngine.Debug.Log("[ConfigBatTool]正式服打表完成");
        }

        [MenuItem("[FC Project]/Config/8) 自动化测试服打表", false, 9)]
        public static void SyncConfigDoTestConfig()
        {
            UpdateSVNConfig();
            CommitSVNConfig();
            SyncProjectConfigFromSVN();
            DoTestConfigBat();
        }

        [MenuItem("[FC Project]/Config/9) 自动化正式服打表", false, 10)]
        public static void SyncConfigDoReleaseConfig()
        {
            UpdateSVNConfig();
            CommitSVNConfig();
            SyncProjectConfigFromSVN();
            DoReleaseConfigBat();
        }

        [MenuItem("[FC Project]/Config/10) 自动化测试服打表_不更新SVN", false, 11)]
        public static void SyncConfigDoTestConfig_NoUpdateSVN()
        {
            SyncProjectConfigFromSVN();
            DoTestConfigBat();
        }

        [MenuItem("[FC Project]/Config/11) 自动化正式服打表_不更新SVN", false, 12)]
        public static void SyncConfigDoReleaseConfig_NoUpdateSVN()
        {
            SyncProjectConfigFromSVN();
            DoReleaseConfigBat();
        }

        private static string GetSyncExcelProjectToPath()
        {
            string excelProjectToPath = Path.GetFullPath(Application.dataPath + @"\..\..\_Config_" + EditorAppConst.AppName + @"\_Excel\");
            return excelProjectToPath;
        }

        private static string GetExistsExcelProjectToPath()
        {
            string excelProjectToPath = GetSyncExcelProjectToPath();
            if (!Directory.Exists(excelProjectToPath))
            {
                excelProjectToPath = Path.GetFullPath(Application.dataPath + @"\..\..\_Config\_Excel\");
            }
            return excelProjectToPath;
        }
    }
}