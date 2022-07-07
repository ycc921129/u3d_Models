/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_updateTask : S2CJsonProto
    {
        public S2C_updateTask_data data; 
        public S2C_updateTask()  
        {
            type = WSNetMsg.S2C_updateTask;  
        }
    }
}