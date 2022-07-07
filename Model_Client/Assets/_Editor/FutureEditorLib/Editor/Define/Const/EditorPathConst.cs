/*
 Author:du
 Time:2017.11.8
*/

using System;
using UnityEngine;

namespace FutureEditor
{
    public static class EditorPathConst
    {
        #region Project
        public static string ResResourcesTempName = "Resources_TEMP";

        public static string ShortPresetResourcesPath = "/_AppCore/PresetRes/Resources/Preset/";
        public static string ShortResArtPath = "/_Res/Art/";
        public static string ShortResResourcesPath = "/_Res/Resources/";

        public static string ProjectPath = Application.dataPath.Split(new string[] { "Assets" }, StringSplitOptions.None)[0];
        public static string PluginsPath = Application.dataPath + "/Plugins/";
        public static string PresetResourcesPath = Application.dataPath + ShortPresetResourcesPath;
        public static string ResArtPath = Application.dataPath + ShortResArtPath;
        public static string ResResourcesPath = Application.dataPath + ShortResResourcesPath;
        public static string ResResourcesTempPath = Application.dataPath + "/_Res/" + ResResourcesTempName + "/";

        public static string ABOutputPath = Application.streamingAssetsPath + "/www";
        public static string ABZipOutputFilePath = Application.streamingAssetsPath + "/www.zip";
        public static string ABVersionFilePath = Application.streamingAssetsPath + "/version.txt";

        public const string ABExtension = EP_AppConst.ABExtName;
        public const string ABManifestExtension = ".manifest";
        #endregion

        #region AssetPath
        /// 目录
        // 加载资源
        public static string AnimPath = ResResourcesPath + EP_ABPakConst.Anim;
        public static string AtlasPath = ResArtPath + EP_ABPakConst.Atlas;
        public static string AudioPath = ResResourcesPath + EP_ABPakConst.Audio;
        public static string DataPath = ResResourcesPath + EP_ABPakConst.Data;
        public static string DynamicFontPath = ResResourcesPath + EP_ABPakConst.DynamicFont;
        public static string EffectPath = ResArtPath + EP_ABPakConst.Effect;
        public static string FramesPath = ResArtPath + EP_ABPakConst.Frames;
        public static string ModulePath = ResResourcesPath + EP_ABPakConst.Module;
        public static string ScenePath = ResResourcesPath + EP_ABPakConst.Prefab;
        public static string SkeletonPath = ResArtPath + EP_ABPakConst.Skeleton;
        public static string SpritePath = ResArtPath + EP_ABPakConst.Sprite;
        public static string TexturePath = ResResourcesPath + EP_ABPakConst.Texture;
        public static string VideoPath = ResResourcesPath + EP_ABPakConst.Video;
        // 公共加载资源
        public static string ShaderPath = ResResourcesPath + EP_ABPakConst.Shader;
        public static string MaterialPath = ResResourcesPath + EP_ABPakConst.Material;
        public static string StaticFontPath = ResResourcesPath + EP_ABPakConst.StaticFont;
        public static string EffectLibPath = ResArtPath + EP_ABPakConst.EffectLib;
        // 公共基础资源
        public static string SkeletonShadersPath = PluginsPath + EP_ABPakConst.SkeletonShaders;
        public static string SkeletonGraphicShadersPath = PluginsPath + EP_ABPakConst.SkeletonGraphicShaders;

        /// 指定文件
        // 预置静态基础字体文件: PreMainFont
        public static string PreStaticFontFile = PresetResourcesPath + EP_ABPakConst.PreDynamicFontFile;
        // 公共基础资源文件: SkeletonShader-Fill
        public static string SkeletonSkeletonShaderFillFile = PluginsPath + EP_ABPakConst.SkeletonSkeletonShaderFillFile;
        // 公共基础资源文件: SkeletonEditorIcon
        public static string SkeletonEditorGUIAtlasAssetIconFile = PluginsPath + EP_ABPakConst.SkeletonEditorGUIAtlasAssetIconFile;
        public static string SkeletonEditorGUISkeletonDataAssetIconFile = PluginsPath + EP_ABPakConst.SkeletonEditorGUISkeletonDataAssetIconFile;
        #endregion
    }
}