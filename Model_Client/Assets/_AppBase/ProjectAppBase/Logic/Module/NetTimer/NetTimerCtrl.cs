/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using ProjectApp.Data;
using ProjectApp.Protocol;
using System;
using System.Collections.Generic;

namespace ProjectApp
{
    public class NetTimerCtrl : BaseCtrl
    {
        public static NetTimerCtrl Instance { get; private set; }

        private NetTimerModel model;

        private string orderIdPreName = "NetTimer";

        /// <summary>
        /// 最小的定时器间隔, 超过这个值才会向服务器请求定时器
        /// </summary>
        private int minTimeOut = 60;

        /// <summary>
        /// 最大定时器数量, 网络定时器最大的数量
        /// </summary>
        private int maxCount = 30;

        /// <summary>
        /// 定时器是否已经同步网络-表示网络定时器是否初始化完成
        /// </summary>
        public bool isSyncNet
        {
            get;
            private set;
        }

        /// <summary>
        /// 定时器任务
        /// </summary>
        private TimerTask timeTask;

        #region 生命周期

        protected override void OnInit()
        {
            Instance = this;
            model = moduleMgr.GetModel(ModelConst.NetTimerModel) as NetTimerModel;
        }

        protected override void OnDispose()
        {
            Instance = null;
        }

        #endregion 生命周期

        #region 消息

        protected override void AddListener()
        {
            ctrlDispatcher.AddPriorityListener(CtrlMsg.Preferences_InitComplete, OnSyncTimer);
            wsNetDispatcher.AddListener(WSNetMsg.S2C_timer_add, OnProto_NetTimerAdd);
            wsNetDispatcher.AddListener(WSNetMsg.S2C_timer_del, OnProto_NetTimerDel);
            wsNetDispatcher.AddListener(WSNetMsg.S2C_timer_out, OnProto_NetTimerOut);
            wsNetDispatcher.AddListener(WSNetMsg.S2C_timer_sync, OnProto_NetTimerSync);
        }

        protected override void RemoveListener()
        {
            ctrlDispatcher.RemovePriorityListener(CtrlMsg.Preferences_InitComplete, OnSyncTimer);
            wsNetDispatcher.RemoveListener(WSNetMsg.S2C_timer_add, OnProto_NetTimerAdd);
            wsNetDispatcher.RemoveListener(WSNetMsg.S2C_timer_del, OnProto_NetTimerDel);
            wsNetDispatcher.RemoveListener(WSNetMsg.S2C_timer_out, OnProto_NetTimerOut);
            wsNetDispatcher.RemoveListener(WSNetMsg.S2C_timer_sync, OnProto_NetTimerSync);
        }

        #endregion 消息

        private void OnSyncTimer(object obj)
        {
            if (timeTask == null)
            {
                //去同步定时器
                LogUtil.Log("[NetTimerCtrl]OnSyncTimer");

                //同步网络定时器
                //TODO 服务器计时器
                //SendProto_TimerSync(); 

                timeTask = TimerUtil.General.AddLoopTimer(1, OnLocalLoopTimer);
            }
        }

        private NetTimerData CreateNetTimerData(bool isLocal, bool outlineRun, int timeout, TimerCallBackData data)
        {
            long serverTime = DateTimeMgr.Instance.ServerTickTimestamp;
            if (Math.Abs(DateTimeMgr.Instance.GetCurrTimestamp() - DateTimeMgr.Instance.ServerTickTimestamp) < 30)
                serverTime = DateTimeMgr.Instance.GetCurrTimestamp();

            return new NetTimerData()
            {
                id = OrderIdUtil.GenerateTimeId(orderIdPreName),
                isLocal = isLocal,
                outlineRun = outlineRun,
                startTimeStamp = serverTime,
                timeout = timeout,
                elapsed = 0,
                data = data,
            };
        }

