/*
 Author:du
 Time:2017.12.11
*/

/*
    非压缩的格式: RGBA32/RGB32/RGBA24/RGB24
    硬件纹理压缩格式: ASTC/ETC/PVRTC/DXT

    高清晰无压缩
      RGBA32
    中清晰中压缩
      RGB24+Dithering
      RGBA16+Dithering
      RGB16+Dithering
      RGB24
      RGBA16
      RGB16
    低清晰高压缩
      ETC1 + Alpha (可压缩)
      ETC2 (可压缩)
      PVRTC4
      ASTC_RGBA

    新方案:
      iOS旧版本纹理格式PVRTC_RGBA4
      iOS新方案是PVRTC+Alpha8，在不考虑5s的情况下，直接ASTC_5x5~ASTC_6x6
      安卓新方案是ASTC
    业界常用压缩方案一：
      安卓：业界一般选择ETC1，因为它是OpenGL ES图形标准的一部分，并且被所有使用opengl2.0的Android设备所支持。但由于ETC1不带a通道，因此，带a通道的图需要将原贴图拆分通道，一分为二分别压缩，最后在渲染时合并计算。
      IOS：业界一般使用pvrtc RGBA 4bits格式
    业界常用压缩方案二：
      安卓：ETC2，尺寸要求（能被4整除）舍弃部分低端设备市场
      IOS：PVRTC、ASTC（舍弃部分老ios设备）
      ASTC4\5\6\7四种压缩格式，目前来看，肉眼可见的差别都不明显，可以ASCT5起步，对于比较常见的UI，采用的是ASTC5的压缩格式，不太重要的UI，是ASTC6
*/

using UnityEngine;
using UnityEditor;
using System.IO;

namespace FutureEditor
{
    public class Texture_AssetImportTool : AssetPostprocessor
    {
        /// <summary>
        /// 是否启用自动导入纹理格式
        /// </summary>
        private const bool IsUse = true;

        /// <summary>
        /// Texture压缩质量
        /// </summary>
        private const int TextureCompressionQuality = (int)UnityEditor.TextureCompressionQuality.Normal;
        /// <summary>
        /// 默认压缩质量
        /// </summary>
        private const TextureImporterCompression DefaultCompression = TextureImporterCompression.Compressed;
        /// <summary>
        /// 是否启动Crunched压缩 (不压缩则为高清模式)
        /// </summary>
        private const bool UseCrunchedCompression = false;
        /// <summary>
        /// Crunched压缩质量
        /// </summary>
        private const int CrunchedCompressionQuality = 50;

        private TextureImporter importer;

        private string jpgExtension = ".jpg";
        private string pngExtension = ".png";

        private bool isUseFramesAtlas = false;
        private string atlasName = "Atlas_";
        private string framesAtlasName = "Frames_";
        private string effectAtlasName = "Effect_";

        // 安卓格式
        private TextureImporterFormat androidFormat = TextureImporterFormat.ETC2_RGBA8;
        // iOS格式
#if UNITY_2019 || UNITY_2020
        private TextureImporterFormat iOSFormat = TextureImporterFormat.ASTC_5x5;
#else
        private TextureImporterFormat iOSFormat = TextureImporterFormat.ASTC_RGBA_5x5;
#endif

        // 默认格式
        private TextureImporterFormat defaultFormat = TextureImporterFormat.RGBA32;
        // PC格式
        private TextureImporterFormat pcFormat = TextureImporterFormat.RGBA32;
        // 单纹理低精度格式
        //private TextureImporterFormat textureLowFormat = TextureImporterFormat.RGB16;
        private TextureImporterFormat textureLowFormat = TextureImporterFormat.RGB24;
        // 单纹理透明低精度格式
        //private TextureImporterFormat textureAlphaLowFormat = TextureImporterFormat.RGBA16;
        private TextureImporterFormat textureAlphaLowFormat = TextureImporterFormat.RGBA32;
        // 单纹理格式
        private TextureImporterFormat textureFormat = TextureImporterFormat.RGB24;
        // 单纹理透明格式
        private TextureImporterFormat textureAlphaFormat = TextureImporterFormat.RGBA32;

