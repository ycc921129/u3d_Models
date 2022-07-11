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
    /// Fragments_Redeem VO
    /// [碎片兑换条件表] [A] [_Excel/激励配置表/S_碎片兑换条件表_A.xlsx]
    /// </summary>
    public partial class Fragments_RedeemVO : BaseVO
    {
        /// <summary>
        /// 英雄碎片ID
        /// 标识类型: A
        /// </summary>
        public int fragmentsID;

        /// <summary>
        /// 英雄碎片级别（0:s 1:a 2:b）
        /// 标识类型: A
        /// </summary>
        public int fragmentsLevel;

        /// <summary>
        /// 英雄碎片种类
        /// 标识类型: A
        /// </summary>
        public int fragmentsType;

        /// <summary>
        /// 英雄碎片积满数量
        /// 标识类型: A
        /// </summary>
        public int fragmentsCount;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public Fragments_RedeemVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public Fragments_RedeemVO(VOModel voModel) : base(voModel) { }
    }
}