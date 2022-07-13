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
            offline_timestamp = preferences.offline_timestamp;
            lastOnline_timestamp = preferences.lastOnline_timestamp;
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
        #endregion

        #region RefType

        #endregion
    }
}