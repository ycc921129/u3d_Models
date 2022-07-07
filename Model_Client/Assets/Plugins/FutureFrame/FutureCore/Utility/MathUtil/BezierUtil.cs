/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    /// <summary>
    /// 贝塞尔曲线工具
    /// </summary>
    public static class BezierUtil
    {
        public static Vector3 Curve(Vector3 v0, Vector3 v1, float t)
        {
            Vector3 v = Vector3.zero;
            float t1 = (1 - t);
            v = t1 * v0 + v1 * t;
            return v;
        }

        public static Vector3 Curve(Vector3 v0, Vector3 v1, Vector3 v2, float t)
        {
            Vector3 v = Vector3.zero;
            float t1 = (1 - t) * (1 - t);
            float t2 = t * (1 - t);
            float t3 = t * t;
            v = v0 * t1 + 2 * t2 * v1 + t3 * v2;
            return v;
        }

        public static Vector3 Curve(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, float t)
        {
            Vector3 v = Vector3.zero;
            float t1 = (1 - t) * (1 - t) * (1 - t);
            float t2 = (1 - t) * (1 - t) * t;
            float t3 = t * t * (1 - t);
            float t4 = t * t * t;
            v = v0 * t1 + 3 * t2 * v1 + 3 * t3 * v2 + v3 * t4;
            return v;
        }
    }
}