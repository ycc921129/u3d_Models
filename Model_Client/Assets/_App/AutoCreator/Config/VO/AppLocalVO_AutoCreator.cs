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
    /// AppLocal VO
    /// [应用本地表] [L] [_Excel/本地配置表/Y_应用本地表_L.xlsx]
    /// </summary>
    public partial class AppLocalVO : BaseVO
    {
        /// <summary>
        /// 值
        /// 标识类型: L
        /// </summary>
        public int value;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public AppLocalVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public AppLocalVO(VOModel voModel) : base(voModel) { }
    }
}