/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public static class VectorUtil
    {
        public static Vector3 GetVector3(float[] array)
        {
            if (array == null || array.Length == 0)
            {
                return Vector3.zero;
            }
            else if (array.Length == 3)
            {
                return new Vector3(array[0], array[1], array[2]);
            }
            else if (array.Length == 2)
            {
                return GetVector2(array);
            }
            return Vector3.zero;
        }

        public static Vector2 GetVector2(float[] array)
        {
            if (array == null || array.Length == 0)
            {
                return Vector2.zero;
            }
            else if (array.Length == 2)
            {
                return new Vector2(array[0], array[1]);
            }
            return Vector2.zero;
        }

        public static Vector3 GetSameXY1(float value)
        {
            return GetSameXYCustomZ(value, 1);
        }

        public static Vector3 GetSameXYCustomZ(float value, float z)
        {
            return new Vector3(value, value, z);
        }

        public static Vector3 GetSameXYZ(float value)
        {
            return new Vector3(value, value, value);
        }

        public static Vector2 GetSameXY(float value)
        {
            return new Vector2(value, value);
        }

        public static Vector3 World2Local(Transform tf, Vector3 worldPos)
        {
            // 世界坐标转相对坐标  
            return tf.InverseTransformPoint(worldPos);  
        }

        public static Vector3 Local2World(Transform tf, Vector3 localPos)
        {
            // 相对坐标转世界坐标
            return tf.TransformPoint(localPos);
        }
    }
}