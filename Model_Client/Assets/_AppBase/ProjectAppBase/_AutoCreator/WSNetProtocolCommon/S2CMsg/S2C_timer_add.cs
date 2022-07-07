
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_timer_add : S2CJsonProto
    {
        public S2C_timer_add_data data; 
        public S2C_timer_add()
        {
            type = WSNetMsg.S2C_timer_add;
        }
    }
}