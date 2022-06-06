using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frame
{
    public class SimpleTimer : MonoBehaviour
    {
        private Dictionary<Action, float> mIntervalDic = new Dictionary<Action, float>();
        private List<Action> triggers = new List<Action>();

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
            if (mIntervalDic.Count > 0)
            {
                triggers.Clear();
                foreach (KeyValuePair<Action, float> KeyValue in mIntervalDic)
                {
                    if (KeyValue.Value <= GetTime())
                    {
                        triggers.Add(KeyValue.Key);
                    }
                }
                for (int i = 0; i < triggers.Count; i++)
                {
                    Action func = triggers[i];
                    mIntervalDic.Remove(func);

                    func();
                }
            }
        }

        public void AddTimer(float interval, Action func)
        {
            if (null != func)
            {
                if (interval <= 0)
                {
                    func();
                    return;
                }
                mIntervalDic[func] = GetTime() + interval;
            }
        }

        public void RemoveTimer(Action func)
        {
            if (null != func)
            {
                if (mIntervalDic.ContainsKey(func))
                {
                    mIntervalDic.Remove(func);
                }
            }
        }

        public void ClearAll()
        {
            mIntervalDic.Clear();
            triggers.Clear();
        }

        public void Destroy()
        {
            Destroy(this);
        }

        private void OnDestroy()
        {
            ClearAll();
            mIntervalDic = null;
            triggers = null;
        }
    }
}