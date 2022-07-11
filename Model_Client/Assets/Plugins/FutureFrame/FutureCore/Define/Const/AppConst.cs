/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.12
*/

/*
    #define
    #undef
    [Conditional()]

    引擎预编译平台宏
    UNITY_EDITOR : 编辑器
    UNITY_ANDROID : 安卓
    UNITY_IOS : iOS
    UNITY_STANDALONE : PC

    引擎预编译宏
    ENABLE_PROFILE : 引擎性能分析

    插件预编译宏
    ODIN_INSPECTOR : Odin

    项目模式预编译宏
    C_NO_IN_UNITY : 不在Unity环境中
    C_DESIGNER_MODE : 策划编辑模式
    C_NET_CUSTOM_PROTOCOL : 网络自定义协议
    C_USE_DRAGON_BONES : 使用龙骨

    项目调试预编译宏
    C_DEBUG_AB_PROFILER : 调试AB分析器
    C_DEBUG_AB : 调试AB
    C_DEBUG_NET : 调试网络

    项目发布预编译宏
    C_INTERNAL_DLL : 内部DLL
    C_HOTFIX_DLL : 热更新DLL
    C_ENABLE_NORMAL_LOG : 开启普通日志
    C_ENABLE_WARN_LOG : 开启警告日志
    C_ENABLE_ERROR_LOG : 开启错误日志
*/

using System;
using System.Collections.Generic;
using FuturePlugin;
using UnityEngine;

namespace FutureCore
{
    public static class AppConst
    {
        #region Field
        // 包名
        public static string PackageName = AppFacade_Frame.PackageName;
        // 是否是调试版本
        public static bool IsDebugVersion = true;
        // 包版本号
        public static string PackageVersion = "null";
        // 系统+版本
        public static string Package_os_ver = "STANDALONE";
        //运营商
        public static string Package_imsi = "CMCC";
        // 内置配置表哈希值
        public static string ConfigInternalHash = null;
        // 内置配置表版本号
        public static string ConfigInternalVersion = null;
        // 服务器配置表哈希值
        public static string ConfigServerHash = null;
        // 服务器配置表版本号
        public static string ConfigServerVersion = null;

        // 默认资源模式 是否Resources模式
        public const bool IsResourcesMode_Default = true;
        // 默认加载模式 是否同步加载模式
        public const bool IsSyncLoadMode_Default = false;

        // 是否是开发构建
        public const bool IsDevelopmentBuild = false;
        // 是否开发模式 (不拷贝资源)
        public static bool IsDevelopMode = true;
        // 是否调试模式
        public static bool IsDebugMode = true;
        // 是否热更新模式
        public static bool IsHotUpdateMode = false;
        // 是否开启调试引擎分析器
        public static bool IsEnabledEngineProfiler = false;
        // 是否开启调试应用分析器
        public static bool IsEnableAssistAppProfiler = false;
        // 是否开启调试应用控制台
        public static bool IsEnableAssistAppConsole = false;
        // 是否开启引擎调试日志
        public static bool IsEnabledEngineLog = true;
        // 是否开启日志
        public static bool IsEnabledLog = true;
        // 是否显示网络日志
        public const bool IsDisplayNetLog = false;
        // 是否显示网络协议通讯日志
        public const bool IsDisplayNetProtoLog = true;

        public const LogType EnabledFilterLogType = LogType.Log | LogType.Warning | LogType.Error | LogType.Assert | LogType.Exception; //(LogType.Log | LogType.Warning | LogType.Error | LogType.Assert | LogType.Exception) &~ LogType.Log;
        public const bool IsRunInBG = true;
        public const int SleepTimeoutMode = SleepTimeout.NeverSleep;
        public const int AntiAliasing = 0;
        public const int HighFrameRate = 60;
        public const int LowFrameRate = 45;
        public const float HDHighViewScale = 1f;
        public const float HDLowViewScale = 0.9f;
        public const float PixelsPerUnit = 100f;
        public static float FrameRateTimestep = 1f / HighFrameRate;
        public const float LowFrameRateTimestep = 1f / LowFrameRate;
        // Streaming Assets下默认是不压缩的
        // 将此后缀名加入到安卓Gradle工程的android_aaptOptions_noCompress中, 即可保证在打包apk的时候不要压缩这部分资源即可
        public const string ABExtName = ".bytes";
        public static bool HasAssetPackage = true;

