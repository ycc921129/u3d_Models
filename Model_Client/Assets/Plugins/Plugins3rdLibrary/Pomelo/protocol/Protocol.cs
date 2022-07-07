using System;
using System.Text;
using BWebSocket = BestHTTP.WebSocket.WebSocket;
using UnityEngine;
using static FutureCore.WebSocket;
using FutureCore;
using Newtonsoft.Json.Linq;
using ProjectApp;
using System.Collections.Generic;
using Beebyte.Obfuscator;

namespace Pomelo.Client
{
    [Skip]
    public class BaseS2CJ
    {
        public int code = 0;
        public string message = "SUCCESS";
        public JObject data;  
    }

    public class Protocol
    {
        private MessageProtocol messageProtocol;
        private ProtocolState state;
        private Transporter transporter;
        private HandShakeService handshake;
        private BWebSocket socket;

        private PomeloMsgOnDispatcher pomeloMsgOnDispatcher;
        private BaseS2CJ baseS2CJsonProto;        
        private WSNetMgr wsnetMgr;
        public const string TAG = "[Protocol] ";  

        public Protocol(BWebSocket socket)
        {
            this.socket = socket;
            this.wsnetMgr = WSNetMgr.Instance;
            this.baseS2CJsonProto = new BaseS2CJ(); 
            this.transporter = new Transporter(socket, this.processMessage);
            this.handshake = new HandShakeService(this);
            this.state = ProtocolState.start;
        }  

        internal void start(JObject user, Action<JObject> callback)
        {
            this.transporter.start();  

            this.handshake.request(user, callback);  

            this.state = ProtocolState.handshaking;
        }

        //Send notify, do not neted id
        internal void send(string route, JObject msg)
        {
            send(route, 0, msg);
        }

        //Send request, user request id 
        internal void send(string route, uint id, JObject msg)
        {
            if (this.state != ProtocolState.working) return;

            byte[] body = messageProtocol.encode(route, id, msg);

            send(PackageType.PKG_DATA, body);
        }

        internal void send(PackageType type)
        {
            if (this.state == ProtocolState.closed) return;
            transporter.send(PackageProtocol.encode(type));
        }

        //Send system message, these message do not use messageProtocol
        internal void send(PackageType type, JObject msg)
        {
            //This method only used to send system package
            if (type == PackageType.PKG_DATA) return;

            byte[] body = Encoding.UTF8.GetBytes(msg.ToString());

            send(type, body);
        }

        //Send message use the transporter
        internal void send(PackageType type, byte[] body)
        {
            if (this.state == ProtocolState.closed) return;

            byte[] pkg = PackageProtocol.encode(type, body);

            transporter.send(pkg);
        }

        //Invoke by Transporter, process the message
        internal void processMessage(byte[] bytes)
        {
            Package pkg = PackageProtocol.decode(bytes);

            //Ignore all the message except handshading at handshake stage
            if (pkg.type == PackageType.PKG_HANDSHAKE && this.state == ProtocolState.handshaking)
            {
                //Ignore all the message except handshading
                //JObject data = (JObject)SimpleJson.SimpleJson.DeserializeObject(Encoding.UTF8.GetString(pkg.body));
                JObject data = SerializeUtil.ToObject<JObject>(Encoding.UTF8.GetString(pkg.body));

                processHandshakeData(data);          
                this.state = ProtocolState.working;

                //握手成功后发送心跳
                PomeloMsgOnDispatcher.Instance.Dispatch(RouteConst.HeartBeat_Success); 
            }
            else if (pkg.type == PackageType.PKG_HEARTBEAT && this.state == ProtocolState.working) 
            {
                JObject heartbeatInfo = SerializeUtil.ToObject<JObject>(Encoding.UTF8.GetString(pkg.body));
                PomeloMsgOnDispatcher.Instance.Dispatch(RouteConst.Route_heartbeat, heartbeatInfo);
            }
            else if (pkg.type == PackageType.PKG_DATA && this.state == ProtocolState.working)
            {
                processMessage(messageProtocol.decode(pkg.body));
            }
            else if (pkg.type == PackageType.PKG_KICK)
            {
                this.close();
            }
        }

