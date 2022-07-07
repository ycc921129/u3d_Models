/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;

namespace FutureCore
{
    /// <summary>
    /// Loading进度流程状态
    /// </summary>
    public enum ProgressState : int
    {
        /// <summary>
        /// 未启动 = -10
        /// </summary>
        Unstart_no = -10,
        /// <summary>
        /// 资源版本热更新 = 0
        /// </summary>
        //[InspectorName("资源版本热更新")]
        VersionUpdate_0 = 0,
        /// <summary>
        /// 资源初始化 = 20
        /// </summary>
        AssetsInit_20 = 20,
        /// <summary>
        /// 基础资源初始化 = 25
        /// </summary>
        PermanentAssetsInit_25 = 25,
        /// <summary>
        /// 连接登录 = 30
        /// </summary>
        ConnectLogin_30 = 30,
        /// <summary>
        /// 远程存储初始化 = 60
        /// </summary>
        PreferencesInit_60 = 60,
        /// <summary>
        /// 配置表初始化 = 70
        /// </summary>
        ConfigInit_70 = 70,
        /// <summary>
        /// 预加载开始 = 80
        /// </summary>
        PreloadStart_80 = 80,
        /// <summary>
        /// 显示场景 = 100
        /// </summary>
        ShowScene_100 = 100,
    }

    public class UserInfo
    {
        public string userId;
    }

    /// <summary>
    /// 全局应用
    /// 应用层逻辑注入到框架层
    /// </summary>
    public static class App
    {
        #region User
        public static UserInfo UserInfo;
        #endregion User

        #region Application
        private static FCApplication currApplication = null;
        private static ProgressState currProgressState = ProgressState.Unstart_no; 

        public static void InitApplication(FCApplication application)
        {
            currApplication = application;
            currApplication.Init();
            currApplication.Enable();
        }

        public static ProgressState GetCurrProgressState()
        {
            return currProgressState;
        }

        public static int GetCurrProgressStateValue()
        {
            return (int)currProgressState;
        }

        public static void Restart()
        {
            currApplication.Restart();
        }

        public static void Quit()
        {
            currApplication.Quit();
        }
        #endregion Application

        #region AppFacade
        public static string GetAppName()
        {
            return AppFacade_Frame.AppName;
        }

        public static string GetPackageName()
        {
            return AppFacade_Frame.PackageName;
        }

        public static string GetAESKey()
        {
            return AppFacade_Frame.AESKey;
        }

        public static string GetAESIVector()
        {
            return AppFacade_Frame.AESIVector;
        }

        public static string[] GetWebSocketUrls()
        {
            return AppFacade_Frame.WebSocketUrls;
        }

        public static string GetWebSocketPort()
        {
            return AppFacade_Frame.WebSocketPort;
        }

        public static string GetWebSocketTestPort()
        {
            return AppFacade_Frame.WebSocketTestPort;
        }

        public static string GetWebSocketDevPort()
        {
            return AppFacade_Frame.WebSocketDevPort;
        }

        public static string GetDomain()
        {
            return AppFacade_Frame.Domain;
        }

        public static string GetSDKApiPrefix()
        {
            return AppFacade_Frame.SDKApiPrefix;
        }

        public static string GetBuglyAppIDForAndroid()
        {
            return AppFacade_Frame.BuglyAppIDForAndroid;
        }

        public static string GetBuglyAppIDForiOS()
        {
            return AppFacade_Frame.BuglyAppIDForiOS;
        }

        public static bool GetIsWeakNetwork()
        {
            return AppFacade_Frame.IsWeakNetwork;
        }

        public static bool GetIsOfflineGame()
        {
            return AppFacade_Frame.IsOfflineGame;
        }

        public static ISDK[] GetCustomSDKs()
        {
            return AppFacade_Frame.CustomSDKs;
        }

        public static bool GetIsUseUGameAndroid()
        {
            return AppFacade_Frame.IsUseUGameAndroid;
        }

        public static string GetAppDesc()
        {
            return AppFacade_Frame.AppDesc;
        }

