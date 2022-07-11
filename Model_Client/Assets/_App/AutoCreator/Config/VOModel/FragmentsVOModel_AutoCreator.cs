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
    /// Fragments(BaseFragments) VOModel
    /// [碎片产出表] [A] [_Excel/激励配置表/C_碎片产出表_A.xlsx]
    /// </summary>
    public partial class FragmentsVOModel : BaseVOModel<FragmentsVOModel, FragmentsVO>
    {
        public override string DescName { get { return "_Excel/激励配置表/C_碎片产出表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string BaseVOName { get { return "BaseFragments"; } }
        public override string VOName { get { return "Fragments"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "fragments", "fragmentsRange", "fragmentsAverage", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override FragmentsVO MakeVO()
        {
            FragmentsVO vo = new FragmentsVO(this);
            return vo;
        }
    }
}