/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

//#define UtilTest

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beebyte.Obfuscator;
using Newtonsoft.Json.Linq;
using ProjectApp;
#if UNITY_EDITOR
using FutureEditor;
#endif

namespace FutureCore
{
    /// <summary>
    /// WebSocket代理管理器
    /// </summary>
    public sealed class WSNetProxyMgr : BaseMgr<WSNetProxyMgr>
    {
        public const string PrefsKey = "WSNetProxyMgr_UrlsWeight";

        /// <summary>
        /// 当前连接使用的配置
        /// </summary>
        private ConnectionConfig[] configs = null;

        /// <summary>
        /// 最近的连接数据
        /// </summary>
        private Dictionary<string, ConnectionData> datas = null;

        /// <summary>
        /// 是否是断线了
        /// </summary>
        private bool isDropped = false;

        /// <summary>
        /// App启动
        /// </summary>
        private bool isFirst = true;

        /// <summary>
        /// 并发连接的间隔
        /// </summary>
        private const float intervalSimultanouslyTime = 5 * 60;

        /// <summary>
        /// 上次连接的时间
        /// </summary>
        private float lastSimultaneouslyTime = -intervalSimultanouslyTime;

        /// <summary>
        /// 连接行为类
        /// </summary>
        private ConnectionBehavior connectionBehavior;

        /// <summary>
        /// 缓存数据的条目数
        /// </summary>
        private const int CacheDataCnt = 10;

        //连接成功, 参数不为空，表示连接成功, 参数为空表示连接失败
        private Action<WebSocketProxy> connectionSuccess;

        /// <summary>
        /// 连接WebSocket
        /// </summary>
        public void Connection(string[] urls, Action<WebSocketProxy> connectionSuccessFunc)
        {
            if (configs == null)
            {
                configs = new ConnectionConfig[urls.Length];
                for (int i = 0; i < urls.Length; i++)
                {
                    configs[i] = new ConnectionConfig(urls[i], 0);
                }
            }
            if (connectionSuccess == null)
            {
                connectionSuccess = connectionSuccessFunc;
            }
            Connection(configs);
        }

        /// <summary>
        /// 连接WebSocket
        /// </summary>
        /// <param name="configs">连接配置</param>
        /// <param name="Reset"></param>
        public void Connection(ConnectionConfig[] configs, bool Reset = false)
        {
            currWebSocket = null;

            Stop();

            this.configs = configs;

            //重置排名数据
            if (Reset)
            {
#if UtilTest
                LogUtil.Log("重置连接数据！");
#endif
                PlayerPrefs.DeleteKey(PrefsKey);
                datas = null;
            }

            if (datas == null)
            {
                LoadConnectionData();
            }

            //判断是轮训还是并发
            /*
             * 并发的条件
             * 1.APP启动
             * 2.掉线了 且 5分钟之内没有并发连接过
             * */
            bool isSimultaneously = isFirst || (isDropped && (Time.time - lastSimultaneouslyTime > intervalSimultanouslyTime));

#if UtilTest
            LogUtil.Log("datas:" + (datas == null) + " isDropped:" + isDropped
                + " connItervalTime:" + (Time.time - lastSimultaneouslyTime) + " isSimultaneously:" + isSimultaneously);
#endif

            if (datas == null)
                datas = new Dictionary<string, ConnectionData>();

            //isSimultaneously = false;

            //并发连接
            if (isSimultaneously)
            {
#if UtilTest
                LogUtil.Log("开始并发连接!");
#endif
                connectionBehavior = new SimultaneouslyConnection(this);
                lastSimultaneouslyTime = Time.time;
            }
            else
            {
#if UtilTest
                LogUtil.Log("开始轮询连接!");
#endif
                connectionBehavior = new RotationConnection(this);
            }

            isFirst = false;
            connectionBehavior.Connection();
        }

        /// <summary>
        /// 停止连接
        /// </summary>
        public void Stop()
        {
            if (connectionBehavior != null)
            {
                connectionBehavior.Stop();
                connectionBehavior = null;
            }
        }

        private ConnectionData AddData(string url, int weight)
        {
            if (!datas.ContainsKey(url))
                datas.Add(url, new ConnectionData() { sort = new List<int>(), url = url });
            var conndata = datas[url];
            conndata.sort.Add(weight);
            if (conndata.sort.Count > CacheDataCnt)
            {
                conndata.idx++;
                conndata.sort.RemoveAt(0);
            }
            return conndata;
        }

