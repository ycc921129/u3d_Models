using System;
using BestHTTP.WebSocket;
using FutureCore;
using UnityEngine;
using static FutureCore.WebSocket;
using BWebSocket = BestHTTP.WebSocket.WebSocket;

namespace Pomelo.Client
{
    public class Transporter
    {
        public const string TAG = "[Transporter] ";  
        public const int HeadLength = 4;
        
        private BWebSocket socket;  
        private Action<byte[]> messageProcesser;
        private TransportState transportState;

        private byte[] headBuffer = new byte[4];
        private byte[] buffer;
        private int bufferOffset = 0;
        private int pkgLength = 0;
          
        //internal Action onDisconnect = null;

        public Transporter(BWebSocket socket, Action<byte[]> processer)
        {
            this.socket = socket;
            this.messageProcesser = processer;
            transportState = TransportState.readHead;
        }

        public void start()
        {
            this.socket.OnBinary += OnBinaryMessageReceived;
        }

        public void send(byte[] buffer)
        {
            if (this.transportState != TransportState.closed)
            {
                this.socket.Send(buffer);                
            }
        }
     
        void OnBinaryMessageReceived(BWebSocket webSocket, byte[] message)
        {
            if (this.transportState == TransportState.closed) return;

            BWebSocket socket = this.socket;
            int length = message.Length; 
            
            if (length > 0)
            {
                processBytes(message, 0, length);
            }
            else
            {
                LogUtil.LogError(StringUtil.Concat(TAG, "length < 0, length : ", length.ToString()));  
            }
        }

        internal void close() 
        {
            this.transportState = TransportState.closed;
        }          

        internal void processBytes(byte[] bytes, int offset, int limit)
        {
            if (this.transportState == TransportState.readHead)
            {
                readHead(bytes, offset, limit);
            }
            else if (this.transportState == TransportState.readBody)
            {
                readBody(bytes, offset, limit);
            }
        }

        private bool readHead(byte[] bytes, int offset, int limit)
        {
            int length = limit - offset;
            int headNum = HeadLength - bufferOffset;

            if (length >= headNum)
            {
                //Write head buffer
                writeBytes(bytes, offset, headNum, bufferOffset, headBuffer);
                //Get package length
                pkgLength = (headBuffer[1] << 16) + (headBuffer[2] << 8) + headBuffer[3];

                //Init message buffer
                buffer = new byte[HeadLength + pkgLength];
                writeBytes(headBuffer, 0, HeadLength, buffer);
                offset += headNum;
                bufferOffset = HeadLength;
                this.transportState = TransportState.readBody;

                if (offset <= limit) processBytes(bytes, offset, limit);
                return true;
            }
            else
            {
                writeBytes(bytes, offset, length, bufferOffset, headBuffer);
                bufferOffset += length;
                return false;
            }
        }

        private void readBody(byte[] bytes, int offset, int limit)
        {
            int length = pkgLength + HeadLength - bufferOffset;
            if ((offset + length) <= limit)
            {
                writeBytes(bytes, offset, length, bufferOffset, buffer);
                offset += length;

                //Invoke the protocol api to handle the message
                this.messageProcesser.Invoke(buffer);
                this.bufferOffset = 0;
                this.pkgLength = 0;

                if (this.transportState != TransportState.closed) this.transportState = TransportState.readHead;
                if (offset < limit) processBytes(bytes, offset, limit);
            }
            else
            {
                writeBytes(bytes, offset, limit - offset, bufferOffset, buffer);
                bufferOffset += limit - offset;
                this.transportState = TransportState.readBody;
            }
        }

        private void writeBytes(byte[] source, int start, int length, byte[] target)
        {
            writeBytes(source, start, length, 0, target);
        }

        private void writeBytes(byte[] source, int start, int length, int offset, byte[] target)
        {
            for (int i = 0; i < length; i++)
            {
                target[offset + i] = source[start + i];
            }
        }  
    }
}