using System.Collections.Generic;
using System.IO;
using FairyGUI;
using ProjectApp;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace FutureEditor
{
    public static class FGUIContollerLoadCreateTool_v1
    {
        private const string content =
@"/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

/// <summary>
/// FGUI控制器自动生成
/// </summary>
namespace UI.{0}
{
    public partial class className
    {
{1}    }
}";

        private const int index = 1;
        private static Dictionary<string, string> classDic = new Dictionary<string, string>();
        private static Dictionary<string, string> constClassDic = new Dictionary<string, string>();
        private static string C_DestFolderPath = Application.dataPath + "/_App/AutoCreator/FGUIPagesDefine/Common/";
        private static string DestFolderPath = Application.dataPath + "/_App/AutoCreator/FGUIPagesDefine/Project/";

        //[MenuItem("[FC Project]/FGUI/ContollerCreate/1) 生成所有界面控制器常量", false, 0)]
        public static void CreateAllControllerScripts()
        {
            UIPackage.RemoveAllPackages();
            UIMgrRegister.AutoRegisterBinder();
            UIMgrRegister.AutoRegisterCommonPackages();

            CreateControllerScripts(true);  
            CreateControllerScripts(false);

            UIPackage.RemoveAllPackages();
            EditorSceneManager.SaveOpenScenes();
        }

        //[MenuItem("[FC Project]/FGUI/ContollerCreate/2) 生成通用界面控制器常量", false, 1)]
        public static void CreateController_ScriptsCommon()
        {
            UIPackage.RemoveAllPackages();
            UIMgrRegister.AutoRegisterBinder();
            UIMgrRegister.AutoRegisterCommonPackages();

            CreateControllerScripts(true);

            UIPackage.RemoveAllPackages();
            EditorSceneManager.SaveOpenScenes();
        }

        //[MenuItem("[FC Project]/FGUI/ContollerCreate/3) 生成项目界面控制器常量", false, 2)]
        public static void CreateController_ScriptsProject()
        {
            UIPackage.RemoveAllPackages();
            UIMgrRegister.AutoRegisterBinder();
            UIMgrRegister.AutoRegisterCommonPackages();

            CreateControllerScripts(false);

            UIPackage.RemoveAllPackages();
            EditorSceneManager.SaveOpenScenes();
        }

        private static void CreateControllerScripts(bool isCreateCommon)
        {
            classDic.Clear();
            constClassDic.Clear();

            string[] ids = AssetDatabase.FindAssets("_fui t:textAsset");
            foreach (var item in ids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(item);
                int pos = assetPath.LastIndexOf("_fui");
                if (pos == -1)
                    continue;

                assetPath = assetPath.Substring(0, pos);
                LoadOneUI(assetPath, isCreateCommon);
            }
            CreateScripts(isCreateCommon);
        }

        private static void CreateScripts(bool isCreateCommon)
        {
            if (isCreateCommon)
            {
                // 通用模块使用增量生成
                //if (Directory.Exists(C_DestFolderPath))
                //Directory.Delete(C_DestFolderPath, true);
            }
            else
            {
                if (Directory.Exists(DestFolderPath))
                {
                    Directory.Delete(DestFolderPath, true);
                }
                Directory.CreateDirectory(DestFolderPath);
            }

            string startPre = isCreateCommon ? C_DestFolderPath : DestFolderPath;
            foreach (var item in classDic)
            {
                if (item.Key.StartsWith(startPre))
                    FileTool.CreateTxt(item.Key, item.Value, true);
            }
            foreach (var item in constClassDic)
            {
                if (item.Key.StartsWith(startPre))
                    FileTool.CreateTxt(item.Key, item.Value, true);
            }

            if (isCreateCommon)
                Debug.Log("[FGUIContollerCreateTool]生成通用界面控制器常量完成:" + startPre);
            else
                Debug.Log("[FGUIContollerCreateTool]生成项目界面控制器常量完成:" + startPre);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void LoadOneUI(string resPath, bool isCreateCommon)
        {
            string objName = new FileInfo(Path.GetFullPath(resPath)).Name;
            UIPackage pkg = UIPackage.GetByName(objName);
            if (pkg == null)
            {
                pkg = UIPackage.AddPackage(resPath);
            }

            List<GComponent> list = new List<GComponent>();
            foreach (var item in pkg.GetItems())
            {
                if (item.type == PackageItemType.Component)
                {
                    GComponent gComponent = null;

                    var tmpGomponent = pkg.CreateObject(item.name);
                    gComponent = tmpGomponent as GComponent;

                    if (gComponent == null)
                    {
                        DisposeGObject(tmpGomponent);
                        continue;
                    }

                    string tmpPropertyStr = "";
                    string constPropertyStr = "";
                    List<string> nameList = new List<string>();
                    foreach (var tmp in gComponent.Controllers)
                    {
                        nameList.Clear();
                        for (int i = 0; i < tmp.pageCount; i++)
                        {
                            //Debug.Log($"{item.name}:{tmp.name}:{tmp.GetPageName(i)}:Index:{i}");

                            //string contName = tmp.name;
                            //string[] names = contName.Split('_');
                            //if(names!=null&&names.Length==2)
                            //contName = names[1];

                            string name = tmp.GetPageName(i).Replace(" ", "");
                            if (nameList.Contains(name))
                            {
                                continue;
                            }
                            nameList.Add(name);

                            string summary =
@"        /// <summary>
        /// {0}:{1}
        /// </summary>
        ";
                            summary = string.Format(summary, i, tmp.GetPageName(i));

                            tmpPropertyStr += summary;
                            constPropertyStr += summary;
                            string contName = tmp.name.Replace("cont_", "");
                            contName = FirstLetterToUpper(contName);
                            tmpPropertyStr += string.Format("public int _{0}_{1} = {2};\r\n", contName, name, i);
                            constPropertyStr += string.Format("public const int {0}_{1} = {2};\r\n", contName, name, i);
                        }
                    }

                    List<Controller> controllers = gComponent.Controllers;
                    DisposeGObject(gComponent);
                    if (controllers.Count != 0)
                    {
                        if (isCreateCommon)
                        {
                            if (!pkg.name.StartsWith("C"))
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (pkg.name.StartsWith("C"))
                            {
                                continue;
                            }
                        }

                        string objClassContent = content.Replace("{0}", pkg.name).Replace("{1}", tmpPropertyStr).Replace("className", item.name);
                        string constClassContent = content.Replace("{0}", pkg.name).Replace("{1}", constPropertyStr).Replace("className", item.name);
                        string prePath = pkg.name.StartsWith("C") ? C_DestFolderPath : DestFolderPath;
                        string path = prePath + pkg.name + "/" + item.name + "_AutoCreator.cs";
                        string constPath = prePath + pkg.name + "/" + item.name + "_Const_AutoCreator.cs";
                        if (!classDic.ContainsKey(path))
                        {
                            classDic.Add(path, objClassContent);
                            constClassDic.Add(constPath, constClassContent);
                        }
                    }
                }
            }
        }

        private static void DisposeGObject(GObject gObject)
        {
            gObject.Dispose();
            if (gObject.displayObject != null)
            {
                if (gObject.displayObject.gameObject)
                {
                    Object.Destroy(gObject.displayObject.gameObject);
                }
            }
        }

        private static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}