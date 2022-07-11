/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using FutureCore;
using FairyGUI;

namespace ProjectApp
{
    public static class UIMgrRegister
    {
        public static void AutoRegisterBinder()
        {
            UI.A001_commonModule.A001_commonModuleBinder.BindAll();
            UI.C505_tips.C505_tipsBinder.BindAll();
            UI.CS603_gameWindows.CS603_gameWindowsBinder.BindAll();
            UI.CS608_loading.CS608_loadingBinder.BindAll();
            UI.G001_tutorial.G001_tutorialBinder.BindAll();
            UI.G006_gameSetting.G006_gameSettingBinder.BindAll();
            UI.G017_gameRedeem.G017_gameRedeemBinder.BindAll();
            UI.G018_rewardCard.G018_rewardCardBinder.BindAll();
            UI.G019_redeemActivite.G019_redeemActiviteBinder.BindAll();
            UI.G020_skinRedeem.G020_skinRedeemBinder.BindAll();
            UI.G101_propWindow.G101_propWindowBinder.BindAll();
            UI.G103_endGame.G103_endGameBinder.BindAll();
            UI.G116_rate.G116_rateBinder.BindAll();
            UI.G501_encourage.G501_encourageBinder.BindAll();
            UI.MM101_basicInfo.MM101_basicInfoBinder.BindAll();
            UI.MM201_consumeCoinAni.MM201_consumeCoinAniBinder.BindAll();
            UI.MM202_rewardAni.MM202_rewardAniBinder.BindAll();
            UI.MM304_commonWindow.MM304_commonWindowBinder.BindAll();
            UI.MM507_commonMask.MM507_commonMaskBinder.BindAll();
            UI.MM513_Guide.MM513_GuideBinder.BindAll();
            UI.MM9_mmTool.MM9_mmToolBinder.BindAll();
        }

        public static void AutoRegisterCommonPackages()
        {
            List<string> commonPackages = new List<string>();

            commonPackages.Add("A000_common");
            commonPackages.Add("A002_bigBackground");

            UIMgr.Instance.RegisterCommonPackages(commonPackages);
        }
    }
}