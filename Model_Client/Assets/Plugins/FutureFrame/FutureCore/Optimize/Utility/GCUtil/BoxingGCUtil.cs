/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Runtime.InteropServices;

namespace FutureCore
{
    public static unsafe class BoxingGCUtil
    {
        private static object s_Int = 0;
        private static int* s_IntPtr = null;
        private static GCHandle s_IntHandle;

        private static object s_UInt = 0;
        private static uint* s_UIntPtr = null;
        private static GCHandle s_UIntHandle;

        private static object s_Float = 0;
        private static float* s_FloatPtr = null;
        private static GCHandle s_FloatHandle;

        private static object s_Double = 0;
        private static double* s_DoublePtr = null;
        private static GCHandle s_DoubleHandle;

        private static void Init()
        {
            s_IntHandle = GCHandle.Alloc(s_Int, GCHandleType.Pinned);
            s_IntPtr = (int*)s_IntHandle.AddrOfPinnedObject().ToPointer();

            s_UIntHandle = GCHandle.Alloc(s_UInt, GCHandleType.Pinned);
            s_UIntPtr = (uint*)s_UIntHandle.AddrOfPinnedObject().ToPointer();

            s_FloatHandle = GCHandle.Alloc(s_Float, GCHandleType.Pinned);
            s_FloatPtr = (float*)s_FloatHandle.AddrOfPinnedObject().ToPointer();

            s_DoubleHandle = GCHandle.Alloc(s_Double, GCHandleType.Pinned);
            s_DoublePtr = (double*)s_DoubleHandle.AddrOfPinnedObject().ToPointer();
        }

        public static object GetInt(int value)
        {
            *s_IntPtr = value;
            return s_Int;
        }

        public static object GetUInt(uint value)
        {
            *s_UIntPtr = value;
            return s_UInt;
        }

        public static object GetFloat(float value)
        {
            *s_FloatPtr = value;
            return s_Float;
        }

        public static object GetDouble(double value)
        {
            *s_DoublePtr = value;
            return s_Double;
        }
    }
}