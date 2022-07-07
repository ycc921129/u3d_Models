/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections;
using UnityEngine;

namespace FutureCore
{
    public enum TaskListItemStatus
    {
        None = 0,
        Running,
        Success,
        Failure
    }

    public abstract class TaskListItem
    {
        public virtual float Progress { get; protected set; }
        private bool latch;
        protected string mErr = string.Empty;
        protected object mOwnerSystem;
        protected float mStartedTime;

        public float ElapsedTime => Time.time - mStartedTime;
        public TaskListItemStatus status { get; private set; }
        public virtual string TaskName { get; }
        public virtual string ErrInfo { get; }

        public bool isRunning => status == TaskListItemStatus.Running;

        public Action<bool> onFinished;

        public void Execute(object owner, Action<bool> callback = null)
        {
            onFinished = callback;
            Progress = 0;
            if (!isRunning) CoroutineMgr.Instance.StartCoroutine(Updater(owner, callback));
        }

        private IEnumerator Updater(object owner, Action<bool> callback)
        {
            while (Tick(owner) == TaskListItemStatus.Running) yield return null;

            if (callback != null)
            {
                callback(status == TaskListItemStatus.Success);
            }
        }

        public TaskListItemStatus Tick(object owner)
        {
            if (status == TaskListItemStatus.Running)
            {
                OnUpdate();
                latch = false;
                return status;
            }
            //如果任务结束了跳过这一帧//
            if (latch)
            {
                latch = false;
                return status;
            }
            mStartedTime = Time.time;
            status = TaskListItemStatus.Running;
            mOwnerSystem = owner;
            OnExecute();
            if (status == TaskListItemStatus.Running)
                OnUpdate();
            return status;
        }

        public void EndAction(bool success = true)
        {
            if (status != TaskListItemStatus.Running)
            {
                OnForcedStop();
                return;
            }

            latch = true;
            status = success ? TaskListItemStatus.Success : TaskListItemStatus.Failure;
            Progress = status == TaskListItemStatus.Success ? 1 : 0;
            OnStop();
        }

        public void Reset()
        {
            status = TaskListItemStatus.None;
            OnReset();
        }

        protected virtual void OnExecute()
        {
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnStop()
        {
        }

        protected virtual void OnReset()
        {
        }

        protected virtual void OnForcedStop()
        {
        }
    }
}