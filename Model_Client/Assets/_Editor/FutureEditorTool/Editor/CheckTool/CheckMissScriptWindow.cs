using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public class CheckMissScriptWindow : EditorWindow
    {
        private Vector2 scrollPos = Vector2.zero;
        private List<string> pathList = new List<string>();
        private List<GameObject> missObjList = new List<GameObject>();

        [MenuItem("[FC Toolkit]/Check/检查脚本丢失窗口", false, 3)]
        public static void DeleteMissScript()
        {
            GetWindow<CheckMissScriptWindow>("检查脚本丢失窗口").Show();
        }

        private void CleanupMissingScripts()
        {
            foreach (GameObject obj in missObjList)
            {
                Component[] components = obj.GetComponents<Component>();
                SerializedObject serializedObject = new SerializedObject(obj);
                SerializedProperty prop = serializedObject.FindProperty("m_Component");
                int r = 0;
                for (int j = 0; j < components.Length; j++)
                {
                    if (components[j] == null)
                    {
                        prop.DeleteArrayElementAtIndex(j - r);
                        r++;
                    }
                }
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(obj);
            }
        }

        private void CheckMissingScripts()
        {
            List<string> withoutExtensions = new List<string>() { ".prefab" };
            string[] files = Directory.GetFiles(Application.dataPath, "*.prefab", SearchOption.AllDirectories)
                .Where(s => withoutExtensions.Contains(Path.GetExtension(s).ToLower())).ToArray();

            int count = 0;
            foreach (string fileName in files)
            {
                count++;
                EditorUtility.DisplayProgressBar("Processing...", "搜寻所有Prefab....", count / files.Length);
                string fixedFileName = fileName.Replace("\\", "/");
                int index = fixedFileName.IndexOf("Assets");
                fixedFileName = fixedFileName.Substring(index);
                pathList.Add(fixedFileName);
            }
            EditorUtility.ClearProgressBar();

            int count2 = 0;
            foreach (string path in pathList)
            {
                count2++;
                EditorUtility.DisplayProgressBar("Processing...", "遍历丢失脚本Prefab中....", count / pathList.Count);
                GameObject obj = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
                GetAllChild(obj);
            }
            EditorUtility.ClearProgressBar();
        }

        private void GetAllChild(GameObject obj)
        {
            CheckGameObject(obj);
            GameObject[] childArr = GetChildrenGameObject(obj);
            foreach (GameObject child in childArr)
            {
                CheckGameObject(child);
                GetAllChild(child);
            }
        }

        private void CheckGameObject(GameObject go)
        {
            Component[] components = go.GetComponents<Component>();
            for (int j = 0; j < components.Length; j++)
            {
                if (components[j] == null)
                {
                    missObjList.Add(go);
                }
            }
        }

        private void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            //if (GUILayout.Button("删除所有Prefab上的Miss脚本", GUILayout.Width(300)))
            //{
            //    CleanupMissingScripts();
            //}
            EditorGUILayout.Space();
            if (GUILayout.Button("检查Prefab脚本Miss情况", GUILayout.Height(30)))
            {
                pathList = new List<string>();
                missObjList = new List<GameObject>();
                CheckMissingScripts();
            }
            EditorGUILayout.Space();
            if (GUILayout.Button("打印脚本丢失预设路径", GUILayout.Height(30)))
            {
                foreach (GameObject obj in missObjList)
                {
                    string path = AssetDatabase.GetAssetPath(obj);
                    Debug.LogError(path);
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            foreach (GameObject obj in missObjList)
            {
                EditorGUILayout.ObjectField(obj, typeof(GameObject), false);
            }

            EditorGUILayout.EndScrollView();
        }

        public GameObject[] GetChildrenGameObject(GameObject go)
        {
            List<GameObject> list = new List<GameObject>();
            foreach (Transform child in go.transform)
            {
                list.Add(child.gameObject);
            }
            return list.ToArray();
        }
    }
}