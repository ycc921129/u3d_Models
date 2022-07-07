/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace FutureCore
{
    public static class MathUtil
    {
        #region 常量
        public static Vector3 AxisX = new Vector3(1, 0, 0);
        public static Vector3 AxisY = new Vector3(0, 1, 0);
        public static Vector3 AxisZ = new Vector3(0, 0, 1);
        public static Vector3 XYZ1 = Vector3.one;

        public static float PI = 3.14159265358979323846264338f;
        public static float ONE_DIV_PI = 1.0f / Mathf.PI;

        public static float COS_15 = Mathf.Cos(Mathf.Deg2Rad * 15.0f);
        public static float COS_35 = Mathf.Cos(Mathf.Deg2Rad * 35.0f);
        public static float COS_45 = Mathf.Cos(Mathf.Deg2Rad * 45.0f);
        public static float COS_75 = Mathf.Cos(Mathf.Deg2Rad * 75.0f);
        public static float COS_60 = Mathf.Cos(Mathf.Deg2Rad * 60.0f);
        public static float COS_30 = Mathf.Cos(Mathf.Deg2Rad * 30.0f);
        public static float COS_20 = Mathf.Cos(Mathf.Deg2Rad * 20.0f);

        public static Vector2 AxisX2D = new Vector2(1, 0);
        public static Vector2 AxisY2D = new Vector2(0, 1);

        public static float EPSILON = 0.001f;
        #endregion

        #region 时间
        public static float GetTime(Vector3 startPos, Vector3 endPos, float speed)
        {
            float dis = Vector3.Distance(startPos, endPos);
            return dis / speed;
        }

        public static float GetTime(float length1, float length2, float speed)
        {
            float dis = 0f;
            if (length1 > length2)
            {
                dis = length1 - length2;
            }
            else
            {
                dis = length2 - length1;
            }
            return dis / speed;
        }

        public static float GetTime(float distance, float speed)
        {
            return distance / speed;
        }
        #endregion

        #region 数值
        /// <summary>
        /// 中国式四舍五入
        /// </summary>
        /// <param name="value">参数value为要处理的浮点数</param>
        /// <param name="digit">参数digit为要保留的小数点位数</param>
        public static float RoundChina(float value, int digit)
        {
            float vt = Mathf.Pow(10, digit);
            //1.乘以倍数 + 0.5
            float vx = value * vt + 0.5f;
            //2.向下取整
            float temp = Mathf.Floor(vx);
            //3.再除以倍数
            return temp / vt;
        }

        /// <summary>
        /// 中国式四舍五入
        /// </summary>
        public static int RoundChinaToInt(float value)
        {
            return (int)RoundChina(value, 0);
        }

        public static int RoundToInt(float value)
        {
            return Mathf.RoundToInt(value);
        }

        public static int CeilToInt(float value)
        {
            return Mathf.CeilToInt(value);
        }

        public static int FloorToInt(float number)
        {
            return Mathf.FloorToInt(number);
        }

        public static int ClampMinMax(int value, int min, int max)
        {
            return Mathf.Clamp(value, min, max);
        }

        public static bool Approximately(float a, float b)
        {
            return Mathf.Approximately(a, b);
        }

        public static bool IsEqualFloat(float a, float b)
        {
            return (Mathf.Abs(a - b) < 0.001f);
        }

        public static bool IsEqualFloatRaw(float a, float b)
        {
            return (Mathf.Abs(a - b) < 0.05f);
        }

        public static string FloatToF1String(float value)
        {
            return value.ToString("f1");
        }

        /// <summary>
        /// 是否是奇数
        /// </summary>
        public static bool IsOddNumber(int num)
        {
            return (num & 1) == 1;
        }

        /// <summary>
        /// 是否是偶数
        /// </summary>
        public static bool IsEvenNumber(int num)
        {
            return (num & 1) == 0;
        }
        #endregion

        #region 随机
        /// <summary>
        /// 随机柏林噪声
        /// </summary>
        public static float RandomPerlinNoise(float x, float y)
        {
            return Mathf.PerlinNoise(x, y);
        }

        /// <summary>
        /// 随机枚举
        /// </summary>
        public static E RandomEnum<E>()
        {
            E[] enumValue = System.Enum.GetValues(typeof(E)) as E[];
            E color = enumValue[Random.Range(0, enumValue.Length)];
            return color;
        }

        public static int RandomRange(int[] range)
        {
            if (range == null)
            {
                return 0;
            }
            if (range.Length == 2)
            {
                return RandomRange(range[0], range[1]);
            }
            else
            {
                return range[0];
            }
        }

        public static int RandomRangeIncludeEnd(int[] range)
        {
            if (range == null)
            {
                return 0;
            }
            if (range.Length == 2)
            {
                return RandomRange(range[0], range[1] + 1);
            }
            else
            {
                return range[0];
            }
        }

        public static float RandomRange(float[] range)
        {
            if (range == null)
            {
                return 0;
            }
            if (range.Length == 2)
            {
                return RandomRange(range[0], range[1]);
            }
            else
            {
                return range[0];
            }
        }

        public static float RandomRangeIncludeEnd(float[] range)
        {
            if (range == null)
            {
                return 0;
            }
            if (range.Length == 2)
            {
                return RandomRange(range[0], range[1]);
            }
            else
            {
                return range[0];
            }
        }

        public static int RandomRange(int begin, int end)
        {
            return Random.Range(begin, end);
        }

        public static int RandomRangeIncludeEnd(int begin, int end)
        {
            return Random.Range(begin, end + 1);
        }

        public static float RandomRange(float begin, float end)
        {
            return Random.Range(begin, end);
        }

        public static float RandomRangeIncludeEnd(float begin, float end)
        {
            return Random.Range(begin, end);
        }

        public static float RandomZeroOne()
        {
            return RandomRange(0f, 1f);
        }

        public static bool RandomBool()
        {
            return Random.value > 0.5f;
        }

        public static Vector2 RandomVector2()
        {
            return Random.insideUnitCircle * 1f;
        }

        public static Vector3 RandomVector3()
        {
            return Random.insideUnitSphere * 1f;
        }

        /// <summary>
        /// 随机数组
        /// </summary>
        public static int RandomArray(int[] array)
        {
            return array[RandomIndex(array.Length)];
        }

        /// <summary>
        /// 随机数组
        /// </summary>
        public static float RandomArray(float[] array)
        {
            return array[RandomIndex(array.Length)];
        }

        /// <summary>
        /// 随机数组
        /// </summary>
        public static T RandomArray<T>(T[] array)
        {
            return array[RandomIndex(array.Length)];
        }

        /// <summary>
        /// 随机列表下标
        /// </summary>
        public static int RandomListIndex(List<int> list)
        {
            return RandomIndex(list.Count);
        }

        /// <summary>
        /// 随机列表
        /// </summary>
        public static int RandomList(List<int> list)
        {
            return list[RandomIndex(list.Count)];
        }

        /// <summary>
        /// 随机列表
        /// </summary>
        public static float RandomList(List<float> list)
        {
            return list[RandomIndex(list.Count)];
        }

        /// <summary>
        /// 随机列表
        /// </summary>
        public static T RandomList<T>(List<T> list)
        {
            return list[RandomIndex(list.Count)];
        }

        /// <summary>
        /// 随机下标
        /// </summary>
        public static int RandomIndex(int Length)
        {
            if (Length == 0)
            {
                return -1;
            }
            else if(Length == 1)
            {
                return 0;
            }
            return Random.Range(0, Length);
        }

        /// <summary>
        /// 百分比随机
        /// </summary>
        public static bool RandomPercent(float probability)
        {
            return probability >= Random.value;
        }

        /// <summary>
        /// 百分比随机
        /// </summary>
        public static bool RandomPercent(int probability)
        {
            int random = Random.Range(0, 100);
            return probability >= random;
        }

        /// <summary>
        /// 数组权重概率随机
        /// </summary>
        public static int RandomWeight(List<int> weightArray)
        {
            float totalWeight = 0;
            for (int i = 0; i < weightArray.Count; i++)
            {
                totalWeight += weightArray[i];
            }

            float randomWeight = Random.Range(0, totalWeight);
            if (randomWeight >= totalWeight)
            {
                return weightArray.Count - 1;
            }

            float currWeight = 0;
            for (int i = 0; i < weightArray.Count; i++)
            {
                currWeight += weightArray[i];
                if (randomWeight < currWeight)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 数组权重概率随机
        /// </summary>
        public static int RandomWeight(List<float> weightArray)
        {
            float totalWeight = 0;
            for (int i = 0; i < weightArray.Count; i++)
            {
                totalWeight += weightArray[i];
            }

            float randomWeight = Random.Range(0, totalWeight);
            if (randomWeight >= totalWeight)
            {
                return weightArray.Count - 1;
            }

            float currWeight = 0;
            for (int i = 0; i < weightArray.Count; i++)
            {
                currWeight += weightArray[i];
                if (randomWeight < currWeight)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 数组权重概率随机
        /// </summary>
        public static int RandomWeight(int[] weightArray)
        {
            int totalWeight = 0;
            for (int i = 0; i < weightArray.Length; i++)
            {
                totalWeight += weightArray[i];
            }

            int randomWeight = Random.Range(0, totalWeight);
            if (randomWeight >= totalWeight)
            {
                return weightArray.Length - 1;
            }

            int currWeight = 0;
            for (int i = 0; i < weightArray.Length; i++)
            {
                currWeight += weightArray[i];
                if (randomWeight < currWeight)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 数组权重概率随机
        /// </summary>
        public static int RandomWeight(float[] weightArray)
        {
            float totalWeight = 0;
            for (int i = 0; i < weightArray.Length; i++)
            {
                totalWeight += weightArray[i];
            }

            float randomWeight = Random.Range(0, totalWeight);
            if (randomWeight >= totalWeight)
            {
                return weightArray.Length - 1;
            }

            float currWeight = 0;
            for (int i = 0; i < weightArray.Length; i++)
            {
                currWeight += weightArray[i];
                if (randomWeight < currWeight)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 权重概率随机
        /// </summary>
        public static int RandomWeight(List<KeyValuePair<int, float>> weightList)
        {
            return RandomWeight<int>(weightList);
        }

        /// <summary>
        /// 权重概率随机
        /// </summary>
        public static int RandomWeight(List<KeyValuePair<int, int>> weightList)
        {
            return RandomWeight<int>(weightList);
        }

        /// <summary>
        /// 权重概率随机
        /// </summary>
        public static T RandomWeight<T>(List<KeyValuePair<T, float>> weightList)
        {
            float totalWeight = 0;
            for (int i = 0; i < weightList.Count; i++)
            {
                totalWeight += weightList[i].Value;
            }

            float randomWeight = Random.Range(0, totalWeight);
            if (randomWeight >= totalWeight)
            {
                return weightList[weightList.Count - 1].Key;
            }

            float currWeight = 0;
            for (int i = 0; i < weightList.Count; i++)
            {
                currWeight += weightList[i].Value;
                if (randomWeight < currWeight)
                {
                    return weightList[i].Key;
                }
            }
            return default(T);
        }

        /// <summary>
        /// 权重概率随机
        /// </summary>
        public static T RandomWeight<T>(List<KeyValuePair<T, int>> weightList)
        {
            int totalWeight = 0;
            for (int i = 0; i < weightList.Count; i++)
            {
                totalWeight += weightList[i].Value;
            }

            int randomWeight = Random.Range(0, totalWeight);
            if (randomWeight >= totalWeight)
            {
                return weightList[weightList.Count - 1].Key;
            }

            int currWeight = 0;
            for (int i = 0; i < weightList.Count; i++)
            {
                currWeight += weightList[i].Value;
                if (randomWeight < currWeight)
                {
                    return weightList[i].Key;
                }
            }
            return default(T);
        }
        #endregion

        #region 集合随机
        public static void RandomSortArray<T>(T[] array)
        {
            System.Random r = new System.Random();
            for (int i = 0; i < array.Length; i++)
            {
                int index = r.Next(array.Length);
                T temp = array[i];
                array[i] = array[index];
                array[index] = temp;
            }
        }

        public static void RandomSortList<T>(List<T> list)
        {
            System.Random r = new System.Random();
            for (int i = 0; i < list.Count; i++)
            {
                int index = r.Next(list.Count);
                T temp = list[i];
                list[i] = list[index];
                list[index] = temp;
            }
        }

        /// <summary>
        /// 集合等概率随机
        /// </summary>
        public static void EqualProbabilityRandomArray(int[] array)
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                int temp = array[randomIndex];
                array[randomIndex] = array[i];
                array[i] = temp;
            }
        }

        /// <summary>
        /// 集合等概率随机
        /// </summary>
        public static void EqualProbabilityRandomArray(float[] array)
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                float temp = array[randomIndex];
                array[randomIndex] = array[i];
                array[i] = temp;
            }
        }

        /// <summary>
        /// 集合等概率随机
        /// </summary>
        public static void EqualProbabilityRandomList(List<int> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                int temp = list[randomIndex];
                list[randomIndex] = list[i];
                list[i] = temp;
            }
        }

        /// <summary>
        /// 集合等概率随机
        /// </summary>
        public static void EqualProbabilityRandomList(List<float> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                float temp = list[randomIndex];
                list[randomIndex] = list[i];
                list[i] = temp;
            }
        }
        #endregion

        #region 圆形
        /// <summary>
        /// 角度转弧度
        /// </summary>
        public static float AngleToRadian(float angle)
        {
            return angle * PI / 180;
        }

        /// <summary>
        /// 弧度转角度
        /// </summary>
        public static float RadianToAngle(float radian)
        {
            return radian * 180 / PI;
        }

        /// <summary>
        /// 贝塞尔曲线
        /// </summary>
        /// <param name="from">第一个点</param>
        /// <param name="to">第二个点</param>
        /// <param name="center">中间点</param>
        /// <param name="progress">进度</param>
        /// <returns>位置</returns>
        public static Vector3 BezierCurve(Vector3 from, Vector3 to, Vector3 center, float progress)
        {
            return Mathf.Pow((1 - progress), 2) * from + 2 * progress * (1 - progress) * center + progress * progress * to;
        }
        #endregion

        #region 向量
        /// <summary>
        /// Moves a point /current/ in a straight line towards a /target/ point.
        /// </summary>
        public static Vector3 Vector3MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            // avoid vector ops because current scripting backends are terrible at inlining
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
        /// Linearly interpolates between two vectors.
        /// </summary>
        public static Vector3 Vector3Lerp(Vector3 a, Vector3 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new Vector3(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t
            );
        }

        /// <summary>
        /// 移动到达这个距离
        /// </summary>
        public static bool MoveArriveDistance(Transform tf, Vector3 tarPos, float dis, float speed)
        {
            float step = speed * Time.deltaTime;
            tf.position = Vector3.MoveTowards(tf.position, tarPos, step);

            float sqrLenght = (tarPos - tf.position).sqrMagnitude;
            if (sqrLenght <= (dis * dis))
            {
                return true;
            }
            return false;
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
        /// 计算坐标间的距离
        /// </summary>
        public static float Distance(Vector2 v1, Vector2 v2)
        {
            return Vector2.Distance(v1, v2);
        }

        /// <summary>
        /// 计算两个三维坐标相差的距离
        /// </summary>
        public static float DistancePow(Vector3 a, Vector3 b)
        {
            return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z);
        }

        /// <summary>
        /// 计算两个二维坐标相差的距离
        /// </summary>
        public static float DistancePow(Vector2 a, Vector2 b)
        {
            return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y);
        }

        /// <summary>
        /// 获取两个点间的夹角
        /// </summary>
        public static float GetAngle(Vector3 form, Vector3 to)
        {
            Vector3 nVector = Vector3.zero;
            nVector.x = to.x;
            nVector.y = form.y;
            float a = to.y - nVector.y;
            float b = nVector.x - form.x;
            float tan = a / b;
            return Mathf.Atan(tan) * 180.0f * ONE_DIV_PI;
        }

        /// <summary>
        /// 计算角度
        /// </summary>
        public static float GetAngleByDot(Vector3 v1, Vector3 v2)
        {
            float dot = Vector3.Dot(v1, v2);
            float mv1 = Mathf.Sqrt(v1.x * v1.x + v1.y * v1.y);
            float mv2 = Mathf.Sqrt(v2.x * v2.x + v2.y * v2.y);

            float angle = Mathf.Acos(dot / mv1 * mv2) * Mathf.Rad2Deg;
            if (v2.x > v1.x) angle *= -1;
            return angle;
        }

        /// <summary>
        /// 大概的方向
        /// </summary>
        public static Vector3 ApproximateDir(Vector3 dir)
        {
            float dotX = Vector3.Dot(dir, AxisX);
            float dotZ = Vector3.Dot(dir, AxisZ);
            if (Mathf.Abs(dotX) > Mathf.Abs(dotZ))
            {
                return dotX > 0 ? AxisX : -AxisX;
            }
            else
            {
                return dotZ > 0 ? AxisZ : -AxisZ;
            }
        }

        /// <summary>
        /// normalize 并且返回长度
        /// </summary>
        public static float Normalize(ref Vector3 vec)
        {
            float length = Mathf.Sqrt((vec.x * vec.x) + (vec.y * vec.y) + (vec.z * vec.z));
            if (length > 0)
            {
                float oneDivLength = 1.0f / length;
                vec.x = vec.x * oneDivLength;
                vec.y = vec.y * oneDivLength;
                vec.z = vec.z * oneDivLength;
            }
            return length;
        }

        /// <summary>
        /// 尝试到达那个点
        /// </summary>
        public static Vector3 TryToMoveToPosWithSpeed(Vector3 dest, Vector3 cur, float speed, float time)
        {
            Vector3 dir = dest - cur;
            float dis = Normalize(ref dir);
            if (speed * time < dis)
            {
                return cur + dir * speed * time;
            }
            else
            {
                return dest;
            }
        }

        /// <summary>
        /// 移动人物制定距离相差多少的值
        /// </summary>
        /// <param name="dest">目标点</param>
        /// <param name="cur">当前坐标</param>
        /// <param name="speed"></param>
        /// <param name="time">速度</param>
        public static Vector3 OffsetToMoveToPosWithSpeed(Vector3 dest, Vector3 cur, float speed, float time)
        {
            Vector3 dir = dest - cur;
            Vector3 maxOffset = dir;
            float dis = Normalize(ref dir);
            if (speed * time < dis)
            {
                return dir * speed * time;
            }
            else
            {
                return maxOffset;
            }
        }

        /// <summary>
        /// 获取两点之间距离一定百分比的一个点
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="distance">起始点到目标点距离百分比</param>
        public static Vector3 GetBetweenPercentPoint(Vector3 start, Vector3 end, float percent)
        {
            Vector3 normal = (end - start).normalized;
            float distance = Vector3.Distance(start, end);
            return normal * (distance * percent) + start;
        }

        /// <summary>
        /// 获取两点之间一定距离的点
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="distance">距离</param>
        public static Vector3 GetBetweenDistancePoint(Vector3 start, Vector3 end, float distance)
        {
            Vector3 normal = (end - start).normalized;
            return normal * distance + start;
        }

        /// <summary>
        /// 获取旋转后的位置
        /// </summary>
        /// <param name="centerPosition">中心点</param>
        /// <param name="targetPosition">目标点</param>
        /// <param name="angele">逆时针旋转角度使用正角度，顺时针使用负角度。</param>
        /// <returns>旋转后的位置</returns>
        public static Vector3 GetRotatePosition(Vector3 centerPosition, Vector3 targetPosition, float angele)
        {
            float endX = (targetPosition.x - centerPosition.x) * Mathf.Cos(angele * Mathf.Deg2Rad) -
                         (targetPosition.y - centerPosition.y) * Mathf.Sin(angele * Mathf.Deg2Rad) + centerPosition.x;
            float endY = (targetPosition.y - centerPosition.y) * Mathf.Cos(angele * Mathf.Deg2Rad) +
                         (targetPosition.x - targetPosition.x) * Mathf.Sin(angele * Mathf.Deg2Rad) + centerPosition.y;
            return new Vector3(endX, endY, 0);
        }

        /// <summary>
        /// 获取旋转后的位置
        /// </summary>
        /// <param name="centerPosition">中心点</param>
        /// <param name="targetPosition">目标点</param>
        /// <param name="angele">逆时针旋转角度使用正角度，顺时针使用负角度。</param>
        /// <returns>旋转后的位置</returns>
        public static Vector2 GetRotatePosition(Vector2 centerPosition, Vector2 targetPosition, float angele)
        {
            float endX = (targetPosition.x - centerPosition.x) * Mathf.Cos(angele * Mathf.Deg2Rad) -
                         (targetPosition.y - centerPosition.y) * Mathf.Sin(angele * Mathf.Deg2Rad) + centerPosition.x;
            float endY = (targetPosition.y - centerPosition.y) * Mathf.Cos(angele * Mathf.Deg2Rad) +
                         (targetPosition.x - targetPosition.x) * Mathf.Sin(angele * Mathf.Deg2Rad) + centerPosition.y;
            return new Vector2(endX, endY);
        }
        #endregion

        #region 数学插值
        /// <summary>
        /// Loops the value t, so that it is never larger than length and never smaller than 0.
        /// </summary>
        public static float Repeat(float t, float length)
        {
            return Clamp(t - Mathf.Floor(t / length) * length, 0.0f, length);
        }

        /// <summary>
        /// PingPongs the value t, so that it is never larger than length and never smaller than 0.
        /// </summary>
        public static float PingPong(float t, float length)
        {
            t = Repeat(t, length * 2F);
            return length - Mathf.Abs(t - length);
        }

        /// <summary>
        /// Calculates the ::ref::Lerp parameter between of two values.
        /// </summary>
        public static float InverseLerp(float a, float b, float value)
        {
            if (a != b)
                return Clamp01((value - a) / (b - a));
            else
                return 0.0f;
        }

        /// <summary>
        /// Calculates the shortest difference between two given angles.
        /// </summary>
        public static float DeltaAngle(float current, float target)
        {
            float delta = Mathf.Repeat((target - current), 360.0F);
            if (delta > 180.0F)
                delta -= 360.0F;
            return delta;
        }

        /// <summary>
        /// Clamps a value between a minimum float and maximum float value.
        /// </summary>
        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        /// <summary>
        /// Clamps value between min and max and returns value.
        /// Set the position of the transform to be that of the time
        /// but never less than 1 or more than 3
        /// </summary>
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        /// <summary>
        /// Clamps value between 0 and 1 and returns value
        /// </summary>
        public static float Clamp01(float value)
        {
            if (value < 0F)
                return 0F;
            else if (value > 1F)
                return 1F;
            else
                return value;
        }

        /// <summary>
        /// Interpolates between /a/ and /b/ by /t/. /t/ is clamped between 0 and 1.
        /// </summary>
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * Clamp01(t);
        }

        /// <summary>
        /// Interpolates between /a/ and /b/ by /t/ without clamping the interpolant.
        /// </summary>
        public static float LerpUnclamped(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        /// <summary>
        /// Same as ::ref::Lerp but makes sure the values interpolate correctly when they wrap around 360 degrees.
        /// </summary>
        public static float LerpAngle(float a, float b, float t)
        {
            float delta = Repeat((b - a), 360);
            if (delta > 180)
                delta -= 360;
            return a + delta * Clamp01(t);
        }

        /// <summary>
        /// Moves a value /current/ towards /target/.
        /// </summary>
        public static float MoveTowards(float current, float target, float maxDelta)
        {
            if (Mathf.Abs(target - current) <= maxDelta)
                return target;
            return current + Mathf.Sign(target - current) * maxDelta;
        }

        /// <summary>
        /// Same as ::ref::MoveTowards but makes sure the values interpolate correctly when they wrap around 360 degrees.
        /// </summary>
        public static float MoveTowardsAngle(float current, float target, float maxDelta)
        {
            float deltaAngle = DeltaAngle(current, target);
            if (-maxDelta < deltaAngle && deltaAngle < maxDelta)
                return target;
            target = current + deltaAngle;
            return MoveTowards(current, target, maxDelta);
        }

        /// <summary>
        /// Interpolates between /min/ and /max/ with smoothing at the limits.
        /// </summary>
        public static float SmoothStep(float from, float to, float t)
        {
            t = Mathf.Clamp01(t);
            t = -2.0F * t * t * t + 3.0F * t * t;
            return to * t + from * (1F - t);
        }
        #endregion

        #region 运算符
        /// <summary>
        /// 运算符计算
        /// </summary>
        public static string DataTableCompute(string str)
        {
            DataTable table = new DataTable();
            string value = table.Compute(str, null).ToString();
            return value;
        }
        #endregion
    }
}