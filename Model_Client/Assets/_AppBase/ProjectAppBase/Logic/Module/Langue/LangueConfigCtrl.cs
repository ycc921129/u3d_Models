/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

/*
    【19种语言码】Google语种翻译标准 ISO 639-1 langue code 19个
    en英语 zh繁体中文(默认) zh_CN简体中文 es西班牙 fr法国 ja日本 de德国 ru俄罗斯 pt葡萄牙
    @in印度尼西亚(in是关键字，需要加@) hi_IN印地语 vn越南 tr土耳其 ar阿拉伯 pl波兰 th泰国 kr韩国 uk乌克兰 ro罗马尼亚

    https://zh.wikipedia.org/wiki/ISO_639-1%E4%BB%A3%E7%A0%81%E8%A1%A8
*/

using ProjectApp.Data;

namespace ProjectApp
{
    /// <summary>
    /// 国际化接口
    /// </summary>
    public static class I18N_Config
    {
        public static bool IsUse()
        {
            return LangueConfigCtrl.Instance.IsUse();
        }

        public static string Get(int id)
        {
            return LangueConfigCtrl.Instance.Get(id);
        }

        public static string Get(int id, string rawText)
        {
            return LangueConfigCtrl.Instance.Get(id, rawText);
        }

        public static string Get(string key)
        {
            return LangueConfigCtrl.Instance.Get(key);
        }

        public static string Get(string key, string rawText)
        {
            return LangueConfigCtrl.Instance.Get(key, rawText);
        }
    }

    /// <summary>
    /// 动态多语言控制器
    /// </summary>
    public class LangueConfigCtrl : LangueCtrl
    {
        public static new LangueConfigCtrl Instance { get; private set; }

        protected override void OnInit()
        {
            Instance = this;
            SetLangueVOModel();
        }

        protected override void OnDispose()
        {
            Instance = null;
        }

        protected override void SetLangueVOModel()
        {
            langueVOModel = LangueConfigVOModel.Instance;
        }
    }
}