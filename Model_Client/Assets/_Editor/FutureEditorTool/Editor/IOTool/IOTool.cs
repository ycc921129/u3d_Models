using System.IO;
using UnityEditor;

namespace FutureEditor
{
    public static class IOTool
    {
        public static void DeleteFileOrDirectory(string path)
        {
            FileUtil.DeleteFileOrDirectory(path);
        }

        public static void MoveFileOrDirectory(string form, string to)
        {
            FileUtil.MoveFileOrDirectory(form, to);
        }

        public static bool IsHidden(DirectoryInfo directory)
        {
            if ((directory.Attributes & FileAttributes.Hidden) > 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsHidden(FileInfo file)
        {
            if ((file.Attributes & FileAttributes.Hidden) > 0)
            {
                return true;
            }
            return false;
        }
    }
}