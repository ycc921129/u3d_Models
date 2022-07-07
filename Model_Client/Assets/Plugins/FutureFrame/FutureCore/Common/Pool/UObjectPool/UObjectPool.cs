/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace FutureCore
{
    public class UObjectPool<T> where T : ReusbleUObject, new()
    {
        private List<T> m_objects;
        private GameObject m_prefab;

        private Transform m_prefabRoot;
        private Transform m_releaseRoot;
        private Transform m_getRoot;

        public int CountAll { get; private set; }
        public int CountInactive { get; private set; }
        public int CountActive { get { return CountAll - CountInactive; } }

        public UObjectPool(Component componentPrefab, Transform releaseRoot = null, Transform getRoot = null)
        {
            m_objects = new List<T>();
            m_prefab = componentPrefab.gameObject;

            m_prefabRoot = m_prefab.transform.parent;
            m_releaseRoot = releaseRoot;
            m_getRoot = getRoot;
        }

        public UObjectPool(GameObject prefab, Transform releaseRoot = null, Transform getRoot = null)
        {
            m_objects = new List<T>();
            m_prefab = prefab;

            m_prefabRoot = m_prefab.transform.parent;
            m_releaseRoot = releaseRoot;
            m_getRoot = getRoot;
        }

        /// <summary>
        /// 生产对象
        /// </summary>
        public T Get()
        {
            T obj = null;
            for (int i = m_objects.Count - 1; i >= 0; i--)
            {
                T objItem = m_objects[i];
                if (objItem == null)
                {
                    m_objects.Remove(objItem);
                    CountAll--;
                    continue;
                }
                if (!objItem.IsActive)
                {
                    obj = objItem;
                    CountInactive--;
                    break;
                }
            }

            if (obj == null)
            {
                obj = new T();
                obj.gameObject = EngineUtil.Instantiate(m_prefab);
                m_objects.Add(obj);

                CountAll++;
                obj.New();
            }

            if (m_getRoot)
            {
                obj.gameObject.transform.SetParent(m_getRoot, false);
            }
            else
            {
                if (m_prefabRoot)
                {
                    obj.gameObject.transform.SetParent(m_prefabRoot, false);
                }
            }

            obj.Get();
            obj.gameObject.SetActive(true);
            return obj;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public void Release(T obj)
        {
            if (Contains(obj))
            {
                if (m_releaseRoot)
                {
                    obj.gameObject.transform.SetParent(m_releaseRoot, false);
                }
                obj.gameObject.SetActive(false);
                CountInactive++;
                obj.Release();
            }
        }

        /// <summary>
        /// 是否包含此对象
        /// </summary>
        public bool Contains(T obj)
        {
            return m_objects.Contains(obj);
        }

        /// <summary>
        /// 回收所有对象
        /// </summary>
        public void ReleaseAll()
        {
            for (int i = m_objects.Count - 1; i >= 0; i--)
            {
                T objItem = m_objects[i];
                if (objItem == null)
                {
                    m_objects.Remove(objItem);
                    CountAll--;
                    continue;
                }
                if (objItem.IsActive)
                {
                    Release(objItem);
                }
            }
        }

        /// <summary>
        /// 清除不活跃
        /// </summary>
        public void ClearNotActive()
        {
            for (int i = m_objects.Count - 1; i >= 0; i--)
            {
                T objItem = m_objects[i];
                if (!objItem.IsActive)
                {
                    DestroyObj(objItem);
                }
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            for (int i = m_objects.Count - 1; i >= 0; i--)
            {
                T objItem = m_objects[i];
                if (objItem == null)
                {
                    m_objects.Remove(objItem);
                    CountAll--;
                    continue;
                }
                DestroyObj(objItem);
            }
            m_objects.Clear();
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        private void DestroyObj(T obj)
        {
            m_objects.Remove(obj);
            EngineUtil.Destroy(obj.gameObject);
            obj.Dispose();

            CountAll--;
        }

        /// <summary>
        /// 销毁对象池
        /// </summary>
        public void Dispose()
        {
            Clear();
            m_prefab = null;
            m_objects = null;
        }
    }
}