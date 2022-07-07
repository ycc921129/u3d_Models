/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;

namespace FutureCore
{
    public class WebSocketProxy : WebSocket
    {
        public int idx = 0;

        public string url
        {
            get;
            private set;
        }

        /// <summary>
        /// 正常关闭事件
        /// </summary>
        public event Action<WebSocket> norMalCloseEvent;

        public WebSocketProxy(int idx, string url, Dictionary<string, string> headers = null) : base(url, headers)
        {
            this.idx = idx;
            this.url = url;
        }

        public void NormalClose()
        {
            norMalCloseEvent?.Invoke(this);
            norMalCloseEvent = null;
            Close();
        }
    }
}