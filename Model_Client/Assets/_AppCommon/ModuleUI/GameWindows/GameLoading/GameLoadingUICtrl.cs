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
    public class GameLoadingUICtrl : BaseUICtrl
    {
        private GameLoadingUI ui;

        public override void Init()
        {
            if (CommonConfig.GameLoadingUI)
            {
                base.Init();
            }
        }

        public override void Dispose()
        {
            if (CommonConfig.GameLoadingUI)
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
                ui = new GameLoadingUI(this);
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
            uiCtrlDispatcher.AddListener(UICtrlMsg.MumWaitUI_Open, OpenUI);
            uiCtrlDispatcher.AddListener(UICtrlMsg.GameLoadingUI_Open, OpenUI);
            uiCtrlDispatcher.AddListener(UICtrlMsg.GameLoadingUI_Close, CloseUI);

            AppDispatcher.Instance.AddListener(AppMsg.UI_DisplayWaitUI, OpenUI);
            AppDispatcher.Instance.AddListener(AppMsg.UI_DisplayWaitTimeUI, OpenUI);
            AppDispatcher.Instance.AddListener(AppMsg.UI_HideWaitUI, CloseUI);
        }

        protected override void RemoveListener()
        {
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.MumWaitUI_Open, OpenUI);
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.GameLoadingUI_Open, OpenUI);
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.GameLoadingUI_Close, CloseUI);

            AppDispatcher.Instance.RemoveListener(AppMsg.UI_DisplayWaitUI, OpenUI);
            AppDispatcher.Instance.RemoveListener(AppMsg.UI_DisplayWaitTimeUI, OpenUI);
            AppDispatcher.Instance.RemoveListener(AppMsg.UI_HideWaitUI, CloseUI);
        }

        protected override void AddServerListener()
        {
        }

        protected override void RemoveServerListener()
        {
        }
        #endregion
    }
}