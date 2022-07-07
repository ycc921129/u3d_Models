/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Pomelo.Client;
using Newtonsoft.Json.Linq;

namespace FutureCore
{
    public enum WSNetState : byte
    {
        // 无
        None = 0,
        // 无网络
        NoNetwork = 1,
        // 连接中
        Connecting = 2,
        // 已连接
        Connected = 3,
        // 已关闭
        Closed = 4,
        // 网络异常
        Exception = 5,

        // 登录中
        Logining = 6,
        // 登录成功
        LoginSuccess = 7,

        // 登录失败 需要延迟登录
        LoginFailed_MustDelay = 8,
        // 发送登录协议错误
        SendLoginError = 9,
        // Preferences解析错误
        PreferencesParseError = 10,
        // 配置表解析错误
        ConfigParseError = 11,
        // 配置表序列化错误
        ConfigSerializeError = 12,
    }

    public sealed class WSNetMgr : BaseMgr<WSNetMgr>
    {
        public const bool IsUseEncrypt = true;
        public const bool IsUseLZ4 = false;
        public const string UseLZ4Suffix = "lz4-skip/";
        public const float DefaultTimeout = 6f;
        public const int MaxProtoHandleCount = 10;

        private Dictionary<string, Type> protoC2STypeDict = new Dictionary<string, Type>();
        private Dictionary<string, Type> protoS2CTypeDict = new Dictionary<string, Type>();
        private List<string> c2sProtoLogIgnoreList = new List<string>();
        private List<string> s2cProtoLogIgnoreList = new List<string>();

        public bool hasFirstConnect;
        public bool isConnecting;
        public bool isConnected;

        private string[] fullUrls;
        private string newestConnectionUrl;
        private WebSocketProxy webSocket;
        private bool isWss;
        private Action connectSucceedFunc;

        private WSNetState m_state = WSNetState.None;
        public WSNetState State
        {
            get
            {
                return m_state;
            }
            set
            {
                m_state = value;
                AppDispatcher.Instance.Dispatch(AppMsg.WebSocketServer_StateUpdate, m_state);
            }
        }

        public override void Init()
        {
            base.Init();
            AddListener();
            WSNetDispatcher.Instance.Init(MaxProtoHandleCount, s2cProtoLogIgnoreList);
        }

        public override void Dispose()
        {
            base.Dispose();
            RemoveListener();

            // 防止关闭应用无法发送消息, 关闭应用时候不关闭网络, 但会关闭监听
            ClearWebSocketEvent();
        }

        private void AddListener()
        {
            ChannelDispatcher.Instance.AddPriorityListener(ChannelRawMsg.NetworkStatusChanged_False, OnNetworkStatusChangedFalse);
        }

        private void RemoveListener()
        {
            ChannelDispatcher.Instance.RemovePriorityListener(ChannelRawMsg.NetworkStatusChanged_False, OnNetworkStatusChangedFalse);
        }

        private void OnNetworkStatusChangedFalse(object param)
        {
            if (!hasFirstConnect) return;

            Close();
        }

        public void RegisterC2SProtoType(string protoKey, Type prtoType)
        {
            if (!protoC2STypeDict.ContainsKey(protoKey))
            {
                protoC2STypeDict.Add(protoKey, prtoType);
            }
        }

        public void RegisterS2CProtoType(string protoKey, Type prtoType)
        {
            if (!protoS2CTypeDict.ContainsKey(protoKey))
            {
                protoS2CTypeDict.Add(protoKey, prtoType);
            }
        }

        public void RegisterC2SProtoLogIgnore(string msgType)
        {
            if (!c2sProtoLogIgnoreList.Contains(msgType))
            {
                c2sProtoLogIgnoreList.Add(msgType);
            }
        }

        public void RegisterS2CProtoLogIgnore(string msgType)
        {
            if (!s2cProtoLogIgnoreList.Contains(msgType))
            {
                s2cProtoLogIgnoreList.Add(msgType);
            }
        }

        public bool IsDisplayLog()
        {
            if (!AppConst.IsDisplayNetProtoLog) return false;

            if (Application.isEditor)
            {
                return true;
            }
            else if (Channel.Current.debug)
            {
                return true;
            }

            return false;
        }

