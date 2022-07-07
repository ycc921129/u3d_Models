/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

//#define USE_WebSocket_CSharp

#if USE_WebSocket_CSharp

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FutureCore
{
    public delegate void WebSocketOpenEventHandler(WebSocket sender);
    public delegate void WebSocketMessageEventHandler(WebSocket sender, string data);
    public delegate void WebSocketBinaryEventHandler(WebSocket sender, byte[] data);
    public delegate void WebSocketCloseEventHandler(WebSocket sender, ushort closeCode, string message);
    public delegate void WebSocketErrorEventHandler(WebSocket sender, string exception);

    public enum WebSocketCloseCode : ushort
    {
        /* Do NOT use NotSet - it's only purpose is to indicate that the close code cannot be parsed. */
        NotSet = 0,
        Normal = 1000,
        Away = 1001,
        ProtocolError = 1002,
        UnsupportedData = 1003,
        Undefined = 1004,
        NoStatus = 1005,
        Abnormal = 1006,
        InvalidData = 1007,
        PolicyViolation = 1008,
        TooBig = 1009,
        MandatoryExtension = 1010,
        ServerError = 1011,
        TlsHandshakeFailure = 1015
    }

    public enum WebSocketState
    {
        Connecting,
        Open,
        Closing,
        Closed
    }

    public interface IWebSocket
    {
        event WebSocketOpenEventHandler OnOpenEvent;
        event WebSocketMessageEventHandler OnMessageEvent;
        event WebSocketBinaryEventHandler OnBinaryEvent;
        event WebSocketErrorEventHandler OnErrorEvent;
        event WebSocketCloseEventHandler OnCloseEvent;

        WebSocketState State { get; }
    }

    public static class WebSocketHelpers
    {
        public static WebSocketCloseCode ParseCloseCodeEnum(int closeCode)
        {
            if (Enum.IsDefined(typeof(WebSocketCloseCode), closeCode))
            {
                return (WebSocketCloseCode)closeCode;
            }
            else
            {
                return WebSocketCloseCode.Undefined;
            }
        }

        public static WebSocketException GetErrorMessageFromCode(int errorCode, Exception inner)
        {
            switch (errorCode)
            {
                case -1:
                    return new WebSocketUnexpectedException("WebSocket instance not found.", inner);

                case -2:
                    return new WebSocketInvalidStateException("WebSocket is already connected or in connecting state.", inner);

                case -3:
                    return new WebSocketInvalidStateException("WebSocket is not connected.", inner);

                case -4:
                    return new WebSocketInvalidStateException("WebSocket is already closing.", inner);

                case -5:
                    return new WebSocketInvalidStateException("WebSocket is already closed.", inner);

                case -6:
                    return new WebSocketInvalidStateException("WebSocket is not in open state.", inner);

                case -7:
                    return new WebSocketInvalidArgumentException("Cannot close WebSocket. An invalid code was specified or reason is too long.", inner);

                default:
                    return new WebSocketUnexpectedException("Unknown error.", inner);
            }
        }
    }

    public class WebSocketException : Exception
    {
        public WebSocketException()
        {
        }

        public WebSocketException(string message) : base(message)
        {
        }

        public WebSocketException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class WebSocketUnexpectedException : WebSocketException
    {
        public WebSocketUnexpectedException()
        {
        }

        public WebSocketUnexpectedException(string message) : base(message)
        {
        }

        public WebSocketUnexpectedException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class WebSocketInvalidArgumentException : WebSocketException
    {
        public WebSocketInvalidArgumentException()
        {
        }

        public WebSocketInvalidArgumentException(string message) : base(message)
        {
        }

        public WebSocketInvalidArgumentException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class WebSocketInvalidStateException : WebSocketException
    {
        public WebSocketInvalidStateException()
        {
        }

        public WebSocketInvalidStateException(string message) : base(message)
        {
        }

        public WebSocketInvalidStateException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class WebSocket : IWebSocket
    {
        public enum WebSocketStatus : byte
        {
            Connecting = 0,
            Open = 1,
            Closing = 2,
            Closed = 3,
            Unknown = 4,
        };

        public event WebSocketOpenEventHandler OnOpenEvent;
        public event WebSocketMessageEventHandler OnMessageEvent;
        public event WebSocketBinaryEventHandler OnBinaryEvent;
        public event WebSocketErrorEventHandler OnErrorEvent;
        public event WebSocketCloseEventHandler OnCloseEvent;

        private readonly object Lock = new object();
        private static readonly int BufferSize = 40960;

        private string url;
        private Uri uri;
        private Dictionary<string, string> headers;
        private ClientWebSocket m_Socket;

        private CancellationTokenSource m_TokenSource;
        private CancellationToken m_CancellationToken;

        private bool isDispose = false;
        private bool isSending = false;
        private List<ArraySegment<byte>> sendTextQueue = new List<ArraySegment<byte>>();
        private List<ArraySegment<byte>> sendBytesQueue = new List<ArraySegment<byte>>();

        private ArraySegment<byte> cacheSendBuffer = new ArraySegment<byte>();
        private ArraySegment<byte> cacheReceiveBuffer = new ArraySegment<byte>(new byte[BufferSize]);
        private ArraySegment<byte> cacheBuffer = new ArraySegment<byte>(new byte[BufferSize]);

        public WebSocket(string url, Dictionary<string, string> headers = null)
        {
            this.url = url;
            uri = new Uri(url);

            if (headers == null)
            {
                this.headers = new Dictionary<string, string>();
            }
            else
            {
                this.headers = headers;
            }

            string protocol = uri.Scheme;
            if (!protocol.Equals("ws") && !protocol.Equals("wss"))
            {
                throw new ArgumentException("Unsupported protocol: " + protocol);
            }
        }

        public WebSocketStatus GetState()
        {
            switch (State)
            {
                case WebSocketState.Connecting:
                    return WebSocketStatus.Connecting;
                case WebSocketState.Open:
                    return WebSocketStatus.Open;
                case WebSocketState.Closing:
                    return WebSocketStatus.Closing;
                case WebSocketState.Closed:
                    return WebSocketStatus.Closed;
                default:
                    return WebSocketStatus.Unknown;
            }
            return WebSocketStatus.Unknown;
        }

        public void StartConnect()
        {
            Task task = Connect();
        }

        private async Task Connect()
        {
            try
            {
                m_TokenSource = new CancellationTokenSource();
                m_CancellationToken = m_TokenSource.Token;

                m_Socket = new ClientWebSocket();
                // 修改保活时间 15秒
                m_Socket.Options.KeepAliveInterval = new TimeSpan(0, 0, 15);
                m_Socket.Options.SetBuffer(BufferSize, BufferSize, cacheBuffer);

                foreach (var header in headers)
                {
                    m_Socket.Options.SetRequestHeader(header.Key, header.Value);
                }

                await m_Socket.ConnectAsync(uri, m_CancellationToken);
                OnOpenEvent?.Invoke(this);
            }
            catch (Exception e)
            {
                OnErrorEvent?.Invoke(this, e.ToString());
                OnCloseEvent?.Invoke(this, (ushort)WebSocketCloseCode.Abnormal, e.ToString());
            }
        }

        public void CancelConnection()
        {
            m_TokenSource?.Cancel();
        }

        public void StartReceive()
        {
            Task task = Receive();
        }

        public async Task Receive()
        {
            try
            {
                await DoReceive();
            }
            catch (Exception e)
            {
                OnErrorEvent?.Invoke(this, e.ToString());
                OnCloseEvent?.Invoke(this, (ushort)WebSocketCloseCode.Abnormal, e.ToString());
            }
            finally
            {
                if (m_Socket != null)
                {
                    m_TokenSource.Cancel();
                    await Close();
                }
            }
        }

        public WebSocketState State
        {
            get
            {
                switch (m_Socket.State)
                {
                    case System.Net.WebSockets.WebSocketState.Connecting:
                        return WebSocketState.Connecting;

                    case System.Net.WebSockets.WebSocketState.Open:
                        return WebSocketState.Open;

                    case System.Net.WebSockets.WebSocketState.CloseSent:
                    case System.Net.WebSockets.WebSocketState.CloseReceived:
                        return WebSocketState.Closing;

                    case System.Net.WebSockets.WebSocketState.Closed:
                        return WebSocketState.Closed;

                    default:
                        return WebSocketState.Closed;
                }
            }
        }

        public Task Send(string message)
        {
            var encoded = Encoding.UTF8.GetBytes(message);
            return SendMessage(sendTextQueue, WebSocketMessageType.Text, new ArraySegment<byte>(encoded, 0, encoded.Length));
        }

        public Task Send(byte[] bytes)
        {
            return SendMessage(sendBytesQueue, WebSocketMessageType.Binary, new ArraySegment<byte>(bytes));
        }

        private async Task SendMessage(List<ArraySegment<byte>> queue, WebSocketMessageType messageType, ArraySegment<byte> buffer)
        {
            // Return control to the calling method immediately.
            await Task.Yield();

            // Make sure we have data.
            if (buffer.Count == 0)
            {
                return;
            }

            // The state of the connection is contained in the context Items dictionary.
            bool sending;

            lock (Lock)
            {
                sending = isSending;

                // If not, we are now.
                if (!isSending)
                {
                    isSending = true;
                }
            }

            if (!sending)
            {
                // Lock with a timeout, just in case.
                if (!Monitor.TryEnter(m_Socket, 1000))
                {
                    // If we couldn't obtain exclusive access to the socket in one second, something is wrong.
                    await m_Socket.CloseAsync(WebSocketCloseStatus.InternalServerError, string.Empty, m_CancellationToken);
                    return;
                }

                try
                {
                    // Send the message synchronously.
                    var t = m_Socket.SendAsync(buffer, messageType, true, m_CancellationToken);
                    t.Wait(m_CancellationToken);
                }
                finally
                {
                    Monitor.Exit(m_Socket);
                }

                // Note that we've finished sending.
                lock (Lock)
                {
                    isSending = false;
                }

                // Handle any queued messages.
                await HandleQueue(queue, messageType);
            }
            else
            {
                // Add the message to the queue.
                lock (Lock)
                {
                    queue.Add(buffer);
                }
            }
        }

        private async Task HandleQueue(List<ArraySegment<byte>> queue, WebSocketMessageType messageType)
        {
            ArraySegment<byte> handleBuffer = cacheSendBuffer;
            lock (Lock)
            {
                // Check for an item in the queue.
                if (queue.Count > 0)
                {
                    // Pull it off the top.
                    handleBuffer = queue[0];
                    queue.RemoveAt(0);
                }
            }

            // Send that message.
            if (handleBuffer.Count > 0)
            {
                await SendMessage(queue, messageType, handleBuffer);
            }
        }

        //private class WaitForBackgroundThread
        //{
        //    public System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter GetAwaiter()
        //    {
        //        return Task.Run(() => { }).ConfigureAwait(false).GetAwaiter();
        //    }
        //}

        public async Task DoReceive()
        {
            // 切到子线程
            //await new WaitForBackgroundThread();
            await Task.Yield();

            try
            {
                ArraySegment<byte> receiveBuffer = cacheReceiveBuffer;
                while (m_Socket.State == System.Net.WebSockets.WebSocketState.Open)
                {
                    Task<WebSocketReceiveResult> resultTask = null;
                    WebSocketReceiveResult result = null;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        do
                        {
                            //result = await m_Socket.ReceiveAsync(receiveBuffer, m_CancellationToken);
                            resultTask = m_Socket.ReceiveAsync(receiveBuffer, m_CancellationToken);
                            await resultTask;
                            result = resultTask.Result;

                            if (result.MessageType != WebSocketMessageType.Close && result.Count > 0)
                            {
                                ms.Write(receiveBuffer.Array, receiveBuffer.Offset, result.Count);
                            }
                        }
                        while (!result.EndOfMessage);

                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await Close();
                            OnCloseEvent?.Invoke(this, (ushort)WebSocketHelpers.ParseCloseCodeEnum((int)result.CloseStatus), "WebSocketMessageType.Close");
                            break;
                        }

                        ms.Seek(0, SeekOrigin.Begin);
                        byte[] receiveBytes = ms.ToArray();
                        if (receiveBytes.Length > 0)
                        {
                            if (result.MessageType == WebSocketMessageType.Text)
                            {
                                OnMessageEvent?.Invoke(this, Encoding.UTF8.GetString(receiveBytes));
                            }
                            else if (result.MessageType == WebSocketMessageType.Binary)
                            {
                                OnBinaryEvent?.Invoke(this, receiveBytes);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                m_TokenSource.Cancel();
                OnErrorEvent?.Invoke(this, e.ToString());
                OnCloseEvent?.Invoke(this, (ushort)WebSocketCloseCode.Abnormal, e.ToString());
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

        public async Task Close()
        {
            await Task.Yield();

            if (State == WebSocketState.Open)
            {
                await m_Socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, m_CancellationToken);
            }

            if (!isDispose)
            {
                m_Socket.Dispose();
                isDispose = true;
            }
        }

        public void Dispose()
        {
            if (!isDispose)
            {
                m_Socket.Dispose();
                isDispose = true;
            }
        }
    }
}

#endif