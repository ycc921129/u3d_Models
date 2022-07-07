using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class CopyPrintPathTool
    {
        [MenuItem("Assets/[FC Shortcut]/CopyPrint/复制打印资源路径", false, -140)]
        private static void PrintAssetsPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            path = "\"" + path + "\"";
            GUIUtility.systemCopyBuffer = path;
            Debug.Log(path);
        }

        [MenuItem("Assets/[FC Shortcut]/CopyPrint/复制打印Resources资源路径", false, -140)]
        private static void PrintItemAssetsPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            path = path.Replace("Assets/_Res/Resources/", string.Empty);
            path = PathTool.GetPathWithoutExtention(path);
            path = "\"" + path + "\"";
            GUIUtility.systemCopyBuffer = path;
            Debug.Log(path);
        }

        [MenuItem("GameObject/[FC 对象]/CopyPrint/复制打印对象路径", false, 0)]
        private static void OnSelectedChanged()
        {
            GameObject gobj = Selection.activeGameObject;
            List<Transform> list = new List<Transform>();
            if (gobj != null)
            {
                SetParentList(gobj, list);
                string path = string.Empty;
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    path += list[i].name + "/";
                }
                path += gobj.name;
                path = "\"" + path + "\"";
                GUIUtility.systemCopyBuffer = path;
                Debug.Log(path);
            }
        }

        private static void SetParentList(GameObject gobj, List<Transform> list)
        {
            if (gobj.transform.parent != null)
            {
                list.Add(gobj.transform.parent);
                SetParentList(gobj.transform.parent.gameObject, list);
            }
        }
    }
}