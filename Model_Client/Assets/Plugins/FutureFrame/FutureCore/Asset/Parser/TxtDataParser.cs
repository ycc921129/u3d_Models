/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.2
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FutureCore
{
    public class TxtDataParser : IDisposable
    {
        private List<string> lineList;
        private Dictionary<string, string[]> keyDic;

        public TxtDataParser(string path)
        {
            lineList = new List<string>();
            keyDic = new Dictionary<string, string[]>();
            Read(path);
        }

        public void Read(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    string lineStr;
                    while ((lineStr = sr.ReadLine()) != null)
                    {
                        lineStr = lineStr.Trim();
                        lineList.Add(lineStr);
                        string[] s = lineStr.Split(SplitConst.DefaultText);
                        string[] values = new string[s.Length - 1];
                        for (int i = 1; i < s.Length; i++)
                        {
                            values[i - 1] = s[i];
                        }
                        keyDic.Add(s[0], values);
                    }
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError(e.ToString());
                throw;
            }
        }

        public string[] GetValue(string key)
        {
            if (keyDic.ContainsKey(key))
            {
                return keyDic[key];
            }
            return null;
        }

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < lineList.Count; i++)
            {
                yield return lineList[i];
            }
        }

        public void Dispose()
        {
            lineList.Clear();
            keyDic.Clear();
            lineList = null;
            keyDic = null;
        }
    }
}