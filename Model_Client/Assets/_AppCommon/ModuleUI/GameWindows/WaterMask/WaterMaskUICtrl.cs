/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using System;
using FutureCore;

namespace ProjectApp
{
    public class WaterMaskUICtrl : BaseUICtrl
    {
        private WaterMaskUI ui;

        public override void Init()
        {
            if (CommonConfig.WaterMaskUI)
            {
                base.Init();
            }
        }

        public override void Dispose()
        {
            if (CommonConfig.WaterMaskUI)
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
                ui = new WaterMaskUI(this);
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
            ctrlDispatcher.AddListener(CtrlMsg.Game_Start, OpenUI);

            uiCtrlDispatcher.AddListener(UICtrlMsg.WaterMaskUI_Open, OpenUI);
            uiCtrlDispatcher.AddListener(UICtrlMsg.WaterMaskUI_Close, CloseUI);

            AppDispatcher.Instance.AddListener(AppMsg.App_SwitchLanguage, OnSwitchLanguage);
        }
        protected override void RemoveListener()
        {
            ctrlDispatcher.RemoveListener(CtrlMsg.Game_Start, OpenUI);

            uiCtrlDispatcher.RemoveListener(UICtrlMsg.WaterMaskUI_Open, OpenUI);
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.WaterMaskUI_Close, CloseUI);

            AppDispatcher.Instance.RemoveListener(AppMsg.App_SwitchLanguage, OnSwitchLanguage);
        }

        protected override void AddServerListener()
        {
            //wsNetDispatcher.AddListener(WSNetMsg.S2C_XXX, OnS2CXXXResp);
        }

        protected override void RemoveServerListener()
        {
            //wsNetDispatcher.RemoveListener(WSNetMsg.S2C_XXX, OnS2CXXXResp);
        }
        #endregion

        private void OnS2CXXXResp(BaseS2CJsonProto protoMsg)
        {
        }

        private void OnSwitchLanguage(object obj)
        {
            if (ui == null) return;
            ui.UpdateCode();
        }

    }
}