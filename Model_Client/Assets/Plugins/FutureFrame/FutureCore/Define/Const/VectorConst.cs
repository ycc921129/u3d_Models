/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public static class VectorConst
    {
        public static Vector3 Zero = Vector3.zero;
        public static Vector3 One = Vector3.one;
        public static Vector3 PPUOne = One * AppConst.PixelsPerUnit;
        public static Vector3 Half = new Vector3(0.5f, 0.5f, 0.5f);
        public static Vector3 XMirror = new Vector3(-1f, 1f, 1f);
    }
}