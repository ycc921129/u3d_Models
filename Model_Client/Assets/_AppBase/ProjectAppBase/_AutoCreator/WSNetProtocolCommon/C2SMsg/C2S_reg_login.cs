/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_reg_login : C2SJsonProto<C2S_reg_login_data>  
    {
        public C2S_reg_login()
        {
            type = WSNetMsg.C2S_reg_login;
            bind_s2c_type = WSNetMsg.S2C_reg_login;
            route = RouteConst.C2S_reg_login_route;
        }
    }
}