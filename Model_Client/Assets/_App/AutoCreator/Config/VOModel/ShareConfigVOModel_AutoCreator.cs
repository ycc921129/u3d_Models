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
    /// ShareConfig VOModel
    /// [社媒入口] [C] [_Excel/通用配置表/S_社媒入口_C.xlsx]
    /// </summary>
    public partial class ShareConfigVOModel : BaseVOModel<ShareConfigVOModel, ShareConfigVO>
    {
        public override string DescName { get { return "_Excel/通用配置表/S_社媒入口_C.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "ShareConfig"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.Client; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "jump", "androidLink", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override ShareConfigVO MakeVO()
        {
            ShareConfigVO vo = new ShareConfigVO(this);
            return vo;
        }
    }
}