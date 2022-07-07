/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_updateTask : C2SJsonProto<C2S_updateTask_data>
    {
        public C2S_updateTask()
        {
            type = WSNetMsg.C2S_updateTask;
            bind_s2c_type = WSNetMsg.S2C_updateTask;
            route = RouteConst.C2S_logicInvoke_route;  
        }
    }
}