using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class MissingScriptsCheckTool
    {
        [MenuItem("[FC Toolkit]/Check/检查场景脚本丢失", false, 0)]
        public static void CheckMissingScript()
        {
            GameObject[] gos = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in gos)
            {
                Debug.LogFormat("检查: {0}", go.name);
                Component[] components = go.GetComponents<Component>();
                foreach (Component component in components)
                {
                    if (component == null)
                    {
                        Debug.LogFormat("发现: {0} : {1}", go.name, component.GetType().Name);
                    }
                }
            }
            AssetDatabase.Refresh();
            Debug.Log("检查完成");
        }

        [MenuItem("[FC Toolkit]/Check/移除场景脚本丢失", false, 1)]
        public static void RemoveMissingScript()
        {
            GameObject[] gos = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in gos)
            {
                SerializedObject so = new SerializedObject(go);
                SerializedProperty soProperties = so.FindProperty("m_Component");
                Component[] components = go.GetComponents<Component>();
                int propertyIndex = 0;
                foreach (Component component in components)
                {
                    if (component == null)
                    {
                        Debug.LogFormat("移除: {0} : {1}", go.name, component.GetType().Name);
                        soProperties.DeleteArrayElementAtIndex(propertyIndex);
                    }
                    ++propertyIndex;
                }
                so.ApplyModifiedProperties();
            }
            AssetDatabase.Refresh();
            Debug.Log("移除完成");
        }

        [MenuItem("[FC Toolkit]/Check/选择目录检查脚本丢失", false, 2)]
        public static void SelectDirCheckMissingScript()
        {
            UnityEngine.Object[] selections = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
            if (selections != null && selections.Length > 0)
            {
                foreach (UnityEngine.Object obj in selections)
                {
                    string selectPath = AssetDatabase.GetAssetPath(obj);
                    if (Directory.Exists(selectPath))
                    {
                        FindPrefab(selectPath);
                    }
                    else
                    {
                        Debug.Log("请选择文件夹");
                    }
                }
            }
            Resources.UnloadUnusedAssets();
            GC.Collect();
            AssetDatabase.Refresh();
            Debug.Log("检查完成");
        }

        private static void FindPrefab(string selectPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(selectPath);
            FileInfo[] prefabs = dirInfo.GetFiles("*.prefab");
            for (int i = 0; i < prefabs.Length; i++)
            {
                string assetPath = PathTool.FilePathToAssetPath(prefabs[i].FullName);
                CheckPrefab(assetPath);
            }
            string[] juniorDirs = Directory.GetDirectories(dirInfo.FullName);
            foreach (string juniorDir in juniorDirs)
            {
                FindPrefab(juniorDir);
            }
        }

        private static void CheckPrefab(string assetPath)
        {
            GameObject go = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;
            Debug.LogFormat("检查: {0}", go.name);
            Component[] components = go.GetComponentsInChildren<Component>();
            foreach (Component component in components)
            {
                if (component == null)
                {
                    Debug.LogErrorFormat("发现: {0}", go.name);
                }
            }
            go = null;
        }
    }
}