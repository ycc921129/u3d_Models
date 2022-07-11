/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public sealed class DispatcherMgr : BaseMgr<DispatcherMgr>
    {
        private void ClearAllDispatcher()
        {
            // Inherit
            AppDispatcher.Instance.Clear();
            ChannelDispatcher.Instance.Clear();
            CtrlDispatcher.Instance.Clear();
            DataDispatcher.Instance.Clear();
            GameDispatcher.Instance.Clear();
            MainThreadDispatcher.Instance.Clear();
            ModelDispatcher.Instance.Clear();
            UICtrlDispatcher.Instance.Clear();
            // Specific
            RedDotDispatcher.Instance.Clear();
            // Other
            NetTimerDispatcher.Instance.Clear();
        }

        public override void Dispose()
        {
            base.Dispose();
            ClearAllDispatcher();
        }
    }
}