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
            moduleMgr.AddModel(ModelConst.NetTimerModel, new NetTimerModel());
            moduleMgr.AddModel(ModelConst.NetworkErrorModel, new NetworkErrorModel());
        }

        public static void AutoRegisterUIType()
        {
            ModuleMgr moduleMgr = ModuleMgr.Instance;
            moduleMgr.AddUIType(UIConst.GameLoadingUI, typeof(GameLoadingUI));
            moduleMgr.AddUIType(UIConst.LoginUI, typeof(LoginUI));
            moduleMgr.AddUIType(UIConst.ReconnectUI, typeof(ReconnectUI));
            moduleMgr.AddUIType(UIConst.UpdataUI, typeof(UpdataUI));
            moduleMgr.AddUIType(UIConst.WaterMaskUI, typeof(WaterMaskUI));
            moduleMgr.AddUIType(UIConst.LoadingUI, typeof(LoadingUI));
            moduleMgr.AddUIType(UIConst.TipsUI, typeof(TipsUI));
        }

        public static void AutoRegisterCtrl()
        {
            ModuleMgr moduleMgr = ModuleMgr.Instance;
            moduleMgr.AddCtrl(CtrlConst.ChannelMsg_CommonCtrl, new ChannelMsg_CommonCtrl());
            moduleMgr.AddCtrl(CtrlConst.CommonInitCtrl, new CommonInitCtrl());
            moduleMgr.AddCtrl(CtrlConst.PreferencesDataReadyCtrl, new PreferencesDataReadyCtrl());
            moduleMgr.AddCtrl(CtrlConst.CoinCtrl, new CoinCtrl());
            moduleMgr.AddCtrl(CtrlConst.HeartBeatCtrl, new HeartBeatCtrl());
            moduleMgr.AddCtrl(CtrlConst.IAPCtrl, new IAPCtrl());
            moduleMgr.AddCtrl(CtrlConst.InfoCtrl, new InfoCtrl());
            moduleMgr.AddCtrl(CtrlConst.InviteCtrl, new InviteCtrl());
            moduleMgr.AddCtrl(CtrlConst.InterstitialCtrl, new InterstitialCtrl());
            moduleMgr.AddCtrl(CtrlConst.LangueConfigCtrl, new LangueConfigCtrl());
            moduleMgr.AddCtrl(CtrlConst.LangueCtrl, new LangueCtrl());
            moduleMgr.AddCtrl(CtrlConst.LangueGameCtrl, new LangueGameCtrl());
            moduleMgr.AddCtrl(CtrlConst.LoginCtrl, new LoginCtrl());
            moduleMgr.AddCtrl(CtrlConst.NetTimerCtrl, new NetTimerCtrl());
            moduleMgr.AddCtrl(CtrlConst.NetworkErrorCtrl, new NetworkErrorCtrl());
            moduleMgr.AddCtrl(CtrlConst.OfflineTimeCtrl, new OfflineTimeCtrl());
            moduleMgr.AddCtrl(CtrlConst.ReconnectCtrl, new ReconnectCtrl());
            moduleMgr.AddCtrl(CtrlConst.StatisticCtrl, new StatisticCtrl());
            moduleMgr.AddCtrl(CtrlConst.UserSessionCtrl, new UserSessionCtrl());
            moduleMgr.AddCtrl(CtrlConst.WeakNetworkCtrl, new WeakNetworkCtrl());
        }

        public static void AutoRegisterUICtrl()
        {
            ModuleMgr moduleMgr = ModuleMgr.Instance;
            moduleMgr.AddUICtrl(UICtrlConst.GameLoadingUICtrl, new GameLoadingUICtrl());
            moduleMgr.AddUICtrl(UICtrlConst.LoginUICtrl, new LoginUICtrl());
            moduleMgr.AddUICtrl(UICtrlConst.ReconnectUICtrl, new ReconnectUICtrl());
            moduleMgr.AddUICtrl(UICtrlConst.UpdataUICtrl, new UpdataUICtrl());
            moduleMgr.AddUICtrl(UICtrlConst.WaterMaskUICtrl, new WaterMaskUICtrl());
            moduleMgr.AddUICtrl(UICtrlConst.LoadingUICtrl, new LoadingUICtrl());
            moduleMgr.AddUICtrl(UICtrlConst.TipsUICtrl, new TipsUICtrl());
        }
    }
}