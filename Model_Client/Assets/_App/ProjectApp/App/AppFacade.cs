using FutureCore;
using UnityEngine;

namespace ProjectApp
{
#if UNITY_ANDROID || UNITY_STANDALONE
    /// <summary>
    /// 应用外观
    /// </summary>
    public static class AppFacade
    {
#region 应用配置
        /// <summary>
        /// 应用代号
        /// </summary>
        public const string AppName = "payday";    

        /// <summary>
        /// 项目描述
        /// </summary>
        public const string AppDesc = "主框架";    

        /// <summary>
        /// 包名  
        /// </summary>
        public const string PackageName = "com.integralwallb.payday";  

        /// <summary>
        /// 密钥Key
        /// </summary>
        public const string AESKey = "3KdDaa0yvNziL9rz";  

        /// <summary>    
        /// 密钥IVector
        /// </summary>  
        public const string AESIVector = "KJSDNwZTDaTSHqJc";

        /// <summary>
        /// 服务器标签
        /// </summary>
        public const string ServerTag = "test_zef";        

        /// <summary>
        /// 域名
        /// </summary>
        public const string Domain = "pondmurmurs.link"; 
         
        /// <summary> 
        /// 游戏服连接组
        /// </summary> 
        public static string[] WebSocketUrls =
        {
            "wss://pondmurmurs.link",
            "wss://www.pondmurmurs.link",
            "wss://cloudflare.pondmurmurs.link",
        };

        /// <summary>
        /// 正服端口  
        /// </summary>
        public const string WebSocketPort = "/mm2-pro/websocket/";

        /// <summary>
        /// 测服端口
        /// </summary>
        public const string WebSocketTestPort = "/mm2-test/websocket/";

        /// <summary>
        /// 开发服端口
        /// </summary>
        public const string WebSocketDevPort = "/mm2-dev/websocket/";

        /// <summary>
        /// SDK接口前缀
        /// 根据产品发布的账号来填写: solitaire / slidey
        /// </summary>
        public const string SDKApiPrefix = AppName;

        /// <summary>
        /// BuglyAppIDForAndroid
        /// </summary>
        public const string BuglyAppIDForAndroid = "8d30676fa7";

        /// <summary>
        /// BuglyAppIDForiOS
        /// </summary>
        public const string BuglyAppIDForiOS = "7c5289d3ae";

        /// <summary>
        /// 是否弱联网
        /// </summary>
        public const bool IsWeakNetwork = false;    

        /// <summary>  
        /// 是否离线游戏
        /// </summary>
        public const bool IsOfflineGame = false;

        /// <summary>
        /// 自定义SDK组
        /// </summary>
        public static ISDK[] CustomSDKs;   

        /// <summary>
        /// 是否使用UGameAndroid编译(只影响Android编译，不影响iOS编译)
        /// </summary>
        public const bool IsUseUGameAndroid = true;
#endregion

#region 方法
        /// <summary>
        /// 项目入口
        /// </summary>
        public static void MainFunc()
        {
            // 设置分辨率
            //AppConst.StandardResolution = new Vector2Int(720, 1280);
        }

        /// <summary>
        /// 初始化前 自定义项目设置
        /// </summary>
        public static void InitFunc()
        {
            // 不使用内置设置
            //AppConst.UseInternalSetting = false;

            // 设置多语言
            //AppConst.IsMultiLangue = true;
            //AppConst.DefaultLangue = "en";

            // 不需要游戏载入界面
            //CommonConfig.LoadingUI = false;
            // 不需要App更新提醒
            //AppConst.IsNeedPromptAppUpdate = false;
        }

        /// <summary>
        /// 启动游戏 自定义项目设置
        /// </summary>
        public static void StartUpFunc()
        {
            // 比如不需要Loading延迟
            //AppConst.ShowLoadingSplashPageTime = 0;
            //AppConst.IsLoadingDelay = false;
            //AppConst.LoadingDelayTime = 0;
            //AppConst.LoadingCompleteDelayTime = 0;
            //AppConst.GameStartReadyDelayTime = 0;
        }

        /// <summary>
        /// 开始游戏前 自定义项目设置
        /// </summary>
        public static void GameStartFunc()
        {
            // 比如不需要主框架的mainCamera fguiCamera audioListener
            //CameraMgr.Instance.mainCamera.enabled = false;
            //CameraMgr.Instance.fguiCamera.enabled = false;
            //AudioMgr.Instance.audioListener.enabled = false;
        }
#endregion
    }

#elif UNITY_IOS

