/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public class PreferencesDispatcher<T> : BaseDispatcher<PreferencesDispatcher<T>, string, ChangeValue<T>>
    {
        public ChangeValue<T> changeValue;

        public PreferencesDispatcher()
        {
            changeValue = new ChangeValue<T>();
        }
    }
}