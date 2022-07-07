/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_getTask : S2CJsonProto
    {
        public List<S2C_getTask_data> data; 
        public S2C_getTask()
        {
            type = WSNetMsg.S2C_getTask;
        }
    }
}