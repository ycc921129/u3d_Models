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
    /// Combo VOModel
    /// [连消配置表] [A] [_Excel/游戏配置表/C_连消配置表_A.xlsx]
    /// </summary>
    public partial class ComboVOModel : BaseVOModel<ComboVOModel, ComboVO>
    {
        public override string DescName { get { return "_Excel/游戏配置表/C_连消配置表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "Combo"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "weights", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override ComboVO MakeVO()
        {
            ComboVO vo = new ComboVO(this);
            return vo;
        }
    }
}