        public bool Connect(Action connectSucceedFunc = null)
        {
            hasFirstConnect = true;

            if (IsDisplayLog())
            {
                LogUtil.LogFormat("[WSNetMgr]ConnectBefore IsNetAvailable:{0} isConnecting:{1} isConnected:{2}",
                    NetConst.IsNetAvailable, isConnecting, isConnected);
            }

            if (!NetConst.IsNetAvailable)
            {
                State = WSNetState.NoNetwork;
                return false;
            }
            if (isConnecting)
            {
                return false;
            }
            if (isConnected || webSocket != null)
            {
                Close();
            }

            State = WSNetState.None;
            this.connectSucceedFunc = connectSucceedFunc;
            WSNetProxyConnect();
            return true;
        }

        private void WSNetProxyConnect()
        {
            if (fullUrls == null)
            {
                isWss = IsAppWssUrl();

                string[] appUrls = App.GetWebSocketUrls();
                int urlCount = appUrls.Length;

                fullUrls = new string[urlCount];
                for (int i = 0; i < urlCount; i++)
                {
                    fullUrls[i] = appUrls[i];
                    if (Channel.Current.debug)
                    {
                        fullUrls[i] += App.GetWebSocketTestPort();

#if BUILD_DEBUGURL || UNITY_EDITOR    
                        //强制国内开发服
                        if (AppConst.Unity_CH && i == 0)
                            fullUrls[i] = "wss://test.aoemo.com/websocket/";
#endif
                    }
                    else
                    {
                        fullUrls[i] += App.GetWebSocketPort();
                    }

                    //if (isWss && IsUseLZ4)
                    //{
                    //    fullUrls[i] += UseLZ4Suffix;
                    //}
                }

                newestConnectionUrl = fullUrls[0];
            }

            State = WSNetState.Connecting;
            isConnected = false;
            isConnecting = true;
            webSocket = null;

            LogUtil.Log("[WSNetMgr]Start ProxyConnection");

            WSNetProxyMgr.Instance.Connection(fullUrls, OnWSNetProxyConnectionComplete);

            AppDispatcher.Instance.Dispatch(AppMsg.WebSocketServer_ConnectStart);
        }

        private void OnWSNetProxyConnectionComplete(WebSocketProxy webSocketProxy)
        {
            if (webSocketProxy == null)
            {
                State = WSNetState.Exception;
                isConnected = false;
                isConnecting = false;

                LogUtil.Log("[WSNetMgr]ProxyConnection Fail");
                return;
            }

            State = WSNetState.Connected;
            isConnected = true;
            isConnecting = false;

            newestConnectionUrl = webSocketProxy.url;
            webSocket = webSocketProxy;
            webSocket.OnCloseEvent += OnWebSocketClose;
            webSocket.OnErrorEvent += OnWebSocketError;

            if (IsDisplayLog())
            {
                LogUtil.Log("[WSNetMgr]ProxyConnection Complete: " + webSocketProxy.url);
                WSNetProxyMgr.Instance.LogDatas();
            }

            webSocket.StartReceive();

            //HACK pomelo握手
            webSocket.connect(null, OnWebSocketOpen);
        }

        public string GetNewestConnectionUrl()
        {
            return newestConnectionUrl;
        }

        public string GetCurrUrl()
        {
            if (webSocket != null)
            {
                return webSocket.url;
            }
            return fullUrls[0];
        }

