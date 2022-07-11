/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using System;
using FutureCore;
using FutureCore.Data;

namespace ProjectApp.Data
{
    /// <summary>
    /// BaseFragments VO
    /// [连消碎片产出基类表] [Base] [_Excel/基类配置表/C_连消碎片产出基类表_Base.xlsx]
    /// </summary>
    public partial class BaseFragmentsVO : BaseVO
    {
        /// <summary>
        /// 碎片当前值
        /// 标识类型: Base
        /// </summary>
        public int fragments;

        /// <summary>
        /// 碎片奖励范围
        /// 标识类型: Base
        /// </summary>
        public int[] fragmentsRange;

        /// <summary>
        /// 碎片奖励期望值
        /// 标识类型: Base
        /// </summary>
        public int fragmentsAverage;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public BaseFragmentsVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public BaseFragmentsVO(VOModel voModel) : base(voModel) { }
    }
}