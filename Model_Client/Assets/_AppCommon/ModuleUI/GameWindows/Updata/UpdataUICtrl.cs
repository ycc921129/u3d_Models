/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using ProjectApp.Protocol;
using UnityEngine;

namespace ProjectApp
{
    public class UpdataUICtrl : BaseUICtrl
    {      
        UpdataUI ui;
        LoginModel userModel;

        UpdateUIData uIData;

        public override void Init()
        {
            if (CommonConfig.UpdataUI)
            {
                base.Init();
            }
        }

        public override void Dispose()
        {
            if (CommonConfig.UpdataUI)
            {
                base.Dispose();
            }
        }

        #region 生命周期
        protected override void OnInit()
        {
            userModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;
        }

        protected override void OnDispose()
        {
        }

        public override void OpenUI(object args = null)
        {
            if (this.ui == null)
            {
                ui = new UpdataUI(this);
                ui.Open(args);
            }
        }

        public override void CloseUI(object args = null)
        {
            if (ui != null)
            {
                if (uIData.cancelFunc != null)
                    uIData.cancelFunc();
                ui.Close();
                ui = null;
                CommonConst.UpdataStatus = false;
            }
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            uiCtrlDispatcher.AddListener(UICtrlMsg.C_OpenUpdataUI, OpenUI);
            uiCtrlDispatcher.AddListener(UICtrlMsg.C_CloseUpdateUI, CloseUI);

            uiCtrlDispatcher.AddListener(UICtrlMsg.UpdateUI_ShowNeed, OnShowNeed);
            uiCtrlDispatcher.AddListener(UICtrlMsg.UpdateUI_ShowMust, OnShowMust);
        }

        protected override void RemoveListener()
        {
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.C_OpenUpdataUI, OpenUI);
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.C_CloseUpdateUI, CloseUI);

            uiCtrlDispatcher.RemoveListener(UICtrlMsg.UpdateUI_ShowNeed, OnShowNeed);
            uiCtrlDispatcher.RemoveListener(UICtrlMsg.UpdateUI_ShowMust, OnShowMust);
        }

        protected override void AddServerListener()
        {
           
        }
       
        protected override void RemoveServerListener()
        {
         
        }
        #endregion

        #region Event
        public void OnGameUpdata()
        {
            Channel.Current.updateApp(SerializeUtil.ToJson(uIData.update));
        }

        public void OnShowNeed(object args = null)
        {
            CommonConst.UpdataStatus = true;
            uIData = args as UpdateUIData;
            OpenUI(1);
        }

        public void OnShowMust(object args = null)
        {
            CommonConst.UpdataStatus = true;

            UpdateUIData updateUIData = new UpdateUIData();
            updateUIData.update = args as Update;
            uIData = updateUIData;
            OpenUI(0);
        }
        #endregion
    }
}