/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using System.Collections.Generic;

namespace ProjectApp
{
    public static class PreferencesMsg
    {
        /// <summary>
        /// 游戏启动次数
        /// </summary>
        public const string gameStartCount = "Preferences_gameStartCount";
        /// <summary>
        /// 每天登陆的次数
        /// </summary>
        public const string loginGameTodayTimes = "Preferences_loginGameTodayTimes";
        /// <summary>
        /// 数据版本
        /// </summary>
        public const string data_ver = "Preferences_data_ver";
        /// <summary>
        /// 是否历史中开始过游戏
        /// </summary>
        public const string haveBeenGameStart = "Preferences_haveBeenGameStart";
        /// <summary>
        /// 最近登录日期，比如 20190322
        /// 新的一天需要清理daily和claim_dailytasks字段
        /// 用来在心跳的时候检查是否跨天
        /// </summary>
        public const string date = "Preferences_date";
        /// <summary>
        /// 用来检测服务器时区的跨天
        /// </summary>
        public const string lastLoginDays = "Preferences_lastLoginDays";
        /// <summary>
        /// 离线时的时间戳（秒为单位，如1553242544）
        /// </summary>
        public const string offline_timestamp = "Preferences_offline_timestamp";
        /// <summary>
        /// 上次离线时的时间戳（秒为单位，如1553242544）
        /// </summary>
        public const string lastOnline_timestamp = "Preferences_lastOnline_timestamp";
        /// <summary>
        /// 模块次数控制字典
        /// </summary>
        public const string moduleControlDict = "Preferences_moduleControlDict";
        /// <summary>
        /// 用户所有的定时器
        /// </summary>
        public const string netTimers = "Preferences_netTimers";
        /// <summary>
        /// 插屏激活天数，第三天后才激活
        /// </summary>
        public const string interstitial_ActiveDay = "Preferences_interstitial_ActiveDay";
        /// <summary>
        /// 今天展示插屏次数
        /// </summary>
        public const string interstitial_TodayShowCount = "Preferences_interstitial_TodayShowCount";
        /// <summary>
        /// 今天点击插屏次数
        /// </summary>
        public const string interstitial_TodayClickCount = "Preferences_interstitial_TodayClickCount";
        /// <summary>
        /// 插屏冷却时间戳
        /// </summary>
        public const string interstitial_TimeStamp = "Preferences_interstitial_TimeStamp";
    }
}