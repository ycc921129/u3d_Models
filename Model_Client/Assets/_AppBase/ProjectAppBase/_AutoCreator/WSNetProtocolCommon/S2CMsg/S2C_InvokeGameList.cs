/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_InvokeGameList : S2CJsonProto
    {  
        public object data;

        public S2C_InvokeGameList()
        { 
            type = WSNetMsg.S2C_InvokeGameList;
            route = RouteConst.Route_gamelist;
        }
    }
}