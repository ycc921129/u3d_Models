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
    /// Fragments_Redeem VOModel
    /// [碎片兑换条件表] [A] [_Excel/激励配置表/S_碎片兑换条件表_A.xlsx]
    /// </summary>
    public partial class Fragments_RedeemVOModel : BaseVOModel<Fragments_RedeemVOModel, Fragments_RedeemVO>
    {
        public override string DescName { get { return "_Excel/激励配置表/S_碎片兑换条件表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "Fragments_Redeem"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "fragmentsID", "fragmentsLevel", "fragmentsType", "fragmentsCount", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override Fragments_RedeemVO MakeVO()
        {
            Fragments_RedeemVO vo = new Fragments_RedeemVO(this);
            return vo;
        }
    }
}