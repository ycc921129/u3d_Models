/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using UnityEngine;

namespace FutureCore
{
    /// <summary>
    /// iOS
    /// </summary>
    public static class TextureCombineiOSUtil
    {
        public static Texture2D Combine(Texture2D tex, Texture2D tex1)
        {
            int length = 512;
            byte[] data = null;
            var blcokBytes = 0;
            switch (tex.format)
            {
                case TextureFormat.PVRTC_RGB2:
                case TextureFormat.PVRTC_RGBA2:
                    blcokBytes = 4;
                    data = new byte[length / 4 * length];
                    break;

                case TextureFormat.PVRTC_RGB4:
                case TextureFormat.PVRTC_RGBA4:
                    blcokBytes = 8;
                    data = new byte[length / 2 * length];
                    break;

                default:
                    LogUtil.Log("Not supported.");
                    return null;
            }

            //填充左下角 256
            CombineBlocks(tex.GetRawTextureData(), data, tex.width, 4, blcokBytes, 0);
            //填充左上角 256
            CombineBlocks(tex.GetRawTextureData(), data, tex.width, 4, blcokBytes, BlcokOffset(tex.width, blcokBytes));
            //填充右下角 256
            CombineBlocks(tex.GetRawTextureData(), data, tex.width, 4, blcokBytes, 2 * BlcokOffset(tex.width, blcokBytes));

            //填充右上角区域
            //左下角 128
            CombineBlocks(tex1.GetRawTextureData(), data, tex1.width, 4, blcokBytes, 3 * BlcokOffset(tex.width, blcokBytes));
            //左上角 128
            CombineBlocks(tex1.GetRawTextureData(), data, tex1.width, 4, blcokBytes, 3 * BlcokOffset(tex.width, blcokBytes) + BlcokOffset(tex1.width, blcokBytes));
            //右下角 128
            CombineBlocks(tex1.GetRawTextureData(), data, tex1.width, 4, blcokBytes, 3 * BlcokOffset(tex.width, blcokBytes) + 2 * BlcokOffset(tex1.width, blcokBytes));
            //右上角 128
            CombineBlocks(tex1.GetRawTextureData(), data, tex1.width, 4, blcokBytes, 3 * BlcokOffset(tex.width, blcokBytes) + 3 * BlcokOffset(tex1.width, blcokBytes));

            var combinedTex = new Texture2D(length, length, tex.format, false);

            combinedTex.LoadRawTextureData(data);
            // 注意Apply()的第二个参数是true，表示合并贴图后立即删除内存拷贝，也就是Read/Write了。
            combinedTex.Apply(false, true);

            return combinedTex;
        }

        private static int BlcokOffset(int length, int blcokBytes)
        {
            return length / (16 / blcokBytes) * length;
        }

        private static void CombineBlocks(byte[] src, byte[] dst, int length, int block, int bytes, int blockOffest)
        {
            int cell = length / block;
            for (int i = 0; i < cell; i++)
            {
                for (int j = 0; j < cell; j++)
                {
                    int srcindex = ((i * cell) + j) * bytes;
                    int dstindex = srcindex + blockOffest;
                    Buffer.BlockCopy(src, srcindex, dst, dstindex, bytes);
                }
            }
        }
    }
}