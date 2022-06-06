using UnityEngine;

namespace Frame
{
    /// <summary>
    /// MonoBehaviour 简单单例类
    /// </summary>
    public abstract class SingletonMonoSimple<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_instance;
        public static T Instance
        {
            get
            {
                return m_instance;
            }
        }

        protected virtual void Awake()
        {
            m_instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            m_instance = null;
        }
    }
}