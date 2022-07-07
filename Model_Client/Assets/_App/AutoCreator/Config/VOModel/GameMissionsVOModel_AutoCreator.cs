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
    /// GameMissions VOModel
    /// [Card兑换表] [A] [_Excel/游戏配置表/C_Card兑换表_A.xlsx]
    /// </summary>
    public partial class GameMissionsVOModel : BaseVOModel<GameMissionsVOModel, GameMissionsVO>
    {
        public override string DescName { get { return "_Excel/游戏配置表/C_Card兑换表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "GameMissions"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "missionCountries", "exchangeID", "amount", "exchangeID_release", "amount_release", "rewardType", "coin", "taskDic", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override GameMissionsVO MakeVO()
        {
            GameMissionsVO vo = new GameMissionsVO(this);
            return vo;
        }
    }
}