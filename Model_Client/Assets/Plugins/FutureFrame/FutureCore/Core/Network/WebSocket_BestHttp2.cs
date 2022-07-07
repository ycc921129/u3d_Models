/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

#define USE_WebSocket_BestHttp

#if USE_WebSocket_BestHttp

using System;
using System.Collections.Generic;
using BestHTTP;
using BestHTTP.WebSocket;
using Newtonsoft.Json.Linq;
using Pomelo.Client;
using BWebSocket = BestHTTP.WebSocket.WebSocket;

namespace FutureCore
{
    public class WebSocket
    {
        public enum WebSocketStatus : byte
        {
            Connecting = 0,
            Open = 1,
            Closing = 2,
            Closed = 3,
            Unknown = 4,
        };

        /// <summary>
        /// 默认超时时间
        /// </summary>
        public static float DefaultTimeout = 6f;
        /// <summary>
        /// 保活时间
        /// </summary>
        public static int KeepAliveInterval = 15;

        static WebSocket()
        {
            InitBestHttpSetting();
        }

        private static void InitBestHttpSetting()
        {
            HTTPManager.ConnectTimeout = TimeSpan.FromSeconds(DefaultTimeout);
            HTTPManager.RequestTimeout = TimeSpan.FromSeconds(DefaultTimeout);
        }

        public delegate void OnThisWebSocketOpenDelegate(WebSocket webSocket);
        public delegate void OnThisWebSocketMessageDelegate(WebSocket webSocket, string message);
        public delegate void OnThisWebSocketBinaryDelegate(WebSocket webSocket, byte[] data);
        public delegate void OnThisWebSocketClosedDelegate(WebSocket webSocket, ushort code, string message);
        public delegate void OnThisWebSocketErrorDelegate(WebSocket webSocket, string reason);
        public OnThisWebSocketOpenDelegate OnOpenEvent;
        public OnThisWebSocketMessageDelegate OnMessageEvent;
        public OnThisWebSocketBinaryDelegate OnBinaryEvent;
        public OnThisWebSocketClosedDelegate OnCloseEvent;
        public OnThisWebSocketErrorDelegate OnErrorEvent;

        private string url;
        private BWebSocket webSocket;

        public WebSocket(string url, Dictionary<string, string> headers = null)
        {  
            this.url = url;
            webSocket = new BWebSocket(new Uri(url));
            protocol = new Protocol(webSocket);

            webSocket.OnOpen += OnOpen;
            //webSocket.OnMessage += OnMessage;
            //webSocket.OnBinary += OnBinary;
            webSocket.OnClosed += OnClosed;
            webSocket.OnError += OnError;            

#if !UNITY_WEBGL
            webSocket.StartPingThread = true;
            int keepAliveInterval = KeepAliveInterval;
            webSocket.PingFrequency = keepAliveInterval * 1000;
            webSocket.CloseAfterNoMessage = TimeSpan.FromSeconds(keepAliveInterval);
#if !BESTHTTP_DISABLE_PROXY
            if (HTTPManager.Proxy != null)
            {
                webSocket.InternalRequest.Proxy = new HTTPProxy(HTTPManager.Proxy.Address, HTTPManager.Proxy.Credentials, false);
            }
#endif
#endif
        }

        private void OnIncompleteFrame(BWebSocket webSocket, BestHTTP.WebSocket.Frames.WebSocketFrameReader reader)
        {
            LogUtil.Log("OnIncompleteFrame: " + reader.Data.Length);
        }

        public WebSocketStatus GetState()
        {
            if (webSocket != null)
            {
                switch (webSocket.State)
                {
                    case WebSocketStates.Connecting:
                        return WebSocketStatus.Connecting;
                    case WebSocketStates.Open:
                        return WebSocketStatus.Open;  
                    case WebSocketStates.Closing:
                        return WebSocketStatus.Closing;
                    case WebSocketStates.Closed:
                        return WebSocketStatus.Closed;
                    case WebSocketStates.Unknown:
                        return WebSocketStatus.Unknown;
                    default:
                        return WebSocketStatus.Unknown;
                }
            }
            return WebSocketStatus.Unknown;
        }

        public void StartConnect()
        {
            if (webSocket != null)
            {
                webSocket.Open();
            }
        }        

        public void CancelConnection()
        {
            Close();
        }

        public void Send(string route, string bind_s2c_type, JObject msg)   
        {
            if (webSocket != null)
            { 
                request(route, bind_s2c_type, msg);   
            }
        }

        public void Send(byte[] bytes)
        {
            if (webSocket != null)
            {
                webSocket.Send(bytes);
            }
        }

        public void StartReceive()
        {
            if (webSocket != null)
            {
                webSocket.StartReceive();
            }  
        }

        public void Close()
        {
            if (webSocket != null)
            {
                protocol.close();
                webSocket.Close();
                ClearEvent();
                webSocket = null;
            }
        }
        public void ClearEvent()
        {
            OnOpenEvent = null;
            OnMessageEvent = null;
            OnBinaryEvent = null;
            OnCloseEvent = null;
            OnErrorEvent = null;
        }

        private void OnOpen(BWebSocket webSocket)
        {
            if (OnOpenEvent != null)
            {
                OnOpenEvent(this);
                OnOpenEvent = null;
            }
        }

        private void OnMessage(BWebSocket webSocket, string message)
        {
            if (OnMessageEvent != null)
            {
                OnMessageEvent(this, message);
            }
        }     

        private void OnBinary(BWebSocket webSocket, byte[] data) 
        {
            if (OnBinaryEvent != null)
            {
                OnBinaryEvent(this, data);
            }
        }

        private void OnClosed(BWebSocket webSocket, ushort code, string message)
        {
            if (OnCloseEvent != null)
            {
                OnCloseEvent(this, code, message);
            }
        }

        private void OnError(BWebSocket webSocket, string reason)
        {
            if (OnErrorEvent != null)
            {
                OnErrorEvent(this, reason);
            }
        }

        #region pomelo
        /// <summary>
        /// pomelo  
        /// </summary>
        private uint reqId = 1;
        private Protocol protocol; 

        public bool connect(JObject user, Action<JObject> handshakeCallback)
        {
            try
            {
                protocol.start(user, handshakeCallback);  
                return true;
            }
            catch (Exception e)
            {
                LogUtil.Log(e.ToString());
                return false;
            }
        }        

        public void request(string route, string bind_s2c_type, JObject msg)  
        {
            protocol.AddCallBack(reqId, bind_s2c_type);  
            protocol.send(route, reqId, msg);
            reqId++;    
        }  

        public void push(string route, string bind_s2c_type)
        {
            if (protocol == null) return;
            protocol.AddCallBack(route, bind_s2c_type);
        }

        public void notify(string route, JObject msg)
        {    
            protocol.send(route, msg);
        }

        public void SendHeartBeat()
        {
            protocol.send(PackageType.PKG_HEARTBEAT);  
        }
        #endregion 
    }
}
#endif    