        public static void AppFacadeInit()
        {
            if (AppFacade_Frame.InitFunc != null)
            {
                AppFacade_Frame.InitFunc();
            }
        }

        public static void AppFacadeStartUp()
        {
            if (AppFacade_Frame.StartUpFunc != null)
            {
                AppFacade_Frame.StartUpFunc();
            }
        }

        public static void AppFacadeGameStart()
        {
            if (AppFacade_Frame.GameStartFunc != null)
            {
                AppFacade_Frame.GameStartFunc();
            }
        }
        #endregion

        #region UIMsg

        private static float LoadingProgressDelayTime = 0f;

        public static void DisplayLoadingUI()
        {
            AppDispatcher.Instance.Dispatch(AppMsg.UI_DisplayLoadingUI);
        }

        public static void HideLoadingUI(bool isDelay = false)
        {
            if (!isDelay)
            {
                AppDispatcher.Instance.Dispatch(AppMsg.UI_HideLoadingUI);
                return;
            }
            TimerUtil.Simple.AddTimer(0.5f, () =>
            {
                AppDispatcher.Instance.Dispatch(AppMsg.UI_HideLoadingUI);
            });
        }

        public static void SetLoadingUI(ProgressState state, bool isDelay = false)
        {
            currProgressState = state;
            SetLoadingUI(state.ToString(), (int)state, isDelay);
        }

        public static void SetLoadingUI(string info, int progress, bool isDelay = false)
        {
            SetLoadingUIInfo(info);
            SetLoadingUIProgress(progress, isDelay);
        }

        public static void SetLoadingUIInfo(string info)
        {
            AppDispatcher.Instance.Dispatch(AppMsg.UI_SetLoadingUIInfo, info);
        }

        public static void SetLoadingUIProgress(int progress, bool isDelay = false)
        {
            if (!isDelay)
            {
                AppDispatcher.Instance.Dispatch(AppMsg.UI_SetLoadingUIProgress, progress);
                return;
            }

            LoadingProgressDelayTime += AppConst.LoadingDelayTime;
            TimerUtil.Simple.AddTimer(LoadingProgressDelayTime, () =>
            {
                AppDispatcher.Instance.Dispatch(AppMsg.UI_SetLoadingUIProgress, progress);
            });
        }

        public static void SetLoadingUIProgressComplete(bool isDelay = false)
        {
            if (!isDelay)
            {
                AppDispatcher.Instance.Dispatch(AppMsg.UI_LoadingUIProgressComplete, 100);
                return;
            }

            LoadingProgressDelayTime += AppConst.LoadingCompleteDelayTime + (AppConst.LoadingDelayTime * 2);
            TimerUtil.Simple.AddTimer(LoadingProgressDelayTime, () =>
            {
                LoadingProgressDelayTime = 0;
                AppDispatcher.Instance.Dispatch(AppMsg.UI_LoadingUIProgressComplete, 100);
            });
        }

        public static void DisplayWaitUI()
        {
            AppDispatcher.Instance.Dispatch(AppMsg.UI_DisplayWaitUI);
        }

        public static void DisplayWaitTimeUI(float funcTime, Action func)
        {
            WaitTimeActionClass waitObj = new WaitTimeActionClass(funcTime, func);
            AppDispatcher.Instance.Dispatch(AppMsg.UI_DisplayWaitTimeUI, waitObj);
        }

        public static void HideWaitUI()
        {
            AppDispatcher.Instance.Dispatch(AppMsg.UI_HideWaitUI);
        }

        public static void ShowTipsUI(string text)
        {
            AppDispatcher.Instance.Dispatch(AppMsg.UI_ShowTipsUI, text);
        }

        public static void ShowAffirmUI(string contentInfo, string affirmInfo, string cancelInfo, Action affirmFunc, Action cancelFunc)
        {
            AppDispatcher.Instance.Dispatch(AppMsg.UI_ShowAffirmUI);
        }

        public static void ShowPlatformTipsUI(string text)
        {
            AppDispatcher.Instance.Dispatch(AppMsg.UI_ShowPlatformTipsUI, text);
        }
        #endregion UIMsg
    }
}