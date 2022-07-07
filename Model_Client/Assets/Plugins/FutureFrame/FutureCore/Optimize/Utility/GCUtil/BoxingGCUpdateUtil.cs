/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Runtime.InteropServices;
using UnityEngine;

namespace FutureCore
{
    public static unsafe class BoxingGCUpdateUtil
    {
        private static readonly object[] s_UpdateParam = new object[1];
        private static readonly object s_Float = 0.0f;
        private static float* s_FloatPtr = null;
        private static GCHandle s_FloatHandle;

        private static void Init()
        {
            s_FloatHandle = GCHandle.Alloc(s_Float, GCHandleType.Pinned);
            s_FloatPtr = (float*)s_FloatHandle.AddrOfPinnedObject().ToPointer();
            s_UpdateParam[0] = s_Float;
        }

        private static object[] GetDeltaTime()
        {
            *s_FloatPtr = Time.deltaTime;
            return s_UpdateParam;
        }
    }
}