using FairyGUI;
using FutureCore;
using FutureCore.Data;
using ProjectApp.Data;
using ProjectApp.Protocol;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectApp
{
    public partial class CommonGlobal : Singleton<CommonGlobal>
    {
        public PreferencesMgr preferencesMgr;
        public LoginModel loginModel;

        private const string TAG = "[CommonGlobal] ";

        public void Init()
        {
            preferencesMgr = PreferencesMgr.Instance;
            loginModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;

            Vibration = PrefsUtil.ReadBool(PrefsKeyConst.Common_isOpenVibration, VibrationHelper.IsOpenVibration);
        }

        public S2C_reg_login_data LoginData
        {
            get
            {
                return loginModel.loginData;
            }
        }

        #region currency
        private const float DEFULTCOEFFICIENT = 0.0000463591f;
        private float curCoefficient = 0;
        private const string DEFULTCURRENCY = "R$";
        public string curCurrency = string.Empty;

        /// <summary>
        /// 金币换算倍率
        /// </summary>
        public float Coefficient
        {
            get
            {
                if (curCoefficient != 0) return curCoefficient;
                var voList = CardLocalVOModel.Instance.GetVOList();
                if (voList == null) return DEFULTCOEFFICIENT;
                for (int i = 0; i < voList.Count; i++)
                {
                    for (int j = 0; j < voList[i].Codename.Length; j++)
                    {
                        if (reg_country.Contains(voList[i].Codename[j]))
                        {
                            curCoefficient = voList[i].Value;
                            return curCoefficient;
                        }
                    }
                }

                return DEFULTCOEFFICIENT;
            }
        }

        public string Currency
        {
            get
            {
                if (curCurrency != string.Empty) return curCurrency;
                var voList = CardLocalVOModel.Instance.GetVOList();
                for (int i = 0; i < voList.Count; i++)
                {
                    for (int j = 0; j < voList[i].Codename.Length; j++)
                    {
                        if (reg_country.Contains(voList[i].Codename[j]))
                        {
                            curCurrency = voList[i].currency;
                            return curCurrency;
                        }
                    }
                }

                return DEFULTCURRENCY;
            }
        }
        #endregion currency

        #region property

        /// <summary>
        /// 邀请码
        /// </summary>
        public string InvitedCode
        {
            get
            {
                if (loginModel == null
                    || loginModel.loginData == null
                    || loginModel.loginData.info == null) return "";

                return loginModel.loginData.info.invite_code;
            }
        }

        public bool CanShowOffline()
        {
            if (PreferencesMgr.Instance == null)
            {
                return false;
            }
            if (PreferencesMgr.Instance.Offline_timestamp <= 0)
            {
                return false;
            }
            long offset = DateTimeMgr.Instance.GetServerCurrTimestamp() - PreferencesMgr.Instance.Offline_timestamp;
            return offset > 5 * 60;
        }
        #endregion property

        #region 用户信息相关     
        public string GetDefaultHead
        {
            get
            {
                return CommonLoadMgr.Instance.LoadGameFguiUrl(CommonConst.defaultHead);
            }
        }

        public void GetPlayerHead(GLoader head, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                head.url = GetDefaultHead;
                return;
            }
            DownloadUtil.Instance.DownloadImage(url, (Sprite obj) =>
            {
                if (head != null && !head.isDisposed)
                    head.texture = new NTexture(obj);
            }, true);
        }

        public bool LoginFacebookOrGoogleStatus
        {
            get
            {
                LogUtil.Log("登陆公共平台状态  + " + Channel.Current.firebaseIsLogin());
                return Channel.Current.firebaseIsLogin();
            }
        }

        public bool BindMobileStatus
        {
            get
            {
                return true;
            }
        }

        public void BindingMobile()
        {
            if (!BindMobileStatus)
            {
                if (CommonConfig.AmazonSNSUI)
                {
                    UICtrlDispatcher.Instance.Dispatch(UICtrlMsg.GetVerifyCodeUI_Open);
                }
                else
                {
                    Channel.Current.accountKitTokenCheck("");
                }
            }
        }

        public void SetMusicStatus(int value)
        {
            AudioMgr.Instance.IsOpenBGM = value == 0 ? false : true;
            AudioMgr.Instance.BgmVolume = value;
        }

        public void SetSoundStatus(int value)
        {
            AudioMgr.Instance.IsOpenEffect = value == 0 ? false : true;
            GRoot.inst.soundVolume = value;
        }

        public bool Vibration
        {
            get
            {
                return VibrationHelper.IsOpenVibration;
            }
            set
            {
                VibrationHelper.IsOpenVibration = value;
            }
        }
        #endregion 用户信息相关                

        #region 提示

        public void ShowTips(string content)
        {
            UICtrlDispatcher.Instance.Dispatch(UICtrlMsg.C_OpenTipsUI, content);
        }

        #endregion 提示                 

        #region 货币
        public long Coin
        {
            get
            {
                if (loginModel == null
                    || loginModel.loginData == null
                    || loginModel.loginData.acct == null) return 0;

                return loginModel.loginData.acct.coin;
            }
        }
        #endregion

        #region 敏感功能和兑换
        /// <summary>
        /// 是否启用敏感功能
        /// </summary>
        public bool GetModulesConf_EnableFuns()
        {
            if (loginModel == null
                || loginModel.loginData == null
                || loginModel.loginData.info == null
                || loginModel.loginData.info.is_open_sensfunc == null
                || !loginModel.loginData.info.is_open_sensfunc.Contains("funs"))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 是否开启兑换  
        /// </summary>
        public bool Is_open_exchange
        {
            get
            {
                if (loginModel == null
                   || loginModel.loginData == null
                   || loginModel.loginData.info == null)
                {
                    return false;
                }

                return loginModel.loginData.info.is_open_exchange;
            }
        }
        #endregion

        #region 平台兑换
        public string reg_country
        {
            get
            {
#if UNITY_EDITOR
                return "US";
#else                
                if (loginModel == null
                  || loginModel.loginData == null
                  || loginModel.loginData.info == null
                  || loginModel.loginData.info.reg_country == null)
                {
                    return "US";
                }
                else
                    return loginModel.loginData.info.reg_country;
#endif
            }
        }
        #endregion
    }
}