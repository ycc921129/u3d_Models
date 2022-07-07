/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Text;

namespace FutureCore
{
    public static class StringGCUtil
    {
        public static StringBuilder Builder = new StringBuilder(2048);
        public static string Str = GetGarbageFreeString(Builder);
        private const char s_Space = ' ';

        private static string GetGarbageFreeString(StringBuilder stringBuilder)
        {
            return (string)Builder.GetType().GetField("_str", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(Builder);
        }

        public static string Format(string format, params object[] args)
        {
            if (format == null) return string.Empty;
            if (args == null) return string.Empty;

            GarbageFreeClear();
            Builder.AppendFormat(format, args);
            return Str;
        }

        public static void GarbageFreeClear()
        {
            Builder.Length = 0;
            Builder.Append(s_Space, Builder.Capacity);
            Builder.Length = 0;
        }

        public static void Clear()
        {
            Builder.Remove(0, Builder.Length);
        }

        public static string GetString()
        {
            return Str;
        }
    }
}