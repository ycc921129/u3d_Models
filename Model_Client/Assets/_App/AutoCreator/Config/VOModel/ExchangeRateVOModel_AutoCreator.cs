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
    /// ExchangeRate VOModel
    /// [汇率表] [A] [_Excel/激励配置表/H_汇率表_A.xlsx]
    /// </summary>
    public partial class ExchangeRateVOModel : BaseVOModel<ExchangeRateVOModel, ExchangeRateVO>
    {
        public override string DescName { get { return "_Excel/激励配置表/H_汇率表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "ExchangeRate"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "giftType", "gameName", "pgName", "exchangeRateValue", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override ExchangeRateVO MakeVO()
        {
            ExchangeRateVO vo = new ExchangeRateVO(this);
            return vo;
        }
    }
}