        public bool IsAppWssUrl()
        {
            if (fullUrls == null)
            {
                string[] urls = App.GetWebSocketUrls();
                for (int i = 0; i < urls.Length; i++)
                {
                    if (!IsWssUrl(urls[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return isWss;
        }

        public bool IsWssUrl(string url)
        {
            return url.Contains("wss://");
        }

        private void ClearWebSocketEvent()
        {
            if (webSocket != null)
            {
                webSocket.ClearEvent();
            }
        }

        public void Close()
        {
            LogUtil.Log("[WSNetMgr]Close");

            bool isDisconnect = false;
            if (isConnected)
            {
                isDisconnect = true;
            }

            State = WSNetState.Closed;
            isConnecting = false;
            isConnected = false;
            AppGlobal.IsLoginSucceed = false;

            if (webSocket != null)
            {
                ClearWebSocketEvent();
                webSocket.NormalClose();
            }
            webSocket = null;

            if (isDisconnect)
            {
                AppDispatcher.Instance.Dispatch(AppMsg.WebSocketServer_Disconnect);
            }
        }

        public bool ImmediateSendHeartBeat(BaseC2SJsonProto protoMsg)
        {
            bool isCanSend = webSocket != null && NetConst.IsNetAvailable;
            if (!isCanSend) return false;

            webSocket.SendHeartBeat();
            if (IsDisplayLog())
            {
                string logInfo = string.Format("[WSNetMgr]Send: C2S_heartbeat");
                LogUtil.LogObject(logInfo, protoMsg);
            }

            return true;
        }

        public bool ImmediateSend(BaseC2SJsonProto protoMsg, bool notify = false)
        {
            string msgType = protoMsg.type;
            try
            {
                bool isCanSend = webSocket != null && isConnected && NetConst.IsNetAvailable;
                if (isCanSend)
                {
                    if (!protoC2STypeDict.ContainsKey(msgType))
                    {
                        if (IsDisplayLog())
                        {
                            LogUtil.LogErrorFormat("[WSNetMgr]ImmediateSend: {0} No Registered Message", msgType);
                        }
                        return false;
                    }

                    SendProtoMsg(protoMsg, notify);

                    if (IsDisplayLog())
                    {
                        if (!c2sProtoLogIgnoreList.Contains(msgType))
                        {
                            string logInfo = string.Format("[WSNetMgr]ImmediateSend: {0}", msgType);
                            LogUtil.LogObject(logInfo, protoMsg);
                        }
                    }
                }
                else
                {
                    if (IsDisplayLog())
                    {
                        if (!c2sProtoLogIgnoreList.Contains(msgType))
                        {
                            string logInfo = string.Format("[WSNetMgr]Can't ImmediateSend: {0} | Param: webSocket != null:{1} isConnected:{2} NetConst.IsNetAvailable:{3}", msgType, (webSocket != null).ToString(), isConnected, NetConst.IsNetAvailable);
                            LogUtil.LogObject(logInfo, protoMsg);
                        }
                    }
                    return false;
                }
                return isCanSend;
            }
            catch (Exception e)
            {
                if (IsDisplayLog())
                {
                    LogUtil.LogErrorFormat("[WSNetMgr]ImmediateSend {0} Exception: {1}\nRawJson:{2}"
                        , msgType, e.ToString(), SerializeUtil.ToRawJson(protoMsg));
                }
                return false;
            }
        }

        public bool Send(BaseC2SJsonProto protoMsg, bool notify = false)
        {
            string msgType = protoMsg.type;
            try
            {
                bool isCanSend = webSocket != null && isConnected && NetConst.IsNetAvailable && AppGlobal.IsLoginSucceed;
                if (isCanSend)
                {
                    if (!protoC2STypeDict.ContainsKey(msgType))
                    {
                        if (IsDisplayLog())
                        {
                            LogUtil.LogErrorFormat("[WSNetMgr]Send: {0} No Registered Message", msgType);
                        }
                        return false;
                    }

                    SendProtoMsg(protoMsg, notify);

                    if (IsDisplayLog())
                    {
                        if (!c2sProtoLogIgnoreList.Contains(msgType))
                        {
                            string logInfo = string.Format("[WSNetMgr]Send: {0}", msgType);
                            LogUtil.LogObject(logInfo, protoMsg);
                        }
                    }
                }
                else
                {
                    if (IsDisplayLog())
                    {
                        if (!c2sProtoLogIgnoreList.Contains(msgType))
                        {
                            string logInfo = string.Format("[WSNetMgr]Can't Send: {0} ", msgType);
                            LogUtil.LogObject(logInfo, protoMsg);
                        }
                    }
                    return false;
                }
                return isCanSend;
            }
            catch (Exception e)
            {
                if (IsDisplayLog())
                {
                    LogUtil.LogErrorFormat("[WSNetMgr]Send: {0} Exception: {1}\nRawJson:{2}"
                        , msgType, e.ToString(), SerializeUtil.ToRawJson(protoMsg));
                }
                return false;
            }
        }

        private void SendProtoMsg(BaseC2SJsonProto protoMsg, Action action)
        {

        }


        private void SendProtoMsg(BaseC2SJsonProto protoMsg, bool notify = false)
        {
            if (!protoC2STypeDict.ContainsKey(protoMsg.type))
            {
                if (IsDisplayLog())
                {
                    LogUtil.LogErrorFormat("[WSNetMgr]SendProtoMsg: {0} No Registered Message", protoMsg.type);  
                }
                return;
            };

            JObject wsMsg = SerializeUtil.ToObject<JObject>(protoMsg.send_json);
            string route = protoMsg.route;
            string bind_s2c_type = protoMsg.bind_s2c_type;
            if (notify) webSocket.notify(route, wsMsg);
            else webSocket.Send(route, bind_s2c_type, wsMsg);
        }

        public void ReceiveProtoMsg(JObject result, string bind_s2c_type)
        {
            long rpcId = 0;
            string jsonMsg = "";
            try
            {
                jsonMsg = result.ToString();
                if (IsDisplayLog())
                {
                    if (string.IsNullOrWhiteSpace(jsonMsg))
                    {
                        LogUtil.LogError("[WSNetMgr]ReceiveProtoMsg: {0} Exception: Json is null or whiteSpace");
                        return;
                    }
                }

                if (!protoS2CTypeDict.ContainsKey(bind_s2c_type))
                {
                    if (IsDisplayLog())
                    {
                        LogUtil.LogErrorFormat("[WSNetMgr]ReceiveProtoMsg: {0} No Registered Message, Json:\n{1}", bind_s2c_type, jsonMsg);  
                    }
                    return;
                }

                Type protoMsgType = protoS2CTypeDict[bind_s2c_type];
                BaseS2CJsonProto s2cProtoMsg = SerializeUtil.ToObject(jsonMsg, protoMsgType) as BaseS2CJsonProto;
                s2cProtoMsg.SetRawJson(jsonMsg);
                if (Convert.ToInt32(result["code"]) != 0)
                {
                    s2cProtoMsg.err = result["message"].ToString();
                }

                if (WSNetDispatcher.Instance)
                {
                    WSNetDispatcher.Instance.Dispatch(s2cProtoMsg);
                }
            }
            catch (Exception e)
            {
                if (!AppGlobal.IsGameStart)
                {
                    State = WSNetState.PreferencesParseError;
                    AppDispatcher.Instance.Dispatch(AppMsg.WebSocketServer_PreferencesParseError);
                }
                if (IsDisplayLog())
                {
                    LogUtil.LogErrorFormat("[WSNetMgr]ReceiveProtoMsg: {0} {1} Exception: {2} Json:\n{3}", rpcId, bind_s2c_type, e.ToString(), jsonMsg);
                }
            }
        }

        #region WebSocket
        private void OnWebSocketOpen(JObject handCall)
        {
            LogUtil.Log("[WSNetMgr]OnWebSocketOpen");

            State = WSNetState.Connected;
            isConnected = true;
            isConnecting = false;

            if (connectSucceedFunc != null)
            {
                connectSucceedFunc();
                connectSucceedFunc = null;
            }
            AppDispatcher.Instance.Dispatch(AppMsg.WebSocketServer_ConnectSucceed);
        }

        private void OnWebSocketClose(WebSocket webSocket, ushort closeCode, string message)
        {
            if (this.webSocket == webSocket)
            {
                LogUtil.LogFormat("[WSNetMgr]OnWebSocketClose code:{0} message:{1}", closeCode.ToString(), message);
                Close();
            }
            else
            {
                LogUtil.LogFormat("[WSNetMgr]LastWebSocket OnWebSocketClose warning:Received the last socket close! code:{0}  message:{1}", closeCode.ToString(), message);
                webSocket.Close();
            }
        }

        private void OnWebSocketError(WebSocket webSocket, string exception)
        {
            if (this.webSocket == webSocket)
            {
                if (exception != null)
                {
                    LogUtil.LogErrorFormat("[WSNetMgr]OnWebSocketError\n{0}", exception);
                }
                else
                {
                    LogUtil.Log("[WSNetMgr]OnWebSocketError");
                }

                Close();
                State = WSNetState.Exception;
                AppDispatcher.Instance.Dispatch(AppMsg.WebSocketServer_Exception);
            }
            else
            {
                if (exception != null)
                {
                    LogUtil.LogFormat("[WSNetMgr]LastWebSocket OnWebSocketError\n{0}", exception);
                }
                else
                {
                    LogUtil.Log("[WSNetMgr]LastWebSocket OnWebSocketError");
                }
                webSocket.Close();
            }
        }
        #endregion
    }
}