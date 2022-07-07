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
            UI.C505_tips.C505_tipsBinder.BindAll();
            UI.CS603_gameWindows.CS603_gameWindowsBinder.BindAll();
            UI.CS608_loading.CS608_loadingBinder.BindAll();
        }

        public static void AutoRegisterCommonPackages()
        {
            List<string> commonPackages = new List<string>();

            commonPackages.Add("A001_commonModule");
            commonPackages.Add("A002_bigBackground");

            UIMgr.Instance.RegisterCommonPackages(commonPackages);
        }
    }
}