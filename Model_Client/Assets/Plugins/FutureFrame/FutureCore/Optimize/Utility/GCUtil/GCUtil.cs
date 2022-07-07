/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using UnityEngine.Scripting;

namespace FutureCore
{
    public static class GCUtil
    {
        static GCUtil()
        {
            GarbageCollector.GCModeChanged += (GarbageCollector.Mode mode) =>
            {
                LogUtil.Log("[GCUtil]GCModeChanged: " + mode);
            };
        }

        public static void LogGCMode()
        {
            LogUtil.Log("[GCUtil]GCMode: " + GarbageCollector.GCMode);
        }

        public static void EnableGC()
        {
            GarbageCollector.GCMode = GarbageCollector.Mode.Enabled;
            GC.Collect();
        }

        public static void DisableGC()
        {
            GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
        }

        public static bool IsIncremental()
        {
            return GarbageCollector.isIncremental;
        }

        public static ulong GetIncrementalTimeSliceNanoseconds()
        {
            return GarbageCollector.incrementalTimeSliceNanoseconds;
        }
    }
}