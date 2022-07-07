/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UObject = UnityEngine.Object;

namespace FutureCore
{
    [Serializable]
    public class AssetLoader
    {
        #region Field
        private const bool IsResourcesMode = AppConst.IsResourcesMode_Default;
        private const bool IsSyncMode = AppConst.IsSyncLoadMode_Default;

        private ResMgr mgr = ResMgr.Instance;

        [SerializeField]
        private List<string> loadedPaths;
        #endregion

        #region Init
        public AssetLoader()
        {
            loadedPaths = new List<string>();
        }
        #endregion

        #region Load
        public void LoadTypeAsset(string assetPath, UAssetType uAssetType, LoadedObjectFunc func, bool isSync = IsSyncMode)
        {
            switch (uAssetType)
            {
                case UAssetType.Object:
                    LoadObject(assetPath, func, isSync);
                    return;
                case UAssetType.Prefab:
                    LoadPrefab(assetPath, (asset) => func(asset), isSync);
                    return;
                case UAssetType.GameObject:
                    LoadGameObject(assetPath, (asset) => func(asset), isSync);
                    return;
                case UAssetType.Sprite:
                    LoadSprite(assetPath, (asset) => func(asset), isSync);
                    return;
                case UAssetType.Texture:
                    LoadTexture(assetPath, (asset) => func(asset), isSync);
                    return;
                case UAssetType.AudioClip:
                    LoadAudioClip(assetPath, (asset) => func(asset), isSync);
                    return;
                case UAssetType.TextAsset:
                    LoadTextAsset(assetPath, (asset) => func(asset), isSync);
                    return;
                case UAssetType.Material:
                    LoadMaterial(assetPath, (asset) => func(asset), isSync);
                    return;
                case UAssetType.Shader:
                    LoadShader(assetPath, (asset) => func(asset), isSync);
                    return;
                case UAssetType.Font:
                    LoadFont(assetPath, (asset) => func(asset), isSync);
                    return;
                case UAssetType.VideoClip:
                    LoadVideoClip(assetPath, (asset) => func(asset), isSync);
                    return;
                case UAssetType.RuntimeAnimatorController:
                    LoadAnimatorController(assetPath, (asset) => func(asset), isSync);
                    return;
                case UAssetType.ScriptableObject:
                    LoadScriptableObject(assetPath, (asset) => func(asset), isSync);
                    return;
            }
        }