        /// <summary>  
        /// 添加定时器
        /// </summary>
        /// <param name="timeout">多少秒后通知</param>
        /// <param name="data">定时器数据</param>
        /// <param name="outlineRun">离线是否需要运行（也就是离线状态是否要算时间）, false 表示这个定时器一定是本地运算/param>
        /// <param name="islocal">是否本地运行，false 如果在服务器允许的情况下，在服务器中运行</param>
        /// <returns>返回定时器数据， 如果返回空表示客户端数据异常</returns>
        public NetTimerData AddTimer(int timeout, TimerCallBackData data, bool outlineRun = true)
        {
            bool islocal = false;
            //先判断是网络还是本地运算
            bool isNet = isSyncNet && !islocal && outlineRun && timeout >= minTimeOut && model.NetTimerCnt < maxCount;
            NetTimerData ntd = CreateNetTimerData(!isNet, outlineRun, timeout, data);

            if (isNet)
            {
                LogUtil.LogFormat("[NetTimerCtrl] Net Timer Add {0}", ntd);
                if (!model.AddTimer(ntd))
                {
                    LogUtil.LogError("[NetTimerCtrl] Timer Error, id repeat!");
                    return null;
                }
                SendProto_TimerAdd(ntd);
            }
            else
            {
                LogUtil.LogFormat("[NetTimerCtrl] Timer Add {0}", ntd);
                if (!model.AddTimer(ntd))
                {
                    LogUtil.LogError("[NetTimerCtrl] Timer Error, id repeat!");
                    return null;
                }
            }
            return ntd;
        }

        /// <summary>
        /// 添加一个定时器，当玩家离线时，定时器被自动删除
        /// </summary>
        public NetTimerData AddTimerOutLineDel(int timeout, TimerCallBackData data)
        {
            NetTimerData ntd = CreateNetTimerData(true, false, timeout, data);
            if (!model.AddTimer(ntd, false))
            {
                LogUtil.LogError("[NetTimerCtrl] id repeat!");
                return null;
            }

            return ntd;
        }

        /// <summary>
        /// 删除一个定时器
        /// </summary>
        public void RemoveTimer(string id)
        {
            var ntd = model.RemoveTimer(id);
            if (ntd != null && !ntd.isLocal)
            {
                //LogUtil.Log("删除定时器:" + id);
                SendProto_TimerDel(id);
            }
        }

        /// <summary>
        /// 编辑定时器
        /// </summary>
        /// <param name="id">定时器id</param>
        /// <param name="timeOutInc">定时器超时时间</param>
        /// <returns>true 表示修改成功 false 表示修改失败</returns>
        public bool EditorTimer(string id, int timeOutInc)
        {
            if (timeOutInc == 0)
                return true;

            //修改本地定时器数据, 发包给服务器
            NetTimerData timerData = FindTimer(id);
            if (timerData == null)
                return false;

            //定时器已经到期
            if (timerData.elapsed >= timerData.timeout)
                return false;

            timerData.timeout += timeOutInc;
            if (timerData.timeout <= minTimeOut)
            {
                if (timerData.timeout < 1)
                    timerData.timeout = 1;

                //定时器从网络定时器移动到
                if (!timerData.isLocal)
                {
                    model.MoveToLocalTimer(timerData.id);
                    SendProto_TimerDel(timerData.id);
                }
            }

            if (!timerData.isLocal)
                //发包通知服务器
                SendProto_TimerAdd(timerData);

            PreferencesMgr.Instance.SaveNetTimers();
            return true;
        }

        public NetTimerData FindTimer(string id)
        {
            return model.FindTimer(id);
        }

        #region 发包处理

        /// <summary>
        /// 定时器请求完成回调
        /// </summary>
        private readonly List<Action> overActions = new List<Action>();

        private C2S_timer_sync timer_sync = new C2S_timer_sync();
        private C2S_timer_sync_data timer_sync_data = new C2S_timer_sync_data();

