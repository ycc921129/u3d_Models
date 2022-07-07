/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using FutureCore;
using FutureCore.Data;

namespace ProjectApp.Data
{
    /// <summary>
    /// AppCommon-应用常量 VOStaticKey
    /// [应用通用表] [A] [_Excel/通用配置表/Y_应用通用表_A.xlsx]
    /// </summary>
    public static class AppCommonVOStaticKey
    {
        /// <summary>
        /// [id=1]
        /// 白名单分享文案
        /// </summary>
        public const string ShareContentWhiteList = "ShareContentWhiteList";

        /// <summary>
        /// [id=2]
        /// 黑名单分享文案
        /// </summary>
        public const string ShareContentBlackList = "ShareContentBlackList";

        /// <summary>
        /// [id=3]
        /// 隐私政策
        /// </summary>
        public const string PrivacyPolicyLink = "PrivacyPolicyLink";

        /// <summary>
        /// [id=4]
        /// 服务条款
        /// </summary>
        public const string ServiceAgreement = "ServiceAgreement";

        /// <summary>
        /// [id=5]
        /// 金币自定义区间
        /// </summary>
        public const string coinStaticRange = "coinStaticRange";

        /// <summary>
        /// [id=6]
        /// 累计获得总金币到达该值后产出减少
        /// </summary>
        public const string CoinsLimitValue = "CoinsLimitValue";
    }
}