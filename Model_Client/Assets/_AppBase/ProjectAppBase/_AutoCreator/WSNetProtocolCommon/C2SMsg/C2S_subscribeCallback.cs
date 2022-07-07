/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
  
namespace ProjectApp.Protocol
{
    public class C2S_subscribeCallback : C2SJsonProto<C2S_subscribeCallback_data>
    {
        public C2S_subscribeCallback()
        {
            type = WSNetMsg.C2S_subscribeCallback;
            bind_s2c_type = WSNetMsg.S2C_subscribeCallback;
            route = RouteConst.C2S_subscribeCallback_route;  
        }
    }
}