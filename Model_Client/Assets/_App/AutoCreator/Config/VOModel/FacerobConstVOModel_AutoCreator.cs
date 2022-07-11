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
    /// FacerobConst VOModel
    /// [游戏常量表] [A] [_Excel/游戏配置表/Y_游戏常量表_A.xlsx]
    /// </summary>
    public partial class FacerobConstVOModel : BaseVOModel<FacerobConstVOModel, FacerobConstVO>
    {
        public override string DescName { get { return "_Excel/游戏配置表/Y_游戏常量表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "FacerobConst"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "staticKey", "staticValue", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return true; } }

        protected override FacerobConstVO MakeVO()
        {
            FacerobConstVO vo = new FacerobConstVO(this);
            return vo;
        }

        protected override void InitVOStaticField()
        {
            FacerobConstVOStatic.InitStaticField();
        }
    }
}