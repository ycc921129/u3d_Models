/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using UnityEngine;

namespace ProjectApp
{
    public class TipsUICtrl : BaseUICtrl
    {
        TipsUIRealize ui;

        public override void Init()
        {
            if (CommonConfig.TipsUI)
            {
                base.Init();
            }
        }

        public override void Dispose()
        {
            if (CommonConfig.TipsUI)
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
                ui = new TipsUIRealize(this);
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
            AppDispatcher.Instance.AddListener(AppMsg.UI_ShowTipsUI, OpenUI);

            uiCtrlDispatcher.AddListener(UICtrlMsg.C_OpenTipsUI, OpenUI);
            uiCtrlDispatcher.AddListener(UICtrlMsg.C_CloseTipsUI, CloseUI);
        }

        protected override void RemoveListener()
        {
            AppDispatcher.Instance.RemoveListener(AppMsg.UI_ShowTipsUI, OpenUI);

            uiCtrlDispatcher.RemoveListener(UICtrlMsg.C_OpenTipsUI, OpenUI);
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.C_CloseTipsUI, CloseUI);
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
