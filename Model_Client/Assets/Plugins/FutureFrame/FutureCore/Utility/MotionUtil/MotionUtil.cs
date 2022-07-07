/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public static class MotionUtil
    {
        /// <summary>
        /// 移动到目标
        /// </summary>
        public static Vector3 Vector3MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            float toVector_x = target.x - current.x;
            float toVector_y = target.y - current.y;
            float toVector_z = target.z - current.z;

            float sqdist = toVector_x * toVector_x + toVector_y * toVector_y + toVector_z * toVector_z;

            if (sqdist == 0 || (maxDistanceDelta >= 0 && sqdist <= maxDistanceDelta * maxDistanceDelta))
                return target;
            var dist = (float)System.Math.Sqrt(sqdist);

            return new Vector3(current.x + toVector_x / dist * maxDistanceDelta,
                current.y + toVector_y / dist * maxDistanceDelta,
                current.z + toVector_z / dist * maxDistanceDelta);
        }

        /// <summary>
        /// 是否到达这个点
        /// </summary>
        public static bool IsArriveDistance(Vector3 curPos, Vector3 tarPos)
        {
            // 因为向量的大小由勾股定理得出，所以有开方操作。
            // 只是比较向量之间的大小，建议使用Vector3.sqrMagnitude进行比较，提高效率和节约性能。
            float sqrLenght = (tarPos - curPos).sqrMagnitude;
            if (sqrLenght == 0f)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否到达这个距离
        /// </summary>
        public static bool IsArriveDistance(Vector3 curPos, Vector3 tarPos, float dis)
        {
            // 因为向量的大小由勾股定理得出，所以有开方操作。
            // 只是比较向量之间的大小，建议使用Vector3.sqrMagnitude进行比较，提高效率和节约性能。
            float sqrLenght = (tarPos - curPos).sqrMagnitude;
            if (sqrLenght <= (dis * dis))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 角度插值弧线
        /// </summary>
        public static void EulerArcs(Transform start, Transform target, float speed, float rotationAngle)
        {
            // 方向
            Vector3 direction = target.position - start.position;
            // 转角度
            float angle = 360 - Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            start.eulerAngles = new Vector3(0, 0, angle);
            // 转方向
            start.rotation = start.rotation * Quaternion.Euler(0, 0, rotationAngle);
            start.Translate(Vector3.up * speed * Time.deltaTime);
        }

        /// <summary>
        /// 球形插值弧线
        /// </summary>
        public static void SlerpArcs(Transform start, Transform target, float speed)
        {
            start.up = Vector3.Slerp(start.up, target.position - start.position,
                0.5f / Vector3.Distance(start.position, target.position));
            start.position += start.up * speed * Time.deltaTime;
        }
    }
}