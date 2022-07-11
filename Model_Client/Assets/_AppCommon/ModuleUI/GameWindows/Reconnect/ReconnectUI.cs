/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using UnityEngine;
using FairyGUI;
using System;
using UI.CS603_gameWindows;

namespace ProjectApp
{
    public class ReconnectUI : BaseUI
    {
        ReconnectUICtrl ctrl;

        UI.CS603_gameWindows.com_retry ui;

        public ReconnectUI(ReconnectUICtrl baseUICtrl) : base(baseUICtrl)
        {
            ctrl = baseUICtrl;
            uiName = UIConst.ReconnectUI;
        }

        protected override void SetUIInfo(UIInfo uiInfo)
        {
            uiInfo.packageName = "CS603_gameWindows";
            uiInfo.assetName = "com_retry";
            uiInfo.isTickUpdate = true;
            uiInfo.layerType = UILayerType.System;
            uiInfo.isNeedUIMask = true;
        }

        #region 生命周期
        protected override void OnInit()
        {

        }

        protected override void OnBind()
        {
            ui = baseUI as UI.CS603_gameWindows.com_retry;         
        }

        protected override void OnOpenBefore(object args)
        {
            InitUICom();
        }

        protected override void OnOpen(object args)
        {
            this.ui.text_time.alpha = 0;

            // 添加标识
            //if (ui.text_code != null)
            //{
            //    ui.text_code.text = Channel.Current.aid + "\nver" + Channel.Current.ver;
            //}
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
        public void InitUICom()
        {
            this.ui.com_btnStatus.btn_retry.onClick.Add(OnBtnClick);
            this.ui.com_btnStatus.btn_cancel.onClick.Add(OnBtnClick);
        }
        #endregion

        #region Event
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (time > 0)
            {
                time -= Time.deltaTime;
                this.ui.text_time.text = string.Format("Please wait {0}s...", Math.Ceiling(time));
            }
            else
            {
                this.ui.text_time.text = "Please wait 0s...";
            }
        }

        float time;
        public void ShowWait(object args = null)
        {
            this.ui.text_time.alpha = 1;
            time = (int)args;
        }

        public void CloseWait(object args = null)
        {
            this.ui.text_time.alpha = 0;
        }

        public void DelayCallback()
        {
            this.ui.com_btnStatus.cont_status.SetSelectedIndex(com_btnStatus.Status_help);
        }

        public void OnBtnClick(EventContext eventContext)
        {
            if (eventContext.sender == this.ui.com_btnStatus.btn_retry)
            {
                ctrl.OnRetry();
                return;
            }
            if (eventContext.sender == this.ui.com_btnStatus.btn_cancel) 
            {
                ctrl.OnHelp();
                return;
            }
        }
        #endregion

        #region feedback
        public string GetErrStatus
        {
            get
            {
                return string.Format("Error: Network error: {0}, GameSence Shown: {1}", this.ui.text_status.text, UIMgr.Instance.IsExistUI(UIConst.LoadingUI));
            }
        }
        #endregion
    }
}