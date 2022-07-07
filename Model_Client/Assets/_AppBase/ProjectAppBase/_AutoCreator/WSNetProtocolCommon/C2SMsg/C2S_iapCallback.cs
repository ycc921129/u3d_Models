using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_iapCallback : C2SJsonProto<C2S_iapCallback_data>
    {
        public C2S_iapCallback()
        {
            type = WSNetMsg.C2S_iapCallback;
            bind_s2c_type = WSNetMsg.S2C_iapCallback;
            route = RouteConst.C2S_iapCallback_route;  
        }
    }
}