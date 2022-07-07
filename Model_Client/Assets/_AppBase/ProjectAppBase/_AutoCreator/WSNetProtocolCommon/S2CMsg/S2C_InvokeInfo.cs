/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using Newtonsoft.Json.Linq;

namespace ProjectApp.Protocol
{
    public class S2C_InvokeInfo : S2CJsonProto
    {  
        public object data;

        public S2C_InvokeInfo()
        { 
            type = WSNetMsg.S2C_InvokeInfo;
            route = RouteConst.Route_info;
        }
    }
}