        /// <summary>
        /// 发送同步定时器
        /// </summary>
        public bool SendProto_TimerSync(List<string> syncTimerIds = null, Action overAction = null)
        {
            timer_sync.data = timer_sync_data;
            timer_sync_data.ids = syncTimerIds;
            if (WSNetMgr.Instance.Send(timer_sync))
            {
                overActions.Add(overAction);
                LogUtil.Log("[NetTimeCtrl]SendProto_TimerSync");
                return true;
            }
            return false;
        }

        private C2S_timer_add m_c2s_Timer_Add = new C2S_timer_add();
        private C2S_timer_add_data m_c2s_Timer_Add_data = new C2S_timer_add_data();

        private void SendProto_TimerAdd(NetTimerData timerData)
        {
            C2S_timer_add cta = m_c2s_Timer_Add;
            cta.data = m_c2s_Timer_Add_data;
            cta.data.id = timerData.id;
            cta.data.timeout = timerData.timeout;
            cta.data.data = timerData.data;
            WSNetMgr.Instance.Send(cta);
            LogUtil.Log("[NetTimerCtrl]SendProto_TimerAdd");
        }

        private List<string> m_timerDelIds = new List<string>();

        private void SendProto_TimerDel(string id)
        {
            m_timerDelIds.Clear();
            m_timerDelIds.Add(id);
            SendProto_TimerDel(m_timerDelIds);
        }

        private C2S_timer_del m_c2s_timer_del = new C2S_timer_del();
        private C2S_timer_del_data m_c2s_timer_del_data = new C2S_timer_del_data();

        private void SendProto_TimerDel(List<string> ids)
        {
            C2S_timer_del ctd = m_c2s_timer_del;
            ctd.data = m_c2s_timer_del_data;
            ctd.data.ids = ids;
            WSNetMgr.Instance.Send(ctd);
            LogUtil.Log("[NetTimerCtrl]SendProto_TimeDel");
        }

        #endregion 发包处理

        #region 收包处理

        private void OnProto_NetTimerAdd(BaseS2CJsonProto data)
        {
            S2C_timer_add sta = data as S2C_timer_add;
            if (sta == null || sta.data == null)
            {
                LogUtil.LogError("[NetTimeCtrl]pack is not SC2_timer_add");
                return;
            }
            LogUtil.LogFormat("[NetTimeCtrl]time add, Current Time Cnt {0}", sta.data.timer_cnt);
        }

        private void OnProto_NetTimerDel(BaseS2CJsonProto data)
        {
            S2C_timer_del std = data as S2C_timer_del;
            if (std == null)
            {
                LogUtil.LogError("[NetTimeCtrl]pack is not S2C_timer_del");
                return;
            }
            if (!string.IsNullOrEmpty(std.err))
            {
                LogUtil.LogError("[NetTimeCtrl] del err:" + std.err);
                return;
            }
            LogUtil.LogFormat("[NetTimeCtrl]time del,Current Time Cnt {0}", std.data.timer_cnt);
        }

        private void OnProto_NetTimerSync(BaseS2CJsonProto data)
        {
            S2C_timer_sync sts = data as S2C_timer_sync;
            if (sts == null)
            {
                LogUtil.LogError("[NetTimeCtrl]pack is not S2C_timer_sync");
                return;
            }
            if (!string.IsNullOrEmpty(data.err))
            {
                LogUtil.Log("[NetTimeCtrl] Sync Err:" + data.err);
                return;
            }

            S2C_timer_sync_data stsd = sts.data;
            minTimeOut = stsd.min_timeout;
            maxCount = stsd.max_count;

            if (sts.data.timers != null)
            {
                int len = sts.data.timers.Count;
                for (int i = 0; i < len; i++)
                {
                    var tdata = sts.data.timers[i];
                    if (tdata == null)
                    {
                        LogUtil.LogError("[NetTimerCtrl] pack error,timers null cnt:" + i);
                        continue;
                    }
                    NetTimerData timerdata = TimeData2NetTimeData(tdata, false, true);
                    //把网络定时器数据添加到本地
                    if (!isSyncNet)
                        model.AddTimer(timerdata);
                    else
                        model.ReplaceTimer(timerdata);
                }
            }

            if (!isSyncNet)
            {
                //本地数据初始化
                model.InitData();

                isSyncNet = true;
                //网络定时器初始化完成
                ctrlDispatcher.Dispatch(CtrlMsg.NetTimer_InitComplete);
            }

            Action syncAction = overActions.Count > 0 ? overActions[0] : null;
            if (overActions.Count > 0)
                overActions.RemoveAt(0);

            LogUtil.Log("[NetTimeCtrl]NetTimer Sync");

            if (syncAction != null)
                syncAction();
        }

