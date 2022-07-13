/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using FuturePlugin;
using System.Collections.Generic;

namespace ProjectApp
{
    public class MainScene : BaseScene
    {
        public override int SceneIdx { get { return SceneConst.MainIdx; } }
        public override int PreLoadId { get { return PreLoadIdConst.MainScene; } }

        private static AssetLoader m_loader;
        public static AssetLoader Loader
        {
            get
            {
                if (m_loader == null)
                {
                    m_loader = new AssetLoader();
                }
                return m_loader;
            }
        }

        private Dictionary<string, UAssetType> preLoadAssetDict = new Dictionary<string, UAssetType>();

        public MainScene()
        {
        }

        public override AssetLoader GetLoader()
        {
            return Loader;
        }

        protected override void OnEnter()
        {
            // 统计登录漏斗1
            LoginStatistics.InitFunnel();

            AppGlobal.IsGameStart = false;
            App.SetLoadingUI(ProgressState.VersionUpdate_0, AppConst.IsLoadingDelay);
        }

        protected override void OnLeave()
        { 
            LoadPipelineMgr.Instance.UnloadPreLoad(PreLoadId);
        }

        protected override void OnSwitchSceneComplete(object param = null)
        {
            // 统计
            ChannelMgr.Instance.SendStatisticEvent(StatisticConst.init_loading_page);
            // 统计登录时长
            ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.login_succeed);
            if (ProjectApplication.Instance.IsNewInstall)
            {
                ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.new_user_login_succeed);
            }
            // 统计加载时长
            ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.loading_time);
            if (ProjectApplication.Instance.IsNewInstall)
            {
                ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.new_user_loading_time);
            }

            StartUpAppProcess();
            TimerUtil.Simple.AddTimer(AppConst.ShowLoadingSplashPageTime, () => Channel.Current.hideSplashPage());
        }

        private void StartUpAppProcess()
        {
            LogUtil.Log("[MainScene]Start Up App Process");
            AppDispatcher.Instance.Dispatch(AppMsg.App_StartUp);            

            // 初始化资源
            if (!AppConst.IsDevelopMode)
            {
                VersionUpdateMgr.Instance.StartUpProcess(InitAssets);
            }
            else
            {
                InitAssets();
            }
        }

        private void InitAssets()
        {
            AppDispatcher.Instance.AddListener(AppMsg.System_AssetsInitComplete, OnAssetsInitComplete);

            LogUtil.Log("[MainScene]Init Assets");
            App.SetLoadingUI(ProgressState.AssetsInit_20, AppConst.IsLoadingDelay);
            ResMgr.Instance.InitAssets();
        }

        private void OnAssetsInitComplete(object param = null)
        {
            AppDispatcher.Instance.RemoveListener(AppMsg.System_AssetsInitComplete, OnAssetsInitComplete);
            AppDispatcher.Instance.AddListener(AppMsg.System_PermanentAssetsInitComplete, OnPermanentAssetsInitComplete);

            App.SetLoadingUI(ProgressState.PermanentAssetsInit_25, AppConst.IsLoadingDelay);
            ResMgr.Instance.InitPermanentAssets();
        }

        private void OnPermanentAssetsInitComplete(object param = null)
        {
            // 登录漏斗统计2
            LoginStatistics.AddFunnelData_connectlogin_2();

            AppDispatcher.Instance.RemoveListener(AppMsg.System_PermanentAssetsInitComplete, OnPermanentAssetsInitComplete);
            CtrlDispatcher.Instance.AddListener(CtrlMsg.Login_Succeed, OnLoginSucceed);
            CtrlDispatcher.Instance.AddListener(CtrlMsg.WeakNetwork_LoginSucceed, OnLoginSucceed);
              
            App.SetLoadingUI(ProgressState.ConnectLogin_30, AppConst.IsLoadingDelay);
            LoginCtrl.Instance.InitLogin();

            if (App.GetIsWeakNetwork())
            {
                AttemptWeakNetworkMode();
            }
        }

        private void AttemptWeakNetworkMode()
        {
            // 弱联网模式
            float weakNetWaitTime = 6f;
            if (App.GetIsOfflineGame())
            {
                weakNetWaitTime = 0f;
            }

            TimerUtil.Simple.AddTimer(weakNetWaitTime, () =>
            {
                if (LoginCtrl.Instance.hasUpdateApkUIDisplay) return;
                if (WeakNetworkCtrl.Instance.IsCanOfflineLogin())
                {
                    LogUtil.Log("[MainScene]Can Offline Login");
                    WeakNetworkCtrl.Instance.SendOfflineLoginReq();
                }
                else
                {
                    UICtrlDispatcher.Instance.Dispatch(UICtrlMsg.ReconnectUI_Open);
                }
            });
        }

        private void OnLoginSucceed(object param = null)
        {
            // 登录漏斗统计4
            LoginStatistics.AddFunnelData_preferencesinitstart_4();

            CtrlDispatcher.Instance.RemoveListener(CtrlMsg.Login_Succeed, OnLoginSucceed);
            CtrlDispatcher.Instance.RemoveListener(CtrlMsg.WeakNetwork_LoginSucceed, OnLoginSucceed);

            App.SetLoadingUI(ProgressState.PreferencesInit_60, AppConst.IsLoadingDelay);
            PreferencesMgr.Instance.InitPreferences();

            // 登录漏斗统计6
            LoginStatistics.AddFunnelData_preferencesinitend_6();

            OnPreferencesInitComplete();
        }

        private void OnPreferencesInitComplete()
        {
            // 登录漏斗统计7
            LoginStatistics.AddFunnelData_configinit_7();

            AppDispatcher.Instance.AddListener(AppMsg.System_ConfigInitComplete, OnConfigInitComplete);

            App.SetLoadingUI(ProgressState.ConfigInit_70, AppConst.IsLoadingDelay);
            if (AppConst.IsConfigPreInit && ConfigMgr.Instance.IsParsedClientConfig)
            {
                OnConfigInitComplete();
            }
            else
            {
                ConfigMgr.Instance.ParseAllClientConfig();
            }
        }

        private void OnConfigInitComplete(object param = null)
        {
            AppDispatcher.Instance.RemoveListener(AppMsg.System_ConfigInitComplete, OnConfigInitComplete);
            AppDispatcher.Instance.AddListener(AppMsg.UI_LoadingUIProgressComplete, OnLoadingComplete);

            App.SetLoadingUI(ProgressState.PreloadStart_80, AppConst.IsLoadingDelay);
            LoadPipelineMgr.Instance.PreLoad(PreLoadId, Loader, preLoadAssetDict, null, OnPreLoadComplete);
        }

        private void OnPreLoadComplete()
        {
            App.SetLoadingUI(ProgressState.ShowScene_100, AppConst.IsLoadingDelay);
            App.SetLoadingUIProgressComplete(AppConst.IsLoadingDelay);
        }

        private void OnLoadingComplete(object param = null)
        {
            // 登录漏斗统计8
            LoginStatistics.AddFunnelData_loadcomplete_8();

            AppDispatcher.Instance.RemoveListener(AppMsg.UI_LoadingUIProgressComplete, OnLoadingComplete);
            
            CtrlDispatcher.Instance.Dispatch(CtrlMsg.Preferences_InitComplete);

            CtrlDispatcher.Instance.Dispatch(CtrlMsg.Game_StartReady);

            TimerUtil.Simple.AddTimer(AppConst.GameStartReadyDelayTime, () =>
            {
                OperationGameStartStatistic(); 

                AudioMgr.Instance.InitDefaultButtonClickSound(AudioConst.UIButtonDefault);
                AudioMgr.Instance.PlayBGM(AudioConst.BGMMainScene);

                App.AppFacadeGameStart();
                ModuleMgr.Instance.AllModuleGameStart();
                ShowScene();
                HideLoadingUI();

                // FB统计
                ChannelMgr.Instance.SendFBEvent(FBStatisticConst.EVENT_NAME_ACTIVATED_APP);
                // 登录漏斗统计9
                LoginStatistics.AddFunnelData_gamestart_9();
            });
        }

        private void OperationGameStartStatistic()
        {            
            // 统计进入游戏时长
            int upperLimit = 120 * 1000;
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.loading_time, upperLimit);
            if (ProjectApplication.Instance.IsNewInstall)
            {
                ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.new_user_loading_time, upperLimit);
            }
            // 统计进入游戏
            ChannelMgr.Instance.SendStatisticEvent(StatisticConst.init_main_page);
            if (ProjectApplication.Instance.IsNewInstall)
            {
                ChannelMgr.Instance.SendStatisticEvent(StatisticConst.new_user_init_main_page);
            }
        }

        private void ShowScene()
        {
            LogUtil.Log("<color=green>[MainScene]ShowScene</color>");
            CtrlDispatcher.Instance.Dispatch(CtrlMsg.Game_StartBefore);
            CtrlDispatcher.Instance.Dispatch(CtrlMsg.Game_Start);
            CtrlDispatcher.Instance.Dispatch(CtrlMsg.Game_StartLater);
            AppGlobal.IsGameStart = true;            
        }

        public override void Dispose()
        {
            if (null != m_loader)
            {
                m_loader.Dispose();
                m_loader = null;
            }
        }
    }
}