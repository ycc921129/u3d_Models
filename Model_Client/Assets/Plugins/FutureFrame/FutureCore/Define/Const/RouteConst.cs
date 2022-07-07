/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using Beebyte.Obfuscator;

namespace ProjectApp
{
    [Skip]
    public static partial class RouteConst
    {
        #region 发送
        public const string C2S_reg_login_route = "conn.handler.login";
        public const string C2S_preferences_route = "user.handler.updatePref";
        public const string C2S_goodsList_route = "user.handler.goodsList";
        public const string C2S_exchangeOrder_route = "user.handler.exchangeOrder";
        public const string C2S_exchangeList_route = "user.handler.exchangeList";
        public const string C2S_logicInvoke_route = "user.handler.logicInvoke";
        public const string C2S_pluginInvoke_route = "user.handler.pluginInvoke";
        public const string C2S_update_coin_route = "user.handler.updateAcct";
        public const string C2S_Invite_route = "user.handler.inviteBind";        
        public const string C2S_subscribeOrder_route = "user.handler.subscribeOrder";
        public const string C2S_subscribeCallback_route = "user.handler.subscribeCallback";
        public const string C2S_subscribeNotifyAck_route = "user.handler.subscribeNotifyAck";
        public const string C2S_iapOrder_route = "user.handler.iapOrder";
        public const string C2S_iapCallback_route = "user.handler.iapCallback";
        public const string C2S_iapNotifyAck_route = "user.handler.iapNotifyAck";
        #endregion

        #region 广播  
        /// <summary>
        /// 通知：更新info
        /// </summary>
        public const string Route_info = "NOTIFY_UPDATE_USER_INFO";
        /// <summary>
        /// 通知：更新兑换订单数据
        /// </summary>
        public const string Route_exchange = "NOTIFY_UPDATE_USER_EXCHANGE";
        /// <summary>
        /// 服务端通知：游戏列表
        /// </summary>
        public const string Route_gamelist = "NOTIFY_UPDATE_OFFERWALL_GAME_LIST";
        /// <summary>
        /// 服务端通知：任务列表
        /// </summary>
        public const string Route_tasklist = "NOTIFY_UPDATE_USER_TASK_LIST";
        /// <summary>
        /// 服务端通知：更新acct
        /// </summary>
        public const string Route_updateCoin = "NOTIFY_UPDATE_USER_ACCT";
        /// <summary>
        /// 更新Stattis
        /// </summary>
        public const string Route_updateStatis = "NOTIFY_UPDATE_USER_STATIS";        
        /// <summary>
        /// 订阅广播
        /// </summary>
        public const string Route_subscribe = "NOTIFY_UPDATE_USER_SUBSCRIBE";
        /// <summary>
        /// 内购广播  
        /// </summary>
        public const string Route_iap = "NOTIFY_UPDATE_USER_IAP";
        #endregion

        /// <summary>
        /// 心跳
        /// </summary>
        public const string Route_heartbeat = "heartbeat";
        /// <summary>
        /// 握手成功后发送心跳
        /// </summary>
        public const string HeartBeat_Success = "HeartBeat_Success";
    }
}