/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using Beebyte.Obfuscator;
using ProjectApp.Protocol;

namespace ProjectApp.Data
{
    /// <summary>
    /// 定时器数据
    /// </summary>
    public class NetTimerData
    {
        /// <summary>
        /// 定时器id
        /// </summary>
        public string id;
        /// <summary>
        /// 是否本地定时器
        /// </summary>
        public bool isLocal;
        /// <summary>
        /// 离线是否需要运行
        /// </summary>
        public bool outlineRun;
        /// <summary>
        /// 启动定时器时的时间戳
        /// </summary>
        public long startTimeStamp;
        /// <summary>
        /// 定时器超时时间
        /// </summary>
        public int timeout;
        /// <summary>
        /// 已过去的时间
        /// </summary>
        public int elapsed;
        /// <summary>
        /// 定时器数据
        /// </summary>
        public TimerCallBackData data;

        public override string ToString()
        {
            return string.Format("id:{0} islocal:{1} outlineRun:{2} startTimeStamp:{3} timeout:{4} elapsed:{5} data:{6}",
                id, isLocal, outlineRun, startTimeStamp, timeout, elapsed, data == null ? "null" : data.ToString());
        }

        /// <summary>
        /// 定时器数据
        /// </summary>
        [Skip]
        public class TimerCallBackData
        {
            public string tag;
            public object data;
        }
    }
}