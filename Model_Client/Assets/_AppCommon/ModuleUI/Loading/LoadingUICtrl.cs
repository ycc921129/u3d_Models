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
    public class LoadingUICtrl : BaseUICtrl
    {
        private LoadingUIRealize ui;

        public override void Init()
        {
            if (CommonConfig.LoadingUI)
            {
                base.Init();
            }
        }

        public override void Dispose()
        {
            if (CommonConfig.LoadingUI)
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
                ui = new LoadingUIRealize(this);
                ui.Open(args);
            }
        }

        public override void CloseUI(object args = null)
        {
            if (ui != null)
            {
                ui.Close();
                ui = null;
            }
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            uiCtrlDispatcher.AddListener(UICtrlMsg.Ctrl_LoadingOpen, OpenUI);
            uiCtrlDispatcher.AddListener(UICtrlMsg.Ctrl_LoadingClose, CloseUI);

            ctrlDispatcher.AddListener(CtrlMsg.Login_Succeed, RemoveDownTimeEvent);
            ctrlDispatcher.AddListener(CtrlMsg.Login_ReloginSucceed, RemoveDownTimeEvent);

            AppDispatcher.Instance.AddListener(AppMsg.UI_DisplayLoadingUI, OpenUI);
            AppDispatcher.Instance.AddListener(AppMsg.UI_SetLoadingUIProgress, SetLoadingValue);
            AppDispatcher.Instance.AddListener(AppMsg.UI_HideLoadingUI, CloseUI);

            AppDispatcher.Instance.AddListener(AppMsg.WebSocketServer_PreferencesParseError, SetPreferencesParseError);
            AppDispatcher.Instance.AddListener(AppMsg.System_ConfigInitError, SetConfigInitFailed);
            AppDispatcher.Instance.AddListener(AppMsg.Http_RequestConfiging, AddDownTimeEvent);
        }

        protected override void RemoveListener()
        {
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.Ctrl_LoadingOpen, OpenUI);
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.Ctrl_LoadingClose, CloseUI);

            ctrlDispatcher.RemoveListener(CtrlMsg.Login_Succeed, RemoveDownTimeEvent);
            ctrlDispatcher.RemoveListener(CtrlMsg.Login_ReloginSucceed, RemoveDownTimeEvent);

            AppDispatcher.Instance.RemoveListener(AppMsg.UI_DisplayLoadingUI, OpenUI);
            AppDispatcher.Instance.RemoveListener(AppMsg.UI_SetLoadingUIProgress, SetLoadingValue);
            AppDispatcher.Instance.RemoveListener(AppMsg.UI_HideLoadingUI, CloseUI);

            AppDispatcher.Instance.RemoveListener(AppMsg.WebSocketServer_PreferencesParseError, SetPreferencesParseError);
            AppDispatcher.Instance.RemoveListener(AppMsg.System_ConfigInitError, SetConfigInitFailed);
            AppDispatcher.Instance.RemoveListener(AppMsg.Http_RequestConfiging, AddDownTimeEvent);
        }

        protected override void AddServerListener()
        {
        }

        protected override void RemoveServerListener()
        {
        }
        #endregion


        #region  Event
        public void SetLoadingValue(object args = null)
        {
            if (ui != null)
                this.ui.SetLoading((int)args);
        }

        public void AddDownTimeEvent(object args = null)
        {            
            TimerUtil.Simple.AddTimer(5, OpenReconnectUI);
        }

        public void RemoveDownTimeEvent(object args = null)
        {
            TimerUtil.Simple.RemoveTimer(OpenReconnectUI);
        }

        private void SetPreferencesParseError(object args = null)
        {
            uiCtrlDispatcher.Dispatch(UICtrlMsg.C_OpenReconnectUI);
        }

        private void SetConfigInitFailed(object args = null)
        {
            uiCtrlDispatcher.Dispatch(UICtrlMsg.C_OpenReconnectUI);
        }

        private void OpenReconnectUI()
        {
            uiCtrlDispatcher.Dispatch(UICtrlMsg.C_OpenReconnectUI);
        }
        #endregion
    }
}