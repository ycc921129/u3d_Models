/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using System.Collections.Generic;
using FutureCore;
using FutureCore.Data;

namespace ProjectApp.Data
{
    /// <summary>
    /// FacerobRewardConst VOStatic
    /// [激励常量表] [A] [_Excel/激励配置表/J_激励常量表_A.xlsx]
    /// </summary>
    public static class FacerobRewardConstVOStatic
    {
        /// <summary>
        /// [id=1]
        /// X天方案时间（单位：小时）
        /// 24
        /// </summary>
        public static int XdayBywaitTime = 24;

        /// <summary>
        /// [id=2]
        /// X天方案奖励最大值
        /// 5000
        /// </summary>
        public static int XdayByMaxValue = 5000;

        /// <summary>
        /// [id=3]
        /// 短期目标种类（0：货币 1：碎片 2：货币且碎片）
        /// 2
        /// </summary>
        public static int RewardTypeByTarget = 2;

        /// <summary>
        /// [id=4]
        /// 短期目标奖励是否需要广告（0：不需要 1：需要）
        /// 1
        /// </summary>
        public static int RewardTargetIsNeedAD = 1;

        /// <summary>
        /// [id=5]
        /// 短期目标关闭按键的奖励系数
        /// 0.5f
        /// </summary>
        public static float RewardTargetbyClose = 0.5f;

        /// <summary>
        /// [id=6]
        /// 短期目标关闭按键的领取奖励概率（0:无奖励关闭 1:有奖励 0~1：走概率是否有奖励）
        /// 1f
        /// </summary>
        public static float RewardTargetPercentbyClose = 1f;

        /// <summary>
        /// [id=7]
        /// 弃用
        /// 1f
        /// </summary>
        public static float FlipRewardRandom = 1f;

        /// <summary>
        /// [id=8]
        /// 翻牌获取碎片的概率（0~1，1:必获得碎片）
        /// 0.7f
        /// </summary>
        public static float fragments_flipReward = 0.7f;

        /// <summary>
        /// [id=9]
        /// 挖矿奖励种类（0：货币 1：碎片 2：货币且碎片）
        /// 2
        /// </summary>
        public static int RewardTypeByKuang = 2;

        /// <summary>
        /// [id=10]
        /// 挖矿每点击X下按键，弹窗奖励
        /// 6
        /// </summary>
        public static int KuangByClickCount = 6;

        /// <summary>
        /// [id=11]
        /// 挖矿前X次免费奖励
        /// 2
        /// </summary>
        public static int KuangByFreeReward = 2;

        /// <summary>
        /// [id=12]
        /// 挖矿关闭按键的奖励系数
        /// 0.5f
        /// </summary>
        public static float KuangRewardByClose = 0.5f;

        /// <summary>
        /// [id=13]
        /// 挖矿关闭按键的领取奖励概率（0:无奖励关闭 1:有奖励 0~1：走概率是否有奖励）
        /// 0f
        /// </summary>
        public static float KuangRewardPercentbyClose = 0f;

        /// <summary>
        /// [id=14]
        /// 5连消奖励种类（0：货币 1：碎片 2：货币且碎片）
        /// 2
        /// </summary>
        public static int RewardTypeByNormal = 2;

        /// <summary>
        /// [id=15]
        /// 超级连消达到X次后才弹出奖励
        /// 5
        /// </summary>
        public static int ComboRewardCount = 5;

        /// <summary>
        /// [id=16]
        /// 连消关闭按钮领取奖励系数
        /// 0.5f
        /// </summary>
        public static float ComboRewardbyClose = 0.5f;

        /// <summary>
        /// [id=17]
        /// 连消关闭按钮领取奖励概率（0:无奖励关闭 1:有奖励 0~1：走概率是否有奖励）
        /// 1f
        /// </summary>
        public static float ComboRewardPercentbyClose = 1f;

        /// <summary>
        /// [id=18]
        /// 钻石显示整数还是小数，X值以后是小数
        /// 999999999
        /// </summary>
        public static int DiamondShowIntOrFloat = 999999999;

