/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;

namespace FutureCore
{
    public class CompatableValues<K, V>
    {
        private Dictionary<K, V> mDict = new Dictionary<K, V>();
        private Func<K, V> onNewValueObjFunc;

        public CompatableValues(Func<K, V> onNewValueObjFunc)
        {
            this.onNewValueObjFunc = onNewValueObjFunc;
        }

        public V Get(K keyObj)
        {
            V valueObj;
            if (!mDict.TryGetValue(keyObj, out valueObj))
            {
                valueObj = onNewValueObjFunc(keyObj);
                mDict.Add(keyObj, valueObj);
            }
            return valueObj;
        }

        public Dictionary<K, V> GetCompatable()
        {
            return mDict;
        }

        public void Dispose()
        {
            mDict.Clear();
            mDict = null;
            onNewValueObjFunc = null;
        }
    }
}