using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_billing_state : C2SJsonProto<C2S_billing_state_data>
    {
        public C2S_billing_state()
        {
            type = WSNetMsg.C2S_billing_state;
            bind_s2c_type = WSNetMsg.S2C_billing_state;
            data = new C2S_billing_state_data();
        }
    }
}