/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore.FC_Debuger
{
    public sealed partial class DebuggerConsoleMB : MonoBehaviour
    {
        private sealed class SettingsWindow : ScrollableDebuggerWindowBase
        {
            private DebuggerConsoleMB m_DebuggerComponent = null;
            //private SettingComponent m_SettingComponent = null;
            private float m_LastIconX = 0f;
            private float m_LastIconY = 0f;
            private float m_LastWindowX = 0f;
            private float m_LastWindowY = 0f;
            private float m_LastWindowWidth = 0f;
            private float m_LastWindowHeight = 0f;
            private float m_LastWindowScale = 0f;

            public override void Initialize(params object[] args)
            {
                m_DebuggerComponent = Debugger;
                if (m_DebuggerComponent == null)
                {
                    LogUtil.LogError("Debugger component is invalid.");
                    return;
                }

                m_LastIconX = DefaultIconRect.x;
                m_LastIconY = DefaultIconRect.y;
                m_LastWindowX = DefaultWindowRect.x;
                m_LastWindowY = DefaultWindowRect.y;

                m_LastWindowWidth = PlayerPrefs.GetFloat(Key_DebuggerConsoleMB_Width, DefaultWindowRect.width);
                m_LastWindowHeight = PlayerPrefs.GetFloat(Key_DebuggerConsoleMB_Height, DefaultWindowRect.height);

                if (PlayerPrefs.HasKey(Key_DebuggerConsoleMB_WindowScale))
                {
                    m_LastWindowScale = PlayerPrefs.GetFloat(Key_DebuggerConsoleMB_WindowScale, DefaultWindowScale);
                    m_DebuggerComponent.WindowScale = m_LastWindowScale;
                }
                else
                {
                    m_DebuggerComponent.WindowScale = m_LastWindowScale = DefaultWindowScale;
                }

                m_DebuggerComponent.IconRect = new Rect(m_LastIconX, m_LastIconY, DefaultIconRect.width, DefaultIconRect.height);
                m_DebuggerComponent.WindowRect = new Rect(m_LastWindowX, m_LastWindowY, m_LastWindowWidth, m_LastWindowHeight);
            }

            public override void OnUpdate(float elapseSeconds, float realElapseSeconds)
            {
                if (m_LastIconX != m_DebuggerComponent.IconRect.x)
                {
                    m_LastIconX = m_DebuggerComponent.IconRect.x;
                }

                if (m_LastIconY != m_DebuggerComponent.IconRect.y)
                {
                    m_LastIconY = m_DebuggerComponent.IconRect.y;
                }

                if (m_LastWindowX != m_DebuggerComponent.WindowRect.x)
                {
                    m_LastWindowX = m_DebuggerComponent.WindowRect.x;
                }

                if (m_LastWindowY != m_DebuggerComponent.WindowRect.y)
                {
                    m_LastWindowY = m_DebuggerComponent.WindowRect.y;
                }

                if (m_LastWindowWidth != m_DebuggerComponent.WindowRect.width)
                {
                    m_LastWindowWidth = m_DebuggerComponent.WindowRect.width;
                }

                if (m_LastWindowHeight != m_DebuggerComponent.WindowRect.height)
                {
                    m_LastWindowHeight = m_DebuggerComponent.WindowRect.height;
                }

                if (m_LastWindowScale != m_DebuggerComponent.WindowScale)
                {
                    m_LastWindowScale = m_DebuggerComponent.WindowScale;
                }
            }

            protected override void OnDrawScrollableWindow()
            {
                GUILayout.Label("<b>Window Settings</b>");
                GUILayout.BeginVertical("box");
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Position:", GUILayout.Width(60f));
                        GUILayout.Label("Drag window caption to move position.");
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        float width = m_DebuggerComponent.WindowRect.width;
                        GUILayout.Label("Width:", GUILayout.Width(60f));
                        if (GUILayout.RepeatButton("-", GUILayout.Width(30f)))
                        {
                            width--;
                        }
                        width = GUILayout.HorizontalSlider(width, 100f, Screen.width - 20f);
                        if (GUILayout.RepeatButton("+", GUILayout.Width(30f)))
                        {
                            width++;
                        }
                        width = Mathf.Clamp(width, 100f, Screen.width - 20f);
                        if (width != m_DebuggerComponent.WindowRect.width)
                        {
                            PlayerPrefs.SetFloat(Key_DebuggerConsoleMB_Width, width);
                            m_DebuggerComponent.WindowRect = new Rect(m_DebuggerComponent.WindowRect.x, m_DebuggerComponent.WindowRect.y, width, m_DebuggerComponent.WindowRect.height);
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        float height = m_DebuggerComponent.WindowRect.height;
                        GUILayout.Label("Height:", GUILayout.Width(60f));
                        if (GUILayout.RepeatButton("-", GUILayout.Width(30f)))
                        {
                            height--;
                        }
                        height = GUILayout.HorizontalSlider(height, 100f, Screen.height - 20f);
                        if (GUILayout.RepeatButton("+", GUILayout.Width(30f)))
                        {
                            height++;
                        }
                        height = Mathf.Clamp(height, 100f, Screen.height - 20f);
                        if (height != m_DebuggerComponent.WindowRect.height)
                        {
                            PlayerPrefs.SetFloat(Key_DebuggerConsoleMB_Height, height);
                            m_DebuggerComponent.WindowRect = new Rect(m_DebuggerComponent.WindowRect.x, m_DebuggerComponent.WindowRect.y, m_DebuggerComponent.WindowRect.width, height);
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        float scale = m_DebuggerComponent.WindowScale;
                        GUILayout.Label("Scale:", GUILayout.Width(60f));
                        if (GUILayout.RepeatButton("-", GUILayout.Width(30f)))
                        {
                            scale -= 0.01f;
                        }
                        scale = GUILayout.HorizontalSlider(scale, 0.5f, 4f);
                        if (GUILayout.RepeatButton("+", GUILayout.Width(30f)))
                        {
                            scale += 0.01f;
                        }
                        scale = Mathf.Clamp(scale, 0.5f, 4f);
                        if (scale != m_DebuggerComponent.WindowScale)
                        {
                            m_DebuggerComponent.WindowScale = scale;
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("0.5x", GUILayout.Height(60f)))
                        {
                            m_DebuggerComponent.WindowScale = 0.5f;
                        }
                        if (GUILayout.Button("1.0x", GUILayout.Height(60f)))
                        {
                            m_DebuggerComponent.WindowScale = 1f;
                        }
                        if (GUILayout.Button("1.5x", GUILayout.Height(60f)))
                        {
                            m_DebuggerComponent.WindowScale = 1.5f;
                        }
                        if (GUILayout.Button("2.0x", GUILayout.Height(60f)))
                        {
                            m_DebuggerComponent.WindowScale = 2f;
                        }
                        if (GUILayout.Button("2.5x", GUILayout.Height(60f)))
                        {
                            m_DebuggerComponent.WindowScale = 2.5f;
                        }
                        if (GUILayout.Button("3.0x", GUILayout.Height(60f)))
                        {
                            m_DebuggerComponent.WindowScale = 3f;
                        }
                        if (GUILayout.Button("3.5x", GUILayout.Height(60f)))
                        {
                            m_DebuggerComponent.WindowScale = 3.5f;
                        }
                        if (GUILayout.Button("4.0x", GUILayout.Height(60f)))
                        {
                            m_DebuggerComponent.WindowScale = 4f;
                        }
                    }
                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Reset Layout", GUILayout.Height(30f)))
                    {
                        PlayerPrefs.DeleteKey(Key_DebuggerConsoleMB_Width);
                        PlayerPrefs.DeleteKey(Key_DebuggerConsoleMB_Height);
                        PlayerPrefs.DeleteKey(Key_DebuggerConsoleMB_WindowScale);
                        m_DebuggerComponent.ResetLayout();
                    }

                    GUILayout.Space(15);
                    GUILayout.Label("【常驻命令按钮组】", GUILayout.Width(200f));
                    if (GUILayout.Button("Open Reporter", GUILayout.Height(30f)))
                    {
                        GameObject obj = GameObject.Find("[FutureFrame]/[Launcher]/Reporter");
                        if (obj)
                        {
                            Reporter reporter = obj.GetComponent<Reporter>();
                            if (reporter)
                            {
                                reporter.DoShow();
                            }
                        }
                    }
                    GUILayout.Space(5);
                    if (GUILayout.Button("Hide Reporter", GUILayout.Height(30f)))
                    {
                        GameObject obj = GameObject.Find("[FutureFrame]/[Launcher]/Reporter");
                        if (obj)
                        {
                            Reporter reporter = obj.GetComponent<Reporter>();
                            if (reporter)
                            {
                                reporter.DoHide();
                            }
                        }
                    }
                    GUILayout.Space(5);
                    if (GUILayout.Button("Minimize Debugger", GUILayout.Height(30f)))
                    {
                        Debugger.SetIsShowFullWindow(false);
                    }
                    GUILayout.Space(5);
                    if (GUILayout.Button("Exit Debugger", GUILayout.Height(30f)))
                    {
                        Debugger.enabled = false;
                    }
                    GUILayout.Space(2);
                }
                GUILayout.EndVertical();
            }
        }
    }
}