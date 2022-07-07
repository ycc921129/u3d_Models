/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FuturePlugin
{
    [ExecuteInEditMode]
    public class CombineSubCollider : MonoBehaviour
    {
        [ContextMenu("合并子物体碰撞盒")]
        private void CombineSelf()
        {
            Combine(gameObject);
        }

        public static void Combine(GameObject gameObject)
        {
            Transform parent = gameObject.transform;
            Vector3 postion = parent.position;
            Quaternion rotation = parent.rotation;
            Vector3 scale = parent.localScale;
            parent.position = Vector3.zero;
            parent.rotation = Quaternion.Euler(Vector3.zero);
            parent.localScale = Vector3.one;

            Collider[] colliders = parent.GetComponentsInChildren<Collider>();
            foreach (Collider child in colliders)
            {
                DestroyImmediate(child);
            }
            Vector3 center = Vector3.zero;
            Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
            foreach (Renderer child in renders)
            {
                center += child.bounds.center;
            }
            center /= parent.GetComponentsInChildren<Transform>().Length;
            Bounds bounds = new Bounds(center, Vector3.zero);
            foreach (Renderer child in renders)
            {
                bounds.Encapsulate(child.bounds);
            }
            BoxCollider boxCollider = parent.gameObject.AddComponent<BoxCollider>();
            boxCollider.center = bounds.center - parent.position;
            boxCollider.size = bounds.size;

            parent.position = postion;
            parent.rotation = rotation;
            parent.localScale = scale;
        }
    }
}