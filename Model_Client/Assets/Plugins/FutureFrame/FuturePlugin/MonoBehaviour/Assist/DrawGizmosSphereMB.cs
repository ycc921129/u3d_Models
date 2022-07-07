/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FuturePlugin
{
    public class DrawGizmosSphereMB : MonoBehaviour
    {
#if UNITY_EDITOR
        public bool isShow = true;
        public Color color = Color.red;
        public float radius;

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public void SetData(float radius)
        {
            this.radius = radius;
        }

        void OnDrawGizmos()
        {
            if (!isShow) return;
            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }
}