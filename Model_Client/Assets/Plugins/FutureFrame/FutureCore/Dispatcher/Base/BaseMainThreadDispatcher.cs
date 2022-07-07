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
    public class BaseMainThreadDispatcher<T, Msg, Param> : SingletonMono<T>
        where T : SingletonMono<T>
        where Param : class
    {
        private class MainThreadMsgClass
        {
            public Msg currMsgId;
            public Param currParam;
        }

        private Queue<MainThreadMsgClass> m_msgQueue = new Queue<MainThreadMsgClass>();
        private Dictionary<Msg, List<Action<Param>>> m_msgPriorityDict = new Dictionary<Msg, List<Action<Param>>>();
        private Dictionary<Msg, List<Action<Param>>> m_msgDict = new Dictionary<Msg, List<Action<Param>>>();
        private Dictionary<Msg, List<Action<Param>>> m_msgOnceDict = new Dictionary<Msg, List<Action<Param>>>();

        private object m_queueLock = new object();

        private void Update()
        {
            if (m_msgQueue.Count <= 0) return;

            while (m_msgQueue.Count > 0)
            {
                MainThreadMsgClass msg;
                lock (m_queueLock)
                {
                    msg = m_msgQueue.Dequeue();
                }
                AutoDispatch(msg.currMsgId, msg.currParam);
            }
        }

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
            if (!m_msgPriorityDict.ContainsKey(msgId)
             && !m_msgDict.ContainsKey(msgId)
             && !m_msgOnceDict.ContainsKey(msgId))
                return;

            MainThreadMsgClass msg = new MainThreadMsgClass
            {
                currMsgId = msgId,
                currParam = param,
            };
            lock (m_queueLock)
            {
                m_msgQueue.Enqueue(msg);
            }
        }

        private void AutoDispatch(Msg msgId, Param param)
        {
            InvokeMethods(m_msgPriorityDict, msgId, param);
            InvokeMethods(m_msgDict, msgId, param);
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
            lock (m_queueLock)
            {
                m_msgQueue.Clear();
            }
            m_msgPriorityDict.Clear();
            m_msgDict.Clear();
            m_msgOnceDict.Clear();
        }

        protected override string ParentRootName
        {
            get
            {
                return AppObjConst.DispatcherGoName;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            m_msgPriorityDict.Clear();
            m_msgDict.Clear();
            m_msgOnceDict.Clear();
            m_msgPriorityDict = null;
            m_msgDict = null;
            m_msgOnceDict = null;
        }
    }
}