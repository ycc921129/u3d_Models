/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;

namespace ProjectApp.Data
{
    /// <summary>
    /// 远程存储
    /// </summary>
    [Serializable]
    public partial class Preferences
    {
        /// <summary>
        /// 数据版本
        /// </summary>
        public long data_ver = 0;        

        /// <summary>
        /// 是否历史中开始过游戏
        /// </summary>
        public bool haveBeenGameStart = false;

        /// <summary>
        /// 最近登录日期，比如 20190322
        /// 新的一天需要清理daily和claim_dailytasks字段
        /// 用来在心跳的时候检查是否跨天
        /// </summary>
        public string date = string.Empty;

        /// <summary>
        /// 用来检测服务器时区的跨天
        /// </summary>
        public int lastLoginDays = 0;

        /// <summary>
        /// 离线时的时间戳（秒为单位，如1553242544）
        /// </summary>
        public long offline_timestamp = 0;

        /// <summary>
        /// 上次离线时的时间戳（秒为单位，如1553242544）
        /// </summary>
        public long lastOnline_timestamp = 0;  

        /// <summary>
        /// 模块次数控制字典
        /// </summary>
        public Dictionary<string, int> moduleControlDict = new Dictionary<string, int>();     

        /// <summary>
        /// 用户所有的定时器
        /// </summary>
        public Dictionary<string, NetTimerData> netTimers = new Dictionary<string, NetTimerData>();
    }
}