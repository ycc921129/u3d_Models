
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_timer_out : S2CJsonProto
    {
        public S2C_timer_out_data data; 
        public S2C_timer_out()
        {
            type = WSNetMsg.S2C_timer_out;
        }
    }
}