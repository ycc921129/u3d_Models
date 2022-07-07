/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_getGameList : C2SJsonProto<C2S_getGameList_data>
    {  
        public C2S_getGameList()  
        {
            type = WSNetMsg.C2S_getGameList;
            bind_s2c_type = WSNetMsg.S2C_getGameList;  
            route = RouteConst.C2S_pluginInvoke_route;      
        }  
    }
}