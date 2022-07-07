/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/


namespace ProjectApp
{
    public static partial class UICtrlMsg
    {
        public const uint CtrlMsg_CommonEvent = 11000;

        #region 断网
        /// <summary>
        /// 开启断网提示
        /// </summary>
        public const uint OpenReconnectFunc = CtrlMsg_CommonEvent + 1;
        /// <summary>
        /// 关闭断网提示
        /// </summary>
        public const uint CloseReconnectFunc = CtrlMsg_CommonEvent + 2;
        #endregion

        #region PayPal体验卡

        /// <summary>
        /// 通知主UI显示或关闭PayPal体验卡按钮入口
        /// </summary>
        public const uint SetPayPalTrialButtonDisplay = CtrlMsg_CommonEvent + 3;

        #endregion
    }
}