        private void Load<T>(string assetPath, LoadedObjectFunc func, bool isSync = IsSyncMode) where T : UObject
        {
            if (mgr.HasCache(assetPath))
            {
                T cacheAsset = GetInCache<T>(assetPath);
                func(cacheAsset);
                return;
            }

            mgr.Load<T>(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }

        private void LoadObject(string assetPath, LoadedObjectFunc func, bool isSync = IsSyncMode)
        {
            if (mgr.HasCache(assetPath))
            {
                UObject cacheAsset = GetInCache<UObject>(assetPath);
                func(cacheAsset);
                return;
            }

            mgr.LoadObject(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }

        public void LoadPrefab(string assetPath, LoadedGameObjectFunc func, bool isSync = IsSyncMode)
        {
            if (mgr.HasCache(assetPath))
            {
                GameObject cacheAsset = GetInCache<GameObject>(assetPath);
                func(cacheAsset);
                return;
            }

            mgr.LoadPrefab(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }

        public void LoadGameObject(string assetPath, LoadedGameObjectFunc func, bool isSync = IsSyncMode)
        {
            if (mgr.HasCache(assetPath))
            {
                GameObject cacheAsset = GetInCache<GameObject>(assetPath);
                GameObject gameObject = EngineUtil.Instantiate(cacheAsset);
                func(gameObject);
                return;
            }

            mgr.LoadGameObject(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }

        public void LoadSprite(string assetPath, LoadedSpriteFunc func, bool isSync = IsSyncMode)
        {
            if (mgr.HasCache(assetPath))
            {
                Sprite cacheAsset = GetInCache<Sprite>(assetPath);
                func(cacheAsset);
                return;
            }

            mgr.LoadSprite(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }

        public void LoadTexture(string assetPath, LoadedTextureFunc func, bool isSync = IsSyncMode)
        {
            if (mgr.HasCache(assetPath))
            {
                Texture cacheAsset = GetInCache<Texture>(assetPath);
                func(cacheAsset);
                return;
            }

            mgr.LoadTexture(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }

        public void LoadAudioClip(string assetPath, LoadedAudioClipFunc func, bool isSync = IsSyncMode)
        {
            if (mgr.HasCache(assetPath))
            {
                AudioClip cacheAsset = GetInCache<AudioClip>(assetPath);
                func(cacheAsset);
                return;
            }

            mgr.LoadAudioClip(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }

        public void LoadTextAsset(string assetPath, LoadedTextAssetFunc func, bool isSync = IsSyncMode)
        {
            if (mgr.HasCache(assetPath))
            {
                TextAsset cacheAsset = GetInCache<TextAsset>(assetPath);
                func(cacheAsset);
                return;
            }

            mgr.LoadTextAsset(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }

        public void LoadMaterial(string assetPath, LoadedMaterialFunc func, bool isSync = IsSyncMode)
        {
            if (mgr.HasCache(assetPath))
            {
                Material cacheAsset = GetInCache<Material>(assetPath);
                func(cacheAsset);
                return;
            }

            mgr.LoadMaterial(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }

        public void LoadShader(string assetPath, LoadedShaderFunc func, bool isSync = IsSyncMode)
        {
            if (mgr.HasCache(assetPath))
            {
                Shader cacheAsset = GetInCache<Shader>(assetPath);
                func(cacheAsset);
                return;
            }

            mgr.LoadShader(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }

        public void LoadFont(string assetPath, LoadedFontFunc func, bool isSync = IsSyncMode)
        {
            if (mgr.HasCache(assetPath))
            {
                Font cacheAsset = GetInCache<Font>(assetPath);
                func(cacheAsset);
                return;
            }

            mgr.LoadFont(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }

        public void LoadVideoClip(string assetPath, LoadedVideoClipFunc func, bool isSync = IsSyncMode)
        {
            if (mgr.HasCache(assetPath))
            {
                VideoClip cacheAsset = GetInCache<VideoClip>(assetPath);
                func(cacheAsset);
                return;
            }

            mgr.LoadVideoClip(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }

        public void LoadAnimatorController(string assetPath, LoadedRuntimeAnimatorControllerFunc func, bool isSync = IsSyncMode)
        {
            if (mgr.HasCache(assetPath))
            {
                RuntimeAnimatorController cacheAsset = GetInCache<RuntimeAnimatorController>(assetPath);
                func(cacheAsset);
                return;
            }

            mgr.LoadAnimatorController(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }

        public void LoadScriptableObject(string assetPath, LoadedScriptableObjectFunc func, bool isSync = IsSyncMode)
        {
            if (mgr.HasCache(assetPath))
            {
                ScriptableObject cacheAsset = GetInCache<ScriptableObject>(assetPath);
                func(cacheAsset);
                return;
            }

            mgr.LoadScriptableObject(assetPath, func, isSync);
            AddLoadedPath(assetPath);
        }
        #endregion

        #region SyncLoad
        public UObject SyncLoadTypeAsset(string assetPath, UAssetType uAssetType, bool isSync = IsSyncMode)
        {
            switch (uAssetType)
            {
                case UAssetType.Object:
                    return SyncLoadObject(assetPath);
                case UAssetType.Prefab:
                    return SyncLoadPrefab(assetPath);
                case UAssetType.GameObject:
                    return SyncLoadGameObject(assetPath);
                case UAssetType.Sprite:
                    return SyncLoadSprite(assetPath);
                case UAssetType.Texture:
                    return SyncLoadTexture(assetPath);
                case UAssetType.AudioClip:
                    return SyncLoadAudioClip(assetPath);
                case UAssetType.TextAsset:
                    return SyncLoadTextAsset(assetPath);
                case UAssetType.Material:
                    return SyncLoadMaterial(assetPath);
                case UAssetType.Shader:
                    return SyncLoadShader(assetPath);
                case UAssetType.Font:
                    return SyncLoadFont(assetPath);
                case UAssetType.VideoClip:
                    return SyncLoadVideoClip(assetPath);
                case UAssetType.RuntimeAnimatorController:
                    return SyncLoadAnimatorController(assetPath);
                case UAssetType.ScriptableObject:
                    return SyncLoadScriptableObject(assetPath);
            }
            return null;
        }

        private T SyncLoad<T>(string assetPath) where T : UObject
        {
            if (mgr.HasCache(assetPath))
            {
                T cacheAsset = GetInCache<T>(assetPath);
                return cacheAsset;
            }

            T asset = mgr.SyncLoad<T>(assetPath);
            AddLoadedPath(assetPath);
            return asset;
        }

        private UObject SyncLoadObject(string assetPath)
        {
            if (mgr.HasCache(assetPath))
            {
                UObject cacheAsset = GetInCache<UObject>(assetPath);
                return cacheAsset;
            }

            UObject asset = mgr.SyncLoadObject(assetPath);
            AddLoadedPath(assetPath);
            return asset;
        }

        public GameObject SyncLoadPrefab(string assetPath)
        {
            if (mgr.HasCache(assetPath))
            {
                GameObject cacheAsset = GetInCache<GameObject>(assetPath);
                return cacheAsset;
            }

            GameObject asset = mgr.SyncLoadPrefab(assetPath);
            AddLoadedPath(assetPath);
            return asset;
        }

        public GameObject SyncLoadGameObject(string assetPath)
        {
            GameObject prefab = SyncLoadPrefab(assetPath);
            GameObject gameObject = EngineUtil.Instantiate(prefab);
            return gameObject;
        }

        public Sprite SyncLoadSprite(string assetPath)
        {
            if (mgr.HasCache(assetPath))
            {
                Sprite cacheAsset = GetInCache<Sprite>(assetPath);
                return cacheAsset;
            }

            Sprite asset = mgr.SyncLoadSprite(assetPath);
            AddLoadedPath(assetPath);
            return asset;
        }

        public Texture SyncLoadTexture(string assetPath)
        {
            if (mgr.HasCache(assetPath))
            {
                Texture cacheAsset = GetInCache<Texture>(assetPath);
                return cacheAsset;
            }

            Texture asset = mgr.SyncLoadTexture(assetPath);
            AddLoadedPath(assetPath);
            return asset;
        }

        public AudioClip SyncLoadAudioClip(string assetPath)
        {
            if (mgr.HasCache(assetPath))
            {
                AudioClip cacheAsset = GetInCache<AudioClip>(assetPath);
                return cacheAsset;
            }

            AudioClip asset = mgr.SyncLoadAudioClip(assetPath);
            AddLoadedPath(assetPath);
            return asset;
        }

        public TextAsset SyncLoadTextAsset(string assetPath)
        {
            if (mgr.HasCache(assetPath))
            {
                TextAsset cacheAsset = GetInCache<TextAsset>(assetPath);
                return cacheAsset;
            }

            TextAsset asset = mgr.SyncLoadTextAsset(assetPath);
            AddLoadedPath(assetPath);
            return asset;
        }

        public Material SyncLoadMaterial(string assetPath)
        {
            if (mgr.HasCache(assetPath))
            {
                Material cacheAsset = GetInCache<Material>(assetPath);
                return cacheAsset;
            }

            Material asset = mgr.SyncLoadMaterial(assetPath);
            AddLoadedPath(assetPath);
            return asset;
        }

        public Shader SyncLoadShader(string assetPath)
        {
            if (mgr.HasCache(assetPath))
            {
                Shader cacheAsset = GetInCache<Shader>(assetPath);
                return cacheAsset;
            }

            Shader asset = mgr.SyncLoadShader(assetPath);
            AddLoadedPath(assetPath);
            return asset;
        }

        public Font SyncLoadFont(string assetPath)
        {
            if (mgr.HasCache(assetPath))
            {
                Font cacheAsset = GetInCache<Font>(assetPath);
                return cacheAsset;
            }

            Font asset = mgr.SyncLoadFont(assetPath);
            AddLoadedPath(assetPath);
            return asset;
        }

        public VideoClip SyncLoadVideoClip(string assetPath)
        {
            if (mgr.HasCache(assetPath))
            {
                VideoClip cacheAsset = GetInCache<VideoClip>(assetPath);
                return cacheAsset;
            }

            VideoClip asset = mgr.SyncLoadVideoClip(assetPath);
            AddLoadedPath(assetPath);
            return asset;
        }

        public RuntimeAnimatorController SyncLoadAnimatorController(string assetPath)
        {
            if (mgr.HasCache(assetPath))
            {
                RuntimeAnimatorController cacheAsset = GetInCache<RuntimeAnimatorController>(assetPath);
                return cacheAsset;
            }

            RuntimeAnimatorController asset = mgr.SyncLoadAnimatorController(assetPath);
            AddLoadedPath(assetPath);
            return asset;
        }

        public ScriptableObject SyncLoadScriptableObject(string assetPath)
        {
            if (mgr.HasCache(assetPath))
            {
                ScriptableObject cacheAsset = GetInCache<ScriptableObject>(assetPath);
                return cacheAsset;
            }

            ScriptableObject asset = mgr.SyncLoadScriptableObject(assetPath);
            AddLoadedPath(assetPath);
            return asset;
        }
        #endregion

        #region CacheLoad
        public T GetInCache<T>(string assetPath) where T : UObject
        {
            return mgr.GetInCache<T>(assetPath);
        }

        public T GetCache<T>(string assetPath, UAssetType uAssetType) where T : UObject
        {
            if (!mgr.HasCache(assetPath))
            {
                UObject asset = SyncLoadTypeAsset(assetPath, uAssetType);
                mgr.AddCache(assetPath, asset);
            }
            return mgr.GetInCache<T>(assetPath);
        }

        public GameObject GetCacheGameObject(string assetPath)
        {
            GameObject prefab = GetCache<GameObject>(assetPath, UAssetType.Prefab);
            return EngineUtil.Instantiate(prefab);
        }
        #endregion

        #region Func
        private void AddLoadedPath(string path)
        {
            loadedPaths.Add(path);
        }

        public void ClearCache()
        {
            for (int i = 0; i < loadedPaths.Count; i++)
            {
                mgr.DelCache(loadedPaths[i]);
            }
        }

        public void Release()
        {
            if (!IsResourcesMode)
            {
                for (int i = 0; i < loadedPaths.Count; i++)
                {
                    mgr.Unload(loadedPaths[i]);
                }
            }
            if (loadedPaths.Count > 0)
            {
                loadedPaths.Clear();
            }
        }

        public void Dispose()
        {
            loadedPaths.Clear();
            loadedPaths = null;
        }
        #endregion
    }
}