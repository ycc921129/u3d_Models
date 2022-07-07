/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace FutureCore
{
    public sealed class HttpMgr : BaseMonoMgr<HttpMgr>
    {
        private const int DefaultTimeout = 5;
        private Dictionary<string, Texture2D> textureCaches = new Dictionary<string, Texture2D>();

        public void Send(string webUrl, Action<bool, DownloadHandler> callBack, int timeout = DefaultTimeout, bool isPost = false, Dictionary<string, object> dic = null)
        {
            if (callBack == null) return;

            if (!isPost)
            {
                GetUrl(webUrl, callBack, timeout);
            }
            else
            {
                PostUrl(webUrl, callBack, timeout, dic == null ? string.Empty : SerializeUtil.ToJson(dic));
            }
        }

        private void GetUrl(string webUrl, Action<bool, DownloadHandler> callBack, int timeout)
        {
            UnityWebRequest webReq = UnityWebRequest.Get(webUrl);
            webReq.timeout = timeout;
            StartCoroutine(OnHttpRequest(webReq, callBack));
        }

        private void PostUrl(string webUrl, Action<bool, DownloadHandler> callBack, int timeout, string json)
        {
            WWWForm form = new WWWForm();
            form.AddField(string.Empty, json);
            UnityWebRequest webReq = UnityWebRequest.Post(webUrl, form);
            webReq.timeout = timeout;
            StartCoroutine(OnHttpRequest(webReq, callBack));
        }

        private IEnumerator OnHttpRequest(UnityWebRequest webReq, Action<bool, DownloadHandler> callBack)
        {
            yield return webReq.SendWebRequest();

            bool isError = false;
            if (webReq.isNetworkError || webReq.isHttpError || webReq.responseCode != WebRequestConst.Succeed)
            {
                isError = true;
                string errorMsg = webReq.url + " Error: " + webReq.error + " Code: " + webReq.responseCode + " Text: " + webReq.downloadHandler.text;
                LogUtil.LogError("[HttpMgr]" + errorMsg);
            }
            else if (webReq.downloadHandler.text == "error")
            {
                isError = true;
                string errorMsg = "请求失败";
                LogUtil.LogError("[HttpMgr]" + errorMsg);
            }
            callBack(isError, webReq.downloadHandler);

            webReq.Dispose();
        }

        public void DownTexture(string webUrl, Action<Texture2D> callback, bool isCache = true)
        {
            if (textureCaches.ContainsKey(webUrl))
            {
                callback(textureCaches[webUrl]);
                return;
            }
            StartCoroutine(OnDownTexture(webUrl, callback, isCache));
        }

        IEnumerator OnDownTexture(string webUrl, Action<Texture2D> callback, bool isCache)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(webUrl))
            {
                uwr.timeout = DefaultTimeout;
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    LogUtil.LogError("[HttpMgr]" + uwr.error);
                    callback(null);
                    yield break;
                }

                Texture2D texture2D = DownloadHandlerTexture.GetContent(uwr);
                callback(texture2D);
                if (isCache)
                {
                    if (!textureCaches.ContainsKey(webUrl))
                    {
                        textureCaches.Add(webUrl, texture2D);
                    }
                }
            }
        }

        /*
        IEnumerator GetAssetBundle()
        {
            using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle("http://www.my-server.com/mybundle"))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.Log(uwr.error);
                }
                else
                {
                    // Get downloaded asset bundle
                    AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
                }
            }
        }
        */
    }
}