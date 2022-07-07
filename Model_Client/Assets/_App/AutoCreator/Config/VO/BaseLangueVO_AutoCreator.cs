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
    /// BaseLangue-动态多语言 VO
    /// [多语言基类表] [Base] [_Excel/本地配置表/D_多语言基类表_Base (动态多语言).xlsx]
    /// </summary>
    public partial class BaseLangueVO : BaseVO
    {
        /// <summary>
        /// 英文文本
        /// 标识类型: Base
        /// </summary>
        public string en;

        /// <summary>
        /// 默认中文是繁体中文
        /// 标识类型: Base
        /// </summary>
        public string zh;

        /// <summary>
        /// 简体中文
        /// 标识类型: Base
        /// </summary>
        public string zh_CN;

        /// <summary>
        /// 西班牙
        /// 标识类型: Base
        /// </summary>
        public string es;

        /// <summary>
        /// 法国
        /// 标识类型: Base
        /// </summary>
        public string fr;

        /// <summary>
        /// 日本
        /// 标识类型: Base
        /// </summary>
        public string ja;

        /// <summary>
        /// 德国
        /// 标识类型: Base
        /// </summary>
        public string de;

        /// <summary>
        /// 俄罗斯
        /// 标识类型: Base
        /// </summary>
        public string ru;

        /// <summary>
        /// 葡萄牙
        /// 标识类型: Base
        /// </summary>
        public string pt;

        /// <summary>
        /// 印度尼西亚（in是关键字，需要加@）
        /// 标识类型: Base
        /// </summary>
        public string @in;

        /// <summary>
        /// 印地语
        /// 标识类型: Base
        /// </summary>
        public string hi_IN;

        /// <summary>
        /// 越南
        /// 标识类型: Base
        /// </summary>
        public string vi;

        /// <summary>
        /// 土耳其
        /// 标识类型: Base
        /// </summary>
        public string tr;

        /// <summary>
        /// 阿拉伯
        /// 标识类型: Base
        /// </summary>
        public string ar;

        /// <summary>
        /// 波兰
        /// 标识类型: Base
        /// </summary>
        public string pl;

        /// <summary>
        /// 泰国
        /// 标识类型: Base
        /// </summary>
        public string th;

        /// <summary>
        /// 韩国
        /// 标识类型: Base
        /// </summary>
        public string ko;

        /// <summary>
        /// 乌克兰
        /// 标识类型: Base
        /// </summary>
        public string uk;

        /// <summary>
        /// 罗马尼亚
        /// 标识类型: Base
        /// </summary>
        public string ro;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public BaseLangueVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public BaseLangueVO(VOModel voModel) : base(voModel) { }
    }
}