    /// <summary>
    /// 应用外观
    /// </summary>
    public static class AppFacade
    {
    #region 应用配置
        /// <summary>
        /// 应用代号
        /// </summary>
        public const string AppName = "solitaire";

        /// <summary>
        /// 项目描述
        /// </summary>
        public const string AppDesc = "主框架";

        /// <summary>
        /// 包名
        /// </summary>
        public const string PackageName = "com.games.free.solitaire";

        /// <summary>
        /// 密钥Key
        /// </summary>
        public const string AESKey = "6ijV8xZ4FJcGNrzg";

        /// <summary>
        /// 密钥IVector
        /// </summary>
        public const string AESIVector = "mPuqVGKJsQtNX7xd";

        /// <summary>
        /// 服务器标签
        /// </summary>
        public const string ServerTag = "test_zef";

        /// <summary>
        /// 游戏服连接组
        /// </summary>
        public static string[] WebSocketUrls =
        {
            "wss://aoecat.cc", // 官服
            "wss://d3nkreroeqacbs.cloudfront.net", // AWS CDN
            "wss://cloudflare.aoecat.cc", // Cloudflare CDN
        };

        /// <summary>
        /// 正服端口
        /// </summary>
        public const string WebSocketPort = "/8006/";

        /// <summary>
        /// 测服端口
        /// </summary>
        public const string WebSocketTestPort = "/8056/";

        /// <summary>
        /// 域名
        /// </summary>
        public const string Domain = "solitaire.cc";

        /// <summary>
        /// SDK接口前缀
        /// 根据产品发布的账号来填写: solitaire / slidey
        /// </summary>
        public const string SDKApiPrefix = AppName;

        /// <summary>
        /// BuglyAppIDForAndroid
        /// </summary>
        public const string BuglyAppIDForAndroid = "8d30676fa7";

        /// <summary>
        /// BuglyAppIDForiOS
        /// </summary>
        public const string BuglyAppIDForiOS = "7c5289d3ae";

        /// <summary>
        /// 是否弱联网
        /// </summary>
        public const bool IsWeakNetwork = false;

        /// <summary>
        /// 是否离线游戏
        /// </summary>
        public const bool IsOfflineGame = false;

        /// <summary>
        /// 自定义SDK组
        /// </summary>
        public static ISDK[] CustomSDKs = new ISDK[]{ new BasePlatformOS(), new BaseChannel(), new BaseCoreSDK() };

        /// <summary>
        /// 是否使用UGameAndroid编译(只影响Android编译，不影响iOS编译)
        /// </summary>
        public const bool IsUseUGameAndroid = true;
    #endregion

    #region 方法
        /// <summary>
        /// 项目入口
        /// </summary>
        public static void MainFunc()
        {
            // 设置分辨率
            //AppConst.StandardResolution = new Vector2Int(720, 1280);
        }

        /// <summary>
        /// 初始化前 自定义项目设置
        /// </summary>
        public static void InitFunc()
        {
            // 不使用内置设置
            //AppConst.UseInternalSetting = false;

            // 设置多语言
            //AppConst.IsMultiLangue = true;
            //AppConst.DefaultLangue = "en";

            // 不需要游戏载入界面
            //CommonConfig.LoadingUI = false;
            // 不需要App更新提醒
            //AppConst.IsNeedPromptAppUpdate = false;
        }

        /// <summary>
        /// 启动游戏 自定义项目设置
        /// </summary>
        public static void StartUpFunc()
        {
            // 比如不需要Loading延迟
            //AppConst.ShowLoadingSplashPageTime = 0;
            //AppConst.IsLoadingDelay = false;
            //AppConst.LoadingDelayTime = 0;
            //AppConst.LoadingCompleteDelayTime = 0;
            //AppConst.GameStartReadyDelayTime = 0;
        }

        /// <summary>
        /// 开始游戏前 自定义项目设置
        /// </summary>
        public static void GameStartFunc()
        {
            // 比如不需要主框架的mainCamera fguiCamera audioListener
            //CameraMgr.Instance.mainCamera.enabled = false;
            //CameraMgr.Instance.fguiCamera.enabled = false;
            //AudioMgr.Instance.audioListener.enabled = false;
        }
    #endregion
    }
#endif
}