/*
 Author:du
 Time:2017.10.30
*/

using System;
using System.IO;

namespace FutureEditor
{
    public class CustomScriptGenerateTool : UnityEditor.AssetModificationProcessor
    {
        public static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", "");
            if (path.ToLower().EndsWith(".cs"))
            {
                string name = "du";
                DateTime time = DateTime.Now;
                string content = File.ReadAllText(path);
                content = content.Replace("#AUTHORNAME#", name);
                content = content.Replace("#CREATETIME#", string.Format("{0}.{1}.{2}", time.Year, time.Month, time.Day));
                File.WriteAllText(path, content);
            }
        }
    }
}