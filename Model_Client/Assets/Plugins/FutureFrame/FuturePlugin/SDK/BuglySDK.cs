/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FuturePlugin
{
    public static class BuglySDK
    {
        public const bool IsUseBugly = true;

        private static string AppIDForAndroid;
        private static string AppIDForiOS;
        private static string ChannelName;
        private static string AppVersion;
        private static string User;

        public static void SetInfo(string ChannelName, string AppVersion, string User)
        {
            BuglySDK.ChannelName = ChannelName;
            BuglySDK.AppVersion = AppVersion;
            BuglySDK.User = User;
        }

        public static void Init(string AppIDForAndroid, string AppIDForiOS)
        {
            if (!IsUseBugly) return;

            string appId = null;
            if (Application.platform == RuntimePlatform.Android)
            {
                if (string.IsNullOrEmpty(AppIDForAndroid)) return;
                if (AppIDForAndroid.Contains("No")) return;
                appId = AppIDForAndroid;
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (string.IsNullOrEmpty(AppIDForiOS)) return;
                if (AppIDForiOS.Contains("No")) return;
                appId = AppIDForiOS;
            }
            else
            {
                return;
            }

            BuglySDK.AppIDForAndroid = AppIDForAndroid;
            BuglySDK.AppIDForiOS = AppIDForiOS;

            InitSDKBefore();
            InitSDK();
            InitUser();

            LogUtil.Log("[BuglySDK]Init Done " + appId);
        }

        private static void InitSDKBefore()
        {
            //开启SDK的日志打印，发布版本请务必关闭
            BuglyAgent.ConfigDebugMode(false);

            //修改应用默认配置信息：渠道号、版本、用户标识、Android初始化延时等。
            //在初始化之前调用
            //渠道号默认值为空，
            //版本默认值
            //Android应用默认读取AndroidManifest.xml中的android:versionName
            //iOS应用默认读取Info.plist文件中CFBundleShortVersionString和CFBundleVersion，拼接为CFBundleShortVersionString(CFBundleVersion)格式，例如1.0.1(10)
            //用户标识默认值为Unknown
            //Android初始化延时时间， 默认为0，单位毫秒
            BuglyAgent.ConfigDefault(ChannelName, AppVersion, User, 0);

            //设置自动上报日志信息的级别，默认LogError，则>=LogError的日志都会自动捕获上报。
            BuglyAgent.ConfigAutoReportLogLevel(LogSeverity.LogError);

            //配置是否在捕获上报C#异常信息后就立即退出应用，避免后续产生更多非预期的C#的异常。
            //在初始化之前调用
            BuglyAgent.ConfigAutoQuitApplication(false);

            //注册日志回调，替换使用 'Application.RegisterLogCallback(Application.LogCallback)'注册日志回调的方式
            BuglyAgent.RegisterLogCallback(null);
        }

        private static void InitSDK()
        {
            //初始化Bugly，传入Bugly网站注册获得的App ID。
#if UNITY_ANDROID
            BuglyAgent.InitWithAppId(AppIDForAndroid);
#elif UNITY_IPHONE || UNITY_IOS
            BuglyAgent.InitWithAppId (AppIDForiOS);
#endif
            //如果你确认已在对应的iOS工程或Android工程中初始化SDK，那么在脚本中只需启动C#异常捕获上报功能即可
            BuglyAgent.EnableExceptionHandler();
        }

        private static void InitUser()
        {
            //设置用户标识，如果不设置，默认为Unknown。
            //在初始化之后调用
            BuglyAgent.SetUserId(User);

            //设置应用场景标识
#if UNITY_ANDROID
            BuglyAgent.SetScene(3450);
#elif UNITY_IPHONE || UNITY_IOS
            BuglyAgent.SetScene(3261);
#endif
        }
    }
}