using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

namespace FutureEditor
{
    public static class ToolbarStyles
    {
        public static readonly GUIStyle Command1ButtonStyle;
        public static readonly GUIStyle Command2ButtonStyle;
        public static readonly GUIStyle Command3ButtonStyle;
        public static readonly GUIStyle Command4ButtonStyle;
        public static readonly GUIStyle Command5ButtonStyle;

        public static readonly Color Color_in = new Color(0.7960784f, 0.9607843f, 0.9843137f);
        public static readonly Color Color_out = new Color(0.5f, 0.9882354f, 1f);

        static ToolbarStyles()
        {
            Command1ButtonStyle = new GUIStyle("Command")
            {
                fontSize = 12,
                fixedWidth = 30 - 4,
                fixedHeight = 22,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold,
            };
            Command2ButtonStyle = new GUIStyle("Command")
            {
                fontSize = 12,
                fixedWidth = 40 - 4,
                fixedHeight = 22,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold,
            };
            Command3ButtonStyle = new GUIStyle("Command")
            {
                fontSize = 12,
                fixedWidth = 50 - 4,
                fixedHeight = 22,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold,
            };
            Command4ButtonStyle = new GUIStyle("Command")
            {
                fontSize = 12,
                fixedWidth = 60 - 4,
                fixedHeight = 22,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold,
            };
            Command5ButtonStyle = new GUIStyle("Command")
            {
                fontSize = 12,
                fixedWidth = 70 - 4,
                fixedHeight = 22,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold,
            };
        }
    }

    [InitializeOnLoad]
    public class Unity3dLeftButtonExtender
    {
        static Unity3dLeftButtonExtender()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            if (EditorApplication.isCompiling) return;
            if (Event.current.type == EventType.KeyDown || Event.current.type == EventType.KeyUp) return;

            Color tempColor = GUI.color;
            GUILayout.FlexibleSpace();

            GUI.color = ToolbarStyles.Color_out;
            if (GUILayout.Button(new GUIContent("????????????", "???????????????????????????"), ToolbarStyles.Command4ButtonStyle))
            {
                ConfigBatTool.OpenConfigDir();
                return;
            }
            if (GUILayout.Button(new GUIContent("????????????", "????????????????????????"), ToolbarStyles.Command4ButtonStyle))
            {
                if (EditorUtility.DisplayDialog("???????????????????????????", "???????????????????????????????????????", "??????", "??????"))
                {
                    ConfigBatTool.SyncConfigDoTestConfig();
                    return;
                }
                return;
            }
            if (GUILayout.Button(new GUIContent("????????????", "????????????????????????"), ToolbarStyles.Command4ButtonStyle))
            {
                if(EditorUtility.DisplayDialog("???????????????????????????", "???????????????????????????????????????", "??????", "??????"))
                {
                    ConfigBatTool.SyncConfigDoReleaseConfig();
                    return;
                }
                return;
            }

            GUI.color = ToolbarStyles.Color_in;
            if (GUILayout.Button(new GUIContent("??????IDE", "??????FGUI??????"), ToolbarStyles.Command4ButtonStyle))
            {
                // ???????????????
                FGUISVNVersionTool.OpenFolder();
                // ??????IDE
                FGUIOpenIDETool.OpenIDE();
                // ??????Fgui??????????????????
                FGUIContollerCreateTool_v2.OpenTool();
                return;
            }
            if (GUILayout.Button(new GUIContent("????????????", "????????????SVN????????????"), ToolbarStyles.Command4ButtonStyle))
            {
                if (EditorUtility.DisplayDialog("???????????????????????????", "????????????????????????SVN???????????????", "??????", "??????"))
                {
                    FGUISVNVersionTool.UpdateSVN();
                    return;
                }
                return;
            }
            if (GUILayout.Button(new GUIContent("????????????", "????????????SVN????????????"), ToolbarStyles.Command4ButtonStyle))
            {
                if (EditorUtility.DisplayDialog("???????????????????????????", "????????????????????????SVN???????????????", "??????", "??????"))
                {
                    FGUISVNVersionTool.CommitSVN();
                    return;
                }
                return;
            }

            GUI.color = tempColor;
            if (GUILayout.Button(new GUIContent("???", "????????????SVN??????"), ToolbarStyles.Command1ButtonStyle))
            {
                AppTool.OpenProjectFolder();
                AppTool.OpenSVNFolder();
                return;
            }
            if (GUILayout.Button(new GUIContent("???", "????????????"), ToolbarStyles.Command1ButtonStyle))
            {
                AppTool.RefreshProject();
                return;
            }
            if (GUILayout.Button(new GUIContent("P", "????????????"), ToolbarStyles.Command1ButtonStyle))
            {
                EditorApplication.isPlaying = !EditorApplication.isPlaying;
                return;
            }

            GUI.color = tempColor;
        }
    }
}