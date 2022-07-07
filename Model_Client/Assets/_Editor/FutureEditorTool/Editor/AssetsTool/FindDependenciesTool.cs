using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace FutureEditor
{
    public static class FindDependenciesTool
    {
        [MenuItem("Assets/[FC Shortcut]/Dependencies/查找依赖项", false, 0)]
        private static void Find()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (!string.IsNullOrEmpty(path))
            {
                string[] infos = AssetDatabase.GetDependencies(path);
                foreach (string file in infos)
                {
                    Debug.Log(file, AssetDatabase.LoadAssetAtPath<Object>(GetRelativeAssetsPath(file)));
                }
            }
            Debug.Log("匹配结束");
        }

        [MenuItem("Assets/[FC Shortcut]/Dependencies/查找被依赖项", false, 1)]
        private static void ReverseFind()
        {
            EditorSettings.serializationMode = SerializationMode.ForceText;
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (!string.IsNullOrEmpty(path))
            {
                string guid = AssetDatabase.AssetPathToGUID(path);
                List<string> withoutExtensions = new List<string>() { ".prefab", ".unity", ".mat", ".asset" };
                string[] files = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories)
                    .Where(s => withoutExtensions.Contains(Path.GetExtension(s).ToLower())).ToArray();
                int startIndex = 0;

                EditorApplication.update = delegate ()
                {
                    string file = files[startIndex];

                    bool isCancel = EditorUtility.DisplayCancelableProgressBar("匹配资源中", file, (float)startIndex / (float)files.Length);

                    if (Regex.IsMatch(File.ReadAllText(file), guid))
                    {
                        Debug.Log(file, AssetDatabase.LoadAssetAtPath<Object>(GetRelativeAssetsPath(file)));
                    }

                    startIndex++;
                    if (isCancel || startIndex >= files.Length)
                    {
                        EditorUtility.ClearProgressBar();
                        EditorApplication.update = null;
                        startIndex = 0;
                        Debug.Log("匹配结束");
                    }
                };
            }
        }

        [MenuItem("Assets/[FC Shortcut]/Dependencies/查找引用", true, 0)]
        private static bool OFind()
        {
            return IsSelectObject();
        }

        [MenuItem("Assets/[FC Shortcut]/Dependencies/查找反引用", true, 1)]
        private static bool OReverseFind()
        {
            return IsSelectObject();
        }

        private static bool IsSelectObject()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            return (!string.IsNullOrEmpty(path));
        }

        private static string GetRelativeAssetsPath(string path)
        {
            return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
        }
    }
}