        private void OnPreprocessTexture()
        {
            if (!IsUse) return;
            if (IsThisAssetJumpOver(assetPath)) return;

            importer = (TextureImporter)assetImporter;
            if (importer == null) return;

            // 用于存储导入的自定义数据
            //importer.userData

            if (assetPath.IndexOf("_Res/Resources/UI") != -1)
            {
                importer.textureType = TextureImporterType.Default;
                importer.textureShape = TextureImporterShape.Texture2D;

                UniformSet();
                DoSettings(true, false, false);
                SetTextrueETC2Format(assetPath);
                SetAlphaIsTransparency(true);
            }
            else if (assetPath.IndexOf("_Res/Art/Atlas") != -1)
            {
                // 拿到所有的子精灵
                // importer.spritesheet;
                importer.textureType = TextureImporterType.Sprite;
                importer.spriteImportMode = SpriteImportMode.Single;

                UniformSet();
                DoSettings(true, false, false);
                SetSpriteMeshType(true);
                SetAlphaIsTransparency(true);

                string dirName = Path.GetDirectoryName(assetPath);
                string spritePackingTag = Path.GetFileName(dirName);
                importer.spritePackingTag = EditorAppConst.AtlasTag_FullRect + atlasName + spritePackingTag;
            }
            else if (assetPath.IndexOf("_Res/Art/Frames") != -1)
            {
                importer.textureType = TextureImporterType.Sprite;
                importer.spriteImportMode = SpriteImportMode.Single;

                UniformSet();
                DoSettings(isUseFramesAtlas, false, true);
                SetSpriteMeshType(true);
                SetAlphaIsTransparency(true);

                importer.spritePackingTag = null;
                if (isUseFramesAtlas)
                {
                    string folderName = "Frames";
                    string spritePackingTag = assetPath.Substring(assetPath.IndexOf(folderName) + folderName.Length + 1);
                    spritePackingTag = spritePackingTag.Substring(0, spritePackingTag.IndexOf("/"));
                    importer.spritePackingTag = EditorAppConst.AtlasTag_FullRect + framesAtlasName + spritePackingTag;
                }
            }
            else if (assetPath.IndexOf("_Res/Art/Sprite") != -1)
            {
                importer.textureType = TextureImporterType.Sprite;
                importer.spriteImportMode = SpriteImportMode.Single;

                UniformSet();
                DoSettings(false, false, true);
                SetSpriteMeshType(true);

                SetAlphaIsTransparency(true);
                SetAlphaTransparency(assetPath);

                importer.spritePackingTag = null;
            }
            else if (assetPath.IndexOf("_Res/Resources/Texture") != -1)
            {
                importer.textureType = TextureImporterType.Default;
                importer.textureShape = TextureImporterShape.Texture2D;

                UniformSet();
                DoSettings(false, false, true);

                SetAlphaIsTransparency(false);
                SetAlphaTransparency(assetPath);
            }
            else if (assetPath.IndexOf("_Res/Art/TextureLib") != -1)
            {
                return; // 暂不对纹理库做强制处理

                importer.textureType = TextureImporterType.Default;
                importer.textureShape = TextureImporterShape.Texture2D;

                UniformSet();
                DoSettings(false, false, true);

                SetAlphaIsTransparency(true);
                SetAlphaTransparency(assetPath);
            }
            else if (assetPath.IndexOf("_Res/Art/EffectLib") != -1)
            {
                // 序列帧特效
                if (assetPath.IndexOf("FE") != -1)
                {
                    importer.textureType = TextureImporterType.Sprite;
                    importer.spriteImportMode = SpriteImportMode.Single;

                    UniformSet();

                    DirectoryInfo dir = new DirectoryInfo(assetPath);
                    int fileNum = dir.Parent.GetFiles().Length;
                    if (fileNum > 2)
                    {
                        DoSettings(true, false, false);
                        SetSpriteMeshType(true);

                        string folderName = "EffectLib";
                        string spritePackingTag = assetPath.Substring(assetPath.IndexOf(folderName) + folderName.Length + 1);
                        spritePackingTag = spritePackingTag.Substring(0, spritePackingTag.IndexOf("/"));
                        importer.spritePackingTag = EditorAppConst.AtlasTag_FullRect + effectAtlasName + spritePackingTag;
                    }
                    else
                    {
                        DoSettings(false, false, true);
                        SetSpriteMeshType(true);
                        SetAlphaTransparency(assetPath);

                        importer.spritePackingTag = null;
                    }
                    SetAlphaIsTransparency(true);
                }
                // 其他特效
                else
                {
                    importer.textureType = TextureImporterType.Default;
                    importer.textureShape = TextureImporterShape.Texture2D;

                    UniformSet();
                    DoSettings(false, false, true);

                    SetAlphaIsTransparency(true);
                    SetAlphaTransparency(assetPath);
                }
            }
            else if (assetPath.IndexOf("_Res/Art/EffectLib") != -1)
            {
                importer.textureType = TextureImporterType.Default;
                importer.textureShape = TextureImporterShape.Texture2D;

                UniformSet();
                DoSettings(false, false, true);

                SetAlphaIsTransparency(true);
                SetAlphaTransparency(assetPath);
            }
            else if (assetPath.IndexOf("_Res/Art/Skeleton") != -1)
            {
                importer.textureType = TextureImporterType.Default;
                importer.textureShape = TextureImporterShape.Texture2D;

                UniformSet();
                DoSettings(false, false, true);

                SetAlphaIsTransparency(true);
                SetAlphaTransparency(assetPath);
            }
            else if (assetPath.IndexOf("_Res/Resources/StaticFont") != -1)
            {
                importer.textureType = TextureImporterType.Default;
                importer.textureShape = TextureImporterShape.Texture2D;

                UniformSet();
                DoSettings(false, false, true);

                SetAlphaIsTransparency(false);
                SetAlphaTransparency(assetPath);
            }

            //EditorUtility.SetDirty(importer);
        }

