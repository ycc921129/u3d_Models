/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using FairyGUI;
using FutureCore;
using System;
using UnityEngine;

namespace ProjectApp
{
    public class CommonLoadMgr : BaseMgr<CommonLoadMgr>
    {
        /// <summary>
        /// 获取游戏FGUI通用包中的图片
        /// </summary>
        /// <param name="name"></param>
        public string LoadGameFguiUrl(string name)
        {
            if (UIPackage.GetItemURL("A001_commonModule", name) != null)
                return UIPackage.GetItemURL("A001_commonModule", name);
            if (UIPackage.GetItemURL("A000_common", name) != null)
                return UIPackage.GetItemURL("A000_common", name);
            return null;
        }
    
        /// <summary>
        /// 获取国旗图片
        /// </summary>
        /// <param name="flag">国家</param>
        /// <returns></returns>
        public string GetFlag(string _flag)
        {
            string flag = _flag.ToLower();
            string url = LoadGameFguiUrl(CommonConst.defaultCountry_prefix + flag);
            if (url == null)
                url = LoadGameFguiUrl(CommonConst.defaultCountry);
            return url;
        }

       
    }
}
