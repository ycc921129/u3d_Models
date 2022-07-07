using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class OpenFolderTool
    {
        [MenuItem("[FC Toolkit]/OpenFolder/Data", false, 0)]
        public static void OpenDataFolder()
        {
            OpenFolder(Application.dataPath);
        }

        [MenuItem("[FC Toolkit]/OpenFolder/StreamingAssets", false, 1)]
        public static void OpenStreamingAssetsFolder()
        {
            OpenFolder(Application.streamingAssetsPath);
        }

        [MenuItem("[FC Toolkit]/OpenFolder/PersistentData", false, 2)]
        public static void OpenPersistentDataFolder()
        {
            OpenFolder(Application.persistentDataPath);
        }

        [MenuItem("[FC Toolkit]/OpenFolder/TemporaryCache", false, 3)]
        public static void OpenTemporaryCacheFolder()
        {
            OpenFolder(Application.temporaryCachePath);
        }

        [MenuItem("[FC Toolkit]/OpenFolder/AppDataLocalUnity", false, 4)]
        public static void OpenAppDataLocalUnityFolder()
        {
            OpenFolder(string.Format(@"C:/Users/{0}/AppData/Local/Unity", Environment.UserName));
        }

        [MenuItem("[FC Toolkit]/OpenFolder/AppDataLocalLowUnity", false, 5)]
        public static void OpenAppDataLocalLowUnityFolder()
        {
            OpenFolder(string.Format(@"C:/Users/{0}/AppData/LocalLow/Unity", Environment.UserName));
        }

        [MenuItem("[FC Toolkit]/OpenFolder/AppDataRoamingUnity", false, 6)]
        public static void OpenAppDataRoamingUnityFolder()
        {
            OpenFolder(string.Format(@"C:/Users/{0}/AppData/Roaming/Unity", Environment.UserName));
        }

        [MenuItem("[FC Toolkit]/OpenFolder/ProgramDataUnity", false, 7)]
        public static void OpenProgramDataUnityFolder()
        {
            OpenFolder(@"C:/ProgramData/Unity");
        }

        [MenuItem("[FC Toolkit]/OpenFolder/UnityAssetStore", false, 8)]
        public static void OpenUnityAssetStoreFolder()
        {
            OpenFolder(string.Format(@"C:/Users/{0}/AppData/Roaming/Unity/Asset Store-5.x", Environment.UserName));
        }

        [MenuItem("[FC Toolkit]/OpenFolder/UnityEditorLog", false, 9)]
        public static void OpenUnityEditorLog()
        {
            OpenFolder(string.Format(@"C:/Users/{0}/AppData/Local/Unity/Editor", Environment.UserName));
        }

        [MenuItem("[FC Toolkit]/OpenFolder/Editor/Application", false, -1)]
        public static void OpenApplicationFolder()
        {
            OpenFolder(EditorApplication.applicationPath);
        }

        [MenuItem("[FC Toolkit]/OpenFolder/Editor/ApplicationContents", false, 0)]
        public static void OpenApplicationContentsFolder()
        {
            OpenFolder(EditorApplication.applicationContentsPath);
        }

        [MenuItem("[FC Toolkit]/OpenFolder/Editor/LibIl2CppVM", false, 1)]
        public static void OpenLibIl2CppVMFolder()
        {
            /*
             IL2Cpp加密有两种思路：
             1)对libil2cpp加壳
             2)对global-metadata.dat进行加密
             考虑到libil2cpp.so是在Unity启动流程中被加载的，加壳的话需要hook或者修改Unity源码。因此加密global-metadata.dat性价比是最高的。
             打开Unity安装目录Editor\Data\il2cpp\libil2cpp\vm
             找到MetadataCache.cpp和MetadataLoader.cpp，MetadataCache::Initialize中通过MetadataLoader::LoadMetadataFile加载了global-metadata.dat
             据此即可对dat进行加密，无论是加入加密算法还是修改路径都是可以的。
             对于自动化流程，安卓可以在出包以后，zip解压apk进行处理；iOS则在打包为XCode工程时进行处理，ipa一旦生成，就不好进行修改了。这些可以做成Editor脚本或者Jenkins脚本。
             麻烦的一点是需要修改Unity安装目录的代码，因此需要注意出包使用同一台机器，同时对开发人员平时自行打包做一些兼容处理，例如加载加密失败则Fallback到正常的读取流程中。
             */
            OpenFolder(EditorApplication.applicationContentsPath + "/il2cpp/libil2cpp/vm");
        }

        public static void OpenFolder(string path)
        {
            Process.Start("explorer.exe", path.Replace("/", "\\"));
            UnityEngine.Debug.Log(path);
        }
    }
}