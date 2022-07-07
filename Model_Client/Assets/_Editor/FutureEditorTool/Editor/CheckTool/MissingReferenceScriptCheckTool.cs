using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class MissingReferenceScriptCheckTool
    {
        [MenuItem("[FC Toolkit]/Check/检查场景脚本变量引用丢失", false, 0)]
        private static void Check()
        {
            GameObject[] gos = Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in gos)
            {
                CheckMissingReferenceScript(go);
            }
            Debug.Log("检查完成");
        }

        private static void CheckMissingReferenceScript(GameObject go)
        {
            if (null == go) return;
            MonoBehaviour[] scripts = go.GetComponentsInChildren<MonoBehaviour>(true);
            if (null != scripts)
            {
                string tips = string.Empty;
                for (int i = 0; i < scripts.Length; i++)
                {
                    MonoBehaviour mono = scripts[i];
                    if (null == mono) continue;
                    SerializedObject tempObject = new SerializedObject(mono);
                    SerializedProperty temProperty = tempObject.GetIterator();
                    bool isHas = false;
                    string nullField = null;
                    while (temProperty.NextVisible(true))
                    {
                        if (temProperty.propertyType == SerializedPropertyType.ObjectReference
                            && temProperty.objectReferenceValue == null
                            && temProperty.objectReferenceInstanceIDValue != 0)
                        {
                            isHas = true;
                            nullField = temProperty.propertyPath;
                            tips += mono.GetType().ToString() + " | " + temProperty.propertyPath + ": 变量引用丢失\t\n";
                        }
                    }
                    if (isHas)
                    {
                        Debug.LogError("此脚本有变量引用丢失: " + mono.GetType() + " | " + nullField, mono);
                    }
                }
                if (!string.IsNullOrEmpty(tips))
                {
                    EditorUtility.DisplayDialog("脚本变量引用丢失", tips, "确定");
                }
            }
        }
    }
}