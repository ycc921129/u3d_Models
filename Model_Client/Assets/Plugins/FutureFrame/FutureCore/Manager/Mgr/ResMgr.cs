/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.13
*/

using System;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;
using UObject = UnityEngine.Object;

namespace FutureCore
{
    public sealed class ResMgr : BaseMgr<ResMgr>
    {
        #region Field
        private const bool IsResourcesMode = AppConst.IsResourcesMode_Default;
        private const bool IsSyncMode = AppConst.IsSyncLoadMode_Default;

        private static AssetLoader m_loader;
        public static AssetLoader Loader
        {
            get
            {
                if (m_loader == null)
                    m_loader = new AssetLoader();
                return m_loader;
            }
        }

        private Dictionary<string, UAssetType> permanentAssetDict = new Dictionary<string, UAssetType>();
        private Dictionary<string, UObject> assetsCacheDict = new Dictionary<string, UObject>();
        private Dictionary<string, UObject> assetsDynamicCacheDict = new Dictionary<string, UObject>();

        // 加密包
        public const bool IsUseEncryptTexture = false;
        public ABDecryptMap textureDecryptABMap;
        public AssetBundle textureDecryptAB;
        #endregion

        #region Init
        protected override void New()
        {
            base.New();
            if (IsUseEncryptTexture)
            {
                textureDecryptABMap = ABDecryptUtil.GetABDecryptMap();
                foreach (string abName in textureDecryptABMap.abDict.Keys)
                {
                    textureDecryptAB = ABDecryptUtil.LoadOffsetAssetBundle(abName, textureDecryptABMap.abDict[abName]);
                    AddFguiPackage(textureDecryptAB);
                    return;
                }
            }
        }

        public override void Init()
        {
            base.Init();
            Application.lowMemory -= OnLowMemoryGC;
            Application.lowMemory += OnLowMemoryGC;
        }

        public void InitAssets()
        {
            if (!IsResourcesMode)
            {
                AssetBundleMgr.Instance.InitAssetBundle(()=> AppDispatcher.Instance.Dispatch(AppMsg.System_AssetsInitComplete));
            }
            else
            {
                AppDispatcher.Instance.Dispatch(AppMsg.System_AssetsInitComplete);
            }
        }

        public void InitPermanentAssets()
        {
            LoadPipelineMgr.Instance.LoadPermanentAssets(permanentAssetDict, () =>
            {
                GraphicsMgr.Instance.InitPermanentGraphics();
                AudioMgr.Instance.InitPermanentAudioClip();
                AppDispatcher.Instance.Dispatch(AppMsg.System_PermanentAssetsInitComplete);
            });
        }

        public void SetPermanentAssets(Dictionary<string, UAssetType> permanentAssetDict)
        {
            this.permanentAssetDict = permanentAssetDict;
        }

        public Dictionary<string, UAssetType> GetPermanentAssets()
        {
            return permanentAssetDict;
        }
        #endregion

        #region Fgui
        public UIPackage AddFguiPackage(string packageName, string resUIPath)
        {
            if (!IsUseEncryptTexture)
            {
                return UIPackage.AddPackage(resUIPath);
            }
            else
            {
                packageName = packageName + "_fui" + AppConst.ABExtName;
                return UIPackage.AddPackage(textureDecryptAB, textureDecryptAB, packageName);
            }
        }

        private UIPackage AddFguiPackage(AssetBundle assetBundle)
        {
            return UIPackage.AddPackage(assetBundle);
        }
        #endregion

        #region Load
        public void Load<T>(string assetPath, LoadedObjectFunc func, bool isSync = IsSyncMode) where T : UObject
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.Load<T>(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.Load<T>(pakName, assetName, func, isSync);
            }
        }

