/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using FutureCore;

namespace ProjectApp.Protocol
{
    public class S2C_getGameList : S2CJsonProto
    {
        public List<S2C_getGameList_data>  data;   
        public S2C_getGameList()  
        {
            type = WSNetMsg.S2C_getGameList;    
        }  
    }  
}