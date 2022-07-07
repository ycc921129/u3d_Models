/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp.Protocol
{
    /// <summary>
    /// 定时器过期返回
    /// </summary>
    public class S2C_timer_out_data
    {
        /// <summary>
        /// 定时器id
        /// </summary>
        public string id;
        /// <summary>
        /// 超时时间
        /// </summary>
        public int timeout;
        /// <summary>
        /// 定时器数据
        /// </summary>
        public TimerCallBackData data;
    }
}