        private void OnPostprocessTexture(Texture2D texture)
        {
            if (!IsUse) return;
            if (IsThisAssetJumpOver(assetPath)) return;

            if (assetPath.IndexOf("_Res/Resources/UI") != -1)
            {
                SetMaxTextureSize(GetTextureRawMaxSize());
            }
            else if (assetPath.IndexOf("_Res/Art/Atlas") != -1)
            {
                SetMaxTextureSize(GetTextureRawMaxSize());
            }
            else if (assetPath.IndexOf("_Res/Art/Frames") != -1)
            {
                SetMaxTextureSize(GetTextureRawMaxSize());
            }
            else if (assetPath.IndexOf("_Res/Art/Sprite") != -1)
            {
                SetMaxTextureSize(GetTextureRawMaxSize());
            }
            else if (assetPath.IndexOf("_Res/Resources/Texture") != -1)
            {
                SetMaxTextureSize(GetTextureRawMaxSize());
            }
            else if (assetPath.IndexOf("_Res/Art/TextureLib") != -1)
            {
                SetMaxTextureSize(GetTextureRawMaxSize());
            }
            else if (assetPath.IndexOf("_Res/Art/EffectLib") != -1)
            {
                SetMaxTextureSize(GetTextureRawMaxSize());
            }
            else if (assetPath.IndexOf("_Res/Art/EffectLib") != -1)
            {
                SetMaxTextureSize(GetTextureRawMaxSize());
            }
            else if (assetPath.IndexOf("_Res/Art/Skeleton") != -1)
            {
                SetMaxTextureSize(GetTextureRawMaxSize());
            }
            else if (assetPath.IndexOf("_Res/Resources/StaticFont") != -1)
            {
                SetMaxTextureSize(GetTextureRawMaxSize());
            }
        }

        private bool IsThisAssetJumpOver(string thisAssetPath)
        {
            if (thisAssetPath.Contains("#32#"))
            {
                return true;
            }
            return false;
        }

        private void UniformSet()
        {
            importer.ClearPlatformTextureSettings("Android");
            importer.ClearPlatformTextureSettings("iPhone");
            importer.ClearPlatformTextureSettings("Standalone");

            importer.spritePixelsPerUnit = EP_AppConst.PixelsPerUnit;
            importer.isReadable = false;
            importer.sRGBTexture = true;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.filterMode = FilterMode.Bilinear;
            importer.alphaSource = TextureImporterAlphaSource.FromInput;
            importer.mipmapEnabled = false;
            importer.streamingMipmaps = false;
            importer.borderMipmap = false;

            TextureImporterSettings setting = new TextureImporterSettings();
            importer.ReadTextureSettings(setting);
            setting.mipmapEnabled = false;
            setting.streamingMipmaps = false;
            setting.borderMipmap = false;
            setting.spriteGenerateFallbackPhysicsShape = false;
            importer.SetTextureSettings(setting);
        }

