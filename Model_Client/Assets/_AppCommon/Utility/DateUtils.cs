using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectApp
{

    public class DateUtils
    {
        /// <summary>          
        /// 时间戳转为C#格式时间          
        /// </summary>          
        /// <param name=”timeStamp”></param>          
        /// <returns></returns>    
        public static DateTime GetTime(int timeStamp)
        {
            // System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return startTime.AddSeconds(timeStamp);
        }

        /// <summary>
        /// 获取当前客户端Linx时间戳 单位秒
        /// </summary>
        /// <returns></returns>
        public static int GetNowClientTime()
        {
            return ConvertDateTimeInt(System.DateTime.Now);
        }

        /// <summary>
        /// 获取当前小时数的总秒数
        /// </summary>
        /// <returns></returns>
        public static int GetNowHourTimeStamp()
        {
            return DateTime.Now.Hour * 3600;
        }

        /// <summary>
        /// 获取当前分钟数的总秒数
        /// </summary>
        /// <returns></returns>
        public static int GetNowMinuteTimeStamp()
        {
            return DateTime.Now.Minute * 60;
        }

        /// <summary>
        /// 获取当前的秒数
        /// </summary>
        /// <returns></returns>
        public static int GetNowSecondTimeStamp()
        {
            return DateTime.Now.Second;
        }

        /// <summary>
        /// 获取当前时间从今天的0点开始到现在的时间戳
        /// </summary>
        /// <returns></returns>
        public static int GetNowTimeStampFromZeroOClock()
        {
            return GetNowHourTimeStamp() + GetNowMinuteTimeStamp() + GetNowSecondTimeStamp();
        }

        /// <summary>   
        /// DateTime时间格式转换为Unix时间戳格式          
        /// </summary>          
        /// <param name=”time”></param>          
        /// <returns></returns>   
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 剩余时间
        /// </summary>
        /// <param name="diff"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetTimeDiffShowAll(int diff)
        {
            // 时间差大于0
            if (diff >= 0)
            {
                // 获取天数
                int d = (int)Mathf.Floor(diff / 60 / 60 / 24);
                // 取余，余下的数值就是时、分、秒
                diff %= 60 * 60 * 24;
                // 获取小时数
                int h = (int)Mathf.Floor(diff / 60 / 60);
                // 取余，余下的数值就是分、秒
                diff %= 60 * 60;
                // 获取分钟数
                int m = (int)Mathf.Floor(diff / 60);
                // 取余，余下的数值就是秒
                int s = diff % 60;

                if (d > 0)
                {
                    return d + "天" + FixZero(h) + "时" + FixZero(m) + "分";
                }
                if (d == 0 && h > 0)
                {
                    return FixZero(h) + "时" + FixZero(m) + "分" + FixZero(s) + "秒";
                }
                if (d == 0 && h == 0 && m > 0)
                {
                    return FixZero(m) + "分" + FixZero(s) + "秒";
                }
                if (d == 0 && h == 0 && m == 0 && s > 0)
                {
                    return FixZero(s) + "秒";
                }
            }

            return diff.ToString();
        }

        ///<summary>
        /// 获取时间差值 分：秒
        /// 为0不显示 
        ///</summary>
        public static string GetTimeDiffMinuteAndSecond(int diff)
        {
            // 时间差大于0
            if (diff >= 0)
            {
                // 获取天数
                //  int d = (int)Mathf.Floor(diff / 60 / 60 / 24);
                // 取余，余下的数值就是时、分、秒
                diff %= 60 * 60 * 24;
                // 获取小时数
                int h = (int)Mathf.Floor(diff / 60 / 60);
                // 取余，余下的数值就是分、秒
                diff %= 60 * 60;
                // 获取分钟数
                int m = (int)Mathf.Floor(diff / 60);
                // 取余，余下的数值就是秒
                int s = diff % 60;

                string diffStr = "";
                //diffStr += FixZero(h) + ":";
                diffStr += FixZero(m) + ":";
                diffStr += FixZero(s);
                return diffStr;
            }
            return "0";//diff.ToString();
        }


        ///<summary>
        /// 获取时间差值 时：分：秒
        /// 为0不显示 
        ///</summary>
        public static string GetTimeDiff(int diff)
        {
            // 时间差大于0
            if (diff >= 0)
            {
                // 获取天数
                //  int d = (int)Mathf.Floor(diff / 60 / 60 / 24);
                // 取余，余下的数值就是时、分、秒
                diff %= 60 * 60 * 24;
                // 获取小时数
                int h = (int)Mathf.Floor(diff / 60 / 60);
                // 取余，余下的数值就是分、秒
                diff %= 60 * 60;
                // 获取分钟数
                int m = (int)Mathf.Floor(diff / 60);
                // 取余，余下的数值就是秒
                int s = diff % 60;

                string diffStr = "";
                diffStr += FixZero(h) + ":";
                diffStr += FixZero(m) + ":";
                diffStr += FixZero(s);
                return diffStr;
            }
            return "0";//diff.ToString();
        }
        

        public static string FixZero(int num)
        {
            // 数值小于10的前面加0。
            // 转换为字符串
            return (num < 10) ? '0' + num.ToString() : num.ToString();
        }
    }
}