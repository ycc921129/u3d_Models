/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FuturePlugin
{
    public static class PlatformUtil
    {
        public const string Windows = "Windows";
        public const string Android = "Android";
        public const string iOS = "iOS";
        public const string OSX = "OSX";
        public const string Webgl = "Webgl";

        private const bool IsTestRelease = false;

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

        public static bool IsEditor
        {
            get
            {
                bool retValue = false;
#if UNITY_EDITOR
                if (IsTestRelease)
                {
                    retValue = false;
                }
                else
                {
                    retValue = true;
                }
#endif
                return retValue;
            }
        }

        public static bool IsMobilePlatform
        {
            get
            {
                bool retValue = false;
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
                retValue = true;
#endif
                return retValue;
            }
        }

        public static bool IsStandalone
        {
            get
            {
                bool retValue = false;
#if UNITY_STANDALONE || UNITY_EDITOR
                retValue = true;
#endif
                return retValue;
            }
        }

        public static bool IsAndroid
        {
            get
            {
                bool retValue = false;
#if UNITY_ANDROID && !UNITY_EDITOR
                retValue = true;
#endif
                return retValue;
            }
        }

        public static bool IsiOS
        {
            get
            {
                bool retValue = false;
#if UNITY_IOS && !UNITY_EDITOR
                retValue = true;
#endif
                return retValue;
            }
        }
    }
}