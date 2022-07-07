
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_iapOrder : S2CJsonProto
    {
        public S2C_iapOrder_data data;
        public S2C_iapOrder()
        {
            type = WSNetMsg.S2C_iapOrder;
        }  
    }
}