/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public static class PhysicsUtil
    {
        public static Vector3 ClosestPoint(Vector3 point, Collider collider, Vector3 position, Quaternion rotation)
        {
            return Physics.ClosestPoint(point, collider, position, rotation);
        }

        public static bool RaycastGameObject(Camera camera, int mask, GameObject gameObject)
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, mask))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool RaycastGameObject_NonAlloc(RaycastHit[] results, Camera camera, int mask, GameObject gameObject)
        {
            if (Physics.RaycastNonAlloc(camera.ScreenPointToRay(Input.mousePosition), results, Mathf.Infinity, mask) > 0)
            {
                if (results[0].collider.gameObject == gameObject)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckSphere(Vector3 position, float radius, int layerMask)
        {
            return Physics.CheckSphere(position, radius, layerMask);
        }

        public static int OverlapSphere_NonAlloc(Vector3 position, float radius, Collider[] results, int layerMask)
        {
            return Physics.OverlapSphereNonAlloc(position, radius, results, layerMask);
        }

        public static bool CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
        {
            return Physics2D.CircleCast(origin, radius, direction, distance, layerMask);
        }

        public static int CircleCast_NonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
        {
            return Physics2D.CircleCastNonAlloc(origin, radius, direction, results, distance, layerMask);
        }
    }
}