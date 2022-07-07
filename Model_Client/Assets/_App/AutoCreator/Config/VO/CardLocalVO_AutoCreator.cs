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
    /// CardLocal VO
    /// [激励本地化] [A] [_Excel/通用配置表/B_激励本地化_A.xlsx]
    /// </summary>
    public partial class CardLocalVO : BaseVO
    {
        /// <summary>
        /// 国家(国家顺序勿动)
        /// 标识类型: A
        /// </summary>
        public string Name;

        /// <summary>
        /// 语言代号
        /// 标识类型: A
        /// </summary>
        public string[] Codename;

        /// <summary>
        /// 1个金币换算的倍率
        /// 标识类型: A
        /// </summary>
        public float Value;

        /// <summary>
        /// 货币符号
        /// 标识类型: A
        /// </summary>
        public string currency;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public CardLocalVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public CardLocalVO(VOModel voModel) : base(voModel) { }
    }
}