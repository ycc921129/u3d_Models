/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.6
*/

using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UObject = UnityEngine.Object;

namespace FutureCore
{
    public class ResourcesLoader : SingletonMono<ResourcesLoader>
    {
        #region Load
        public void Load<T>(string assetPath, LoadedObjectFunc func, bool isSyncMode) where T : UObject
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Generic;
            req.loadedObjectFunc = func;
            req.assetPath = assetPath;
            LoadAsset<T>(req);
        }

        public void LoadObject(string assetPath, LoadedObjectFunc func, bool isSyncMode)
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Object;
            req.loadedObjectFunc = func;
            req.assetPath = assetPath;
            LoadAsset<UObject>(req);
        }

        public void LoadPrefab(string assetPath, LoadedGameObjectFunc func, bool isSyncMode)
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Prefab;
            req.loadedGameObjectFunc = func;
            req.assetPath = assetPath;
            LoadAsset<GameObject>(req);
        }

        public void LoadGameObject(string assetPath, LoadedGameObjectFunc func, bool isSyncMode)
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.GameObject;
            req.loadedGameObjectFunc = func;
            req.assetPath = assetPath;
            LoadAsset<GameObject>(req);
        }

        public void LoadSprite(string assetPath, LoadedSpriteFunc func, bool isSyncMode)
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Sprite;
            req.loadedSpriteFunc = func;
            req.assetPath = assetPath;
            LoadAsset<Sprite>(req);
        }

        public void LoadTexture(string assetPath, LoadedTextureFunc func, bool isSyncMode)
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Texture;
            req.loadedTextureFunc = func;
            req.assetPath = assetPath;
            LoadAsset<Texture>(req);
        }

        public void LoadAudioClip(string assetPath, LoadedAudioClipFunc func, bool isSyncMode)
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.AudioClip;
            req.loadedAudioClipFunc = func;
            req.assetPath = assetPath;
            LoadAsset<AudioClip>(req);
        }

        public void LoadTextAsset(string assetPath, LoadedTextAssetFunc func, bool isSyncMode)
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.TextAsset;
            req.loadedTextAssetFunc = func;
            req.assetPath = assetPath;
            LoadAsset<TextAsset>(req);
        }

        public void LoadMaterial(string assetPath, LoadedMaterialFunc func, bool isSyncMode)
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Material;
            req.loadedMaterialFunc = func;
            req.assetPath = assetPath;
            LoadAsset<Material>(req);
        }

        public void LoadShader(string assetPath, LoadedShaderFunc func, bool isSyncMode)
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Shader;
            req.loadedShaderFunc = func;
            req.assetPath = assetPath;
            LoadAsset<Shader>(req);
        }

        public void LoadFont(string assetPath, LoadedFontFunc func, bool isSyncMode)
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.Font;
            req.loadedFontFunc = func;
            req.assetPath = assetPath;
            LoadAsset<Font>(req);
        }

        public void LoadVideoClip(string assetPath, LoadedVideoClipFunc func, bool isSyncMode)
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.VideoClip;
            req.LoadedVideoClipFunc = func;
            req.assetPath = assetPath;
            LoadAsset<VideoClip>(req);
        }

        public void LoadAnimatorController(string assetPath, LoadedRuntimeAnimatorControllerFunc func, bool isSyncMode)
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.RuntimeAnimatorController;
            req.loadedRuntimeAnimatorControllerFunc = func;
            req.assetPath = assetPath;
            LoadAsset<RuntimeAnimatorController>(req);
        }

        public void LoadScriptableObject(string assetPath, LoadedScriptableObjectFunc func, bool isSyncMode)
        {
            ResLoadRequest req = new ResLoadRequest();
            req.isSyncMode = isSyncMode;
            req.assetType = UAssetType.ScriptableObject;
            req.loadedScriptableObjectFunc = func;
            req.assetPath = assetPath;
            LoadAsset<ScriptableObject>(req);
        }

        private void LoadAsset<T>(ResLoadRequest loadReq) where T : UObject
        {
            if (loadReq.isSyncMode)
            {
                T asset = SyncLoad<T>(loadReq.assetPath);
                loadReq.OnLoaded(asset);
            }
            else
            {
                StartCoroutine(AsyncLoad<T>(loadReq));
            }
        }

        private IEnumerator AsyncLoad<T>(ResLoadRequest loadReq) where T : UObject
        {
            ResourceRequest resReq = Resources.LoadAsync<T>(loadReq.assetPath);
            yield return resReq;
            T asset = resReq.asset as T;
            CheckAsset(asset, loadReq.assetPath);
            loadReq.OnLoaded(asset);
        }
        #endregion

        #region SyncLoad
        public T SyncLoad<T>(string assetPath) where T : UObject
        {
            T asset = Resources.Load<T>(assetPath);
            CheckAsset(asset, assetPath);
            return asset;
        }

        public UObject SyncLoadObject(string assetPath)
        {
            return SyncLoad<UObject>(assetPath);
        }

        public GameObject SyncLoadPrefab(string assetPath)
        {
            return SyncLoad<GameObject>(assetPath);
        }

        public GameObject SyncLoadGameObject(string assetPath)
        {
            GameObject prefab = SyncLoadPrefab(assetPath);
            return EngineUtil.Instantiate(prefab);
        }

        public Sprite SyncLoadSprite(string assetPath)
        {
            if (!ResMgr.IsUseEncryptTexture)
            {
                return SyncLoadPrefab(assetPath).GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                return DecryptSyncLoad<Sprite>(assetPath);
            }
        }

        public Texture SyncLoadTexture(string assetPath)
        {
            if (!ResMgr.IsUseEncryptTexture)
            {
                return SyncLoad<Texture>(assetPath);
            }
            else
            {
                return DecryptSyncLoad<Texture>(assetPath);
            }
        }

        public AudioClip SyncLoadAudioClip(string assetPath)
        {
            return SyncLoad<AudioClip>(assetPath);
        }

        public TextAsset SyncLoadTextAsset(string assetPath)
        {
            return SyncLoad<TextAsset>(assetPath);
        }

        public Material SyncLoadMaterial(string assetPath)
        {
            return SyncLoad<Material>(assetPath);
        }

        public Shader SyncLoadShader(string assetPath)
        {
            return SyncLoad<Shader>(assetPath);
        }

        public Font SyncLoadFont(string assetPath)
        {
            return SyncLoad<Font>(assetPath);
        }

        public VideoClip SyncLoadVideoClip(string assetPath)
        {
            return SyncLoad<VideoClip>(assetPath);
        }

        public RuntimeAnimatorController SyncLoadAnimatorController(string assetPath)
        {
            return SyncLoad<RuntimeAnimatorController>(assetPath);
        }

        public ScriptableObject SyncLoadScriptableObject(string assetPath)
        {
            return SyncLoad<ScriptableObject>(assetPath);
        }
        #endregion

        #region DecryptSyncLoad
        public T DecryptSyncLoad<T>(string assetPath) where T : UObject
        {
            ResMgr mgr = ResMgr.Instance;
            T asset = mgr.textureDecryptAB.LoadAsset<T>(mgr.textureDecryptABMap.pathMapDict[assetPath]);
            CheckAsset(asset, assetPath);
            return asset;
        }
        #endregion

        #region Func
        private void CheckAsset(UObject asset, string path)
        {
            if (asset == null)
            {
                LogUtil.LogErrorFormat("[ResourcesLoader]Load Asset Is Null:{0}", path);
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