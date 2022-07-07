/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp
{
    /// <summary>
    /// 渠道消息
    /// Base_1000-9999
    /// </summary>
    public static class ChannelMsg
    {
        public const string NAME = "ChannelMsg";
        public const uint BASE = 10000;
        private static uint Cursor_BASE = BASE;

        // 响应登入后绑定Token
        public static readonly uint OnLoginBindToken = ++Cursor_BASE;
        // (旧)响应订阅回调
        public static readonly uint OnHandlePurchase = ++Cursor_BASE;
        // (新)响应订阅回调
        public static readonly uint OnBillingSubscribePurchased = ++Cursor_BASE;
        // 响应内购回调
        public static readonly uint OnBillingIapPurchased = ++Cursor_BASE;
    }
}