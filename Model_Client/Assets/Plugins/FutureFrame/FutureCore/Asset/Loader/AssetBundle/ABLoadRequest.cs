/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.9
*/

using System;
using UnityEngine;
using UnityEngine.Video;
using UObject = UnityEngine.Object;

namespace FutureCore
{
    public class ABLoadRequest
    {
        public string assetPath;
        public string abName;
        public string assetName;

        public bool isSyncMode;
        public UAssetType assetType;
        public Type inLoadType;

        public LoadedObjectFunc loadedObjectFunc;
        public LoadedGameObjectFunc loadedGameObjectFunc;
        public LoadedSpriteFunc loadedSpriteFunc;
        public LoadedTextureFunc loadedTextureFunc;
        public LoadedAudioClipFunc loadedAudioClipFunc;
        public LoadedTextAssetFunc loadedTextAssetFunc;
        public LoadedMaterialFunc loadedMaterialFunc;
        public LoadedShaderFunc loadedShaderFunc;
        public LoadedFontFunc loadedFontFunc;
        public LoadedVideoClipFunc LoadedVideoClipFunc;
        public LoadedRuntimeAnimatorControllerFunc loadedRuntimeAnimatorControllerFunc;
        public LoadedScriptableObjectFunc loadedScriptableObjectFunc;

        public void OnLoaded(UObject asset)
        {
            switch (assetType)
            {
                case UAssetType.Generic:
                    loadedObjectFunc(asset);
                    return;
                case UAssetType.Object:
                    loadedObjectFunc(asset);
                    return;
                case UAssetType.Prefab:
                    loadedGameObjectFunc(asset as GameObject);
                    return;
                case UAssetType.GameObject:
                    GameObject prefab = asset as GameObject;
                    GameObject gameObject = EngineUtil.Instantiate(prefab);
                    loadedGameObjectFunc(gameObject);
                    return;
                case UAssetType.Sprite:
                    if (AppConst.IsResourcesMode_Default)
                    {
                        GameObject spritePrefab = asset as GameObject;
                        Sprite sprite = spritePrefab.GetComponent<SpriteRenderer>().sprite;
                        loadedSpriteFunc(sprite);
                    }
                    else
                    {
                        loadedSpriteFunc(asset as Sprite);
                    }
                    return;
                case UAssetType.Texture:
                    loadedTextureFunc(asset as Texture);
                    return;
                case UAssetType.AudioClip:
                    loadedAudioClipFunc(asset as AudioClip);
                    return;
                case UAssetType.TextAsset:
                    loadedTextAssetFunc(asset as TextAsset);
                    return;
                case UAssetType.Material:
                    loadedMaterialFunc(asset as Material);
                    return;
                case UAssetType.Shader:
                    loadedShaderFunc(asset as Shader);
                    return;
                case UAssetType.Font:
                    loadedFontFunc(asset as Font);
                    return;
                case UAssetType.VideoClip:
                    LoadedVideoClipFunc(asset as VideoClip);
                    return;
                case UAssetType.RuntimeAnimatorController:
                    loadedRuntimeAnimatorControllerFunc(asset as RuntimeAnimatorController);
                    return;
                case UAssetType.ScriptableObject:
                    loadedScriptableObjectFunc(asset as ScriptableObject);
                    return;
            }
        }
    }
}