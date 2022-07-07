/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace FutureCore
{
    public interface IDelayItem
    {
        bool IsDestroyed { get; set; }
    }

    public class DelayItem : IDelayItem
    {
        public bool IsDestroyed { get; set; }
    }

    public class DelayList<T> : List<T> where T : IDelayItem
    {
        private bool m_RemoveDirty = false;
        private bool m_AddDirty = false;
        private List<T> delayAddList = new List<T>();

        public void DelayAdd(T item)
        {
            delayAddList.Add(item);
            m_AddDirty = true;
        }

        public void DelayRemove(T item)
        {
            item.IsDestroyed = true;
            m_RemoveDirty = true;
        }

        public void DelayRemoveAt(int index)
        {
            Remove(this[index]);
        }

        public void ApplyDelayCommand()
        {
            if (m_RemoveDirty)
            {
                int count = Count;
                int i = 0, j = 0;
                while (i < count)
                {
                    if (this[i].IsDestroyed)
                    {
                        j = i + 1;
                        while (j < count)
                        {
                            if (!this[j].IsDestroyed)
                            {
                                this[i] = this[j];
                                i++;
                            }
                            j++;
                        }
                        RemoveRange(i, this.Count - i);
                        break;
                    }
                    i++;
                }
                m_RemoveDirty = false;
            }

            if (m_AddDirty)
            {
                AddRange(delayAddList);
                delayAddList.Clear();
                m_AddDirty = false;
            }
        }
    }
}