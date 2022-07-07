/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace FutureCore
{
    public class TwoKeyDictionary<T1, T2, T3> : Dictionary<T1, Dictionary<T2, T3>>
    {
        public new Dictionary<T2, T3> this[T1 key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Add(key, new Dictionary<T2, T3>());
                }

                Dictionary<T2, T3> returnObj = null;
                TryGetValue(key, out returnObj);

                return returnObj;
            }
        }

        public bool ContainsKey(T1 key1, T2 key2)
        {
            Dictionary<T2, T3> returnObj = null;
            TryGetValue(key1, out returnObj);

            if (returnObj == null)
            {
                return false;
            }

            return returnObj.ContainsKey(key2);
        }
    }
}