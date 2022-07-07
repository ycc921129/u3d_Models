/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public interface IRefCounter
    {
        int RefCount { get; }

        void Retain();
        void Release();
    }
}