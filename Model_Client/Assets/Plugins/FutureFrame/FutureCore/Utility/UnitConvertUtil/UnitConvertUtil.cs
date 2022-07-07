/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public class UnitConvertUtil
    {
        public static string[] UnitSuffixs = { "", "K", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj ", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", "ca", "cb", "cc", "cd", "ce", "cf", "cg", "ch", "ci", "cj", "ck", "cl", "cm", "cn", "co", "cp", "cq", "cr", "cs", "ct", "cu", "cv", "cw", "cx", "cy", "da", "db", "dc", "dd", "de", "df", "dg", "dh", "di", "dj", "dk", "dl", "dm", "dn", "do", "dp", "dq", "dr", "ds", "dt", "du", "dv", "dw", "dx", "dy" };

        #region 最新需求

        /// <summary>
        /// 转换成xxx+单位的形式 如999K,没有小数点
        /// </summary>
        public static string ConvertIntUnitStr(double value)
        {
            int count = GetDigitNumber(value);
            int unitNum = 0;
            while (count > 3)
            {
                count -= 3;
                value /= 1000;
                unitNum++;
            }
            return (int)value + UnitSuffixs[unitNum];
        }

        /// <summary>
        /// 小于1000显示xxx,大于1000显示xxx.xx+单位的形式
        /// </summary>
        public static string ConvertFloatUnitStr(double value, int decimalNum = 2, int unitNameNum = 3)
        {
            int count = GetDigitNumber(value);
            int unitNum = 0;
            while (count > unitNameNum)
            {
                count -= 3;
                value /= 1000;
                unitNum++;
            }
            if (unitNum == 0)
            {
                return value.ToString("f0");
            }
            return value.ToString("f" + decimalNum) + UnitSuffixs[unitNum];
        }

        public static int GetDigitNumber(double value)
        {
            int count = 0;
            while (value > 1)
            {
                count++;
                value = value / 10;
            }
            return count;
        }

        #endregion 最新需求

        /// <summary>
        /// 单位换算显示
        /// 换算成1k这种显示
        /// </summary>
        public static string ConvertUnit(int value)
        {
            int unit = GetDigitNumber(value);
            if (unit > 0)
            {
                unit--;
            }
            int unitIdx = 0;
            if (unit >= 6)
            {
                unitIdx = (int)Mathf.Ceil(unit / 3) - 1;
            }
            unitIdx = Mathf.Min(unitIdx, UnitSuffixs.Length - 1);

            float baseNum = Mathf.Pow(10, unitIdx * 3);
            string numToShow = Mathf.Ceil(value / baseNum).ToString();
            string numToShowString = string.Empty;
            int j = 0;
            for (int i = numToShow.Length - 1; i >= 0; i--)
            {
                j++;
                numToShowString = numToShow[i] + numToShowString;
                if (i != 0 && j % 3 == 0)
                {
                    numToShowString = "," + numToShowString;
                }
            }
            return numToShowString + UnitSuffixs[unitIdx];
        }

        public static string ConvertUnit(long value)
        {
            long unit = GetDigitNumber(value);
            if (unit > 0)
            {
                unit--;
            }
            int unitIdx = 0;
            if (unit >= 6)
            {
                unitIdx = (int)Mathf.Ceil(unit / 3) - 1;
            }
            unitIdx = Mathf.Min(unitIdx, UnitSuffixs.Length - 1);

            float baseNum = Mathf.Pow(10, unitIdx * 3);
            string numToShow = Mathf.Ceil(value / baseNum).ToString();
            string numToShowString = string.Empty;
            int j = 0;
            for (int i = numToShow.Length - 1; i >= 0; i--)
            {
                j++;
                numToShowString = numToShow[i] + numToShowString;
                if (i != 0 && j % 3 == 0)
                {
                    numToShowString = "," + numToShowString;
                }
            }
            return numToShowString + UnitSuffixs[unitIdx];
        }

        public static string ConvertIntUnit(double value)
        {
            return ConvertIntUnitStr(value);
        }

        public static string ConvertFloatUnit(double value)
        {
            if (value >= 1000)
            {
                return ConvertUnit((int)value);
            }
            else
            {
                return value.ToString("0.00");
            }
        }

        /// <summary>
        /// 返回最多七位，没有小数的格式,每3位加点
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertNoDecimalUnit(double value, string symbol = ".")
        {
            int count = GetDigitNumber(value);
            int unitNum = 0;
            while (count > 7)
            {
                count -= 3;
                value /= 1000;
                unitNum++;
            }
            //if (unitNum == 0)
            //{
            //    return value.ToString("f0");
            //}
            string valueStr = value.ToString("f0");
            string unitStr = UnitSuffixs[unitNum];
            for (int i = 0; i < (valueStr.Length - 1) / 3; i++)
            {
                int index = valueStr.Length % 3 + i * ((valueStr.Length - 1) / 3) + i;
                valueStr = valueStr.Insert(index, symbol);
            }
            if (valueStr.StartsWith(symbol))
                valueStr = valueStr.Remove(0, 1);
            return valueStr + unitStr;
        }
    }
}