        /// <summary>
        /// 连接成功事件
        /// </summary>
        /// <param name="connBehavior"></param>
        /// <param name="webSocket"></param>
        private void OnSuccess(ConnectionBehavior connBehavior, WebSocketProxy webSocket)
        {
            if (connectionBehavior == connBehavior)
            {
                isDropped = false;
                currWebSocket = webSocket;
                currWebSocket.norMalCloseEvent += WebNormalClose;
                currWebSocket.OnCloseEvent += WebSocketClose;

                if (WSNetMgr.Instance.IsDisplayLog())
                {
                    LogUtil.Log("[WSNetProxyMgr]触发连接成功的事件! " + currWebSocket.url);
                }

                if (connectionSuccess != null)
                    connectionSuccess(currWebSocket);
            }
        }

        /// <summary>
        /// 连接失败事件
        /// </summary>
        /// <param name="connBehavior"></param>
        private void OnFail(ConnectionBehavior connBehavior)
        {
            if (connectionBehavior == connBehavior)
            {
                LogUtil.Log("[WSNetProxyMgr]触发连接失败的事件!");
                if (connectionSuccess != null)
                    connectionSuccess(null);
            }
        }

        /// <summary>
        /// 当前的连接
        /// </summary>
        private WebSocketProxy currWebSocket;

        private void WebSocketClose(WebSocket webSocket, ushort closeCode, string message)
        {
            LogUtil.LogFormat("[WSNetProxyMgr]连接被关闭:{0} closeCode:{1} message:{2}", (currWebSocket == webSocket), closeCode, message);
            if (currWebSocket == webSocket)
            {
                isDropped = true;
                LogUtil.Log("[WSNetProxyMgr]掉线降权重!" + (webSocket as WebSocketProxy).url + "," + message);
                SetWeight(-1, (webSocket as WebSocketProxy).url, -3);
            }
        }

        /// <summary>
        /// 正常关闭
        /// </summary>
        /// <param name="webSocket"></param>
        private void WebNormalClose(WebSocket webSocket)
        {
            LogUtil.Log("[WSNetProxyMgr]连接正常关闭:" + (currWebSocket == webSocket));
            if (currWebSocket == webSocket)
            {
                currWebSocket = null;
            }
        }

        /// <summary>
        /// 存储的连接数据
        /// </summary>
        public void LogDatas()
        {
            if (datas == null)
            {
                LogUtil.Log("[WSNetProxyMgr]datas is null!");
                return;
            }

            foreach (var dataItem in datas)
            {
                if (dataItem.Value != null && dataItem.Value.sort != null)
                {
                    LogUtil.Log(string.Format("[WSNetProxyMgr]Url:{0} StartIdx:{1} Count:{2}", dataItem.Value.url, dataItem.Value.idx, dataItem.Value.sort.Count));
                    int len = dataItem.Value.sort.Count;
                    string sortValue = string.Empty;
                    for (int i = 0; i < len; i++)
                    {
                        sortValue += " " + dataItem.Value.sort[i];
                    }
                    LogUtil.Log("[WSNetProxyMgr]SortValue:" + sortValue);
                }
            }
        }

