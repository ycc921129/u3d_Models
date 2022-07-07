/****************************************************************************
* ScriptType: 框架 - 插屏业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp.Data
{
    public partial class Preferences
    {
        /// <summary>
        /// 插屏激活天数，第三天后才激活
        /// </summary>
        public int interstitial_ActiveDay;

        /// <summary>
        /// 今天展示插屏次数
        /// </summary>
        public int interstitial_TodayShowCount;

        /// <summary>
        /// 今天点击插屏次数
        /// </summary>
        public int interstitial_TodayClickCount;

        /// <summary>
        /// 插屏冷却时间戳
        /// </summary>
        public int interstitial_TimeStamp;

        /// <summary>
        /// 有效用户视频次数
        /// </summary>
        public int videoEffective_count;
        /// <summary>
        /// 是否已经打点了有效用户
        /// </summary>
        public bool isLogEffective = false;
    }
}