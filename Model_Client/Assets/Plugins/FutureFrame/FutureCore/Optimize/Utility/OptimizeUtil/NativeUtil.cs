/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using Unity.Collections;
using UnityEngine;

namespace FutureCore
{
    public static class NativeUtil
    {
        public static void DisposeVector3NativeArray(NativeArray<Vector3> vector3s)
        {
            vector3s.Dispose();
        }
    }
}