/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.3
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace FutureCore
{
    public static class TransformExtend
    {
        /// <summary>
        /// 广度优先搜索遍历
        /// </summary>
        /// <param name="root"></param>
        /// <typeparam name="TP">遍历时调用的函数的参数的类型</typeparam>
        /// <typeparam name="TR">遍历时调用的函数的返回值的类型</typeparam>
        /// <param name="visitFunc">遍历时调用的函数
        /// <para>TR Function(Transform t, T para)</para>
        /// </param>
        /// <param name="para">遍历时调用的函数的第二个参数</param>
        /// <param name="failReturnValue">遍历时查找失败的返回值</param>
        /// <returns>遍历时调用的函数的返回值</returns>
        public static TR BFSVisit<TP, TR>(this Transform root, Func<Transform, TP, TR> visitFunc, TP para, TR failReturnValue = default(TR))
        {
            TR ret = visitFunc(root, para);
            if (ret != null && !ret.Equals(failReturnValue))
                return ret;
            Queue<Transform> parents = new Queue<Transform>();
            parents.Enqueue(root);
            while (parents.Count > 0)
            {
                Transform parent = parents.Dequeue();
                foreach (Transform child in parent)
                {
                    ret = visitFunc(child, para);
                    if (ret != null && !ret.Equals(failReturnValue))
                        return ret;
                    parents.Enqueue(child);
                }
            }
            return failReturnValue;
        }

        /// <summary>
        /// 深度优先搜索遍历
        /// </summary>
        /// <param name="root"></param>
        /// <typeparam name="TP">遍历时调用的函数的参数的类型</typeparam>
        /// <typeparam name="TR">遍历时调用的函数的返回值的类型</typeparam>
        /// <param name="visitFunc">遍历时调用的函数
        /// <para>TR Function(Transform t, T para)</para>
        /// </param>
        /// <param name="para">遍历时调用的函数的第二个参数</param>
        /// <param name="failReturnValue">遍历时查找失败的返回值</param>
        /// <returns>遍历时调用的函数的返回值</returns>
        public static TR DFSVisit<TP, TR>(this Transform root, Func<Transform, TP, TR> visitFunc, TP para, TR failReturnValue = default(TR))
        {
            Stack<Transform> parents = new Stack<Transform>();
            parents.Push(root);
            while (parents.Count > 0)
            {
                Transform parent = parents.Pop();
                TR ret = visitFunc(parent, para);
                if (ret != null && !ret.Equals(failReturnValue))
                    return ret;
                for (int i = parent.childCount - 1; i >= 0; i--)
                {
                    parents.Push(parent.GetChild(i));
                }
            }
            return failReturnValue;
        }

        /// <summary>
        /// 根据名字查找并返回子孙，广度优先搜索
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="childName">要查找的子孙的名字</param>
        /// <returns>要查找的子孙的Transform</returns>
        public static T FindComponent_BFS<T>(this Transform trans, string childName) where T : Component
        {
            var target = BFSVisit(trans,
                (t, str) => { if (t.name.Equals(str)) return t; return null; },
                childName
            );

            if (target == null)
            {
                LogUtil.LogError(string.Format("Cann't find child transform {0} in {1}", childName, trans.gameObject.name));
                return null;
            }

            T component = target.GetComponent<T>();
            if (component == null)
            {
                LogUtil.LogError("Component is null, type = " + typeof(T).Name);
                return null;
            }
            return component;
        }

        /// <summary>
        /// 根据名字查找并返回子孙，广度优先搜索
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="tagName">要查找的子孙的名字</param>
        /// <returns>要查找的子孙的Transform</returns>
        public static Transform FindChild_ByTag(this Transform trans, string tagName)
        {
            var target = BFSVisit(trans,
                (t, str) => { if (t.tag.Equals(str)) return t; return null; },
                tagName
            );

            if (target == null)
            {
                LogUtil.LogError(string.Format("Cann't find child transform {0} in {1}", tagName, trans.gameObject.name));
                return null;
            }

            return target;
        }

        /// <summary>
        /// 根据名字查找并返回子孙，深度优先搜索
        /// </summary>
        /// /// <param name="trans"></param>
        /// <param name="childName">要查找的子孙的名字</param>
        /// <returns>要查找的子孙的Transform</returns>
        public static T FindComponent_DFS<T>(this Transform trans, string childName) where T : Component
        {
            var target = DFSVisit(trans,
                (t, str) => { if (t.name.Equals(str)) return t; return null; },
                childName
            );

            if (target == null)
            {
                LogUtil.LogError(string.Format("Cann't find child transform {0} in {1}", childName, trans.gameObject.name));
                return null;
            }

            T component = target.GetComponent<T>();
            if (component == null)
            {
                LogUtil.LogError("Component is null, type = " + typeof(T).Name);
                return null;
            }
            return component;
        }

        /// <summary>
        /// 根据名字查找并返回子孙，广度优先搜索
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="childName">要查找的子孙的名字</param>
        /// <returns>要查找的子孙的Transform</returns>
        public static Transform FindTransform_BFS(this Transform trans, string childName)
        {
            Transform tf = trans.FindComponent_BFS<Transform>(childName);
            return tf;
        }

        /// <summary>
        /// 根据名字查找并返回子孙，广度优先搜索
        /// </summary>
        public static GameObject FindGameObject_BFS(this GameObject go, string childName)
        {
            Transform tf = go.transform.FindComponent_BFS<Transform>(childName);
            return tf.gameObject;
        }

        /// <summary>
        /// 根据名字查找并返回子孙，深度优先搜索
        /// </summary>
        /// /// <param name="trans"></param>
        /// <param name="childName">要查找的子孙的名字</param>
        /// <returns>要查找的子孙的Transform</returns>
        public static Transform FindTransform_DFS(this Transform trans, string childName)
        {
            Transform tf = trans.FindComponent_DFS<Transform>(childName);
            return tf;
        }

        /// <summary>
        /// 根据名字查找并返回子孙，深度优先搜索
        /// </summary>
        public static GameObject FindGameObject_DFS(this GameObject go, string childName)
        {
            Transform tf = go.transform.FindComponent_DFS<Transform>(childName);
            return tf.gameObject;
        }

        /// <summary>
        /// 根据名字在子对象中查找组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="trans"></param>
        /// <param name="name"></param>
        /// /// <param name="reportError"></param>
        /// <returns></returns>
        public static T FindComponent<T>(this Transform trans, string name, bool reportError = true) where T : Component
        {
            Transform target = trans.Find(name);
            if (target == null)
            {
                if (reportError)
                {
                    LogUtil.LogError("Transform is null, name = " + name);
                }

                return null;
            }

            T component = target.GetComponent<T>();
            if (component == null)
            {
                if (reportError)
                {
                    LogUtil.LogError("Component is null, type = " + typeof(T).Name);
                }

                return null;
            }

            return component;
        }

        /// <summary>
        /// 初始化物体的相对位置、旋转、缩放
        /// </summary>
        /// <param name="trans"></param>
        public static void InitTransformLocal(this Transform trans)
        {
            trans.localPosition = Vector3.zero;
            trans.localScale = Vector3.one;
            trans.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// 可以递归地查找所有子节点的某个T类型的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transform"></param>
        /// <param name="recursive"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public static T[] FindComponentsInChildren<T>(this Transform transform, bool recursive = true, bool includeInactive = true) where T : Component
        {

            if (recursive)
            {
                var list = new List<T>();
                GetChildren(transform, includeInactive, ref list);
                return list.ToArray();
            }
            else
            {
                return transform.GetComponentsInChildren<T>(includeInactive);
            }
        }

        public static T GetComponentsInParent<T>(this Transform transform) where T : Component
        {
            if (transform == null)
            {
                return null;
            }
            if (transform.GetComponent<T>() != null)
            {
                return transform.GetComponent<T>();
            }
            if (transform.parent != null)
            {
                return transform.parent.GetComponentInParent<T>();
            }
            return null;
        }

        public static Transform GetChildByName(this Transform transform, string name, bool recursive = true, bool includeInactive = true)
        {
            Transform target;
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child != null && (includeInactive || transform.gameObject.activeSelf))
                {
                    if (child.name == name)
                    {
                        return child;
                    }
                    else
                    {
                        target = child.GetChildByName(name);
                        if (target != null) return target;
                    }
                }
            }

            return null;
        }

        private static void GetChildren<T>(Transform t, bool includeInactive, ref List<T> list)
        {
            if (includeInactive || t.gameObject.activeSelf)
            {
                for (int i = 0; i < t.childCount; i++)
                {
                    if (t.GetChild(i) != null)
                    {
                        GetChildren(t.GetChild(i), includeInactive, ref list);
                    }
                }

                var comp = t.GetComponent<T>();
                if (comp != null)
                {
                    list.Add(comp);
                }
            }

        }

        public static void SetPositionAndRotation(this Transform tran, Vector3 position, Quaternion rotation)
        {
            tran.SetPositionAndRotation(position, rotation);
        }

        public static void SetPositionX(this Transform tran, float x)
        {
            Vector3 pos = tran.position;
            pos.x = x;
            tran.position = pos;
        }

        public static void SetPositionY(this Transform tran, float y)
        {
            Vector3 pos = tran.position;
            pos.y = y;
            tran.position = pos;
        }

        public static void SetPositionZ(this Transform tran, float z)
        {
            Vector3 pos = tran.position;
            pos.z = z;
            tran.position = pos;
        }

        public static void SetLocalPositionX(this Transform tran, float x)
        {
            Vector3 pos = tran.localPosition;
            pos.x = x;
            tran.localPosition = pos;
        }

        public static void SetLocalPositionY(this Transform tran, float y)
        {
            Vector3 pos = tran.localPosition;
            pos.y = y;
            tran.localPosition = pos;
        }

        public static void SetLocalPositionZ(this Transform tran, float z)
        {
            Vector3 pos = tran.localPosition;
            pos.z = z;
            tran.localPosition = pos;
        }

        public static void SetLocalScaleX(this Transform tran, float x)
        {
            Vector3 pos = tran.localScale;
            pos.x = x;
            tran.localScale = pos;
        }

        public static void SetLocalScaleY(this Transform tran, float y)
        {
            Vector3 pos = tran.localScale;
            pos.y = y;
            tran.localScale = pos;
        }

        public static void SetLocalScaleZ(this Transform tran, float z)
        {
            Vector3 pos = tran.localScale;
            pos.z = z;
            tran.localScale = pos;
        }

        public static void SetLocalEulerAngleZ(this Transform tran, float z)
        {
            while (z < 0)
            {
                z += 360;
            }
            while (z > 360)
            {
                z -= 360;
            }
            Vector3 pos = tran.localEulerAngles;
            pos.z = z;
            tran.localEulerAngles = pos;
        }

        public static void SetLocalEulerAngleX(this Transform tran, float x)
        {
            while (x < 0)
            {
                x += 360;
            }
            while (x > 360)
            {
                x -= 360;
            }
            Vector3 pos = tran.localEulerAngles;
            pos.x = x;
            tran.localEulerAngles = pos;
        }

        public static void SetLocalEulerAngleY(this Transform tran, float y)
        {
            while (y < 0)
            {
                y += 360;
            }
            while (y > 360)
            {
                y -= 360;
            }
            Vector3 pos = tran.localEulerAngles;
            pos.y = y;
            tran.localEulerAngles = pos;
        }

        public static void SetEulerAngleZ(this Transform tran, float z)
        {
            Vector3 pos = tran.eulerAngles;
            pos.z = z;
            tran.eulerAngles = pos;
        }

        public static void SetEulerAngleX(this Transform tran, float x)
        {
            Vector3 pos = tran.eulerAngles;
            pos.x = x;
            tran.eulerAngles = pos;
        }

        public static void SetEulerAngleY(this Transform tran, float y)
        {
            Vector3 pos = tran.eulerAngles;
            pos.y = y;
            tran.eulerAngles = pos;
        }
    }
}