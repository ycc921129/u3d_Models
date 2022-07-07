/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore.FC_Debuger
{
    /// <summary>
    /// 调试器激活窗口类型。
    /// </summary>
    public enum DebuggerConsoleActiveWindowType : byte
    {
        /// <summary>
        /// 总是打开。
        /// </summary>
        AlwaysOpen = 0,

        /// <summary>
        /// 仅在开发模式时打开。
        /// </summary>
        OnlyOpenWhenDevelopment,

        /// <summary>
        /// 仅在编辑器中打开。
        /// </summary>
        OnlyOpenInEditor,

        /// <summary>
        /// 总是关闭。
        /// </summary>
        AlwaysClose,
    }
}