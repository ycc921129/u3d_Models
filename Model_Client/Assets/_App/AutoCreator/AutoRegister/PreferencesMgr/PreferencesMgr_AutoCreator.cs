/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections.Generic;
using FutureCore;
using FutureCore.Data;
using ProjectApp.Data;
using ProjectApp.Protocol;

namespace ProjectApp
{
    public partial class PreferencesMgr
    {
        private void OnInitPreferences()
        {
            gameStartCount = preferences.gameStartCount;
            loginGameTodayTimes = preferences.loginGameTodayTimes;
            data_ver = preferences.data_ver;
            haveBeenGameStart = preferences.haveBeenGameStart;
            date = preferences.date;
            lastLoginDays = preferences.lastLoginDays;
            offline_timestamp = preferences.offline_timestamp;
            lastOnline_timestamp = preferences.lastOnline_timestamp;
            interstitial_ActiveDay = preferences.interstitial_ActiveDay;
            interstitial_TodayShowCount = preferences.interstitial_TodayShowCount;
            interstitial_TodayClickCount = preferences.interstitial_TodayClickCount;
            interstitial_TimeStamp = preferences.interstitial_TimeStamp;
        }

        #region ValueType

        private int gameStartCount;
        /// <summary>
        /// 游戏启动次数
        /// </summary>
        public int GameStartCount
        {
            get { return gameStartCount; }
            set
            {
                if (gameStartCount == value) return;
                ChangeValue<int> changeValue = PreferencesDispatcher<int>.Instance.changeValue;
                changeValue.oldValue = gameStartCount;
                changeValue.newValue = value;

                gameStartCount = value;
                preferences.gameStartCount = gameStartCount;
                AddToAutoDelaySaveList(PreferencesField.gameStartCount, preferences.gameStartCount);
                PreferencesDispatcher<int>.Instance.Dispatch(PreferencesMsg.gameStartCount, changeValue);
                dataDispatcher.Dispatch(PreferencesMsg.gameStartCount);
            }
        }

        private int loginGameTodayTimes;
        /// <summary>
        /// 每天登陆的次数
        /// </summary>
        public int LoginGameTodayTimes
        {
            get { return loginGameTodayTimes; }
            set
            {
                if (loginGameTodayTimes == value) return;
                ChangeValue<int> changeValue = PreferencesDispatcher<int>.Instance.changeValue;
                changeValue.oldValue = loginGameTodayTimes;
                changeValue.newValue = value;

                loginGameTodayTimes = value;
                preferences.loginGameTodayTimes = loginGameTodayTimes;
                AddToAutoDelaySaveList(PreferencesField.loginGameTodayTimes, preferences.loginGameTodayTimes);
                PreferencesDispatcher<int>.Instance.Dispatch(PreferencesMsg.loginGameTodayTimes, changeValue);
                dataDispatcher.Dispatch(PreferencesMsg.loginGameTodayTimes);
            }
        }

        private long data_ver;
        /// <summary>
        /// 数据版本
        /// </summary>
        public long Data_ver
        {
            get { return data_ver; }
            set
            {
                if (data_ver == value) return;
                ChangeValue<long> changeValue = PreferencesDispatcher<long>.Instance.changeValue;
                changeValue.oldValue = data_ver;
                changeValue.newValue = value;

                data_ver = value;
                preferences.data_ver = data_ver;
                Save(PreferencesField.data_ver, preferences.data_ver);
                PreferencesDispatcher<long>.Instance.Dispatch(PreferencesMsg.data_ver, changeValue);
                dataDispatcher.Dispatch(PreferencesMsg.data_ver);
            }
        }

        private bool haveBeenGameStart;
        /// <summary>
        /// 是否历史中开始过游戏
        /// </summary>
        public bool HaveBeenGameStart
        {
            get { return haveBeenGameStart; }
            set
            {
                if (haveBeenGameStart == value) return;
                ChangeValue<bool> changeValue = PreferencesDispatcher<bool>.Instance.changeValue;
                changeValue.oldValue = haveBeenGameStart;
                changeValue.newValue = value;

                haveBeenGameStart = value;
                preferences.haveBeenGameStart = haveBeenGameStart;
                AddToAutoDelaySaveList(PreferencesField.haveBeenGameStart, preferences.haveBeenGameStart);
                PreferencesDispatcher<bool>.Instance.Dispatch(PreferencesMsg.haveBeenGameStart, changeValue);
                dataDispatcher.Dispatch(PreferencesMsg.haveBeenGameStart);
            }
        }

