/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public class ChangeValue<T>
    {
        public T oldValue;
        public T newValue;

        public ChangeValue()
        {
        }

        public ChangeValue(T oldValue, T newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }
    }
}