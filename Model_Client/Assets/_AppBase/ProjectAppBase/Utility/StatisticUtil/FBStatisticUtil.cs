/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp
{
    public enum FBMoneyType
    {
        Coin,
        Credit,
    }

    /// <summary>
    /// https://developers.facebook.com/docs/app-events/getting-started-app-events-android/?translation#
    /// </summary>
    public static partial class FBStatisticUtil
    {
        /**
         * Include the Facebook namespace via the following code:
         * using Facebook.Unity;
         *
         * For more details, please take a look at:
         * developers.facebook.com/docs/unity/reference/current/FB.LogAppEvent
         */
        public static void AchieveLevelEvent(string level)
        {
            var parameters = DictionaryPool<string, object>.Get();
            parameters[FBStatisticConst.EVENT_PARAM_LEVEL] = level;
            ChannelMgr.Instance.SendFBEvent(FBStatisticConst.EVENT_NAME_ACHIEVED_LEVEL, parameters);
            DictionaryPool<string, object>.Release(parameters);
        }

        /**
         * Include the Facebook namespace via the following code:
         * using Facebook.Unity;
         *
         * For more details, please take a look at:
         * developers.facebook.com/docs/unity/reference/current/FB.LogAppEvent
         */
        public static void SpendCreditsEvent(FBMoneyType contentType, string contentId, double totalValue)
        {
            // contentType 货币类型
            // contentId 配置表来源

            var parameters = DictionaryPool<string, object>.Get();
            parameters[FBStatisticConst.EVENT_PARAM_CONTENT_TYPE] = contentType.ToString();
            parameters[FBStatisticConst.EVENT_PARAM_CONTENT_ID] = contentId;
            ChannelMgr.Instance.SendFBEvent(FBStatisticConst.EVENT_NAME_SPENT_CREDITS, (float)totalValue, parameters);
            DictionaryPool<string, object>.Release(parameters);
        }
    }
}