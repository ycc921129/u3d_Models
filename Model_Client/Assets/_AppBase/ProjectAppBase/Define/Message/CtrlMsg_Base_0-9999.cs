/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp
{
    /// <summary>
    /// Ctrl消息
    /// Base_0-9999
    /// </summary>
    public static partial class CtrlMsg
    {
        public const string NAME = "CtrlMsg";
        public const uint BASE = 0;
        private static uint Cursor_BASE = BASE;

        /// 通用消息
        public static readonly uint CommonMsg = ++Cursor_BASE;

        /// 初始化
        // 用户配置 初始化
        public static readonly uint UserConfiguration_Init = ++Cursor_BASE;

        /// 流程
        // 登录 成功
        public static readonly uint Login_Succeed = ++Cursor_BASE;
        // 登录 重登等待延时
        public static readonly uint Login_ReloginWaitDelay = ++Cursor_BASE;
        // 登录 重登等待延时结束
        public static readonly uint Login_ReloginWaitDelayEnd = ++Cursor_BASE;
        // 登录 重登成功
        public static readonly uint Login_ReloginSucceed = ++Cursor_BASE;

        // 信息 初始化完成
        public static readonly uint Info_InitComplete = ++Cursor_BASE;
        // 信息 更新完成
        public static readonly uint Info_Updated = ++Cursor_BASE;

        // 远程存储 初始化完成
        public static readonly uint Preferences_InitComplete = ++Cursor_BASE;

        // 游戏 开始准备
        public static readonly uint Game_StartReady = ++Cursor_BASE;
        // 游戏 开始之前
        public static readonly uint Game_StartBefore = ++Cursor_BASE;
        // 游戏 开始
        public static readonly uint Game_Start = ++Cursor_BASE;
        // 游戏 开始之后
        public static readonly uint Game_StartLater = ++Cursor_BASE;

        // 弱联网 登录成功
        public static readonly uint WeakNetwork_LoginSucceed = ++Cursor_BASE;
        // 弱联网 打开连接网络UI
        public static readonly uint WeakNetwork_ShowConnectUI = ++Cursor_BASE;
        // 弱联网UI 点击消息
        public static readonly uint WeakNetworkUI_Click = ++Cursor_BASE;

        // 离线时间 通知
        public static readonly uint OfflineTime_Inform = ++Cursor_BASE;
        // 模块控制 更新
        public static readonly uint ModuleControl_Update = ++Cursor_BASE;

        // 游戏数据 初始化完成
        public static readonly uint GameData_InitComplete = ++Cursor_BASE;

        /// 内购
        // 内购 点击购买物品
        public static readonly uint IAP_ClickPurchaseGood = ++Cursor_BASE;
        // 内购 点击购买订阅
        public static readonly uint IAP_ClickPurchaseSubscribe = ++Cursor_BASE;
        // 内购 购买物品
        public static readonly uint IAP_PurchaseGood = ++Cursor_BASE;
        // 内购 购买订阅
        public static readonly uint IAP_PurchaseSubscribe = ++Cursor_BASE;
        // 订阅内购商品id分发
        public static readonly uint IAP_ProductDetailsData = ++Cursor_BASE;

        /// 核心模块
        // 心跳 更新服务器当前时间
        public static readonly uint HeartBeat_UpdateServerCurrTime = ++Cursor_BASE;
        // 网络定时器 初始化完成
        public static readonly uint NetTimer_InitComplete = ++Cursor_BASE;

        /// 业务模块
        // 邀请数量 变化
        public static readonly uint Module_InviteCountChange = ++Cursor_BASE;
        // 礼品兑换开关 变化
        public static readonly uint Module_GiftSwitchChange = ++Cursor_BASE;
        // 分成列表发生 变化
        public static readonly uint Module_RankCommissionChange = ++Cursor_BASE;
    }
}