
using FutureCore;
using System.Collections.Generic;

namespace ProjectApp.Protocol
{
    public class S2C_exchangeList : S2CJsonProto
    {
        public List<S2C_exchangeList_data> data; 
        public S2C_exchangeList()
        {
            type = WSNetMsg.S2C_exchangeList;    
        }
    }
}