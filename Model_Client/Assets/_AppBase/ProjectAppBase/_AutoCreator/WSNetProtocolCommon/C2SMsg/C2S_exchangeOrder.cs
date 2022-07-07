/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    /// <summary>
    /// 用户-兑换下单
    /// </summary>
    public class C2S_exchangeOrder : C2SJsonProto<C2S_exchangeOrder_data>
    {
        public C2S_exchangeOrder()
        {
            type = WSNetMsg.C2S_exchangeOrder;
            bind_s2c_type = WSNetMsg.S2C_exchangeOrder;
            route = RouteConst.C2S_exchangeOrder_route;    
        }
    }
}