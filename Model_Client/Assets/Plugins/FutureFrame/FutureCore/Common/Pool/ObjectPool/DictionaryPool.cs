/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace FutureCore
{
    public static class DictionaryPool<K, V>
    {
        private static readonly ObjectPool<Dictionary<K, V>> s_DictionaryPool = new ObjectPool<Dictionary<K, V>>(null, Clear);

        private static void Clear(Dictionary<K, V> dic)
        {
            dic.Clear();
        }

        public static Dictionary<K, V> Get()
        {
            return s_DictionaryPool.Get();
        }

        public static void Release(Dictionary<K, V> toReleaseDic)
        {
            s_DictionaryPool.Release(toReleaseDic);
        }
    }
}
