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
    /// Combo_test_easy VO
    /// [连消配置表] [A] [_Excel/游戏配置表/C_连消配置表_A.xlsx]
    /// </summary>
    public partial class Combo_test_easyVO : BaseVO
    {
        /// <summary>
        /// 第一行为分数区间配置，后面的数据为权重配置
        /// 标识类型: A
        /// </summary>
        public int[] weights;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public Combo_test_easyVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public Combo_test_easyVO(VOModel voModel) : base(voModel) { }
    }
}