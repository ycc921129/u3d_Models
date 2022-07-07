/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public static class ColliderUtil
    {
        public static Vector3 GetBoxColliderCenterPos(Transform transform, BoxCollider boxCollider)
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetTRS(transform.position, transform.rotation, transform.lossyScale);
            return matrix * new Vector4(boxCollider.center.x, boxCollider.center.y, boxCollider.center.z, 1);
        }
    }
}