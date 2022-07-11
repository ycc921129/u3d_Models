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
    /// Combo_test VO
    /// [连消配置表] [A] [_Excel/游戏配置表/C_连消配置表_A.xlsx]
    /// </summary>
    public partial class Combo_testVO : BaseVO
    {
        /// <summary>
        /// 第一行为分数区间配置，后面的数据为权重配置
        /// 标识类型: A
        /// </summary>
        public int[] weights;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public Combo_testVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public Combo_testVO(VOModel voModel) : base(voModel) { }
    }
}