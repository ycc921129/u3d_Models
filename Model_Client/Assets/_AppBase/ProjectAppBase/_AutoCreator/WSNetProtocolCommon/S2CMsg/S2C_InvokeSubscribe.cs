/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_InvokeSubscribe : S2CJsonProto
    {
        public object data;

        public S2C_InvokeSubscribe()
        {
            type = WSNetMsg.S2C_InvokeSubscribe;
            route = RouteConst.Route_subscribe; 
        }
    }
}  