        private void DoSettings(bool isAtlas, bool isSingleSprite, bool isAlphaTexture)
        {
            int maxSize = GetTextureRawMaxSize();

            TextureImporterPlatformSettings defaultSetting = importer.GetDefaultPlatformTextureSettings();
            defaultSetting.maxTextureSize = maxSize;
            defaultSetting.resizeAlgorithm = TextureResizeAlgorithm.Bilinear;
            defaultSetting.textureCompression = DefaultCompression;
            defaultSetting.crunchedCompression = UseCrunchedCompression;
            defaultSetting.compressionQuality = CrunchedCompressionQuality;
            SetTextureFormat(defaultSetting, isAtlas, isSingleSprite, isAlphaTexture);
            defaultSetting.format = defaultFormat;
            importer.SetPlatformTextureSettings(defaultSetting);

            TextureImporterPlatformSettings standaloneSetting = importer.GetPlatformTextureSettings("Standalone");
            standaloneSetting.overridden = true;
            standaloneSetting.maxTextureSize = maxSize;
            standaloneSetting.resizeAlgorithm = TextureResizeAlgorithm.Bilinear;
            standaloneSetting.textureCompression = DefaultCompression;
            standaloneSetting.compressionQuality = CrunchedCompressionQuality;
            SetTextureFormat(standaloneSetting, isAtlas, isSingleSprite, isAlphaTexture);
            standaloneSetting.format = pcFormat;
            importer.SetPlatformTextureSettings(standaloneSetting);

            TextureImporterPlatformSettings androidSetting = importer.GetPlatformTextureSettings("Android");
            androidSetting.overridden = true;
            androidSetting.maxTextureSize = maxSize;
            androidSetting.resizeAlgorithm = TextureResizeAlgorithm.Bilinear;
            androidSetting.textureCompression = DefaultCompression;
            SetTextureFormat(androidSetting, isAtlas, isSingleSprite, isAlphaTexture);
            importer.SetPlatformTextureSettings(androidSetting);

            TextureImporterPlatformSettings iPhoneSetting = importer.GetPlatformTextureSettings("iPhone");
            iPhoneSetting.format = iOSFormat;
            iPhoneSetting.overridden = true;
            iPhoneSetting.maxTextureSize = maxSize;
            iPhoneSetting.textureCompression = DefaultCompression;
            iPhoneSetting.resizeAlgorithm = TextureResizeAlgorithm.Bilinear;
            iPhoneSetting.compressionQuality = TextureCompressionQuality;
            SetTextureFormat(iPhoneSetting, isAtlas, isSingleSprite, isAlphaTexture);
            importer.SetPlatformTextureSettings(iPhoneSetting);
        }

        private void SetSpriteMeshType(bool isFullRect = true)
        {
            TextureImporterSettings setting = new TextureImporterSettings();
            importer.ReadTextureSettings(setting);
            if (isFullRect)
            {
                setting.spriteMeshType = SpriteMeshType.FullRect;
            }
            else
            {
                setting.spriteMeshType = SpriteMeshType.Tight;
            }
            importer.SetTextureSettings(setting);
        }

        private void SetTextureFormat(TextureImporterPlatformSettings setting, bool isAtlas, bool isSingleSprite, bool isAlphaTexture)
        {
            if (isAtlas)
            {
                // 图集升级为ETC2 不再拆分透明通道
                setting.allowsAlphaSplitting = false;
                setting.format = androidFormat;
            }
            else if (isSingleSprite)
            {
                setting.allowsAlphaSplitting = false;
                setting.format = textureLowFormat;
            }
            else if (isAlphaTexture)
            {
                setting.allowsAlphaSplitting = false;
                setting.format = textureAlphaLowFormat;
            }
            else
            {
                setting.allowsAlphaSplitting = false;
                setting.format = textureFormat;
            }
        }

