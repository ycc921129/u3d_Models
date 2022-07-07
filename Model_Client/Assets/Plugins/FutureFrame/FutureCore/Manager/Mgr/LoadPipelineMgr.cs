/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.30
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UObject = UnityEngine.Object;

namespace FutureCore
{
    public sealed class LoadPipelineMgr : BaseMgr<LoadPipelineMgr>
    {
        private Dictionary<int, AssetLoader> preLoaderDic = new Dictionary<int, AssetLoader>();
        private bool isPreLoading;

        private Dictionary<string, UAssetType> preLoadAssetDict;
        private int totalPreLoadCount;
        private int preLoadCompleteCount;
        private int frameMaxLoadCount;

        public delegate void VoidPreLoadDelegate(int curCount, int totalCount);
        private VoidPreLoadDelegate onPreLoadCB;
        private Action onPreLoadComplete;

        private int currPreLoadId;

        public override void Init()
        {
            base.Init();
            frameMaxLoadCount = AppUtil.GetPreMaxLoadCount();
        }

        public override void Dispose()
        {
            base.Dispose();
            preLoaderDic.Clear();
            preLoaderDic = null;
        }

        #region PreLoad

        public void PreLoad(int preLoadId, AssetLoader loader, Dictionary<string, UAssetType> preLoadAssetDict, VoidPreLoadDelegate onPreLoadCB, Action onPreLoadComplete)
        {
            if (isPreLoading)
            {
                LogUtil.LogError("[LoadPipelineMgr]Error:PreLoading Now");
                return;
            }
            if (preLoaderDic.ContainsKey(preLoadId))
            {
                LogUtil.LogError("[LoadPipelineMgr]Error:Repeat PreLoad " + preLoadId);
                return;
            }

            LogUtil.Log("[LoadPipelineMgr]PreLoad: " + currPreLoadId);

            this.onPreLoadComplete = onPreLoadComplete;
            if (preLoadAssetDict == null || preLoadAssetDict.Count == 0)
            {
                AllPreLoadComplete();
                return;
            }

            isPreLoading = true;
            currPreLoadId = preLoadId;
            this.preLoadAssetDict = preLoadAssetDict;
            totalPreLoadCount = preLoadAssetDict.Count;
            this.onPreLoadCB = onPreLoadCB;
            preLoaderDic[currPreLoadId] = loader;
            CoroutineMgr.Instance.StartRoutine(OnStartPreLoad(loader));
        }

        private IEnumerator OnStartPreLoad(AssetLoader loader)
        {
            yield return YieldConst.WaitForEndOfFrame;
            int i = 0;
            foreach (string assetPath in preLoadAssetDict.Keys)
            {
                if (i % frameMaxLoadCount == 0)
                {
                    yield return YieldConst.WaitForEndOfFrame;
                }
                string currLoadPath = assetPath;
                loader.LoadTypeAsset(currLoadPath, preLoadAssetDict[currLoadPath], (asset) => OnOncePreLoadComplete(currLoadPath, asset));
                i++;
            }
        }

        private void OnOncePreLoadComplete(string currLoadPath, UObject asset)
        {
            preLoadCompleteCount++;
            ResMgr.Instance.AddCache(currLoadPath, asset);
            onPreLoadCB(preLoadCompleteCount, totalPreLoadCount);

            if (preLoadCompleteCount == totalPreLoadCount)
            {
                AllPreLoadComplete();
            }
        }

        private void AllPreLoadComplete()
        {
            LogUtil.Log("[LoadPipelineMgr]AllPreLoadComplete: " + currPreLoadId);

            isPreLoading = false;
            preLoadCompleteCount = 0;
            onPreLoadComplete();
            onPreLoadComplete = null;
        }

        public void UnloadPreLoad(int preLoadId)
        {
            AssetLoader loader = null;
            if (preLoaderDic.TryGetValue(preLoadId, out loader))
            {
                loader.ClearCache();
                loader.Release();
                preLoaderDic.Remove(preLoadId);
            }
        }

        public int GetCurrPreLoadId()
        {
            return currPreLoadId;
        }

        #endregion PreLoad

        #region PermanentAssets

        private int loadPermanentCompleteCount;
        private int totalLoadPermanentCount;
        private Action onAllLoadPermanentComplete;

        public void LoadPermanentAssets(Dictionary<string, UAssetType> permanentAssets, Action onAllLoadPermanentComplete)
        {
            LogUtil.Log("[LoadPipelineMgr]LoadPermanentAssets");

            this.onAllLoadPermanentComplete = onAllLoadPermanentComplete;
            if (permanentAssets == null && permanentAssets.Count == 0)
            {
                AllLoadPermanentComplete();
                return;
            }

            totalLoadPermanentCount = permanentAssets.Count;
            CoroutineMgr.Instance.StartRoutine(OnStartLoadPermanent(permanentAssets));
        }

        private IEnumerator OnStartLoadPermanent(Dictionary<string, UAssetType> permanentAssets)
        {
            yield return YieldConst.WaitForEndOfFrame;
            int i = 0;
            foreach (string assetPath in permanentAssets.Keys)
            {
                if (i % frameMaxLoadCount == 0)
                {
                    yield return YieldConst.WaitForEndOfFrame;
                }
                string currLoadPath = assetPath;
                ResMgr.Loader.LoadTypeAsset(currLoadPath, permanentAssets[currLoadPath], (asset) => OnOnceLoadPermanentComplete(currLoadPath, asset));
                i++;
            }
        }

        private void OnOnceLoadPermanentComplete(string currLoadPath, UObject asset)
        {
            loadPermanentCompleteCount++;
            ResMgr.Instance.AddCache(currLoadPath, asset);

            if (loadPermanentCompleteCount == totalLoadPermanentCount)
            {
                AllLoadPermanentComplete();
            }
        }

        private void AllLoadPermanentComplete()
        {
            LogUtil.Log("[LoadPipelineMgr]AllLoadPermanentComplete");

            loadPermanentCompleteCount = 0;
            onAllLoadPermanentComplete();
            onAllLoadPermanentComplete = null;
        }

        #endregion PermanentAssets
    }
}