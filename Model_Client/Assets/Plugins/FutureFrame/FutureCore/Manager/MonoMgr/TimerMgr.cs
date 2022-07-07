/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public enum TimerTimeType : int
    {
        /// <summary>
        /// 未初始化
        /// </summary>
        Null = -1,
        /// <summary>
        /// 从游戏开发到现在的时间，会随着游戏的暂停而停止计算。
        /// </summary>
        Time = 0,
        /// <summary>
        /// 不考虑timescale时候与time相同，若timescale被设置，则无效。
        /// </summary>
        UnscaledTime = 1,
        /// <summary>
        /// 示自游戏开始后的总时间，即使暂停也会不断的增加。
        /// </summary>
        RealtimeSinceStartup = 2,
    }

    public sealed class TimerMgr : BaseMonoMgr<TimerMgr>
    {
        private GameObject simpleTimersRoot;
        private GameObject timersRoot;
        private GameObject heavyTimersRoot;

        private void InitTimersRoot()
        {
            simpleTimersRoot = new GameObject("SimpleTimers");
            simpleTimersRoot.SetParent(gameObject);

            timersRoot = new GameObject("Timers");
            timersRoot.SetParent(gameObject);

            heavyTimersRoot = new GameObject("HeavyTimers");
            heavyTimersRoot.SetParent(gameObject);
        }

        public SimpleTimer CreateSimpleTimer(string name, TimerTimeType type)
        {
            SimpleTimer simpleTimer = simpleTimersRoot.AddComponent<SimpleTimer>();
            simpleTimer.SetTimer(name, type);
            return simpleTimer;
        }

        public Timer CreateTimer(string name, TimerTimeType type)
        {
            Timer timer = timersRoot.AddComponent<Timer>();
            timer.SetTimer(name, type);
            return timer;
        }

        public HeavyTimer CreateHeavyTimer(string name, TimerTimeType type)
        {
            HeavyTimer heavyTimer = heavyTimersRoot.AddComponent<HeavyTimer>();
            heavyTimer.SetTimer(name, type);
            return heavyTimer;
        }

        #region Mgr
        public override void Init()
        {
            base.Init();
            InitTimersRoot();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        #endregion
    }
}