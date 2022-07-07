/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public abstract class BaseView
    {
        protected DynamicAssetLoader loader;

        protected ModuleMgr moduleMgr;

        protected ModelDispatcher modelDispatcher;
        protected ViewDispatcher viewDispatcher;
        protected CtrlDispatcher ctrlDispatcher;
        protected UICtrlDispatcher uiCtrlDispatcher;
        protected DataDispatcher dataDispatcher;
        protected GameDispatcher gameDispatcher;
        protected WSNetDispatcher wsNetDispatcher;

        public virtual void Init()
        {
            loader = new DynamicAssetLoader();
            Assignment();
            AddListener();
        }

        public virtual void Dispose()
        {
            loader.Release();
            loader.Dispose();
            loader = null;
            UnAssignment();
        }

        protected virtual void Assignment()
        {
            moduleMgr = ModuleMgr.Instance;

            modelDispatcher = ModelDispatcher.Instance;
            viewDispatcher = ViewDispatcher.Instance;
            ctrlDispatcher = CtrlDispatcher.Instance;
            uiCtrlDispatcher = UICtrlDispatcher.Instance;
            dataDispatcher = DataDispatcher.Instance;
            gameDispatcher = GameDispatcher.Instance;
            wsNetDispatcher = WSNetDispatcher.Instance;
        }
        protected virtual void UnAssignment()
        {
            moduleMgr = null;

            modelDispatcher = null;
            viewDispatcher = null;
            ctrlDispatcher = null;
            uiCtrlDispatcher = null;
            dataDispatcher = null;
            gameDispatcher = null;
            wsNetDispatcher = null;
        }

        protected abstract void AddListener();
        protected abstract void RemoveListener();
    }
}