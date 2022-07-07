
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_timer_del : S2CJsonProto
    {
        public S2C_timer_del_data data; 
        public S2C_timer_del()
        {
            type = WSNetMsg.S2C_timer_del;
        }
    }
}