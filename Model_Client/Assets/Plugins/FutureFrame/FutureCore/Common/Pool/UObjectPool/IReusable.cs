/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    /// <summary>
    /// 可复用对象
    /// </summary>
    public interface IReusable
    {
        /// <summary>
        /// 构造
        /// </summary>
        void New();
        /// <summary>
        /// 销毁
        /// </summary>
        void Dispose();

        /// <summary>
        /// 取出
        /// </summary>
        void Get();
        /// <summary>
        /// 回收
        /// </summary>
        void Release();
    }
}