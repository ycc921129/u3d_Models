/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_invite : C2SJsonProto<C2S_invite_data>
    {
        public C2S_invite()
        {
            type = WSNetMsg.C2S_invite;
            bind_s2c_type = WSNetMsg.S2C_invite;
            route = RouteConst.C2S_Invite_route;
        }
    }
}