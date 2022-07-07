/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

#define UseProfilerSampler

using System;
using System.Diagnostics;
using Unity.Profiling;
using UnityEngine.Profiling;

namespace FuturePlugin
{
    public static class ProfilerSampler
    {
        public static bool IsUseProfilerSampler = true;

        public static void Atuo(string name, Action func)
        {
            using (new ProfilerMarker(name).Auto())
            {
                func();
            }
        }

        private static CustomSampler GetCustom(string name)
        {
            if (!IsUseProfilerSampler) return null;

            CustomSampler sampler = CustomSampler.Create(name);
            return sampler;
        }

        public static CustomSampler BeginCustom(string name, UnityEngine.Object obj = null)
        {
            if (!IsUseProfilerSampler) return null;

            CustomSampler sampler = GetCustom(name);
            sampler.Begin(obj);
            return sampler;
        }

        public static void EndCustom(CustomSampler sampler)
        {
            if (!IsUseProfilerSampler) return;
            if (sampler == null) return;

            sampler.End();
        }

        [Conditional("UseProfilerSampler")]
        public static void BeginSample(string name, UnityEngine.Object obj = null)
        {
            if (!IsUseProfilerSampler) return;

            Profiler.BeginSample(name, obj);
        }

        [Conditional("UseProfilerSampler")]
        public static void EndSample()
        {
            if (!IsUseProfilerSampler) return;

            Profiler.EndSample();
        }
    }
}