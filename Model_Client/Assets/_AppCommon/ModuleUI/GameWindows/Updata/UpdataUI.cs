/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using FairyGUI;

namespace ProjectApp
{
    public class UpdataUI : BaseUI
    {
        UpdataUICtrl ctrl;
        UI.CS603_gameWindows.com_gameUpdate ui;

        public UpdataUI(UpdataUICtrl baseUICtrl) : base(baseUICtrl)
        {
            ctrl = baseUICtrl;
            uiName = UIConst.UpdataUI;
        }

        protected override void SetUIInfo(UIInfo uiInfo)
        {
            uiInfo.packageName = "CS603_gameWindows";
            uiInfo.assetName = "com_gameUpdate";
            uiInfo.layerType = UILayerType.System;
            uiInfo.isNeedUIMask = true;
            uiInfo.isNeedOpenAnim = true;
            uiInfo.isNeedCloseAnim = false;
#if UNITY_EDITOR
            uiInfo.isTickUpdate = true;
#endif
        }

        #region 生命周期
        protected override void OnInit()
        {
          
        }

        protected override void OnBind()
        {
            ui = baseUI as UI.CS603_gameWindows.com_gameUpdate;
            InitUICom();
        }

        protected override void OnOpenBefore(object args)
        {
            InitUICom();
        }

        protected override void OnOpen(object args)
        {
            SetUIStatus((int)args);
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

#if UNITY_EDITOR
        public override void OnUpdate()
        {
            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.Escape))
            {
                CtrlCloseUI();
            }
        }
#endif
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
        public void InitUICom()
        {
            this.ui.com_buttomStatus.btn_update.onClick.Add(OnBtnClick);
            this.ui.com_buttomStatus.btn_cancel.onClick.Add(OnBtnClick);
        }
        #endregion

        #region Event
        public void OnBtnClick(EventContext eventContext)
        {
            if (eventContext.sender == this.ui.com_buttomStatus.btn_update)
            {
                ctrl.OnGameUpdata();
                return;
            }
            if (eventContext.sender == this.ui.com_buttomStatus.btn_cancel)
            {
                ctrl.CloseUI();
                return;
            }
        }

        public void SetUIStatus(int index)
        {
            this.ui.com_buttomStatus.cont_showStatus.SetSelectedIndex(index);
        }
        #endregion
    }
}
