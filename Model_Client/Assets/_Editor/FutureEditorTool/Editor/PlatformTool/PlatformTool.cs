using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace FutureEditor
{
    [InitializeOnLoad]
    public static class PlatformTool
    {
        public const string Windows = "Windows";
        public const string Android = "Android";
        public const string iOS = "iOS";
        public const string OSX = "OSX";
        public const string Webgl = "Webgl";

        static PlatformTool()
        {
            LaunchDynamicPlatformFolder();
        }

        private static void LaunchDynamicPlatformFolder()
        {
            if (EditorState.instance.IsLaunchPlatformFolder) return;

            DynamicPlatformFolder();
            EditorState.instance.IsLaunchPlatformFolder = true;
        }

        public static void DynamicPlatformFolder()
        {
            string folderName = null;
            string platform = CurrPlatformName;
            if (platform == Windows || platform == OSX)
            {
                folderName = "Standalone";
            }
            else if (platform == Android)
            {
                folderName = Android;
            }
            else if (platform == iOS)
            {
                folderName = iOS;
            }

            string lastDir = "Assets/_App/_Platform/";
            string targetDir = lastDir + folderName;
            targetDir = Path.GetFullPath(targetDir);
            if (Directory.Exists(Path.GetFullPath(lastDir)))
            {
                bool isDirty = false;
                List<string> dirs = new List<string>(Directory.GetDirectories(Path.GetFullPath(lastDir), "*", SearchOption.AllDirectories));
                foreach (var dirItem in dirs)
                {
                    if (dirItem.Contains(targetDir))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(dirItem);
                        if (FolderControlTool.HasThisAttribute(dirInfo, FileAttributes.Hidden))
                        {
                            isDirty = true;
                            FolderControlTool.SetFolderNoHide(dirInfo);
                        }
                    }
                    else
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(dirItem);
                        if (!FolderControlTool.HasThisAttribute(dirInfo, FileAttributes.Hidden))
                        {
                            isDirty = true;
                            FolderControlTool.SetFolderHide(dirInfo);
                        }
                    }
                }

                if (isDirty)
                {
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }

        public static bool IsAndroidPlatform()
        {
            return CurrPlatformName == Android;
        }

        public static bool IsiOSPlatform()
        {
            return CurrPlatformName == iOS;
        }

        public static string GetPlatformName(UnityEditor.BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return Windows;
                case BuildTarget.Android:
                    return Android;
                case BuildTarget.iOS:
                    return iOS;
                case BuildTarget.StandaloneOSX:
                    return OSX;
                case BuildTarget.WebGL:
                    return Webgl;
                default:
                    return null;
            }
        }

        public static string CurrPlatformName
        {
            get
            {
#if UNITY_EDITOR
                BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
                switch (target)
                {
                    case BuildTarget.StandaloneWindows:
                    case BuildTarget.StandaloneWindows64:
                        return Windows;
                    case BuildTarget.Android:
                        return Android;
                    case BuildTarget.iOS:
                        return iOS;
                    case BuildTarget.StandaloneOSX:
                        return OSX;
                    case BuildTarget.WebGL:
                        return Webgl;
                    default:
                        return null;
                }
#else
                UnityEngine.RuntimePlatform platform = UnityEngine.Application.platform;
                switch (platform)
                {
                    case UnityEngine.RuntimePlatform.WindowsPlayer:
                    case UnityEngine.RuntimePlatform.WindowsEditor:
                        return Windows;
                    case UnityEngine.RuntimePlatform.Android:
                        return Android;
                    case UnityEngine.RuntimePlatform.IPhonePlayer:
                        return iOS;
                    case UnityEngine.RuntimePlatform.OSXPlayer:
                        return OSX;
                    case UnityEngine.RuntimePlatform.WebGLPlayer:
                        return Webgl;
                    default:
                        return null;
                }
#endif
            }
        }
    }
}