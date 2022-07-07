using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class PreferencesMgrAutoRegisterTool_v2
    {
        /// <summary>
        /// 注册快速保存列表
        /// </summary>
        private static List<string> addQuickSave_List = new List<string>
        {
            // 收集检查安装包
            "hacker_pgs",
        };

        /// <summary>
        /// 注册即时发送保存列表
        /// </summary>
        private static List<string> immediateSend_List = new List<string>
        {
        };

        /// <summary>
        /// 注册静默保存列表
        /// </summary>
        private static List<string> quiesce_List = new List<string>
        {
            // 数据版本号
            "data_ver",
        };

        /// <summary>
        /// 注册加密值类型列表
        /// </summary>
        private static List<string> cryption_List = new List<string>
        {
            // 金币
            "coin",
            // 累积金币
            "acc_coin",
        };

        /// <summary>
        /// 注册大数值类型列表
        /// </summary>
        private static List<string> bigNumber_List = new List<string>
        {
            // 字符串金币
            "_coin",
            // 累积字符串金币
            "_acc_coin",
        };

        private class FieldInfo
        {
            public string name;
            public bool isImmediate;
            public bool isCryption;
            public bool isBigNumber;
            public bool isValueType;
            public string typeName;
        }

        private const string FieldPath = "Assets/_App/AutoCreator/AutoRegister/PreferencesMgr/PreferencesField_AutoCreator.cs";
        private const string MsgPath = "Assets/_App/AutoCreator/AutoRegister/PreferencesMgr/PreferencesMsg_AutoCreator.cs";
        private const string MgrPath = "Assets/_App/AutoCreator/AutoRegister/PreferencesMgr/PreferencesMgr_AutoCreator.cs";

        /// <summary>
        /// 注释
        /// </summary>
        private static string notes = string.Empty;
        /// <summary>
        /// 是否开始读取注释
        /// </summary>
        private static bool isReadNotes;

        #region 内容
        private const string AutoCreatorComments =
@"/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/
";

        private static string FieldStr = "        public const string FieldName = \"FieldName\";";
        private static string FieldClassStr = AutoCreatorComments +
@"
using System.Collections.Generic;

namespace ProjectApp
{
    public static class PreferencesField
    {
//FieldConst
    }
}";

        private static string MsgStr = "        public const string FieldName = \"Preferences_FieldName\";";
        private static string MsgClassStr = AutoCreatorComments +
@"
using System.Collections.Generic;

namespace ProjectApp
{
    public static class PreferencesMsg
    {
//MsgConst
    }
}";

        private static string MgrClassStr = AutoCreatorComments +
@"
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections.Generic;
using FutureCore;
using FutureCore.Data;
using ProjectApp.Data;
using ProjectApp.Protocol;

namespace ProjectApp
{
    public partial class PreferencesMgr
    {
        private void OnInitPreferences()
        {
//OnInitPreferences
        }

        #region ValueType
//ValueType
        #endregion

        #region RefType
//RefType
        #endregion
    }
}";

        private static string InitPreferencesStr = @"            FieldName = preferences.FieldName;";

        private static string ValueFieldStr = @"
        private WhatDesc*Type FieldName;*notes
        public WhatDesc*Type PropertyName
        {
            get { return FieldName; }
            set
            {
                if (FieldName == value) return;
                ChangeValue<WhatDesc*Type> changeValue = PreferencesDispatcher<WhatDesc*Type>.Instance.changeValue;
                changeValue.oldValue = FieldName;
                changeValue.newValue = value;

                FieldName = value;
                preferences.FieldName = FieldName;
                AddSave(PreferencesField.FieldName, preferences.FieldName);
                PreferencesDispatcher<WhatDesc*Type>.Instance.Dispatch(PreferencesMsg.FieldName, changeValue);
                dataDispatcher.Dispatch(PreferencesMsg.FieldName);
            }
        }";

        private static string RefInitRef = @"
        public *type UpName
        {
            get
            {
                *RefInitNewRef
                return preferences.typeName;
            }
        }";

        private static string RefInitNewRef = "if (preferences.typeName == null) preferences.typeName = new *type();";

        private static string RefFieldStr = @"
        public void SavePropertyName()
        {
            AddSave(PreferencesField.FieldName, PropertyName);
            PreferencesDispatcher<object>.Instance.Dispatch(PreferencesMsg.FieldName);
            dataDispatcher.Dispatch(PreferencesMsg.FieldName);
        }";
        #endregion

        #region All Auto String Info
        private static string Auto_PreferencesMgr_Value;
        private static string Auto_PreferencesMgr_RefStr;
        private static string Auto_PreferencesMgr_TmpInitStr;
        #endregion

        public static void AutoRegisterPreferences()
        {
            AutoRegisterPreferencesField();
            AutoRegisterPreferencesMsg();
            AutoRegisterPreferencesMgr();
        }

        private static void AutoRegisterPreferencesField()
        {
            string auto_PreferencesField = string.Empty;
            AutoRegisterSettings asset = RegisterSettingsAssetTool.GetAutoRegisterSetting();
            for (int i = 0; i < asset.autoRegisterPath.Count; i++)
            {
                string path = AssetDatabase.GetAssetPath(asset.autoRegisterPath[i]);
                RegisterPreferencesVariate(path, FieldStr, ref auto_PreferencesField);
            }

            if (auto_PreferencesField.Length > 0)
            {
                auto_PreferencesField = auto_PreferencesField.Substring(0, auto_PreferencesField.Length - 2);
            }
            string classStr = FieldClassStr.Replace("//FieldConst", auto_PreferencesField);
            WriteFile(FieldPath, classStr);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[PreferencesMgrAutoRegisterTool]处理PreferencesField常量完成:" + FieldPath);
        }

        private static void AutoRegisterPreferencesMsg()
        {
            string auto_PreferencesMsg = string.Empty;
            AutoRegisterSettings asset = RegisterSettingsAssetTool.GetAutoRegisterSetting();
            for (int i = 0; i < asset.autoRegisterPath.Count; i++)
            {
                string path = AssetDatabase.GetAssetPath(asset.autoRegisterPath[i]);
                RegisterPreferencesVariate(path, MsgStr, ref auto_PreferencesMsg);
            }

            if (auto_PreferencesMsg.Length > 0)
            {
                auto_PreferencesMsg = auto_PreferencesMsg.Substring(0, auto_PreferencesMsg.Length - 2);
            }
            string classStr = MsgClassStr.Replace("//MsgConst", auto_PreferencesMsg);
            WriteFile(MsgPath, classStr);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[PreferencesMgrAutoRegisterTool]处理PreferencesMsg常量完成:" + MsgPath);
        }

        private static void RegisterPreferencesVariate(string path, string variateStr, ref string classStr)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.GetDirectories().Length != 0)
                {
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < directories.Length; i++)
                    {
                        RegisterPreferencesVariate(directories[i].FullName, variateStr, ref classStr);
                    }
                }

                List<FileInfo> preferences = new List<FileInfo>();
                // 取出Preferences数据类
                FileInfo[] files = directory.GetFiles();
                for (int j = 0; j < files.Length; j++)
                {
                    if (!files[j].Name.EndsWith(".cs") && !files[j].Name.Contains("Preferences")) continue;

                    StreamReader content = files[j].OpenText();
                    string line;
                    while ((line = content.ReadLine()) != null)
                    {
                        if (line.Trim().Equals("public partial class Preferences"))
                        {
                            preferences.Add(files[j]);
                            break;
                        };
                    }
                    content.Close();
                    content.Dispose();
                }

                // 处理自动注册的逻辑
                for (int i = 0; i < preferences.Count; i++)
                {
                    isReadNotes = false;

                    StreamReader content = preferences[i].OpenText();
                    string line;
                    while ((line = content.ReadLine()) != null)
                    {
                        if (!isReadNotes)
                        {
                            if (line.IndexOf("<summary>") > -1)
                            {
                                isReadNotes = true;
                                notes = string.Empty;
                            }
                            else
                                notes = string.Empty;
                        }

                        if (line.Trim().StartsWith("public"))
                        {
                            isReadNotes = false;
                            string[] fields = line.Trim().Split(' ');

                            if (fields[2].Equals("class")) continue;

                            string name = string.Empty;
                            int index = Array.IndexOf(fields, "="); //先判断是否有赋初始值

                            if (index == -1) // 如果没有赋初始值，则直接取 ; 号左边为字段名 需要区分 AAA; 和 AAA ;两者的区别
                            {
                                for (int k = 0; k < fields.Length; k++)
                                {
                                    if (fields[k].Equals(";"))
                                    {
                                        index = k;
                                    }

                                    if (fields[k].EndsWith(";"))
                                    {
                                        index = k + 1;
                                    }
                                }
                            }

                            // 赋值
                            name = fields[index - 1].Replace(";", "");
                            classStr += notes;
                            classStr += variateStr.Replace("FieldName", name) + "\r\n";
                        }
                        else if (isReadNotes)
                            notes += line + "\r\n";
                    }
                    content.Close();
                    content.Dispose();
                }
            }
        }

        private static void AutoRegisterPreferencesMgr()
        {
            Auto_PreferencesMgr_Value = string.Empty;
            Auto_PreferencesMgr_RefStr = string.Empty;
            Auto_PreferencesMgr_TmpInitStr = string.Empty;

            AutoRegisterSettings asset = RegisterSettingsAssetTool.GetAutoRegisterSetting();
            for (int i = 0; i < asset.autoRegisterPath.Count; i++)
            {
                string path = AssetDatabase.GetAssetPath(asset.autoRegisterPath[i]);
                RegisterPreferencesMgr(path);
            }

            if (Auto_PreferencesMgr_Value.Length > 0)
            {
                Auto_PreferencesMgr_Value = Auto_PreferencesMgr_Value.Substring(0, Auto_PreferencesMgr_Value.Length - 2);
            }

            if (Auto_PreferencesMgr_RefStr.Length > 0)
            {
                Auto_PreferencesMgr_RefStr = Auto_PreferencesMgr_RefStr.Substring(0, Auto_PreferencesMgr_RefStr.Length - 2);
            }

            if (Auto_PreferencesMgr_TmpInitStr.Length > 0)
            {
                Auto_PreferencesMgr_TmpInitStr = Auto_PreferencesMgr_TmpInitStr.Substring(0, Auto_PreferencesMgr_TmpInitStr.Length - 2);
            }

            string classStr = MgrClassStr.Replace("//ValueType", Auto_PreferencesMgr_Value).Replace("//RefType", Auto_PreferencesMgr_RefStr).Replace("//OnInitPreferences", Auto_PreferencesMgr_TmpInitStr);
            WriteFile(MgrPath, classStr);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[PreferencesMgrAutoRegisterTool]处理PreferencesMgr完成:" + MgrPath);
        }

        private static void RegisterPreferencesMgr(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.GetDirectories().Length != 0)
                {
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < directories.Length; i++)
                    {
                        RegisterPreferencesMgr(directories[i].FullName);
                    }
                }

                List<FileInfo> preferences = new List<FileInfo>();

                // 取出Preferences数据类
                FileInfo[] files = directory.GetFiles();
                for (int j = 0; j < files.Length; j++)
                {
                    if (!files[j].Name.EndsWith(".cs") && !files[j].Name.Contains("Preferences")) continue;

                    StreamReader content = files[j].OpenText();
                    string line;
                    while ((line = content.ReadLine()) != null)
                    {
                        if (line.Trim().Equals("public partial class Preferences"))
                        {
                            preferences.Add(files[j]);
                            ProjectAutoRegisterTool_v2.AutoRegisterVisualizationInfo(files[j].FullName, VisualizationType.Preferences);
                            break;
                        };
                    }
                    content.Close();
                    content.Dispose();
                }

                // 处理自动注册的逻辑
                for (int i = 0; i < preferences.Count; i++)
                {
                    isReadNotes = false;
                    StreamReader content = preferences[i].OpenText();
                    string line;
                    while ((line = content.ReadLine()) != null)
                    {
                        if (!isReadNotes)
                        {
                            if (line.IndexOf("<summary>") > -1)
                            {
                                isReadNotes = true;
                                notes = string.Empty;
                            }
                            else
                                notes = string.Empty;
                        }

                        if (line.Trim().StartsWith("public"))
                        {
                            isReadNotes = false;
                            string[] fields = line.Trim().Split(' ');

                            if (fields[2].Equals("class")) continue;

                            string type = string.Empty;
                            string name = string.Empty;

                            int index = Array.IndexOf(fields, "="); //先判断是否有赋初始值

                            if (index == -1) // 如果没有赋初始值，则直接取 ; 号左边为字段名  需要区分  AAA; 和 AAA ;两者的区别
                            {
                                for (int k = 0; k < fields.Length; k++)
                                {
                                    if (fields[k].Equals(";"))
                                    {
                                        index = k;
                                    }

                                    if (fields[k].EndsWith(";"))
                                    {
                                        index = k + 1;
                                    }
                                }
                            }

                            // 赋值
                            name = fields[index - 1].Replace(";", "");
                            for (int j = 1; j < index - 1; j++)
                            {
                                type += fields[j];
                            }

                            if (GetValueType(type))
                            {
                                if (!string.IsNullOrEmpty(notes))
                                    notes = "\r\n" + notes.Substring(0, notes.Length - 2);

                                // 赋值 value
                                string finalStr = ValueFieldStr.Replace("*notes", notes);
                                finalStr = finalStr.Replace("FieldName", name);
                                finalStr = finalStr.Replace("PropertyName", GetFirstUperStr(name));
                                if (immediateSend_List.Contains(name))
                                {
                                    finalStr = finalStr.Replace("AddSave", "SaveLater_ImmediateSendSave");
                                }
                                else if (quiesce_List.Contains(name))
                                {
                                    finalStr = finalStr.Replace("AddSave", "Save");
                                }
                                else
                                {
                                    finalStr = finalStr.Replace("AddSave", addQuickSave_List.Contains(name) ? "AddToAutoQuickSaveList" : "AddToAutoDelaySaveList");
                                }

                                if (cryption_List.Contains(name))
                                {
                                    finalStr = finalStr.Replace("WhatDesc", "Obscured").Replace("*Type", GetFirstUperStr(type));
                                }
                                else if (bigNumber_List.Contains(name))
                                {
                                    finalStr = finalStr.Replace("WhatDesc", "Big").Replace("*Type", "Integer");
                                }
                                else
                                {
                                    finalStr = finalStr.Replace("WhatDesc", string.Empty).Replace("*Type", type);
                                }

                                Auto_PreferencesMgr_Value += finalStr + "\r\n";
                                // 赋值 TmpInitStr
                                Auto_PreferencesMgr_TmpInitStr += InitPreferencesStr.Replace("FieldName", name) + "\r\n";
                            }
                            else
                            {
                                // 赋值Ref
                                string initRefInfo = RefInitRef;
                                if (IsArrayType(type))
                                {
                                    initRefInfo = initRefInfo.Replace("                *RefInitNewRef\r\n", string.Empty);
                                }
                                else
                                {
                                    initRefInfo = initRefInfo.Replace("*RefInitNewRef", RefInitNewRef);
                                }

                                string initStr = initRefInfo.Replace("*type", type).Replace("UpName", GetFirstUperStr(name)).Replace("typeName", name);
                                string finalStr = RefFieldStr.Replace("PropertyName", GetFirstUperStr(name));
                                finalStr = finalStr.Replace("FieldName", name);
                                if (immediateSend_List.Contains(name))
                                {
                                    finalStr = finalStr.Replace("AddSave", "SaveLater_ImmediateSendSave");
                                }
                                else if (quiesce_List.Contains(name))
                                {
                                    finalStr = finalStr.Replace("AddSave", "Save");
                                }
                                else
                                {
                                    finalStr = finalStr.Replace("AddSave", addQuickSave_List.Contains(name) ? "AddToAutoQuickSaveList" : "AddToAutoDelaySaveList");
                                }

                                if (!string.IsNullOrEmpty(notes))
                                    //去除换行
                                    notes = "\r\n" + notes.Substring(0, notes.Length - 2);

                                Auto_PreferencesMgr_RefStr += notes;
                                Auto_PreferencesMgr_RefStr += initStr + finalStr + "\r\n";
                            }
                        }
                        else if (isReadNotes)
                            notes += line + "\r\n";
                    }
                    content.Close();
                    content.Dispose();
                }
            }
        }

        private static bool GetDictionaryTypeName(string filesName)
        {
            if (filesName.StartsWith("Dictionary")) return true;
            return false;
        }

        private static bool GetValueType(string filesType)
        {
            if (filesType.EndsWith("[]") ||
                filesType.StartsWith("Dictionary") ||
                filesType.StartsWith("List") ||
                filesType.StartsWith("Stack") ||
                filesType.StartsWith("Queue") ||
                filesType.StartsWith("Ref_") ||
                filesType.EndsWith("_Ref"))
                return false;

            return true;
        }

        private static bool IsArrayType(string filesType)
        {
            return filesType.Contains("[]");
        }

        private static string GetFirstUperStr(string str)
        {
            if (str[0].ToString() == "_")
            {
                string str0 = str[0].ToString();
                return str0 + str[1].ToString().ToUpper() + str.Substring(2);
            }
            else
            {
                return str[0].ToString().ToUpper() + str.Substring(1);
            }
        }

        private static void WriteFile(string targetPath, string classStr)
        {
            FileTool.CreateTxt(targetPath, classStr, true, true);
        }
    }
}