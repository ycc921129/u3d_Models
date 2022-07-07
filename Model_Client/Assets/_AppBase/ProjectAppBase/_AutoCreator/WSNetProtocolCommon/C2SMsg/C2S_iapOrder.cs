using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_iapOrder : C2SJsonProto<C2S_iapOrder_data>
    {
        public C2S_iapOrder()
        {  
            type = WSNetMsg.C2S_iapOrder;
            bind_s2c_type = WSNetMsg.S2C_iapOrder;  
            route = RouteConst.C2S_iapOrder_route;
        }
    }
}