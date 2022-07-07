using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_subscribeNotifyAck : C2SJsonProto<C2S_subscribeNotifyAck_data>
    {
        public C2S_subscribeNotifyAck()
        {
            type = WSNetMsg.C2S_subscribeNotifyAck;
            data = new C2S_subscribeNotifyAck_data();
            route = RouteConst.C2S_subscribeNotifyAck_route;
        }
    }
}