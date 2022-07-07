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
    /// Insterstitial VOModel
    /// [插屏表] [A] [_Excel/通用配置表/C_插屏表_A.xlsx]
    /// </summary>
    public partial class InsterstitialVOModel : BaseVOModel<InsterstitialVOModel, InsterstitialVO>
    {
        public override string DescName { get { return "_Excel/通用配置表/C_插屏表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "Insterstitial"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "activation", "insertMax", "insertInterval", "intervalInvalid", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override InsterstitialVO MakeVO()
        {
            InsterstitialVO vo = new InsterstitialVO(this);
            return vo;
        }
    }
}