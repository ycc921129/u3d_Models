/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

#if UNITY_EDITOR

using System.Diagnostics;
using System;

namespace FuturePlugin
{
    public static class PerformanceTester
    {
        public static PerformanceData Execute(Action action, int runCount = 1)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < runCount; i++)
            {
                action();
            }
            sw.Stop();
            long totalMilliseconds = sw.ElapsedMilliseconds;
            double meanMilliseconds = sw.ElapsedMilliseconds / (double)runCount;

            UnityEngine.Debug.LogFormat("[PerformanceTestTool]次数:{0} 总耗时:{1}ms 单次耗时:{2}ms", runCount, totalMilliseconds, meanMilliseconds);
            return new PerformanceData(totalMilliseconds, meanMilliseconds);
        }
    }

    public class PerformanceData
    {
        public long TotalMilliseconds { get; set; }
        public double MeanMilliseconds { get; set; }

        public PerformanceData(long totalMilliseconds, double meanMilliseconds)
        {
            TotalMilliseconds = totalMilliseconds;
            MeanMilliseconds = meanMilliseconds;
        }
    }
}

#endif