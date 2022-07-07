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
    public class AllBoxColliderGizmos : MonoBehaviour
    {
#if UNITY_EDITOR
        private void Update()
        {
            BoxCollider[] boxColliders = FindObjectsOfType<BoxCollider>();
            for (int i = 0; i < boxColliders.Length; i++)
            {
                BoxCollider boxCollider = boxColliders[i];
                if (boxCollider.GetComponent<BoxColliderGizmos>() == null)
                {
                    boxCollider.gameObject.AddComponent<BoxColliderGizmos>();
                }
            }
        }
#endif
    }
}