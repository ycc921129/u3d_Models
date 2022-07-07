/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2018.1.10
*/

using System;
using System.Collections.Generic;

namespace FutureCore
{
    public enum TickUpdateType : int
    {
        TickUpdate = 1,
        LateUpdate = 2,
        FixedUpdate = 3,
    }

    public sealed class TickMgr : BaseMonoMgr<TickMgr>
    {
        private const int MaxUpdatesPriority = 10;
        private const int MaxLateUpdatesPriority = 10;
        private const int MaxFixedUpdatesPriority = 10;

        private List<Action> generalList = new List<Action>();
        private List<Action>[] updatesList = new List<Action>[MaxUpdatesPriority];
        private List<Action>[] lateUpdatesList = new List<Action>[MaxLateUpdatesPriority];
        private List<Action>[] fixedUpdatesList = new List<Action>[MaxFixedUpdatesPriority];

        public void Add(Action action)
        {
            generalList.Add(action);
        }

        public void Remove(Action action)
        {
            generalList.Remove(action);
        }

        public void Add(Action action, TickUpdateType type = TickUpdateType.TickUpdate, int priority = 0)
        {
            int maxPriority = 0;
            List<Action>[] list = null;
            switch (type)
            {
                case TickUpdateType.TickUpdate:
                    maxPriority = MaxUpdatesPriority;
                    list = updatesList;
                    break;
                case TickUpdateType.LateUpdate:
                    maxPriority = MaxLateUpdatesPriority;
                    list = lateUpdatesList;
                    break;
                case TickUpdateType.FixedUpdate:
                    maxPriority = MaxFixedUpdatesPriority;
                    list = fixedUpdatesList;
                    break;
            }

            if (priority > maxPriority)
            {
                priority = maxPriority;
            }
            if (list[priority] == null)
            {
                list[priority] = ListPool<Action>.Get();
            }
            list[priority].Add(action);
        }

        public void Remove(Action action, TickUpdateType type = TickUpdateType.TickUpdate, int priority = 0)
        {
            int maxPriority = 0;
            List<Action>[] list = null;
            switch (type)
            {
                case TickUpdateType.TickUpdate:
                    maxPriority = MaxUpdatesPriority;
                    list = updatesList;
                    break;
                case TickUpdateType.LateUpdate:
                    maxPriority = MaxLateUpdatesPriority;
                    list = lateUpdatesList;
                    break;
                case TickUpdateType.FixedUpdate:
                    maxPriority = MaxFixedUpdatesPriority;
                    list = fixedUpdatesList;
                    break;
            }

            if (priority > maxPriority)
            {
                priority = maxPriority;
            }
            if (list[priority] == null) return;

            list[priority].Remove(action);
            if (list[priority].Count == 0)
            {
                ListPool<Action>.Release(list[priority]);
            }
        }

        private void Update()
        {
            if (generalList.Count != 0)
            {
                for (int i = 0; i < generalList.Count; i++)
                {
                    generalList[i]();
                }
            }

            InvokeMethod(updatesList);
        }

        private void LateUpdate()
        {
            InvokeMethod(lateUpdatesList);
        }

        private void FixedUpdate()
        {
            InvokeMethod(fixedUpdatesList);
        }

        private void InvokeMethod(List<Action>[] lists)
        {
            if (lists.Length == 0) return;
            for (int i = 0; i < lists.Length; i++)
            {
                List<Action> actions = lists[i];
                if (actions == null) continue;
                for (int j = 0; j < actions.Count; j++)
                {
                    try
                    {
                        actions[j]();
                    }
                    catch (Exception e)
                    {
                        LogUtil.LogError(e);
                    }
                }
            }
        }

        #region Mgr
        public override void Init()
        {
            base.Init();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            generalList.Clear();
            for (int i = 0; i < updatesList.Length; i++)
            {
                if (updatesList[i] != null)
                {
                    updatesList[i].Clear();
                }
            }
            for (int i = 0; i < lateUpdatesList.Length; i++)
            {
                if (lateUpdatesList[i] != null)
                {
                    lateUpdatesList[i].Clear();
                }
            }
            for (int i = 0; i < fixedUpdatesList.Length; i++)
            {
                if (fixedUpdatesList[i] != null)
                {
                    fixedUpdatesList[i].Clear();
                }
            }

            generalList = null;
            updatesList = null;
            lateUpdatesList = null;
            fixedUpdatesList = null;
        }
        #endregion
    }
}