using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frame
{
    public class TimerTask
    {
        private Timer timer;

        public int repeatCount;
        public float interval;
        private Action<TimerTask> onCallBack;
        public object[] args;
        public float time;
        public bool isActive = true;
        public bool isFrameUpdate = false;

        public TimerTask(Timer timer, float interval, Action<TimerTask> onCallBack, params object[] args)
        {
            this.timer = timer;

            repeatCount = 1;
            this.interval = interval;
            this.onCallBack = onCallBack;
            this.args = args;
            time = timer.GetTriggerTime(interval);
        }

        public TimerTask(Timer timer, int repeatCount, float interval, Action<TimerTask> onCallBack, params object[] args)
        {
            this.timer = timer;

            this.repeatCount = repeatCount;
            this.interval = interval;
            this.onCallBack = onCallBack;
            this.args = args;
            time = timer.GetTriggerTime(interval);
        }

        public TimerTask(Timer timer, Action<TimerTask> onCallBack, params object[] args)
        {
            this.timer = timer;

            this.onCallBack = onCallBack;
            this.args = args;
        }

        public void SetActive(bool isActive)
        {
            if (this.isActive == isActive) return;

            this.isActive = isActive;
            if (!isFrameUpdate)
            {
                if (this.isActive)
                {
                    time = timer.GetTriggerTime(interval);
                }
            }
        }

        public void CallFunc()
        {
            if (onCallBack != null)
            {
                onCallBack(this);
            }
        }

        public void Kill(bool needComplete = false)
        {
            if (needComplete)
            {
                CallFunc();
            }
            Dispose();
        }

        public void Dispose()
        {
            timer.RemoveTimer(this);
        }

        public static implicit operator bool(TimerTask timerTask)
        {
            return null != timerTask;
        }
    }

    public class Timer : MonoBehaviour
    {
        public const int INFINITE_LOOP = -1;

        private List<TimerTask> timerTaskList = new List<TimerTask>();
        private List<TimerTask> timerTaskTriggers = new List<TimerTask>();

        [SerializeField]
        private string timerName;
        private TimerTimeType type = TimerTimeType.Null;

        public void SetTimer(string name, TimerTimeType type)
        {
            this.timerName = name + "_" + type.ToString();
            this.type = type;
        }

        private float GetTime()
        {
            switch (type)
            {
                case TimerTimeType.Time:
                    return Time.time;
                case TimerTimeType.UnscaledTime:
                    return Time.unscaledTime;
                case TimerTimeType.RealtimeSinceStartup:
                    return Time.realtimeSinceStartup;
            }
            return 0;
        }

        private void Update()
        {
            if (timerTaskList.Count > 0)
            {
                timerTaskTriggers.Clear();
                for (int i = 0; i < timerTaskList.Count; i++)
                {
                    TimerTask timerTask = timerTaskList[i];
                    if (!timerTask.isActive) continue;

                    if (timerTask.isFrameUpdate)
                    {
                        timerTaskTriggers.Add(timerTask);
                    }
                    else
                    {
                        if (timerTask.time <= GetTime())
                        {
                            timerTaskTriggers.Add(timerTask);
                        }
                    }
                }

                for (int i = 0; i < timerTaskTriggers.Count; i++)
                {
                    TimerTask triggerTimerTask = timerTaskTriggers[i];
                    if (triggerTimerTask.repeatCount != INFINITE_LOOP)
                    {
                        triggerTimerTask.repeatCount--;
                    }
                    if (triggerTimerTask.repeatCount == 0)
                    {
                        timerTaskList.Remove(triggerTimerTask);
                    }
                    else
                    {
                        if (!triggerTimerTask.isFrameUpdate)
                        {
                            triggerTimerTask.time = GetTriggerTime(triggerTimerTask.interval);
                        }
                    }
                    triggerTimerTask.CallFunc();
                }
            }
        }

        public TimerTask AddTimer(float interval, Action<TimerTask> onCallBack, params object[] args)
        {
            TimerTask timeTask = new TimerTask(this, interval, onCallBack, args);
            AddTimer(timeTask);
            return timeTask;
        }

        public TimerTask AddRepeatTimer(int repeatCount, float interval, Action<TimerTask> onCallBack, params object[] args)
        {
            TimerTask timeTask = new TimerTask(this, repeatCount, interval, onCallBack, args);
            AddTimer(timeTask);
            return timeTask;
        }

        public TimerTask AddLoopTimer(float interval, Action<TimerTask> onCallBack, params object[] args)
        {
            TimerTask timeTask = new TimerTask(this, INFINITE_LOOP, interval, onCallBack, args);
            AddTimer(timeTask);
            return timeTask;
        }

        public TimerTask AddFrameUpdateTimer(Action<TimerTask> onCallBack, params object[] args)
        {
            TimerTask timeTask = new TimerTask(this, onCallBack, args);
            timeTask.repeatCount = INFINITE_LOOP;
            timeTask.isFrameUpdate = true;
            AddTimer(timeTask);
            return timeTask;
        }

        public void AddTimer(TimerTask timerTask)
        {
            if (null != timerTask)
            {
                if (!timerTask.isFrameUpdate)
                {
                    if (timerTask.interval <= 0)
                    {
                        timerTask.CallFunc();
                        return;
                    }
                }
                timerTaskList.Add(timerTask);
            }
        }

        public void RemoveTimer(TimerTask timerTask)
        {
            if (timerTask == null) return;
            if (timerTaskList == null) return;
            if (timerTaskList.Count == 0) return;

            if (timerTaskList.Contains(timerTask))
            {
                timerTaskList.Remove(timerTask);
            }
        }

        public void ClearAll()
        {
            timerTaskList.Clear();
            timerTaskTriggers.Clear();
        }

        public void Destroy()
        {
            Destroy(this);
        }

        public float GetTriggerTime(float interval)
        {
            return GetTime() + interval;
        }

        private void OnDestroy()
        {
            ClearAll();
            timerTaskList = null;
            timerTaskTriggers = null;
        }
    }
}