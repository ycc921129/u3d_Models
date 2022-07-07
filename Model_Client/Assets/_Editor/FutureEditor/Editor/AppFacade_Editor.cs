using FutureCore;
using ProjectApp;

namespace FutureEditor
{
    /// <summary>
    /// 应用外观_编辑器
    /// </summary>
    public static class AppFacade_Editor
    {
        /// <summary>
        /// 应用代号
        /// </summary>
        public const string AppName = AppFacade.AppName;

        /// <summary>
        /// 项目描述
        /// </summary>
        public const string AppDesc = AppFacade.AppDesc;

        /// <summary>
        /// 包名
        /// </summary>
        public const string PackageName = AppFacade.PackageName;

        /// <summary>
        /// 密钥Key
        /// </summary>
        public const string AESKey = AppFacade.AESKey;

        /// <summary>
        /// 密钥IVector
        /// </summary>
        public const string AESIVector = AppFacade.AESIVector;

        /// <summary>
        /// 服务器标签  
        /// </summary>
        public const string ServerTag = AppFacade.ServerTag;

        /// <summary>
        /// 游戏服连接组
        /// </summary>
        public static string[] WebSocketUrls = AppFacade.WebSocketUrls;  
        
        /// <summary>
        /// 域名
        /// </summary>
        public const string Domain = AppFacade.Domain;

        /// <summary>
        /// SDK接口前缀
        /// 根据产品发布的账号来填写: solitaire / slidey
        /// </summary>
        public const string SDKApiPrefix = AppFacade.SDKApiPrefix;

        /// <summary>
        /// BuglyAppIDForAndroid
        /// </summary>
        public const string BuglyAppIDForAndroid = AppFacade.BuglyAppIDForAndroid;

        /// <summary>
        /// BuglyAppIDForiOS
        /// </summary>
        public const string BuglyAppIDForiOS = AppFacade.BuglyAppIDForiOS;

        /// <summary>
        /// 是否弱联网
        /// </summary>
        public const bool IsWeakNetwork = AppFacade.IsWeakNetwork;

        /// <summary>
        /// 是否离线游戏
        /// </summary>
        public const bool IsOfflineGame = AppFacade.IsOfflineGame;

        /// <summary>
        /// 自定义SDK组
        /// </summary>
        public static ISDK[] CustomSDKs = AppFacade.CustomSDKs;

        /// <summary>
        /// 是否使用UGameAndroid编译(只影响android编译，不影响ios编译)
        /// </summary>
        public const bool IsUseUGameAndroid = AppFacade.IsUseUGameAndroid;
    }
}