/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Text;

namespace FutureCore
{
    public static class Base64EncodeUtil
    {
        public static string Base64Encode(string source)
        {
            return Base64Encode(EncodeConst.UTF8, source);
        }

        public static string Base64Decode(string result)
        {
            return Base64Decode(EncodeConst.UTF8, result);
        }

        public static string Base64EncodeUTF8NoBOM(string source)
        {
            return Base64Encode(EncodeConst.UTF8_NoBOM, source);
        }

        public static string Base64DecodeUTF8NoBOM(string result)
        {
            return Base64Decode(EncodeConst.UTF8_NoBOM, result);
        }

        /// <summary>
        /// 编码
        /// </summary>
        public static string Base64Encode(Encoding encoding, string source)
        {
            byte[] bytes = encoding.GetBytes(source);
            string encode = Convert.ToBase64String(bytes);
            return encode;
        }

        /// <summary>
        /// 解码
        /// </summary>
        public static string Base64Decode(Encoding encoding, string result)
        {
            byte[] bytes = Convert.FromBase64String(result);
            string decode = encoding.GetString(bytes);
            return decode;
        }

        /// <summary>
        /// 编码
        /// </summary>
        public static string Base64EncodeString(byte[] source)
        {
            return Convert.ToBase64String(source);
        }

        /// <summary>
        /// 解码
        /// </summary>
        public static byte[] Base64DecodeBytes(string result)
        {
            return Convert.FromBase64String(result);
        }
    }
}