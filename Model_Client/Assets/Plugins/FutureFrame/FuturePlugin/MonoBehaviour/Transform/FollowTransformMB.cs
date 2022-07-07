/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FuturePlugin
{
    public class FollowTransformMB : MonoBehaviour
    {
        public Transform target;

        private void Start()
        {
            transform.position = target.position;
        }

        private void Update()
        {
            transform.position = target.position;
        }
    }
}