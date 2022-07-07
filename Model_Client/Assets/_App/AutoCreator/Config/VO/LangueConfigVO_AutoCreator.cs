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
    /// LangueConfig(BaseLangue) VO
    /// [多语言配置模块表] [L] [_Excel/本地配置表/D_多语言配置模块表_L (动态多语言) .xlsx]
    /// </summary>
    public partial class LangueConfigVO : BaseLangueVO
    {
        /// <summary>
        /// 自定义构造
        /// </summary>
        public LangueConfigVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public LangueConfigVO(VOModel voModel) : base(voModel) { }
    }
}