using System.Diagnostics;
using System.IO;

namespace FutureEditor
{
    public static class DirectoryTool
    {
        public static void CopyDir(string fromDir, string toDir)
        {
            if (!CheckPath(fromDir))
            {
                return;
            }
            ClearDirectory(toDir);

            string[] fromDirs = Directory.GetDirectories(fromDir);
            foreach (string fromDirName in fromDirs)
            {
                string dirName = Path.GetFileName(fromDirName);
                string toDirName = Path.Combine(toDir, dirName);
                CopyDir(fromDirName, toDirName);
            }
            string[] files = Directory.GetFiles(fromDir);
            foreach (string fromFileName in files)
            {
                string fileName = Path.GetFileName(fromFileName);
                string toFileName = Path.Combine(toDir, fileName);
                File.Copy(fromFileName, toFileName, true);
            }
        }

        public static void DeleteTypeFile(string dir, string typeExt)
        {
            if (!CheckPath(dir))
            {
                return;
            }
            string[] files = Directory.GetFiles(dir);
            foreach (string fileItem in files)
            {
                string ext = Path.GetExtension(fileItem);
                if (ext.EndsWith(typeExt))
                {
                    File.Delete(fileItem);
                }
            }
            string[] dirs = Directory.GetDirectories(dir);
            foreach (string dirItem in dirs)
            {
                DeleteTypeFile(dirItem, typeExt);
            }
        }

        public static void MoveDir(string fromDir, string toDir)
        {
            if (!CheckPath(fromDir))
            {
                return;
            }

            CopyDir(fromDir, toDir);
            Directory.Delete(fromDir, true);
        }

        public static void ClearDirectory(string dir)
        {
            if (Directory.Exists(dir))
                Directory.Delete(dir, true);
            Directory.CreateDirectory(dir);
        }

        public static bool CheckPath(string dir)
        {
            if (!Directory.Exists(dir))
            {
                UnityEngine.Debug.LogError("目录不存在 : " + dir);
                return false;
            }
            return true;
        }

        public static void OpenDirectory(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            path = path.Replace("/", "\\");
            if (!Directory.Exists(path))
            {
                UnityEngine.Debug.LogError("No Directory: " + path);
                return;
            }

            Process.Start("explorer.exe", path);
        }
    }
}