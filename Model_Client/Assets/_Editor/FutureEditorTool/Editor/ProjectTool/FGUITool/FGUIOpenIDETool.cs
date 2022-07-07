using UnityEditor;
using System.Diagnostics;
using System.IO;
using Debug = UnityEngine.Debug;

namespace FutureEditor
{
    public static class FGUIOpenIDETool
    {
        [MenuItem("[FC Project]/FGUI/OpenIDE/打开FairyGUI软件", false, 0)]
        public static void OpenIDE()
        {
            string ideFile = @"C:\DevelopTool\FairyGUI-Editor.exe - 快捷方式.lnk";
            string fguiDir = AppEditorInfo.UISVNPath;

            if (!File.Exists(ideFile))
            {
                Debug.Log("[FGUIOpenIDETool]不存在FGUI快捷方式: " + ideFile);
                ideFile = string.Empty;
            }
            if (!Directory.Exists(fguiDir))
            {
                Debug.Log("[FGUIOpenIDETool]不存在FGUI项目: " + fguiDir);
                fguiDir = string.Empty;
                return;
            }

            if (!string.IsNullOrEmpty(ideFile))
            {
                Process process = new Process();
                process.StartInfo.FileName = ideFile;
                process.StartInfo.Arguments = fguiDir;
                process.Start();
            }
            else
            {
                DirectoryInfo dir = new DirectoryInfo(fguiDir);
                FileInfo[] infos = dir.GetFiles();
                string fairyName = null;
                foreach (FileInfo info in infos)
                {
                    if (info.Name.Contains(".fairy"))
                    {
                        fairyName = info.Name;
                        break;
                    }
                }
                string path = Path.GetFullPath(fguiDir + fairyName);
                Process.Start(path);
            }

            Debug.Log("[FGUIOpenIDETool]打开FairyGUI软件");
        }
    }
}