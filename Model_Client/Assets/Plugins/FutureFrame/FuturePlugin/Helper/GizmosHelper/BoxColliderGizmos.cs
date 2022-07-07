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
    public class BoxColliderGizmos : MonoBehaviour
    {
#if UNITY_EDITOR
        private bool isAwakePlaying = true;
        public BoxCollider boxCollider;
        public Vector3 center;
        public Vector3 size;

        private void Awake()
        {
            isAwakePlaying = Application.isPlaying;

            boxCollider = GetComponent<BoxCollider>();
            if (boxCollider == null)
            {
                boxCollider = gameObject.AddComponent<BoxCollider>();
            }
            center = boxCollider.center;
            size = boxCollider.size;
        }

        private void Update()
        {
            if (boxCollider == null)
            {
                if (Application.isEditor)
                {
                    DestroyImmediate(this);
                }
                else
                {
                    Destroy(this);
                }
                return;
            }

            center = boxCollider.center;
            size = boxCollider.size;
            GizmosHelper.Instance.DrawCube(transform, transform.position, transform.lossyScale, transform.up, transform.forward, center, size, Color.red);
        }

        private void OnDestroy()
        {
            if (isAwakePlaying && Application.isEditor) return;
            GizmosHelper.Instance.RemoveCube(transform);
        }
#endif
    }
}