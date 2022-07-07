/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FuturePlugin
{
    public enum ChannelType : int
    {
        /// <summary>
        /// 本地测试
        /// </summary>
        LocalDebug = 0,

        /// <summary>
        /// 外网审核
        /// </summary>
        NetCheck = 1,

        /// <summary>
        /// 外网正式
        /// </summary>
        NetRelease = 2,
    }
}