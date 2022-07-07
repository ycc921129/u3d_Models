
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_timer_sync : S2CJsonProto
    {
        public S2C_timer_sync_data data; 
        public S2C_timer_sync()
        {
            type = WSNetMsg.S2C_timer_sync;
        }
    }
}