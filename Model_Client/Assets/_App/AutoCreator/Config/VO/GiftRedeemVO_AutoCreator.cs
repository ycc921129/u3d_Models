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
    /// GiftRedeem VO
    /// [钻石礼包兑换条件表] [A] [_Excel/激励配置表/Z_钻石礼包兑换条件表_A.xlsx]
    /// </summary>
    public partial class GiftRedeemVO : BaseVO
    {
        /// <summary>
        /// 礼品卡名字
        /// 标识类型: A
        /// </summary>
        public string giftName;

        /// <summary>
        /// 礼品卡种类
        /// 标识类型: A
        /// </summary>
        public int giftType;

        /// <summary>
        /// 是否是X天方案(0:不是 1:是)
        /// 标识类型: A
        /// </summary>
        public int IsDay_X;

        /// <summary>
        /// 兑换奖励
        /// 标识类型: A
        /// </summary>
        public int gameValue;

        /// <summary>
        /// 兑换礼包（0.关卡 1.视频）
        /// 标识类型: A
        /// </summary>
        public int redeemType;

        /// <summary>
        /// 条件1(消耗金币)
        /// 标识类型: A
        /// </summary>
        public int step1_redeem;

        /// <summary>
        /// 条件2(视频目标数)
        /// 标识类型: A
        /// </summary>
        public int step2_video;

        /// <summary>
        /// 条件2(等待时间，单位小时)
        /// 标识类型: A
        /// </summary>
        public int step2_waitTime;

        /// <summary>
        /// 条件4(排队,初始值)
        /// 标识类型: A
        /// </summary>
        public int[] step3_rank;

        /// <summary>
        /// 条件4(排队,条件切换值)
        /// 标识类型: A
        /// </summary>
        public int step3_rankChange;

        /// <summary>
        /// 条件4(条件切换后看视频加的排名)
        /// 标识类型: A
        /// </summary>
        public int step3_ChangeByAddRank;

        /// <summary>
        /// 条件4(切换前，看1次视频加排名)
        /// 标识类型: A
        /// </summary>
        public int[] step3_addRankByVideo;

        /// <summary>
        /// 条件4(切换前，每X秒加X排名)
        /// 标识类型: A
        /// </summary>
        public int[] step3_addRankByTime;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public GiftRedeemVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public GiftRedeemVO(VOModel voModel) : base(voModel) { }
    }
}