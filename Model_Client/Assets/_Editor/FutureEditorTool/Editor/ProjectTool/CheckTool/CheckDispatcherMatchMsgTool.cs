using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class CheckDispatcherMatchMsgTool
    {
        private static List<string> MatchList = new List<string>
        {
            "AppDispatcher.Instance",
            "appDispatcher.",

            "MainThreadDispatcher.Instance.",
            "mainThreadDispatcher.",

            "ChannelDispatcher.Instance.",
            "channelDispatcher.",

            "ModelDispatcher.Instance.",
            "modelDispatcher.",

            "ViewDispatcher.Instance.",
            "viewDispatcher.",

            "CtrlDispatcher.Instance.",
            "ctrlDispatcher.",

            "UICtrlDispatcher.Instance.",
            "uiCtrlDispatcher.",

            "GameDispatcher.Instance.",
            "gameDispatcher.",
        };

        private static Dictionary<string, string> RegexMap = new Dictionary<string, string>()
        {
            {"AppDispatcher", "AppDispatcher.*[(]AppMsg.*"},
            {"appDispatcher", "appDispatcher.*[(]AppMsg.*"},

            {"MainThreadDispatcher", "MainThreadDispatcher.*[(]MainThreadMsg.*"},
            {"mainThreadDispatcher", "mainThreadDispatcher.*[(]MainThreadMsg.*"},

            {"ChannelDispatcher", "ChannelDispatcher.*[(]Channel?(Raw|.)Msg.*"},
            {"channelDispatcher", "channelDispatcher.*[(]Channel?(Raw|.)Msg.*"},

            {"ModelDispatcher", "ModelDispatcher.*[(]ModelMsg.*"},
            {"modelDispatcher", "modelDispatcher.*[(]ModelMsg.*"},

            {"ViewDispatcher", "ViewDispatcher.*[(]ModelMsg.*"},
            {"viewDispatcher", "viewDispatcher.*[(]ModelMsg.*"},

            {"CtrlDispatcher", "CtrlDispatcher.*[(](CtrlMsg.*|msgList.*)"},
            {"ctrlDispatcher", "ctrlDispatcher.*[(](CtrlMsg.*|msgList.*)"},

            {"UICtrlDispatcher", "UICtrlDispatcher.*[(](UICtrlMsg.*|msgList.*)"},
            {"uiCtrlDispatcher", "uiCtrlDispatcher.*[(](UICtrlMsg.*|msgList.*)"},

            {"GameDispatcher", "GameDispatcher.*[(]GameMsg.*"},
            {"gameDispatcher", "gameDispatcher.*[(]GameMsg.*"},
        };

        [MenuItem("[FC Project]/Check/检查派发器消息匹配", false, 0)]
        public static void CheckDispatcherMatchMsg()
        {
            Debug.Log("[CheckDispatcherMatchMsgTool]开始检查: 检查派发器消息匹配");

            AutoRegisterSettings asset = RegisterSettingsAssetTool.GetAutoRegisterSetting();
            for (int i = 0; i < asset.autoRegisterPath.Count; i++)
            {
                string path = AssetDatabase.GetAssetPath(asset.autoRegisterPath[i]);
                DoCheckDispatcherMatchMsg(path);
            }

            Debug.Log("[CheckDispatcherMatchMsgTool]检查结束: 检查派发器消息匹配");
        }

        private static void DoCheckDispatcherMatchMsg(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.GetDirectories().Length != 0)
                {
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < directories.Length; i++)
                    {
                        DoCheckDispatcherMatchMsg(directories[i].FullName);
                    }
                }

                FileInfo[] files = directory.GetFiles();
                for (int j = 0; j < files.Length; j++)
                {
                    FileInfo file = files[j];
                    string filePath = file.FullName;
                    if (!file.Name.EndsWith(".cs")) continue;

                    string Name = file.Name.Split('.')[0];
                    if (Name.EndsWith("Scene")
                        || (!Name.EndsWith("VOModel") && Name.EndsWith("Model"))
                        || Name.EndsWith("Ctrl")
                        || Name.EndsWith("UICtrl")
                        || Name.EndsWith("GCtrl")
                        || Name.EndsWith("UI"))
                    {
                        StreamReader content = file.OpenText();
                        string line = null;
                        while ((line = content.ReadLine()) != null)
                        {
                            foreach (var match in MatchList)
                            {
                                if (!line.Contains(match)) continue;
                                if (line.Trim().StartsWith("//")) continue;

                                string key = line.Split('.')[0];
                                key = Regex.Replace(key, "[^a-zA-Z]", string.Empty).Trim();

                                try
                                {
                                    if (!RegexMap.ContainsKey(key)) continue;
                                    if (line.Contains("msgId") || line.Contains("MsgId")) continue;
                                    if (line.Contains("msg") || line.Contains("Msg")) continue;

                                    string pattern = RegexMap[key];
                                    if (!Regex.IsMatch(line, pattern))
                                    {
                                        Debug.LogError("[CheckDispatcherMatchMsgTool]派发器消息匹配错误\n" + filePath + "\n" + line);
                                        break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Debug.LogError("[CheckDispatcherMatchMsgTool]" + filePath + ":\n" + e);
                                }
                            }
                        }
                        content.Close();
                        content.Dispose();
                    }
                }
            }
        }
    }
}