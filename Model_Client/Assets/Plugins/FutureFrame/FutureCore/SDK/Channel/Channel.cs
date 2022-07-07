/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using FuturePlugin;
using UnityEngine;

namespace FutureCore
{
    public static class Channel
    {
        public static BaseChannel Current = null;
        public static bool IsRelease = false;
        public static ChannelType CurrType = ChannelType.LocalDebug;

        private static GameObject ChannelRoot;

        public static void InitPlatform()
        {
#if UNITY_IOS && !UNITY_EDITOR
            iOSChannelApi.init();
#endif
        }

        public static void Init(BaseChannel customChannel = null)
        {
            Dispose();
            LogUtil.Log("[Channel]Init");

            AppObjConst.ChannelGo = new GameObject(AppObjConst.Channel);
            AppObjConst.ChannelGo.SetParent(AppObjConst.FutureFrameGo);
            ChannelRoot = AppObjConst.ChannelGo;

            if (customChannel == null)
            {
#if UNITY_STANDALONE || UNITY_EDITOR
                Current = new BaseChannel();
#elif UNITY_ANDROID && !UNITY_EDITOR
                Current = new AndroidChannel();
#elif UNITY_IOS && !UNITY_EDITOR
                Current = new iOSChannel();
#else
                Current = new BaseChannel();
#endif
            }
            else
            {
                Current = customChannel;
            }

            Current.Init(ChannelRoot);
            SetCurrChannelType();
            InitChannelSetting();
            SetAppConst();
        }

        private static void SetCurrChannelType()
        {
            if (Current.debug)
            {
                CurrType = ChannelType.LocalDebug;
            }
            else
            {
                CurrType = ChannelType.NetRelease;
            }
        }

        private static void InitChannelSetting()
        {
            AppBuildType appBuildType = Current.buildType;
            switch (appBuildType)
            {
                case AppBuildType.Debug:
                case AppBuildType.DebugFormal:
                    IsRelease = false;
                    EngineLauncher.Instance.reporter.numOfCircleToShow = AppConst.LogsViewerShowNum_Debug;
                    break;
                case AppBuildType.Release:
                    IsRelease = true;
                    EngineLauncher.Instance.reporter.numOfCircleToShow = AppConst.LogsViewerShowNum_Release;
                    break;
                default:
                    IsRelease = false;
                    EngineLauncher.Instance.reporter.numOfCircleToShow = AppConst.LogsViewerShowNum_Debug;
                    break;
            }
        }

        private static void SetAppConst()
        {
            AppConst.IsDebugVersion = Current.debug;
        }

        public static int GetChannelType()
        {
            return (int)CurrType;
        }

        public static string GetChannelDefine()
        {
            return Current.GetChannelDefine();
        }

        public static ChannelCallBackApi GetCBApi()
        {
            return Current.GetCBApi();
        }

        private static void Dispose()
        {
            Current = null;
        }
    }
}