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
    /// ShareConfig VO
    /// [社媒入口] [C] [_Excel/通用配置表/S_社媒入口_C.xlsx]
    /// </summary>
    public partial class ShareConfigVO : BaseVO
    {
        /// <summary>
        /// 跳转名
        /// 标识类型: A
        /// </summary>
        public string jump;

        /// <summary>
        /// 链接
        /// 标识类型: A
        /// </summary>
        public string androidLink;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public ShareConfigVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public ShareConfigVO(VOModel voModel) : base(voModel) { }
    }
}