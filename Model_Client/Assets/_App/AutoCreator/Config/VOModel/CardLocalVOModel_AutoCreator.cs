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
    /// CardLocal VOModel
    /// [激励本地化] [A] [_Excel/通用配置表/B_激励本地化_A.xlsx]
    /// </summary>
    public partial class CardLocalVOModel : BaseVOModel<CardLocalVOModel, CardLocalVO>
    {
        public override string DescName { get { return "_Excel/通用配置表/B_激励本地化_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "CardLocal"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "Name", "Codename", "Value", "currency", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override CardLocalVO MakeVO()
        {
            CardLocalVO vo = new CardLocalVO(this);
            return vo;
        }
    }
}