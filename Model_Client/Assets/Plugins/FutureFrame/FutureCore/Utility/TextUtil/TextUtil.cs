/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public static class TextUtil
    {
        /// <summary>
        /// 是否为非法文本
        /// 0-9（48-57） a-z（97-123） A-Z（65-90）
        /// </summary>
        public static bool IsIllegalText(string text)
        {
            char[] chars = text.ToLower().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                if (!((c >= 48 && c <= 57) || (c >= 97 && c <= 123)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}