        public void LoadObject(string assetPath, LoadedObjectFunc func, bool isSync = IsSyncMode)
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.LoadObject(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.LoadObject(pakName, assetName, func, isSync);
            }
        }

        public void LoadPrefab(string assetPath, LoadedGameObjectFunc func, bool isSync = IsSyncMode)
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.LoadPrefab(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.LoadPrefab(pakName, assetName, func, isSync);
            }
        }

        public void LoadGameObject(string assetPath, LoadedGameObjectFunc func, bool isSync = IsSyncMode)
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.LoadGameObject(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.LoadGameObject(pakName, assetName, func, isSync);
            }
        }

        public void LoadSprite(string assetPath, LoadedSpriteFunc func, bool isSync = IsSyncMode)
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.LoadSprite(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.LoadSprite(pakName, assetName, func, isSync);
            }
        }

        public void LoadTexture(string assetPath, LoadedTextureFunc func, bool isSync = IsSyncMode)
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.LoadTexture(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.LoadTexture(pakName, assetName, func, isSync);
            }
        }

        public void LoadAudioClip(string assetPath, LoadedAudioClipFunc func, bool isSync = IsSyncMode)
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.LoadAudioClip(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.LoadAudioClip(pakName, assetName, func, isSync);
            }
        }

        public void LoadTextAsset(string assetPath, LoadedTextAssetFunc func, bool isSync = IsSyncMode)
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.LoadTextAsset(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.LoadTextAsset(pakName, assetName, func, isSync);
            }
        }

        public void LoadMaterial(string assetPath, LoadedMaterialFunc func, bool isSync = IsSyncMode)
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.LoadMaterial(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.LoadMaterial(pakName, assetName, func, isSync);
            }
        }

        public void LoadShader(string assetPath, LoadedShaderFunc func, bool isSync = IsSyncMode)
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.LoadShader(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.LoadShader(pakName, assetName, func, isSync);
            }
        }

        public void LoadFont(string assetPath, LoadedFontFunc func, bool isSync = IsSyncMode)
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.LoadFont(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.LoadFont(pakName, assetName, func, isSync);
            }
        }

        public void LoadVideoClip(string assetPath, LoadedVideoClipFunc func, bool isSync = IsSyncMode)
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.LoadVideoClip(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.LoadVideoClip(pakName, assetName, func, isSync);
            }
        }

        public void LoadAnimatorController(string assetPath, LoadedRuntimeAnimatorControllerFunc func, bool isSync = IsSyncMode)
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.LoadAnimatorController(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.LoadAnimatorController(pakName, assetName, func, isSync);
            }
        }

        public void LoadScriptableObject(string assetPath, LoadedScriptableObjectFunc func, bool isSync = IsSyncMode)
        {
            if (IsResourcesMode)
            {
                ResourcesLoader.Instance.LoadScriptableObject(assetPath, func, isSync);
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AssetBundleLoader.Instance.LoadScriptableObject(pakName, assetName, func, isSync);
            }
        }
        #endregion

        #region SyncLoad
        public T SyncLoad<T>(string assetPath) where T : UObject
        {
            if (IsResourcesMode)
            {
                T asset = ResourcesLoader.Instance.SyncLoad<T>(assetPath);
                return asset;
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                T asset = AssetBundleLoader.Instance.SyncLoad<T>(pakName, assetName);
                return asset;
            }
        }

        public UObject SyncLoadObject(string assetPath)
        {
            if (IsResourcesMode)
            {
                UObject asset = ResourcesLoader.Instance.SyncLoadObject(assetPath);
                return asset;
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                UObject asset = AssetBundleLoader.Instance.SyncLoadObject(pakName, assetName);
                return asset;
            }
        }

        public GameObject SyncLoadPrefab(string assetPath)
        {
            if (IsResourcesMode)
            {
                GameObject asset = ResourcesLoader.Instance.SyncLoadPrefab(assetPath);
                return asset;
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                GameObject asset = AssetBundleLoader.Instance.SyncLoadPrefab(pakName, assetName);
                return asset;
            }
        }

        public GameObject SyncLoadGameObject(string assetPath)
        {
            GameObject prefab = SyncLoadPrefab(assetPath);
            GameObject gameObject = EngineUtil.Instantiate(prefab);
            return gameObject;
        }

        public Sprite SyncLoadSprite(string assetPath)
        {
            if (IsResourcesMode)
            {
                Sprite asset = ResourcesLoader.Instance.SyncLoadSprite(assetPath);
                return asset;
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                Sprite asset = AssetBundleLoader.Instance.SyncLoadSprite(pakName, assetName);
                return asset;
            }
        }

        public Texture SyncLoadTexture(string assetPath)
        {
            if (IsResourcesMode)
            {
                Texture asset = ResourcesLoader.Instance.SyncLoadTexture(assetPath);
                return asset;
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                Texture asset = AssetBundleLoader.Instance.SyncLoadTexture(pakName, assetName);
                return asset;
            }
        }

        public AudioClip SyncLoadAudioClip(string assetPath)
        {
            if (IsResourcesMode)
            {
                AudioClip asset = ResourcesLoader.Instance.SyncLoadAudioClip(assetPath);
                return asset;
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                AudioClip asset = AssetBundleLoader.Instance.SyncLoadAudioClip(pakName, assetName);
                return asset;
            }
        }

        public TextAsset SyncLoadTextAsset(string assetPath)
        {
            if (IsResourcesMode)
            {
                TextAsset asset = ResourcesLoader.Instance.SyncLoadTextAsset(assetPath);
                return asset;
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                TextAsset asset = AssetBundleLoader.Instance.SyncLoadTextAsset(pakName, assetName);
                return asset;
            }
        }

        public Material SyncLoadMaterial(string assetPath)
        {
            if (IsResourcesMode)
            {
                Material asset = ResourcesLoader.Instance.SyncLoadMaterial(assetPath);
                return asset;
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                Material asset = AssetBundleLoader.Instance.SyncLoadMaterial(pakName, assetName);
                return asset;
            }
        }

        public Shader SyncLoadShader(string assetPath)
        {
            if (IsResourcesMode)
            {
                Shader asset = ResourcesLoader.Instance.SyncLoadShader(assetPath);
                return asset;
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                Shader asset = AssetBundleLoader.Instance.SyncLoadShader(pakName, assetName);
                return asset;
            }
        }

        public Font SyncLoadFont(string assetPath)
        {
            if (IsResourcesMode)
            {
                Font asset = ResourcesLoader.Instance.SyncLoadFont(assetPath);
                return asset;
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                Font asset = AssetBundleLoader.Instance.SyncLoadFont(pakName, assetName);
                return asset;
            }
        }

        public VideoClip SyncLoadVideoClip(string assetPath)
        {
            if (IsResourcesMode)
            {
                VideoClip asset = ResourcesLoader.Instance.SyncLoadVideoClip(assetPath);
                return asset;
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                VideoClip asset = AssetBundleLoader.Instance.SyncLoadVideoClip(pakName, assetName);
                return asset;
            }
        }

        public RuntimeAnimatorController SyncLoadAnimatorController(string assetPath)
        {
            if (IsResourcesMode)
            {
                RuntimeAnimatorController asset = ResourcesLoader.Instance.SyncLoadAnimatorController(assetPath);
                return asset;
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                RuntimeAnimatorController asset = AssetBundleLoader.Instance.SyncLoadAnimatorController(pakName, assetName);
                return asset;
            }
        }

        public ScriptableObject SyncLoadScriptableObject(string assetPath)
        {
            if (IsResourcesMode)
            {
                ScriptableObject asset = ResourcesLoader.Instance.SyncLoadScriptableObject(assetPath);
                return asset;
            }
            else
            {
                string pakName, assetName;
                GetPakAndAssetName(assetPath, out pakName, out assetName);
                ScriptableObject asset = AssetBundleLoader.Instance.SyncLoadScriptableObject(pakName, assetName);
                return asset;
            }
        }
        #endregion

        #region CacheLoad
        public GameObject GetInCacheGameObject(string assetPath)
        {
            GameObject prefab = GetInCache<GameObject>(assetPath);
            return EngineUtil.Instantiate(prefab);
        }

        public T GetInCache<T>(string assetPath) where T : UObject
        {
            return assetsCacheDict[assetPath] as T;
        }

        public T GetInDynamicCache<T>(string assetPath) where T : UObject
        {
            return assetsDynamicCacheDict[assetPath] as T;
        }
        #endregion

        #region Cache
        public void AddCache(string assetPath, UObject asset)
        {
            if (!HasCache(assetPath))
            {
                assetsCacheDict[assetPath] = asset;
            }
        }

        public void DelCache(string assetPath)
        {
            if (!HasCache(assetPath))
            {
                LogUtil.LogError("[ResMgr]Del Cache Asset Is Null:" + assetPath);
                return;
            }
            assetsCacheDict.Remove(assetPath);
        }

        public bool HasCache(string assetPath)
        {
            return assetsCacheDict.ContainsKey(assetPath);
        }

        public bool HasDynamicCache(string assetPath)
        {
            return assetsDynamicCacheDict.ContainsKey(assetPath);
        }

        public void AddDynamicCache(string assetPath, UObject asset)
        {
            if (!HasDynamicCache(assetPath))
            {
                assetsDynamicCacheDict[assetPath] = asset;
            }
        }

        public void DelDynamicCache(string assetPath)
        {
            if (!HasDynamicCache(assetPath))
            {
                LogUtil.LogError("[ResMgr]Del Dynamic Asset Cache Is Null:" + assetPath);
                return;
            }
            assetsDynamicCacheDict.Remove(assetPath);
        }
        #endregion

        #region Unload
        public void ClearDynamicCache()
        {
            assetsDynamicCacheDict.Clear();
        }

        public void UnloadNullReferenceAssets()
        {
            if (IsResourcesMode) return;

            AssetBundleMgr.Instance.UnloadNullReferenceAB();
        }

        public void Unload(string assetPath)
        {
            if (IsResourcesMode) return;

            string abPakName = GetABPakName(assetPath);
            AssetBundleMgr.Instance.UnloadAsset(abPakName);
        }

        public void UnloadAssetObj(UObject assetObj)
        {
            Resources.UnloadAsset(assetObj);
        }

        public AsyncOperation GCAssets(bool isSystemGC = false)
        {
            // 清理资源之前，一般不需要调用GC
            if (isSystemGC)
            {
                UnityWebRequest.ClearCookieCache();
                Caching.ClearCache();

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            // 全局静态变量和类成员变量引用的资源，务必先把引用设为Null值，然后再调用Reources.UnloadUnusedAssets才能正确释放。
            return Resources.UnloadUnusedAssets();
        }

        private void OnLowMemoryGC()
        {
            UnityWebRequest.ClearCookieCache();
            Caching.ClearCache();

            Resources.UnloadUnusedAssets();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion

        #region Private
        private void GetPakAndAssetName(string assetPath, out string abPakName, out string assetName)
        {
            abPakName = GetABPakName(assetPath);
            int index = assetPath.LastIndexOf("/") + 1;
            assetName = assetPath.Substring(index);
        }

        private string GetABPakName(string assetPath)
        {
            return ABPakUtil.GetABPak(assetPath);
        }

        private void DisposeLoader()
        {
            if (null != m_loader)
            {
                m_loader.Dispose();
                m_loader = null;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            DisposeLoader();
            assetsCacheDict.Clear();
            assetsDynamicCacheDict.Clear();
            assetsCacheDict = null;
            assetsDynamicCacheDict = null;
        }
        #endregion

        #region Info
        public UObject[] GetAllObjectInfo()
        {
            UObject[] objects = Resources.FindObjectsOfTypeAll<UObject>();
            return objects;
        }

        public List<string> GetCacheAssetsInfo()
        {
            List<string> infos = new List<string>();
            infos.AddRange(assetsCacheDict.Keys);
            return infos;
        }

        public List<string> GetDynamicAssetsCacheInfo()
        {
            List<string> infos = new List<string>();
            infos.AddRange(assetsDynamicCacheDict.Keys);
            return infos;
        }
        #endregion

        #region EncryptAssetBundle
        public AssetBundle LoadEncryptAB(string abName)
        {
            string abPath = Application.streamingAssetsPath + "/_AssetBundles/Android/assets/_res/assetbundle/" + abName;
            AssetBundle ab = ABDecryptUtil.LoadOffsetAssetBundleByPath(abPath, "asdfasdfjklasdfargaergdcvjirnfioajsdviojaoiperjf");
            return ab;
        }

        public GameObject EncryptABSyncLoadGameObject(AssetBundle ab, string path)
        {
            string name = path.Split('/')[1];
            GameObject go = UObject.Instantiate(ab.LoadAsset<GameObject>(name));
            EngineUtil.AddFixABShader(go);
            return go;
        }
        #endregion
    }
}