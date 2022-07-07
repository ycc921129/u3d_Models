/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public class ConstantLerper
    {
        private float currProgress;
        private float currValue;

        public Vector3 Lerp(Vector3 start, Vector3 end, float speed)
        {
            currProgress += 1f / speed * Time.deltaTime;
            return Vector3.Lerp(start, end, currProgress);
        }

        public Vector3 LerpOverflow(Vector3 start, Vector3 end, float speed)
        {
            currProgress += 1f / speed * Time.deltaTime;
            return start + (end - start) * currProgress;
        }

        public float NolinearLerpMax(float curr, float max, float speed)
        {
            currValue = curr;
            currValue += (max - currValue) * speed * Time.deltaTime;
            return currValue;
        }

        public float NolinearLerpMin(float curr, float min, float speed)
        {
            currValue = curr;
            currValue -= (currValue - min) * speed * Time.deltaTime;
            return currValue;
        }
    }
}