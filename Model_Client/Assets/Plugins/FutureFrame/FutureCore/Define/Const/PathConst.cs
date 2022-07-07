/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public static class PathConst
    {
        // 数据根目录
        public static string DataDir = PathUtil.RawDataPath + "Data/";
        // 配置表目录
        public static string ConfigDir = DataDir + "Config/";
        // 缓存目录
        public static string CacheDir = DataDir + "Cache/";
        // 运营配置
        public static string OperationConfigDir = ConfigDir + "OperationConfig/";
        // Wss配置表目录
        public static string WssConfigDir = ConfigDir + "WssConfig/";
        // 过时配置表目录
        public static string ObsoleteConfigDir = ConfigDir + "ObsoleteConfig/";

        // Wss模式游戏配置文件前缀
        public static string WssGameConfigFilePrefix = "W";
        // Wss模式游戏配置路径
        public static string WssGameConfigPath;

        // 缓存运营配置路径
        public static string CacheOperationConfigPath = OperationConfigDir + "OperationConfig" + AppConst.ABExtName;

        // 缓存本地登录消息路径
        public static string CacheS2CLoginMsgPath = CacheDir + "CacheLoginMsg" + AppConst.ABExtName;

        // 缓存服务器GameData路径
        public static string CacheServerGameDataPath = CacheDir + "CacheServerGameData" + AppConst.ABExtName;
        // 缓存本地GameData路径
        public static string CacheLocalGameDataPath = CacheDir + "CacheLocalGameData" + AppConst.ABExtName;

        // 缓存服务器Preferences路径
        public static string CacheServerPreferencesPath = CacheDir + "CacheServerPreferences" + AppConst.ABExtName;
        // 缓存本地Preferences路径
        public static string CacheLocalPreferencesPath = CacheDir + "CacheLocalPreferences" + AppConst.ABExtName;

        // 日志目录
        public static string LogDir = PathUtil.RawDataPath + "Log/";
        // 日志文件格式
        public static string LogFilesFormat = ".log";
        // 日志文件前缀
        public static string ReceivedLogPrefix = "Received_";
        // 异常日志文件前缀
        public static string UnhandledLogPrefix = "Unhandled_";
    }
}