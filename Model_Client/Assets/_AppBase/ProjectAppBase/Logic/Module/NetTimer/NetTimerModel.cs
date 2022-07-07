/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using ProjectApp.Data;
using System.Collections.Generic;

namespace ProjectApp
{
    public class NetTimerModel : BaseModel
    {
        #region 生命周期

        protected override void OnInit()
        {
        }

        protected override void OnDispose()
        {
        }

        protected override void OnReset()
        {
        }

        #endregion 生命周期

        #region 读取数据

        protected override void OnReadData()
        {
        }

        #endregion 读取数据

        /// <summary>
        /// 本地的定时器 如果一个定时器游戏关闭就清除的话，可以只存在这，不存Preferences
        /// </summary>
        private Dictionary<string, NetTimerData> localTimers = new Dictionary<string, NetTimerData>();

        /// <summary>
        /// 网络定时器
        /// </summary>
        private Dictionary<string, NetTimerData> netTimers = new Dictionary<string, NetTimerData>();

        public int LocalTimerCnt
        {
            get
            {
                return localTimers.Count;
            }
        }

        public int NetTimerCnt
        {
            get
            {
                return netTimers.Count;
            }
        }

        public int AllTimerCnt
        {
            get
            {
                return LocalTimerCnt + NetTimerCnt;
            }
        }

        /// <summary>
        /// 数据初始化_定时器进行分类处理，方便本地定时器的逻辑运算
        /// </summary>
        public void InitData()
        {
            long nowServerCurrTimeStamp = DateTimeMgr.Instance.ServerTickTimestamp;
            foreach (var item in PreferencesMgr.Instance.NetTimers.Values)
            {
                if (item.isLocal)
                {
                    //如果离线也要运行定时器
                    if (item.outlineRun)
                    {
                        long diffTime = nowServerCurrTimeStamp - item.startTimeStamp;
                        if (diffTime > item.timeout)
                            item.elapsed = item.timeout + 1;
                        else
                            item.elapsed = item.elapsed + (int)diffTime;
                    }
                    localTimers.Add(item.id, item);
                }
            }
        }

        /// <summary>
        /// 添加定时器
        /// </summary>
        /// <param name="data">定时器数据</param>
        /// <param name="isSave">如果是本地定时器有效 是否保存到Preferences</param>
        public bool AddTimer(NetTimerData data, bool isSave = true)
        {
            if (data == null)
            {
                LogUtil.LogError("[NetTimerModel] AddTimer NetTimerData is null");
                return false;
            }

            if (!data.isLocal)
            {
                if (netTimers.ContainsKey(data.id))
                    return false;
                netTimers.Add(data.id, data);
            }
            else
            {
                if (localTimers.ContainsKey(data.id))
                    return false;
                localTimers.Add(data.id, data);
                if (isSave)
                {
                    PreferencesMgr.Instance.NetTimers.Add(data.id, data);
                    PreferencesMgr.Instance.SaveNetTimers();
                    PreferencesMgr.Instance.ImmediateSendSave();
                }
            }
            LogUtil.Log("添加定时器:" + data.id);
            return true;
        }

        /// <summary>
        /// 更换定时器数据- 只会更换网络定时器
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ReplaceTimer(NetTimerData data)
        {
            if (!netTimers.ContainsKey(data.id))
                return false;
            netTimers[data.id] = data;
            return true;
        }

        /// <summary>
        /// 网络定时器转换为本地定时器
        /// </summary>
        /// <param name="timerId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public bool MoveToLocalTimer(string timerId)
        {
            if (!netTimers.ContainsKey(timerId))
                return false;
            NetTimerData netTimerData = netTimers[timerId];
            netTimerData.isLocal = true;

            netTimers.Remove(timerId);
            localTimers.Add(timerId, netTimerData);
            return true;
        }

        /// <summary>
        /// 移除定时器
        /// </summary>
        public NetTimerData RemoveTimer(string id)
        {
            NetTimerData result = null;

            result = RemoveTimer(id, true);
            if (result != null)
                return result;

            result = RemoveTimer(id, false);
            if (result != null)
                return result;

            return null;
        }

        /// <summary>
        /// 移除定时器
        /// </summary>
        /// <param name="id">定时器id</param>
        /// <param name="isLocal">是否是本地定时器</param>
        public NetTimerData RemoveTimer(string id, bool isLocal)
        {
            Dictionary<string, NetTimerData> dic = isLocal ? localTimers : netTimers;
            NetTimerData result = null;
            if (dic.TryGetValue(id, out result))
            {
                dic.Remove(id);
                if (PreferencesMgr.Instance.NetTimers.Remove(id))
                {
                    PreferencesMgr.Instance.SaveNetTimers();
                    PreferencesMgr.Instance.ImmediateSendSave();
                }
            }
            return result;
        }

        /// <summary>
        /// 查找一个定时器
        /// </summary>
        public NetTimerData FindTimer(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            NetTimerData result = null;

            result = FindTimer(id, true);
            if (result != null)
                return result;

            result = FindTimer(id, false);
            if (result != null)
                return result;

            return null;
        }

        /// <summary>
        /// 查找一个定时器
        /// </summary>
        public NetTimerData FindTimer(string id, bool isLocal)
        {
            Dictionary<string, NetTimerData> dic = isLocal ? localTimers : netTimers;
            NetTimerData result = null;
            if (dic.TryGetValue(id, out result))
                return result;
            return null;
        }

        public IEnumerable<NetTimerData> AllTimerData()
        {
            foreach (var item in localTimers.Values)
            {
                yield return item;
            }
            foreach (var item in netTimers.Values)
            {
                yield return item;
            }
        }

        public IEnumerable<NetTimerData> LocalTimers
        {
            get
            {
                return localTimers.Values;
            }
        }

        public IEnumerable<NetTimerData> NetTimers
        {
            get
            {
                return netTimers.Values;
            }
        }
    }
}