/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public abstract class BaseCtrl
    {
        public string ctrlName;
        public bool isEnable = true;
        public bool IsNew { get; private set; }

        protected ModuleMgr moduleMgr;

        protected ModelDispatcher modelDispatcher;
        protected ViewDispatcher viewDispatcher;
        protected CtrlDispatcher ctrlDispatcher;
        protected UICtrlDispatcher uiCtrlDispatcher;
        protected DataDispatcher dataDispatcher;
        protected GameDispatcher gameDispatcher;

        public void New()
        {
            if (!isEnable) return;

            OnNew();
            IsNew = true;
        }
        public virtual void Init()
        {
            if (!isEnable) return;

            Assignment();
            AddListener();
            AddServerListener();
            OnInit();
        }
        public virtual void StartUp()
        {
            if (!isEnable) return;

            OnStartUp();
        }
        public void ReadData()
        {
            OnReadData();
        }
        public virtual void GameStart()
        {
            if (!isEnable) return;

            OnGameStart();
        }
        public virtual void Dispose()
        {
            if (!isEnable) return;

            RemoveListener();
            RemoveServerListener();
            OnDispose();
            UnAssignment();
            IsNew = false;
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
        }

        protected virtual void OnNew() { }
        protected abstract void OnInit();
        protected virtual void OnStartUp() { }
        protected virtual void OnReadData() { }
        protected virtual void OnGameStart() { }
        protected abstract void OnDispose();

        protected virtual void AddListener() { }
        protected virtual void RemoveListener() { }
        protected virtual void AddServerListener() { }
        protected virtual void RemoveServerListener() { }
    }
}