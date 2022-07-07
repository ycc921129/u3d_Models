/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;

namespace FutureCore
{
    public static class OrderIdUtil
    {
        private static int m_useCount;

        public static string GenerateTimeId(string pre = null)
        {
            if (m_useCount == 1000)
            {
                m_useCount = 0;
            }
            else
            {
                m_useCount++;
            }

            string id = m_useCount.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            if (pre != null)
            {
                return pre + id;
            }
            return id;
        }

        public static string GenerateGuid(string pre = null)
        {
            string id = Guid.NewGuid().GetHashCode().ToString();
            if (pre != null)
            {
                return pre + id;
            }
            return id;
        }
    }
}