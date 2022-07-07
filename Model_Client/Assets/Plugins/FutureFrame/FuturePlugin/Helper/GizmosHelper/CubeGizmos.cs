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
    public class CubeGizmos : MonoBehaviour
    {
#if UNITY_EDITOR
        public bool isVisible = true;
        public Color color = new Color(1, 0, 0f, 0.6f);
        private Mesh mesh;

        private void Start()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            mesh = meshFilter.sharedMesh;
            if (GetComponent<MeshRenderer>() == null)
            {
                gameObject.AddComponent<MeshRenderer>();
            }
        }

        private void OnDrawGizmos()
        {
            if (!isVisible) return;

            Color rawColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawMesh(mesh, 0, transform.position, transform.rotation, transform.lossyScale);
            Gizmos.color = rawColor;
        }

        //private void OnDrawGizmosSelected()
        //{
        //}
#endif
    }
}