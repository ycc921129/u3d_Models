using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_timer_sync : C2SJsonProto<C2S_timer_sync_data>
    {
        public C2S_timer_sync()
        {
            type = WSNetMsg.C2S_timer_sync;
            bind_s2c_type = WSNetMsg.S2C_timer_sync;
            data = new C2S_timer_sync_data();
        }
    }
}