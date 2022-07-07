
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_exchangeOrder : S2CJsonProto
    {
        public S2C_exchangeOrder_data data; 
        public S2C_exchangeOrder()
        {
            type = WSNetMsg.S2C_exchangeOrder;  
        }
    }
}