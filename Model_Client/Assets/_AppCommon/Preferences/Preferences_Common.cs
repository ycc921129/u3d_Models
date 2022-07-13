/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;

namespace ProjectApp.Data
{
    [Serializable]
    public partial class Preferences
    {
        /// <summary>
        /// 游戏启动次数
        /// </summary>
        public int gameStartCount = 0;
                  
        /// <summary>
        /// 离线时的时间戳（秒为单位，如1553242544）
        /// </summary>
        public long offline_timestamp = 0;

        /// <summary>
        /// 上次离线时的时间戳（秒为单位，如1553242544）
        /// </summary>
        public long lastOnline_timestamp = 0;
    }
}
