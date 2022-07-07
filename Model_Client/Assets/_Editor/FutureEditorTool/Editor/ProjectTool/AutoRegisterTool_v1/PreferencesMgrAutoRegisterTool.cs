using System;
using System.Collections;
using System.Collections.Generic;
using ProjectApp.Data;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class PreferencesMgrAutoRegisterTool
    {
        /// <summary>
        /// 注册通用即时保存列表
        /// </summary>
        private static List<string> ImmediateList = new List<string>
        {
            // 收集检查安装包
            "hacker_pgs",
        };

        /// <summary>
        /// 注册加密值类型列表
        /// </summary>
        private static List<string> CryptionList = new List<string>
        {
            // 金币
            "coin",
            // 累积金币
            "acc_coin",
        };

        /// <summary>
        /// 注册大数值类型列表
        /// </summary>
        private static List<string> BigNumberList = new List<string>
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

        public const string MsgPath = "Assets/_App/ProjectApp/_AutoCreator/PreferencesMgr/PreferencesMsg_AutoCreator.cs";
        public const string MgrPath = "Assets/_App/ProjectApp/_AutoCreator/PreferencesMgr/PreferencesMgr_AutoCreator.cs";

        #region 内容
        private const string AutoCreatorComments =
@"/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/
";

        public static string MsgStr = "        public const string FieldName = \"Preference_FieldName\";";

        public static string MsgClassStr = AutoCreatorComments +
@"
using System.Collections.Generic;

namespace ProjectApp
{
    public static class PreferencesMsg
    {
//MsgConst
    }
}";

        public static string MgrClassStr = AutoCreatorComments +
@"
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections.Generic;
using FutureCore;
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

        public static string InitPreferencesStr = @"            FieldName = preferences.FieldName;";

        public static string ValueFieldStr = @"
        private WhatDesc*Type FieldName;
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
                PreferencesDispatcher<WhatDesc*Type>.Instance.Dispatch(PreferencesMsg.FieldName, changeValue);
                IsImmediate(PreferencesMsg.FieldName, preferences.FieldName);
            }
        }";

        public static string InitRef = @"
        public *type UpName { get { return preferences.typeName; } }";

        public static string RefFieldStr = @"
        public void SavePropertyName()
        {
            IsImmediate(PreferencesMsg.FieldName, preferences.FieldName);
            PreferencesDispatcher<object>.Instance.Dispatch(PreferencesMsg.FieldName);
        }";
        #endregion

        public static void AutoRegisterPreferencesMsg()
        {
            string tmpConstStr = string.Empty;

            Type type = typeof(Preferences);
            foreach (var item in type.GetFields())
            {
                tmpConstStr += MsgStr.Replace("FieldName", item.Name) + "\n";
            }
            if (tmpConstStr.Length > 0)
            {
                tmpConstStr = tmpConstStr.Substring(0, tmpConstStr.Length - 1);
            }
            string classStr = MsgClassStr.Replace("//MsgConst", tmpConstStr);
            FileTool.CreateTxt(MsgPath, classStr, true, true);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("处理PreferenceMsg常量完成:" + MsgPath);
        }

        public static void AutoRegisterPreferencesMgr()
        {
            Type type = typeof(Preferences);
            List<FieldInfo> infoList = new List<FieldInfo>();
            foreach (var item in type.GetFields())
            {
                FieldInfo info = new FieldInfo();
                info.name = item.Name;
                info.isCryption = CryptionList.Contains(item.Name);
                info.isBigNumber = BigNumberList.Contains(item.Name);
                info.isImmediate = ImmediateList.Contains(item.Name);
                info.isValueType = item.FieldType.IsValueType || item.FieldType.Name == "String";

                if (!info.isValueType)
                {
                    if (typeof(IDictionary).IsAssignableFrom(item.FieldType))
                    {
                        info.typeName = GetDictionaryTypeName(item.FieldType);
                    }
                    else if (item.FieldType.IsGenericType) //List
                    {
                        string subType = GetTypeStrByType(item.FieldType.GetGenericArguments()[0]);

                        info.typeName = string.Format("List<{0}>", subType);
                    }

                    else
                    {
                        info.typeName = item.FieldType.Name;
                    }
                }
                else
                    info.typeName = GetTypeStrByType(item.FieldType);
                infoList.Add(info);
            }

            string valueStr = string.Empty;
            string tmpInitStr = string.Empty;
            foreach (var item in infoList.FindAll((w) => w.isValueType))
            {
                string fianlStr = ValueFieldStr.Replace("FieldName", item.name).Replace("PropertyName", GetFirstUperStr(item.name)).Replace("IsImmediate", item.isImmediate ? "AddToSaveList" : "AddToAutoDelaySaveList");

                if (item.isCryption)
                {
                    fianlStr = fianlStr.Replace("WhatDesc", "Obscured").Replace("*Type", GetFirstUperStr(item.typeName));
                }
                else if (item.isBigNumber)
                {
                    fianlStr = fianlStr.Replace("WhatDesc", "Big").Replace("*Type", "Integer");
                }
                else
                {
                    fianlStr = fianlStr.Replace("WhatDesc", string.Empty).Replace("*Type", item.typeName);
                }
                valueStr += fianlStr + "\n";

                tmpInitStr += InitPreferencesStr.Replace("FieldName", item.name) + "\n";
            }
            foreach (var item in infoList.FindAll((w) => !w.isValueType))
            {

            }
            if (tmpInitStr.Length > 0)
            {
                tmpInitStr = tmpInitStr.Substring(0, tmpInitStr.Length - 1);
            }

            string RefStr = string.Empty;
            foreach (var item in infoList.FindAll((w) => !w.isValueType))
            {
                string initStr = InitRef.Replace("*type", item.typeName).Replace("UpName", GetFirstUperStr(item.name)).Replace("typeName", item.name);

                string finalStr = RefFieldStr.Replace("PropertyName", GetFirstUperStr(item.name)).Replace("IsImmediate", item.isImmediate ? "AddToSaveList" : "AddToAutoDelaySaveList").Replace("FieldName", item.name);
                RefStr += initStr + finalStr + "\n";
            }
            string classStr = MgrClassStr.Replace("//ValueType", valueStr).Replace("//RefType", RefStr).Replace("//OnInitPreferences", tmpInitStr);
            FileTool.CreateTxt(MgrPath, classStr, true, true);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("处理PreferenceMgr完成:" + MgrPath);
        }

        private static string GetDictionaryTypeName(Type type)
        {
            string subType0 = GetTypeStrByType(type.GetGenericArguments()[0]);
            string subType1 = "";

            Type type1 = type.GetGenericArguments()[1];
            if (typeof(IDictionary).IsAssignableFrom(type1))
            {

                subType1 = GetDictionaryTypeName(type1);
            }
            else if (type1.IsGenericType)
            {
                string subType = GetTypeStrByType(type1.GetGenericArguments()[0]);

                subType1 = string.Format("List<{0}>", subType);
            }
            else
            {
                subType1 = GetTypeStrByType(type1);
            }

            return string.Format("Dictionary<{0},{1}>", subType0, subType1);
        }

        private static string GetTypeStrByType(Type type)
        {
            switch (type.Name)
            {
                case "Boolean":
                    return "bool";
                case "Byte":
                    return "byte";
                case "SByte":
                    return "sbyte";
                case "Char":
                    return "char";
                case "Decimal":
                    return "decimal";
                case "Double":
                    return "double";
                case "Single":
                    return "float";
                case "Int32":
                    return "int";
                case "UInt32":
                    return "uint";
                case "Int64":
                    return "long";
                case "UInt64":
                    return "ulong";
                case "Object":
                    return "object";
                case "Int16":
                    return "short";
                case "UInt16":
                    return "ushort";
                case "String":
                    return "string";
                default:
                    return type.Name;
            }
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
    }
}