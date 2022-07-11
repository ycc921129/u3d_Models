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
    /// GiftRedeem VOModel
    /// [钻石礼包兑换条件表] [A] [_Excel/激励配置表/Z_钻石礼包兑换条件表_A.xlsx]
    /// </summary>
    public partial class GiftRedeemVOModel : BaseVOModel<GiftRedeemVOModel, GiftRedeemVO>
    {
        public override string DescName { get { return "_Excel/激励配置表/Z_钻石礼包兑换条件表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "GiftRedeem"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "giftName", "giftType", "IsDay_X", "gameValue", "redeemType", "step1_redeem", "step2_video", "step2_waitTime", "step3_rank", "step3_rankChange", "step3_ChangeByAddRank", "step3_addRankByVideo", "step3_addRankByTime", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override GiftRedeemVO MakeVO()
        {
            GiftRedeemVO vo = new GiftRedeemVO(this);
            return vo;
        }
    }
}