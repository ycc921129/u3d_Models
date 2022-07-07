
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_billing_state : S2CJsonProto
    {
        public S2C_billing_state_data data;
        public S2C_billing_state()
        {
            type = WSNetMsg.S2C_billing_state;
        }
    }
}