/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using FutureCore;

namespace ProjectApp.Protocol
{
    // 数据存储
    public class C2S_preferences : C2SJsonProto<C2S_preferences_data>
    {
        public C2S_preferences()
        {
            type = WSNetMsg.C2S_preferences;
            bind_s2c_type = WSNetMsg.S2C_preferences;
            route = RouteConst.C2S_preferences_route;    
        }
    }

    public class C2S_preferences_data  
    {
        public Dictionary<string, object> inc;
        public Dictionary<string, object> set;

        public C2S_preferences_data()
        {
            inc = new Dictionary<string, object>();
            set = new Dictionary<string, object>();
        }
    }
}