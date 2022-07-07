using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using FutureCore;
using Newtonsoft.Json.Linq;

namespace Pomelo.Client
{
    public class HandShakeService
    {
        public const string TAG = "[HandShakeService] ";
        private Protocol protocol;
        private Action<JObject> callback;        

        public const string Version = "0.3.0";
        public const string Type = "unity-socket";


        public HandShakeService(Protocol protocol)
        {
            this.protocol = protocol;
        }

        public void request(JObject user, Action<JObject> callback)
        {
            byte[] body = Encoding.UTF8.GetBytes(buildMsg(user).ToString());

            protocol.send(PackageType.PKG_HANDSHAKE, body);

            this.callback = callback;
        }

        internal void invokeCallback(JObject data)
        {
            //Invoke the handshake callback
            if (callback != null) callback.Invoke(data);
        }

        public void ack()
        {
            LogUtil.Log(StringUtil.Concat(TAG, "send ack to server!"));
            protocol.send(PackageType.PKG_HANDSHAKE_ACK, new byte[0]);
        }

        private JObject buildMsg(JObject user)
        {
            if (user == null) user = new JObject();

            JObject msg = new JObject();

            //Build sys option
            JObject sys = new JObject();
            sys["version"] = Version;
            sys["type"] = Type;

            //Build handshake message
            msg["sys"] = sys;
            msg["user"] = user;

            return msg;
        }
    }
}
