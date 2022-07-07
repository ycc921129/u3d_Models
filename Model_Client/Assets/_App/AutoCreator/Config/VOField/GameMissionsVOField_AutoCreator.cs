/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

namespace ProjectApp.Data
{
    /// <summary>
    /// GameMissions VOField
    /// [Card兑换表] [A] [_Excel/游戏配置表/C_Card兑换表_A.xlsx]
    /// </summary>
    public static class GameMissionsVOField
    {
        /// <summary>
        /// 兑换国家
        /// 标识类型: A
        /// </summary>
        public static string missionCountries = "missionCountries";

        /// <summary>
        /// 兑换真金对应ID(测服包)
        /// 标识类型: A
        /// </summary>
        public static string exchangeID = "exchangeID";

        /// <summary>
        /// 兑换金额(测服包)
        /// 标识类型: A
        /// </summary>
        public static string amount = "amount";

        /// <summary>
        /// 兑换真金对应ID(正式包)
        /// 标识类型: A
        /// </summary>
        public static string exchangeID_release = "exchangeID_release";

        /// <summary>
        /// 兑换金额(正式包)
        /// 标识类型: A
        /// </summary>
        public static string amount_release = "amount_release";

        /// <summary>
        /// 兑换奖励种类(0:金币 1:真金)
        /// 标识类型: A
        /// </summary>
        public static string rewardType = "rewardType";

        /// <summary>
        /// 金币(种类不是金币就忽略)
        /// 标识类型: A
        /// </summary>
        public static string coin = "coin";

        /// <summary>
        /// 任务数据
        /// 标识类型: A
        /// </summary>
        public static string taskDic = "taskDic";
    }
}