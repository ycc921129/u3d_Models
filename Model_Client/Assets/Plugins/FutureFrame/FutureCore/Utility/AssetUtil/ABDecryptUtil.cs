/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace FutureCore
{
    public class ABDecryptMap
    {
        public Dictionary<string, string> abDict;
        public Dictionary<string, string> pathMapDict;
    }

    public static class ABDecryptUtil
    {
        public static string outputPath = Application.streamingAssetsPath + "/Pak/";
        public static string mapJsonPath = outputPath + "ETextureMap" + AppConst.ABExtName;
        public static string manifestPath = outputPath + "Pak";

        public static ABDecryptMap GetABDecryptMap()
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile(mapJsonPath, 0, 111);
            TextAsset textAsset = assetBundle.LoadAsset<TextAsset>("assets/etexturemap.bytes");
            string json = Encoding.UTF8.GetString(textAsset.bytes, 0, textAsset.bytes.Length);
            ABDecryptMap map = SerializeUtil.ToObject<ABDecryptMap>(json);
            assetBundle.Unload(false);
            return map;
        }

        private static IEnumerator m_GetABDecryptMap(Action<ABDecryptMap> cb)
        {
            string url = "file://" + outputPath + "ETextureMap" + AppConst.ABExtName;
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            byte[] bytes = www.downloadHandler.data;
            string json = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            ABDecryptMap map = SerializeUtil.ToObject<ABDecryptMap>(json);
            cb(map);
        }

        public static AssetBundleManifest LoadAssetBundleManifest()
        {
            return AssetBundle.LoadFromFile(manifestPath).LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        public static AssetBundle LoadOffsetAssetBundle(string abName)
        {
            string encryptKey = "asdfasdfjklasdfargaergdcvjirnfioajsdviojaoiperjf";
            ulong offset = (ulong)Encoding.UTF8.GetBytes(encryptKey).Length;
            AssetBundle assetBundle = AssetBundle.LoadFromFile(outputPath + abName, 0, offset);
            return assetBundle;
        }

        public static AssetBundle LoadOffsetAssetBundle(string abName, string encryptKey)
        {
            ulong offset = (ulong)Encoding.UTF8.GetBytes(encryptKey).Length;
            AssetBundle assetBundle = AssetBundle.LoadFromFile(outputPath + abName, 0, offset);
            return assetBundle;
        }

        public static AssetBundle LoadOffsetAssetBundleByPath(string abPath, string encryptKey)
        {
            ulong offset = (ulong)Encoding.UTF8.GetBytes(encryptKey).Length;
            AssetBundle assetBundle = AssetBundle.LoadFromFile(abPath, 0, offset);
            return assetBundle;
        }

        public static AssetBundle LoadOffsetAssetBundleByPath(string abPath, ulong offset)
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile(abPath, 0, offset);
            return assetBundle;
        }
    }
}