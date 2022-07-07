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
    public class WSNetDispatcher : SingletonMono<WSNetDispatcher>
    {
        private object queueLock = new object();
        private int maxHandleCount = 10;

        private Queue<BaseS2CJsonProto> protoMsgQueue = new Queue<BaseS2CJsonProto>();
        private BaseS2CJsonProto[] protoMsgDispatchArray;
        private int currNeedHandleCount;
        private List<string> logIgnoreList;

        private Dictionary<string, List<Action<BaseS2CJsonProto>>> protoPriorityMsgDict = new Dictionary<string, List<Action<BaseS2CJsonProto>>>();
        private Dictionary<string, List<Action<BaseS2CJsonProto>>> protoMsgDict = new Dictionary<string, List<Action<BaseS2CJsonProto>>>();
        private Dictionary<string, List<Action<BaseS2CJsonProto>>> protoMsgOnceDict = new Dictionary<string, List<Action<BaseS2CJsonProto>>>();

        public void Init(int maxHandleCount, List<string> logIgnoreList)
        {
            this.maxHandleCount = maxHandleCount;
            protoMsgDispatchArray = new BaseS2CJsonProto[maxHandleCount];
            this.logIgnoreList = logIgnoreList;
        }

        private void Update()
        {
            if (protoMsgQueue.Count == 0) return;

            GetProtoMsgDispatchArray();
            if (currNeedHandleCount == 0) return;
            for (int i = 0; i < currNeedHandleCount; i++)
            {
                try
                {
                    BaseS2CJsonProto protoMsg = protoMsgDispatchArray[i];
                    AutoDispatch(protoMsg);
                }
                catch (Exception e)
                {
                    LogUtil.LogError("[WSNetDispatcher]AutoDispatch Exception: " + e.ToString());
                }
                finally
                {
                    protoMsgDispatchArray[i] = null;
                }
            }
        }

        private void GetProtoMsgDispatchArray()
        {
            currNeedHandleCount = 0;
            for (int i = 0; i < maxHandleCount; i++)
            {
                BaseS2CJsonProto protoMsg = null;
                lock (queueLock)
                {
                    protoMsg = protoMsgQueue.Dequeue();
                }
                protoMsgDispatchArray[i] = protoMsg;
                currNeedHandleCount++;
                if (protoMsgQueue.Count == 0)
                {
                    return;
                }
            }
        }

        public void Dispatch(BaseS2CJsonProto protoMsg)
        {
            lock (queueLock)
            {
                protoMsgQueue.Enqueue(protoMsg);
            }
        }
          
        private void AutoDispatch(BaseS2CJsonProto protoMsg)
        {
            if (WSNetMgr.Instance.IsDisplayLog())
            {
                if (!string.IsNullOrEmpty(protoMsg.err))
                {
                    string logInfo = string.Format("[WSNetDispatcher]Response: {0} Error: {1}", protoMsg.type, protoMsg.err);
                    LogUtil.LogErrorFormat("{0}\n{1}", logInfo, protoMsg.GetRawJson());
                }
                else
                {
                    if (!logIgnoreList.Contains(protoMsg.type))
                    {
                        string logInfo = string.Format("[WSNetDispatcher]Response: {0}", protoMsg.type);
                        LogUtil.LogFormat("{0}\n{1}", logInfo, protoMsg.GetRawJson());
                    }
                }
            }

            string msgId = protoMsg.type;
            InvokeMethods(protoPriorityMsgDict, msgId, protoMsg);
            InvokeMethods(protoMsgDict, msgId, protoMsg);
            InvokeMethods(protoMsgOnceDict, msgId, protoMsg);

            if (protoMsgOnceDict.ContainsKey(msgId))
            {
                ListPool<Action<BaseS2CJsonProto>>.Release(protoMsgOnceDict[msgId]);
                protoMsgOnceDict.Remove(msgId);
            }
        }

        private void InvokeMethods(Dictionary<string, List<Action<BaseS2CJsonProto>>> msgDict, string msgId, BaseS2CJsonProto protoMsg)
        {
            if (!msgDict.ContainsKey(msgId)) return;

            List<Action<BaseS2CJsonProto>> rawList = msgDict[msgId];
            int funcCount = rawList.Count;

            if (funcCount == 1)
            {
                Action<BaseS2CJsonProto> onEvent = rawList[0];
                onEvent(protoMsg);
                return;
            }

            List<Action<BaseS2CJsonProto>> invokeFuncs = ListPool<Action<BaseS2CJsonProto>>.Get();
            invokeFuncs.AddRange(rawList);
            for (int i = 0; i < funcCount; i++)
            {
                try
                {
                    Action<BaseS2CJsonProto> onEvent = invokeFuncs[i];
                    onEvent(protoMsg);
                }
                catch (Exception e)
                {
                    LogUtil.LogError("[WSNetDispatcher]InvokeMethods Exception: " + e.ToString());
                }
            }
            ListPool<Action<BaseS2CJsonProto>>.Release(invokeFuncs);
        }

        public void ClearProtoMsg()
        {
            lock (queueLock)
            {
                protoMsgQueue.Clear();
            }
            LogUtil.LogFormat("[WSNetDispatcher]ClearProtoMsg");
        }

        public void AddPriorityListener(string msgId, Action<BaseS2CJsonProto> listener)
        {
            if (protoPriorityMsgDict.ContainsKey(msgId))
            {
                protoPriorityMsgDict[msgId].Add(listener);
            }
            else
            {
                List<Action<BaseS2CJsonProto>> list = ListPool<Action<BaseS2CJsonProto>>.Get();
                list.Add(listener);
                protoPriorityMsgDict.Add(msgId, list);
            }
        }

        public void AddListener(string msgId, Action<BaseS2CJsonProto> listener)
        {
            if (protoMsgDict.ContainsKey(msgId))
            {
                protoMsgDict[msgId].Add(listener);
            }
            else
            {
                List<Action<BaseS2CJsonProto>> list = ListPool<Action<BaseS2CJsonProto>>.Get();
                list.Add(listener);
                protoMsgDict.Add(msgId, list);
            }
        }

        public void AddOnceListener(string msgId, Action<BaseS2CJsonProto> listener)
        {
            if (protoMsgOnceDict.ContainsKey(msgId))
            {
                protoMsgOnceDict[msgId].Add(listener);
            }
            else
            {
                List<Action<BaseS2CJsonProto>> list = ListPool<Action<BaseS2CJsonProto>>.Get();
                list.Add(listener);
                protoMsgOnceDict.Add(msgId, list);
            }
        }

        public void RemovePriorityListener(string msgId, Action<BaseS2CJsonProto> listener)
        {
            if (protoPriorityMsgDict.ContainsKey(msgId))
            {
                List<Action<BaseS2CJsonProto>> list = protoPriorityMsgDict[msgId];
                list.Remove(listener);
                if (list.Count == 0)
                {
                    ListPool<Action<BaseS2CJsonProto>>.Release(list);
                    protoPriorityMsgDict.Remove(msgId);
                }
            }
        }

        public void RemoveListener(string msgId, Action<BaseS2CJsonProto> listener)
        {
            if (protoMsgDict.ContainsKey(msgId))
            {
                List<Action<BaseS2CJsonProto>> list = protoMsgDict[msgId];
                list.Remove(listener);
                if (list.Count == 0)
                {
                    ListPool<Action<BaseS2CJsonProto>>.Release(list);
                    protoMsgDict.Remove(msgId);
                }
            }
        }

        public void RemoveOnceListener(string msgId, Action<BaseS2CJsonProto> listener)
        {
            if (protoMsgOnceDict.ContainsKey(msgId))
            {
                List<Action<BaseS2CJsonProto>> list = protoMsgOnceDict[msgId];
                list.Remove(listener);
                if (list.Count == 0)
                {
                    ListPool<Action<BaseS2CJsonProto>>.Release(list);
                    protoMsgOnceDict.Remove(msgId);
                }
            }
        }

        public void Clear()
        {
            lock (queueLock)
            {
                protoMsgQueue.Clear();
            }
            protoPriorityMsgDict.Clear();
            protoMsgDict.Clear();
            protoMsgOnceDict.Clear();
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

            Clear();
            protoMsgQueue = null;
            protoPriorityMsgDict = null;
            protoMsgDispatchArray = null;
            protoMsgDict = null;
            protoMsgOnceDict = null;
        }
    }
}