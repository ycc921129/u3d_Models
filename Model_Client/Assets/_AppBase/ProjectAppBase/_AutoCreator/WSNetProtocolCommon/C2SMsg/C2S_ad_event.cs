/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_ad_event : C2SJsonProto<C2S_ad_event_data>
    {
        public C2S_ad_event()
        {
            type = WSNetMsg.C2S_ad_event;
        }
    }
}