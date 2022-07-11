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
    /// LevelAdd VOModel
    /// [等级配置表] [A] [_Excel/游戏配置表/L_等级配置表_A.xlsx]
    /// </summary>
    public partial class LevelAddVOModel : BaseVOModel<LevelAddVOModel, LevelAddVO>
    {
        public override string DescName { get { return "_Excel/游戏配置表/L_等级配置表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "LevelAdd"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "levelUp", "score", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override LevelAddVO MakeVO()
        {
            LevelAddVO vo = new LevelAddVO(this);
            return vo;
        }
    }
}