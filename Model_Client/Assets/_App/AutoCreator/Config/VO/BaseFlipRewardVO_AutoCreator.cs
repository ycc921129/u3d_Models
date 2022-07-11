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
    /// BaseFlipReward VO
    /// [翻牌产出基类表] [Base] [_Excel/基类配置表/F_翻牌产出基类表_Base.xlsx]
    /// </summary>
    public partial class BaseFlipRewardVO : BaseVO
    {
        /// <summary>
        /// 钻石当前值
        /// 标识类型: Base
        /// </summary>
        public int diamond;

        /// <summary>
        /// 权重
        /// 标识类型: Base
        /// </summary>
        public int[] cardRange;

        /// <summary>
        /// 奖励1
        /// 标识类型: Base
        /// </summary>
        public int[] cardReward1;

        /// <summary>
        /// 奖励2
        /// 标识类型: Base
        /// </summary>
        public int[] cardReward2;

        /// <summary>
        /// 奖励3
        /// 标识类型: Base
        /// </summary>
        public int[] cardReward3;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public BaseFlipRewardVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public BaseFlipRewardVO(VOModel voModel) : base(voModel) { }
    }
}