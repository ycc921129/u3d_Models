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
    /// FlipReward(BaseFlipReward) VOModel
    /// [翻牌产出表] [A] [_Excel/激励配置表/F_翻牌产出表_A.xlsx]
    /// </summary>
    public partial class FlipRewardVOModel : BaseVOModel<FlipRewardVOModel, FlipRewardVO>
    {
        public override string DescName { get { return "_Excel/激励配置表/F_翻牌产出表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string BaseVOName { get { return "BaseFlipReward"; } }
        public override string VOName { get { return "FlipReward"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "diamond", "cardRange", "cardReward1", "cardReward2", "cardReward3", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override FlipRewardVO MakeVO()
        {
            FlipRewardVO vo = new FlipRewardVO(this);
            return vo;
        }
    }
}