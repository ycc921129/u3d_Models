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
    public class BaseMonoComponent : MonoBehaviour
    {
        public BaseEntity baseEntity;

        public virtual void Dispose() { }

        #region Mono一些用法
        //public RangeInt rangeInt;

        //[Range(0, 100)]
        //public float rangeFloat;
        #endregion
    }
}