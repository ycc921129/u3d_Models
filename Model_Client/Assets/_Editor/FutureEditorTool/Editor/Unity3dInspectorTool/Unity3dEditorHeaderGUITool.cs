using UnityEngine;
using UnityEditor;

namespace FutureEditor
{
    public static class Unity3dEditorHeaderGUITool
    {
        [InitializeOnLoadMethod]
        private static void Init()
        {
            Editor.finishedDefaultHeaderGUI += editor =>
            {
                if (editor.target != null)
                {
                    GameObject go = editor.target as GameObject;
                    if (go == null)
                    {
                        return;
                    }
                }

                if (editor.targets == null || editor.targets.Length == 0) return;
                Object[] gos = editor.targets;
                if (gos == null || gos.Length == 0) return;

                if (GUILayout.Button("Reset Transform"))
                {
                    for (int i = 0; i < gos.Length; i++)
                    {
                        Object obj = gos[i];
                        if (!obj) return;
                        GameObject go = gos[i] as GameObject;
                        if (!go) return;
                        Transform tf = go.GetComponent<Transform>();
                        if (!tf) return;

                        if (tf.localPosition != Vector3.zero)
                        {
                            tf.localPosition = Vector3.zero;
                        }
                        if (tf.localScale != Vector3.one)
                        {
                            tf.localScale = Vector3.one;
                        }
                        if (tf.localEulerAngles != Vector3.zero)
                        {
                            tf.localEulerAngles = Vector3.zero;
                        }
                    }
                }
            };
        }
    }
}