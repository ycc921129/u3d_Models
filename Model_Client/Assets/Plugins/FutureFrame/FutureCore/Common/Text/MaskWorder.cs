/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using System.Text;

namespace FutureCore
{
    public class MaskWorder
    {
        private Dictionary<char, List<string>> maskWordLib;

        public MaskWorder(string[] content)
        {
            BuildMaskWordLib(content);
        }

        /// <summary>
        /// 构建屏蔽字库
        /// </summary>
        private void BuildMaskWordLib(string[] content)
        {
            maskWordLib = new Dictionary<char, List<string>>();
            foreach (string shieldStr in content)
            {
                if (string.IsNullOrEmpty(shieldStr))
                    continue;

                string newStr = shieldStr.Trim();
                if (string.IsNullOrEmpty(newStr))
                    continue;

                char firstChar = newStr[0];
                if (maskWordLib.ContainsKey(firstChar))
                {
                    if (!maskWordLib[firstChar].Contains(newStr))
                    {
                        maskWordLib[firstChar].Add(newStr);
                    }
                }
                else
                {
                    maskWordLib.Add(firstChar, new List<string> { newStr });
                }
            }
        }

        /// <summary>
        /// 是否通过屏蔽字库
        /// </summary>
        public bool Search(string rawStr)
        {
            bool isPass = true;

            if (string.IsNullOrEmpty(rawStr))
                return isPass;

            int rawLen = rawStr.Length;
            for (int i = 0; i < rawLen; i++)
            {
                char checkChar = rawStr[i];
                if (maskWordLib.ContainsKey(checkChar))
                {
                    foreach (string keyStr in maskWordLib[checkChar])
                    {
                        if (rawStr.Contains(keyStr))
                        {
                            isPass = false;
                            return isPass;
                        }
                    }
                }
            }
            return isPass;
        }

        /// <summary>
        /// 获取可通过屏蔽字库的文本
        /// </summary>
        public string Replace(string rawStr)
        {
            bool isPass = true;
            string outStr = string.Empty;

            if (string.IsNullOrEmpty(rawStr))
                return outStr;

            int rawLen = rawStr.Length;
            StringBuilder sb = new StringBuilder(rawLen);
            for (int i = 0; i < rawLen; i++)
            {
                bool isCheckPass = true;
                char checkChar = rawStr[i];
                if (maskWordLib.ContainsKey(checkChar))
                {
                    foreach (string keyStr in maskWordLib[checkChar])
                    {
                        if (rawStr.Contains(keyStr))
                        {
                            isPass = false;
                            isCheckPass = false;
                            int starLen = keyStr.Length;
                            sb.Append('*', starLen);
                            i += starLen - 1;
                            break;
                        }
                    }
                }
                if (isCheckPass)
                {
                    sb.Append(checkChar);
                }
            }

            if (isPass)
            {
                outStr = rawStr;
            }
            else
            {
                outStr = sb.ToString();
            }
            return outStr;
        }
    }
}