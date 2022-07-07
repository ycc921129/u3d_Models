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
    /// Insterstitial VO
    /// [插屏表] [A] [_Excel/通用配置表/C_插屏表_A.xlsx]
    /// </summary>
    public partial class InsterstitialVO : BaseVO
    {
        /// <summary>
        /// 激活插屏天数
        /// 标识类型: A
        /// </summary>
        public int activation;

        /// <summary>
        /// 每日插屏上限
        /// 标识类型: A
        /// </summary>
        public int insertMax;

        /// <summary>
        /// 插屏间隔/s
        /// 标识类型: A
        /// </summary>
        public int insertInterval;

        /// <summary>
        /// 点击插屏失效次数
        /// 标识类型: A
        /// </summary>
        public int intervalInvalid;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public InsterstitialVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public InsterstitialVO(VOModel voModel) : base(voModel) { }
    }
}