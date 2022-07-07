/*
贴图导入unity后会自动设置成压缩格式，它会先判断贴图是否有透明通道。

Android：不带透明通道压缩成ETC1，带透明通道压缩成ETC2，不被4整除fall back到RGBA32

IOS: 不带透明通道压缩成RGB PVRTC，带透明通道压缩成RGBA PVRTC ，不是2的整数次幂fall back到RGBA32

Texture图片拖入的时候，如下图所示，unity会默认设置ToNerest，这样会自动保证Android平台下图片被4整除，IOS平台下图片是2的整除次幂，所以默认情况下图片都可以得到最好的压缩。

但是如果是在做UGUI的话，由于需要将Texture设置成Sprite那么它就不会ToNearest了，这样就会导致一个问题。美术同学很难保证图片的尺寸是2的整数次幂，这样IOS下就都无法进行压缩了。

通过上面的问题我想做一件事就是当UI图片拖入unity的时候，自动设置图片的最优格式。（最优解只用于大图模式，也就是长宽一遍超过128另一边超过64这样的图，这样的图不适合放在图集中）

1.美术同学虽然很难保证图片是2的整数次幂，但是保证图片能被4整除这一点很容易，所以我会要求她们提供的图片必须被4整除。

2.Android下优先设置图片规则才用ETC+Crunched的格式，如果图片无法被4整除在设置ASTC格式，如果安卓手机不支持ASTC则会fall back到RGBA32

3.IOS下优先设置图片规则采用PVRTC4，如果图片不是2的整数次幂在设置ASTC格式，如果不支持fall back到RGBA32

4.我们只提供图片首次导入最优方案，后面是可以在修改的。比如一张被4整除并且带ALPHA的图片，根据这个规则导入后会自动设置ETC2+Crunched格式，但是如果美术同学觉得效果不好，后面可以在修改成ASTC RGBA 4X4 或者RGBA32.
 */

using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public class UGUITexture_AssetImportTool : AssetPostprocessor
    {
        private void OnPreprocessTexture()
        {
            TextureImporter importer = assetImporter as TextureImporter;
            if (importer == null) return;

            if (!IsFirstImport(importer)) return;

            if (assetPath.IndexOf("_Res/Resources/UGUI") != -1)
            {
                importer.textureType = TextureImporterType.Sprite;
                TextureImporterPlatformSettings settings = importer.GetPlatformTextureSettings("iPhone");
                bool isPowerOfTwo = IsPowerOfTwo(importer);
                TextureImporterFormat defaultAlpha = isPowerOfTwo ? TextureImporterFormat.PVRTC_RGBA4 : TextureImporterFormat.ASTC_RGBA_4x4;
                TextureImporterFormat defaultNotAlpha = isPowerOfTwo ? TextureImporterFormat.PVRTC_RGB4 : TextureImporterFormat.ASTC_RGB_6x6;
                settings.overridden = true;
                settings.format = importer.DoesSourceTextureHaveAlpha() ? defaultAlpha : defaultNotAlpha;
                importer.SetPlatformTextureSettings(settings);

                settings = importer.GetPlatformTextureSettings("Android");
                settings.overridden = true;
                settings.allowsAlphaSplitting = false;
                bool divisible4 = IsDivisibleOf4(importer);
                defaultAlpha = divisible4 ? TextureImporterFormat.ETC2_RGBA8Crunched : TextureImporterFormat.ASTC_RGBA_4x4;
                defaultNotAlpha = divisible4 ? TextureImporterFormat.ETC_RGB4Crunched : TextureImporterFormat.ASTC_RGB_6x6;
                settings.format = importer.DoesSourceTextureHaveAlpha() ? defaultAlpha : defaultNotAlpha;
                importer.SetPlatformTextureSettings(settings);
            }
        }

        // 被4整除
        private bool IsDivisibleOf4(TextureImporter importer)
        {
            (int width, int height) = GetTextureImporterSize(importer);
            return (width % 4 == 0 && height % 4 == 0);
        }

        // 2的整数次幂
        private bool IsPowerOfTwo(TextureImporter importer)
        {
            (int width, int height) = GetTextureImporterSize(importer);
            return (width == height) && (width > 0) && ((width & (width - 1)) == 0);
        }

        // 贴图不存在、meta文件不存在、图片尺寸发生修改需要重新导入
        private bool IsFirstImport(TextureImporter importer)
        {
            (int width, int height) = GetTextureImporterSize(importer);
            Texture tex = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            bool hasMeta = File.Exists(AssetDatabase.GetAssetPathFromTextMetaFilePath(assetPath));
            return tex == null || !hasMeta || (tex.width != width && tex.height != height);
        }

        // 获取导入图片的宽高
        private (int, int) GetTextureImporterSize(TextureImporter importer)
        {
            if (importer != null)
            {
                object[] args = new object[2];
                MethodInfo mi = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
                mi.Invoke(importer, args);
                return ((int)args[0], (int)args[1]);
            }
            return (0, 0);
        }
    }
}