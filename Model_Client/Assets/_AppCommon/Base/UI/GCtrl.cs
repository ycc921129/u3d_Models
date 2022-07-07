/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

/****************************************************************************
 * Copyright (c) 2019.4.2 yuannan （创建者)
 * Copyright (c) 2018.4.8. yuannan  (最后修改者)
 ****************************************************************************/

namespace ProjectApp
{
    using System;
    using FairyGUI;
    using FutureCore;

    public class GCtrl<T> where T : GComponent, new()
    {
        public T ui;//显示的界面

        protected UICtrlDispatcher uiCtrlDispatcher;
        protected CtrlDispatcher ctrlDispatcher;

        public GCtrl(GComponent com)
        {
            ui = com as T;
            uiCtrlDispatcher = UICtrlDispatcher.Instance;
            ctrlDispatcher = CtrlDispatcher.Instance;
            OnBind();
            InitUI();
        }

        /// <summary>
        /// 所有的事件监听在这里添加监听
        /// </summary>
        public virtual void AddListener()
        {
        }

        /// <summary>
        /// 所有的事件监听在这里添加监听
        /// </summary>
        public virtual void RemoveListener()
        {
        }

        public virtual void OnBind()
        {
           
        }

        public virtual void InitUI()
        {
            AddListener();
        }

        public object mData;

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="_data">显示界面时可附带的数据</param>
        public virtual void SetData(object _data)
        {
            this.mData = _data;
            OnFreshView();
        }

        public virtual void OnFreshView() {

        }    
    }
}