        private void processHandshakeData(JObject msg)
        {
            //Handshake error
            if (msg.Property("code") == null || msg.Property("sys") == null || Convert.ToInt32(msg["code"]) != 200)
            {
                throw new Exception("Handshake error! Please check your handshake config.");
            }

            //Set compress data
            JObject sys = (JObject)msg["sys"];

            //心跳时间设置
            if (sys != null && sys["heartbeat"] != null)
            {
                try
                {
                    var time = int.Parse(sys["heartbeat"].ToString()); 
                    DateTimeMgr.Instance.SetHeartBeatTime(time);
                }
                catch (Exception e)
                {
                    LogUtil.LogError(StringUtil.Concat(TAG, "heartbeat is error."));
                }
            }

            JObject dict = new JObject();
            if (sys.Property("dict") != null) dict = (JObject)sys["dict"];


            JObject protos = new JObject();
            JObject serverProtos = new JObject();
            JObject clientProtos = new JObject();

            if (sys.Property("protos") != null)
            {
                protos = (JObject)sys["protos"];
                serverProtos = (JObject)protos["server"];
                clientProtos = (JObject)protos["client"];
            }

            messageProtocol = new MessageProtocol(dict, serverProtos, clientProtos);            

            //send ack and change protocol state
            handshake.ack();
            this.state = ProtocolState.working;

            //Invoke handshake callback
            JObject user = new JObject();
            if (msg.Property("user") != null) user = (JObject)msg["user"];
            handshake.invokeCallback(user);
        }        

        internal void close()
        {
            transporter.close();            
            this.state = ProtocolState.closed; 
        }

        internal void processMessage(Message msg)
        {
            if (msg.type == MessageType.MSG_RESPONSE)
            {
                //eventManager.InvokeCallBack(msg.id, msg.data);
                wsnetMgr.ReceiveProtoMsg(msg.data, ProtoTypeById_s2c(msg.id));
            }
            else if (msg.type == MessageType.MSG_PUSH)
            {
                JObject data = null; 
                try
                {
                    //HACK 屏蔽心跳协议时间out
                    if (msg.route == "SESSION_HEARTBEAT_TIMEOUT") return;
                    if (wsnetMgr == null) wsnetMgr = WSNetMgr.Instance; 
                    baseS2CJsonProto.data = msg.data;          
                    data = SerializeUtil.GetJObjectByObject(baseS2CJsonProto);  
                    wsnetMgr.ReceiveProtoMsg(data, ProtoTypeByRoute_s2c(msg.route));
                }  
                catch (Exception e)
                {
                    LogUtil.LogError(TAG + "MSG_PUSH data is error .");
                }
            }
        }

        private Dictionary<uint, string> protoTypeByReqId = new Dictionary<uint, string>();
        private Dictionary<string, string> protoTypeByReqRoute = new Dictionary<string, string>();
        public void AddCallBack(uint id, string bind_s2c_type)
        {
            this.protoTypeByReqId.Add(id, bind_s2c_type);
        }

        private string ProtoTypeById_s2c(uint id)
        {
            if (protoTypeByReqId.ContainsKey(id))
                return protoTypeByReqId[id];

            LogUtil.LogError("ProtoTypeById_s2c is error, id :" + id);
            return "";  
        }

        public void AddCallBack(string route, string bind_s2c_type)
        {
            if (protoTypeByReqRoute.ContainsKey(route)) return; 

            this.protoTypeByReqRoute.Add(route, bind_s2c_type);
        }
          
        private string ProtoTypeByRoute_s2c(string route)
        {
            if (protoTypeByReqRoute.ContainsKey(route))
                return protoTypeByReqRoute[route];

            LogUtil.LogError("ProtoTypeById_s2c is error, route :" + route);
            return "";
        } 
    }
}

