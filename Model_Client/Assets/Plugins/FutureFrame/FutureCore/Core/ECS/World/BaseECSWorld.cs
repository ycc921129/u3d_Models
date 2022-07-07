/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    /// <summary>
    /// 空间容器
    /// 1）持有所有实体
    /// 2）持有所有需要遍历逻辑的组件
    /// 3）持有所有系统
    /// 4）驱动主循环
    /// 
    /// 支持OOP与ECS设计
    /// ECS框架重点
    /// 1）自动生成Component类型枚举
    /// 2）自动生成Entity类型枚举
    /// 3）通过组件筛选得到实体
    /// 4）组件获取实体自动转换为挂载主体的实体类型
    /// </summary>
    public abstract class BaseECSWorld : MonoBehaviour
    {
        protected virtual void Awake() { }

        protected virtual void Start() { }

        protected virtual void OnDestroy() { }

        protected virtual void Update() { }

        protected virtual void LateUpdate() { }

        protected virtual void FixedUpdate() { }
    }
}