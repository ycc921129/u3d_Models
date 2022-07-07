/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_getMainInfo : C2SJsonProto<C2S_getMainInfo_data>
    {
        public C2S_getMainInfo()
        {
            type = WSNetMsg.C2S_getMainInfo;
            bind_s2c_type = WSNetMsg.S2C_getMainInfo;
            route = RouteConst.C2S_logicInvoke_route;      
        }
    }
}