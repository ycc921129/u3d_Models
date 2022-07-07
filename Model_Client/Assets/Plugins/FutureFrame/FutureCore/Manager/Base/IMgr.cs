/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.13
*/

namespace FutureCore
{
    public interface IMgr
    {
        void Init();
        void StartUp();
        void DisposeBefore();
        void Dispose();
    }
}