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
    /// ComboReward(BaseComboReward) VO
    /// [连消产出表] [A] [_Excel/激励配置表/C_连消产出表_A.xlsx]
    /// </summary>
    public partial class ComboRewardVO : BaseComboRewardVO
    {
        /// <summary>
        /// 自定义构造
        /// </summary>
        public ComboRewardVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public ComboRewardVO(VOModel voModel) : base(voModel) { }
    }
}