/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FutureCore;
using FairyGUI;
using ProjectApp.Protocol;

namespace ProjectApp
{
    public class LoginUI : BaseUI
    {
        #region 需要注册的UI对象
        /// <summary>
        /// faceBook登陆
        /// </summary>
        protected GButton btn_facebookLogin;
        /// <summary>
        /// google登陆
        /// </summary>
        protected GButton btn_googleLogin;
        #endregion

        protected LoginUICtrl ctrl;
        protected UI.CS603_gameWindows.com_login ui;

        public LoginUI(LoginUICtrl ctrl) : base(ctrl)
        {
            uiName = UIConst.LoginUI;
            this.ctrl = ctrl;
        }

        protected override void SetUIInfo(UIInfo uiInfo)
        {
            uiInfo.packageName = "CS603_gameWindows";
            uiInfo.assetName = "com_login";
            uiInfo.layerType = UILayerType.Top;
            uiInfo.isNeedUIMask = true;
        }

        #region 生命周期
        protected override void OnInit()
        {
        }

        protected override void OnBind()
        {
            ui = baseUI as UI.CS603_gameWindows.com_login;           
        }

        protected override void OnOpenBefore(object args)
        {
            InitUICom();
        }

        protected override void OnOpen(object args)
        {
        }

        protected override void OnClose()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnDisplay(object args)
        {
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
        }

        protected override void RemoveListener()
        {
        }
        #endregion

        #region Bind
        private void InitUICom()
        {
            if (this.btn_facebookLogin != null)
                this.btn_facebookLogin.onClick.Add(FaceBookLoginEvent);
            if (this.btn_googleLogin != null)
                this.btn_googleLogin.onClick.Add(GoogleLoginEvent);

            this.ui.btn_close.onClick.Add(CloseEvent);
        }

        private void FaceBookLoginEvent()
        {
            ctrl.OnFaceBooKLogin();
        }

        private void GoogleLoginEvent()
        {
            ctrl.OnGoogleLogin();
        }

        private void CloseEvent()
        {
            ctrl.CloseUI();
        }
        #endregion
    }
}