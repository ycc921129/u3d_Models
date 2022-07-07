/****************************************************************************
* ScriptType: Ö÷¿ò¼Ü
* ÇëÎðÐÞ¸Ä!!!
****************************************************************************/

using System;
using System.Collections.Generic;

namespace FutureCore
{
    public static class SortUtil
    {
        private class SortItem
        {
            public object data;
            public int weight;
        }

        private class SortItemComparer : IComparer<SortItem>
        {
            public int Compare(SortItem x, SortItem y)
            {
                if (x.weight > y.weight)
                    return 1;
                if (x.weight < y.weight)
                    return -1;
                return 0;
            }
        }

        private static readonly IComparer<SortItem> sortItemComparer = new SortItemComparer();
        private static List<SortItem> sortItemList = new List<SortItem>();

        public static void Sort<T>(this List<T> list, Func<T, int> getWeightFunc)
        {
            if (list == null || list.Count <= 0)
                return;

            int cnt = list.Count;
            for (int i = 0; i < cnt; i++)
            {
                if (sortItemList.Count <= i)
                    sortItemList.Add(new SortItem());
                sortItemList[i].weight = getWeightFunc(list[i]);
                sortItemList[i].data = list[i];
            }

            sortItemList.Sort(0, cnt, sortItemComparer);
            for (int i = 0; i < cnt; i++)
            {
                list[i] = (T)sortItemList[i].data;
            }
        }

        public static void Sort<T>(this T[] array, Func<T, int> getWeightFunc)
        {
            if (array == null || array.Length <= 1)
                return;

            int cnt = array.Length;
            for (int i = 0; i < cnt; i++)
            {
                if (sortItemList.Count <= i)
                    sortItemList.Add(new SortItem());
                sortItemList[i].weight = getWeightFunc(array[i]);
                sortItemList[i].data = array[i];
            }

            sortItemList.Sort(0, cnt, sortItemComparer);
            for (int i = 0; i < cnt; i++)
            {
                array[i] = (T)sortItemList[i].data;
            }
        }
    }
}