        private string date;
        /// <summary>
        /// 最近登录日期，比如 20190322
        /// 新的一天需要清理daily和claim_dailytasks字段
        /// 用来在心跳的时候检查是否跨天
        /// </summary>
        public string Date
        {
            get { return date; }
            set
            {
                if (date == value) return;
                ChangeValue<string> changeValue = PreferencesDispatcher<string>.Instance.changeValue;
                changeValue.oldValue = date;
                changeValue.newValue = value;

                date = value;
                preferences.date = date;
                AddToAutoDelaySaveList(PreferencesField.date, preferences.date);
                PreferencesDispatcher<string>.Instance.Dispatch(PreferencesMsg.date, changeValue);
                dataDispatcher.Dispatch(PreferencesMsg.date);
            }
        }

        private int lastLoginDays;
        /// <summary>
        /// 用来检测服务器时区的跨天
        /// </summary>
        public int LastLoginDays
        {
            get { return lastLoginDays; }
            set
            {
                if (lastLoginDays == value) return;
                ChangeValue<int> changeValue = PreferencesDispatcher<int>.Instance.changeValue;
                changeValue.oldValue = lastLoginDays;
                changeValue.newValue = value;

                lastLoginDays = value;
                preferences.lastLoginDays = lastLoginDays;
                AddToAutoDelaySaveList(PreferencesField.lastLoginDays, preferences.lastLoginDays);
                PreferencesDispatcher<int>.Instance.Dispatch(PreferencesMsg.lastLoginDays, changeValue);
                dataDispatcher.Dispatch(PreferencesMsg.lastLoginDays);
            }
        }

        private long offline_timestamp;
        /// <summary>
        /// 离线时的时间戳（秒为单位，如1553242544）
        /// </summary>
        public long Offline_timestamp
        {
            get { return offline_timestamp; }
            set
            {
                if (offline_timestamp == value) return;
                ChangeValue<long> changeValue = PreferencesDispatcher<long>.Instance.changeValue;
                changeValue.oldValue = offline_timestamp;
                changeValue.newValue = value;

                offline_timestamp = value;
                preferences.offline_timestamp = offline_timestamp;
                AddToAutoDelaySaveList(PreferencesField.offline_timestamp, preferences.offline_timestamp);
                PreferencesDispatcher<long>.Instance.Dispatch(PreferencesMsg.offline_timestamp, changeValue);
                dataDispatcher.Dispatch(PreferencesMsg.offline_timestamp);
            }
        }

        private long lastOnline_timestamp;
        /// <summary>
        /// 上次离线时的时间戳（秒为单位，如1553242544）
        /// </summary>
        public long LastOnline_timestamp
        {
            get { return lastOnline_timestamp; }
            set
            {
                if (lastOnline_timestamp == value) return;
                ChangeValue<long> changeValue = PreferencesDispatcher<long>.Instance.changeValue;
                changeValue.oldValue = lastOnline_timestamp;
                changeValue.newValue = value;

                lastOnline_timestamp = value;
                preferences.lastOnline_timestamp = lastOnline_timestamp;
                AddToAutoDelaySaveList(PreferencesField.lastOnline_timestamp, preferences.lastOnline_timestamp);
                PreferencesDispatcher<long>.Instance.Dispatch(PreferencesMsg.lastOnline_timestamp, changeValue);
                dataDispatcher.Dispatch(PreferencesMsg.lastOnline_timestamp);
            }
        }

        private int interstitial_ActiveDay;
        /// <summary>
        /// 插屏激活天数，第三天后才激活
        /// </summary>
        public int Interstitial_ActiveDay
        {
            get { return interstitial_ActiveDay; }
            set
            {
                if (interstitial_ActiveDay == value) return;
                ChangeValue<int> changeValue = PreferencesDispatcher<int>.Instance.changeValue;
                changeValue.oldValue = interstitial_ActiveDay;
                changeValue.newValue = value;

                interstitial_ActiveDay = value;
                preferences.interstitial_ActiveDay = interstitial_ActiveDay;
                AddToAutoDelaySaveList(PreferencesField.interstitial_ActiveDay, preferences.interstitial_ActiveDay);
                PreferencesDispatcher<int>.Instance.Dispatch(PreferencesMsg.interstitial_ActiveDay, changeValue);
                dataDispatcher.Dispatch(PreferencesMsg.interstitial_ActiveDay);
            }
        }

