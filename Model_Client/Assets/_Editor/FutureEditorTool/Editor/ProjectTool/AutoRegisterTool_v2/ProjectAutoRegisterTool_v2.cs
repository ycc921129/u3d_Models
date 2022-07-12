using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public enum VisualizationType : int
    {
        MVC = 0,
        Scene = 1,
        Config = 2,
        GameData = 3,
        Preferences = 4,
        Dispose = 5,
    }

    public static class ProjectAutoRegisterTool_v2
    {
        private const string AutoCreatorComments =
@"/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

";

        private static string TableStr = "    ";
        private static string ThreeTab = TableStr + TableStr + TableStr;
        private static string ResUIDir = EditorPathConst.ResResourcesPath + "UI";

        #region All Auto String Info
        private static string Auto_SceneMgr;
        private static string Auto_ConfigConst;
        private static string Auto_ConfigMgr;
        private static string Auto_UIMgr;
        private static string Auto_ModelConst;
        private static string Auto_UIConst;
        private static string Auto_CtrlConst;
        private static string Auto_UICtrlConst;
        private static string Auto_ModuleMgr_Model;
        private static string Auto_ModuleMgr_UIType;
        private static string Auto_ModuleMgr_Ctrl;
        private static string Auto_ModuleMgr_UICtrl;
        #endregion

        [MenuItem("[FC Project]/AutoRegister/自动注册项目", false, -3)]
        public static void AutoRegisterAll()
        {
            Debug.Log("[ProjectAutoRegisterTool]自动注册项目开始");

            Debug.Log("------------------------------------------自注册开始----------------------------------------------------------------");
            RegisterAutoAssetInAllDirectory(true);
            
            PreferencesMgrAutoRegisterTool_v2.AutoRegisterPreferences();
            Debug.Log("------------------------------------------自注册完毕----------------------------------------------------------------");

            Debug.Log("------------------------------------------检查程序正确性开始----------------------------------------------------------------");
            CheckDispatcherMatchMsgTool.CheckDispatcherMatchMsg();
            CheckProgramRuntimeTool.DoCheck();

            RegisterSettingsAssetTool.SaveAutoRegisterSetting();
            RegisterSettingsAssetTool.SaveAutoRegisterVisualizationData();
            Debug.Log("------------------------------------------检查程序正确性完毕----------------------------------------------------------------");

            Debug.Log("------------------------------------------配置项目设置开始----------------------------------------------------------------");
            ProjectSettings_TagLayerTool.Set();
            Debug.Log("------------------------------------------配置项目设置完毕----------------------------------------------------------------");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("<color=green>[ProjectAutoRegisterTool]自动注册项目完成</color>");
        }

        [MenuItem("[FC Project]/AutoRegister/自动注销项目", false, -2)]
        public static void AutoCancelAll()
        {
            RegisterAutoAssetInAllDirectory(false);

            RegisterSettingsAssetTool.SaveAutoRegisterSetting();
            RegisterSettingsAssetTool.SaveAutoRegisterVisualizationData();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("[ProjectAutoRegisterTool]自动注销项目完成");
        }

        private static void RegisterAutoAssetInAllDirectory(bool isRegister)
        {
            InitAllAutoInfo();
            if (isRegister)
            {
                AutoRegisterSettings asset = RegisterSettingsAssetTool.GetAutoRegisterSetting();
                for (int i = 0; i < asset.autoRegisterPath.Count; i++)
                {
                    string path = AssetDatabase.GetAssetPath(asset.autoRegisterPath[i]);
                    AutoRegisterSceneMgr(path);
                    AutoCreateConfigConst(path);
                    AutoRegisterConfigMgr(path);
                    AutoRegisterUIMgr(path);
                    AutoCreateModelConst(path);
                    AutoCreateUIConst(path);
                    AutoCreateCtrlConst(path);
                    AutoCreateUICtrlConst(path);
                    AutoRegisterModuleMgr(path);
                }
                // Create 在注册的时候执行
                FinishCreateConfigConst();
                FinishCreateModelConst();
                FinishCreateUIConst();
                FinishCreateCtrlConst();
                FinishCreateUICtrlConst();
            }

            // Register 在任何时候都执行
            FinishRegisterSceneMgr();
            FinishRegisterConfigMgr();
            FinishRegisterUIMgr();
            FinishRegisterModuleMgr();
        }

        private static void InitAllAutoInfo()
        {
            Auto_SceneMgr = string.Empty;
            Auto_ConfigConst = string.Empty;
            Auto_ConfigMgr = string.Empty;
            Auto_UIMgr = string.Empty;
            Auto_ModelConst = string.Empty;
            Auto_UIConst = string.Empty;
            Auto_CtrlConst = string.Empty;
            Auto_UICtrlConst = string.Empty;
            Auto_ModuleMgr_Model = string.Empty;
            Auto_ModuleMgr_UIType = string.Empty;
            Auto_ModuleMgr_Ctrl = string.Empty;
            Auto_ModuleMgr_UICtrl = string.Empty;

            AutoRegisterVisualizationInfo(null, VisualizationType.Dispose);
        }

        public static void AutoRegisterVisualizationInfo(string path, VisualizationType type)
        {
            AutoRegisterVisualizationData visualizationAsset = RegisterSettingsAssetTool.GetAutoRegisterVisualizationData();
            if (path != null)
            {
                path = PathTool.GetRawPath(PathTool.FilePathToAssetPath(path));
            }
            switch (type)
            {
                case VisualizationType.MVC:
                    DefaultAsset defaultAsset = AssetDatabase.LoadAssetAtPath<DefaultAsset>(path);
                    if (!visualizationAsset.module.Contains(defaultAsset))
                    {
                        visualizationAsset.module.Add(defaultAsset);
                    }
                    break;
                case VisualizationType.Scene:
                    TextAsset scene = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                    if (!visualizationAsset.scene.Contains(scene))
                    {
                        visualizationAsset.scene.Add(scene);
                    }
                    break;
                case VisualizationType.Config:
                    TextAsset config = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                    if (!visualizationAsset.config.Contains(config))
                    {
                        visualizationAsset.config.Add(config);
                    }
                    break;
                case VisualizationType.GameData:
                    TextAsset gameData = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                    if (!visualizationAsset.gameData.Contains(gameData))
                    {
                        visualizationAsset.gameData.Add(gameData);
                    }
                    break;
                case VisualizationType.Preferences:
                    TextAsset preference = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                    if (!visualizationAsset.preferences.Contains(preference))
                    {
                        visualizationAsset.preferences.Add(preference);
                    }
                    break;
                case VisualizationType.Dispose:
                    visualizationAsset.module.Clear();
                    visualizationAsset.scene.Clear();
                    visualizationAsset.config.Clear();
                    visualizationAsset.gameData.Clear();
                    visualizationAsset.preferences.Clear();
                    break;
            }
        }

        private static void AutoRegisterSceneMgr(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (IOTool.IsHidden(directory)) return;
                if (directory.GetDirectories().Length != 0)
                {
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < directories.Length; i++)
                    {
                        AutoRegisterSceneMgr(directories[i].FullName);
                    }
                }

                // 处理自动注册的逻辑
                FileInfo[] files = directory.GetFiles();
                for (int j = 0; j < files.Length; j++)
                {
                    if (!files[j].Name.EndsWith(".cs")) continue;
                    if (IOTool.IsHidden(files[j])) continue;

                    string Name = files[j].Name.Split('.')[0];
                    if (Name.EndsWith("Scene"))
                    {
                        Auto_SceneMgr += ThreeTab + "sceneMgr.AddScene(new " + Name + "());\r\n";
                        AutoRegisterVisualizationInfo(files[j].FullName, VisualizationType.Scene);
                    }
                }
            }
        }

        private static void FinishRegisterSceneMgr()
        {
            if (Auto_SceneMgr.Length > 0)
            {
                Auto_SceneMgr = Auto_SceneMgr.Substring(0, Auto_SceneMgr.Length - 2);
            }

            string classStr = AutoCreatorComments +
@"using FutureCore;

namespace ProjectApp
{
    public static class SceneMgrRegister
    {
        public static void AutoRegisterScene()
        {
            SceneMgr sceneMgr = SceneMgr.Instance;
//ReplaceScene
        }
    }
}";
            string replaceConfig = "//ReplaceScene";
            classStr = classStr.Replace(replaceConfig, Auto_SceneMgr);
            string targetPath = Application.dataPath + "/_App/AutoCreator/AutoRegister/SceneMgr/SceneMgrRegister_AutoCreator.cs";
            WriteFile(targetPath, classStr);
            Debug.Log("[ProjectAutoRegisterTool]处理SceneMgr完成:" + targetPath);
            AssetDatabase.Refresh();
        }

        private static void AutoCreateConfigConst(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (IOTool.IsHidden(directory)) return;
                if (directory.GetDirectories().Length != 0)
                {
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < directories.Length; i++)
                    {
                        AutoCreateConfigConst(directories[i].FullName);
                    }
                }

                // 处理自动注册的逻辑
                FileInfo[] files = directory.GetFiles();
                for (int j = 0; j < files.Length; j++)
                {
                    if (!files[j].Name.EndsWith(".cs")) continue;
                    if (IOTool.IsHidden(files[j])) continue;

                    string Name = files[j].Name.Split('.')[0];
                    if (Name.EndsWith("VOModel_AutoCreator"))
                    {
                        string name = Name.Replace("VOModel_AutoCreator", "");
                        Auto_ConfigConst += TableStr + TableStr + "public const string " + name + " = \"" + name + "\";\r\n";
                    }
                    if (Name.EndsWith("VO_AutoCreator"))
                    {
                        AutoRegisterVisualizationInfo(files[j].FullName, VisualizationType.Config);
                    }
                }
            }
        }

        private static void FinishCreateConfigConst()
        {
            if (Auto_ConfigConst.Length > 0)
            {
                Auto_ConfigConst = Auto_ConfigConst.Substring(0, Auto_ConfigConst.Length - 2);
            }

            string configConstClassStr = AutoCreatorComments +
@"namespace ProjectApp
{
    public static class ConfigConst
    {
//ReplaceConfigConst
    }
}";

            string replaceModelConst = "//ReplaceConfigConst";
            configConstClassStr = configConstClassStr.Replace(replaceModelConst, Auto_ConfigConst);

            string targetPath = Application.dataPath + "/_App/AutoCreator/AutoRegister/ConfigMgr/ConfigConst_AutoCreator.cs";
            WriteFile(targetPath, configConstClassStr);
            Debug.Log("[ProjectAutoRegisterTool]处理Config常量完成:" + targetPath);
        }

        private static void AutoRegisterConfigMgr(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (IOTool.IsHidden(directory)) return;
                if (directory.GetDirectories().Length != 0)
                {
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < directories.Length; i++)
                    {
                        AutoRegisterConfigMgr(directories[i].FullName);
                    }
                }

                // 处理自动注册的逻辑
                FileInfo[] files = directory.GetFiles();
                for (int j = 0; j < files.Length; j++)
                {
                    if (!files[j].Name.EndsWith(".cs")) continue;
                    if (IOTool.IsHidden(files[j])) continue;

                    string Name = files[j].Name.Split('.')[0];
                    if (Name.EndsWith("VOModel_AutoCreator"))
                    {
                        Auto_ConfigMgr += ThreeTab + "configMgr.AddConfigVOModel(ConfigConst."
                            + Name.Replace("VOModel_AutoCreator", "") + ", " + Name.Replace("_AutoCreator", "") + ".Instance);\r\n";
                    }
                    if (Name.EndsWith("VO_AutoCreator"))
                    {
                        AutoRegisterVisualizationInfo(files[j].FullName, VisualizationType.Config);
                    }
                }
            }
        }

        private static void FinishRegisterConfigMgr()
        {
            if (Auto_ConfigMgr.Length > 0)
            {
                Auto_ConfigMgr = Auto_ConfigMgr.Substring(0, Auto_ConfigMgr.Length - 2);
            }

            string classStr = AutoCreatorComments +
@"using FutureCore;
using ProjectApp.Data;

namespace ProjectApp
{
    public static class ConfigMgrRegister
    {
        public static void AutoRegisterConfig()
        {
            ConfigMgr configMgr = ConfigMgr.Instance;
//ReplaceConfig
        }
    }
}";
            string replaceConfig = "//ReplaceConfig";
            classStr = classStr.Replace(replaceConfig, Auto_ConfigMgr);

            string targetPath = Application.dataPath + "/_App/AutoCreator/AutoRegister/ConfigMgr/ConfigMgrRegister_AutoCreator.cs";
            WriteFile(targetPath, classStr);
            Debug.Log("[ProjectAutoRegisterTool]处理ConfigMgr完成:" + targetPath);
        }

        private static void AutoRegisterUIMgr(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (IOTool.IsHidden(directory)) return;
                if (directory.GetDirectories().Length != 0)
                {
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < directories.Length; i++)
                    {
                        AutoRegisterUIMgr(directories[i].FullName);
                    }
                }

                // 处理自动注册的逻辑
                FileInfo[] files = directory.GetFiles();
                for (int j = 0; j < files.Length; j++)
                {
                    FileInfo file = files[j];
                    if (!file.Name.EndsWith(".cs")) continue;
                    if (IOTool.IsHidden(file)) continue;

                    string dirName = file.DirectoryName;
                    string Name = file.Name.Split('.')[0];
                    if (dirName.Contains("AutoCreator") && dirName.Contains("FGUI_") && Name.EndsWith("Binder"))
                    {
                        Auto_UIMgr += ThreeTab + "UI." + Name.Replace("Binder", "") + "." + Name + ".BindAll();" + "\r\n";
                    }
                }
            }
        }

        private static void FinishRegisterUIMgr()
        {
            string uiCommonPackageInfo = string.Empty;
            if (Auto_UIMgr.Length > 0)
            {
                Auto_UIMgr = Auto_UIMgr.Substring(0, Auto_UIMgr.Length - 2);

                string fguiPackage = "_fui.bytes";
                DirectoryInfo uiDirInfo = new DirectoryInfo(ResUIDir);
                FileInfo[] files = uiDirInfo.GetFiles();
                foreach (FileInfo file in files)
                {
                    if (file.Name.EndsWith(fguiPackage))
                    {
                        if (file.Name.StartsWith("A") || file.Name.StartsWith("Font_"))
                        {
                            string packageName = "\"" + file.Name.Replace(fguiPackage, string.Empty) + "\"";
                            string row = ThreeTab + "commonPackages.Add(" + packageName + ");" + "\r\n";
                            uiCommonPackageInfo += row;
                        }
                    }
                }
                if (uiCommonPackageInfo.Length > 0)
                {
                    uiCommonPackageInfo = uiCommonPackageInfo.Substring(0, uiCommonPackageInfo.Length - 2);
                }
            }

            string classStr = AutoCreatorComments +
@"using System.Collections.Generic;
using UnityEngine;
using FutureCore;
using FairyGUI;

namespace ProjectApp
{
    public static class UIMgrRegister
    {
        public static void AutoRegisterBinder()
        {
//ReplaceBinder
        }

        public static void AutoRegisterCommonPackages()
        {
            List<string> commonPackages = new List<string>();

//ReplaceCommonPackage

            UIMgr.Instance.RegisterCommonPackages(commonPackages);
        }
    }
}";

            string replaceBinderConst = "//ReplaceBinder";
            classStr = classStr.Replace(replaceBinderConst, Auto_UIMgr);
            string replaceCommonPackageConst = "//ReplaceCommonPackage";
            classStr = classStr.Replace(replaceCommonPackageConst, uiCommonPackageInfo);

            string targetPath = Application.dataPath + "/_App/AutoCreator/AutoRegister/UIMgr/UIMgrRegister_AutoCreator.cs";
            WriteFile(targetPath, classStr);
            Debug.Log("[ProjectAutoRegisterTool]处理UIMgr完成:" + targetPath);
        }

        private static void AutoCreateModelConst(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.GetDirectories().Length != 0)
                {
                    DirectoryInfo[] directories = directory.GetDirectories();
                    if (IOTool.IsHidden(directory)) return;
                    for (int i = 0; i < directories.Length; i++)
                    {
                        AutoCreateModelConst(directories[i].FullName);
                    }
                }

                // 处理自动注册的逻辑
                FileInfo[] files = directory.GetFiles();
                bool haveMVC = false;
                for (int j = 0; j < files.Length; j++)
                {
                    if (!files[j].Name.EndsWith(".cs")) continue;
                    if (IOTool.IsHidden(files[j])) continue;

                    string Name = files[j].Name.Split('.')[0];
                    if (!Name.EndsWith("VOModel") && Name.EndsWith("Model"))
                    {
                        Auto_ModelConst += TableStr + TableStr + "public const string " + Name + " = \"" + Name + "\";\r\n";
                        haveMVC = true;
                    }
                }
                if (haveMVC)
                    AutoRegisterVisualizationInfo(path, VisualizationType.MVC);
            }
        }

        private static void FinishCreateModelConst()
        {
            if (Auto_ModelConst.Length > 0)
            {
                Auto_ModelConst = Auto_ModelConst.Substring(0, Auto_ModelConst.Length - 2);
            }

            string modelConstClassStr = AutoCreatorComments +
@"namespace ProjectApp
{
    public static class ModelConst
    {
//ReplaceModelConst
    }
}";

            string replaceModelConst = "//ReplaceModelConst";
            modelConstClassStr = modelConstClassStr.Replace(replaceModelConst, Auto_ModelConst);

            string targetPath = Application.dataPath + "/_App/AutoCreator/AutoRegister/ModuleMgr/ModelConst_AutoCreator.cs";
            WriteFile(targetPath, modelConstClassStr);
            Debug.Log("[ProjectAutoRegisterTool]处理Model常量完成:" + targetPath);
        }

        private static void AutoCreateUIConst(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (IOTool.IsHidden(directory)) return;
                if (directory.GetDirectories().Length != 0)
                {
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < directories.Length; i++)
                    {
                        AutoCreateUIConst(directories[i].FullName);
                    }
                }

                // 处理自动注册的逻辑
                FileInfo[] files = directory.GetFiles();
                bool haveMVC = false;
                for (int j = 0; j < files.Length; j++)
                {
                    if (!files[j].Name.EndsWith(".cs")) continue;
                    if (IOTool.IsHidden(files[j])) continue;

                    string Name = files[j].Name.Split('.')[0];
                    if (Name.EndsWith("UI"))
                    {
                        Auto_UIConst += TableStr + TableStr + "public const string " + Name + " = \"" + Name + "\";\r\n";
                        haveMVC = true;
                    }
                }

                if (haveMVC)
                    AutoRegisterVisualizationInfo(path, VisualizationType.MVC);
            }
        }

        private static void FinishCreateUIConst()
        {
            if (Auto_UIConst.Length > 0)
            {
                Auto_UIConst = Auto_UIConst.Substring(0, Auto_UIConst.Length - 2);
            }

            string uiConstClassStr = AutoCreatorComments +
@"namespace ProjectApp
{
    public static class UIConst
    {
//ReplaceModelConst
    }
}";

            string replaceModelConst = "//ReplaceModelConst";
            uiConstClassStr = uiConstClassStr.Replace(replaceModelConst, Auto_UIConst);

            string targetPath = Application.dataPath + "/_App/AutoCreator/AutoRegister/ModuleMgr/UIConst_AutoCreator.cs";
            WriteFile(targetPath, uiConstClassStr);
            Debug.Log("[ProjectAutoRegisterTool]处理UI常量完成:" + targetPath);
        }

        private static void AutoCreateCtrlConst(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (IOTool.IsHidden(directory)) return;
                if (directory.GetDirectories().Length != 0)
                {
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < directories.Length; i++)
                    {
                        AutoCreateCtrlConst(directories[i].FullName);
                    }
                }

                // 处理自动注册的逻辑
                FileInfo[] files = directory.GetFiles();
                bool haveMVC = false;
                for (int j = 0; j < files.Length; j++)
                {
                    if (!files[j].Name.EndsWith(".cs")) continue;
                    if (IOTool.IsHidden(files[j])) continue;

                    string Name = files[j].Name.Split('.')[0];
                    if (!Name.EndsWith("GCtrl") && !Name.EndsWith("UICtrl") && Name.EndsWith("Ctrl"))
                    {
                        Auto_CtrlConst += TableStr + TableStr + "public const string " + Name + " = \"" + Name + "\";\r\n"; ;
                        haveMVC = true;
                    }
                }

                if (haveMVC)
                    AutoRegisterVisualizationInfo(path, VisualizationType.MVC);
            }
        }

        private static void FinishCreateCtrlConst()
        {
            if (Auto_CtrlConst.Length > 0)
            {
                Auto_CtrlConst = Auto_CtrlConst.Substring(0, Auto_CtrlConst.Length - 2);
            }

            string ctrlConstClassStr = AutoCreatorComments +
@"namespace ProjectApp
{
    public static class CtrlConst
    {
//ReplaceCtrlConst
    }
}";

            string replaceModelConst = "//ReplaceCtrlConst";
            ctrlConstClassStr = ctrlConstClassStr.Replace(replaceModelConst, Auto_CtrlConst);

            string targetPath = Application.dataPath + "/_App/AutoCreator/AutoRegister/ModuleMgr/CtrlConst_AutoCreator.cs";
            WriteFile(targetPath, ctrlConstClassStr);
            Debug.Log("[ProjectAutoRegisterTool]处理Ctrl常量完成:" + targetPath);
        }

        private static void AutoCreateUICtrlConst(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (IOTool.IsHidden(directory)) return;
                if (directory.GetDirectories().Length != 0)
                {
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < directories.Length; i++)
                    {
                        AutoCreateUICtrlConst(directories[i].FullName);
                    }
                }

                // 处理自动注册的逻辑
                FileInfo[] files = directory.GetFiles();
                bool haveMVC = false;
                for (int j = 0; j < files.Length; j++)
                {
                    if (!files[j].Name.EndsWith(".cs")) continue;
                    if (IOTool.IsHidden(files[j])) continue;

                    string Name = files[j].Name.Split('.')[0];
                    if (Name.EndsWith("UICtrl"))
                    {
                        Auto_UICtrlConst += TableStr + TableStr + "public const string " + Name + " = \"" + Name + "\";\r\n";
                        haveMVC = true;
                    }
                }

                if (haveMVC)
                    AutoRegisterVisualizationInfo(path, VisualizationType.MVC);
            }
        }

        private static void FinishCreateUICtrlConst()
        {
            if (Auto_UICtrlConst.Length > 0)
            {
                Auto_UICtrlConst = Auto_UICtrlConst.Substring(0, Auto_UICtrlConst.Length - 2);
            }

            string uiCtrlConstClassStr = AutoCreatorComments +
@"namespace ProjectApp
{
    public static class UICtrlConst
    {
//ReplaceUICtrlConst
    }
}";

            string replaceModelConst = "//ReplaceUICtrlConst";
            uiCtrlConstClassStr = uiCtrlConstClassStr.Replace(replaceModelConst, Auto_UICtrlConst);

            string targetPath = Application.dataPath + "/_App/AutoCreator/AutoRegister/ModuleMgr/UICtrlConst_AutoCreator.cs";
            WriteFile(targetPath, uiCtrlConstClassStr);
            Debug.Log("[ProjectAutoRegisterTool]处理UICtrl常量完成:" + targetPath);
        }

        private static void AutoRegisterModuleMgr(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (IOTool.IsHidden(directory)) return;
                if (directory.GetDirectories().Length != 0)
                {
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < directories.Length; i++)
                    {
                        AutoRegisterModuleMgr(directories[i].FullName);
                    }
                }

                // 处理自动注册的逻辑
                FileInfo[] files = directory.GetFiles();
                bool haveMVC = false;
                for (int j = 0; j < files.Length; j++)
                {
                    if (!files[j].Name.EndsWith(".cs")) continue;
                    if (IOTool.IsHidden(files[j])) continue;

                    string Name = files[j].Name.Split('.')[0];
                    if (!Name.EndsWith("VOModel") && Name.EndsWith("Model"))
                    {
                        Auto_ModuleMgr_Model += ThreeTab + "moduleMgr.AddModel(ModelConst." + Name + ", new " + Name + "());\r\n";
                        haveMVC = true;
                        continue;
                    }
                    if (Name.EndsWith("UI") && !Name.StartsWith("BaseUI"))
                    {
                        Auto_ModuleMgr_UIType += ThreeTab + "moduleMgr.AddUIType(UIConst." + Name + ", typeof(" + Name + "));\r\n";
                        haveMVC = true;
                        continue;
                    }
                    if (Name.EndsWith("UICtrl"))
                    {
                        Auto_ModuleMgr_UICtrl += ThreeTab + "moduleMgr.AddUICtrl(UICtrlConst." + Name + ", new " + Name + "());\r\n";
                        haveMVC = true;
                        continue;
                    }
                    if (!Name.EndsWith("GCtrl") && !Name.EndsWith("UICtrl") && Name.EndsWith("Ctrl"))
                    {
                        Auto_ModuleMgr_Ctrl += ThreeTab + "moduleMgr.AddCtrl(CtrlConst." + Name + ", new " + Name + "());\r\n";
                        haveMVC = true;
                        continue;
                    }
                }

                if (haveMVC)
                    AutoRegisterVisualizationInfo(path, VisualizationType.MVC);
            }
        }

        private static void FinishRegisterModuleMgr()
        {
            if (Auto_ModuleMgr_Model.Length > 0)
            {
                Auto_ModuleMgr_Model = Auto_ModuleMgr_Model.Substring(0, Auto_ModuleMgr_Model.Length - 2);
            }
            if (Auto_ModuleMgr_UIType.Length > 0)
            {
                Auto_ModuleMgr_UIType = Auto_ModuleMgr_UIType.Substring(0, Auto_ModuleMgr_UIType.Length - 2);
            }
            if (Auto_ModuleMgr_Ctrl.Length > 0)
            {
                Auto_ModuleMgr_Ctrl = Auto_ModuleMgr_Ctrl.Substring(0, Auto_ModuleMgr_Ctrl.Length - 2);
            }
            if (Auto_ModuleMgr_UICtrl.Length > 0)
            {
                Auto_ModuleMgr_UICtrl = Auto_ModuleMgr_UICtrl.Substring(0, Auto_ModuleMgr_UICtrl.Length - 2);
            }

            string classStr = AutoCreatorComments +
@"using FutureCore;

namespace ProjectApp
{
    public static class ModuleMgrRegister
    {
        public static void AutoRegisterModel()
        {
            ModuleMgr moduleMgr = ModuleMgr.Instance;
//ReplaceModel
        }

        public static void AutoRegisterUIType()
        {
            ModuleMgr moduleMgr = ModuleMgr.Instance;
//ReplaceUIType
        }

        public static void AutoRegisterCtrl()
        {
            ModuleMgr moduleMgr = ModuleMgr.Instance;
//ReplaceCtrl
        }

        public static void AutoRegisterUICtrl()
        {
            ModuleMgr moduleMgr = ModuleMgr.Instance;
//ReplaceUICtrl
        }
    }
}";

            string replaceModel = "//ReplaceModel";
            string replaceUIType = "//ReplaceUIType";
            string replaceCtrl = "//ReplaceCtrl";
            string replaceUICtrl = "//ReplaceUICtrl";

            classStr = classStr.Replace(replaceModel, Auto_ModuleMgr_Model);
            classStr = classStr.Replace(replaceUIType, Auto_ModuleMgr_UIType);
            classStr = classStr.Replace(replaceCtrl, Auto_ModuleMgr_Ctrl);
            classStr = classStr.Replace(replaceUICtrl, Auto_ModuleMgr_UICtrl);

            string targetPath = Application.dataPath + "/_App/AutoCreator/AutoRegister/ModuleMgr/ModuleMgrRegister_AutoCreator.cs";
            WriteFile(targetPath, classStr);
            Debug.Log("[ProjectAutoRegisterTool]处理ModuleMgr完成:" + targetPath);
        }        

        private static void WriteFile(string targetPath, string classStr)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(targetPath);
            if (!directoryInfo.Parent.Exists)
            {
                Directory.CreateDirectory(directoryInfo.Parent.FullName);
            }
            File.WriteAllText(targetPath, classStr, new UTF8Encoding(false));
        }
    }
}