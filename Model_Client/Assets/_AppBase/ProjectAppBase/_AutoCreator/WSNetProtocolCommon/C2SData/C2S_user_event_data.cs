/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace ProjectApp.Protocol
{
    public class C2S_user_event_data
    {
        // 会话开启时间
        public int session_time;
        public List<UserEvent> events;
    }

    /// <summary>
    /// 玩家事件
    /// </summary>
    public class UserEvent
    {
        // 事件名
        public string e;
        // 发生次数
        public int times;
    }
}
