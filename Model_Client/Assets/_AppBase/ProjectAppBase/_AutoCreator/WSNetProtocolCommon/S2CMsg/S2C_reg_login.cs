/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_reg_login : S2CJsonProto
    {
        public S2C_reg_login_data data; 
        public int sequenceNumber = 0;

        public S2C_reg_login() 
        {
            type = WSNetMsg.S2C_reg_login;  
        }
    }
}