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
    /// Langue(BaseLangue)-动态多语言 VO
    /// [多语言通用表] [L] [_Excel/本地配置表/D_多语言通用表_L (动态多语言).xlsx]
    /// </summary>
    public partial class LangueVO : BaseLangueVO
    {
        /// <summary>
        /// 马来西亚
        /// 标识类型: L
        /// </summary>
        public string ms;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public LangueVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public LangueVO(VOModel voModel) : base(voModel) { }
    }
}