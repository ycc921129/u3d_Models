/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using FutureCore;

namespace ProjectApp.Protocol
{
    // 服务器异常
    public class S2C_except : S2CJsonProto
    {
        public object data;

        public S2C_except()
        {
            type = WSNetMsg.S2C_except;
        }
    }
}