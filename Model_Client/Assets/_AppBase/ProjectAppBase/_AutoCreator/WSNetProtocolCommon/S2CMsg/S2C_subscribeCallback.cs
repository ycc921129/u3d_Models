/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_subscribeCallback : S2CJsonProto
    {  
        public S2C_subscribeCallback_data data;

        public S2C_subscribeCallback()
        { 
            type = WSNetMsg.S2C_subscribeCallback;
        }  
    }
}