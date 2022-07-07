using UnityEditor;
using Spine.Unity;
using System.IO;
using UnityEngine;

namespace FutureEditor
{
    /// <summary>
    /// Spine动画常量生成工具
    /// </summary>
    public static class SpineAnimNameCreateTool
    {
        private static string spineAssetsPath = "Assets/_Res/Resources/Skeleton";
        private static string codeCreatePath = "Assets/_App/AutoCreator/Automation/SpineAnimName";

        [MenuItem("[FC Project]/Automation/SpineAnimName/生成Spine动画常量脚本", false, 0)]
        public static void Create()
        {
            if (Directory.Exists(spineAssetsPath))
            {
                if (!Directory.Exists(codeCreatePath))
                {
                    Directory.CreateDirectory(codeCreatePath);
                }

                string[] files = Directory.GetFiles(spineAssetsPath);
                for (int i = 0; i < files.Length; i++)
                {
                    string codes = 
@"/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

namespace ProjectApp
{
    public static class $className
    {
";

                    GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(files[i]);
                    if (!go) continue;

                    go = Object.Instantiate(go);
                    SkeletonAnimation skeAnim = go.GetComponentInChildren<SkeletonAnimation>();

                    string fileName = Path.GetFileName(files[i]);
                    string className = "Spine_" + fileName.Substring(0, fileName.IndexOf('.'));
                    Debug.Log("[SpineAnimNameCreateTool]文件名称: " + className);

                    bool isFirst = true;
                    if (skeAnim != null)
                    {
                        foreach (var item in skeAnim.skeleton.Data.Animations)
                        {
                            if (!isFirst)
                            {
                                codes += "\r\n\r\n";
                            }

                            codes += "        public const string " + item.Name + " = \"" + item.Name + "\";";
                            isFirst = false;
                        }
                    }

                    codes += @"
    }
}";

                    codes = codes.Replace("$className", className);

                    File.WriteAllText(codeCreatePath + "/" + className + "_AutoCreator.cs", codes);
                    Object.DestroyImmediate(go);
                }

                Debug.Log("[SpineAnimNameCreateTool]生成Spine动画常量脚本完成");
            }
        }
    }
}