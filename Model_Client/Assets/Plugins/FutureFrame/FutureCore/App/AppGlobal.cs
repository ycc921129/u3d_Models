/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    /// <summary>
    /// 游戏全局状态
    /// </summary>
    public static class AppGlobal
    {
        private static bool m_isLoginSucceed = false;
        /// <summary>
        /// 是否用户登录成功
        /// </summary>
        public static bool IsLoginSucceed
        {
            get
            {
                return WSNetMgr.Instance.isConnected && NetConst.IsNetAvailable && m_isLoginSucceed;
            }
            set
            {
                m_isLoginSucceed = value;
            }
        }

        /// <summary>
        /// 游戏是否开始
        /// </summary>
        public static bool IsGameStart = false;

        /// <summary>
        /// 游戏是否暂停
        /// </summary>
        public static bool IsGamePause = false;

        /// <summary>
        /// 是否显示断线提示
        /// </summary>
        public static bool IsShowDisconnectionTips = true;
    }
}