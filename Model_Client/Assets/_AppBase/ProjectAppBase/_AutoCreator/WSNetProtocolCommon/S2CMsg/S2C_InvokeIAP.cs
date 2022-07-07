﻿/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_InvokeIAP : S2CJsonProto
    {  
        public object data;

        public S2C_InvokeIAP()
        { 
            type = WSNetMsg.S2C_InvokeIAP;
            route = RouteConst.Route_iap;    
        }
    }
}