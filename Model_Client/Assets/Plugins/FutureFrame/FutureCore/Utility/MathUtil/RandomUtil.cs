/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;

namespace FutureCore
{
    public static class RandomUtil
    {
        private static Random random;

        /// <summary>
        /// 设置随机种子
        /// </summary>
        public static Random SetRandomSeed(int seed)
        {
            random = new System.Random(seed);
            return random;
        }

        /// <summary>
        /// 随机整型
        /// </summary>
        public static int RandomRange(int min, int max)
        {
            return random.Next(min, max);
        }

        /// <summary>
        /// 随机浮点数
        /// </summary>
        public static float RandomRange(float min, float max)
        {
            var r = random.NextDouble();
            return (float)(r * (max - min) + min);
        }
    }
}