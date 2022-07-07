/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace ProjectApp.Protocol
{
    public class C2S_subscribeNotifyAck_data
    {
        /// <summary>
        /// 用于客户端自行发货确认，确认请求后，后台不再通知该笔订单数据，否则会多次通知 （订阅不需要发货）
        /// NOTIFY_UPDATE_USER_SUBSCRIBE中的id
        /// </summary>
        public int id;
    }
}
