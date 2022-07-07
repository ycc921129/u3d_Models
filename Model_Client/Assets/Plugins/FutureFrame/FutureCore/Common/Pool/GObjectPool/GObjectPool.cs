/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace FutureCore
{
    public class GObjectPool
    {
        private Transform m_prefabRoot;
        private Transform m_releaseRoot;
        private Transform m_getRoot;

        private Action<GameObject> m_onNew;
        private Action<GameObject> m_onGet;
        private Action<GameObject> m_onRelease;

        private List<GameObject> m_objects;
        private GameObject m_prefab;

        public int CountAll { get; private set; }
        public int CountInactive { get; private set; }
        public int CountActive { get { return CountAll - CountInactive; } }

        public void Init(DynamicAssetLoader loader, string prefabPath, Transform releaseRoot = null, Transform getRoot = null)
        {
            m_objects = new List<GameObject>();
            m_prefab = loader.SyncLoadPrefab(prefabPath);

            m_prefabRoot = m_prefab.transform.parent;
            m_releaseRoot = releaseRoot;
            m_getRoot = getRoot;
        }

        public void Init(GameObject prefab, Transform releaseRoot = null, Transform getRoot = null)
        {
            m_objects = new List<GameObject>();
            m_prefab = prefab;

            m_prefabRoot = m_prefab.transform.parent;
            m_releaseRoot = releaseRoot;
            m_getRoot = getRoot;
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
        public GameObject Get()
        {
            GameObject obj = null;
            for (int i = m_objects.Count - 1; i >= 0; i--)
            {
                GameObject objItem = m_objects[i];
                if (!objItem)
                {
                    m_objects.Remove(objItem);
                    CountAll--;
                    continue;
                }
                if (!objItem.activeSelf)
                {
                    obj = objItem;
                    CountInactive--;
                    break;
                }
            }

            if (obj == null)
            {
                obj = EngineUtil.Instantiate(m_prefab);
                m_objects.Add(obj);
                CountAll++;

                if (m_onNew != null)
                {
                    m_onNew(obj);
                }
            }

            if (m_getRoot)
            {
                obj.transform.SetParent(m_getRoot, false);
            }
            else
            {
                if (m_prefabRoot)
                {
                    obj.transform.SetParent(m_prefabRoot, false);
                }
            }

            if (m_onGet != null)
            {
                m_onGet(obj);
            }

            obj.SetActive(true);
            return obj;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public void Release(GameObject obj)
        {
            if (Contains(obj))
            {
                if (m_releaseRoot)
                {
                    obj.transform.SetParent(m_releaseRoot, false);
                }
                obj.SetActive(false);
                CountInactive++;

                if (m_onRelease != null)
                {
                    m_onRelease(obj);
                }
            }
        }

        /// <summary>
        /// 是否包含此对象
        /// </summary>
        public bool Contains(GameObject obj)
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
                GameObject objItem = m_objects[i];
                if (!objItem)
                {
                    m_objects.Remove(objItem);
                    CountAll--;
                    continue;
                }
                if (objItem.activeSelf)
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
                GameObject objItem = m_objects[i];
                if (!objItem.activeSelf)
                {
                    DestroyObj(objItem);
                    m_objects.Remove(objItem);
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
                GameObject objItem = m_objects[i];
                if (!objItem)
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
        private void DestroyObj(GameObject obj)
        {
            EngineUtil.Destroy(obj.gameObject);
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