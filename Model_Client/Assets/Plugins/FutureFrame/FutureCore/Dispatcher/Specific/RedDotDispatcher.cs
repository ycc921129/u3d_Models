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
    public class RedDotData
    {
        public uint msgId;
        public bool isRed = false;
        public RedDotData parentRoot = null;
        public List<RedDotData> subRoots = null;

        public RedDotData(uint msgId, bool isRed)
        {
            this.msgId = msgId;
            this.isRed = isRed;
        }

        public void SetParentRoot(RedDotData parentRoot)
        {
            this.parentRoot = parentRoot;
            if (this.parentRoot.subRoots == null)
            {
                this.parentRoot.subRoots = new List<RedDotData>();
            }
            if (!parentRoot.subRoots.Contains(this))
            {
                parentRoot.subRoots.Add(this);
            }
        }

        public void Update(bool isRed)
        {
            if (this.isRed == isRed) return;

            if (!isRed)
            {
                if (subRoots != null)
                {
                    bool subIsRed = false;
                    foreach (RedDotData subRoot in subRoots)
                    {
                        if (subRoot.isRed)
                        {
                            subIsRed = true;
                            break;
                        }
                    }
                    if (!subIsRed)
                    {
                        foreach (RedDotData subRoot in subRoots)
                        {
                            bool recursionRedDotResult = RecursionRedDotSubResult(subRoot);
                            if (recursionRedDotResult)
                            {
                                subIsRed = true;
                                break;
                            }
                        }
                    }
                    isRed = subIsRed;
                }
            }
            if (this.isRed == isRed) return;

            this.isRed = isRed;
            RedDotDispatcher.Instance.Dispatch(this);

            if (parentRoot != null)
            {
                parentRoot.Update(isRed);
            }
            else
            {
                RedDotDispatcher.Instance.WriteLocalData();
            }
        }

        private bool RecursionRedDotSubResult(RedDotData redDot)
        {
            if (redDot.subRoots == null) return false;

            foreach (RedDotData subItem in redDot.subRoots)
            {
                if (subItem.isRed)
                {
                    return true;
                }
                bool subIsRed = RecursionRedDotSubResult(subItem);
                if (subIsRed)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class RedDotDispatcher : Singleton<RedDotDispatcher>
    {
        private List<uint> m_beforeRedDotRecords = new List<uint>();
        private Dictionary<uint, RedDotData> m_redDotDict = new Dictionary<uint, RedDotData>();
        private Dictionary<uint, List<Action<RedDotData>>> m_redDotMsgDict = new Dictionary<uint, List<Action<RedDotData>>>();

        public RedDotDispatcher()
        {
            if (FCApplication.IsAppQuit) return;

            ReadLocalData();
        }

        private void ReadLocalData()
        {
            if (PrefsUtil.HasKey(PrefsKeyConst.RedDotDispatcher_beforeRedDotRecords))
            {
                m_beforeRedDotRecords = PrefsUtil.ReadObject<List<uint>>(PrefsKeyConst.RedDotDispatcher_beforeRedDotRecords) as List<uint>;
                foreach (uint msgId in m_beforeRedDotRecords)
                {
                    if (m_redDotDict.ContainsKey(msgId))
                        m_redDotDict[msgId].isRed = true;
                    else
                    {
                        RedDotData redDot = new RedDotData(msgId, true);
                        m_redDotDict.Add(redDot.msgId, redDot);
                    }
                }
            }
        }

        public void WriteLocalData()
        {
            m_beforeRedDotRecords.Clear();
            foreach (RedDotData redDot in m_redDotDict.Values)
            {
                if (redDot.isRed)
                {
                    m_beforeRedDotRecords.Add(redDot.msgId);
                }
            }
            PrefsUtil.WriteObject(PrefsKeyConst.RedDotDispatcher_beforeRedDotRecords, m_beforeRedDotRecords);
        }

        public void AddListener(uint msgId, Action<RedDotData> listener)
        {
            if (m_redDotMsgDict.ContainsKey(msgId))
            {
                m_redDotMsgDict[msgId].Add(listener);
            }
            else
            {
                List<Action<RedDotData>> list = ListPool<Action<RedDotData>>.Get();
                list.Add(listener);
                m_redDotMsgDict.Add(msgId, list);
            }
        }

        public void RemoveListener(uint msgId, Action<RedDotData> listener)
        {
            if (m_redDotMsgDict.ContainsKey(msgId))
            {
                List<Action<RedDotData>> list = m_redDotMsgDict[msgId];
                m_redDotMsgDict[msgId].Remove(listener);
                if (list.Count == 0)
                {
                    ListPool<Action<RedDotData>>.Release(list);
                    m_redDotMsgDict.Remove(msgId);
                }
            }
        }

        public void Dispatch(RedDotData redDot)
        {
            uint msgId = redDot.msgId;
            if (!m_redDotMsgDict.ContainsKey(msgId)) return;

            List<Action<RedDotData>> rawList = m_redDotMsgDict[msgId];
            int funcCount = rawList.Count;

            if (funcCount == 1)
            {
                Action<RedDotData> action = rawList[0];
                action(redDot);
                return;
            }

            List<Action<RedDotData>> invokeFuncs = ListPool<Action<RedDotData>>.Get();
            invokeFuncs.AddRange(rawList);
            for (int i = 0; i < funcCount; i++)
            {
                try
                {
                    Action<RedDotData> action = invokeFuncs[i];
                    action(redDot);
                }
                catch (Exception e)
                {
                    LogUtil.LogError(e);
                }
            }
            ListPool<Action<RedDotData>>.Release(invokeFuncs);
        }

        public void Dispatch(uint msgId)
        {
            if (m_redDotDict.ContainsKey(msgId))
            {
                RedDotData redDotData = m_redDotDict[msgId];
                Dispatch(redDotData);
            }
        }

        public RedDotData GetRedDot(uint msgId)
        {
            if (!m_redDotDict.ContainsKey(msgId))
            {
                RedDotData redDot = new RedDotData(msgId, false);
                m_redDotDict.Add(redDot.msgId, redDot);
            }
            return m_redDotDict[msgId];
        }

        public void Clear()
        {
            m_redDotMsgDict.Clear();
        }

        public override void Dispose()
        {
            base.Dispose();
            m_beforeRedDotRecords.Clear();
            m_redDotDict.Clear();
            m_redDotMsgDict.Clear();

            m_beforeRedDotRecords = null;
            m_redDotDict = null;
            m_redDotMsgDict = null;
        }
    }
}