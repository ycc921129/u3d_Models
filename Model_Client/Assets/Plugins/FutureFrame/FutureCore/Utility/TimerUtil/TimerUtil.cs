/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public static class TimerUtil
    {
        private static string name = "TimerUtil";

        #region Time
        private static SimpleTimer _SimpleTimer;
        private static Timer _Timer;
        private static HeavyTimer _HeavyTimer;

        public static SimpleTimer Simple
        {
            get
            {
                if (_SimpleTimer == null)
                {
                    _SimpleTimer = TimerMgr.Instance.CreateSimpleTimer(name, TimerTimeType.Time);
                }
                return _SimpleTimer;
            }
        }

        public static Timer General
        {
            get
            {
                if (_Timer == null)
                {
                    _Timer = TimerMgr.Instance.CreateTimer(name, TimerTimeType.Time);
                }
                return _Timer;
            }
        }

        public static HeavyTimer Heavy
        {
            get
            {
                if (_HeavyTimer == null)
                {
                    _HeavyTimer = TimerMgr.Instance.CreateHeavyTimer(name, TimerTimeType.Time);
                }
                return _HeavyTimer;
            }
        }
        #endregion

        #region UnscaledTime
        private static SimpleTimer _UnscaledSimpleTimer;
        private static Timer _UnscaledTimer;
        private static HeavyTimer _UnscaledHeavyTimer;

        public static SimpleTimer UnscaleSimple
        {
            get
            {
                if (_UnscaledSimpleTimer == null)
                {
                    _UnscaledSimpleTimer = TimerMgr.Instance.CreateSimpleTimer(name, TimerTimeType.UnscaledTime);
                }
                return _UnscaledSimpleTimer;
            }
        }

        public static Timer UnscaleGeneral
        {
            get
            {
                if (_UnscaledTimer == null)
                {
                    _UnscaledTimer = TimerMgr.Instance.CreateTimer(name, TimerTimeType.UnscaledTime);
                }
                return _UnscaledTimer;
            }
        }

        public static HeavyTimer UnscaleHeavy
        {
            get
            {
                if (_UnscaledHeavyTimer == null)
                {
                    _UnscaledHeavyTimer = TimerMgr.Instance.CreateHeavyTimer(name, TimerTimeType.UnscaledTime);
                }
                return _UnscaledHeavyTimer;
            }
        }
        #endregion

        #region RealtimeSinceStartup
        private static SimpleTimer _RealSimpleTimer;
        private static Timer _RealTimer;
        private static HeavyTimer _RealHeavyTimer;

        public static SimpleTimer RealSimple
        {
            get
            {
                if (_RealSimpleTimer == null)
                {
                    _RealSimpleTimer = TimerMgr.Instance.CreateSimpleTimer(name, TimerTimeType.RealtimeSinceStartup);
                }
                return _RealSimpleTimer;
            }
        }

        public static Timer RealGeneral
        {
            get
            {
                if (_RealTimer == null)
                {
                    _RealTimer = TimerMgr.Instance.CreateTimer(name, TimerTimeType.RealtimeSinceStartup);
                }
                return _RealTimer;
            }
        }

        public static HeavyTimer RealHeavy
        {
            get
            {
                if (_RealHeavyTimer == null)
                {
                    _RealHeavyTimer = TimerMgr.Instance.CreateHeavyTimer(name, TimerTimeType.RealtimeSinceStartup);
                }
                return _RealHeavyTimer;
            }
        }
        #endregion
    }
}