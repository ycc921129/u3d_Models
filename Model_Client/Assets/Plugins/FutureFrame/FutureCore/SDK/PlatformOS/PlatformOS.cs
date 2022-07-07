/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using FuturePlugin;
using UnityEngine;

namespace FutureCore
{
    public static class PlatformOS
    {
        public static PlatformOSType CurrType = PlatformOSType.None;
        public static BasePlatformOS Current = null;

        private static GameObject PlatformOSRoot;
        public static PlatformOSCallBackApi CBApi;

        public static void InitPlatform()
        {
        }

        public static void Init(BasePlatformOS customPlatformOS = null)
        {
            Dispose();
            LogUtil.Log("[PlatformOS]Init");

            AppObjConst.PlatformOSGo = new GameObject(AppObjConst.PlatformOS);
            AppObjConst.PlatformOSGo.SetParent(AppObjConst.FutureFrameGo);

            PlatformOSRoot = AppObjConst.PlatformOSGo;
            CBApi = PlatformOSRoot.AddComponent<PlatformOSCallBackApi>();

            if (customPlatformOS == null)
            {
#if UNITY_STANDALONE || UNITY_EDITOR
                Current = new WindowPlatformOS();
#elif UNITY_ANDROID && !UNITY_EDITOR
                Current = new AndroidPlatformOS();
#elif UNITY_IOS && !UNITY_EDITOR
                Current = new iOSPlatformOS();
#else
                Current = new BasePlatformOS();
#endif
            }
            else
            {
                Current = customPlatformOS;
            }

            CurrType = Current.GetPlatformOSType();
        }

        public static int GetPlatformOSId()
        {
            return (int)CurrType;
        }

        private static void Dispose()
        {
            Current = null;
        }
    }
}