        private void SetTextrueETC2Format(string assetPath)
        {
            TextureImporterPlatformSettings androidSetting = importer.GetPlatformTextureSettings("Android");

            importer.alphaSource = TextureImporterAlphaSource.FromInput;
            importer.alphaIsTransparency = true;

            androidSetting.androidETC2FallbackOverride = AndroidETC2FallbackOverride.Quality32Bit;
            androidSetting.crunchedCompression = UseCrunchedCompression;
            androidSetting.resizeAlgorithm = TextureResizeAlgorithm.Bilinear;

            if (!UseCrunchedCompression)
            {
                androidSetting.format = TextureImporterFormat.ETC2_RGBA8;
                androidSetting.textureCompression = DefaultCompression;
                androidSetting.compressionQuality = TextureCompressionQuality;
            }
            else
            {
                androidSetting.format = TextureImporterFormat.ETC2_RGBA8Crunched;
                androidSetting.compressionQuality = TextureCompressionQuality;
            }

            importer.crunchedCompression = UseCrunchedCompression;
            if (!UseCrunchedCompression)
            {
                importer.textureCompression = DefaultCompression;
            }
            else
            {
                importer.crunchedCompression = UseCrunchedCompression;
                importer.compressionQuality = TextureCompressionQuality;
            }
            importer.SetPlatformTextureSettings(androidSetting);
        }

        private void SetTextureHardwareFormat(string assetPath)
        {
            TextureImporterPlatformSettings androidSetting = importer.GetPlatformTextureSettings("Android");
            androidSetting.format = androidFormat;
            importer.SetPlatformTextureSettings(androidSetting);
        }

        private void SetAlphaTransparency(string assetPath)
        {
            string fileExtension = Path.GetExtension(assetPath);
            TextureImporterPlatformSettings androidSetting = importer.GetPlatformTextureSettings("Android");
            if (!importer.DoesSourceTextureHaveAlpha() || fileExtension.Equals(jpgExtension))
            {
                androidSetting.format = textureLowFormat;
                SetAlphaIsTransparency(false);
            }
            else if (importer.DoesSourceTextureHaveAlpha() || fileExtension.Equals(pngExtension))
            {
                androidSetting.format = textureAlphaLowFormat;
                SetAlphaIsTransparency(true);
            }
            importer.SetPlatformTextureSettings(androidSetting);
        }

        private void SetMaxTextureSize(int maxSize)
        {
            TextureImporterPlatformSettings defaultSetting = importer.GetDefaultPlatformTextureSettings();
            defaultSetting.maxTextureSize = maxSize;
            importer.SetPlatformTextureSettings(defaultSetting);

            TextureImporterPlatformSettings standaloneSetting = importer.GetPlatformTextureSettings("Standalone");
            standaloneSetting.overridden = true;
            standaloneSetting.maxTextureSize = maxSize;
            importer.SetPlatformTextureSettings(standaloneSetting);

            TextureImporterPlatformSettings androidSetting = importer.GetPlatformTextureSettings("Android");
            androidSetting.overridden = true;
            androidSetting.maxTextureSize = maxSize;
            importer.SetPlatformTextureSettings(androidSetting);

            TextureImporterPlatformSettings iPhoneSetting = importer.GetPlatformTextureSettings("iPhone");
            iPhoneSetting.overridden = true;
            iPhoneSetting.maxTextureSize = maxSize;
            importer.SetPlatformTextureSettings(iPhoneSetting);
        }

        /// <summary>
        /// 获取纹理原最大尺寸
        /// </summary>
        private int GetTextureRawMaxSize()
        {
            Texture2D tmpTexture = new Texture2D(1, 1);
            byte[] tmpBytes = File.ReadAllBytes(assetPath);
            tmpTexture.LoadImage(tmpBytes);

            int maxSize = 2048;
            maxSize = Mathf.Max(tmpTexture.width, tmpTexture.height);
            maxSize = GetMaxSize(maxSize);
            return maxSize;
        }

        private int GetMaxSize(int size)
        {
            int maxSize = 8192;
            while (true)
            {
                if (maxSize < 32)
                {
                    return 32;
                }
                if (size > maxSize)
                {
                    if (maxSize * 2 > 8192)
                    {
                        return 8192;
                    }
                    return maxSize * 2;
                }
                maxSize = maxSize / 2;
            }
        }

        private void SetAlphaIsTransparency(bool isTransparency)
        {
            importer.alphaIsTransparency = isTransparency;
        }

        private bool IsTextureHaveAlpha()
        {
            return importer.DoesSourceTextureHaveAlpha();
        }
    }
}