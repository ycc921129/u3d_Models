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
            UI.CS608_loading.CS608_loadingBinder.BindAll();
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