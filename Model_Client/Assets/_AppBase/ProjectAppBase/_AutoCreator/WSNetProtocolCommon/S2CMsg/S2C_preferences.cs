/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using FutureCore;

namespace ProjectApp.Protocol
{
    // Preferences
    public class S2C_preferences : S2CJsonProto
    {
        public Dictionary<string, object> data; 
        public S2C_preferences()
        {
            type = WSNetMsg.S2C_preferences;
        }
    }
}