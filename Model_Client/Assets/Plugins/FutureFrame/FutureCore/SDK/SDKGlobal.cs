/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public static class SDKGlobal
    {
        public static void InitPlatform()
        {
            PlatformOS.InitPlatform();
            Channel.InitPlatform();
        }

        public static void Init()
        {
            BasePlatformOS customPlatformOS = null;
            BaseChannel customChannel = null;

            ISDK[] customSdkArray = App.GetCustomSDKs();
            if (customSdkArray != null && customSdkArray.Length != 0)
            {
                for (int i = 0; i < customSdkArray.Length; i++)
                {
                    ISDK sdk = customSdkArray[i];
                    if (sdk == null)
                    {
                        continue;
                    }
                    if (sdk is BasePlatformOS)
                    {
                        customPlatformOS = (BasePlatformOS)sdk;
                    }
                    else if (sdk is BaseChannel)
                    {
                        customChannel = (BaseChannel)sdk;
                    }
                }
            }

            PlatformOS.Init(customPlatformOS);
            Channel.Init(customChannel);
        }
    }
}