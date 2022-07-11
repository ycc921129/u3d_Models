/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using FutureCore;
using FutureCore.Data;

namespace ProjectApp.Data
{
    /// <summary>
    /// FacerobRewardConst VOStaticKey
    /// [激励常量表] [A] [_Excel/激励配置表/J_激励常量表_A.xlsx]
    /// </summary>
    public static class FacerobRewardConstVOStaticKey
    {
        /// <summary>
        /// [id=1]
        /// X天方案时间（单位：小时）
        /// </summary>
        public const string XdayBywaitTime = "XdayBywaitTime";

        /// <summary>
        /// [id=2]
        /// X天方案奖励最大值
        /// </summary>
        public const string XdayByMaxValue = "XdayByMaxValue";

        /// <summary>
        /// [id=3]
        /// 短期目标种类（0：货币 1：碎片 2：货币且碎片）
        /// </summary>
        public const string RewardTypeByTarget = "RewardTypeByTarget";

        /// <summary>
        /// [id=4]
        /// 短期目标奖励是否需要广告（0：不需要 1：需要）
        /// </summary>
        public const string RewardTargetIsNeedAD = "RewardTargetIsNeedAD";

        /// <summary>
        /// [id=5]
        /// 短期目标关闭按键的奖励系数
        /// </summary>
        public const string RewardTargetbyClose = "RewardTargetbyClose";

        /// <summary>
        /// [id=6]
        /// 短期目标关闭按键的领取奖励概率（0:无奖励关闭 1:有奖励 0~1：走概率是否有奖励）
        /// </summary>
        public const string RewardTargetPercentbyClose = "RewardTargetPercentbyClose";

        /// <summary>
        /// [id=7]
        /// 弃用
        /// </summary>
        public const string FlipRewardRandom = "FlipRewardRandom";

        /// <summary>
        /// [id=8]
        /// 翻牌获取碎片的概率（0~1，1:必获得碎片）
        /// </summary>
        public const string fragments_flipReward = "fragments_flipReward";

        /// <summary>
        /// [id=9]
        /// 挖矿奖励种类（0：货币 1：碎片 2：货币且碎片）
        /// </summary>
        public const string RewardTypeByKuang = "RewardTypeByKuang";

        /// <summary>
        /// [id=10]
        /// 挖矿每点击X下按键，弹窗奖励
        /// </summary>
        public const string KuangByClickCount = "KuangByClickCount";

        /// <summary>
        /// [id=11]
        /// 挖矿前X次免费奖励
        /// </summary>
        public const string KuangByFreeReward = "KuangByFreeReward";

        /// <summary>
        /// [id=12]
        /// 挖矿关闭按键的奖励系数
        /// </summary>
        public const string KuangRewardByClose = "KuangRewardByClose";

        /// <summary>
        /// [id=13]
        /// 挖矿关闭按键的领取奖励概率（0:无奖励关闭 1:有奖励 0~1：走概率是否有奖励）
        /// </summary>
        public const string KuangRewardPercentbyClose = "KuangRewardPercentbyClose";

        /// <summary>
        /// [id=14]
        /// 5连消奖励种类（0：货币 1：碎片 2：货币且碎片）
        /// </summary>
        public const string RewardTypeByNormal = "RewardTypeByNormal";

        /// <summary>
        /// [id=15]
        /// 超级连消达到X次后才弹出奖励
        /// </summary>
        public const string ComboRewardCount = "ComboRewardCount";

        /// <summary>
        /// [id=16]
        /// 连消关闭按钮领取奖励系数
        /// </summary>
        public const string ComboRewardbyClose = "ComboRewardbyClose";

        /// <summary>
        /// [id=17]
        /// 连消关闭按钮领取奖励概率（0:无奖励关闭 1:有奖励 0~1：走概率是否有奖励）
        /// </summary>
        public const string ComboRewardPercentbyClose = "ComboRewardPercentbyClose";

        /// <summary>
        /// [id=18]
        /// 钻石显示整数还是小数，X值以后是小数
        /// </summary>
        public const string DiamondShowIntOrFloat = "DiamondShowIntOrFloat";

        /// <summary>
        /// [id=19]
        /// 超级连消达到X次后弹出大富翁奖励
        /// </summary>
        public const string BigRewardCount = "BigRewardCount";

        /// <summary>
        /// [id=20]
        /// 大富翁奖励运动速度(越小越快)
        /// </summary>
        public const string BigRewardSpeed = "BigRewardSpeed";

        /// <summary>
        /// [id=21]
        /// 大富翁奖励倍数
        /// </summary>
        public const string BigRewards = "BigRewards";

        /// <summary>
        /// [id=22]
        /// 大富翁关闭按钮领取奖励系数
        /// </summary>
        public const string BigRewardbyClose = "BigRewardbyClose";

        /// <summary>
        /// [id=23]
        /// 大富翁关闭按钮领取奖励概率（0:无奖励关闭 1:有奖励 0~1：走概率是否有奖励）
        /// </summary>
        public const string BigRewardPercentbyClose = "BigRewardPercentbyClose";

        /// <summary>
        /// [id=24]
        /// 所有奖励免费次数
        /// </summary>
        public const string FreeRewardCount = "FreeRewardCount";
    }
}