        private int interstitial_TodayShowCount;
        /// <summary>
        /// 今天展示插屏次数
        /// </summary>
        public int Interstitial_TodayShowCount
        {
            get { return interstitial_TodayShowCount; }
            set
            {
                if (interstitial_TodayShowCount == value) return;
                ChangeValue<int> changeValue = PreferencesDispatcher<int>.Instance.changeValue;
                changeValue.oldValue = interstitial_TodayShowCount;
                changeValue.newValue = value;

                interstitial_TodayShowCount = value;
                preferences.interstitial_TodayShowCount = interstitial_TodayShowCount;
                AddToAutoDelaySaveList(PreferencesField.interstitial_TodayShowCount, preferences.interstitial_TodayShowCount);
                PreferencesDispatcher<int>.Instance.Dispatch(PreferencesMsg.interstitial_TodayShowCount, changeValue);
                dataDispatcher.Dispatch(PreferencesMsg.interstitial_TodayShowCount);
            }
        }

        private int interstitial_TodayClickCount;
        /// <summary>
        /// 今天点击插屏次数
        /// </summary>
        public int Interstitial_TodayClickCount
        {
            get { return interstitial_TodayClickCount; }
            set
            {
                if (interstitial_TodayClickCount == value) return;
                ChangeValue<int> changeValue = PreferencesDispatcher<int>.Instance.changeValue;
                changeValue.oldValue = interstitial_TodayClickCount;
                changeValue.newValue = value;

                interstitial_TodayClickCount = value;
                preferences.interstitial_TodayClickCount = interstitial_TodayClickCount;
                AddToAutoDelaySaveList(PreferencesField.interstitial_TodayClickCount, preferences.interstitial_TodayClickCount);
                PreferencesDispatcher<int>.Instance.Dispatch(PreferencesMsg.interstitial_TodayClickCount, changeValue);
                dataDispatcher.Dispatch(PreferencesMsg.interstitial_TodayClickCount);
            }
        }

        private int interstitial_TimeStamp;
        /// <summary>
        /// 插屏冷却时间戳
        /// </summary>
        public int Interstitial_TimeStamp
        {
            get { return interstitial_TimeStamp; }
            set
            {
                if (interstitial_TimeStamp == value) return;
                ChangeValue<int> changeValue = PreferencesDispatcher<int>.Instance.changeValue;
                changeValue.oldValue = interstitial_TimeStamp;
                changeValue.newValue = value;

                interstitial_TimeStamp = value;
                preferences.interstitial_TimeStamp = interstitial_TimeStamp;
                AddToAutoDelaySaveList(PreferencesField.interstitial_TimeStamp, preferences.interstitial_TimeStamp);
                PreferencesDispatcher<int>.Instance.Dispatch(PreferencesMsg.interstitial_TimeStamp, changeValue);
                dataDispatcher.Dispatch(PreferencesMsg.interstitial_TimeStamp);
            }
        }
        #endregion

        #region RefType

        /// <summary>
        /// 模块次数控制字典
        /// </summary>
        public Dictionary<string,int> ModuleControlDict
        {
            get
            {
                if (preferences.moduleControlDict == null) preferences.moduleControlDict = new Dictionary<string,int>();
                return preferences.moduleControlDict;
            }
        }
        public void SaveModuleControlDict()
        {
            AddToAutoDelaySaveList(PreferencesField.moduleControlDict, ModuleControlDict);
            PreferencesDispatcher<object>.Instance.Dispatch(PreferencesMsg.moduleControlDict);
            dataDispatcher.Dispatch(PreferencesMsg.moduleControlDict);
        }

        /// <summary>
        /// 用户所有的定时器
        /// </summary>
        public Dictionary<string,NetTimerData> NetTimers
        {
            get
            {
                if (preferences.netTimers == null) preferences.netTimers = new Dictionary<string,NetTimerData>();
                return preferences.netTimers;
            }
        }
        public void SaveNetTimers()
        {
            AddToAutoDelaySaveList(PreferencesField.netTimers, NetTimers);
            PreferencesDispatcher<object>.Instance.Dispatch(PreferencesMsg.netTimers);
            dataDispatcher.Dispatch(PreferencesMsg.netTimers);
        }
        #endregion
    }
}