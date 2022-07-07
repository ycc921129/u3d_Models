/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.9
*/

using UnityEngine;

namespace FutureCore
{
    public class AssetBundleItem
    {
        public string abName;
        private AssetBundle assetBundle;

        public int Referenced { get; private set; }
        public bool IsStatic { get; private set; }

        public AssetBundleItem(string abName, AssetBundle assetBundle)
        {
            this.abName = abName;
            this.assetBundle = assetBundle;

            Referenced = 0;
            IsStatic = AssetBundleMgr.Instance.IsStaticAssetBundle(abName);
        }

        public AssetBundle GetAssetBundle()
        {
            return assetBundle;
        }

        public void AddReferenced()
        {
            ++Referenced;
        }

        public void RemoveReferenced()
        {
            --Referenced;
        }

        public bool CanUnload()
        {
            return !IsStatic && Referenced <= 0;
        }

        public void Unload(bool unloadAllLoadedObjects)
        {
            assetBundle.Unload(unloadAllLoadedObjects);
            assetBundle = null;
        }
    }
}