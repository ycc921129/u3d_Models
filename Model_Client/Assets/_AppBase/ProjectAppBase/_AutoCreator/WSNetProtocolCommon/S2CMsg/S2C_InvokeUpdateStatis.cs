/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_InvokeUpdateStatis : S2CJsonProto
    {  
        public object data;

        public S2C_InvokeUpdateStatis()
        { 
            type = WSNetMsg.S2C_InvokeUpdateStatis;
            route = RouteConst.Route_updateStatis;  
        }
    }
}