        /// <summary>
        /// 加载连接数据
        /// </summary>
        private void LoadConnectionData()
        {
            try
            {
                string connectDataStr = PlayerPrefs.GetString(PrefsKey);

                if (string.IsNullOrEmpty(connectDataStr))
                    return;

                datas = SerializeUtil.ToObject<Dictionary<string, ConnectionData>>(connectDataStr);

                List<string> delKey = new List<string>();
                foreach (var dataItem in datas)
                {
                    if (dataItem.Value == null || dataItem.Value.sort == null || string.IsNullOrEmpty(dataItem.Value.url))
                    {
                        delKey.Add(dataItem.Key);
                    }
                }
                if (delKey.Count > 0)
                {
                    for (int i = 0; i < delKey.Count; i++)
                    {
                        datas.Remove(delKey[i]);
                    }
                }

#if UtilTest
                LogUtil.Log("[WSNetProxyMgr]加载历史的连接数据!");
                LogDatas();
#endif
            }
            catch (Exception e)
            {
                LogUtil.LogError("[WSNetProxyMgr]加载连接数据出错:" + e.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private WebSocket GetWebSocket(int idx, string url)
        {
            WebSocketProxy webSocket = new WebSocketProxy(idx, url);
            return webSocket;
        }

        /// <summary>
        /// 设置权重
        /// </summary>
        /// <param name="url"></param>
        /// <param name="weight"></param>
        private void SetWeight(int idx, string url, int weight)
        {
            if (!datas.ContainsKey(url))
                return;

            if (idx < 0)
            {
                LogUtil.Log("[WSNetProxyMgr]新增分值:" + url + "," + weight);
                //新增分值
                AddData(url, weight);
            }
            else
            {
                //修改分值
                idx = idx - datas[url].idx - 1;
                if (idx >= 0 && idx < datas[url].sort.Count)
                {
                    if (datas[url].sort[idx] == weight)
                        return;
                    datas[url].sort[idx] = weight;

                    LogUtil.Log("[WSNetProxyMgr]设置分值,url:" + url + ",weight:" + weight + "," + datas[url].sort[idx] + "idx:" + idx + ",count:" + datas[url].sort.Count);
                }
                else
                {
                    LogUtil.Log("[WSNetProxyMgr]设置分值竟然不在范围内 idx:" + idx);
                }
            }
            string jsondata = SerializeUtil.ToJson(datas);
            LogUtil.Log("[WSNetProxyMgr]保存:" + jsondata);
            PlayerPrefs.SetString(PrefsKey, jsondata);
        }


#if UNITY_EDITOR && UtilTest

        [SerializeField]
        [InspectorButton_("连接测试")]
        private bool TestConn_TestBtn;

        private void TestConn()
        {
            List<ConnectionConfig> tempconfigs = new List<ConnectionConfig>()
            {
                new ConnectionConfig("ws://127.0.0.1:7780/Echo", 2),
                new ConnectionConfig("ws://127.0.0.1:7781/Echo", 2),
                new ConnectionConfig("ws://127.0.0.1:7782/Echo", 2),
                new ConnectionConfig("ws://127.0.0.1:7783/Echo", 2),
            };
            this.Connection(tempconfigs.ToArray());
        }

        [SerializeField]
        [InspectorButton_("连接数据")]
        private bool ReadConnData_TestBtn;

        private void ReadConnData()
        {
            string jsondata = PlayerPrefs.GetString(PrefsKey);
            if (jsondata != null)
            {
                LogUtil.Log("得到数据:" + jsondata);
                Dictionary<string, ConnectionData> tempdata = SerializeUtil.ToObject<Dictionary<string, ConnectionData>>(jsondata);

                foreach (var item in tempdata.Values)
                {
                    Debug.Log("idx:" + item.idx + " url:" + item.url + " count:" + item.sort.Count);
                    int len = item.sort.Count;
                    string logdata = string.Empty;
                    for (int i = 0; i < len; i++)
                    {
                        logdata += item.sort[i] + " ";
                    }
                    Debug.Log("sort:" + logdata);
                }
            }
        }

        [SerializeField]
        [InspectorButton_("清除数据")]
        private bool ClearConnData_TestBtn;

        private void ClearConnData()
        {
            PlayerPrefs.DeleteKey(PrefsKey);
        }

        [SerializeField]
        [InspectorButton_("停止处理")]
        private bool StopConn_TestBtn;

        private void StopConn()
        {
            Stop();
        }

        //private WebSocket currwebSocket;

        [SerializeField]
        [InspectorButton_("断开当前")]
        private bool DisConn_TestBtn;

        private void DisConn()
        {
            currWebSocket?.NormalClose();
        }

#endif

        #region 结构定义
        [Skip]
        public class ConnectionData
        {
            /// <summary>
            /// 连接的域名
            /// </summary>
            public string url;

            /// <summary>
            /// Sort第一个下标的连接次数
            /// </summary>
            public int idx;

            /// <summary>
            /// 分值,key为第几次 value 表示权值
            /// </summary>
            public List<int> sort;

            /// <summary>
            /// 得到分值
            /// </summary>
            /// <returns></returns>
            public int GetWeight()
            {
                int weight = 0;
                for (int i = 0; i < sort.Count; i++)
                {
                    weight += sort[i];
                }
                return weight;
            }
        }

        /// <summary>
        /// 连接行为
        /// </summary>
        public abstract class ConnectionBehavior
        {
            public WSNetProxyMgr wSNetProxyMgr;

            public Dictionary<WebSocket, bool> websocketIsErrorOrClose = new Dictionary<WebSocket, bool>();

            public ConnectionConfig[] configs
            {
                get
                {
                    return wSNetProxyMgr.configs;
                }
            }

            public Dictionary<string, ConnectionData> datas
            {
                get
                {
                    return wSNetProxyMgr.datas;
                }
            }

            public ConnectionBehavior(WSNetProxyMgr wSNetProxyMgr)
            {
                this.wSNetProxyMgr = wSNetProxyMgr;
            }

            public abstract void Connection();

            public abstract void Stop();

            protected void BindEvent(WebSocket webSocket)
            {
                webSocket.OnOpenEvent += OnSocketSuccess;
                webSocket.OnErrorEvent += OnError;
                webSocket.OnCloseEvent += OnClose;
                BindPush(webSocket);
            }

            protected void BindPush(WebSocket webSocket)
            {
                webSocket.push(RouteConst.Route_info, "S2C_InvokeInfo");
                webSocket.push(RouteConst.Route_exchange, "S2C_InvokeExchange");
                webSocket.push(RouteConst.Route_gamelist, "S2C_InvokeGameList");
                webSocket.push(RouteConst.Route_tasklist, "S2C_InvokeTaskList");
                webSocket.push(RouteConst.Route_updateCoin, "S2C_InvokeUpdateCoin");
                webSocket.push(RouteConst.Route_updateStatis, "S2C_InvokeUpdateStatis");
                webSocket.push(RouteConst.Route_subscribe, "S2C_InvokeSubscribe");
                webSocket.push(RouteConst.Route_iap, "S2C_InvokeIAP");
            }

            protected void CloseRemoveEvent(WebSocket webSocket)
            {
                webSocket.ClearEvent();
            }

            protected virtual void OnError(WebSocket webSocket, string e)
            {
                WebSocketProxy socket = webSocket as WebSocketProxy;
                string socketurl = string.Empty;
                if (socket != null)
                    socketurl = socket.url;

                if (WSNetMgr.Instance.IsDisplayLog())
                {
                    LogUtil.Log("[WSNetProxyMgr]OnError: 连接异常" + socketurl + "," + e);
                }

                CloseRemoveEvent(webSocket);
            }

            /// <summary>
            /// 连接成功
            /// </summary>
            /// <param name="webSocket"></param>
            protected virtual void OnSocketSuccess(WebSocket webSocket)
            {
                CloseRemoveEvent(webSocket);
            }

            /// <summary>
            /// 连接关闭
            /// </summary>
            /// <param name="webSocket"></param>
            protected virtual void OnClose(WebSocket webSocket, ushort closeCode, string message)
            {
                WebSocketProxy socket = webSocket as WebSocketProxy;
                string socketurl = string.Empty;
                if (socket != null)
                    socketurl = socket.url;

                if (WSNetMgr.Instance.IsDisplayLog())
                {
                    LogUtil.Log("[WSNetProxyMgr]OnClose: 连接关闭" + socketurl + "," + closeCode.ToString() + "," + message);
                }

                CloseRemoveEvent(webSocket);
            }
        }

        /// <summary>
        /// 轮询连接
        /// </summary>
        public class RotationConnection : ConnectionBehavior
        {
            /// <summary>
            /// 是否取消 
            /// </summary>
            private bool isCancell = false;

            /// <summary>
            ///
            /// </summary>
            //private readonly CancellationTokenSource token = new CancellationTokenSource();

            /// <summary>
            /// 要连接的对象
            /// </summary>
            private List<string> connectionDatas = new List<string>();

            /// <summary>
            /// 是否已经连接成功
            /// </summary>
            private bool isSuccess;

            public RotationConnection(WSNetProxyMgr wSNetProxyMgr) : base(wSNetProxyMgr)
            {
            }

            public override void Connection()
            {
                int len = configs.Length;
                for (int i = 0; i < len; i++)
                {
                    //有权重记录
                    if (datas.ContainsKey(configs[i].url))
                    {
                        //有权重记录 取记录的值
                        configs[i].weight = datas[configs[i].url].GetWeight();
#if UtilTest
                        LogUtil.Log("存在数据，重算权重:" + configs[i].weight);
#endif
                    }
                }

                for (int i = 0; i < len; i++)
                {
                    int maxidx = i;
                    for (int j = i + 1; j < len; j++)
                    {
                        if (configs[j].weight > configs[maxidx].weight)
                        {
                            maxidx = j;
                        }
                    }
                    if (maxidx != i)
                    {
                        var temp = configs[i];
                        configs[i] = configs[maxidx];
                        configs[maxidx] = temp;
                    }
                    connectionDatas.Add(configs[i].url);
#if UtilTest
                    LogUtil.Log("权重排序idx:" + i + ",url:" + configs[i].url + ",weight:" + configs[i].weight);
#endif
                }
                CoroutineMgr.Instance.StartCoroutine(Begin());


            }

            /// <summary>
            /// 开始处理
            /// </summary>  
            private IEnumerator Begin()
            {
                List<WebSocket> sockets = new List<WebSocket>();
                int idx = 0;
                while (true)
                {
                    if (idx != 0)
                        yield return new WaitForSeconds(2);
                    //await Task.Delay(2000);
                    if (isCancell)
                        break;
                    if (idx >= connectionDatas.Count)
                        break;
                    if (isSuccess)
                        break;
#if UtilTest
                    LogUtil.Log("开始连接:" + connectionDatas[idx]);
#endif
                    var webSocket = wSNetProxyMgr.GetWebSocket(idx, connectionDatas[idx]);
                    BindEvent(webSocket);
                    sockets.Add(webSocket);
                    webSocket.StartConnect();

#if UtilTest
                        LogUtil.Log("轮询连接:" + idx + ",url:" + connectionDatas[idx]);
#endif
                    idx++;
                }

                while (true)
                {
                    //有一个成功就跳出
                    if (isSuccess)
                        break;
                    int len = sockets.Count;
                    for (int i = len - 1; i >= 0; i--)
                    {
                        //任务完成就删除
                        if (sockets[i].GetState() != WebSocket.WebSocketStatus.Connecting)
                            sockets.RemoveAt(i);
                    }
                    len = sockets.Count;
                    if (len <= 0)
                        break;
                    yield return new WaitForSeconds(1);
                }

                if (!isSuccess)
                {
                    LogUtil.Log("[WSNetProxyMgr]轮询连接错误: 全部连接失败!");
                    if (!isCancell)
                    {
                        wSNetProxyMgr.OnFail(this);
                    }
                }
            }

            protected override void OnSocketSuccess(WebSocket webSocket)
            {
                var webSocketProxy = webSocket as WebSocketProxy;

                base.OnSocketSuccess(webSocket);
#if UtilTest
                LogUtil.Log("轮询连接 - 有人连成功了!");
#endif
                if (!isSuccess)
                {
#if UtilTest
                    LogUtil.Log("轮询连接 连接成功!" + webSocketProxy.url);
#endif
                    /*不是第一个连接,获得得分*/
                    if (webSocketProxy.idx != 0)
                        wSNetProxyMgr.SetWeight(-1, webSocketProxy.url, webSocketProxy.idx * 2 + (webSocketProxy.idx - 1));

                    isSuccess = true;
                    if (!isCancell)
                    {
                        wSNetProxyMgr.OnSuccess(this, webSocketProxy);
                    }
                    else
                    {
                        webSocketProxy.Close();
                    }
                }
                else
                {
                    webSocketProxy.Close();
                }
                //连接成功就不需要再去连接了
                isCancell = true;
            }

            protected override void OnClose(WebSocket webSocket, ushort closeCode, string message)
            {
                base.OnClose(webSocket, closeCode, message);
            }

            protected override void OnError(WebSocket webSocket, string e)
            {
                base.OnError(webSocket, e);
            }

            public override void Stop()
            {
                isCancell = true;
            }
        }

        /// <summary>
        /// 并发连接
        /// </summary>
        public class SimultaneouslyConnection : ConnectionBehavior
        {
            /// <summary>
            /// 剩余连接的数量
            /// </summary>
            private int allCnt;

            /// <summary>
            /// 连接成功的数量
            /// </summary>
            private int conneCnt = 0;

            /// <summary>
            /// 是否已经停止
            /// </summary>
            private bool isStop = false;

            private int firstIdx = 0;

            /// <summary>
            /// 一个连接上的
            /// </summary>
            private string firstUrl = null;

            private Dictionary<WebSocket, int> dic = new Dictionary<WebSocket, int>();

            public SimultaneouslyConnection(WSNetProxyMgr wSNetProxyMgr) : base(wSNetProxyMgr)
            {
            }

            /// <summary>
            /// 开始连接
            /// </summary>
            public override void Connection()
            {
                int len = configs.Length;
                allCnt = len;
                //Interlocked.Exchange(ref allCnt, len);
                for (int i = 0; i < len; i++)
                {
                    var webSocket = wSNetProxyMgr.GetWebSocket(i, configs[i].url);
                    var conndata = wSNetProxyMgr.AddData(configs[i].url, 0);
                    dic.Add(webSocket, conndata.idx + conndata.sort.Count);

                    if (WSNetMgr.Instance.IsDisplayLog())
                    {
                        LogUtil.Log(string.Format("[WSNetProxyMgr]并发连接 url:{0} time:{1}", configs[i].url, Time.time));
                    }

                    BindEvent(webSocket);

                    webSocket.StartConnect();
                }
            }

            private void CheckFail(WebSocket webSocket)
            {
                if (--allCnt < 1)
                {
                    LogUtil.Log("[WSNetProxyMgr]全部连接失败!");
                    if (!isStop)
                        //全部连接失败s
                        wSNetProxyMgr.OnFail(this);
                }
            }

            protected override void OnError(WebSocket webSocket, string e)
            {
                CheckFail(webSocket);
                base.OnError(webSocket, e);
            }

            protected override void OnClose(WebSocket webSocket, ushort closeCode, string message)
            {
                CheckFail(webSocket);
                base.OnClose(webSocket, closeCode, message);
            }

            protected override void OnSocketSuccess(WebSocket webSocket)
            {
                WebSocketProxy webSocletProxy = webSocket as WebSocketProxy;
                base.OnSocketSuccess(webSocket);

                if (WSNetMgr.Instance.IsDisplayLog())
                {
                    LogUtil.Log("[WSNetProxyMgr]并发 连接成功!" + webSocletProxy.url);
                }

                int idx = -1;
                if (dic.ContainsKey(webSocket))
                {
                    idx = dic[webSocket];
                    dic.Remove(webSocket);
                }

                if (++conneCnt > 1)
                {
#if UtilTest
                    LogUtil.Log("不是第一个连接成功的!" + webSocletProxy.url);
#endif
                    //在这之前有别人连接过了
                    webSocletProxy.Close();
                    if (idx >= 0)
                    {
                        wSNetProxyMgr.SetWeight(idx, webSocletProxy.url, 1);
                        if (firstUrl != null)
                            wSNetProxyMgr.SetWeight(firstIdx, firstUrl, 2);

                        foreach (var key in dic.Keys)
                        {
                            key.CancelConnection();
                        }
                        dic.Clear();
                    }
                }
                else
                {
                    firstIdx = idx;
                    firstUrl = webSocletProxy.url;

                    if (WSNetMgr.Instance.IsDisplayLog())
                    {
                        LogUtil.Log("[WSNetProxyMgr]第一个连接成功的!" + webSocletProxy.url);
                    }

                    if (!isStop)
                        //第一个连接成功的
                        wSNetProxyMgr.OnSuccess(this, webSocletProxy);
                    else
                        webSocletProxy.Close();
                    wSNetProxyMgr.SetWeight(idx, webSocletProxy.url, 3);
                }
            }

            public override void Stop()
            {
#if UtilTest
                LogUtil.Log("停止处理!");
#endif
                isStop = true;
            }
        }

        /// <summary>
        /// 连接配置
        /// </summary>
        public class ConnectionConfig : ICloneable
        {
            /// <summary>
            /// 连接地址
            /// </summary>
            public string url;

            /// <summary>
            /// 越小权重越高
            /// </summary>
            public int weight;

            public ConnectionConfig(string url, int weight)
            {
                this.url = url;
                this.weight = weight;
            }

            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }
        #endregion
    }
}