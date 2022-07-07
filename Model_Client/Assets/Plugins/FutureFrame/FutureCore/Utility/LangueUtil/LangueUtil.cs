/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace FutureCore
{
    public static class LangueUtil
    {
        public static string GetCurrLangue()
        {
            // 判断非国区手机
            string[] otherLang = Channel.Current.lang.Split('_');
            if (otherLang.Length > 1)
            {
                return otherLang[0];
            }
            return Channel.Current.lang;
        }

        public static string GetCurrLangue(List<string> supportLangues)
        {
            string currLangue = GetLangue(Channel.Current.lang, supportLangues);
            if (currLangue != null)
            {
                return currLangue;
            }
            // 判断非国区手机
            string[] otherLang = Channel.Current.lang.Split('_');
            if (otherLang.Length > 2)
            {
                currLangue = GetLangue(otherLang[0] + "_" + otherLang[1], supportLangues);
                if (currLangue != null)
                {
                    return currLangue;
                }
            }
            currLangue = GetLangue(Channel.Current.langCode, supportLangues);
            if (currLangue != null)
            {
                return currLangue;
            }
            currLangue = GetLangue(AppConst.DefaultLangue, supportLangues);
            if (currLangue != null)
            {
                return currLangue;
            }
            return AppConst.DefaultLangue;
        }

        private static string GetLangue(string langue, List<string> supportLangues)
        {
            langue = langue.Replace("-", "_");
            if (!supportLangues.Contains(langue))
            {
                string @languageTemp = "@" + langue;
                if (!supportLangues.Contains(@languageTemp))
                {
                    LogUtil.LogFormat("[LangueUtil]GetLangue Failed: {0}", langue);
                    return null;
                }
                langue = @languageTemp;
            }
            LogUtil.LogFormat("[LangueUtil]GetLangue Success: {0}", langue);
            return langue;
        }
    }
}