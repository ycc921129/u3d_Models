using UnityEngine;
using UnityEditor;

namespace FutureEditor
{
    public class GUIStyleViewerWindow : EditorWindow
    {
        private Vector2 scrollPosition = Vector2.zero;
        private string search = string.Empty;

        [MenuItem("[FC Toolkit]/GUI/浏览GUI样式窗口", false, 0)]
        public static void Init()
        {
            GetWindow(typeof(GUIStyleViewerWindow));
        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal("HelpBox");
            GUILayout.Label("单击示例可复制其名到剪贴板", "label");
            GUILayout.FlexibleSpace();
            GUILayout.Label("查找:");
            search = EditorGUILayout.TextField(search);
            GUILayout.EndHorizontal();

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            foreach (GUIStyle style in GUI.skin)
            {
                if (style.name.ToLower().Contains(search.ToLower()))
                {
                    GUILayout.BeginHorizontal("PopupCurveSwatchBackground");
                    GUILayout.Space(7);
                    if (GUILayout.Button(style.name, style))
                    {
                        EditorGUIUtility.systemCopyBuffer = "\"" + style.name + "\"";
                    }
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.SelectableLabel("\"" + style.name + "\"");
                    GUILayout.EndHorizontal();
                    GUILayout.Space(11);
                }
            }
            GUILayout.EndScrollView();
        }
    }
}