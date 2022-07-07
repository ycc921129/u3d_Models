/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public abstract class BaseUICtrl : BaseCtrl
    {
        protected RedDotDispatcher redDotDispatcher;

        protected override void Assignment()
        {
            base.Assignment();
            redDotDispatcher = RedDotDispatcher.Instance;
        }
        protected override void UnAssignment()
        {
            base.UnAssignment();
            redDotDispatcher = null;
        }

        public virtual uint GetOpenUIMsg(string uiName)
        {
            return 0;
        }
        public virtual uint GetCloseUIMsg(string uiName)
        {
            return 0;
        }

        public void DispatchOpenUI(string uiName = null, object args = null)
        {
            uint msgId = GetOpenUIMsg(uiName);
            if (msgId == 0)
            {
                OpenUI(args);
                return;
            }
            if (uiCtrlDispatcher != null)
            {
                uiCtrlDispatcher.Dispatch(msgId, args);
            }
        }
        public void DispatchCloseUI(string uiName = null, object args = null)
        {
            uint msgId = GetCloseUIMsg(uiName);
            if (msgId == 0)
            {
                CloseUI(args);
                return;
            }
            if (uiCtrlDispatcher != null)
            {
                uiCtrlDispatcher.Dispatch(msgId, args);
            }
        }

        public abstract void OpenUI(object args = null);
        public abstract void CloseUI(object args = null);
    }
}