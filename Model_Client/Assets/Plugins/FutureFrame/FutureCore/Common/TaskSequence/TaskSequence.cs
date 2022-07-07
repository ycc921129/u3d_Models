/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;

namespace FutureCore
{
    /// <summary>
    /// 任务序列
    /// </summary>
    public class TaskSequence
    {
        public Action onFinish;
        private List<TaskProcedure> taskList;
        private bool isCancel = false;

        public TaskSequence()
        {
            taskList = new List<TaskProcedure>();
        }

        public TaskSequence(Action onFinish)
        {
            taskList = new List<TaskProcedure>();
            this.onFinish = onFinish;
        }

        public TaskSequence Add(Action<TaskProcedure> taskFunc)
        {
            TaskProcedure task = new TaskProcedure();
            task.taskSequence = this;
            task.onTaskFunc = taskFunc;
            taskList.Add(task);
            return this;
        }

        public TaskSequence Add(bool result, Action<TaskProcedure> trueTaskFunc)
        {
            if (!result) return this;
            return Add(trueTaskFunc);
        }

        public TaskSequence Add(bool result, Action<TaskProcedure> trueTaskFunc, Action<TaskProcedure> falseTaskFunc)
        {
            if (result)
            {
                return Add(trueTaskFunc);
            }
            else
            {
                return Add(falseTaskFunc);
            }
        }

        public TaskSequence Add(float delayTime, Action<TaskProcedure> taskFunc)
        {
            TaskProcedure task = new TaskProcedure();
            task.taskSequence = this;
            task.onTaskFunc = (taskParam) =>
            {
                TimerUtil.Simple.AddTimer(delayTime, () =>
                {
                    taskFunc(task);
                });
            };
            taskList.Add(task);
            return this;
        }

        public TaskSequence AddDelay(float delayTime)
        {
            TaskProcedure task = new TaskProcedure();
            task.taskSequence = this;
            task.onTaskFunc = (taskParam) =>
            {
                TimerUtil.Simple.AddTimer(delayTime, () =>
                {
                    if (task.onComplete != null)
                    {
                        task.onComplete();
                    }
                });
            };
            taskList.Add(task);
            return this;
        }

        public TaskSequence Run(float delayTime)
        {
            TimerUtil.Simple.AddTimer(delayTime, () => Run());
            return this;
        }

        public TaskSequence Run()
        {
            if (isCancel)
                return null;
            for (int i = 0; i < taskList.Count - 1; i++)
            {
                int index = i;
                int nextIndex = index + 1;
                taskList[index].onComplete = () => taskList[nextIndex].onTaskFunc(taskList[nextIndex]);
            }

            if (taskList.Count > 0)
            {
                taskList[taskList.Count - 1].onComplete = onFinish;
                taskList[0].onTaskFunc(taskList[0]);
            }
            return this;
        }

        public TaskSequence Clear()
        {
            onFinish = null;
            foreach (TaskProcedure task in taskList)
            {
                task.Dispose();
            }
            taskList.Clear();
            return this;
        }

        public TaskSequence Cancel()
        {
            isCancel = true;
            Clear();
            return this;
        }
    }

    /// <summary>
    /// 任务步骤
    /// </summary>
    public class TaskProcedure
    {
        public TaskSequence taskSequence;
        public Action<TaskProcedure> onTaskFunc;
        public Action onComplete;

        public void InvokeComplete()
        {
            if (onComplete != null)
            {
                onComplete();
            }
        }

        public void DelayInvokeComplete(float delayTime)
        {
            TimerUtil.Simple.AddTimer(delayTime, InvokeComplete);
        }

        public void Dispose()
        {
            taskSequence = null;
            onTaskFunc = null;
            onComplete = null;
        }
    }
}