        private TimerData m_onProto_NetTimerOut = new TimerData();

        private void OnProto_NetTimerOut(BaseS2CJsonProto data)
        {
            //删除本地的网络定时器存储
            S2C_timer_out sto = data as S2C_timer_out;
            if (sto == null)
            {
                LogUtil.LogError("[NetTimeCtrl]pack data is not S2C_timer_out");
                return;
            }
            model.RemoveTimer(sto.data.id, false);

            m_onProto_NetTimerOut.id = sto.data.id;
            m_onProto_NetTimerOut.timeout = sto.data.timeout;
            m_onProto_NetTimerOut.elapsed = sto.data.timeout;
            m_onProto_NetTimerOut.data = sto.data.data;
            NetTimerComplete(m_onProto_NetTimerOut);
        }

        #endregion 收包处理

        /// <summary>
        /// 定时器完成
        /// </summary>
        /// <param name="timerData">定时器数据</param>
        private void NetTimerComplete(TimerData timerData)
        {
            TimerCallBackData tcd = timerData.data as TimerCallBackData;
            if (tcd == null)
            {
                LogUtil.LogFormat("[NetTimeCtrl] TimeData is not TimeCallBackData, data:{0}", timerData.data);
                return;
            }

            NetTimerDispatcher.Instance.Dispatch(tcd.tag, tcd);
            LogUtil.LogFormat("[NetTimeCtrl] TimerComplete id={0} timeout={1}", timerData.id, timerData.timeout);
        }

        private List<NetTimerData> m_completeTimerList = new List<NetTimerData>();

        private void OnLocalLoopTimer(TimerTask timerTask)
        {
            m_completeTimerList.Clear();
            foreach (NetTimerData item in model.LocalTimers)
            {
                if (++item.elapsed >= item.timeout)
                {
                    m_completeTimerList.Add(item);
                }
            }

            //回调定时器，并删除定时器
            int len = m_completeTimerList.Count;
            for (int i = 0; i < len; i++)
            {
                model.RemoveTimer(m_completeTimerList[i].id, true);
                NetTimerComplete(NetTimeData2TimeData(m_completeTimerList[i]));
            }

            //网络定时器 也更新时间，因为界面可能用来做刷新, 但是不处理回调
            foreach (NetTimerData item in model.NetTimers)
            {
                item.elapsed++;
            }
        }

        private TimerData NetTimeData2TimeData(NetTimerData netTimedata)
        {
            if (netTimedata == null)
                return null;

            return new TimerData()
            {
                id = netTimedata.id,
                timeout = netTimedata.timeout,
                elapsed = netTimedata.elapsed,
                data = netTimedata.data
            };
        }

        private NetTimerData TimeData2NetTimeData(TimerData timedata, bool isLocal, bool outlineRun = true)
        {
            if (timedata == null)
                return null;

            return new NetTimerData()
            {
                id = timedata.id,
                isLocal = isLocal,
                outlineRun = outlineRun,
                timeout = timedata.timeout,
                elapsed = timedata.elapsed,
                data = timedata.data as TimerCallBackData
            };
        }
    }
}