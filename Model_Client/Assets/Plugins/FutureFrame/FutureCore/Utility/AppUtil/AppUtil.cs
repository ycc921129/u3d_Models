/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Threading;
using UnityEngine;

namespace FutureCore
{
    public static class AppUtil
    {
        public static int MainThreadId = Thread.CurrentThread.ManagedThreadId;

        public static int GetProcessorCount()
        {
            return SystemInfo.processorCount;
        }

        public static int GetMaxLoadCount()
        {
            return GetProcessorCount() + 1;
        }

        public static int GetPreMaxLoadCount()
        {
            return 6;
        }

        public static bool IsFullLoad()
        {
            return Time.realtimeSinceStartup - Time.time > 0.05f;
        }

        public static float GetFPS()
        {
            float fps = 1.0f / Time.smoothDeltaTime;
            return fps;
        }

        public static string GetFPSInfo()
        {
            string fps = (1.0f / Time.smoothDeltaTime).ToString("0");
            return fps;
        }

        /// <summary>
        /// 设备震动
        /// 震动时长是0.5秒
        /// </summary>
        public static void Vibrate()
        {
#if !UNITY_STANDALONE
            Handheld.Vibrate();
#endif
        }

        public static bool IsReferenceEquals(object obj1, object obj2)
        {
            return ReferenceEquals(obj1, obj2);
        }

        public static bool IsMainThread()
        {
            return Thread.CurrentThread.ManagedThreadId == MainThreadId;
        }

        public static SynchronizationContext GetCurrSynchronizationContext()
        {
            SynchronizationContext context = SynchronizationContext.Current;
            return context;
        }
    }
}