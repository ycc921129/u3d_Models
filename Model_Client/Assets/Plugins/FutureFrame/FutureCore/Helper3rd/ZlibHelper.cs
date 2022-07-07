/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.8
*/

using ComponentAce.Compression.Libs.zlib;
using System.IO;
using System;
using System.Text;

namespace FutureCore
{
    /// <summary>
    /// Zlib 压缩帮助类
    /// </summary>
    public static class ZlibHelper
    {
        public const int Zlib_Buffer_Size = 10000;

        #region CompressBytes
        /// <summary>
        /// 对原始字节数组进行zlib压缩，得到处理结果字节数组
        /// </summary>
        /// <param name="orgBytes">需要被压缩的原始Byte数组数据</param>
        /// <param name="compressRate">压缩率：默认为zlibConst.Z_DEFAULT_COMPRESSION</param>
        /// <returns>压缩后的字节数组，如果出错则返回null</returns>
        public static byte[] CompressBytes(byte[] orgBytes, int compressRate = zlibConst.Z_DEFAULT_COMPRESSION)
        {
            if (orgBytes == null) return null;

            using (System.IO.MemoryStream orgStream = new System.IO.MemoryStream(orgBytes))
            {
                using (System.IO.MemoryStream compressedStream = new System.IO.MemoryStream())
                {
                    using (ZOutputStream outZStream = new ZOutputStream(compressedStream, compressRate))
                    {
                        try
                        {
                            CopyStream(orgStream, outZStream);
                            outZStream.finish();//重要！否则结果数据不完整！
                            //程序执行到这里，CompressedStream就是压缩后的数据
                            if (compressedStream == null) return null;

                            byte[] compressBytes = compressedStream.ToArray();
                            return compressBytes;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }
        #endregion

        #region DeCompressBytes
        /// <summary>
        /// 对经过zlib压缩的数据，进行解密和zlib解压缩，得到原始字节数组
        /// </summary>
        /// <param name="compressedBytes">被压缩的Byte数组数据</param>
        /// <returns>解压缩后的字节数组，如果出错则返回null</returns>
        public static byte[] DeCompressBytes(byte[] compressedBytes)
        {
            if (compressedBytes == null) return null;

            using (System.IO.MemoryStream compressedStream = new System.IO.MemoryStream(compressedBytes))
            {
                using (System.IO.MemoryStream orgStream = new System.IO.MemoryStream())
                {
                    using (ZOutputStream outZStream = new ZOutputStream(orgStream))
                    {
                        try
                        {
                            CopyStream(compressedStream, outZStream);
                            outZStream.finish();//重要！
                            //程序执行到这里，OrgStream就是解压缩后的数据

                            if (orgStream == null)
                            {
                                return null;
                            }
                            byte[] deCompressBytes = orgStream.ToArray();
                            return deCompressBytes;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }
        #endregion

        #region CopyStream
        /// <summary>
        /// 拷贝流
        /// </summary>
        private static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[Zlib_Buffer_Size];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }
        #endregion

        #region GetStringByGZIPData
        /// <summary>
        /// 将解压缩过的二进制数据转换回字符串
        /// </summary>
        public static string GetStringByGZIPData(byte[] zipData)
        {
            return Encoding.UTF8.GetString(zipData);
        }
        #endregion

        #region CompressString
        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="sourceString">需要被压缩的字符串</param>
        /// <returns>压缩后的字符串，如果失败则返回null</returns>
        public static string CompressString(string sourceString, int compressRate = zlibConst.Z_DEFAULT_COMPRESSION)
        {
            byte[] byteSource = Encoding.UTF8.GetBytes(sourceString);
            byte[] byteCompress = CompressBytes(byteSource, compressRate);
            if (byteCompress != null)
            {
                return Convert.ToBase64String(byteCompress);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region DecompressString
        /// <summary>
        /// 解压字符串
        /// </summary>
        /// <param name="sourceString">需要被解压的字符串</param>
        /// <returns>解压后的字符串，如果处所则返回null</returns>
        public static string DecompressString(string sourceString)
        {
            byte[] byteSource = Convert.FromBase64String(sourceString);
            byte[] byteDecompress = DeCompressBytes(byteSource);
            if (byteDecompress != null)
            {
                return Encoding.UTF8.GetString(byteDecompress);
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}