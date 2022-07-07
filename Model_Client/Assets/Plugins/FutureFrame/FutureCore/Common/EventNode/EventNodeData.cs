/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    /// <summary>
    /// 发送注册事件
    /// </summary>
    public class EventNodeData
    {
        public bool isBreak = false;

        /// <summary>
        /// 时间名称或者ID
        /// </summary>
        public string name;

        public EventNodeData(string name)
        {
            this.name = name;
        }

        public void Break()
        {
            isBreak = true;
        }
    }

    /// <summary>
    /// 泛型的事件类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventNodeData<T> : EventNodeData
    {
        public T value;

        public EventNodeData(string name, T v = default) : base(name)
        {
            value = v;
        }
    }
}