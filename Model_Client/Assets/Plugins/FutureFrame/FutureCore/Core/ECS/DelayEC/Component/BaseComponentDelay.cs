/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    /// <summary>
    /// 组件
    /// 承载数据
    /// 1）组件可以组合组件
    /// 2）组件只能有处理自身数据的方法
    /// </summary>
    public abstract class BaseComponentDelay : DelayItem
    {
        public BaseEntityDelay baseEntity;

        public virtual void Dispose() { }
    }
}