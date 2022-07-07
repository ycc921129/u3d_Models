/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    /// <summary>
    /// 可重复使用对象
    /// 通用基类实现
    /// </summary>
    public class ReusbleUObject : IReusable
    {
        public GameObject gameObject;
        public bool IsActive { get; private set; }

        public ReusbleUObject()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        public void New()
        {
            OnNew();
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            OnDispose();
        }

        /// <summary>
        /// 取出
        /// </summary>
        public void Get()
        {
            IsActive = true;
            OnGet();
        }
        /// <summary>
        /// 回收
        /// </summary>
        public void Release()
        {
            IsActive = false;
            OnRelease();
        }

        /// <summary>
        /// 当初始化
        /// </summary>
        protected virtual void OnNew() { }
        /// <summary>
        /// 当销毁
        /// </summary>
        protected virtual void OnDispose() { }

        /// <summary>
        /// 当取出
        /// </summary>
        protected virtual void OnGet() { }
        /// <summary>
        /// 当回收
        /// </summary>
        protected virtual void OnRelease() { }
    }
}