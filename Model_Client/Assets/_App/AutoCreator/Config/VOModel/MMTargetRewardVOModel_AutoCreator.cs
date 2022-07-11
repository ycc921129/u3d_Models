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
    /// MMTargetReward VOModel
    /// [短期目标配置表] [A] [_Excel/激励配置表/D_短期目标配置表_A.xlsx]
    /// </summary>
    public partial class MMTargetRewardVOModel : BaseVOModel<MMTargetRewardVOModel, MMTargetRewardVO>
    {
        public override string DescName { get { return "_Excel/激励配置表/D_短期目标配置表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "MMTargetReward"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "levelTarget", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override MMTargetRewardVO MakeVO()
        {
            MMTargetRewardVO vo = new MMTargetRewardVO(this);
            return vo;
        }
    }
}