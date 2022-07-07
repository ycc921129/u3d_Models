/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
    消息包：混淆长度(包名+协议名) + 混淆数据 + 协议加密数据(协议名+数据)
*/

using System;
using System.IO;
using System.Text;

namespace FutureCore
{
    public static class JsonEncryptUtil
    {
        private const int MsgFirstLength = 1;
        private static char[] RandomKeys = "0123456789qwertyuiopasdfghjklzxcvbnm".ToCharArray();

        private const int LZ4_SKIP_SIZE = 128;
        private const int LZ4_CompressionFlagOffset = 1;
        private const int LZ4_LengthFlagOffset = 4;
        public const byte LZ4_UnCompressionFlag = 0x00;
        public const byte LZ4_BeenCompressedFlag = 0x01;
        private readonly static byte[] LZ4_UnCompressionFlagBytes = { LZ4_UnCompressionFlag };
        private readonly static byte[] LZ4_BeenCompressedFlagBytes = { LZ4_BeenCompressedFlag };

        public static string NoEncryptJsonProto(BaseJsonProto jsonProtoMsg, Type type)
        {
            string json = SerializeUtil.ToJson(jsonProtoMsg, type);
            return json;
        }       

        public static byte[] NoEncryptLZ4JsonProto(BaseJsonProto jsonProtoMsg, Type type)
        {
            byte[] sendBytes = null;
            string json = SerializeUtil.ToJson(jsonProtoMsg, type);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

            int jsonLength = jsonBytes.Length;
            if (jsonLength > LZ4_SKIP_SIZE)
            {
                byte[] jsonLengthBytes = BitConverter.GetBytes(jsonLength);
                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(jsonLengthBytes);
                }

                byte[] lz4CompressBytes;
                int lz4CompressLength = LZ4Helper.CompressBufferPartialFixed(jsonBytes, 0, out lz4CompressBytes);
                //LogUtil.LogFormat("[JsonEncryptUtil]LZ4测试压缩比: {0}/{1} {2} level:{3}", lz4CompressLength, jsonLength, lz4CompressLength / (float)jsonLength * 100.0f + "%", LZ4Helper.Level);
                if (lz4CompressLength > 0)
                {
                    sendBytes = BytesUtil.Combine(jsonLengthBytes, lz4CompressBytes, lz4CompressLength);
                    sendBytes = BytesUtil.Combine(LZ4_BeenCompressedFlagBytes, sendBytes);
                }
                else
                {
                    sendBytes = BytesUtil.Combine(LZ4_UnCompressionFlagBytes, jsonBytes);
                    LogUtil.LogError("[JsonEncryptUtil]NoEncryptLZ4JsonProto Fail!");
                }
            }
            else
            {
                sendBytes = BytesUtil.Combine(LZ4_UnCompressionFlagBytes, jsonBytes);
            }
            return sendBytes;
        }

        public static string DecompressNoEncryptLZ4JsonProto(byte[] bytes)
        {
            int rawSize = BitConverter.ToInt32(bytes, LZ4_CompressionFlagOffset);
            byte[] jsonDecompressBytes;
            int lz4DecompressLength = LZ4Helper.DecompressBufferPartialFixed(bytes, LZ4_CompressionFlagOffset, LZ4_LengthFlagOffset, rawSize, out jsonDecompressBytes);
            if (lz4DecompressLength > 0)
            {
                return Encoding.UTF8.GetString(jsonDecompressBytes, 0, lz4DecompressLength);
            }
            else
            {
                LogUtil.LogError("[JsonEncryptUtil]DecompressLZ4JsonProto Fail!");
                return null;
            }
        }

        public static string NoDecompressNoEncryptLZ4JsonProto(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes, LZ4_CompressionFlagOffset, bytes.Length - LZ4_CompressionFlagOffset);
        }

        public static byte[] EncryptJsonProto(BaseJsonProto jsonProtoMsg, Type type)
        {
            string json = SerializeUtil.ToJson(jsonProtoMsg, type);
            return Encrypt(json, jsonProtoMsg.type.Length);
        }

        public static string DecryptJsonProto(byte[] data)
        {
            return Decrypt(data);
        }

        private static byte[] Encrypt(string json, int protoLength)
        {
            byte[] aesData = AESEncryptUtil.Encrypt(json);

            byte randomLength = (byte)(AppConst.PackageName.Length + protoLength);
            int totalLength = MsgFirstLength + randomLength + aesData.Length;
            byte[] finalBytes = new byte[totalLength];
            finalBytes[0] = randomLength;

            char[] randomDataArray = GetRandomChars(randomLength);
            for (int i = 0; i < randomLength; i++)
            {
                finalBytes[MsgFirstLength + i] = (byte)randomDataArray[i];
            }
            for (int i = MsgFirstLength + randomLength; i < totalLength; i++)
            {
                finalBytes[i] = aesData[i - randomLength - MsgFirstLength];
            }

            return finalBytes;
        }

        private static string Decrypt(byte[] data)
        {
            byte randomLength = data[0];
            int totalLength = data.Length;
            int aesDataLength = totalLength - randomLength - MsgFirstLength;
            byte[] dataBytes = new byte[aesDataLength];
            for (int i = 0; i < aesDataLength; i++)
            {
                dataBytes[i] = data[MsgFirstLength + randomLength + i];
            }
            string json = AESEncryptUtil.Decrypt(dataBytes);
            return json;
        }

        private static char[] GetRandomChars(int length)
        {
            char[] randomChar = new char[length];
            int keysLength = RandomKeys.Length;
            for (int i = 0; i < length; i++)
            {
                float randomIdx = MathUtil.RandomZeroOne() * keysLength;
                int index = MathUtil.CeilToInt(randomIdx);
                index = MathUtil.ClampMinMax(index, 0, keysLength - 1);
                randomChar[i] = RandomKeys[index];
            }
            return randomChar;
        }

        public static string EncryptString(string name, string raw, string associatedData)
        {
            byte[] bytes = AESEncryptUtil.Encrypt(raw);
            string text = Base64EncodeUtil.Base64EncodeString(bytes);
            return text;
        }

        public static string DecryptString(string name, string encryptedValue, string associatedData)
        {
            byte[] aesBytes = Base64EncodeUtil.Base64DecodeBytes(encryptedValue);
            string text = AESEncryptUtil.Decrypt(aesBytes);
            return text;
        }

        public static void WriteObjectInLocalFile(string path, object obj, string key)
        {
            string text = SerializeUtil.ToJson(obj);
            WriteInLocalFile(path, text, key);
        }

        public static void WriteInLocalFile(string path, string text, string key)
        {
            text = Channel.Current.encryptedString(path, text, key);
            File.WriteAllText(path, text);
        }

        public static T ReadFormText<T>(string path, string text, string key) where T : class
        {
            text = Channel.Current.decryptedString(path, text, key);
            if (string.IsNullOrEmpty(text))
            {
                LogUtil.LogError("[JsonEncryptUtil]ReadFormText decryptedString Fail: " + path);
                return null;
            }
            return SerializeUtil.ToObject<T>(text);
        }

        public static T ReadFormLocalFile<T>(string path, string key) where T : class
        {
            string text = File.ReadAllText(path);
            text = Channel.Current.decryptedString(path, text, key);
            if (string.IsNullOrEmpty(text))
            {
                LogUtil.LogError("[JsonEncryptUtil]ReadFormLocalFile decryptedString Fail: " + path);
                return null;
            }
            return SerializeUtil.ToObject<T>(text);
        }
    }
}