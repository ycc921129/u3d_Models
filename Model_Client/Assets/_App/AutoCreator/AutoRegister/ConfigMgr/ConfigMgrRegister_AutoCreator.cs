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
            configMgr.AddConfigVOModel(ConfigConst.GameMissions, GameMissionsVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.Insterstitial, InsterstitialVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.LangueConfig, LangueConfigVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.LangueGame, LangueGameVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.Langue, LangueVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.MMCoin, MMCoinVOModel.Instance);
            configMgr.AddConfigVOModel(ConfigConst.ShareConfig, ShareConfigVOModel.Instance);
        }
    }
}