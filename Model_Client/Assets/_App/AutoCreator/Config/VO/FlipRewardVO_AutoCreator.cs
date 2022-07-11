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
    /// FlipReward(BaseFlipReward) VO
    /// [翻牌产出表] [A] [_Excel/激励配置表/F_翻牌产出表_A.xlsx]
    /// </summary>
    public partial class FlipRewardVO : BaseFlipRewardVO
    {
        /// <summary>
        /// 自定义构造
        /// </summary>
        public FlipRewardVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public FlipRewardVO(VOModel voModel) : base(voModel) { }
    }
}