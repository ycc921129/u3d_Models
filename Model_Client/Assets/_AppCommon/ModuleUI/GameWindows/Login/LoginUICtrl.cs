/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using FutureCore;
using ProjectApp.Protocol;

namespace ProjectApp
{
    public class LoginUICtrl : BaseUICtrl
    {
        private LoginUIRealize ui;

        public override void Init()
        {
            if (CommonConfig.LoginUI)
            {
                base.Init();
            }
        }

        public override void Dispose()
        {
            if (CommonConfig.LoginUI)
            {
                base.Dispose();
            }
        }

        #region 生命周期
        protected override void OnInit()
        {
        }

        protected override void OnDispose()
        {
        }

        public override void OpenUI(object args = null)
        {
            if (ui == null)
            {
                ui = new LoginUIRealize(this);
                ui.Open(args);
            }
        }

        public override void CloseUI(object args = null)
        {
            if (ui != null && !ui.isClose)
            {
                ui.Close();
                ui = null;
            }
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            uiCtrlDispatcher.AddListener(UICtrlMsg.C_OpenLoginUI, OpenUI);
            uiCtrlDispatcher.AddListener(UICtrlMsg.C_CloseLoginUI, CloseUI);

            ctrlDispatcher.AddListener(CtrlMsg.BindLoginTokenFinish, CloseUI);
        }

        protected override void RemoveListener()
        {
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.C_OpenLoginUI, OpenUI);
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.C_CloseLoginUI, CloseUI);

            ctrlDispatcher.RemoveListener(CtrlMsg.BindLoginTokenFinish, CloseUI);
        }

        protected override void AddServerListener()
        {
        }

        protected override void RemoveServerListener()
        {
        }
        #endregion

        #region Event
        public void OnFaceBooKLogin()
        {
            Channel.Current.facebookLogin();
        }

        public void OnGoogleLogin()
        {
            Channel.Current.googleLogin();
        }
        #endregion 
    }
}