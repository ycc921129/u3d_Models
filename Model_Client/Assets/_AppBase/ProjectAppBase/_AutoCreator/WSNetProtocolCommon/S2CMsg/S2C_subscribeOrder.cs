/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_subscribeOrder : S2CJsonProto
    {  
        public S2C_subscribeOrder_data data;

        public S2C_subscribeOrder()
        { 
            type = WSNetMsg.S2C_subscribeOrder;
        }  
    }
}