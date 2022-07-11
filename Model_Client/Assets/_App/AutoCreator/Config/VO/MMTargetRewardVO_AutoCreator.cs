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
    /// MMTargetReward VO
    /// [短期目标配置表] [A] [_Excel/激励配置表/D_短期目标配置表_A.xlsx]
    /// </summary>
    public partial class MMTargetRewardVO : BaseVO
    {
        /// <summary>
        /// 短期目标奖励等级
        /// 标识类型: A
        /// </summary>
        public int levelTarget;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public MMTargetRewardVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public MMTargetRewardVO(VOModel voModel) : base(voModel) { }
    }
}