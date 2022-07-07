/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public interface ITickUpdate
    {
        void OnUpdate();
        void OnLateUpdate();
        void OnFixedUpdate();
    }
}