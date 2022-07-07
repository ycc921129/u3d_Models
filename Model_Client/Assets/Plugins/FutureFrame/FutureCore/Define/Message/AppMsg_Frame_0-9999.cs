/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.10
*/

namespace FutureCore
{
    /// <summary>
    /// 应用消息
    /// Frame_0-9999
    /// </summary>
    public static class AppMsg
    {
        public const string NAME = "AppMsg";
        public const uint BASE = 0;
        private static uint Cursor_BASE = BASE;

        /// 应用消息
        // 应用拉起
        public static readonly uint App_StartUp = ++Cursor_BASE;
        // 应用退出
        public static readonly uint App_Quit = ++Cursor_BASE;
        // 应用失去焦点
        public static readonly uint App_Focus_False = ++Cursor_BASE;
        // 应用开始暂停
        public static readonly uint App_Pause_True = ++Cursor_BASE;
        // 应用游戏暂停
        public static readonly uint App_GamePause = ++Cursor_BASE;
        // 应用游戏恢复
        public static readonly uint App_GameResume = ++Cursor_BASE;
        // 应用切换多语言
        public static readonly uint App_SwitchLanguage = ++Cursor_BASE;

        /// 系统消息
        // 系统资源初始化完成
        public static readonly uint System_AssetsInitComplete = ++Cursor_BASE;
        // 系统初始化常驻资源完成
        public static readonly uint System_PermanentAssetsInitComplete = ++Cursor_BASE;
        // 系统本地配置表初始化解析完成
        public static readonly uint System_LocalConfigInitComplete = ++Cursor_BASE;
        // 系统配置表初始化解析完成
        public static readonly uint System_ConfigInitComplete = ++Cursor_BASE;
        // 系统配置表初始化解析错误
        public static readonly uint System_ConfigInitError = ++Cursor_BASE;
        // 系统管理器启动完成
        public static readonly uint System_ManagerStartUpComplete = ++Cursor_BASE;

        /// 界面消息
        // 显示加载界面
        public static readonly uint UI_DisplayLoadingUI = ++Cursor_BASE;
        // 隐藏加载界面
        public static readonly uint UI_HideLoadingUI = ++Cursor_BASE;
        // 设置加载界面文本
        public static readonly uint UI_SetLoadingUIInfo = ++Cursor_BASE;
        // 设置加载界面进度
        public static readonly uint UI_SetLoadingUIProgress = ++Cursor_BASE;
        // 加载界面进度完成
        public static readonly uint UI_LoadingUIProgressComplete = ++Cursor_BASE;
        // 显示等待时间界面
        public static readonly uint UI_DisplayWaitTimeUI = ++Cursor_BASE;
        // 显示等待界面
        public static readonly uint UI_DisplayWaitUI = ++Cursor_BASE;
        // 隐藏等待界面
        public static readonly uint UI_HideWaitUI = ++Cursor_BASE;
        // 显示Tips界面
        public static readonly uint UI_ShowTipsUI = ++Cursor_BASE;
        // 显示确认界面
        public static readonly uint UI_ShowAffirmUI = ++Cursor_BASE;
        // 打开平台Tips界面
        public static readonly uint UI_ShowPlatformTipsUI = ++Cursor_BASE;

        /// Tcp服务器消息
        // Tcp服务器连接无网络
        public static readonly uint TcpServer_ConnectNoNetwork = ++Cursor_BASE;
        // Tcp服务器连接开始
        public static readonly uint TcpServer_ConnectStart = ++Cursor_BASE;
        // Tcp服务器连接成功
        public static readonly uint TcpServer_ConnectSucceed = ++Cursor_BASE;
        // Tcp服务器连接失败
        public static readonly uint TcpServer_ConnectFailed = ++Cursor_BASE;
        // Tcp服务器断开连接
        public static readonly uint TcpServer_Disconnect = ++Cursor_BASE;
        // Tcp服务器发生异常
        public static readonly uint TcpServer_Exception = ++Cursor_BASE;
        // Tcp服务器心跳超时
        public static readonly uint TcpServer_HeatBeatOverTime = ++Cursor_BASE;

        /// WebSocket服务器消息
        // WebSocket服务器连接无网络
        public static readonly uint WebSocketServer_ConnectNoNetwork = ++Cursor_BASE;
        // WebSocket服务器连接开始
        public static readonly uint WebSocketServer_ConnectStart = ++Cursor_BASE;
        // WebSocket服务器连接成功
        public static readonly uint WebSocketServer_ConnectSucceed = ++Cursor_BASE;
        // WebSocket服务器连接失败
        public static readonly uint WebSocketServer_ConnectFailed = ++Cursor_BASE;
        // WebSocket服务器断开连接
        public static readonly uint WebSocketServer_Disconnect = ++Cursor_BASE;
        // WebSocket服务器发生异常
        public static readonly uint WebSocketServer_Exception = ++Cursor_BASE;
        // WebSocket服务器心跳超时
        public static readonly uint WebSocketServer_HeatBeatOverTime = ++Cursor_BASE;
        // WebSocket服务器Preferences解析错误
        public static readonly uint WebSocketServer_PreferencesParseError = ++Cursor_BASE;
        // WebSocket服务器State更新
        public static readonly uint WebSocketServer_StateUpdate = ++Cursor_BASE;

        /// Http服务器消息
        // Http请求配置
        public static readonly uint Http_RequestConfiging = ++Cursor_BASE;


        /// 界面事件消息
        // 界面打开
        public static readonly uint UIEvent_UIOpen = ++Cursor_BASE;
        // 界面关闭
        public static readonly uint UIEvent_UIClose = ++Cursor_BASE;
        // 界面显示
        public static readonly uint UIEvent_UIDisplay = ++Cursor_BASE;
        // 界面隐藏
        public static readonly uint UIEvent_UIHide = ++Cursor_BASE;
        // 界面点击之前
        public static readonly uint UIEvent_Click_Before = ++Cursor_BASE;
        // 界面双击之前
        public static readonly uint UIEvent_DblClick_Before = ++Cursor_BASE;
        // 界面按下之前
        public static readonly uint UIEvent_ClickDown_Before = ++Cursor_BASE;
        // 界面抬起之前
        public static readonly uint UIEvent_ClickUp_Before = ++Cursor_BASE;
        // 界面点击
        public static readonly uint UIEvent_Click = ++Cursor_BASE;
        // 界面双击
        public static readonly uint UIEvent_DblClick = ++Cursor_BASE;
        // 界面按下
        public static readonly uint UIEvent_ClickDown = ++Cursor_BASE;
        // 界面抬起
        public static readonly uint UIEvent_ClickUp = ++Cursor_BASE;

        /// 按键消息
        // 按键Home
        public static readonly uint KeyCode_Home = ++Cursor_BASE;
        // 按键Escape
        public static readonly uint KeyCode_Escape = ++Cursor_BASE;

        /// 场景消息
        // 场景切换
        public static readonly uint Scene_Switch = ++Cursor_BASE;

        /// 设置消息
        // 时间缩放系数改变
        public static readonly uint TimeScale_Change = ++Cursor_BASE;

        /// 其他消息
        // 射线事件系统启动变化
        public static readonly uint WorldRaycast_EnableChange = ++Cursor_BASE;
        // 发现用户速度作弊
        public static readonly uint AntiCheat_SpeedHackDetected = ++Cursor_BASE;
    }
}