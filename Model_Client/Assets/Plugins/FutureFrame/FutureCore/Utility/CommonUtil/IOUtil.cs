/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.IO;

namespace FutureCore
{
    public static class IOUtil
    {
        public static void DeleteTargetFolderTheAllFileAndFolder(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                File.Delete(file);
            }
            string[] dirs = Directory.GetDirectories(path);
            foreach (string dir in dirs)
            {
                Directory.Delete(dir, true);
            }
        }
    }
}