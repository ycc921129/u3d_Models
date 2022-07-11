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
    /// ComboReward_X(BaseComboReward) VOModel
    /// [连消产出表] [A] [_Excel/激励配置表/C_连消产出表_A.xlsx]
    /// </summary>
    public partial class ComboReward_XVOModel : BaseVOModel<ComboReward_XVOModel, ComboReward_XVO>
    {
        public override string DescName { get { return "_Excel/激励配置表/C_连消产出表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string BaseVOName { get { return "BaseComboReward"; } }
        public override string VOName { get { return "ComboReward_X"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "diamond", "diamondRange", "diamondAverage", "multipleDiamond", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override ComboReward_XVO MakeVO()
        {
            ComboReward_XVO vo = new ComboReward_XVO(this);
            return vo;
        }
    }
}