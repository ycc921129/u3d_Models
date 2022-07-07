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

using System.Collections.Generic;
using FutureCore;
using ProjectApp.Data;

namespace ProjectApp
{
    /// <summary>
    /// 国际化接口
    /// </summary>
    public static class I18N
    {
        public static bool IsUse()
        {
            return LangueCtrl.Instance.IsUse();
        }

        public static string Get(int id)
        {
            return LangueCtrl.Instance.Get(id);
        }

        public static string Get(int id, string rawText)
        {
            return LangueCtrl.Instance.Get(id, rawText);
        }

        public static string Get(string key)
        {
            return LangueCtrl.Instance.Get(key);
        }

        public static string Get(string key, string rawText)
        {
            return LangueCtrl.Instance.Get(key, rawText);
        }
    }

    /// <summary>
    /// 动态多语言控制器
    /// </summary>
    public class LangueCtrl : BaseCtrl
    {
        public static LangueCtrl Instance { get; private set; }

        private List<string> m_ignoreFields = new List<string> { "id", "key", "keyDesc", "desc" };
        protected IVOModel langueVOModel = null;

        public List<string> supportLangues { get; private set; }
        public string currLangue { get { return AppConst.CurrMultiLangue; } }

        private string deviceLangue = null;

        private List<string> langueNames = new List<string>
        {
            "English",
            "繁體中文",
            "简体中文",
            "Español",
            "français",
            "日本語",
            "Deutsch",
            "русский",
            "Português",
            "Bahasa Indonesia",
            "हिन्दी",
            "Tiếng Việt",
            "Türkçe",
            "العربية",
            "język polski",
            "ไทย",
            "한국어",
            "Українська",
            "Română",
            "Malay",
        };
        private List<string> langueCodes = new List<string>
        {
            "en",
            "zh",
            "zh_CN",
            "es",
            "fr",
            "ja",
            "de",
            "ru",
            "pt",
            "@in",
            "hi_IN",
            "vi",
            "tr",
            "ar",
            "pl",
            "th",
            "ko",
            "uk",
            "ro",
            "ms",
        };

        #region Base
        protected override void OnInit()
        {
            Instance = this;
            SetLangueVOModel();
        }

        protected override void OnDispose()
        {
            Instance = null;
        }

        protected override void AddListener()
        {
            AppDispatcher.Instance.AddListener(AppMsg.System_LocalConfigInitComplete, OnLocalConfigInitComplete);
        }

        protected override void RemoveListener()
        {
            AppDispatcher.Instance.RemoveListener(AppMsg.System_LocalConfigInitComplete, OnLocalConfigInitComplete);
        }

        protected virtual void SetLangueVOModel()
        {
            langueVOModel = LangueVOModel.Instance;
        }
        #endregion Base

        #region Event
        private void OnLocalConfigInitComplete(object obj)
        {
            if (!AppConst.IsMultiLangue)
            {
                deviceLangue = AppConst.DefaultLangue;
                return;
            }

            GetDeviceLangue();
        }

        private void GetDeviceLangue()
        {
            supportLangues = new List<string>();
            List<string> langueTableFields = langueVOModel.GetFieldKeyHeadFields();
            if (langueTableFields != null)
            {
                for (int i = 0; i < langueTableFields.Count; i++)
                {
                    string lang = langueTableFields[i];
                    if (m_ignoreFields.Contains(lang)) continue;
                    supportLangues.Add(lang);
                }
                deviceLangue = LangueUtil.GetCurrLangue(supportLangues);
            }
        }
        #endregion

        #region API
        public bool IsUse()
        {
            return AppConst.IsMultiLangue;
        }

        public string Get(int id)
        {
            string value = langueVOModel.GetValueByFieldKey(id, AppConst.CurrMultiLangue);
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            value = langueVOModel.GetValueByFieldKey(id, AppConst.InternalLangue);
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            return id.ToString();
        }

        public string Get(int id, string rawText)
        {
            if (IsUse())
            {
                string value = langueVOModel.GetValueByFieldKey(id, AppConst.CurrMultiLangue);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                value = langueVOModel.GetValueByFieldKey(id, AppConst.InternalLangue);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }
            return rawText;
        }

        public string Get(string key)
        {
            string value = langueVOModel.GetValueByFieldKey(key, AppConst.CurrMultiLangue);
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            value = langueVOModel.GetValueByFieldKey(key, AppConst.InternalLangue);
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            return key;
        }

        public string Get(string key, string rawText)
        {
            if (IsUse())
            {
                string value = langueVOModel.GetValueByFieldKey(key, AppConst.CurrMultiLangue);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                value = langueVOModel.GetValueByFieldKey(key, AppConst.InternalLangue);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }
            return rawText;
        }

        public string SetMultiLangueSwitchGList(FairyGUI.GList langueGList)
        {
            string currLang = langueNames[0];
            int idx = langueCodes.FindIndex((v) => v == AppConst.CurrMultiLangue);
            if (idx != -1)
            {
                currLang = langueNames[idx];
            }

            for (int i = 0; i < langueNames.Count; i++)
            {
                FairyGUI.GComponent item = langueGList.GetChildAt(i).asCom;
                item.inText = langueNames[i];

                int currI = i;
                item.onClick.Set(() =>
                {
                    if (AppConst.CurrMultiLangue != langueCodes[currI])
                    {
                        UIMgr.Instance.SetSwitchLanguage(langueCodes[currI]);

                        FairyGUI.GButton btn = item.GetChild("btn_click").asButton;
                        btn.FireClick(false, false);
                        btn.inText = langueNames[currI];
                    }
                });
            }
            return currLang;
        }
        #endregion
    }
}