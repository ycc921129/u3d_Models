/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using UnityEngine;

namespace FutureCore
{
    /// <summary>
    /// PC与安卓
    /// </summary>
    public static class TextureCombineUtil
    {
        public static Texture2D Combine(Texture2D tex1, Texture2D tex2)
        {
            int length = 512;
            var blcokBytes = 0;
            byte[] data = null;
            switch (tex1.format)
            {
                case TextureFormat.DXT1:
                case TextureFormat.ETC_RGB4:
                case TextureFormat.ETC2_RGB:
                    blcokBytes = 8;
                    data = new byte[length / 2 * length];
                    break;
                case TextureFormat.DXT5:
                case TextureFormat.ETC2_RGBA8:
                case TextureFormat.ASTC_RGB_4x4:
                case TextureFormat.ASTC_RGBA_4x4:
                    blcokBytes = 16;
                    data = new byte[length * length];
                    break;
                default:
                    LogUtil.Log("Not supported.");
                    return null;
            }

            //填充左下角 256
            CombineBlocks(tex1.GetRawTextureData(), data, 0, 0, tex1.width, 4, blcokBytes, length);
            //填充左上角 256 
            CombineBlocks(tex1.GetRawTextureData(), data, 0, tex1.width, tex1.width, 4, blcokBytes, length);
            //填充右下角 256 
            CombineBlocks(tex1.GetRawTextureData(), data, tex1.width, 0, tex1.width, 4, blcokBytes, length);

            //填充右上角区域
            //左下角 128
            CombineBlocks(tex2.GetRawTextureData(), data, tex1.width, tex1.width, tex2.width, 4, blcokBytes, length);
            //左上角 128
            CombineBlocks(tex2.GetRawTextureData(), data, tex1.width, tex1.width + tex2.width, tex2.width, 4, blcokBytes, length);
            //右下角 128
            CombineBlocks(tex2.GetRawTextureData(), data, tex1.width + tex2.width, tex1.width, tex2.width, 4, blcokBytes, length);
            //右上角 128
            CombineBlocks(tex2.GetRawTextureData(), data, tex1.width + tex2.width, tex1.width + tex2.width, tex2.width, 4, blcokBytes, length);

            var combinedTex = new Texture2D(length, length, tex1.format, false);
            combinedTex.LoadRawTextureData(data);
            // 注意Apply()的第二个参数是true，表示合并贴图后立即删除内存拷贝，也就是Read/Write了
            combinedTex.Apply(false, true);

            return combinedTex;
        }

        private static void CombineBlocks(byte[] src, byte[] dst, int dstx, int dsty, int width, int block, int bytes, int length)
        {
            var dstbx = dstx / block;
            var dstby = dsty / block;

            for (int i = 0; i < width / block; i++)
            {
                int dstindex = (dstbx + (dstby + i) * (length / block)) * bytes;
                int srcindex = i * (width / block) * bytes;
                Buffer.BlockCopy(src, srcindex, dst, dstindex, width / block * bytes);
            }
        }
    }
}