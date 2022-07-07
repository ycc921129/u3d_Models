/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public static class PolygonUtil
    {
        public static float Cross(Vector2 v1, Vector2 v2)
        {
            return v1.x * v2.y - v2.x * v1.y;
        }

        public static bool IsPointInRectangle(Vector2 target, Vector3[] rectCorners)
        {
            return IsPointInRectangle(target, rectCorners[0], rectCorners[1], rectCorners[2], rectCorners[3]);
        }

        public static bool IsPointInRectangle(Vector2 target, Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            Vector2 ab = a - b;
            Vector2 at = a - target;
            Vector2 cd = c - d;
            Vector2 ct = c - target;

            Vector2 da = d - a;
            Vector2 dp = d - target;
            Vector2 bc = b - c;
            Vector2 bt = b - target;

            bool isBetweenAB_CD = Cross(ab, at) * Cross(cd, ct) > 0;
            bool isBetweenDA_BC = Cross(da, dp) * Cross(bc, bt) > 0;
            bool res = isBetweenAB_CD && isBetweenDA_BC;
            return res;
        }
    }
}