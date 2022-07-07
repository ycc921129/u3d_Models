/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using FutureCore;
using ProjectApp.Protocol;

namespace ProjectApp
{
    public static class WSNetMgrRegister
    {
        public static void AutoRegisterProtoType()
        {
            WSNetMgr wsNetMgr = WSNetMgr.Instance;
            // gen msg Register from /_AppBase/ProjectAppBase/_AutoCreator/WSNetProtocolCommon/C2SMsg
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_adwords, typeof(C2S_adwords));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_ad_event, typeof(C2S_ad_event));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_billing_state, typeof(C2S_billing_state));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_exchangeList, typeof(C2S_exchangeList));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_exchangeOrder, typeof(C2S_exchangeOrder));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_getGameList, typeof(C2S_getGameList));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_getMainInfo, typeof(C2S_getMainInfo));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_getTask, typeof(C2S_getTask));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_goodsList, typeof(C2S_goodsList));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_heartbeat, typeof(C2S_heartbeat));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_iapCallback, typeof(C2S_iapCallback));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_iapNotifyAck, typeof(C2S_iapNotifyAck));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_iapOrder, typeof(C2S_iapOrder));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_invite, typeof(C2S_invite));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_preferences, typeof(C2S_preferences));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_reg_login, typeof(C2S_reg_login));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_subscribeCallback, typeof(C2S_subscribeCallback));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_subscribeNotifyAck, typeof(C2S_subscribeNotifyAck));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_subscribeOrder, typeof(C2S_subscribeOrder));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_timer_add, typeof(C2S_timer_add));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_timer_del, typeof(C2S_timer_del));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_timer_sync, typeof(C2S_timer_sync));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_updateTask, typeof(C2S_updateTask));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_update_spec_coin, typeof(C2S_update_spec_coin));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_user_event, typeof(C2S_user_event));
            wsNetMgr.RegisterC2SProtoType(WSNetMsg.C2S_user_status, typeof(C2S_user_status));
            // gen msg Register from /_AppBase/ProjectAppBase/_AutoCreator/WSNetProtocolCommon/S2CMsg
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_billing_state, typeof(S2C_billing_state));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_except, typeof(S2C_except));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_exchangeList, typeof(S2C_exchangeList));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_exchangeOrder, typeof(S2C_exchangeOrder));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_game_award_times, typeof(S2C_game_award_times));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_getGameList, typeof(S2C_getGameList));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_getMainInfo, typeof(S2C_getMainInfo));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_getTask, typeof(S2C_getTask));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_goodsList, typeof(S2C_goodsList));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_heartbeat, typeof(S2C_heartbeat));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_iapCallback, typeof(S2C_iapCallback));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_iapNotifyAck, typeof(S2C_iapNotifyAck));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_iapOrder, typeof(S2C_iapOrder));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_invite, typeof(S2C_invite));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_InvokeExchange, typeof(S2C_InvokeExchange));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_InvokeGameList, typeof(S2C_InvokeGameList));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_InvokeIAP, typeof(S2C_InvokeIAP));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_InvokeInfo, typeof(S2C_InvokeInfo));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_InvokeSubscribe, typeof(S2C_InvokeSubscribe));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_InvokeTaskList, typeof(S2C_InvokeTaskList));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_InvokeUpdateCoin, typeof(S2C_InvokeUpdateCoin));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_InvokeUpdateStatis, typeof(S2C_InvokeUpdateStatis));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_preferences, typeof(S2C_preferences));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_reg_login, typeof(S2C_reg_login));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_subscribeCallback, typeof(S2C_subscribeCallback));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_subscribeNotifyAck, typeof(S2C_subscribeNotifyAck));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_subscribeOrder, typeof(S2C_subscribeOrder));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_timer_add, typeof(S2C_timer_add));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_timer_del, typeof(S2C_timer_del));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_timer_out, typeof(S2C_timer_out));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_timer_sync, typeof(S2C_timer_sync));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_updateTask, typeof(S2C_updateTask));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_update_spec_coin, typeof(S2C_update_spec_coin));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_user_event, typeof(S2C_user_event));
            wsNetMgr.RegisterS2CProtoType(WSNetMsg.S2C_user_status, typeof(S2C_user_status));
        }
    }
}