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
             IL2Cpp����������˼·��
             1)��libil2cpp�ӿ�
             2)��global-metadata.dat���м���
             ���ǵ�libil2cpp.so����Unity���������б����صģ��ӿǵĻ���Ҫhook�����޸�UnityԴ�롣��˼���global-metadata.dat�Լ۱�����ߵġ�
             ��Unity��װĿ¼Editor\Data\il2cpp\libil2cpp\vm
             �ҵ�MetadataCache.cpp��MetadataLoader.cpp��MetadataCache::Initialize��ͨ��MetadataLoader::LoadMetadataFile������global-metadata.dat
             �ݴ˼��ɶ�dat���м��ܣ������Ǽ�������㷨�����޸�·�����ǿ��Եġ�
             �����Զ������̣���׿�����ڳ����Ժ�zip��ѹapk���д���iOS���ڴ��ΪXCode����ʱ���д���ipaһ�����ɣ��Ͳ��ý����޸��ˡ���Щ��������Editor�ű�����Jenkins�ű���
             �鷳��һ������Ҫ�޸�Unity��װĿ¼�Ĵ��룬�����Ҫע�����ʹ��ͬһ̨������ͬʱ�Կ�����Աƽʱ���д����һЩ���ݴ���������ؼ���ʧ����Fallback�������Ķ�ȡ�����С�
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