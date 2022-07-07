/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.9
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UObject = UnityEngine.Object;

namespace FutureCore
{
    public class AssetBundleLoader : SingletonMono<AssetBundleLoader>
    {
        #region Field
        private AssetBundleMgr mgr = AssetBundleMgr.Instance;
        #endregion

        #region Load
        public void Load<T>(string abName, string assetName, LoadedObjectFunc func, bool isSyncMode) where T : UObject
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Generic;
            req.inLoadType = AssetTypeConst.GenericType;
            req.loadedObjectFunc = func;
            LoadAsset<T>(abName, assetName, req);
        }

        public void LoadObject(string abName, string assetName, LoadedObjectFunc func, bool isSyncMode)
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Object;
            req.inLoadType = AssetTypeConst.UObjectType;
            req.loadedObjectFunc = func;
            LoadAsset<UObject>(abName, assetName, req);
        }

        public void LoadPrefab(string abName, string assetName, LoadedGameObjectFunc func, bool isSyncMode)
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Prefab;
            req.inLoadType = AssetTypeConst.PrefabType;
            req.loadedGameObjectFunc = func;
            LoadAsset<GameObject>(abName, assetName, req);
        }

        public void LoadGameObject(string abName, string assetName, LoadedGameObjectFunc func, bool isSyncMode)
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.GameObject;
            req.inLoadType = AssetTypeConst.GameObjectType;
            req.loadedGameObjectFunc = func;
            LoadAsset<GameObject>(abName, assetName, req);
        }

        public void LoadSprite(string abName, string assetName, LoadedSpriteFunc func, bool isSyncMode)
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Sprite;
            req.inLoadType = AssetTypeConst.SpriteType;
            req.loadedSpriteFunc = func;
            LoadAsset<Sprite>(abName, assetName, req);
        }

        public void LoadTexture(string abName, string assetName, LoadedTextureFunc func, bool isSyncMode)
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Texture;
            req.inLoadType = AssetTypeConst.TextureType;
            req.loadedTextureFunc = func;
            LoadAsset<Texture>(abName, assetName, req);
        }

        public void LoadAudioClip(string abName, string assetName, LoadedAudioClipFunc func, bool isSyncMode)
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.AudioClip;
            req.inLoadType = AssetTypeConst.AudioClipType;
            req.loadedAudioClipFunc = func;
            LoadAsset<AudioClip>(abName, assetName, req);
        }

        public void LoadTextAsset(string abName, string assetName, LoadedTextAssetFunc func, bool isSyncMode)
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.TextAsset;
            req.inLoadType = AssetTypeConst.TextAssetType;
            req.loadedTextAssetFunc = func;
            LoadAsset<TextAsset>(abName, assetName, req);
        }

        public void LoadMaterial(string abName, string assetName, LoadedMaterialFunc func, bool isSyncMode)
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Material;
            req.inLoadType = AssetTypeConst.MaterialType;
            req.loadedMaterialFunc = func;
            LoadAsset<Material>(abName, assetName, req);
        }

        public void LoadShader(string abName, string assetName, LoadedShaderFunc func, bool isSyncMode)
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Shader;
            req.inLoadType = AssetTypeConst.ShaderType;
            req.loadedShaderFunc = func;
            LoadAsset<Shader>(abName, assetName, req);
        }

        public void LoadFont(string abName, string assetName, LoadedFontFunc func, bool isSyncMode)
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Shader;
            req.inLoadType = AssetTypeConst.ShaderType;
            req.loadedFontFunc = func;
            LoadAsset<Font>(abName, assetName, req);
        }

        public void LoadVideoClip(string abName, string assetName, LoadedVideoClipFunc func, bool isSyncMode)
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.VideoClip;
            req.inLoadType = AssetTypeConst.VideoClipType;
            req.LoadedVideoClipFunc = func;
            LoadAsset<VideoClip>(abName, assetName, req);
        }

        public void LoadAnimatorController(string abName, string assetName, LoadedRuntimeAnimatorControllerFunc func, bool isSyncMode)
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.RuntimeAnimatorController;
            req.inLoadType = AssetTypeConst.RuntimeAnimatorControllerType;
            req.loadedRuntimeAnimatorControllerFunc = func;
            LoadAsset<RuntimeAnimatorController>(abName, assetName, req);
        }

        public void LoadScriptableObject(string abName, string assetName, LoadedScriptableObjectFunc func, bool isSyncMode)
        {
            ABLoadRequest req = new ABLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.ScriptableObject;
            req.inLoadType = AssetTypeConst.ScriptableObjectType;
            req.loadedScriptableObjectFunc = func;
            LoadAsset<ScriptableObject>(abName, assetName, req);
        }
        #endregion

        #region SyncLoad
        public T SyncLoad<T>(string abName, string assetName) where T : UObject
        {
            return SyncLoadAsset<T>(abName, assetName);
        }

        public UObject SyncLoadObject(string abName, string assetName)
        {
            return SyncLoadAsset<UObject>(abName, assetName);
        }

        public GameObject SyncLoadPrefab(string abName, string assetName)
        {
            return SyncLoadAsset<GameObject>(abName, assetName);
        }

        public Sprite SyncLoadSprite(string abName, string assetName)
        {
            return SyncLoadAsset<Sprite>(abName, assetName);
        }

        public Texture SyncLoadTexture(string abName, string assetName)
        {
            return SyncLoadAsset<Texture>(abName, assetName);
        }

        public AudioClip SyncLoadAudioClip(string abName, string assetName)
        {
            return SyncLoadAsset<AudioClip>(abName, assetName);
        }

        public TextAsset SyncLoadTextAsset(string abName, string assetName)
        {
            return SyncLoadAsset<TextAsset>(abName, assetName);
        }

        public Material SyncLoadMaterial(string abName, string assetName)
        {
            return SyncLoadAsset<Material>(abName, assetName);
        }

        public Shader SyncLoadShader(string abName, string assetName)
        {
            return SyncLoadAsset<Shader>(abName, assetName);
        }

        public Font SyncLoadFont(string abName, string assetName)
        {
            return SyncLoadAsset<Font>(abName, assetName);
        }

        public VideoClip SyncLoadVideoClip(string abName, string assetName)
        {
            return SyncLoadAsset<VideoClip>(abName, assetName);
        }

        public RuntimeAnimatorController SyncLoadAnimatorController(string abName, string assetName)
        {
            return SyncLoadAsset<RuntimeAnimatorController>(abName, assetName);
        }

        public ScriptableObject SyncLoadScriptableObject(string abName, string assetName)
        {
            return SyncLoadAsset<ScriptableObject>(abName, assetName);
        }
        #endregion

        #region Private Load
        /// <summary>
        /// 加载资源
        /// </summary>
        private void LoadAsset<T>(string abName, string assetName, ABLoadRequest loadReq) where T : UObject
        {
            abName = mgr.GetCheckABName(abName);
            loadReq.abName = abName;
            loadReq.assetName = assetName;

            if (loadReq.abName == null)
            {
                loadReq.OnLoaded(null);
                return;
            }
            // Sync
            if (loadReq.isSyncMode)
            {
                T asset = SyncLoadAsset<T>(abName, assetName);
                loadReq.OnLoaded(asset);
                return;
            }
            // Async
            List<ABLoadRequest> loadReqs = null;
            if (!mgr.loadReqDic.TryGetValue(abName, out loadReqs))
            {
                loadReqs = new List<ABLoadRequest>();
                loadReqs.Add(loadReq);
                mgr.loadReqDic.Add(abName, loadReqs);
                StartCoroutine(OnGetAssetBundle(abName));
            }
            else
            {
                loadReqs.Add(loadReq);
            }
        }

        /// <summary>
        /// 获取AB包
        /// </summary>
        private IEnumerator OnGetAssetBundle(string abName)
        {
            AssetBundleItem abItem = mgr.GetLoadedAssetBundle(abName);
            if (abItem == null)
            {
                yield return StartCoroutine(OnLoadAssetBundle(abName));

                abItem = mgr.GetLoadedAssetBundle(abName);
                if (abItem == null)
                {
                    mgr.loadReqDic.Remove(abName);
                    LogUtil.LogError("[AssetBundleLoader]OnGetAssetBundle Error: Dependency Incomplete " + abName);
                    yield break;
                }
            }

            AssetBundle ab = abItem.GetAssetBundle();
            List<ABLoadRequest> loadReqs = null;
            if (!mgr.loadReqDic.TryGetValue(abName, out loadReqs))
            {
                mgr.loadReqDic.Remove(abName);
                LogUtil.LogError("[AssetBundleLoader]OnGetAssetBundle Error: LoadReqs Is Null " + abName);
                yield break;
            }

            for (int i = 0; i < loadReqs.Count; i++)
            {
                ABLoadRequest loadReq = loadReqs[i];
                string assetName = loadReq.assetName;
                AssetBundleRequest request = ab.LoadAssetAsync(assetName, loadReq.inLoadType);
                yield return request;
                UObject asset = request.asset;
                CheckAsset(asset, abName, assetName);
                abItem.AddReferenced();
                loadReq.OnLoaded(asset);
            }
            mgr.loadReqDic.Remove(abName);
        }

        /// <summary>
        /// 加载AB包
        /// </summary>
        private IEnumerator OnLoadAssetBundle(string abName)
        {
            string path = mgr.GetFullLoadPath(abName);
            yield return OnLoadAssetBundleDependencies(abName);

            AssetBundleCreateRequest asyncReq = AssetBundle.LoadFromFileAsync(path);
            yield return asyncReq;

            AssetBundle assetBundle = asyncReq.assetBundle;
            AssetBundleItem abItem = new AssetBundleItem(abName, assetBundle);
            mgr.loadedABItemDic.Add(abName, abItem);
        }

        /// <summary>
        /// 加载依赖AB包
        /// </summary>
        private IEnumerator OnLoadDepAssetBundle(string abName)
        {
            string path = mgr.GetFullLoadPath(abName);
            yield return OnLoadAssetBundleDependencies(abName);

            AssetBundleCreateRequest asyncReq = AssetBundle.LoadFromFileAsync(path);
            yield return asyncReq;

            AssetBundle assetBundle = asyncReq.assetBundle;
            AssetBundleItem abItem = new AssetBundleItem(abName, assetBundle);
            mgr.loadedABItemDic.Add(abName, abItem);

            mgr.loadDepABReqList.Remove(abName);
        }

        /// <summary>
        /// 加载AB所有依赖AB包
        /// </summary>
        private IEnumerator OnLoadAssetBundleDependencies(string abName)
        {
            string[] dependencies = mgr.GetABAllDependencies(abName);
            if (dependencies.Length > 0)
            {
                mgr.dependencieDic.Add(abName, dependencies);
                for (int i = 0; i < dependencies.Length; i++)
                {
                    string depABName = dependencies[i];
                    bool isLoading = false;
                    // 未加载完成
                    if (!mgr.loadedABItemDic.ContainsKey(depABName))
                    {
                        // 未正在加载
                        if (!mgr.loadReqDic.ContainsKey(depABName))
                        {
                            // 依赖队列未正在加载
                            if (!mgr.loadDepABReqList.Contains(depABName))
                            {
                                mgr.loadDepABReqList.Add(depABName);
                                yield return StartCoroutine(OnLoadDepAssetBundle(depABName));
                            }
                            else
                            {
                                isLoading = true;
                            }
                        }
                        else
                        {
                            isLoading = true;
                        }
                    }
                    // 正在加载中
                    if (isLoading)
                    {
                        while (!mgr.loadedABItemDic.ContainsKey(depABName))
                        {
                            yield return YieldConst.WaitForEndOfFrame;
                        }
                    }
                    // 加载完成
                    AssetBundleItem abItem = null;
                    if (mgr.loadedABItemDic.TryGetValue(depABName, out abItem))
                    {
                        abItem.AddReferenced();
                    }
                }
            }
        }
        #endregion

        #region Private SyncLoad
        /// <summary>
        /// 同步加载资源
        /// </summary>
        private T SyncLoadAsset<T>(string abName, string assetName) where T : UObject
        {
            abName = mgr.GetCheckABName(abName);
            if (abName == null)
            {
                return null;
            }
            T asset = SyncGetAssetBundle<T>(abName, assetName);
            return asset;
        }

        /// <summary>
        /// 同步获取AB包
        /// </summary>
        private T SyncGetAssetBundle<T>(string abName, string assetName) where T : UObject
        {
            AssetBundleItem abItem = mgr.GetLoadedAssetBundle(abName);
            if (abItem == null)
            {
                SyncLoadAssetBundle(abName);

                abItem = mgr.GetLoadedAssetBundle(abName);
                if (abItem == null)
                {
                    LogUtil.LogError("[AssetBundleLoader]SyncGetAssetBundle Error: " + abName);
                    return null;
                }
            }

            AssetBundle ab = abItem.GetAssetBundle();
            T asset = ab.LoadAsset<T>(assetName);
            CheckAsset(asset, abName, assetName);
            abItem.AddReferenced();
            return asset;
        }

        /// <summary>
        /// 同步加载AB包
        /// </summary>
        private void SyncLoadAssetBundle(string abName)
        {
            string path = mgr.GetFullLoadPath(abName);
            SyncLoadAssetBundleDependencies(abName);

            AssetBundle assetBundle = AssetBundle.LoadFromFile(path);
            AssetBundleItem abItem = new AssetBundleItem(abName, assetBundle);
            mgr.loadedABItemDic.Add(abName, abItem);
        }

        /// <summary>
        /// 同步加载AB所有依赖AB包
        /// </summary>
        private void SyncLoadAssetBundleDependencies(string abName)
        {
            string[] dependencies = mgr.GetABAllDependencies(abName);
            if (dependencies.Length > 0)
            {
                mgr.dependencieDic.Add(abName, dependencies);
                for (int i = 0; i < dependencies.Length; i++)
                {
                    string depABName = dependencies[i];
                    // 未加载完成
                    if (!mgr.loadedABItemDic.ContainsKey(depABName))
                    {
                        SyncLoadAssetBundle(depABName);
                    }
                    // 加载完成
                    AssetBundleItem abItem = null;
                    if (mgr.loadedABItemDic.TryGetValue(depABName, out abItem))
                    {
                        abItem.AddReferenced();
                    }
                }
            }
        }
        #endregion

        #region AB
        public AssetBundle SyncLoadSingleAB(string abName)
        {
            string assetPath = mgr.GetFullLoadPath(abName);
            return AssetBundle.LoadFromFile(assetPath);
        }

        public T SyncLoadAssetFormAB<T>(AssetBundle assetBundle, string assetName) where T : UObject
        {
            return assetBundle.LoadAsset<T>(assetName);
        }

        public T SyncLoadAssetFormAB<T>(string abName, string assetName) where T : UObject
        {
            string assetPath = mgr.GetFullLoadPath(abName);
            AssetBundle assetBundle = AssetBundle.LoadFromFile(assetPath);
            T asset = assetBundle.LoadAsset<T>(assetName);
            CheckAsset(asset, abName, assetName);
            return asset;
        }
        #endregion

        #region Func
        /// <summary>
        /// 检测资源
        /// </summary>
        private void CheckAsset<T>(T asset, string abName, string assetName) where T : UObject
        {
            if (asset == null)
            {
                LogUtil.LogErrorFormat("[AssetBundleLoader]Loaded Asset Is Null:{0}->{1}", abName, assetName);
            }
        }
        #endregion

        #region Base
        protected override string ParentRootName
        {
            get
            {
                return AppObjConst.LoaderGoName;
            }
        }
        #endregion
    }
}