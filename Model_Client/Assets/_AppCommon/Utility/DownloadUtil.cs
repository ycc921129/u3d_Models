/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using FutureCore;
using UnityEngine;

namespace ProjectApp
{
    public class DownloadUtil : Singleton<DownloadUtil>
    {
        Dictionary<string, Sprite> cacheDic = new Dictionary<string, Sprite>();
        Dictionary<string, Action<Sprite>> downLoadingDic = new Dictionary<string, Action<Sprite>>();

        public DownloadUtil()
        {

        }

        public void DownloadImage(string url, Action<Sprite> callback, bool isCache = false)
        {
            if (cacheDic.ContainsKey(url))
            {
                callback(cacheDic[url]);
                return;
            }
            if (downLoadingDic.ContainsKey(url))
            {
                downLoadingDic[url] += callback;
                return;
            }
            else
            {
                downLoadingDic.Add(url, callback);
            }


            CoroutineMgr.Instance.StartCoroutine(DownloadSprite(url, callback, isCache));
        }

        IEnumerator DownloadSprite(string url, Action<Sprite> callback, bool isCache = false)
        {
            WWW www = null;
            try
            {
                www = new WWW(url);
            }
            catch (Exception e)
            {
                Debug.Log("[HttpMgr] DownloadSprite() error : " + e);
            }
            yield return www;
            if (www != null)
            {
                if (www.error != null)
                {
                    Debug.Log("[HttpMgr] DownloadSprite() 下载图片失败， WWW.Error:" + www.error);

                }
                else
                {
                    Texture2D tex = www.texture;
                    Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100f);
                    if (callback != null)
                    {
                        if (downLoadingDic[url] != null)
                            downLoadingDic[url].Invoke(sprite);

                        downLoadingDic.Remove(url);
                    }
                    if (isCache)
                    {
                        if (!cacheDic.ContainsKey(url))
                        {
                            cacheDic.Add(url, sprite);
                        }
                    }
                }

            }
        }
    }
}
