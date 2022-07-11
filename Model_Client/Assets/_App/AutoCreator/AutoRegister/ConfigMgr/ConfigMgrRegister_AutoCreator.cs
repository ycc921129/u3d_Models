/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using FutureCore;
using ProjectApp.Data;

namespace ProjectApp
{
    public static class ConfigMgrRegister
    {
        public static void AutoRegisterConfig()
        {
            ConfigMgr configMgr = ConfigMgr.Instance;
            configMgr.AddConfigVOModel(ConfigConst.AppCommon, AppCommonVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.AppLocal, AppLocalVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.CardLocal, CardLocalVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.CheckIn, CheckInVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.ComboReward, ComboRewardVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.ComboReward_X, ComboReward_XVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.Combo, ComboVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.Combo_test, Combo_testVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.Combo_test_easy, Combo_test_easyVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.ExchangeRate, ExchangeRateVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.FacerobConst, FacerobConstVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.FacerobRewardConst, FacerobRewardConstVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.FlipReward, FlipRewardVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.FlipReward_X, FlipReward_XVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.Fragments, FragmentsVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.Fragments_Redeem, Fragments_RedeemVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.Fragments_X, Fragments_XVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.GiftRedeem, GiftRedeemVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.Insterstitial, InsterstitialVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.LangueConfig, LangueConfigVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.LangueGame, LangueGameVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.Langue, LangueVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.LevelAdd, LevelAddVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.MMCoin, MMCoinVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.MMTargetReward, MMTargetRewardVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.ShareConfig, ShareConfigVOModel.Instance);
        }
    }
}