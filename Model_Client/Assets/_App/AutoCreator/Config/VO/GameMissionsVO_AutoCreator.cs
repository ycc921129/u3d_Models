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
    /// GameMissions VO
    /// [Card兑换表] [A] [_Excel/游戏配置表/C_Card兑换表_A.xlsx]
    /// </summary>
    public partial class GameMissionsVO : BaseVO
    {
        /// <summary>
        /// 兑换国家
        /// 标识类型: A
        /// </summary>
        public string[] missionCountries;

        /// <summary>
        /// 兑换真金对应ID(测服包)
        /// 标识类型: A
        /// </summary>
        public int[] exchangeID;

        /// <summary>
        /// 兑换金额(测服包)
        /// 标识类型: A
        /// </summary>
        public float[] amount;

        /// <summary>
        /// 兑换真金对应ID(正式包)
        /// 标识类型: A
        /// </summary>
        public int[] exchangeID_release;

        /// <summary>
        /// 兑换金额(正式包)
        /// 标识类型: A
        /// </summary>
        public float[] amount_release;

        /// <summary>
        /// 兑换奖励种类(0:金币 1:真金)
        /// 标识类型: A
        /// </summary>
        public int rewardType;

        /// <summary>
        /// 金币(种类不是金币就忽略)
        /// 标识类型: A
        /// </summary>
        public int coin;

        /// <summary>
        /// 任务数据
        /// 标识类型: A
        /// </summary>
        public Dictionary<string, int> taskDic;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public GameMissionsVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public GameMissionsVO(VOModel voModel) : base(voModel) { }
    }
}