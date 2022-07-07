using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class StatisticExportTool
    {
        private static string StatisticBaseConstPath = "Assets/_AppBase/ProjectAppBase/Define/Const/StatisticConst.cs";
        private static string StatisticLogicConstPath = "Assets/_App/ProjectApp/Logic/Define/Const/StatisticConst.cs";
        private static string ExportDir = "Assets/_App/Export/Statistic/";

        /// <summary>
        /// 生成友盟统计后台使用的事件名文件
        /// </summary>
        [MenuItem("[FC Project]/Export/Statistic/导出统计打点数据文本", false, 10)]
        public static void GenerateStatisticKeyTxt()
        {
            string text1 = GetStatisticText(StatisticBaseConstPath);
            string text2 = GetStatisticText(StatisticLogicConstPath);
            string filePath = ExportDir + "StatisticEvent" + "_" + System.DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";

            if (Directory.Exists(ExportDir))
            {
                FileUtil.DeleteFileOrDirectory(ExportDir);
            }
            Directory.CreateDirectory(ExportDir);

            FileTool.CreateTxt(filePath, text1 + "\n" + text2, true, true);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[StatisticExportTool]导出统计埋点数据文本: " + filePath);
        }

        private static string GetStatisticText(string path)
        {
            if (!File.Exists(path)) return string.Empty;

            StringBuilder builder = new StringBuilder();
            string ClassStr = AssetDatabase.LoadAssetAtPath<TextAsset>(path).text.Trim();
            string[] eventKeys = ClassStr.Split('=');
            for (int i = 0; i < eventKeys.Length - 1; i++)
            {
                string dirtyKey = eventKeys[i + 1];
                string key = dirtyKey.Substring(dirtyKey.IndexOf("\"") + 1, dirtyKey.LastIndexOf("\"") - 2);

                string dirtyEventStr = eventKeys[i];
                string summary = "/// <summary>";
                string dirtyDescription = dirtyEventStr.Substring(dirtyEventStr.IndexOf(summary) + summary.Length);

                string stillDirtyDescription = dirtyDescription.Substring(0, dirtyDescription.LastIndexOf("///")).Substring(dirtyDescription.IndexOf("///")).Trim();
                string clearDescription = stillDirtyDescription.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                string description = clearDescription.Replace("/", "").Replace("<summary>", "").Replace("</summary>", "");

                builder.Append(key);
                builder.Append(",");
                builder.Append(description);
                builder.Append("\n");
            }

            string text = builder.ToString();
            if (text.Length > 0)
            {
                text = text.Substring(0, text.Length - 1);
            }
            return text;
        }
    }
}