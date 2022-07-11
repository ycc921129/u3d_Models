/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using FutureCore;
using FutureCore.Data;

namespace ProjectApp.Data
{
    /// <summary>
    /// FacerobConst VOStaticKey
    /// [游戏常量表] [A] [_Excel/游戏配置表/Y_游戏常量表_A.xlsx]
    /// </summary>
    public static class FacerobConstVOStaticKey
    {
        /// <summary>
        /// [id=1]
        /// 使用哪一套规则（0 = cd规则, 1 = 事件规则）
        /// </summary>
        public const string InterstitialMethod = "InterstitialMethod";

        /// <summary>
        /// [id=2]
        /// 插屏间隔cd
        /// </summary>
        public const string InterstitialByCD = "InterstitialByCD";

        /// <summary>
        /// [id=3]
        /// 累计达到X次关闭必播放插屏
        /// </summary>
        public const string InterstitialByCount = "InterstitialByCount";

        /// <summary>
        /// [id=4]
        /// 插屏概率（1 = 100%不出, 0 = 必出插屏）
        /// </summary>
        public const string InterstitialByPercent = "InterstitialByPercent";

        /// <summary>
        /// [id=5]
        /// 达到配置表最大的等级后的升级分数
        /// </summary>
        public const string NeedScoreByMaxLevel = "NeedScoreByMaxLevel";

        /// <summary>
        /// [id=6]
        /// 合成速度基数
        /// </summary>
        public const string blockSpeedFactor = "blockSpeedFactor";

        /// <summary>
        /// [id=7]
        /// 连消动画Good
        /// </summary>
        public const string comboAnimGood = "comboAnimGood";

        /// <summary>
        /// [id=8]
        /// 连消动画Great
        /// </summary>
        public const string comboAnimGreat = "comboAnimGreat";

        /// <summary>
        /// [id=9]
        /// 连消动画Excellent
        /// </summary>
        public const string comboAnimExcellent = "comboAnimExcellent";

        /// <summary>
        /// [id=10]
        /// 连消动画Amazing
        /// </summary>
        public const string comboAnimAmazing = "comboAnimAmazing";

        /// <summary>
        /// [id=11]
        /// 拉取应用的报名
        /// </summary>
        public const string linkGameName = "linkGameName";

        /// <summary>
        /// [id=12]
        /// 碎片解锁等级
        /// </summary>
        public const string fragmentsUnlockByLevel = "fragmentsUnlockByLevel";
    }
}