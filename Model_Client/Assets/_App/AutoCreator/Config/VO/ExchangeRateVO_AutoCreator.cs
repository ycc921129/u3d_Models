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
    /// ExchangeRate VO
    /// [汇率表] [A] [_Excel/激励配置表/H_汇率表_A.xlsx]
    /// </summary>
    public partial class ExchangeRateVO : BaseVO
    {
        /// <summary>
        /// 礼品卡种类
        /// 标识类型: A
        /// </summary>
        public int giftType;

        /// <summary>
        /// 游戏名字
        /// 标识类型: A
        /// </summary>
        public string gameName;

        /// <summary>
        /// 包名字
        /// 标识类型: A
        /// </summary>
        public string pgName;

        /// <summary>
        /// 汇率
        /// 标识类型: A
        /// </summary>
        public float exchangeRateValue;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public ExchangeRateVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public ExchangeRateVO(VOModel voModel) : base(voModel) { }
    }
}