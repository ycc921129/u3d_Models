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
    /// FlipReward_X(BaseFlipReward) VOModel
    /// [翻牌产出表] [A] [_Excel/激励配置表/F_翻牌产出表_A.xlsx]
    /// </summary>
    public partial class FlipReward_XVOModel : BaseVOModel<FlipReward_XVOModel, FlipReward_XVO>
    {
        public override string DescName { get { return "_Excel/激励配置表/F_翻牌产出表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string BaseVOName { get { return "BaseFlipReward"; } }
        public override string VOName { get { return "FlipReward_X"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "diamond", "cardRange", "cardReward1", "cardReward2", "cardReward3", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override FlipReward_XVO MakeVO()
        {
            FlipReward_XVO vo = new FlipReward_XVO(this);
            return vo;
        }
    }
}