        /// 应用设置参数
        // 标准分辨率
        public static Vector2Int StandardResolution = new Vector2Int(720, 1280);
        // UI分辨率
        public static Vector2Int UIResolution = new Vector2Int(720, 1280);
        // PC测试分辨率
        public static Vector2Int PCTestResolution = new Vector2Int(720, 1280);
        // 是否编辑器加载内置配置
        public static bool IsConfigEditorLoadInternally = true;
        // 是否允许配置表回滚
        public static bool IsConfigRollback = false;
        // 是否配置表提前本地初始化
        public static bool IsConfigPreInit = false;
        // 使用内置设置
        public static bool UseInternalSetting = true;
        // 安卓SDK类名
        public static string AndroidSDKClassName = "com.unity3d.player.UnityPlayer";  
        // 渠道测试_Debug版本 
        public static bool ChannelTest_Debug = true;      
        // 渠道测试_Debug版本_是否是开发环境
        public static bool Unity_dev = false;   
        // 渠道测试_Debug版本_是否使用国内链接
        public static bool Unity_dev_CH = true;
        // 渠道测试_编译类型  
        public static AppBuildType ChannelTest_BuildType = AppBuildType.Debug;
        // 渠道测试_开发机版本号
        public static int ChannelTest_VerCode = 1; 
        // 渠道测试_是否缓存插屏广告完成
        public static bool ChannelTest_IsPreLoadIntersititialAd = true;
        // 渠道测试_视频广告是否缓存成功
        public static bool ChannelTest_isPreLoadVideoAdSuccess = true;
        // 是否多语言
        public static bool IsMultiLangue = true;
        // 当前多语言
        public static string CurrMultiLangue = "en";
        // 默认语言
        public static string DefaultLangue = "en";    
        // 内置语言
        public static string InternalLangue = "en";
        // 是否提示App更新
        public static bool IsNeedPromptAppUpdate = true; 
        // 显示拉起应用闪屏时间
        public static float ShowLoadingSplashPageTime = 0.5f;
        // Loading进度进度是否延迟
        public static bool IsLoadingDelay = true;
        // Loading进度延迟时间
        public static float LoadingDelayTime = LowFrameRateTimestep;
        // Loading进度完成延迟时间
        public static float LoadingCompleteDelayTime = 0.5f;
        // 准备游戏开始前延迟时间
        public static float GameStartReadyDelayTime = 0.5f;
        // LogsViewer拉起的圈数 (Debug模式)
        public static int LogsViewerShowNum_Debug = 10;
        // LogsViewer拉起的圈数 (Release模式)
        public static int LogsViewerShowNum_Release = 50;
        /// 项目控制参数
        // 控制器关闭列表
        public static List<string> CtrlDisableList = new List<string>();

        //-----------------------------------------------------运行时参数-------------------------------------------------------------//

        // 是否发布版应用
        public static bool IsReleaseApp;
        // 渠道类型
        public static ChannelType ChannelType;
        // 应用版本
        public static string[] AppVersions;

        // 默认资源版本
        private static string[] DefaultAssetVersions = new string[] { "0", "0", "0" };
        // 本地资源版本
        public static string[] LocalAssetVersions = DefaultAssetVersions;
        // 服务器资源版本
        public static string[] ServerAssetVersions = DefaultAssetVersions;

        // 应用启动时间
        public static DateTime LaunchDateTime;
        #endregion

        #region Func
        public static void Init()
        {
            // 初始化常量
            PackageVersion = Application.version;
            FrameRateTimestep = 1f / Application.targetFrameRate;

            if (Application.isEditor)
            {
                // 1) 编辑器测试配置
                IsDevelopMode = true;
                IsHotUpdateMode = false;
                IsDebugMode = true;

                // 2) 编辑器正式配置
                //IsDevelopMode = false;
                //IsHotUpdateMode = true;
                //IsDebugMode = false;
            }
            else
            {
                // 1) 测试配置
                IsDevelopMode = true;
                IsHotUpdateMode = false;
                IsDebugMode = true;

                // 2) 正式测试配置
                //IsDevelopMode = false;
                //IsHotUpdateMode = true;
                //IsDebugMode = true;

                // 2) 正式配置
                //IsDevelopMode = false;
                //IsHotUpdateMode = true;
                //IsDebugMode = false;
                //IsEnabledDebugLog = false;

                // 3) 非热更正式配置
                //IsDevelopMode = true;
                //IsHotUpdateMode = false;
                //IsDebugMode = false;
            }
        }

        public static void AfterInit()
        {
            IsReleaseApp = VersionMgr.Instance.IsRelease;
            ChannelType = VersionMgr.Instance.ChannelType;
            AppVersions = VersionMgr.Instance.AppVersions;
        }

        public static void UpdateFrameRate()
        {
            FrameRateTimestep = 1f / Application.targetFrameRate;
        }
        #endregion
    }
}