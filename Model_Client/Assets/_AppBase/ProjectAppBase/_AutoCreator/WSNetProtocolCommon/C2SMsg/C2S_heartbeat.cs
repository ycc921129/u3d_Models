/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_heartbeat :C2SJsonProto<C2S_heartbeat_data>
    {
        public C2S_heartbeat()
        {
            type = WSNetMsg.C2S_heartbeat;
            bind_s2c_type = WSNetMsg.S2C_heartbeat;
        }
    }
}
