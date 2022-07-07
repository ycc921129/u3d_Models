using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_timer_del : C2SJsonProto<C2S_timer_del_data>
    {
        public C2S_timer_del()
        {
            type = WSNetMsg.C2S_timer_del;
            bind_s2c_type = WSNetMsg.S2C_timer_del;
            data = new C2S_timer_del_data();
        }
    }
}