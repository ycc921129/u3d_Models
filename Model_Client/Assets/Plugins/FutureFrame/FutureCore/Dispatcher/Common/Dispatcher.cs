/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;

namespace FutureCore
{
    public class Dispatcher<Msg> : IDisposable
    {
        private Dictionary<Msg, List<Action<object>>> m_msgPriorityDic = new Dictionary<Msg, List<Action<object>>>();
        private Dictionary<Msg, List<Action<object>>> m_msgDic = new Dictionary<Msg, List<Action<object>>>();
        private Dictionary<Msg, List<Action<object>>> m_msgOnceDic = new Dictionary<Msg, List<Action<object>>>();

        public void AddPriorityListener(Msg msgId, Action<object> listener)
        {
            if (m_msgPriorityDic.ContainsKey(msgId))
            {
                m_msgPriorityDic[msgId].Add(listener);
            }
            else
            {
                List<Action<object>> list = ListPool<Action<object>>.Get();
                list.Add(listener);
                m_msgPriorityDic.Add(msgId, list);
            }
        }

        public void AddListener(Msg msgId, Action<object> listener)
        {
            if (m_msgDic.ContainsKey(msgId))
            {
                m_msgDic[msgId].Add(listener);
            }
            else
            {
                List<Action<object>> list = ListPool<Action<object>>.Get();
                list.Add(listener);
                m_msgDic.Add(msgId, list);
            }
        }

        public void AddOnceListener(Msg msgId, Action<object> listener)
        {
            if (m_msgOnceDic.ContainsKey(msgId))
            {
                m_msgOnceDic[msgId].Add(listener);
            }
            else
            {
                List<Action<object>> list = ListPool<Action<object>>.Get();
                list.Add(listener);
                m_msgOnceDic.Add(msgId, list);
            }
        }

        private bool ContainsListener(Msg msgId, Action<object> listener, Dictionary<Msg, List<Action<object>>> msgDict)
        {
            if (msgDict.ContainsKey(msgId))
            {
                List<Action<object>> list = msgDict[msgId];
                return list.Contains(listener);
            }
            return false;
        }

        private bool ContainsPriorityListener(Msg msgId, Action<object> listener)
        {
            return ContainsListener(msgId, listener, m_msgPriorityDic);
        }

        private bool ContainsListener(Msg msgId, Action<object> listener)
        {
            return ContainsListener(msgId, listener, m_msgDic);
        }

        private bool ContainsOnceListener(Msg msgId, Action<object> listener)
        {
            return ContainsListener(msgId, listener, m_msgOnceDic);
        }

        public void RemovePriorityListener(Msg msgId, Action<object> listener)
        {
            if (m_msgPriorityDic.ContainsKey(msgId))
            {
                List<Action<object>> list = m_msgPriorityDic[msgId];
                if (list.Contains(listener))
                {
                    list.Remove(listener);
                    if (list.Count == 0)
                    {
                        ListPool<Action<object>>.Release(list);
                        m_msgPriorityDic.Remove(msgId);
                    }
                }
            }
        }

        public void RemoveListener(Msg msgId, Action<object> listener)
        {
            if (m_msgDic.ContainsKey(msgId))
            {
                List<Action<object>> list = m_msgDic[msgId];
                if (list.Contains(listener))
                {
                    list.Remove(listener);
                    if (list.Count == 0)
                    {
                        ListPool<Action<object>>.Release(list);
                        m_msgDic.Remove(msgId);
                    }
                }
            }
        }

        public void RemoveOnceListener(Msg msgId, Action<object> listener)
        {
            if (m_msgOnceDic.ContainsKey(msgId))
            {
                List<Action<object>> list = m_msgOnceDic[msgId];
                if (list.Contains(listener))
                {
                    list.Remove(listener);
                    if (list.Count == 0)
                    {
                        ListPool<Action<object>>.Release(list);
                        m_msgOnceDic.Remove(msgId);
                    }
                }
            }
        }

        public void Dispatch(Msg msgId, object param = null)
        {
            InvokeMethods(m_msgPriorityDic, msgId, param);
            InvokeMethods(m_msgDic, msgId, param);
            InvokeMethods(m_msgOnceDic, msgId, param);

            if (m_msgOnceDic.ContainsKey(msgId))
            {
                ListPool<Action<object>>.Release(m_msgOnceDic[msgId]);
                m_msgOnceDic.Remove(msgId);
            }
        }

        private void InvokeMethods(Dictionary<Msg, List<Action<object>>> msgDict, Msg msgId, object param)
        {
            if (!msgDict.ContainsKey(msgId)) return;

            List<Action<object>> rawList = msgDict[msgId];
            int funcCount = rawList.Count;

            if (funcCount == 1)
            {
                Action<object> onEvent = rawList[0];
                onEvent(param);
                return;
            }

            List<Action<object>> invokeFuncs = ListPool<Action<object>>.Get();
            invokeFuncs.AddRange(rawList);
            for (int i = 0; i < funcCount; i++)
            {
                try
                {
                    Action<object> onEvent = invokeFuncs[i];
                    onEvent(param);
                }
                catch (Exception e)
                {
                    LogUtil.LogError(e);
                }
            }
            ListPool<Action<object>>.Release(invokeFuncs);
        }

        public void Clear()
        {
            m_msgPriorityDic.Clear();
            m_msgDic.Clear();
            m_msgOnceDic.Clear();
        }

        public void Dispose()
        {
            m_msgPriorityDic.Clear();
            m_msgDic.Clear();
            m_msgOnceDic.Clear();
            m_msgPriorityDic = null;
            m_msgDic = null;
            m_msgOnceDic = null;
        }
    }
}