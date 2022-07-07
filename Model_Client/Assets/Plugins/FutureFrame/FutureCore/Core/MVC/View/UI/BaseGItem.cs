/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using FairyGUI;

namespace FutureCore
{
    public class BaseGItem<T> where T : GComponent
    {
        public BaseUI parentUI;
        public GComponent gComponent;
        public T ui;
        public object data;

        public BaseGItem(BaseUI parentUI, GComponent gComponent)
        {
            this.parentUI = parentUI;
            if (gComponent == null)
            {
                Init();
                return;
            }

            this.gComponent = gComponent;
            ui = (T)gComponent;
            Init();
        }
        public BaseGItem(GComponent gComponent)
        {
            if (gComponent == null)
            {
                Init();
                return;
            }

            this.gComponent = gComponent;
            ui = (T)gComponent;
            Init();
        }
        public BaseGItem()
        {
            Init();
        }

        public virtual void Init()
        {
            AddListener();
        }
        public virtual void Dispose()
        {
            RemoveListener();
        }

        public virtual void SetData(object data)
        {
            this.data = data;
            OnRefreshUI();
        }

        public virtual void OnRefreshUI() { }
        public virtual void AddListener() { }
        public virtual void RemoveListener() { }
    }

    public abstract class BaseGItem<T, W> where T : GComponent
    {
        public BaseUI parentUI;
        public GComponent gComponent;
        public T ui;
        public W data;
        public int index = -1;

        public BaseGItem(BaseUI parentUI, GComponent gComponent)
        {
            this.parentUI = parentUI;
            if (gComponent == null)
            {
                Init();
                return;
            }

            this.gComponent = gComponent;
            ui = (T)gComponent;
            Init();
        }
        public BaseGItem(GComponent gComponent)
        {
            if (gComponent == null)
            {
                Init();
                return;
            }

            this.gComponent = gComponent;
            ui = (T)gComponent;
            Init();
        }
        public BaseGItem()
        {
            Init();
        }

        public virtual void Init()
        {
            AddListener();
        }

        public virtual void Dispose()
        {
            RemoveListener();
        }

        public virtual void SetData(object data)
        {
            this.data = (W)data;
            OnRefreshUI();
        }

        public virtual void OnRefreshUI() { }
        public virtual void AddListener() { }
        public virtual void RemoveListener() { }
    }
}