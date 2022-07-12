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

        #region property        

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
        
    }
}