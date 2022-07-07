/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace FutureCore.FC_Debuger
{
    public sealed partial class DebuggerConsoleMB : MonoBehaviour
    {
        private sealed class GMLogicWindow : ScrollableDebuggerWindowBase
        {
            private DebuggerConsoleMB m_DebuggerComponent = null;

            private string typeClassName = "ProjectApp.GM.GM_Logic";
            private string inputParam1Str = string.Empty;
            private string inputParam2Str = string.Empty;
            private string inputParam3Str = string.Empty;
            private string gmReturnStr = string.Empty;
            private string gmParamStr = string.Empty;
            private List<GMCommond_Attribute> gmCommonds;

            private int rowBtnNum = 4;
            private int allRowNum = 0;

            private string currCmdDesc = "无";
            private string currCmdKey = "null";

            public override void Initialize(params object[] args)
            {
                m_DebuggerComponent = Debugger;
                if (m_DebuggerComponent == null)
                {
                    LogUtil.LogError("Debugger component is invalid.");
                    return;
                }
            }

            public override void OnUpdate(float elapseSeconds, float realElapseSeconds)
            {
                return;
            }

            protected override void OnDrawScrollableWindow()
            {
                GUILayout.Label("<b>GM命令 · " + typeClassName + " (使用空格分隔参数)</b>");
                GUILayout.BeginVertical("box");
                {
                    GUILayout.Space(2);
                    GUILayout.Label("【操作台: 当前命令: " + currCmdDesc + " - " + currCmdKey + "】", GUILayout.Width(500f));

                    GUILayout.Space(2);
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("输入命令参数:", GUILayout.Width(85f));
                        inputParam1Str = GUILayout.TextField(inputParam1Str, GUILayout.Width(120f));
                        inputParam2Str = GUILayout.TextField(inputParam2Str, GUILayout.Width(120f));
                        inputParam3Str = GUILayout.TextField(inputParam3Str, GUILayout.Width(120f));
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(2);
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("返回命令结果:", GUILayout.Width(85f));
                        GUILayout.TextArea(gmReturnStr, GUILayout.Width(450f));
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(2);
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("命令指令备注:", GUILayout.Width(85f));
                        GUILayout.TextArea(gmParamStr, GUILayout.Width(450f));
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(2);
                    GUILayout.Label("【命令按钮组】", GUILayout.Width(200f));

                    if (gmCommonds == null)
                    {
                        gmCommonds = GMMgr.Instance.GetAllCommondByClass(typeClassName);
                        allRowNum = gmCommonds.Count / rowBtnNum;
                        if (allRowNum == 0 && gmCommonds.Count != 0)
                        {
                            allRowNum = 1;
                        }
                        else if (gmCommonds.Count % rowBtnNum > 0)
                        {
                            allRowNum++;
                        }
                    }

                    int currBtnIdx = 0;
                    for (int i = 0; i < allRowNum; i++)
                    {
                        GUILayout.Space(2);
                        GUILayout.BeginHorizontal();
                        {
                            for (int j = 0; j < rowBtnNum; j++)
                            {
                                if (currBtnIdx >= gmCommonds.Count) continue;

                                GMCommond_Attribute gmc = gmCommonds[currBtnIdx];
                                GUILayout.Space(2);
                                if (GUILayout.Button(gmc.gmDesc, GUILayout.Width(125f), GUILayout.Height(35f)))
                                {
                                    currCmdDesc = gmc.gmDesc;
                                    currCmdKey = gmc.cmdKey;
                                    gmParamStr = gmc.paramDesc;

                                    string allParam = inputParam1Str + " " + inputParam2Str + " " + inputParam3Str;
                                    object res = GMMgr.Instance.CallCommond(currCmdKey, allParam);
                                    if (res != null)
                                    {
                                        string resInfo = res.ToString();
                                        if (!string.IsNullOrWhiteSpace(resInfo))
                                        {
                                            gmReturnStr = resInfo;
                                        }
                                    }
                                }

                                currBtnIdx++;
                            }
                        }
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.Space(2);
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