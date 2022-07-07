/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    /// <summary>
    /// 系统
    /// 承载逻辑
    /// 1）EntitySystem
    /// 2）GameObjectSystem
    /// </summary>
    public abstract class BaseSystem
    {
        public BaseECSWorld baseWorld;

        public BaseSystem() { }

        public BaseSystem(BaseECSWorld baseWorld)
        {
            this.baseWorld = baseWorld;
        }

        protected virtual void Init() { }

        public virtual void Dispose() { }

        public virtual void Update() { }

        public virtual void LateUpdate() { }

        public virtual void FixedUpdate() { }
    }
}