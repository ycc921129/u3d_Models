/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FuturePlugin
{
    [HelpURL("https://www.baidu.com/")]
    [ExecuteInEditMode]
    public class PosSortingOrder2DMB : MonoBehaviour
    {
        private static int BasicsSortingNumer = 100;

        [Space(1)]
        [ContextMenuItem("ClearFloorHeight", "ClearFloorHeight")]
        [Header("脚底高度")]
        [Tooltip("脚底高度")]
        [Range(-1, 1)]
        [SerializeField]
        private float m_floorHeight;
        private float m_spriteHalfWidth;
        private float m_spriteHalfHeight;
        private SpriteRenderer spriteRenderer;

        void ClearFloorHeight()
        {
            m_floorHeight = 0;
            LogUtil.Log("ClearFloorHeight");
        }

        [ContextMenu("DebugSortingOrder")]
        void DebugSortingOrder()
        {
            LogUtil.Log("DebugSortingOrder");
        }

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            m_spriteHalfWidth = spriteRenderer.bounds.size.x * 0.5f;
            m_spriteHalfHeight = spriteRenderer.bounds.size.y * 0.5f;
        }

#if UNITY_EDITOR
    void LateUpdate()
        {
            Vector3 floorHeightPos = new Vector3
            (
                transform.position.x,
                transform.position.y - m_spriteHalfHeight + m_floorHeight,
                transform.position.z
            );

            float orderNum = Mathf.Abs(floorHeightPos.y) * BasicsSortingNumer;
            if (orderNum < BasicsSortingNumer)
            {
                orderNum = BasicsSortingNumer;
            }
            spriteRenderer.sortingOrder = (int)orderNum;
        }
#endif

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Vector3 floorHeightPos = new Vector3
            (
                transform.position.x,
                transform.position.y - m_spriteHalfHeight + m_floorHeight,
                transform.position.z
            );

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(floorHeightPos + Vector3.left * m_spriteHalfWidth, floorHeightPos + Vector3.right * m_spriteHalfWidth);
        }
#endif
    }
}