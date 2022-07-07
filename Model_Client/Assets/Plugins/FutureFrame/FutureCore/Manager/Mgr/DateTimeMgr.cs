/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;

namespace FutureCore
{
    public enum MonthType : int
    {
        None = 0,
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12,
    }

    public sealed class DateTimeMgr : BaseMgr<DateTimeMgr>
    {
        /// <summary>
        /// Unix时间戳
        /// </summary>
        private static DateTime StartTimestampDT = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

        /// <summary>
        /// 心跳间隔
        /// </summary>
        private long heartBeatInterval = 15;
        /// <summary>
        /// 客户端当前时间与服务器当前时间的差值 (单向)
        /// </summary>
        public int ServerTimeOffset { get; private set; }
        /// <summary>
        /// 记录每次下发的服务器时间
        /// </summary>
        public long ServerTickTimestamp { get; private set; }

        #region CurrTime
        public long GetCurrTimestamp()
        {
            long timestamp = (long)(DateTime.Now - StartTimestampDT).TotalSeconds * 1000;
            return timestamp;
        }

        public string GetCurrTimestampInfo()
        {
            long timestamp = (long)(DateTime.Now - StartTimestampDT).TotalSeconds * 1000;
            return timestamp.ToString();
        }
        #endregion

        #region ServerCurrTime
        public void SetHeartBeatTime(int heartBeatInterval)
        {
            this.heartBeatInterval = heartBeatInterval;
        }

        public long GetHeartBeatTime()
        {
            return heartBeatInterval;
        }

        public void SetServerCurrTimestamp(long serverCurrTimestamp)
        {
            ServerTickTimestamp = serverCurrTimestamp;
            ServerTimeOffset = (int)(GetCurrTimestamp() - serverCurrTimestamp);
        }

        /// <summary>
        /// 获取服务器时间偏移
        /// </summary>
        public float GetServerTimeOffset()
        {
            return ServerTimeOffset;
        }

        /// <summary>
        /// 获取服务器心跳更新时间戳
        /// </summary>
        public long GetServerTickTimestamp()
        {
            return ServerTickTimestamp;
        }

        /// <summary>
        /// 服务器更新时间
        /// </summary>
        public DateTime GetServerTickDateTime()
        {
            return GetDateTime(ServerTickTimestamp);
        }

        /// <summary>
        /// 实时服务器时间戳
        /// </summary>
        public long GetServerCurrTimestamp()
        {
            return GetCurrTimestamp() - ServerTimeOffset;
        }

        /// <summary>
        /// 实时服务器时间
        /// </summary>
        public DateTime GetServerCurrDateTime()
        {
            long timestamp = GetServerCurrTimestamp();
            return GetDateTime(timestamp);
        }
        #endregion

        #region Interval
        public long GetCurrTimeInterval(long timestamp)
        {
            return timestamp - GetCurrTimestamp();
        }

        public long GetServerCurrTimeInterval(long timestamp)
        {
            return timestamp - GetServerCurrTimestamp();
        }

        /// <summary>
        /// 根据时间戳获得剩余天数
        /// </summary>
        public int GetInteralDay(ulong time)
        {
            ulong interal = time - (ulong)GetServerCurrTimestamp();
            int day = UnityEngine.Mathf.CeilToInt(interal * 1f / (60 * 60 * 24));
            return day;
        }

        public void GetIntervalHMS(long interval, out int hour, out int minute, out int second)
        {
            second = (int)(interval % 60);
            int tempMinute = (int)(interval / 60);
            hour = tempMinute / 60;
            minute = tempMinute - (hour * 60);
        }

        public void GetIntervalMS(long interval, out int minute, out int second)
        {
            second = (int)(interval % 60);
            minute = (int)(interval / 60);
        }

        public string GetIntervalHMSTextEn(long interval, int addHour = 0)
        {
            int hour, minute, second;
            GetIntervalHMS(interval, out hour, out minute, out second);
            return string.Format("{0:00}:{1:00}:{2:00}", hour + addHour, minute, second);
        }

        public string GetIntervalHMSTextCn(long interval)
        {
            int hour, minute, second;
            GetIntervalHMS(interval, out hour, out minute, out second);
            return string.Format("{0:00}：{1:00}：{2:00}", hour, minute, second);
        }

        public string GetIntervalMSTextEn(long interval)
        {
            int minute, second;
            GetIntervalMS(interval, out minute, out second);
            return string.Format("{0:00}:{1:00}", minute, second);
        }

        public string GetIntervalMSTextCn(long interval)
        {
            int minute, second;
            GetIntervalMS(interval, out minute, out second);
            return string.Format("{0:00}：{1:00}", minute, second);
        }

        public string GetIntervalDateSimpleString(long interval)
        {
            DateTime dateTime = GetDateTime(interval);
            return DateTimeToSimpleString(dateTime);
        }
        #endregion

