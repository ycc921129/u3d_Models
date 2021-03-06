using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace FutureEditor
{
    [InitializeOnLoad]
    public static class Unity3dRightButtonExtender
    {
        private static class SceneHelper
        {
            static string sceneToOpen;

            public static void StartScene(string scene)
            {
                if (EditorApplication.isPlaying)
                {
                    EditorApplication.isPlaying = false;
                }

                sceneToOpen = scene;
                EditorApplication.update += OnUpdate;
            }

            static void OnUpdate()
            {
                if (sceneToOpen == null ||
                    EditorApplication.isPlaying || EditorApplication.isPaused ||
                    EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    return;
                }

                EditorApplication.update -= OnUpdate;

                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(sceneToOpen);
                    EditorApplication.isPlaying = false;
                }
                sceneToOpen = null;
            }
        }

        private static bool m_Enabled = false;
        private static bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                m_Enabled = value;
                EditorPrefs.SetBool("SceneViewFocuser", value);
            }
        }

        static Unity3dRightButtonExtender()
        {
            m_Enabled = EditorPrefs.GetBool("SceneViewFocuser", false);
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
            EditorApplication.pauseStateChanged += OnPauseChanged;

            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnPauseChanged(PauseState obj)
        {
            if (Enabled && obj == PauseState.Unpaused)
            {
                // Not sure why, but this must be delayed
                EditorApplication.delayCall += EditorWindow.FocusWindowIfItsOpen<SceneView>;
            }
        }

        private static void OnPlayModeChanged(PlayModeStateChange obj)
        {
            if (Enabled && obj == PlayModeStateChange.EnteredPlayMode)
            {
                EditorWindow.FocusWindowIfItsOpen<SceneView>();
            }
        }

        private static void OnToolbarGUI()
        {
            if (EditorApplication.isCompiling) return;
            if (Event.current.type == EventType.KeyDown || Event.current.type == EventType.KeyUp) return;

            Color tempColor = GUI.color;

            // GUI.color = Color.white;
            if (GUILayout.Button(new GUIContent("M", "Enter Main Scene / Ping To Raw Object"), ToolbarStyles.Command1ButtonStyle))
            {
                if (!EditorApplication.isPlaying)
                {
                    SceneHelper.StartScene(AppEditorInfo.MainScenePath);
                }
                else
                {
                    PingObjectTool.PingRawObject();
                }
                return;
            }
            if (GUILayout.Button(new GUIContent("T", "Enter Test Scene / Ping To TestMgr Object"), ToolbarStyles.Command1ButtonStyle))
            {
                if (!EditorApplication.isPlaying)
                {
                    SceneHelper.StartScene(AppEditorInfo.TestScenePath);
                }
                else
                {
                    PingObjectTool.PingTestMgrObject();
                }
                return;
            }

            GUI.changed = false;
            Texture tex = EditorGUIUtility.IconContent(@"UnityEditor.SceneView").image;
            //GUILayout.Toggle(m_enabled, new GUIContent(null, tex, "Focus SceneView when entering play mode"), "Command");
            GUILayout.Toggle(m_Enabled, new GUIContent(null, tex, "??????????????????????????? / Ping To Camera Object"), "Command");
            if (GUI.changed)
            {
                if (!EditorApplication.isPlaying)
                {
                    Enabled = !Enabled;
                }
                else
                {
                    PingObjectTool.PingCameraObject();
                }
            }
            GUI.changed = false;

            GUI.color = ToolbarStyles.Color_in;
            if (GUILayout.Button(new GUIContent("?????????", "??????????????????"), ToolbarStyles.Command3ButtonStyle))
            {
                ClearDataTool.ClearPlayerData();
                return;
            }
            if (GUILayout.Button(new GUIContent("?????????", "??????????????????"), ToolbarStyles.Command3ButtonStyle))
            {
                ProjectAutoRegisterTool_v2.AutoRegisterAll();
                return;
            }
            if (GUILayout.Button(new GUIContent("?????????", ProjectAutomationTool.GetAutomationInfo()), ToolbarStyles.Command3ButtonStyle))
            {
                if (EditorUtility.DisplayDialog("????????????????????????", "????????????????????????", "??????", "??????"))
                {
                    ProjectAutomationTool.DoAutomation();
                    return;
                }
                return;
            }

            GUI.color = ToolbarStyles.Color_out;
            if (GUILayout.Button(new GUIContent("??????", "??????????????????"), ToolbarStyles.Command2ButtonStyle))
            {
                AppTool.SetApplicationIdentifier();
                AppTool.OpenProjectSettings();
                OpenFolderTool.OpenPersistentDataFolder();
                return;
            }
            if (GUILayout.Button(new GUIContent("??????APK", "????????????????????????APK"), ToolbarStyles.Command5ButtonStyle))
            {
                if (!EditorApplication.isPlaying)
                {
                    if (EditorUtility.DisplayDialog("?????????????????????APK", "????????????????????????????????????APK???", "??????", "??????"))
                    {
                        BuildProjectApkClientTool.AutoBuildProjectDebugAndReleaseApk();
                        return;
                    }
                }
                return;
            }
            if (GUILayout.Button(new GUIContent("??????IPA", "iOS????????????Xcode??????"), ToolbarStyles.Command5ButtonStyle))
            {
                if (!EditorApplication.isPlaying)
                {
                    if (EditorUtility.DisplayDialog("?????????????????????IPA", "????????????iOS????????????Xcode?????????", "??????", "??????"))
                    {
                        BuildProjectIpaClientTool.CopyToXcode();
                        return;
                    }
                }
                return;
            }
            if (GUILayout.Button(new GUIContent("??????EXE", "Windows??????EXE??????"), ToolbarStyles.Command5ButtonStyle))
            {
                if (!EditorApplication.isPlaying)
                {
                    if (EditorUtility.DisplayDialog("?????????????????????EXE", "????????????Windows??????EXE?????????", "??????", "??????"))
                    {
                        BuildExeClientTool.BuildLocalDebugExe();
                        return;
                    }
                }
                return;
            }

            GUI.color = tempColor;
        }

        private static class PingObjectTool
        {
            private static GameObject RawObject = null;
            public static void PingRawObject()
            {
                if (RawObject != null)
                {
                    EditorGUIUtility.PingObject(RawObject);
                    Selection.activeObject = RawObject;
                }
            }

            private static int LastTestMgrIdx = -1;
            public static void PingTestMgrObject()
            {
                if (Selection.activeObject != null)
                {
                    if (Selection.activeObject is GameObject)
                    {
                        GameObject rawObj = Selection.activeObject as GameObject;
                        if (!rawObj.name.StartsWith("TestMgr"))
                        {
                            RawObject = rawObj;
                        }
                    }
                }

                List<GameObject> objs = new List<GameObject>();
                GameObject root = GameObject.Find("[MonoManager]");
                if (root != null)
                {
                    foreach (Transform item in root.transform)
                    {
                        if (item.gameObject.name.StartsWith("TestMgr"))
                        {
                            objs.Add(item.gameObject);
                        }
                    }
                }

                if (objs.Count != 0)
                {
                    LastTestMgrIdx++;
                    if (LastTestMgrIdx >= objs.Count)
                    {
                        LastTestMgrIdx = 0;
                    }
                    GameObject curr = objs[LastTestMgrIdx];

                    EditorGUIUtility.PingObject(curr);
                    Selection.activeObject = curr;
                }
            }

            private static int LastCameraIdx = -1;
            public static void PingCameraObject()
            {
                SceneView view = SceneView.lastActiveSceneView;
                Camera cameraObj = null;
                if (view != null)
                {
                    GameObject root = GameObject.Find("[Camera]");
                    if (root != null)
                    {
                        Camera[] cameras = root.GetComponentsInChildren<Camera>();
                        if (cameras.Length <= 0)
                        {
                            return;
                        }
                        else if (cameras.Length == 1)
                        {
                            LastCameraIdx = 0;
                        }
                        else
                        {
                            LastCameraIdx++;
                            if (LastCameraIdx >= cameras.Length)
                            {
                                LastCameraIdx = 0;
                            }
                        }
                        cameraObj = cameras[LastCameraIdx];
                    }

                    if (cameraObj != null)
                    {
                        view.LookAt(cameraObj.transform.position, cameraObj.transform.rotation, 25);

                        EditorGUIUtility.PingObject(cameraObj);
                        Selection.activeObject = cameraObj;
                    }
                }
            }
        }
    }
}