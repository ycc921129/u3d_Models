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
            if (GUILayout.Button(new GUIContent("打表目录", "打开配置表打表目录"), ToolbarStyles.Command4ButtonStyle))
            {
                ConfigBatTool.OpenConfigDir();
                return;
            }
            if (GUILayout.Button(new GUIContent("测服打表", "测试服自动化打表"), ToolbarStyles.Command4ButtonStyle))
            {
                if (EditorUtility.DisplayDialog("【导航键】测服打表", "是否进行测试服自动化打表！", "确认", "取消"))
                {
                    ConfigBatTool.SyncConfigDoTestConfig();
                    return;
                }
                return;
            }
            if (GUILayout.Button(new GUIContent("正服打表", "正式服自动化打表"), ToolbarStyles.Command4ButtonStyle))
            {
                if(EditorUtility.DisplayDialog("【导航键】正服打表", "是否进行正式服自动化打表！", "确认", "取消"))
                {
                    ConfigBatTool.SyncConfigDoReleaseConfig();
                    return;
                }
                return;
            }

            GUI.color = ToolbarStyles.Color_in;
            if (GUILayout.Button(new GUIContent("界面IDE", "打开FGUI软件"), ToolbarStyles.Command4ButtonStyle))
            {
                // 打开文件夹
                FGUISVNVersionTool.OpenFolder();
                // 打开IDE
                FGUIOpenIDETool.OpenIDE();
                // 打开Fgui外部生成工具
                FGUIContollerCreateTool_v2.OpenTool();
                return;
            }
            if (GUILayout.Button(new GUIContent("更新界面", "更新美术SVN目录界面"), ToolbarStyles.Command4ButtonStyle))
            {
                if (EditorUtility.DisplayDialog("【导航键】更新界面", "是否进行更新美术SVN目录界面！", "确认", "取消"))
                {
                    FGUISVNVersionTool.UpdateSVN();
                    return;
                }
                return;
            }
            if (GUILayout.Button(new GUIContent("提交界面", "提交美术SVN目录界面"), ToolbarStyles.Command4ButtonStyle))
            {
                if (EditorUtility.DisplayDialog("【导航键】提交界面", "是否进行提交美术SVN目录界面！", "确认", "取消"))
                {
                    FGUISVNVersionTool.CommitSVN();
                    return;
                }
                return;
            }

            GUI.color = tempColor;
            if (GUILayout.Button(new GUIContent("策", "打开策划SVN目录"), ToolbarStyles.Command1ButtonStyle))
            {
                AppTool.OpenProjectFolder();
                AppTool.OpenSVNFolder();
                return;
            }
            if (GUILayout.Button(new GUIContent("刷", "刷新工程"), ToolbarStyles.Command1ButtonStyle))
            {
                AppTool.RefreshProject();
                return;
            }
            if (GUILayout.Button(new GUIContent("P", "运行项目"), ToolbarStyles.Command1ButtonStyle))
            {
                EditorApplication.isPlaying = !EditorApplication.isPlaying;
                return;
            }

            GUI.color = tempColor;
        }
    }
}