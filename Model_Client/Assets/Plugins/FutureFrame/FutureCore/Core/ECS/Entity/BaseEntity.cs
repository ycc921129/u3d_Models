/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    /// <summary>
    /// 实体
    /// 承载组件
    /// </summary>
    public abstract class BaseEntity
    {
        public uint instId;
        public int entityType;

        public virtual void Init() { }

        public virtual void Dispose() { }
    }
}