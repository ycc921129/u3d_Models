
using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_user_event : C2SJsonProto<C2S_user_event_data>
    {
        public C2S_user_event()
        {
            type = WSNetMsg.C2S_user_event;
            bind_s2c_type = WSNetMsg.S2C_user_event;
            data = new C2S_user_event_data();
        }
    }
}