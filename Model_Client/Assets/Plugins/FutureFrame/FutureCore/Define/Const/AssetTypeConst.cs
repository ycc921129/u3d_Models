/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using UnityEngine;
using UnityEngine.Video;
using UObject = UnityEngine.Object;

namespace FutureCore
{
    public static class AssetTypeConst
    {
        public readonly static Type GenericType = typeof(UObject);
        public readonly static Type UObjectType = typeof(UObject);
        public readonly static Type PrefabType = typeof(GameObject);
        public readonly static Type GameObjectType = typeof(GameObject);
        public readonly static Type SpriteType = typeof(Sprite);
        public readonly static Type TextureType = typeof(Texture);
        public readonly static Type AudioClipType = typeof(AudioClip);
        public readonly static Type TextAssetType = typeof(TextAsset);
        public readonly static Type MaterialType = typeof(Material);
        public readonly static Type ShaderType = typeof(Shader);
        public readonly static Type FontType = typeof(Font);
        public readonly static Type VideoClipType = typeof(VideoClip);
        public readonly static Type RuntimeAnimatorControllerType = typeof(RuntimeAnimatorController);
        public readonly static Type ScriptableObjectType = typeof(ScriptableObject);
    }
}