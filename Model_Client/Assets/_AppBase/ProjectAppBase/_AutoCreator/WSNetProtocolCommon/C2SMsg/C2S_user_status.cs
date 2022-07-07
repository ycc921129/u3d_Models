
using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_user_status : C2SJsonProto<C2S_user_status_data>
    {
        public C2S_user_status()
        {
            type = WSNetMsg.C2S_user_status;
            bind_s2c_type = WSNetMsg.S2C_user_status;
            data = new C2S_user_status_data();
        }
    }
}