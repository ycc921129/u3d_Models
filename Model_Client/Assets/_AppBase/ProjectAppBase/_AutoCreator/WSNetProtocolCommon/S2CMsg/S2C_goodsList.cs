
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_goodsList : S2CJsonProto
    {
        public S2C_goodsList_data data; 
        public S2C_goodsList()
        {
            type = WSNetMsg.S2C_goodsList;  
        }
    }
}