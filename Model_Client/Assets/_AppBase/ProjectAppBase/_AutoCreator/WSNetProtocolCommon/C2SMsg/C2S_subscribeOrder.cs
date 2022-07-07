/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
  
namespace ProjectApp.Protocol
{
    public class C2S_subscribeOrder : C2SJsonProto<C2S_subscribeOrder_data>
    {
        public C2S_subscribeOrder()
        {
            type = WSNetMsg.C2S_subscribeOrder;
            bind_s2c_type = WSNetMsg.S2C_subscribeOrder;
            route = RouteConst.C2S_subscribeOrder_route;  
        }
    }
}