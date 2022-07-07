/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    /// <summary>
    /// MonoBehaviour组件
    /// GameObject与Entity的关联
    /// </summary>
    public class BaseMonoComponentDelay : MonoBehaviour
    {
        public BaseEntityDelay baseEntity;

        public virtual void Dispose() { }
    }
}