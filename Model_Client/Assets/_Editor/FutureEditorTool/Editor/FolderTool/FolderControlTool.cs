using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class FolderControlTool
    {
        private static string ResourcesPath = Application.dataPath + "/_Res/Resources";
        private static string ResourcesMetaFile = Application.dataPath + "/_Res/Resources.meta";
        private static string ResourcesMetaMoveFile = Application.dataPath + "/_Res/.Resources.meta";

        [MenuItem("[FC Release]/FolderControl/_Res\\Resources/隐藏", false, 0)]
        public static void HideGameResFolder()
        {
            SetFolderHide(ResourcesPath);
            MoveFile(ResourcesMetaFile, ResourcesMetaMoveFile);
            AssetDatabase.Refresh();
        }

        [MenuItem("[FC Release]/FolderControl/_Res\\Resources/显示", false, 1)]
        public static void ShowGameResFolder()
        {
            SetFolderNoHide(ResourcesPath);
            MoveFile(ResourcesMetaMoveFile, ResourcesMetaFile);
            AssetDatabase.Refresh();
        }

        [MenuItem("[FC Release]/FolderControl/_Res\\Resources/隐藏", true, 0)]
        private static bool IsHideGameResFolder()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(ResourcesPath);
            return !HasThisAttribute(dirInfo, FileAttributes.Hidden);
        }

        [MenuItem("[FC Release]/FolderControl/_Res\\Resources/显示", true, 1)]
        private static bool IsShowGameResFolder()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(ResourcesPath);
            return HasThisAttribute(dirInfo, FileAttributes.Hidden);
        }

        public static bool HasThisAttribute(DirectoryInfo dir, FileAttributes attribute)
        {
            return (dir.Attributes & attribute) != 0;
        }

        public static void SetFolderHide(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            dirInfo.Attributes |= FileAttributes.Hidden;
        }

        public static void SetFolderNoHide(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            dirInfo.Attributes &= ~FileAttributes.Hidden;
        }

        public static void SetFolderHide(DirectoryInfo dirInfo)
        {
            dirInfo.Attributes |= FileAttributes.Hidden;
        }

        public static void SetFolderNoHide(DirectoryInfo dirInfo)
        {
            dirInfo.Attributes &= ~FileAttributes.Hidden;
        }

        private static void MoveFile(string formFile, string toFile)
        {
            if (File.Exists(formFile))
            {
                if (File.Exists(toFile))
                {
                    File.Delete(toFile);
                }
                File.Move(formFile, toFile);
            }
        }

        public static void ConvertResourcesFolderToResourcesTemp()
        {
            Object resResourcesFolder = AssetDatabase.LoadAssetAtPath(EditorPathConst.ResResourcesPath, typeof(DefaultAsset));
            if (resResourcesFolder != null)
            {
                ObjectNames.SetNameSmart(resResourcesFolder, EditorPathConst.ResResourcesTempName);
                Debug.Log("Convert ResourcesFolder To ResourcesTemp");
            }
        }

        public static void ConvertResourcesTempFolderToResources()
        {
            Object resResourcesTempFolder = AssetDatabase.LoadAssetAtPath(EditorPathConst.ResResourcesTempPath, typeof(DefaultAsset));
            if (resResourcesTempFolder != null)
            {
                ObjectNames.SetNameSmart(resResourcesTempFolder, "Resources");
                Debug.Log("Convert ResourcesTempFolder To Resources");
            }
        }
    }
}