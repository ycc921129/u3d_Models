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
    /// CheckIn VOModel
    /// [签到数值表] [A] [_Excel/通用配置表/Q_签到数值表_A.xlsx]
    /// </summary>
    public partial class CheckInVOModel : BaseVOModel<CheckInVOModel, CheckInVO>
    {
        public override string DescName { get { return "_Excel/通用配置表/Q_签到数值表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "CheckIn"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "reward", "limitReward", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override CheckInVO MakeVO()
        {
            CheckInVO vo = new CheckInVO(this);
            return vo;
        }
    }
}