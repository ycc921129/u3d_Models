/*
 Author:du
 Time:2017.11.8
*/

using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class EnginePrefsTool
    {
        [MenuItem("[FC Toolkit]/EnginePrefs/删除全部PlayerPrefs", false, 0)]
        private static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("删除全部PlayerPrefs完成");
        }

        [MenuItem("[FC Toolkit]/EnginePrefs/删除全部EditorPrefs", false, 1)]
        private static void DeleteAllEditorPrefs()
        {
            EditorPrefs.DeleteAll();
            Debug.Log("删除全部EditorPrefs完成");
        }

        [MenuItem("[FC Toolkit]/EnginePrefs/删除指定EditorPrefs", false, 1)]
        private static void DeleteEditorPrefsKey()
        {
            string key = "xxx";
            EditorPrefs.DeleteKey(key);
            Debug.Log("删除全部EditorPrefs完成");
        }
    }
}