/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Diagnostics;
using UnityEngine;

namespace FutureCore
{
    public static class SearchUtil
    {
        public static void ShowJsonWeb(object obj)
        {
            DebugUtil.CopyToClipboard(SerializeUtil.ToJson(obj));
            Application.OpenURL("https://www.json.cn/");
        }

        public static void ExceptionSearch(Exception e)
        {
            Process.Start(string.Format("https://stackoverflow.com/search?q=", e.Message));
            Process.Start(string.Format("https://www.google.com/search?q=" + e.Message));
            Process.Start(string.Format("https://answers.unity3d.com/search.html?q=" + e.Message));
            Process.Start(string.Format("https://www.baidu.com/s?ie=utf-8&f=8&rsv_bp=1&ch=&tn=baiduerr&bar=&wd=" + e.Message));
        }
    }
}