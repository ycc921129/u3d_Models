/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp
{
    public static class FrameManagerRegister
    {
        public static void Register()
        {
            GlobalMgr globalMgr = GlobalMgr.Instance;
            // 模块管理器
            globalMgr.AddMgr(ModuleMgr.Instance);

            // PreMonoMgr
            globalMgr.AddMgr(AssistDebugMgr.Instance);

            // Mgr
            globalMgr.AddMgr(AssetBundleMgr.Instance);
            //globalMgr.AddMgr(CameraMgr.Instance);
            globalMgr.AddMgr(ConfigMgr.Instance);
            globalMgr.AddMgr(ConsoleMgr.Instance);
            globalMgr.AddMgr(DateTimeMgr.Instance);
            globalMgr.AddMgr(DispatcherMgr.Instance);
            globalMgr.AddMgr(GameMgr.Instance);
            globalMgr.AddMgr(GMMgr.Instance);
            globalMgr.AddMgr(GraphicsMgr.Instance);
            globalMgr.AddMgr(LoadPipelineMgr.Instance);
            globalMgr.AddMgr(ResMgr.Instance);
            globalMgr.AddMgr(SceneMgr.Instance);
            globalMgr.AddMgr(VersionMgr.Instance);

            // MonoMgr
            //globalMgr.AddMgr(AudioMgr.Instance);
            globalMgr.AddMgr(CoroutineMgr.Instance);
            globalMgr.AddMgr(HttpMgr.Instance);
            globalMgr.AddMgr(SceneSwitchMgr.Instance);
            globalMgr.AddMgr(ScreenshotMgr.Instance);
            globalMgr.AddMgr(TestMgr.Instance);
            //globalMgr.AddMgr(ThreadMgr.Instance);
            globalMgr.AddMgr(TickMgr.Instance);
            globalMgr.AddMgr(TimerMgr.Instance);
            //globalMgr.AddMgr(UIMgr.Instance);
            globalMgr.AddMgr(VersionUpdateMgr.Instance);
            globalMgr.AddMgr(WorldSpaceMgr.Instance);

            // AppMgr
            globalMgr.AddMgr(ChannelMgr.Instance);
            globalMgr.AddMgr(PreferencesMgr.Instance);

            // AppMonoMgr 
            globalMgr.AddMgr(AntiCheatMgr.Instance);
        }

        public static void RegisterData()
        {
            // SceneMgr
            SceneMgrRegister.AutoRegisterScene();
            // ConfigMgr
            ConfigMgrRegister.AutoRegisterConfig();
            // WSNetMgr
            WSNetMgrRegister.AutoRegisterProtoType();
            // ModuleMgr
            ModuleMgrRegister.AutoRegisterModel();
            ModuleMgrRegister.AutoRegisterUIType();
            ModuleMgrRegister.AutoRegisterCtrl();
            ModuleMgrRegister.AutoRegisterUICtrl();
            // UIMgr
            UIMgrRegister.AutoRegisterBinder();
            UIMgrRegister.AutoRegisterCommonPackages();
        }
    }
}