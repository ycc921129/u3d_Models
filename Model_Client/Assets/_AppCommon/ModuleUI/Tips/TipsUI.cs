/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using FairyGUI;
namespace ProjectApp
{
    public class TipsUI : BaseUI
    {
        #region 需要注册的UI
        /// <summary>
        /// 提示内容
        /// </summary>
        protected GTextField text_content;

        /// <summary>
        /// 提示动画
        /// </summary>
        protected Transition tra_rise;
        #endregion

        protected TipsUICtrl ctrl;
        protected UI.C505_tips.com_tips ui;

        public TipsUI(TipsUICtrl baseUICtrl) : base(baseUICtrl)
        {
            ctrl = baseUICtrl;
            uiName = UIConst.TipsUI;
        }

        protected override void SetUIInfo(UIInfo uiInfo)
        {
            uiInfo.packageName = "C505_tips";
            uiInfo.assetName = "com_tips";
            uiInfo.layerType = UILayerType.Highest;
        }

        #region 生命周期
        protected override void OnInit()
        {
        }

        protected override void OnBind()
        {
            ui = baseUI as UI.C505_tips.com_tips;
            ui.opaque = false;
        }

        protected override void OnOpenBefore(object args)
        {
        }

        protected override void OnOpen(object args)
        {
            string content = args as string;
            SetTipsContent(content);
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

        #region Event
        public void SetTipsContent(string content)
        {
            if (this.text_content == null) return;

            this.text_content.text = content;

            if (this.tra_rise == null) return;
            this.tra_rise.Play(() => { ctrl.CloseUI(); });
        }
        #endregion
    }
}