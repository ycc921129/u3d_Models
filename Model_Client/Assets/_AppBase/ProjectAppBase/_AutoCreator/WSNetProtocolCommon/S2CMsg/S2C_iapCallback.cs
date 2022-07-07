
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_iapCallback : S2CJsonProto
    {
        public S2C_iapCallback_data data;
        public S2C_iapCallback()
        {
            type = WSNetMsg.S2C_iapCallback;    
        }
    }
}