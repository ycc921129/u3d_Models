/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.12
*/

using System;

namespace FutureCore
{
    /// <summary>
    /// 单例类
    /// </summary>
    public class Singleton<T> : IDisposable where T : class, new()
    {
        private static T m_instance;
        public static T Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new T();
                return m_instance;
            }
        }

        public virtual void Dispose()
        {
            m_instance = null;
        }
    }
}