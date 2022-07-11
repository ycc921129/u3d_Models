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
    /// Combo_test VOModel
    /// [连消配置表] [A] [_Excel/游戏配置表/C_连消配置表_A.xlsx]
    /// </summary>
    public partial class Combo_testVOModel : BaseVOModel<Combo_testVOModel, Combo_testVO>
    {
        public override string DescName { get { return "_Excel/游戏配置表/C_连消配置表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "Combo_test"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "weights", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override Combo_testVO MakeVO()
        {
            Combo_testVO vo = new Combo_testVO(this);
            return vo;
        }
    }
}