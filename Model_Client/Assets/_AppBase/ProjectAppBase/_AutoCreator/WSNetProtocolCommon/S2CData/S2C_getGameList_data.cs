/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace ProjectApp.Protocol
{
    /// <summary>
    ///客户端接口：获取游戏列表
    /// </summary>
    public class S2C_getGameList_data
    {
        public string pg;
        public string pgname;
        public string referrer;
        public List<S2C_getTask_data> task; 
        public long uid;
    }
}