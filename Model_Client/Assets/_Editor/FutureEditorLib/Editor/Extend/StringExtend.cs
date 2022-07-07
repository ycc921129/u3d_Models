/*
 Author:du
 Time:2017.11.3
*/

using System;
using System.Collections.Generic;

namespace FutureEditor
{
    public static class StringExtend
    {
        private const string BoolTrueStr = "True";
        private const string BooltrueStr = "true";
        private const string BoolTRUEStr = "TRUE";
        private const string BoolTrueNumStr = "1";

        public static float ToFloat(this string str)
        {
            float temp = 0;
            float.TryParse(str, out temp);
            return temp;
        }

        public static double ToDouble(this string str)
        {
            double temp = 0d;
            double.TryParse(str, out temp);
            return temp;
        }

        public static byte ToByte(this string str)
        {
            byte temp = 0;
            byte.TryParse(str, out temp);
            return temp;
        }

        public static sbyte ToSByte(this string str)
        {
            sbyte temp = 0;
            sbyte.TryParse(str, out temp);
            return temp;
        }

        public static short ToShort(this string str)
        {
            short temp = 0;
            short.TryParse(str, out temp);
            return temp;
        }

        public static ushort ToUShort(this string str)
        {
            ushort temp = 0;
            ushort.TryParse(str, out temp);
            return temp;
        }

        public static int ToInt(this string str)
        {
            int temp = 0;
            int.TryParse(str, out temp);
            return temp;
        }

        public static uint ToUInt(this string str)
        {
            uint temp = 0;
            uint.TryParse(str, out temp);
            return temp;
        }

        public static long ToLong(this string str)
        {
            long temp = 0;
            long.TryParse(str, out temp);
            return temp;
        }

        public static ulong ToULong(this string str)
        {
            ulong temp = 0;
            ulong.TryParse(str, out temp);
            return temp;
        }

        public static bool ToBool(this string str)
        {
            return str == BoolTrueStr || str == BooltrueStr || str == BoolTRUEStr || str == BoolTrueNumStr;
        }

        public static string[] ToSplitOneArray(this string str)
        {
            return SplitUtil.SplitOneArray(str);
        }

        public static string[][] ToSplitTwoArray(this string str)
        {
            return SplitUtil.SplitTwoArray(str);
        }

        public static Dictionary<string, string> ToDictionary(this string str)
        {
            return SplitUtil.SplitDictionary(str);
        }

        public static object ToValueByType(this string str, string type)
        {
            switch (type)
            {
                case "float":
                    return str.ToFloat();
                case "double":
                    return str.ToDouble();
                case "byte":
                    return str.ToByte();
                case "sbyte":
                    return str.ToSByte();
                case "short":
                    return str.ToShort();
                case "ushort":
                    return str.ToUShort();
                case "int":
                    return str.ToInt();
                case "uint":
                    return str.ToUInt();
                case "long":
                    return str.ToLong();
                case "ulong":
                    return str.ToULong();
                case "bool":
                    return str.ToBool();
                case "string":
                    return str;
            }
            return null;
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        #region Optimize
        public static bool OptimizeStartsWith(this string str, string other)
        {
            return str.StartsWith(other, StringComparison.Ordinal);
        }

        public static bool OptimizeEndsWith(this string str, string other)
        {
            return str.EndsWith(other, StringComparison.Ordinal);
        }

        public static bool OptimizeEquals(this string str, string other)
        {
            return str.Equals(other, StringComparison.Ordinal);
        }
        #endregion
    }
}