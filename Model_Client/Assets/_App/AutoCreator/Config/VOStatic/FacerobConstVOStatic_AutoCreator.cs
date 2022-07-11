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
    /// FacerobConst VOStatic
    /// [游戏常量表] [A] [_Excel/游戏配置表/Y_游戏常量表_A.xlsx]
    /// </summary>
    public static class FacerobConstVOStatic
    {
        /// <summary>
        /// [id=1]
        /// 使用哪一套规则（0 = cd规则, 1 = 事件规则）
        /// 1
        /// </summary>
        public static int InterstitialMethod = 1;

        /// <summary>
        /// [id=2]
        /// 插屏间隔cd
        /// 45f
        /// </summary>
        public static float InterstitialByCD = 45f;

        /// <summary>
        /// [id=3]
        /// 累计达到X次关闭必播放插屏
        /// 3
        /// </summary>
        public static int InterstitialByCount = 3;

        /// <summary>
        /// [id=4]
        /// 插屏概率（1 = 100%不出, 0 = 必出插屏）
        /// 0f
        /// </summary>
        public static float InterstitialByPercent = 0f;

        /// <summary>
        /// [id=5]
        /// 达到配置表最大的等级后的升级分数
        /// 5000
        /// </summary>
        public static int NeedScoreByMaxLevel = 5000;

        /// <summary>
        /// [id=6]
        /// 合成速度基数
        /// 0.1f
        /// </summary>
        public static float blockSpeedFactor = 0.1f;

        /// <summary>
        /// [id=7]
        /// 连消动画Good
        /// 5
        /// </summary>
        public static int comboAnimGood = 5;

        /// <summary>
        /// [id=8]
        /// 连消动画Great
        /// 10
        /// </summary>
        public static int comboAnimGreat = 10;

        /// <summary>
        /// [id=9]
        /// 连消动画Excellent
        /// 15
        /// </summary>
        public static int comboAnimExcellent = 15;

        /// <summary>
        /// [id=10]
        /// 连消动画Amazing
        /// 20
        /// </summary>
        public static int comboAnimAmazing = 20;

        /// <summary>
        /// [id=11]
        /// 拉取应用的报名
        /// "com.oyeah.feelingstar"
        /// </summary>
        public static string linkGameName = "com.oyeah.feelingstar";

        /// <summary>
        /// [id=12]
        /// 碎片解锁等级
        /// 5
        /// </summary>
        public static int fragmentsUnlockByLevel = 5;

        /// <summary>
        /// 初始化字段
        /// </summary>
        public static void InitStaticField()
        {
            FacerobConstVOModel voModel = FacerobConstVOModel.Instance;
            if (voModel.HasStaticKey(FacerobConstVOStaticKey.InterstitialMethod))
                InterstitialMethod = voModel.GetStaticValue(FacerobConstVOStaticKey.InterstitialMethod).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobConstVOStaticKey.InterstitialByCD))
                InterstitialByCD = voModel.GetStaticValue(FacerobConstVOStaticKey.InterstitialByCD).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobConstVOStaticKey.InterstitialByCount))
                InterstitialByCount = voModel.GetStaticValue(FacerobConstVOStaticKey.InterstitialByCount).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobConstVOStaticKey.InterstitialByPercent))
                InterstitialByPercent = voModel.GetStaticValue(FacerobConstVOStaticKey.InterstitialByPercent).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobConstVOStaticKey.NeedScoreByMaxLevel))
                NeedScoreByMaxLevel = voModel.GetStaticValue(FacerobConstVOStaticKey.NeedScoreByMaxLevel).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobConstVOStaticKey.blockSpeedFactor))
                blockSpeedFactor = voModel.GetStaticValue(FacerobConstVOStaticKey.blockSpeedFactor).ToString().ToFloat();
            if (voModel.HasStaticKey(FacerobConstVOStaticKey.comboAnimGood))
                comboAnimGood = voModel.GetStaticValue(FacerobConstVOStaticKey.comboAnimGood).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobConstVOStaticKey.comboAnimGreat))
                comboAnimGreat = voModel.GetStaticValue(FacerobConstVOStaticKey.comboAnimGreat).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobConstVOStaticKey.comboAnimExcellent))
                comboAnimExcellent = voModel.GetStaticValue(FacerobConstVOStaticKey.comboAnimExcellent).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobConstVOStaticKey.comboAnimAmazing))
                comboAnimAmazing = voModel.GetStaticValue(FacerobConstVOStaticKey.comboAnimAmazing).ToString().ToInt();
            if (voModel.HasStaticKey(FacerobConstVOStaticKey.linkGameName))
                linkGameName = voModel.GetStaticValue(FacerobConstVOStaticKey.linkGameName).ToString();
            if (voModel.HasStaticKey(FacerobConstVOStaticKey.fragmentsUnlockByLevel))
                fragmentsUnlockByLevel = voModel.GetStaticValue(FacerobConstVOStaticKey.fragmentsUnlockByLevel).ToString().ToInt();
        }
    }
}