using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class ProjectAutoRegisterTool
    {
        private const string AutoCreatorComments =
@"/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

";

        private static string TableStr = "    ";
        private static string ThreeTab = TableStr + TableStr + TableStr;
        private static string UIDir = EditorPathConst.ResResourcesPath + "UI";

        //不再维护此接口 (反射DLL创建)
        //[MenuItem("[FC Project]/AutoRegister/自动注册项目_v1", false, 0)]
        private static void AutoRegisterAll()
        {
            AutoRegisterSceneMgr(true);

            AutoCreateConfigConst(true);
            AutoRegisterConfigMgr(true);

            AutoRegisterUIMgr(true);

            AutoCreateModelConst(true);
            AutoCreateUIConst(true);
            AutoCreateCtrlConst(true);
            AutoCreateUICtrlConst(true);
            AutoRegisterModuleMgr(true);

            PreferencesMgrAutoRegisterTool.AutoRegisterPreferencesMsg();
            PreferencesMgrAutoRegisterTool.AutoRegisterPreferencesMgr();

            AssetDatabase.Refresh();
            Debug.Log("自动注册项目完成");
        }

        //不再维护此接口 (反射创建)
        //[MenuItem("[FC Project]/AutoRegister/自动注销项目", false, 1)]
        private static void AutoCancelAll()
        {
            AutoRegisterSceneMgr(false);

            //AutoCreateConfigConst(false);
            AutoRegisterConfigMgr(false);

            AutoRegisterUIMgr(false);

            //AutoCreateModelConst(false);
            //AutoCreateUIConst(false);
            //AutoCreateCtrlConst(false);
            //AutoCreateUICtrlConst(false);
            AutoRegisterModuleMgr(false);

            //PreferenceMgrAutoRegisterTool.AutoRegisterPreferenceMsg();
            //PreferenceMgrAutoRegisterTool.AutoRegisterPreferenceMgr();

            AssetDatabase.Refresh();
            Debug.Log("自动注销项目完成");
        }

        private static Assembly GetProjectAssembly()
        {
            Assembly assembly = Assembly.Load("Assembly-CSharp");
            return assembly;
        }

        private static void AutoRegisterSceneMgr(bool isRegister)
        {
            Assembly assembly = GetProjectAssembly();
            string sceneInfo = string.Empty;
            if (isRegister)
            {
                foreach (Type item in assembly.GetTypes())
                {
                    if (item.BaseType != null)
                        if (item.BaseType.Name.StartsWith("BaseScene") && item.Name.EndsWith("Scene"))
                        {
                            Debug.Log("AutoRegister:" + item.Name);
                            string row = ThreeTab + "SceneMgr.Instance.AddScene(new " + item.Name + "());\n";
                            sceneInfo += row;
                        }
                }
                if (sceneInfo.Length > 0)
                {
                    sceneInfo = sceneInfo.Substring(0, sceneInfo.Length - 1);
                }
            }

            string classStr = AutoCreatorComments +
    @"using FutureCore;

namespace ProjectApp
{
    public static class SceneMgrRegister
    {
        public static void AutoRegisterScene()
        {
//ReplaceScene
        }
    }
}";
            string replaceConfig = "//ReplaceScene";
            classStr = classStr.Replace(replaceConfig, sceneInfo);

            string targetPath = Application.dataPath + "/_App/ProjectApp/_AutoCreator/SceneMgr/SceneMgrRegister_AutoCreator.cs";
            File.WriteAllText(targetPath, classStr, new UTF8Encoding(false));
            Debug.Log("处理SceneMgr完成:" + targetPath);
            AssetDatabase.Refresh();
        }

        private static void AutoCreateConfigConst(bool isRegister)
        {
            Assembly assembly = GetProjectAssembly();
            Type[] classes = assembly.GetTypes();
            string configConstInfo = string.Empty;
            if (isRegister)
            {
                foreach (var item in classes)
                {
                    if (item.BaseType == null)
                    {
                        LogUtil.Log(item.Name);
                        continue;
                    }
                    if (item.BaseType.Name.StartsWith("BaseVOModel") && item.Name.EndsWith("VOModel"))
                    {
                        string name = item.Name.Replace("VOModel", "");
                        string row = TableStr + TableStr + "public const string " + name + " = \"" + name + "\";\n";
                        configConstInfo += row;
                    }
                }
                if (configConstInfo.Length > 0)
                {
                    configConstInfo = configConstInfo.Substring(0, configConstInfo.Length - 1);
                }
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
            configConstClassStr = configConstClassStr.Replace(replaceModelConst, configConstInfo);

            string targetPath = Application.dataPath + "/_App/ProjectApp/_AutoCreator/ConfigMgr/ConfigConst_AutoCreator.cs";
            File.WriteAllText(targetPath, configConstClassStr, new UTF8Encoding(false));
            Debug.Log("处理Config常量完成:" + targetPath);
        }

        private static void AutoRegisterConfigMgr(bool isRegister)
        {
            Assembly assembly = GetProjectAssembly();
            string voModelInfo = string.Empty;
            if (isRegister)
            {
                foreach (var item in assembly.GetTypes())
                {
                    if (item.Name.EndsWith("BaseVOModel"))
                    {
                        continue;
                    }
                    if (item.BaseType == null)
                    {
                        continue;
                    }
                    if (item.BaseType.Name.StartsWith("BaseVOModel") && item.Name.EndsWith("VOModel"))
                    {
                        Debug.Log("AutoRegister:" + item.Name);
                        string row = ThreeTab + "ConfigMgr.Instance.AddConfigVOModel(ConfigConst." + item.Name.Replace("VOModel", "") + ", " + item.Name + ".Instance);\n";
                        voModelInfo += row;
                    }
                }
                if (voModelInfo.Length > 0)
                {
                    voModelInfo = voModelInfo.Substring(0, voModelInfo.Length - 1);
                }
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
//ReplaceConfig
        }
    }
}";
            string replaceConfig = "//ReplaceConfig";
            classStr = classStr.Replace(replaceConfig, voModelInfo);

            string targetPath = Application.dataPath + "/_App/ProjectApp/_AutoCreator/ConfigMgr/ConfigMgrRegister_AutoCreator.cs";
            File.WriteAllText(targetPath, classStr, new UTF8Encoding(false));
            Debug.Log("处理ConfigMgr完成:" + targetPath);
        }

        private static void AutoRegisterUIMgr(bool isRegister)
        {
            Assembly assembly = GetProjectAssembly();
            Type[] classes = assembly.GetTypes();
            string uiBinderInfo = string.Empty;
            if (isRegister)
            {
                foreach (var item in classes)
                {
                    if (item.BaseType == null)
                    {
                        continue;
                    }
                    if (item.Namespace != null && item.Namespace.StartsWith("UI.") && item.Name.EndsWith("Binder"))
                    {
                        string row = ThreeTab + item.Namespace + "." + item.Name + ".BindAll();" + "\n";
                        uiBinderInfo += row;
                    }
                }
                if (uiBinderInfo.Length > 0)
                {
                    uiBinderInfo = uiBinderInfo.Substring(0, uiBinderInfo.Length - 1);
                }
            }

            string uiCommonPackageInfo = string.Empty;
            if (isRegister)
            {
                string fguiPackage = "_fui.bytes";
                DirectoryInfo uiDirInfo = new DirectoryInfo(UIDir);
                FileInfo[] files = uiDirInfo.GetFiles();
                foreach (FileInfo file in files)
                {
                    if (file.Name.EndsWith(fguiPackage))
                    {
                        if (file.Name.StartsWith("A") || file.Name.StartsWith("Font_"))
                        {
                            string packageName = "\"" + file.Name.Replace(fguiPackage, string.Empty) + "\"";
                            string row = ThreeTab + "UIMgr.Instance.RegisterCommonPackage(" + packageName + ");" + "\n";
                            uiCommonPackageInfo += row;
                        }
                    }
                }
                if (uiCommonPackageInfo.Length > 0)
                {
                    uiCommonPackageInfo = uiCommonPackageInfo.Substring(0, uiCommonPackageInfo.Length - 1);
                }
            }

            string classStr = AutoCreatorComments +
    @"using FutureCore;

namespace ProjectApp
{
    public static class UIMgrRegister
    {
        public static void AutoRegisterBinder()
        {
//ReplaceBinder
        }

        public static void AutoRegisterCommonPackage()
        {
//ReplaceCommonPackage
        }
    }
}";

            string replaceBinderConst = "//ReplaceBinder";
            classStr = classStr.Replace(replaceBinderConst, uiBinderInfo);
            string replaceCommonPackageConst = "//ReplaceCommonPackage";
            classStr = classStr.Replace(replaceCommonPackageConst, uiCommonPackageInfo);

            string targetPath = Application.dataPath + "/_App/ProjectApp/_AutoCreator/UIMgr/UIMgrRegister_AutoCreator.cs";
            File.WriteAllText(targetPath, classStr, new UTF8Encoding(false));
            Debug.Log("处理UIMgr完成:" + targetPath);
        }

        private static void AutoCreateModelConst(bool isRegister)
        {
            Assembly assembly = GetProjectAssembly();
            Type[] classes = assembly.GetTypes();
            string modelConstInfo = string.Empty;
            if (isRegister)
            {
                foreach (var item in classes)
                {
                    if (item.Name.EndsWith("VOModel"))
                    {
                        continue;
                    }
                    if (item.BaseType == null)
                    {
                        continue;
                    }
                    if (item.BaseType.Name.StartsWith("BaseModel") && item.Name.EndsWith("Model"))
                    {
                        string row = TableStr + TableStr + "public const string " + item.Name + " = \"" + item.Name + "\";\n";
                        modelConstInfo += row;
                    }
                }
                if (modelConstInfo.Length > 0)
                {
                    modelConstInfo = modelConstInfo.Substring(0, modelConstInfo.Length - 1);
                }
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
            modelConstClassStr = modelConstClassStr.Replace(replaceModelConst, modelConstInfo);

            string targetPath = Application.dataPath + "/_App/ProjectApp/_AutoCreator/ModuleMgr/ModelConst_AutoCreator.cs";
            File.WriteAllText(targetPath, modelConstClassStr, new UTF8Encoding(false));
            Debug.Log("处理Model常量完成:" + targetPath);
        }

        private static void AutoCreateUIConst(bool isRegister)
        {
            Assembly assembly = GetProjectAssembly();
            Type[] classes = assembly.GetTypes();
            string uiConstInfo = string.Empty;
            if (isRegister)
            {
                foreach (var item in classes)
                {
                    if (item.BaseType == null)
                    {
                        continue;
                    }
                    if (item.BaseType.Name.StartsWith("BaseUI") && item.Name.EndsWith("UI"))
                    {
                        string row = TableStr + TableStr + "public const string " + item.Name + " = \"" + item.Name + "\";\n";
                        uiConstInfo += row;
                    }
                }
                if (uiConstInfo.Length > 0)
                {
                    uiConstInfo = uiConstInfo.Substring(0, uiConstInfo.Length - 1);
                }
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
            uiConstClassStr = uiConstClassStr.Replace(replaceModelConst, uiConstInfo);

            string targetPath = Application.dataPath + "/_App/ProjectApp/_AutoCreator/ModuleMgr/UIConst_AutoCreator.cs";
            File.WriteAllText(targetPath, uiConstClassStr, new UTF8Encoding(false));
            Debug.Log("处理UI常量完成:" + targetPath);
        }

        private static void AutoCreateCtrlConst(bool isRegister)
        {
            Assembly assembly = GetProjectAssembly();
            Type[] classes = assembly.GetTypes();
            string ctrlConstInfo = string.Empty;
            if (isRegister)
            {
                foreach (var item in classes)
                {
                    if (item.BaseType == null)
                    {
                        continue;
                    }
                    if (item.BaseType.Name.StartsWith("BaseCtrl") && item.Name.EndsWith("Ctrl"))
                    {
                        string row = TableStr + TableStr + "public const string " + item.Name + " = \"" + item.Name + "\";\n";
                        ctrlConstInfo += row;
                    }
                }
                if (ctrlConstInfo.Length > 0)
                {
                    ctrlConstInfo = ctrlConstInfo.Substring(0, ctrlConstInfo.Length - 1);
                }
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
            ctrlConstClassStr = ctrlConstClassStr.Replace(replaceModelConst, ctrlConstInfo);

            string targetPath = Application.dataPath + "/_App/ProjectApp/_AutoCreator/ModuleMgr/CtrlConst_AutoCreator.cs";
            File.WriteAllText(targetPath, ctrlConstClassStr, new UTF8Encoding(false));
            Debug.Log("处理Ctrl常量完成:" + targetPath);
        }

        private static void AutoCreateUICtrlConst(bool isRegister)
        {
            Assembly assembly = GetProjectAssembly();
            Type[] classes = assembly.GetTypes();
            string uiCtrlConstInfo = string.Empty;
            if (isRegister)
            {
                foreach (var item in classes)
                {
                    if (item.BaseType == null)
                    {
                        continue;
                    }
                    if (item.BaseType.Name.StartsWith("BaseUICtrl") && item.Name.EndsWith("UICtrl"))
                    {
                        string row = TableStr + TableStr + "public const string " + item.Name + " = \"" + item.Name + "\";\n";
                        uiCtrlConstInfo += row;
                    }
                }
                if (uiCtrlConstInfo.Length > 0)
                {
                    uiCtrlConstInfo = uiCtrlConstInfo.Substring(0, uiCtrlConstInfo.Length - 1);
                }
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
            uiCtrlConstClassStr = uiCtrlConstClassStr.Replace(replaceModelConst, uiCtrlConstInfo);

            string targetPath = Application.dataPath + "/_App/ProjectApp/_AutoCreator/ModuleMgr/UICtrlConst_AutoCreator.cs";
            File.WriteAllText(targetPath, uiCtrlConstClassStr, new UTF8Encoding(false));
            Debug.Log("处理UICtrl常量完成:" + targetPath);
        }

        private static void AutoRegisterModuleMgr(bool isRegister)
        {
            Assembly assembly = GetProjectAssembly();
            Type[] classes = assembly.GetTypes();

            string modelInfo = string.Empty;
            string uiInfo = string.Empty;
            string ctrlInfo = string.Empty;
            string uiCtrlInfo = string.Empty;

            if (isRegister)
            {
                foreach (var item in classes)
                {
                    if (item.Name.EndsWith("VOModel"))
                    {
                        continue;
                    }
                    if (item.BaseType == null)
                    {
                        continue;
                    }
                    if (item.BaseType.Name.StartsWith("BaseModel") && item.Name.EndsWith("Model"))
                    {
                        string row = ThreeTab + "ModuleMgr.Instance.AddModel(ModelConst." + item.Name + ", new " + item.Name + "());\n";
                        modelInfo += row;
                    }
                }
                if (modelInfo.Length > 0)
                {
                    modelInfo = modelInfo.Substring(0, modelInfo.Length - 1);
                }

                foreach (var item in classes)
                {
                    if (item.BaseType == null)
                    {
                        continue;
                    }
                    if (item.BaseType.Name.StartsWith("BaseUI") && item.Name.EndsWith("UI"))
                    {
                        string row = ThreeTab + "ModuleMgr.Instance.AddUIType(UIConst." + item.Name + ", typeof(" + item.Name + "));\n";
                        uiInfo += row;
                    }
                }
                if (uiInfo.Length > 0)
                {
                    uiInfo = uiInfo.Substring(0, uiInfo.Length - 1);
                }

                foreach (var item in classes)
                {
                    if (item.BaseType == null)
                    {
                        continue;
                    }
                    if (item.BaseType.Name.StartsWith("BaseCtrl") && item.Name.EndsWith("Ctrl") && !item.Name.EndsWith("UICtrl"))
                    {
                        string row = ThreeTab + "ModuleMgr.Instance.AddCtrl(CtrlConst." + item.Name + ", new " + item.Name + "());\n";
                        ctrlInfo += row;
                    }
                }
                if (ctrlInfo.Length > 0)
                {
                    ctrlInfo = ctrlInfo.Substring(0, ctrlInfo.Length - 1);
                }

                foreach (var item in classes)
                {
                    if (item.BaseType == null)
                    {
                        continue;
                    }
                    if (item.BaseType.Name.StartsWith("BaseUICtrl") && item.Name.EndsWith("UICtrl"))
                    {
                        string row = ThreeTab + "ModuleMgr.Instance.AddUICtrl(UICtrlConst." + item.Name + ", new " + item.Name + "());\n";
                        uiCtrlInfo += row;
                    }
                }
                if (uiCtrlInfo.Length > 0)
                {
                    uiCtrlInfo = uiCtrlInfo.Substring(0, uiCtrlInfo.Length - 1);
                }
            }

            string classStr = AutoCreatorComments +
    @"using FutureCore;

namespace ProjectApp
{
    public static class ModuleMgrRegister
    {
        public static void AutoRegisterModel()
        {
//ReplaceModel
        }

        public static void AutoRegisterCtrl()
        {
//ReplaceCtrl
        }

        public static void AutoRegisterUICtrl()
        {
//ReplaceUICtrl
        }

        public static void AutoRegisterUIType()
        {
//AutoRegisterUIType
        }
    }
}";

            string replaceModel = "//ReplaceModel";
            string replaceUIType = "//AutoRegisterUIType";
            string replaceCtrl = "//ReplaceCtrl";
            string replaceUICtrl = "//ReplaceUICtrl";

            classStr = classStr.Replace(replaceModel, modelInfo);
            classStr = classStr.Replace(replaceUIType, uiInfo);
            classStr = classStr.Replace(replaceCtrl, ctrlInfo);
            classStr = classStr.Replace(replaceUICtrl, uiCtrlInfo);

            string targetPath = Application.dataPath + "/_App/ProjectApp/_AutoCreator/ModuleMgr/AModuleMgrRegister_AutoCreator.cs";
            File.WriteAllText(targetPath, classStr, new UTF8Encoding(false));
            Debug.Log("处理ModuleMgr完成:" + targetPath);
        }
    }
}