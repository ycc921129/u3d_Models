/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.17
*/

using System;
using System.Collections.Generic;

namespace FutureCore
{
    public abstract class BaseDispatcher<T, Msg, Param> : IDisposable
        where T : class, new()
        where Param : class
    {
        private static T m_instance;
        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new T();
                }
                return m_instance;
            }
        }

        private Dictionary<Msg, List<Action<Param>>> m_msgPriorityDict = new Dictionary<Msg, List<Action<Param>>>();
        private Dictionary<Msg, List<Action<Param>>> m_msgDict = new Dictionary<Msg, List<Action<Param>>>();
        private Dictionary<Msg, List<Action<Param>>> m_msgFinallyDict = new Dictionary<Msg, List<Action<Param>>>();
        private Dictionary<Msg, List<Action<Param>>> m_msgOnceDict = new Dictionary<Msg, List<Action<Param>>>();

        public void AddPriorityListener(Msg msgId, Action<Param> listener)
        {
            if (m_msgPriorityDict.ContainsKey(msgId))
            {
                m_msgPriorityDict[msgId].Add(listener);
            }
            else
            {
                List<Action<Param>> list = ListPool<Action<Param>>.Get();
                list.Add(listener);
                m_msgPriorityDict.Add(msgId, list);
            }
        }

        public void AddListener(Msg msgId, Action<Param> listener)
        {
            if (m_msgDict.ContainsKey(msgId))
            {
                m_msgDict[msgId].Add(listener);
            }
            else
            {
                List<Action<Param>> list = ListPool<Action<Param>>.Get();
                list.Add(listener);
                m_msgDict.Add(msgId, list);
            }
        }

        public void AddFinallyListener(Msg msgId, Action<Param> listener)
        {
            if (m_msgFinallyDict.ContainsKey(msgId))
            {
                m_msgFinallyDict[msgId].Add(listener);
            }
            else
            {
                List<Action<Param>> list = ListPool<Action<Param>>.Get();
                list.Add(listener);
                m_msgFinallyDict.Add(msgId, list);
            }
        }

        public void AddOnceListener(Msg msgId, Action<Param> listener)
        {
            if (m_msgOnceDict.ContainsKey(msgId))
            {
                m_msgOnceDict[msgId].Add(listener);
            }
            else
            {
                List<Action<Param>> list = ListPool<Action<Param>>.Get();
                list.Add(listener);
                m_msgOnceDict.Add(msgId, list);
            }
        }

        private bool ContainsListener(Msg msgId, Action<Param> listener, Dictionary<Msg, List<Action<Param>>> msgDict)
        {
            if (msgDict.ContainsKey(msgId))
            {
                List<Action<Param>> list = msgDict[msgId];
                return list.Contains(listener);
            }
            return false;
        }

        private bool ContainsPriorityListener(Msg msgId, Action<Param> listener)
        {
            return ContainsListener(msgId, listener, m_msgPriorityDict);
        }

        private bool ContainsListener(Msg msgId, Action<Param> listener)
        {
            return ContainsListener(msgId, listener, m_msgDict);
        }

        private bool ContainsFinallyListener(Msg msgId, Action<Param> listener)
        {
            return ContainsListener(msgId, listener, m_msgFinallyDict);
        }

        private bool ContainsOnceListener(Msg msgId, Action<Param> listener)
        {
            return ContainsListener(msgId, listener, m_msgOnceDict);
        }

        public void RemovePriorityListener(Msg msgId, Action<Param> listener)
        {
            if (m_msgPriorityDict.ContainsKey(msgId))
            {
                List<Action<Param>> list = m_msgPriorityDict[msgId];
                if (list.Contains(listener))
                {
                    list.Remove(listener);
                    if (list.Count == 0)
                    {
                        ListPool<Action<Param>>.Release(list);
                        m_msgPriorityDict.Remove(msgId);
                    }
                }
            }
        }

        public void RemoveListener(Msg msgId, Action<Param> listener)
        {
            if (m_msgDict.ContainsKey(msgId))
            {
                List<Action<Param>> list = m_msgDict[msgId];
                if (list.Contains(listener))
                {
                    list.Remove(listener);
                    if (list.Count == 0)
                    {
                        ListPool<Action<Param>>.Release(list);
                        m_msgDict.Remove(msgId);
                    }
                }
            }
        }

        public void RemoveFinallyListener(Msg msgId, Action<Param> listener)
        {
            if (m_msgFinallyDict.ContainsKey(msgId))
            {
                List<Action<Param>> list = m_msgFinallyDict[msgId];
                if (list.Contains(listener))
                {
                    list.Remove(listener);
                    if (list.Count == 0)
                    {
                        ListPool<Action<Param>>.Release(list);
                        m_msgFinallyDict.Remove(msgId);
                    }
                }
            }
        }

        public void RemoveOnceListener(Msg msgId, Action<Param> listener)
        {
            if (m_msgOnceDict.ContainsKey(msgId))
            {
                List<Action<Param>> list = m_msgOnceDict[msgId];
                if (list.Contains(listener))
                {
                    list.Remove(listener);
                    if (list.Count == 0)
                    {
                        ListPool<Action<Param>>.Release(list);
                        m_msgOnceDict.Remove(msgId);
                    }
                }
            }
        }

        public void Dispatch(Msg msgId, Param param = null)
        {
            InvokeMethods(m_msgPriorityDict, msgId, param);
            InvokeMethods(m_msgDict, msgId, param);
            InvokeMethods(m_msgFinallyDict, msgId, param);
            InvokeMethods(m_msgOnceDict, msgId, param);

            if (m_msgOnceDict.ContainsKey(msgId))
            {
                ListPool<Action<Param>>.Release(m_msgOnceDict[msgId]);
                m_msgOnceDict.Remove(msgId);
            }
        }
          
        private void InvokeMethods(Dictionary<Msg, List<Action<Param>>> msgDict, Msg msgId, Param param)
        {
            if (!msgDict.ContainsKey(msgId)) return;

            List<Action<Param>> rawList = msgDict[msgId];
            int funcCount = rawList.Count;
            if (funcCount == 1)
            {
                Action<Param> onEvent = rawList[0];
                onEvent(param);
                return;
            }

            List<Action<Param>> invokeFuncs = ListPool<Action<Param>>.Get();
            invokeFuncs.AddRange(rawList);
            for (int i = 0; i < funcCount; i++)
            {
                try
                {
                    Action<Param> onEvent = invokeFuncs[i];
                    onEvent(param);
                }
                catch (Exception e)
                {
                    LogUtil.LogError(e);
                }
            }
            ListPool<Action<Param>>.Release(invokeFuncs); 
        }

        public void Clear()
        {
            m_msgPriorityDict.Clear();
            m_msgDict.Clear();
            m_msgFinallyDict.Clear();
            m_msgOnceDict.Clear();
        }

        public void Dispose()
        {
            m_instance = null;

            m_msgPriorityDict.Clear();
            m_msgDict.Clear();
            m_msgFinallyDict.Clear();
            m_msgOnceDict.Clear();
            m_msgPriorityDict = null;
            m_msgDict = null;
            m_msgFinallyDict = null;
            m_msgOnceDict = null;
        }
    }
}