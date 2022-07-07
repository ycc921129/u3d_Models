/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    /// <summary>
    /// 用户-商品列表
    /// </summary>
    public class C2S_exchangeList : C2SJsonProto<C2S_exchangeList_data>
    {
        public C2S_exchangeList()
        {
            type = WSNetMsg.C2S_exchangeList;
            bind_s2c_type = WSNetMsg.S2C_exchangeList; 
            route = RouteConst.C2S_exchangeList_route; 
        }
    }
}