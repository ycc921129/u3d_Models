/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using UnityEngine;

namespace FuturePlugin
{
    public class EffectTaskMB : MonoBehaviour
    {
        [Header("持续时间")]
        public float duration = 2f;
        [Header("是否跟随对象")]
        public bool isFollowObj = true;
        [HideInInspector]
        public Action<GameObject> taskFunc;

        private bool isTime = false;
        private float currTime = 0;

        private void Start()
        {
            Execute();
        }

        private void Update()
        {
            if (!isTime) return;
            currTime += Time.deltaTime;
            if (currTime >= duration)
            {
                isTime = false;
                OnTask();
            }
        }

        private void Execute()
        {
            if (duration == 0)
            {
                OnTask();
                return;
            }
            isTime = true;
        }

        private void OnTask()
        {
            if (taskFunc != null)
            {
                taskFunc(gameObject);
            }
        }

        private void Reset()
        {
            isTime = false;
            currTime = 0;
        }

        private void OnDisable()
        {
            Reset();
        }
    }
}