/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;

namespace FuturePlugin
{
    public static class ProfilerFrameTiming
    {
        public static void CaptureFrameTimings()
        {
            FrameTimingManager.CaptureFrameTimings();
        }
    }
}