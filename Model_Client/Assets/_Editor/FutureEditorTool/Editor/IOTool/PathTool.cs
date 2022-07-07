using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FutureEditor
{
    public static class PathTool
    {
        public static string FilePathToAssetPath(string path)
        {
            string fullPath = Path.GetFullPath(path);
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                return fullPath.Substring(fullPath.IndexOf("Assets\\"));
            }
            else
            {
                return fullPath.Substring(fullPath.IndexOf("Assets/"));
            }
        }

        public static string GetRawPath(string path)
        {
            return path.Replace("\\", "/");
        }

        public static string GetRelativeAssetsPath(string path)
        {
            return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
        }

        public static void GetAllFilePath(string path, List<string> files)
        {
            string[] names = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);
            foreach (string filename in names)
            {
                string ext = Path.GetExtension(filename);
                if (ext.Equals(".meta"))
                    continue;
                files.Add(filename.Replace('\\', '/'));
            }
            foreach (string dir in dirs)
            {
                GetAllFilePath(dir, files);
            }
        }

        public static string GetPathWithoutExtention(string path)
        {
            string extention = Path.GetExtension(path);
            return path.Replace(extention, string.Empty);
        }
    }
}