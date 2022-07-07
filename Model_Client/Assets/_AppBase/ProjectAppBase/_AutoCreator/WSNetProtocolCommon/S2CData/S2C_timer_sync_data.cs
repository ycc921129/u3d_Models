/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace ProjectApp.Protocol
{
    /// <summary>
    /// 同步定时器返回
    /// </summary>
    public class S2C_timer_sync_data
    {
        /// <summary>
        /// 最小超时时长
        /// </summary>
        public int min_timeout;
        /// <summary>
        /// 最大定时器数量
        /// </summary>
        public int max_count;
        /// <summary>
        /// 定时器列表 
        /// </summary>
        public List<TimerData> timers;
    }

    /// <summary>
    /// 定时器数据
    /// </summary>
    public class TimerData
    {
        /// <summary>
        /// 定时器id
        /// </summary>
        public string id;
        /// <summary>
        /// 超时时长
        /// </summary>
        public int timeout;
        /// <summary>
        /// 已过去的时长
        /// </summary>
        public int elapsed;
        /// <summary>
        /// 定时器数据
        /// </summary>
        public TimerCallBackData data;
    }

    /// <summary>
    /// 定时器数据
    /// </summary>
    public class TimerCallBackData
    {
        public string tag;
        public object data;
    }
}