/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace FutureCore
{
    public class GObjectsPool
    {
        private Transform m_releaseRoot;
        private Transform m_getRoot;
        private DynamicAssetLoader m_loader;
        private Action<GameObject> m_onNew;
        private Action<GameObject> m_onGet;
        private Action<GameObject> m_onRelease;

        private Dictionary<string, GObjectPool> m_pools = new Dictionary<string, GObjectPool>();

        public int CountAll
        {
            get
            {
                int countAll = 0;
                foreach (GObjectPool poolItem in m_pools.Values)
                {
                    countAll += poolItem.CountAll;
                }
                return countAll;
            }
        }
        public int CountInactive
        {
            get
            {
                int countInactive = 0;
                foreach (GObjectPool poolItem in m_pools.Values)
                {
                    countInactive += poolItem.CountInactive;
                }
                return countInactive;
            }
        }
        public int CountActive
        {
            get
            {
                int countActive = 0;
                foreach (GObjectPool poolItem in m_pools.Values)
                {
                    countActive += poolItem.CountActive;
                }
                return countActive;
            }
        }

        public void InitRoot(Transform releaseRoot = null, Transform getRoot = null)
        {
            m_releaseRoot = releaseRoot;
            m_getRoot = getRoot;
        }

        public void InitLoader(DynamicAssetLoader loader)
        {
            m_loader = loader;
        }

        public void InitCallBack(Action<GameObject> onNew, Action<GameObject> onGet, Action<GameObject> onRelease)
        {
            m_onNew = onNew;
            m_onGet = onGet;
            m_onRelease = onRelease;
        }

        /// <summary>
        /// 生产对象
        /// </summary>
        public GameObject Get(string prefabPath)
        {
            if (!m_pools.ContainsKey(prefabPath))
            {
                RegisterNew(prefabPath);
            }
            GObjectPool pool = m_pools[prefabPath];
            return pool.Get();
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public void Release(string prefabPath, GameObject obj)
        {
            GObjectPool pool = null;
            if (m_pools.TryGetValue(prefabPath, out pool))
            {
                pool.Release(obj);
            }
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public void Release(GameObject obj)
        {
            GObjectPool pool = null;
            foreach (GObjectPool poolItem in m_pools.Values)
            {
                if (poolItem.Contains(obj))
                {
                    pool = poolItem;
                    break;
                }
            }
            pool.Release(obj);
        }

        /// <summary>
        /// 回收所有对象
        /// </summary>
        public void ReleaseAll()
        {
            foreach (GObjectPool poolItem in m_pools.Values)
            {
                poolItem.ReleaseAll();
            }
        }

        /// <summary>
        /// 创建新对象池
        /// </summary>
        private void RegisterNew(string prefabPath)
        {
            GObjectPool pool = new GObjectPool();
            pool.Init(m_loader, prefabPath, m_releaseRoot, m_getRoot);
            pool.InitCallBack(m_onNew, m_onGet, m_onRelease);
            m_pools.Add(prefabPath, pool);
        }

        /// <summary>
        /// 清除指定子对象池
        /// </summary>
        public void ClearPool(string prefabPath)
        {
            GObjectPool pool = null;
            if (m_pools.TryGetValue(prefabPath, out pool))
            {
                pool.Clear();
                m_pools.Remove(prefabPath);
            }
        }

        /// <summary>
        /// 清除所有对象池
        /// </summary>
        public void Clear()
        {
            foreach (GObjectPool pool in m_pools.Values)
            {
                pool.Clear();
            }
            m_pools.Clear();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            Clear();
            m_pools = null;
        }
    }
}