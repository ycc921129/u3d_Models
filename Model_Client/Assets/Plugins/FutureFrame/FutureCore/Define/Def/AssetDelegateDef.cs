/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.6
*/

using UnityEngine;
using UnityEngine.Video;
using UObject = UnityEngine.Object;

namespace FutureCore
{
    public delegate void LoadedObjectFunc(UObject uobject);
    public delegate void LoadedGameObjectFunc(GameObject gameObject);
    public delegate void LoadedSpriteFunc(Sprite sprite);
    public delegate void LoadedTextureFunc(Texture texture);
    public delegate void LoadedAudioClipFunc(AudioClip audioClip);
    public delegate void LoadedTextAssetFunc(TextAsset textAsset);
    public delegate void LoadedMaterialFunc(Material material);
    public delegate void LoadedShaderFunc(Shader shader);
    public delegate void LoadedFontFunc(Font font);
    public delegate void LoadedVideoClipFunc(VideoClip videoClip);
    public delegate void LoadedRuntimeAnimatorControllerFunc(RuntimeAnimatorController runtimeAnimatorController);
    public delegate void LoadedScriptableObjectFunc(ScriptableObject scriptableObject);
}