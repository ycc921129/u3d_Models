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
    /// BaseComboReward VO
    /// [连消产出基类表] [Base] [_Excel/基类配置表/C_连消产出基类表_Base.xlsx]
    /// </summary>
    public partial class BaseComboRewardVO : BaseVO
    {
        /// <summary>
        /// 钻石当前值
        /// 标识类型: Base
        /// </summary>
        public int diamond;

        /// <summary>
        /// 钻石奖励范围
        /// 标识类型: Base
        /// </summary>
        public int[] diamondRange;

        /// <summary>
        /// 钻石奖励期望值
        /// 标识类型: Base
        /// </summary>
        public int diamondAverage;

        /// <summary>
        /// 奖励系数（连消，挖矿，大富翁单倍数值，短期奖励)
        /// 标识类型: Base
        /// </summary>
        public float[] multipleDiamond;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public BaseComboRewardVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public BaseComboRewardVO(VOModel voModel) : base(voModel) { }
    }
}