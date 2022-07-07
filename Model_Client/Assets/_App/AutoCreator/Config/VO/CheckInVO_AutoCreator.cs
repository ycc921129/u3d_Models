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
    /// CheckIn VO
    /// [签到数值表] [A] [_Excel/通用配置表/Q_签到数值表_A.xlsx]
    /// </summary>
    public partial class CheckInVO : BaseVO
    {
        /// <summary>
        /// 奖励值
        /// 标识类型: A
        /// </summary>
        public int[] reward;

        /// <summary>
        /// 到达阈值后产出
        /// 标识类型: A
        /// </summary>
        public int[] limitReward;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public CheckInVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public CheckInVO(VOModel voModel) : base(voModel) { }
    }
}