/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_getTask : C2SJsonProto<C2S_getTask_data>
    {
        public C2S_getTask()
        {
            type = WSNetMsg.C2S_getTask;
            bind_s2c_type = WSNetMsg.S2C_getTask;
            route = RouteConst.C2S_logicInvoke_route;    
        }
    }
}