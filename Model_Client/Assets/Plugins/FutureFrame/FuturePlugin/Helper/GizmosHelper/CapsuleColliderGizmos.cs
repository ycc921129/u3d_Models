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
    public class CapsuleColliderGizmos : MonoBehaviour
    {
#if UNITY_EDITOR
        private bool isAwakePlaying = true;
        public CapsuleCollider capsuleCollider;
        public Vector3 center;
        public float radius;
        public float height;
        public CapsuleDirection direction;

        private void Start()
        {
            isAwakePlaying = Application.isPlaying;

            capsuleCollider = GetComponent<CapsuleCollider>();
            if (capsuleCollider == null)
            {
                capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
            }
            center = capsuleCollider.center;
            radius = capsuleCollider.radius;
            height = capsuleCollider.height;
            switch (capsuleCollider.direction)
            {
                case 0:
                    direction = CapsuleDirection.XAxis;
                    break;
                case 1:
                    direction = CapsuleDirection.YAxis;
                    break;
                case 2:
                    direction = CapsuleDirection.ZAxis;
                    break;
            }
        }

        private void Update()
        {
            center = capsuleCollider.center;
            radius = capsuleCollider.radius;
            height = capsuleCollider.height;
            GizmosHelper.Instance.DrawCapsule(transform, transform.position, transform.lossyScale, center, radius, height, direction, Color.red);
        }

        private void OnDestroy()
        {
            if (isAwakePlaying && Application.isEditor) return;
            GizmosHelper.Instance.RemoveCapsule(transform);
        }
#endif
    }
}