        /// <summary>
        /// [id=19]
        /// 超级连消达到X次后弹出大富翁奖励
        /// 10
        /// </summary>
        public static int BigRewardCount = 10;

        /// <summary>
        /// [id=20]
        /// 大富翁奖励运动速度(越小越快)
        /// 0.38f
        /// </summary>
        public static float BigRewardSpeed = 0.38f;

        /// <summary>
        /// [id=21]
        /// 大富翁奖励倍数
        /// [2,3,4,3,2]
        /// </summary>
        public static int[] BigRewards = new int[] { 2, 3, 4, 3, 2 };

        /// <summary>
        /// [id=22]
        /// 大富翁关闭按钮领取奖励系数
        /// 0.5f
        /// </summary>
        public static float BigRewardbyClose = 0.5f;

        /// <summary>
        /// [id=23]
        /// 大富翁关闭按钮领取奖励概率（0:无奖励关闭 1:有奖励 0~1：走概率是否有奖励）
        /// 1f
        /// </summary>
        public static float BigRewardPercentbyClose = 1f;

        /// <summary>
        /// [id=24]
        /// 所有奖励免费次数
        /// 2
        /// </summary>
        public static int FreeRewardCount = 2;

        /// <summary>
        /// 初始化字段
        /// </summary>
        public static void InitStaticField()
        {
            FacerobRewardConstVOModel voModel = FacerobRewardConstVOModel.Instance;
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.XdayBywaitTime))
                XdayBywaitTime = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.XdayBywaitTime).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.XdayByMaxValue))
                XdayByMaxValue = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.XdayByMaxValue).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.RewardTypeByTarget))
                RewardTypeByTarget = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.RewardTypeByTarget).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.RewardTargetIsNeedAD))
                RewardTargetIsNeedAD = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.RewardTargetIsNeedAD).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.RewardTargetbyClose))
                RewardTargetbyClose = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.RewardTargetbyClose).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.RewardTargetPercentbyClose))
                RewardTargetPercentbyClose = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.RewardTargetPercentbyClose).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.FlipRewardRandom))
                FlipRewardRandom = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.FlipRewardRandom).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.fragments_flipReward))
                fragments_flipReward = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.fragments_flipReward).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.RewardTypeByKuang))
                RewardTypeByKuang = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.RewardTypeByKuang).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.KuangByClickCount))
                KuangByClickCount = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.KuangByClickCount).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.KuangByFreeReward))
                KuangByFreeReward = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.KuangByFreeReward).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.KuangRewardByClose))
                KuangRewardByClose = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.KuangRewardByClose).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.KuangRewardPercentbyClose))
                KuangRewardPercentbyClose = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.KuangRewardPercentbyClose).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.RewardTypeByNormal))
                RewardTypeByNormal = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.RewardTypeByNormal).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.ComboRewardCount))
                ComboRewardCount = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.ComboRewardCount).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.ComboRewardbyClose))
                ComboRewardbyClose = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.ComboRewardbyClose).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.ComboRewardPercentbyClose))
                ComboRewardPercentbyClose = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.ComboRewardPercentbyClose).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.DiamondShowIntOrFloat))
                DiamondShowIntOrFloat = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.DiamondShowIntOrFloat).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.BigRewardCount))
                BigRewardCount = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.BigRewardCount).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.BigRewardSpeed))
                BigRewardSpeed = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.BigRewardSpeed).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.BigRewards))
                BigRewards = SerializeUtil.ToObject<int[]>(voModel.GetStaticValue(FacerobRewardConstVOStaticKey.BigRewards).ToString());
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.BigRewardbyClose))
                BigRewardbyClose = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.BigRewardbyClose).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.BigRewardPercentbyClose))
                BigRewardPercentbyClose = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.BigRewardPercentbyClose).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobRewardConstVOStaticKey.FreeRewardCount))
                FreeRewardCount = voModel.GetStaticValue(FacerobRewardConstVOStaticKey.FreeRewardCount).ToString().ToInt();
        }
    }
}