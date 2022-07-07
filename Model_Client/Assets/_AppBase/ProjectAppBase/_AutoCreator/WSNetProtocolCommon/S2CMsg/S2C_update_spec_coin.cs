using FutureCore;

namespace ProjectApp.Protocol
{
    // PP兑换  
    public class S2C_update_spec_coin : S2CJsonProto
    {
        public S2C_update_spec_coin_data data; 
        public S2C_update_spec_coin()
        {
            type = WSNetMsg.S2C_update_spec_coin;
        }  
    }

    public class S2C_update_spec_coin_data
    {

    }
}