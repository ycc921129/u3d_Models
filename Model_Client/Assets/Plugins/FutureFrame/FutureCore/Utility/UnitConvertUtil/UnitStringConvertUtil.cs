/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public class UnitStringConvertUtil
    {
        public static string ConvertUnit(string value)
        {
            if (value.Contains("."))
            {
                value = value.Split('.')[0];
            }
            long unit = value.Length;
            if (unit > 0)
            {
                unit--;
            }
            int unitIdx = 0;
            if (unit >= 6)
            {
                unitIdx = (int)Mathf.Ceil(unit / 3) - 1;
            }
            unitIdx = Mathf.Min(unitIdx, UnitConvertUtil.UnitSuffixs.Length - 1);

            int baseNum = unitIdx * 3;
            string numToShow = value.Remove(value.Length - baseNum, baseNum);
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
            return numToShowString + UnitConvertUtil.UnitSuffixs[unitIdx];
        }

        public static string ConvertIntUnit(string value)
        {
            if (value.Contains("."))
            {
                value = value.Split('.')[0];
            }
            if (value.Length >= 4)
            {
                return ConvertUnit(value);
            }
            else
            {
                return value;
            }
        }

        public static string ConvertFloatUnit(string value)
        {
            if (value.Contains("."))
            {
                value = value.Split('.')[0];
            }
            if (value.Length >= 4)
            {
                return ConvertUnit(value);
            }
            else
            {
                return value + ".00";
            }
        }
    }
}