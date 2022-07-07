using System.Collections.Generic;

namespace FutureEditor
{
    public static class SplitUtil
    {
        public static string[] SplitOneArray(string parserStr)
        {
            if (parserStr.StartsWith("["))
            {
                parserStr = parserStr.Replace("[", string.Empty);
            }
            if (parserStr.EndsWith("]"))
            {
                parserStr = parserStr.Replace("]", string.Empty);
            }

            string[] arr = parserStr.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i].Trim();
            }
            return arr;
        }

        public static string[][] SplitTwoArray(string parserStr)
        {
            parserStr = parserStr.Remove(0, 1);
            string[] arr = parserStr.Split('[');
            string[][] twoArr = new string[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i].Replace("]", string.Empty);
                twoArr[i] = SplitOneArray(arr[i]);
            }
            return twoArr;
        }

        public static string[][] SplitDictionaryTwoArray(string parserStr)
        {
            parserStr = parserStr.Remove(0, 1);
            string[] arr = parserStr.Split('{');
            string[][] twoArr = new string[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i].Replace("}", string.Empty);
                twoArr[i] = SplitOneArray(arr[i]);
            }
            return twoArr;
        }

        public static Dictionary<string, string> SplitDictionary(string parserStr)
        {
            string[][] paramSplit = SplitDictionaryTwoArray(parserStr);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < paramSplit.Length; i++)
            {
                string key = paramSplit[i][0];
                string value = paramSplit[i][1];
                if (key != null && value != null)
                {
                    dict.Add(key, value);
                }
            }
            return dict;
        }
    }
}