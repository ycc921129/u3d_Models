using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_timer_add : C2SJsonProto<C2S_timer_add_data>
    {
        public C2S_timer_add()
        {
            type = WSNetMsg.C2S_timer_add;
            bind_s2c_type = WSNetMsg.S2C_timer_add;
            data = new C2S_timer_add_data();
        }
    }
}