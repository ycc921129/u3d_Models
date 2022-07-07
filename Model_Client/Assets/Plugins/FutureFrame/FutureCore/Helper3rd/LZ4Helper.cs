/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public static class LZ4Helper
    {
        public static int Level = 3;
        public static int ProtoLevel = 3;
        private const bool IncludeSize = false;
        private const bool UseFooter = false;
        private const bool IsSafe = true;

        // 固定缓冲区1MB
        private static byte[] FixedOutBuffer = new byte[1024 * 1024];
        // 复用缓冲区
        private static byte[] ReusableOutBuff;

        public static int CompressBufferPartialFixed(byte[] inBuffer, int partialIndex, out byte[] outBuffer)
        {
            int compressedSize = LZ4.compressBufferPartialFixed(inBuffer, ref FixedOutBuffer, partialIndex, ProtoLevel, IncludeSize);
            outBuffer = FixedOutBuffer;
            return compressedSize;
        }

        public static int DecompressBufferPartialFixed(byte[] inBuffer, int partialIndex, int lengthOffset,  int rawSize, out byte[] outBuffer)
        {
            int bufferSize = inBuffer.Length - partialIndex - lengthOffset;
            int decommpressedSize = LZ4.decompressBufferPartialFixed(inBuffer, ref FixedOutBuffer, partialIndex + lengthOffset, bufferSize, IsSafe, UseFooter, rawSize);
            outBuffer = FixedOutBuffer;
            return decommpressedSize;
        }

        public static bool CompressBuffer(byte[] inBuffer, out byte[] outBuffer)
        {
            bool res = LZ4.compressBuffer(inBuffer, ref ReusableOutBuff, Level);
            outBuffer = ReusableOutBuff;
            return res;
        }

        public static bool DecompressBuffer(byte[] inBuffer, out byte[] outBuffer)
        {
            bool res = LZ4.decompressBuffer(inBuffer, ref ReusableOutBuff);
            outBuffer = ReusableOutBuff;
            return res;
        }
    }
}