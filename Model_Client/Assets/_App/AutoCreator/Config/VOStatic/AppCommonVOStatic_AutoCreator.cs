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
    /// AppCommon-应用常量 VOStatic
    /// [应用通用表] [A] [_Excel/通用配置表/Y_应用通用表_A.xlsx]
    /// </summary>
    public static class AppCommonVOStatic
    {
        /// <summary>
        /// [id=1]
        /// 白名单分享文案
        /// "The Best Mobile Game. Free Cash Rewards, just on {0}! Then Download and Input My Referral Code: {1} to Win Cash!"
        /// </summary>
        public static string ShareContentWhiteList = "The Best Mobile Game. Free Cash Rewards, just on {0}! Then Download and Input My Referral Code: {1} to Win Cash!";

        /// <summary>
        /// [id=2]
        /// 黑名单分享文案
        /// "The Best Mobile Game. Free Cash Rewards, just on {0}! Then Download and Input My Referral Code: {1} to Win Cash!"
        /// </summary>
        public static string ShareContentBlackList = "The Best Mobile Game. Free Cash Rewards, just on {0}! Then Download and Input My Referral Code: {1} to Win Cash!";

        /// <summary>
        /// [id=3]
        /// 隐私政策
        /// "https://docs.google.com/document/d/1UUMhDtrkXqVRfKRKj-16qH0mUPIhJ7c_xAjFGByCmYU/edit?usp=sharing"
        /// </summary>
        public static string PrivacyPolicyLink = "https://docs.google.com/document/d/1UUMhDtrkXqVRfKRKj-16qH0mUPIhJ7c_xAjFGByCmYU/edit?usp=sharing";

        /// <summary>
        /// [id=4]
        /// 服务条款
        /// "https://docs.google.com/document/d/1M1SyHNDASMeMIr92eg7tHwj42IfrOnF9uo11PGzrraM/edit?usp=sharing"
        /// </summary>
        public static string ServiceAgreement = "https://docs.google.com/document/d/1M1SyHNDASMeMIr92eg7tHwj42IfrOnF9uo11PGzrraM/edit?usp=sharing";

        /// <summary>
        /// [id=5]
        /// 金币自定义区间
        /// [-1000000,1000000]
        /// </summary>
        public static int[] coinStaticRange = new int[] { -1000000, 1000000 };

        /// <summary>
        /// [id=6]
        /// 累计获得总金币到达该值后产出减少
        /// 100
        /// </summary>
        public static int CoinsLimitValue = 100;

        /// <summary>
        /// 初始化字段
        /// </summary>
        public static void InitStaticField()
        {
            AppCommonVOModel voModel = AppCommonVOModel.Instance;
            if (voModel.HasStaticKey(AppCommonVOStaticKey.ShareContentWhiteList))
                ShareContentWhiteList = voModel.GetStaticValue(AppCommonVOStaticKey.ShareContentWhiteList).ToString();
            if (voModel.HasStaticKey(AppCommonVOStaticKey.ShareContentBlackList))
                ShareContentBlackList = voModel.GetStaticValue(AppCommonVOStaticKey.ShareContentBlackList).ToString();
            if (voModel.HasStaticKey(AppCommonVOStaticKey.PrivacyPolicyLink))
                PrivacyPolicyLink = voModel.GetStaticValue(AppCommonVOStaticKey.PrivacyPolicyLink).ToString();
            if (voModel.HasStaticKey(AppCommonVOStaticKey.ServiceAgreement))
                ServiceAgreement = voModel.GetStaticValue(AppCommonVOStaticKey.ServiceAgreement).ToString();
            if (voModel.HasStaticKey(AppCommonVOStaticKey.coinStaticRange))
                coinStaticRange = SerializeUtil.ToObject<int[]>(voModel.GetStaticValue(AppCommonVOStaticKey.coinStaticRange).ToString());
            if (voModel.HasStaticKey(AppCommonVOStaticKey.CoinsLimitValue))
                CoinsLimitValue = voModel.GetStaticValue(AppCommonVOStaticKey.CoinsLimitValue).ToString().ToInt();
        }
    }
}