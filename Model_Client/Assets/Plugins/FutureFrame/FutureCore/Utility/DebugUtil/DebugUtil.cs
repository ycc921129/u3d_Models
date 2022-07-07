/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace FutureCore
{
    public static class DebugUtil
    {
        public static void ExtractStackTrace()
        {
            LogUtil.LogFormat("[DebugUtil]ExtractStackTrace:\n{0}\nEND", StackTraceUtility.ExtractStackTrace());
        }

        public static void CurrStackTrace()
        {
            string info = string.Empty;
            StackTrace st = new StackTrace(new StackFrame(true));
            StackFrame sf = st.GetFrame(0);
            info += string.Format("当前堆栈跟踪级别:{0}\n", st.ToString());
            info += string.Format("文件:{0}\n", sf.GetFileName());
            info += string.Format("方法:{0}\n", sf.GetMethod().Name);
            info += string.Format("行号:{0}\n", sf.GetFileLineNumber());
            info += string.Format("列号:{0}", sf.GetFileColumnNumber());
            LogUtil.LogFormat("[DebugUtil]CurrStackTrace:\n{0}\nEND", info);
        }

        public static void CopyToClipboard(string input)
        {
            TextEditor textEditor = new TextEditor();
            textEditor.text = input;
            textEditor.OnFocus();
            textEditor.Copy();
        }

        public static string ToString(Array array)
        {
            if (array == null)
            {
                return "null";
            }
            else
            {
                return "{" + string.Join(", ", array.Cast<object>().Select(o => o.ToString()).ToArray()) + "}";
            }
        }

        public static string ToString<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            if (dict == null)
            {
                return "null";
            }
            else
            {
                return "{" + string.Join(", ", dict.Select(kvp => kvp.Key.ToString() + ":" + kvp.Value.ToString()).ToArray()) + "}";
            }
        }
    }
}