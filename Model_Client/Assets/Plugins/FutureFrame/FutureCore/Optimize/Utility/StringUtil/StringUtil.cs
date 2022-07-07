/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Text;

namespace FutureCore
{
    public static class StringUtil
    {
        #region 字符串拼接

        private static StringBuilder stringBuilder = new StringBuilder();
        private static StringBuilder shareStringBuilder = new StringBuilder();

        public static StringBuilder GetShareStringBuilder()
        {
            shareStringBuilder.Remove(0, stringBuilder.Length);
            return shareStringBuilder;
        }

        public static void ClearShareStringBuilder()
        {
            shareStringBuilder.Remove(0, stringBuilder.Length);
        }

        public static string Format(string src, params object[] args)
        {
            stringBuilder.Remove(0, stringBuilder.Length);
            stringBuilder.AppendFormat(src, args);
            return stringBuilder.ToString();
        }

        public static string Concat(string s1, string s2)
        {
            stringBuilder.Remove(0, stringBuilder.Length);
            stringBuilder.Append(s1);
            stringBuilder.Append(s2);
            return stringBuilder.ToString();
        }

        public static string Concat(string s1, string s2, string s3)
        {
            stringBuilder.Remove(0, stringBuilder.Length);
            stringBuilder.Append(s1);
            stringBuilder.Append(s2);
            stringBuilder.Append(s3);
            return stringBuilder.ToString();
        }

        #endregion

        #region 字符串查询

        public static bool StartsWith(string str, string other)
        {
            return str.StartsWith(other, StringComparison.Ordinal);
        }

        public static bool EndsWith(string str, string other)
        {
            return str.EndsWith(other, StringComparison.Ordinal);
        }

        public static bool Equals(string str, string other)
        {
            return str.Equals(other, StringComparison.Ordinal);
        }

        #endregion
    }
}