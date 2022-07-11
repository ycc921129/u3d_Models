/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

namespace ProjectApp.Data
{
    /// <summary>
    /// GiftRedeem VOField
    /// [钻石礼包兑换条件表] [A] [_Excel/激励配置表/Z_钻石礼包兑换条件表_A.xlsx]
    /// </summary>
    public static class GiftRedeemVOField
    {
        /// <summary>
        /// 礼品卡名字
        /// 标识类型: A
        /// </summary>
        public static string giftName = "giftName";

        /// <summary>
        /// 礼品卡种类
        /// 标识类型: A
        /// </summary>
        public static string giftType = "giftType";

        /// <summary>
        /// 是否是X天方案(0:不是 1:是)
        /// 标识类型: A
        /// </summary>
        public static string IsDay_X = "IsDay_X";

        /// <summary>
        /// 兑换奖励
        /// 标识类型: A
        /// </summary>
        public static string gameValue = "gameValue";

        /// <summary>
        /// 兑换礼包（0.关卡 1.视频）
        /// 标识类型: A
        /// </summary>
        public static string redeemType = "redeemType";

        /// <summary>
        /// 条件1(消耗金币)
        /// 标识类型: A
        /// </summary>
        public static string step1_redeem = "step1_redeem";

        /// <summary>
        /// 条件2(视频目标数)
        /// 标识类型: A
        /// </summary>
        public static string step2_video = "step2_video";

        /// <summary>
        /// 条件2(等待时间，单位小时)
        /// 标识类型: A
        /// </summary>
        public static string step2_waitTime = "step2_waitTime";

        /// <summary>
        /// 条件4(排队,初始值)
        /// 标识类型: A
        /// </summary>
        public static string step3_rank = "step3_rank";

        /// <summary>
        /// 条件4(排队,条件切换值)
        /// 标识类型: A
        /// </summary>
        public static string step3_rankChange = "step3_rankChange";

        /// <summary>
        /// 条件4(条件切换后看视频加的排名)
        /// 标识类型: A
        /// </summary>
        public static string step3_ChangeByAddRank = "step3_ChangeByAddRank";

        /// <summary>
        /// 条件4(切换前，看1次视频加排名)
        /// 标识类型: A
        /// </summary>
        public static string step3_addRankByVideo = "step3_addRankByVideo";

        /// <summary>
        /// 条件4(切换前，每X秒加X排名)
        /// 标识类型: A
        /// </summary>
        public static string step3_addRankByTime = "step3_addRankByTime";
    }
}