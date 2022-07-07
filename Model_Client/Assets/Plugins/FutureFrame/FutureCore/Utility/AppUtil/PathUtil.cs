/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.6
*/

using System.IO;
using FuturePlugin;
using UnityEngine;

namespace FutureCore
{
    public static class PathUtil
    {
        public static string ProjectPath
        {
            get
            {
                return Application.dataPath + "/";
            }
        }

        public static string StreamingAssetsPath
        {
            get
            {
                // 这个路径只能用来AssetBundle.LoadFromFile。
                // 如果想用File类操作，比如File.ReadAllText或者File.Exists或者Directory.Exists这样都是不行的。
                return Application.streamingAssetsPath + "/";
            }
        }

        public static string RawDataPath
        {
            get
            {
                return Application.persistentDataPath + "/";
            }
        }

        public static string DataPath
        {
            get
            {
                string path = null;
                if (AppConst.IsDevelopMode)
                {
                    path = Application.streamingAssetsPath + "/";
                }
                else if (Application.isEditor)
                {
                    path = Application.persistentDataPath + "/";
                }
                else if (Application.isMobilePlatform)
                {
                    path = Application.persistentDataPath + "/";
                }
                else if (Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    path = Application.persistentDataPath + "/";
                }
                else if (Application.platform == RuntimePlatform.OSXEditor)
                {
                    path = Application.persistentDataPath + "/";
                }
                return path;
            }
        }

        public static string FileProtoStreamingAssetsPath
        {
            get
            {
                return "file:///" + StreamingAssetsPath;
            }
        }

        public static string FileProtoPlatformDataPath
        {
            get
            {
                return "file:///" + PlatformDataPath;
            }
        }

        public static string PlatformDataPath
        {
            get
            {
                return string.Format("{0}{1}", DataPath, PlatformSubDataPath);
            }
        }

        public static string PlatformSubDataPath
        {
            get
            {
                return string.Format("{0}/{1}/", "www", PlatformUtil.CurrPlatformName);
            }
        }
        
        public static void NoExistsCreateDir(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        public static void ClearDir(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            if (dirInfo.Exists)
            {
                dirInfo.Delete(true);
            }
            dirInfo.Create();
        }

        public static void ClearPath(string path)
        {
            DirectoryInfo pathInfo = new DirectoryInfo(path);
            if (pathInfo.Parent.Exists)
            {
                pathInfo.Parent.Delete(true);
            }
            pathInfo.Parent.Create();
        }
    }
}