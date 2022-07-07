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
    /// MMCoin VO
    /// [金币产出数值表] [A] [_Excel/通用配置表/J_金币产出数值表_A.xlsx]
    /// </summary>
    public partial class MMCoinVO : BaseVO
    {
        /// <summary>
        /// 当前金币值
        /// 标识类型: A
        /// </summary>
        public float level;

        /// <summary>
        /// 奖励范围
        /// 标识类型: A
        /// </summary>
        public float[] rewardRange;

        /// <summary>
        /// 产出期望
        /// 标识类型: A
        /// </summary>
        public float rewardAverage;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public MMCoinVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public MMCoinVO(VOModel voModel) : base(voModel) { }
    }
}