        #region DateTime
        public DateTime GetCurrDateTime()
        {
            return DateTime.Now;
        }

        public double GetTimestamp(DateTime date)
        {
            return (date - StartTimestampDT).TotalSeconds;
        }

        public DateTime GetDateTime(long timestamp)
        {
            timestamp /= 1000;
            DateTime dt = StartTimestampDT.AddSeconds(timestamp);
            return dt;
        }

        public int GetCurrTimeZone()
        {
            return int.Parse(GetCurrDateTime().ToString("%z"));
        }

        public int GetNowYear()
        {
            DateTime time = GetCurrDateTime();
            return time.Year;
        }

        public int GetNowMonth()
        {
            DateTime time = GetCurrDateTime();
            return time.Month;
        }

        public int GetNowDay()
        {
            DateTime time = GetCurrDateTime();
            return time.Day;
        }

        public int GetNowHour()
        {
            DateTime time = GetCurrDateTime();
            return time.Hour;
        }

        public int GetNowMinute()
        {
            DateTime time = GetCurrDateTime();
            return time.Minute;
        }

        public int GetNowSecond()
        {
            DateTime time = GetCurrDateTime();
            return time.Second;
        }

        public int GetNowMillisecond()
        {
            DateTime time = GetCurrDateTime();
            return time.Millisecond;
        }
        #endregion

        #region DateTimeFormat
        public string DateTimeToMMdd(DateTime date)
        {
            return date.ToString("MM/dd");
        }

        public string DateTimeToYYYYMMDD(DateTime time)
        {
            return time.ToString("yyyyMMdd");
        }

        public string DateTimeToSimpleString(DateTime time)
        {
            return time.ToString("yyyy/MM/dd");
        }

        public string DateTimeToString(DateTime time)
        {
            return time.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public string DateTimeToDetailString(DateTime time)
        {
            return time.ToString("yyyy/MM/dd HH:mm:ss:ffff dddd");
        }

        public DateTime GetDateTimeBy_yyyyMMddStr(string str)
        {
            return new DateTime(GetYearByDateStr(str), GetMonthByDateStr(str), GetDayByDateStr(str));
        }

        public string TimestampToString(long endTimestamp)
        {
            return DateTimeToString(GetDateTime(endTimestamp));
        }
        #endregion

        #region Conversion
        public float Millisecond2Second(uint millisecond)
        {
            if (millisecond == 0)
            {
                return 0f;
            }
            return millisecond / 1000f;
        }

        public double GetTimestampInMilliSecond(DateTime date)
        {
            return GetTimestamp(date) * 1000;
        }

        public string GetMSMTimeUID()
        {
            int minute = GetNowMinute();
            int second = GetNowSecond();
            int millisecond = GetNowMillisecond();
            return string.Concat(minute, second, millisecond);
        }
        #endregion

        #region Calculate
        /// <summary>
        /// 计算两个日期的月份差值
        /// </summary>
        /// <returns>返回1代表newDate比oldDate大一个月 </returns>
        public int GetMonthDuration(string oldDate, string newDate)
        {
            int year = GetYearByDateStr(newDate) - GetYearByDateStr(oldDate);
            int month = GetMonthByDateStr(newDate) - GetMonthByDateStr(oldDate);
            return year * 12 + month;
        }

        public int GetMonthDuration(DateTime oldDate, DateTime newDate)
        {
            int year = newDate.Year - oldDate.Year;
            int month = newDate.Month - oldDate.Month;
            return year * 12 + month;
        }

        /// <summary>
        /// 根据日期字符串返回当前的日期中的天数
        /// </summary>
        /// <param name="dateStr">yyyymmdd</param>
        public int GetDayByDateStr(string dateStr)
        {
            int date = int.Parse(dateStr.Substring(6, 2));
            return date;
        }

        /// <summary>
        /// 根据日期字符串返回当前的日期中的月份
        /// </summary>
        /// <param name="dateStr">yyyymmdd</param>
        public int GetMonthByDateStr(string dateStr)
        {
            int date = int.Parse(dateStr.Substring(4, 2));
            return date;
        }

        /// <summary>
        /// 根据日期字符串返回当前的日期中的年
        /// </summary>
        /// <param name="dateStr">yyyymmdd</param>
        public int GetYearByDateStr(string dateStr)
        {
            int date = int.Parse(dateStr.Substring(0, 4));
            return date;
        }

        /// <summary>
        /// 取得某月的第一天
        /// </summary>
        /// <param name="datetime">要取得月份第一天的时间</param>
        public DateTime FirstDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day);
        }

        //// <summary>
        /// 取得某月的最后一天
        /// </summary>
        /// <param name="datetime">要取得月份最后一天的时间</param>
        public DateTime LastDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
        }
        #endregion
    }
}