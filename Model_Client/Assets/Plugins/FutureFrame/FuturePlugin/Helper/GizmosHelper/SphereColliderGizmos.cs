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
    public class SphereColliderGizmos : MonoBehaviour
    {
#if UNITY_EDITOR
        private bool isAwakePlaying = true;
        public SphereCollider sphereCollider;
        public Vector3 center;
        public float radius;

        private void Start()
        {
            isAwakePlaying = Application.isPlaying;

            sphereCollider = GetComponent<SphereCollider>();
            if (sphereCollider == null)
            {
                sphereCollider = gameObject.AddComponent<SphereCollider>();
            }
            center = sphereCollider.center;
            radius = sphereCollider.radius;
        }

        private void Update()
        {
            center = sphereCollider.center;
            radius = sphereCollider.radius;
            GizmosHelper.Instance.DrawSphere(transform, transform.position, transform.lossyScale, center, radius, Color.red);
        }

        private void OnDestroy()
        {
            if (isAwakePlaying && Application.isEditor) return;
            GizmosHelper.Instance.RemoveSphere(transform);
        }
#endif
    }
}