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
    /// FacerobRewardConst VOModel
    /// [激励常量表] [A] [_Excel/激励配置表/J_激励常量表_A.xlsx]
    /// </summary>
    public partial class FacerobRewardConstVOModel : BaseVOModel<FacerobRewardConstVOModel, FacerobRewardConstVO>
    {
        public override string DescName { get { return "_Excel/激励配置表/J_激励常量表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "FacerobRewardConst"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "staticKey", "staticValue", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return true; } }

        protected override FacerobRewardConstVO MakeVO()
        {
            FacerobRewardConstVO vo = new FacerobRewardConstVO(this);
            return vo;
        }

        protected override void InitVOStaticField()
        {
            FacerobRewardConstVOStatic.InitStaticField();
        }
    }
}