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
    /// LangueGame(BaseLangue) VO
    /// [多语言玩法表] [L] [_Excel/本地配置表/D_多语言玩法表_L (动态多语言).xlsx]
    /// </summary>
    public partial class LangueGameVO : BaseLangueVO
    {
        /// <summary>
        /// 自定义构造
        /// </summary>
        public LangueGameVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public LangueGameVO(VOModel voModel) : base(voModel) { }
    }
}