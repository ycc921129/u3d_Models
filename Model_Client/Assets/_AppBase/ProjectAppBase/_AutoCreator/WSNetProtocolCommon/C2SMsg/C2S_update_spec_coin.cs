using System.Collections;
using System.Collections.Generic;
using FutureCore;

namespace ProjectApp.Protocol
{
    // 金币
    public class C2S_update_spec_coin : C2SJsonProto<C2S_update_spec_coin_data>
    {
        public C2S_update_spec_coin()
        {
            type = WSNetMsg.C2S_update_spec_coin;
            bind_s2c_type = WSNetMsg.S2C_update_spec_coin;
            data = new C2S_update_spec_coin_data();
            route = RouteConst.C2S_update_coin_route;
        }
    }

    public class C2S_update_spec_coin_data
    {
        //货币类型，比如：coin
        public string type;
        //变更值
        public object value;
        // 配置位置信息 校验参数 
        public ArrayList checkpos;
        //原因
        public string reason;        
    }
}