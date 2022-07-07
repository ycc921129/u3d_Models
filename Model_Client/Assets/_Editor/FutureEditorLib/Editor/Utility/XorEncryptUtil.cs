/*
 Author:du
 Time:2017.10.31
*/

using System.Text;

namespace FutureEditor
{
    public static class XorEncryptUtil
    {
        private static byte[] key = new byte[]
        {
            0XE6, 0X80, 0XAA, 0XE7, 0X89, 0XA9, 0XE7, 0XA7, 0X91, 0XE6, 0X8A, 0X80
        };

        public static byte[] Encrypt(byte[] buffer)
        {
            int len = key.Length;
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)(buffer[i] ^ key[i % len]);
            }
            return buffer;
        }

        public static string EncryptString(byte[] buffer)
        {
            buffer = Encrypt(buffer);
            return Encoding.UTF8.GetString(buffer);
        }
    }
}