/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_getMainInfo : S2CJsonProto
    {
        public S2C_getMainInfo_data data;
            
        public S2C_getMainInfo()
        {
            type = WSNetMsg.S2C_getMainInfo;  
            data = new S2C_getMainInfo_data();  
        }
    }
}