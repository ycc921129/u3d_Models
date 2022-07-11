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
    /// Combo_test_easy VOModel
    /// [连消配置表] [A] [_Excel/游戏配置表/C_连消配置表_A.xlsx]
    /// </summary>
    public partial class Combo_test_easyVOModel : BaseVOModel<Combo_test_easyVOModel, Combo_test_easyVO>
    {
        public override string DescName { get { return "_Excel/游戏配置表/C_连消配置表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "Combo_test_easy"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "weights", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override Combo_test_easyVO MakeVO()
        {
            Combo_test_easyVO vo = new Combo_test_easyVO(this);
            return vo;
        }
    }
}