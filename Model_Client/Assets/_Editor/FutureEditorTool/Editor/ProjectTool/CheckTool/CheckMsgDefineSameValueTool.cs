using System;
using System.Collections.Generic;
using FutureCore;
using ProjectApp;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class CheckMsgDefineSameValueTool
    {
        [MenuItem("[FC Project]/Check/检查消息Id重复冲突", false, 0)]
        public static bool CheckMsgDefineSameValue()
        {
            // 只检查数值Id消息
            var checkList = new List<Type>
            {
                typeof(AppMsg),
                typeof(MainThreadMsg),
                typeof(ChannelRawMsg),
                typeof(ChannelMsg),
                typeof(ModelMsg),
                typeof(ViewMsg),
                typeof(CtrlMsg),
                typeof(UICtrlMsg),
                typeof(GameMsg),
                typeof(RedDotMsg),
            };

            var allNames = string.Empty;
            foreach (var type in checkList)
            {
                allNames += type.Name + "、";
            }
            allNames = allNames.Substring(0, allNames.Length - 1);

            Debug.Log("[CheckMsgDefineSameValueTool]开始检查: 消息Id冲突: " + allNames);

            bool isCorrect = true;
            foreach (var type in checkList)
            {
                bool hasError = DoCheckMsgDefineSameValue(type);
                if (hasError)
                {
                    isCorrect = false;
                }
            }

            Debug.Log("[CheckMsgDefineSameValueTool]检查结束: 消息Id冲突");
            return isCorrect;
        }

        private static bool DoCheckMsgDefineSameValue(Type type)
        {
            //Debug.Log(string.Format("开始检查{0}类", type.Name));
            var hasError = false;
            var info = type;
            var fields = info.GetFields();
            var sameValues = new Dictionary<uint, List<string>>();
            var notUintLog = string.Empty;
            foreach (var field in fields)
            {
                if (field.Name == "NAME")
                {
                    continue;
                }
                if (field.Name == "NULL")
                {
                    continue;
                }
                if (field.FieldType.ToString() != "System.UInt32")
                {
                    notUintLog += string.Format(" {0}、", field.Name);
                    continue;
                }

                uint value = (uint)field.GetValue(0);
                List<string> list = null;
                if (sameValues.ContainsKey(value))
                {
                    list = sameValues[value];
                }

                if (list == null)
                {
                    list = new List<string>();
                    sameValues.Add(value, list);
                }
                list.Add(field.Name);
            }
            if (notUintLog.Length > 0)
            {
                notUintLog = notUintLog.Substring(0, notUintLog.Length - 1);
                Debug.LogError(string.Format("【{0}】类的以下常量不是uint类型: \n{1}", type.Name, notUintLog));
                hasError = true;
            }
            foreach (var keyValuePair in sameValues)
            {
                var list = keyValuePair.Value;
                if (list.Count > 1)
                {
                    var logContent = string.Empty;
                    foreach (var sameName in list)
                    {
                        logContent += string.Format("{0}=", sameName);
                    }

                    if (logContent.Length > 0)
                    {
                        logContent = logContent.Substring(0, logContent.Length - 1);
                        Debug.LogError(string.Format("【{0}】类的以下常量都使用了相同值：{1}\n{2}",
                            type.Name, keyValuePair.Key, logContent));
                        hasError = true;
                    }
                }
            }

            // 如果有错误就分隔一下
            //if (hasError)
            //Debug.LogError("---------------------------------------------");
            //Debug.Log(string.Format("结束检查{0}类", type.Name));

            return hasError;
        }
    }
}