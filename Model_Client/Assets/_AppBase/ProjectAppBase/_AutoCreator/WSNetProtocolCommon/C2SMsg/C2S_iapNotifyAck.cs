using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_iapNotifyAck : C2SJsonProto<C2S_iapNotifyAck_data>
    {
        public C2S_iapNotifyAck()
        {
            type = WSNetMsg.C2S_iapNotifyAck;
            bind_s2c_type = WSNetMsg.S2C_iapNotifyAck;  
            route = RouteConst.C2S_iapNotifyAck_route;
        }
    }
}