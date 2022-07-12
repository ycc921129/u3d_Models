/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using FutureCore;

namespace ProjectApp
{
    public static class ModuleMgrRegister
    {
        public static void AutoRegisterModel()
        {
            ModuleMgr moduleMgr = ModuleMgr.Instance;
            moduleMgr.AddModel(ModelConst.LoginModel, new LoginModel());
        }

        public static void AutoRegisterUIType()
        {
            ModuleMgr moduleMgr = ModuleMgr.Instance;
            moduleMgr.AddUIType(UIConst.GameLoadingUI, typeof(GameLoadingUI));
            moduleMgr.AddUIType(UIConst.LoginUI, typeof(LoginUI));
            moduleMgr.AddUIType(UIConst.ReconnectUI, typeof(ReconnectUI));
            moduleMgr.AddUIType(UIConst.LoadingUI, typeof(LoadingUI));
            moduleMgr.AddUIType(UIConst.TipsUI, typeof(TipsUI));
        }

        public static void AutoRegisterCtrl()
        {
            ModuleMgr moduleMgr = ModuleMgr.Instance;
            moduleMgr.AddCtrl(CtrlConst.ChannelMsg_CommonCtrl, new ChannelMsg_CommonCtrl());
            moduleMgr.AddCtrl(CtrlConst.CommonInitCtrl, new CommonInitCtrl());
            moduleMgr.AddCtrl(CtrlConst.PreferencesDataReadyCtrl, new PreferencesDataReadyCtrl());
            moduleMgr.AddCtrl(CtrlConst.InterstitialCtrl, new InterstitialCtrl());
            moduleMgr.AddCtrl(CtrlConst.LangueConfigCtrl, new LangueConfigCtrl());
            moduleMgr.AddCtrl(CtrlConst.LangueCtrl, new LangueCtrl());
            moduleMgr.AddCtrl(CtrlConst.LangueGameCtrl, new LangueGameCtrl());
            moduleMgr.AddCtrl(CtrlConst.LoginCtrl, new LoginCtrl());
            moduleMgr.AddCtrl(CtrlConst.OfflineTimeCtrl, new OfflineTimeCtrl());
            moduleMgr.AddCtrl(CtrlConst.ReconnectCtrl, new ReconnectCtrl());
            moduleMgr.AddCtrl(CtrlConst.WeakNetworkCtrl, new WeakNetworkCtrl());
        }

        public static void AutoRegisterUICtrl()
        {
            ModuleMgr moduleMgr = ModuleMgr.Instance;
            moduleMgr.AddUICtrl(UICtrlConst.GameLoadingUICtrl, new GameLoadingUICtrl());
            moduleMgr.AddUICtrl(UICtrlConst.LoginUICtrl, new LoginUICtrl());
            moduleMgr.AddUICtrl(UICtrlConst.ReconnectUICtrl, new ReconnectUICtrl());
            moduleMgr.AddUICtrl(UICtrlConst.LoadingUICtrl, new LoadingUICtrl());
            moduleMgr.AddUICtrl(UICtrlConst.TipsUICtrl, new TipsUICtrl());
        }
    }
}