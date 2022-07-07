using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class AudioAssetPathCreateTool
    {
        private static string CreatePath = Application.dataPath + "/_Res/Resources/Audio/";
        private static string ClassName = "*ClassName";
        private static string ClassReplaceStr = "//Content";
        private static string ClassStr =
@"/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

namespace ProjectApp
{
    public static class *ClassName
    {
//Content    }
}";

        [MenuItem("[FC Project]/Automation/AssetPath/生成资源路径脚本", false, 0)]
        public static void Create()
        {
            CreateAudioAssetPath();
            Debug.Log("[AssetPathTool]生成资源路径脚本完成");
        }

        private static void CreateAudioAssetPath()
        {
            FileInfo[] files = FileTool.GetFileNameAndPathUnderDirectory(CreatePath);
            Dictionary<string, string> pathDic = new Dictionary<string, string>();
            foreach (var item in files)
            {
                // wav 适用于较短的音乐文件可用作游戏打斗音效
                // aiff 适用于较短的音乐文件可用作游戏打斗音效
                // mp3 适用于较长的音乐文件可用作游戏背景音乐
                // ogg 适用于较长的音乐文件可用作游戏背景音乐
                if (item.Name.EndsWith(".wav") || item.Name.EndsWith(".aiff")
                    || item.Name.EndsWith(".mp3") || item.Name.EndsWith(".ogg"))
                {
                    string name = FileTool.GetFileNameWithOutExtension(item.Name);
                    string path = FileTool.GetResourcePath(item.FullName.Replace("\\", "/"), "Audio/");
                    if (pathDic.ContainsKey(name))
                    {
                        Debug.LogError("[AssetPathTool]重复命名的资源: " + name + "\r\n" + path);
                        continue;
                    }
                    pathDic.Add(name, path);
                }
            }

            string content = string.Empty;
            foreach (var pathItem in pathDic)
            {
                string row = "        public const string " + pathItem.Key + " = \"" + pathItem.Value + "\";\r\n";
                content += row;
            }
            string csStr = ClassStr.Replace(ClassReplaceStr, content).Replace(ClassName, "AudioAssetPath");
            FileTool.CreateTxt("Assets/_App/AutoCreator/Automation/AssetPath/AudioAssetPath_AutoCreator.cs", csStr, true, true);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}