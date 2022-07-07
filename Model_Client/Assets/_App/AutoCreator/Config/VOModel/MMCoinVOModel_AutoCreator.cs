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
    /// MMCoin VOModel
    /// [金币产出数值表] [A] [_Excel/通用配置表/J_金币产出数值表_A.xlsx]
    /// </summary>
    public partial class MMCoinVOModel : BaseVOModel<MMCoinVOModel, MMCoinVO>
    {
        public override string DescName { get { return "_Excel/通用配置表/J_金币产出数值表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "MMCoin"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "level", "rewardRange", "rewardAverage", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override MMCoinVO MakeVO()
        {
            MMCoinVO vo = new MMCoinVO(this);
            return vo;
        }
    }
}