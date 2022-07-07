/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace FutureCore
{
    public class TaskList : TaskListItem
    {
        public enum ActionsExecutionMode
        {
            /// <summary>
            /// 顺序执行
            /// </summary>
            RunInSequence,

            /// <summary>
            /// 并行执行
            /// </summary>
            RunInParallel
        }

        public TaskList(ActionsExecutionMode executionMode)
        {
            this.executionMode = executionMode;
        }

        public override float Progress
        {
            get
            {
                float cur = 0f;
                for (int i = 0; i < actions.Count; i++)
                {
                    cur += actions[i].Progress;
                }
                return cur / actions.Count;
            }
        }

        private readonly List<int> finishedIndeces = new List<int>();

        public List<TaskListItem> actions = new List<TaskListItem>();
        public ActionsExecutionMode executionMode = ActionsExecutionMode.RunInSequence;

        private int mCurIndex;

        protected override void OnExecute()
        {
            mCurIndex = 0;
            finishedIndeces.Clear();
            Progress = 0;
        }

        protected override void OnUpdate()
        {
            if (actions.Count == 0)
            {
                EndAction();
                return;
            }

            switch (executionMode)
            {
                case ActionsExecutionMode.RunInParallel:
                    CheckParallelTask();
                    break;

                case ActionsExecutionMode.RunInSequence:
                    CheckInSequenceTask();
                    break;
            }
        }

        protected override void OnReset()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Reset();
            }

            mCurIndex = 0;
            finishedIndeces.Clear();
        }

        private void CheckParallelTask()
        {
            for (var i = 0; i < actions.Count; i++)
            {
                if (finishedIndeces.Contains(i)) continue;
                var status = actions[i].Tick(mOwnerSystem);
                if (status == TaskListItemStatus.Failure)
                {
                    mErr = actions[i].ErrInfo;
                    EndAction(false);
                    actions[i].onFinished?.Invoke(false);
                    return;
                }

                if (status == TaskListItemStatus.Success)
                {
                    finishedIndeces.Add(i);
                    actions[i].onFinished?.Invoke(true);
                }
            }
            if (finishedIndeces.Count == actions.Count) EndAction();
        }

        private void CheckInSequenceTask()
        {
            for (var i = mCurIndex; i < actions.Count; i++)
            {
                var status = actions[i].Tick(mOwnerSystem);

                if (status == TaskListItemStatus.Failure)
                {
                    EndAction(false);
                    actions[i].onFinished?.Invoke(false);
                    return;
                }

                if (status == TaskListItemStatus.Running)
                {
                    mCurIndex = i;
                    return;
                }
                else
                {
                    actions[i].onFinished?.Invoke(true);
                }
            }
            EndAction();
        }

        public void AddTask(TaskListItem task)
        {
            actions.Add(task);
        }
    }
}