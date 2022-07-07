/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Text;

namespace FutureCore
{
    public static class BytesUtil
    {
        private static Encoding TextEncode = Encoding.UTF8;

        public static byte[] Combine(byte[] first, byte[] second, int secondLength)
        {
            byte[] ret = new byte[first.Length + secondLength];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, secondLength);
            return ret;
        }

        public static byte[] Combine(byte[] bytes1, byte[] bytes2)
        {
            byte[] concatbytes = new byte[bytes1.Length + bytes2.Length];
            Array.Copy(bytes1, concatbytes, bytes1.Length);
            Array.Copy(bytes2, 0, concatbytes, bytes1.Length, bytes2.Length);
            return concatbytes;
        }

        public static byte[] String2Bytes(string str)
        {
            return TextEncode.GetBytes(str);
        }

        public static string Bytes2String(byte[] bytes)
        {
            return TextEncode.GetString(bytes);
        }

        public static string GetHexDesc(byte[] bytes)
        {
            string str = string.Empty;
            if (bytes == null) return str;
            StringBuilder sber = new StringBuilder();
            int num = 1;
            foreach (byte b in bytes)
            {
                sber.AppendFormat("{0:X2}", b);
                if (num != bytes.Length)
                {
                    sber.Append(" ");
                }
                num++;
            }
            return sber.ToString();
        }

        public static byte[] GetBytesByHexDesc(string desc)
        {
            string[] descs = desc.Split(' ');
            byte[] byteArray = new byte[descs.Length];
            for (byte i = 0; i < byteArray.Length; i++)
            {
                byte b = (byte)Convert.ToInt32(descs[i], 16);
                byteArray[i] = b;
            }
            return byteArray;
        }

        public static string GetByteDesc(byte[] bytes)
        {
            string str = string.Empty;
            if (bytes == null) return str;
            StringBuilder sber = new StringBuilder();
            int num = 1;
            foreach (byte b in bytes)
            {
                sber.AppendFormat(b.ToString());
                if (num != bytes.Length)
                {
                    sber.Append(" ");
                }
                num++;
            }
            return sber.ToString();
        }

        public static byte[] GetBytesByByteDesc(string desc)
        {
            string[] descs = desc.Split(' ');
            byte[] byteArray = new byte[descs.Length];
            for (byte i = 0; i < byteArray.Length; i++)
            {
                byte b = (byte)Convert.ToInt32(descs[i]);
                byteArray[i] = b;
            }
            return byteArray;
        }
    }
}