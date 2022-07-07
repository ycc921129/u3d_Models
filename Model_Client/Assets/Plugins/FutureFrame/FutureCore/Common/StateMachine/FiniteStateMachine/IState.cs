/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// 状态Id
        /// </summary>
        uint StateId { get; }

        /// <summary>
        /// 当注册
        /// </summary>
        /// <param name="fsm">状态机</param>
        void OnRegister(object fsm);

        /// <summary>
        /// 当移除
        /// </summary>
        /// <param name="fsm">状态机</param>
        void OnRemove(object fsm);

        /// <summary>
        /// 进入这个状态
        /// </summary>
        /// <param name="prevState">上一次的状态</param>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        void OnEnter(IState prevState, object param1 = null, object param2 = null);

        /// <summary>
        /// 离开这个状态
        /// </summary>
        /// <param name="nextState">下一状态</param>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        void OnLeave(IState nextState, object param1 = null, object param2 = null);

        /// <summary>
        /// 更新
        /// </summary>
        void OnUpdate();

        /// <summary>
        /// 固定更新
        /// </summary>
        void OnFixedUpdate();

        /// <summary>
        /// 延迟更新
        /// </summary>
        void OnLateUpdate();
    }
}