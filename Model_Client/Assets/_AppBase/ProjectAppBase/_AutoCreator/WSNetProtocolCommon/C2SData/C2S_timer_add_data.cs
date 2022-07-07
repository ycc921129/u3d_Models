/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp.Protocol
{
    //创建定时器
    public class C2S_timer_add_data
    {
        /// <summary>
        /// 定时器ID
        /// </summary>
        public string id;
        /// <summary>
        /// 超时时长(秒),必须60秒以上
        /// </summary>
        public int timeout;
        /// <summary>
        /// 定时器数据
        /// </summary>
        public TimerCallBackData data;
    }
}