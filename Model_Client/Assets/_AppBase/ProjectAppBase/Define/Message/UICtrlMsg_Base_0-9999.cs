/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp
{
    /// <summary>
    /// UICtrl消息
    /// Base_0-9999
    /// </summary>
    public static partial class UICtrlMsg
    {
        public const string NAME = "UICtrlMsg";
        public const uint BASE = 0;
        private static uint Cursor_BASE = BASE;

        /// 更新界面
        // 显示需要更新
        public static readonly uint UpdateUI_ShowNeed = ++Cursor_BASE;
        // 显示强制更新
        public static readonly uint UpdateUI_ShowMust = ++Cursor_BASE;

        /// 菊花等待界面
        // 打开菊花等待界面
        public static readonly uint MumWaitUI_Open = ++Cursor_BASE;

        /// 断网界面
        // 打开断网界面
        public static readonly uint ReconnectUI_Open = ++Cursor_BASE;
    }
}