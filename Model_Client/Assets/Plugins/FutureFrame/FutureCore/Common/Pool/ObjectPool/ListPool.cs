/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace FutureCore
{
    public static class ListPool<T>
    {
        private static readonly ObjectPool<List<T>> s_ListPool = new ObjectPool<List<T>>(null, Clear);

        private static void Clear(List<T> l)
        {
            l.Clear();
        }

        public static List<T> Get()
        {
            return s_ListPool.Get();
        }

        public static void Release(List<T> toRelease)
        {
            s_ListPool.Release(toRelease);
        }
    }
}