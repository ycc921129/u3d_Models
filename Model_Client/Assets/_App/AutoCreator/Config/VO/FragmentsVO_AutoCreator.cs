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
    /// Fragments(BaseFragments) VO
    /// [碎片产出表] [A] [_Excel/激励配置表/C_碎片产出表_A.xlsx]
    /// </summary>
    public partial class FragmentsVO : BaseFragmentsVO
    {
        /// <summary>
        /// 自定义构造
        /// </summary>
        public FragmentsVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public FragmentsVO(VOModel voModel) : base(voModel) { }
    }
}