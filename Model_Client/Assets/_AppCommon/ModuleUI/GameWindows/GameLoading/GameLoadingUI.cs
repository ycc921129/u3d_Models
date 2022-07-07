/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp
{
    public class GameLoadingUI : BaseUI
    {
        private GameLoadingUICtrl ctrl;
        private UI.CS603_gameWindows.com_gameLoading ui;

        private WaitTimeActionClass waitTime;
        private float defaultWaiTime = 10f;

        public GameLoadingUI(GameLoadingUICtrl ctrl) : base(ctrl)
        {
            uiName = UIConst.GameLoadingUI;
            this.ctrl = ctrl;
        }

        protected override void SetUIInfo(UIInfo uiInfo)
        {
            uiInfo.packageName = "CS603_gameWindows";
            uiInfo.assetName = "com_gameLoading";
            uiInfo.layerType = UILayerType.System;
            uiInfo.isNeedUIMask = true;
        }

        #region 生命周期
        protected override void OnInit()
        {
        }

        protected override void OnBind()
        {
            ui = baseUI as UI.CS603_gameWindows.com_gameLoading;
        }

        protected override void OnOpenBefore(object args)
        {
            this.ui.img_loading.visible = false;
        }

        protected override void OnOpen(object args)
        {
            if (args == null)
            {
                waitTime = new WaitTimeActionClass()
                {
                    waitTime = defaultWaiTime,
                };
            }
            else
            {
                waitTime = (WaitTimeActionClass)args;
            }

            TimerUtil.Simple.AddTimer(0.5f, ShowLoadingEvent);
            
        }

        protected override void OnClose()
        {
            TimerUtil.Simple.RemoveTimer(ShowLoadingEvent);
            TimerUtil.Simple.RemoveTimer(OnCloseEvent);
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
            //modelDispatcher.AddListener(ModelMsg.XXX, OnXXX);
        }

        protected override void RemoveListener()
        {
            //modelDispatcher.RemoveListener(ModelMsg.XXX, OnXXX);
        }
        #endregion

        #region Event
        public void OnCloseEvent()
        {
            if (waitTime.action != null)
                waitTime.action.Invoke();
            ctrl.CloseUI();
        }


        private void ShowLoadingEvent()
        {
            this.ui.img_loading.visible = true;
            this.ui.tra_loading.Play();
            if (waitTime.waitTime > 0)
            {
                TimerUtil.Simple.AddTimer(5, OnCloseEvent);
            }
        }
        #endregion
    }
}