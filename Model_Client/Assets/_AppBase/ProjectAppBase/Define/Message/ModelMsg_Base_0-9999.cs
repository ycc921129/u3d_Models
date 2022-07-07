/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp
{
    /// <summary>
    /// 模型层消息
    /// Base_0-9999
    /// </summary>
    public static partial class ModelMsg
    {
        public const string NAME = "ModelMsg";
        public const uint BASE = 0;
        private static uint Cursor_BASE = BASE;

        /// 数据
        // 更新现金
        public static readonly uint Database_UpdateUSD = ++Cursor_BASE;
        // 更新钻石
        public static readonly uint Database_UpdateCredit = ++Cursor_BASE;
        // 更新GP卡
        public static readonly uint Database_UpdateGPCard = ++Cursor_BASE;
        // 更新特殊货币
        public static readonly uint Database_UpdateSpecCoin = ++Cursor_BASE;
    }
}