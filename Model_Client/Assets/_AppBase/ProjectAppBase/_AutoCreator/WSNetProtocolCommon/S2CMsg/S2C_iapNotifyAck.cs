
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_iapNotifyAck : S2CJsonProto
    {
        public S2C_iapNotifyAck_data data;
        public S2C_iapNotifyAck()
        {
            type = WSNetMsg.S2C_iapNotifyAck;
        }  
    }
}