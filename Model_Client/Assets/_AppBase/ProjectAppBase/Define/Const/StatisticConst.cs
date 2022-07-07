/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp
{
    public static partial class StatisticConst
    {
        #region 设备

        /// <summary>
        /// 设备_CPU架构,0
        /// </summary>
        public const string os_arch = "os_arch";

        #endregion

        #region 平台层

        /// <summary>
        /// 平台层_进入启动页,0
        /// </summary>
        public const string init_launcher_page = "init_launcher_page";

        /// <summary>
        /// 平台层_第一次进入启动页,0
        /// </summary>
        public const string new_user_init_launcher_page = "new_user_init_launcher_page";

        /// <summary>
        /// 平台层_启动时长,1
        /// </summary>
        public const string launcher_time = "launcher_time";

        /// <summary>
        /// 平台层_onResume_新用户,0
        /// </summary>
        public const string new_user_onResume = "new_user_onResume";

        /// <summary>
        /// 平台层_点击推送,0
        /// </summary>
        public const string push_click = "push_click";

        /// <summary>
        /// 平台层_收到推送详情,0
        /// </summary>
        public const string push_receiver = "push_receiver";

        /// <summary>
        /// 平台层_推广utm字段统计,0
        /// </summary>
        public const string utm = "utm";

        #endregion 平台层

        #region 登录全流程漏斗_新用户

        /// <summary>
        /// 登录流程漏斗_新用户_框架启动1,1
        /// </summary>
        public const string dlld_newuser_framelaunch_1 = "dlld_newuser_framelaunch_1";

        /// <summary>
        /// 登录流程漏斗_新用户_开始登陆2,1
        /// </summary>
        public const string dlld_newuser_connectlogin_2 = "dlld_newuser_connectlogin_2";

        /// <summary>
        /// 登录流程漏斗_新用户_登录成功3,1
        /// </summary>
        public const string dlld_newuser_loginsucceed_3 = "dlld_newuser_loginsucceed_3";

        /// <summary>
        /// 登录流程漏斗_新用户_Preference初始化开始4,1
        /// </summary>
        public const string dlld_newuser_preferencesinitstart_4 = "dlld_newuser_preferencesinitstart_4";

        /// <summary>
        /// 登录流程漏斗_新用户_Preference初始化完成5,1
        /// </summary>
        public const string dlld_newuser_preferencesinitcomplete_5 = "dlld_newuser_preferencesinitcomplete_5";

        /// <summary>
        /// 登录流程漏斗_新用户_Preference初始化结束6,1
        /// </summary>
        public const string dlld_newuser_preferencesinitend_6 = "dlld_newuser_preferencesinitend_6";

        /// <summary>
        /// 登录流程漏斗_新用户_配置初始化7,1
        /// </summary>
        public const string dlld_newuser_configinit_7 = "dlld_newuser_configinit_7";

        /// <summary>
        /// 登录流程漏斗_新用户_载入完成8,1
        /// </summary>
        public const string dlld_newuser_loadcomplete_8 = "dlld_newuser_loadcomplete_8";

        /// <summary>
        /// 登录流程漏斗_新用户_游戏开始9,1
        /// </summary>
        public const string dlld_newuser_gamestart_9 = "dlld_newuser_gamestart_9";

        #endregion

        #region 登录全流程漏斗

        /// <summary>
        /// 登录流程漏斗_框架启动1,1
        /// </summary>
        public const string dlld_framelaunch_1 = "dlld_framelaunch_1";

        /// <summary>
        /// 登录流程漏斗_开始登陆2,1
        /// </summary>
        public const string dlld_connectlogin_2 = "dlld_connectlogin_2";

        /// <summary>
        /// 登录流程漏斗_登录成功3,1
        /// </summary>
        public const string dlld_loginsucceed_3 = "dlld_loginsucceed_3";

        /// <summary>
        /// 登录流程漏斗_Preference初始化开始4,1
        /// </summary>
        public const string dlld_preferencesinitstart_4 = "dlld_preferencesinitstart_4";

        /// <summary>
        /// 登录流程漏斗_Preference初始化完成5,1
        /// </summary>
        public const string dlld_preferencesinitcomplete_5 = "dlld_preferencesinitcomplete_5";

        /// <summary>
        /// 登录流程漏斗_Preference初始化结束6,1
        /// </summary>
        public const string dlld_preferencesinitend_6 = "dlld_preferencesinitend_6";

        /// <summary>
        /// 登录流程漏斗_配置初始化7,1
        /// </summary>
        public const string dlld_configinit_7 = "dlld_configinit_7";

        /// <summary>
        /// 登录流程漏斗_载入完成8,1
        /// </summary>
        public const string dlld_loadcomplete_8 = "dlld_loadcomplete_8";

        /// <summary>
        /// 登录流程漏斗_游戏开始9,1
        /// </summary>
        public const string dlld_gamestart_9 = "dlld_gamestart_9";

        #endregion

        #region 配置表初始化

        /// <summary>
        /// 配置表解析错误,0
        /// </summary>
        public const string config_parseerror = "config_parseerror";

        /// <summary>
        /// 配置表序列化错误,0
        /// </summary>
        public const string config_serializeerror = "config_serializeerror";

        #endregion

        #region 问题定位漏斗_新用户

        /// <summary>
        /// 问题定位漏斗_新用户_1,0
        /// </summary>
        public const string wtld_newuser_1 = "wtld_newuser_1";

        #endregion

        #region 登录

        /// <summary>
        /// 登录_进入loading页,0
        /// </summary>
        public const string init_loading_page = "init_loading_page";


        /// <summary>
        /// socket连接成功,1
        /// </summary>
        public const string socket_connect = "socket_connect";

        /// <summary>
        /// 登录_第一次登录成功,1
        /// </summary>
        public const string login_succeed = "login_succeed";

        /// <summary>
        /// 登录_loading时长,1
        /// </summary>
        public const string loading_time = "loading_time";

        /// <summary>
        /// 登录_进入游戏主页,0
        /// </summary>
        public const string init_main_page = "init_main_page";

        /// <summary>
        /// 登录_新用户第一次登录成功,1
        /// </summary>
        public const string new_user_login_succeed = "new_user_login_succeed";

        /// <summary>
        /// 登录_新用户第一次loading时长,1
        /// </summary>
        public const string new_user_loading_time = "new_user_loading_time";

        /// <summary>
        /// 登录_新用户第一次进入游戏主页,0
        /// </summary>
        public const string new_user_init_main_page = "new_user_init_main_page";

        /// <summary>
        ///  新用户第一次socket连接成功,1
        /// </summary>
        public const string new_user_socket_connected = "new_user_socket_connected";

        #endregion 登录

        #region 网络

        /// <summary>
        /// 网络_网络错误统计,0
        /// </summary>
        public const string network_error = "network_error";

        /// <summary>
        /// 网络_网络可用错误-连接IP失败次数,0
        /// </summary>
        public const string networkavailable_connect_error = "networkavailable_connect_error";

        /// <summary>
        /// 网络_网络可用连接速度-毫秒,1
        /// </summary>
        public const string networkavailable_connect_delay = "networkavailable_connect_delay";

        /// <summary>
        /// 网络_网络延迟-心跳,1
        /// </summary>
        public const string networkavailable_delay = "networkavailable_delay";

        /// <summary>
        /// websocket最优线路,0
        /// </summary>
        public const string ws_best_url = "ws_best_url";

        #endregion 网络

        #region 游戏配置

        /// <summary>
        /// 游戏配置相关_game_settings_url请求次数,0
        /// </summary>
        public const string game_settings_url_request = "game_settings_url_request";

        /// <summary>
        /// 游戏配置相关_game_settings_url返回成功 时长,1
        /// </summary>
        public const string game_settings_url_success = "game_settings_url_success";

        /// <summary>
        /// 游戏配置相关_game_settings_url请求失败 时长,1
        /// </summary>
        public const string game_settings_url_fail = "game_settings_url_fail";

        #endregion 游戏配置

        #region 养成模块

        /// <summary>
        /// 养成模块_离线收益广告按钮点击次数,0
        /// </summary>
        public const string offline_video_ad_btn_click = "offline_video_ad_btn_click";

        /// <summary>
        /// 养成模块_离线收益广告按钮成功次数,0
        /// </summary>
        public const string offline_video_ad_btn_click_success = "offline_video_ad_btn_click_succeed";

        #endregion 养成模块

        #region 抽奖模块

        /// <summary>
        /// 抽奖模块_购买奖券按钮点击次数,0
        /// </summary>
        public const string raffle_buy_ticket_btn_click = "raffle_buy_ticket_btn_click";

        /// <summary>
        /// 抽奖模块_查看更多获奖人按钮点击次数,0
        /// </summary>
        public const string raffle_watch_more_people_btn_click = "raffle_watch_more_people_btn_click";

        #endregion 抽奖模块

        #region 邀请模块

        /// <summary>
        /// 邀请模块_邀请按钮UI点击次数,0
        /// </summary>
        public const string invite_ui_invite_btn_click = "invite_ui_invite_btn_click";

        /// <summary>
        /// 邀请模块_邀请码复制次数,0
        /// </summary>
        public const string invite_ui_invite_code_copy_btn_click = "invite_ui_invitecode_copy_btn_click";

        /// <summary>
        /// 邀请模块_邀请链接复制次数,0
        /// </summary>
        public const string invite_ui_invite_link_copy_btn_click = "invite_ui_invite_link_copy_btn_click";

        /// <summary>
        /// 邀请模块_邀请码填写按钮次数,0
        /// </summary>
        public const string invite_ui_invite_link_input_btn_click = "invite_ui_invite_link_input_btn_click";

        /// <summary>
        /// 邀请模块_邀请码填写成功次数,0
        /// </summary>
        public const string invite_ui_invite_link_input_success = "invite_ui_invite_link_input_success";

        /// <summary>
        /// 邀请模块_邀请界面分享按钮点击次数,0
        /// </summary>
        public const string invite_ui_distribution_btn_get_click = "invite_ui_distribution_btn_get_click";

        /// <summary>
        /// 邀请模块_分销按钮领取成功次数,0
        /// </summary>
        public const string invite_ui_distribution_btn_get_success = "invite_ui_distribution_btn_get_success";

        /// <summary>
        /// 唤醒模块_唤醒按钮广告领取点击次数,0
        /// </summary>
        public const string wakeUp_ui_wakeup_btn_click = "wakeUp_ui_wakeup_btn_click";

        /// <summary>
        /// 唤醒模块_唤醒按钮广告领取成功次数,0
        /// </summary>
        public const string wakeUp_ui_wakeup_btn_click_success = "wakeUp_ui_wakeup_btn_click_success";

        #endregion 邀请模块

        #region 设置模块

        /// <summary>
        /// 设置模块_登陆账号UI点击次数,0
        /// </summary>
        public const string setting_ui_signin_account_btn_click = "setting_ui_signin_account_btn_click";

        /// <summary>
        /// 设置模块_绑定手机号点击次数,0
        /// </summary>
        public const string setting_ui_bind_mobile_btn_click = "setting_ui_signin_account_click";

        /// <summary>
        /// 设置模块_评星UI点击次数,0
        /// </summary>
        public const string setting_ui_rate_btn_click = "setting_ui_rate_btn_click";

        /// <summary>
        /// 设置模块_反馈UI点击次数,0
        /// </summary>
        public const string setting_ui_feedback_btn_click = "setting_ui_feedback_btn_click";

        #endregion 设置模块

        #region 状态栏模块

        /// <summary>
        /// 状态栏_金币栏目点击次数,0
        /// </summary>
        public const string basicInfo_coin_btn_click = "basicInfo_coin_btn_click";

        /// <summary>
        /// 状态栏_钻石栏目点击次数,0
        /// </summary>
        public const string basicInfo_diamond_btn_click = "basicInfo_diamond_btn_click";

        /// <summary>
        /// 状态栏_现金栏目点击次数,0
        /// </summary>
        public const string basicInfos_cash_btn_click = "sbasicInfo_cash_btn_click";

        #endregion 状态栏

        #region 签到模块

        /// <summary>
        /// 签到界面_签到按钮点击次数,0
        /// </summary>
        public const string checkin_ui_checkin_btn_click = "checkin_ui_checkin_btn_click";

        /// <summary>
        /// 签到界面_签到按钮逻辑失败次数,0
        /// </summary>
        public const string checkin_ui_checkin_btn_click_failed = "checkin_ui_checkin_btn_click_failed";

        /// <summary>
        /// 签到视频播放失败
        /// </summary>
        public const string checkin_ui_checkin_btn_click_video_failed = "checkin_ui_checkin_btn_click_video_failed";

        #endregion 签到模块

        #region 新用户通用打点

        /// <summary>
        /// 通用打点_新用户视频广告展示次数,0
        /// </summary>
        public const string new_user_video_ad_play = "new_user_video_ad_play";

        /// <summary>
        /// 通用打点_新用户视频广告完整看完次数,0
        /// </summary>
        public const string new_user_video_ad_complete = "new_user_video_ad_complete";

        /// <summary>
        /// 通用打点_新用户插屏广告展示次数,0
        /// </summary>
        public const string new_user_interstitial_ad_show = "new_user_interstitial_ad_show";

        /// <summary>
        /// 通用打点_新用户分享次数,0
        /// </summary>
        public const string new_user_share = "new_user_share";

        #endregion 新用户通用打点

        #region 通用打点

        /// <summary>
        /// 通用打点_视频广告展示次数,0
        /// </summary>
        public const string video_ad_play = "video_ad_play";

        /// <summary>
        /// 通用打点_视频广告完整看完次数,0
        /// </summary>
        public const string video_ad_complete = "video_ad_complete";

        /// <summary>
        /// 通用打点_插屏广告展示次数,0
        /// </summary>
        public const string interstitial_ad_show = "interstitial_ad_show";

        /// <summary>
        /// 通用打点_分享次数,0
        /// </summary>
        public const string share = "share";

        #endregion 通用打点

        #region PayPal体验卡打点

        /// <summary>
        /// PayPal体验卡打点_PayPal体验卡分享次数,0
        /// </summary>
        public const string paypaltrial_share = "paypaltrial_share";

        /// <summary>
        /// PayPal体验卡打点_PP卡减少冷却广告按钮点击次数,0
        /// </summary>
        public const string paypaltrial_ad_click = "paypaltrial_ad_click";

        /// <summary>
        /// PayPal体验卡打点_PP卡减少冷却广告按钮成功次数,0
        /// </summary>
        public const string paypaltrial_ad_success = "paypaltrial_ad_success";

        /// <summary>
        /// PayPal体验卡打点_按钮点击次数,0
        /// </summary>
        public const string paypaltrial_entry_click = "paypaltrial_entry_click";

        /// <summary>
        /// PayPal体验卡打点_PP卡兑换次数,0
        /// </summary>
        public const string paypaltrial_redeem = "paypaltrial_redeem";

        #endregion

        #region GooglePlay卡打点

        /// <summary>
        /// GooglePlay卡_GP卡看广告点击次数,0
        /// </summary>
        public const string googleplayCard_ad_click = "googleplayCard_ad_click";

        /// <summary>
        /// GooglePlay卡_GP卡看广告成功次数,0
        /// </summary>
        public const string googleplayCard_ad_success = "googleplayCard_ad_success";

        /// <summary>
        /// GooglePlay卡_gp卡关卡领取界面弹出次数,0
        /// </summary>
        public const string googleplayCard_ui_popup_level = "googleplayCard_ui_popup_level";

        #endregion

        #region SDK打点

        /// <summary>
        /// SDK打点_视频sdk触摸按钮,0
        /// </summary>
        public const string video_sdk_touch_button = "video_sdk_touch_button";

        /// <summary>
        /// SDK打点_视频sdk请求,0
        /// </summary>
        public const string video_sdk_request = "video_sdk_request";

        /// <summary>
        /// SDK打点_视频sdk展示,0
        /// </summary>
        public const string video_sdk_impression = "video_sdk_impression";

        /// <summary>
        /// SDK打点_视频sdk获得奖励,0
        /// </summary>
        public const string video_sdk_rewarded = "video_sdk_rewarded";

        /// <summary>
        /// SDK打点_视频sdk点击,0
        /// </summary>
        public const string video_sdk_click = "video_sdk_click";

        /// <summary>
        /// SDK打点_插屏sdk请求,0
        /// </summary>
        public const string interstitial_sdk_request = "interstitial_sdk_request";

        /// <summary>
        /// SDK打点_插屏sdk展示,0
        /// </summary>
        public const string interstitial_sdk_impression = "interstitial_sdk_impression";

        /// <summary>
        /// SDK打点_插屏sdk点击,0
        /// </summary>
        public const string interstitial_sdk_click = "interstitial_sdk_click";

        /// <summary>
        /// SDK打点_横幅sdk请求,0
        /// </summary>
        public const string banner_sdk_request = "banner_sdk_request";

        /// <summary>
        /// SDK打点_横幅sdk展示,0
        /// </summary>
        public const string banner_sdk_impression = "banner_sdk_impression";

        /// <summary>
        /// SDK打点_横幅sdk点击,0
        /// </summary>
        public const string banner_sdk_click = "banner_sdk_click";

        #endregion

        #region 运营

        /// <summary>
        /// 兑换开关状态,0
        /// <summary>
        public const string gifts_switch = "gifts_switch";

        /// <summary>
        /// 开了兑换,0
        /// <summary>
        public const string gifts_switch_on = "gifts_switch_on";

        /// <summary>
        /// 是否显示PP卡槽,0
        /// <summary>
        public const string PayPal_turn_on = "PayPal_turn_on";

        /// <summary>
        /// 从启动应用到激活兑换的时长,1
        /// <summary>
        public const string gifts_switch_open_duration = "gifts_switch_open_duration";

        /// <summary>
        /// 展示主页上的PP卡槽,0
        /// <summary>
        public const string Paypal_homepage = "Paypal_homepage";

        /// <summary>
        /// PP卡领取页面展示数,0
        /// <summary>
        public const string PayPal_impression = "PayPal_impression";

        /// <summary>
        /// 领取按钮点击数,0
        /// <summary>
        public const string PayPal_click = "PayPal_click";

        /// <summary>
        /// 成功领取PP卡次数_或者PP卡增加次数,0
        /// </summary>
        public const string PayPal_reward_success = "PayPal_reward_success";

        #endregion
        
        #region Pp卡通用
        /// <summary>
        /// PayPal卡领取弹窗展示次数,0
        /// <summary>
        public const string pp_card_reward_dialog = "pp_card_reward_dialog";
        /// <summary>
        /// PayPal卡领取弹窗领取成功次数,0
        /// <summary>
        public const string pp_card_reward_success = "pp_card_reward_success";
        /// <summary>
        /// 真兑换系统里成交提交兑换的订单数,0
        /// <summary>
        public const string pp_card_redeem_success = "pp_card_redeem_success";
        /// <summary>
        /// 库存不足弹窗展示次数,0
        /// <summary>
        public const string pp_card_redeem_fail = "pp_card_redeem_fail";
        #endregion

        #region 内购物品

        /// <summary>
        /// 内购物品点击,0
        /// </summary>
        public const string IAPGoods_click = "IAPGoods_click";

        /// <summary>
        /// 内购物品成功,0
        /// </summary>
        public const string IAPGoods_succeed = "IAPGoods_succeed";

        /// <summary>
        /// 内购物品失败,0
        /// </summary>
        public const string IAPGoods_err = "IAPGoods_err";

        #endregion

        #region 内购订阅

        /// <summary>
        /// 内购订阅点击,0
        /// </summary>
        public const string IAPSubscribe_click = "IAPSubscribe_click";

        /// <summary>
        /// 内购订阅成功,0
        /// </summary>
        public const string IAPSubscribe_succeed = "IAPSubscribe_succeed";

        /// <summary>
        /// 内购订阅失败,0
        /// </summary>
        public const string IAPSubscribe_err = "IAPSubscribe_err";

        #endregion

        #region 内购

        /// <summary>
        /// 内购成功,0
        /// </summary>
        public const string IAP_succeed = "IAP_succeed";

        /// <summary>
        /// 内购失败,0
        /// </summary>
        public const string IAP_err = "IAP_err";

        #endregion
        
        #region 订阅校验打点
        /// <summary>
        /// 内购游戏层开始验证,1
        /// </summary>
        public const string newbyear_subs_game_verify_start = "newbyear_subs_game_verify_start";
        /// <summary>
        /// 内购游戏层请求验证,1
        /// </summary>
        public const string newbyear_subs_game_verify_request = "newbyear_subs_game_verify_request";
        /// <summary>
        /// 内购游戏层验证响应通过,1
        /// </summary>
        public const string newbyear_subs_game_verify_respond_ok = "newbyear_subs_game_verify_respond_ok";
        #endregion
    }
}