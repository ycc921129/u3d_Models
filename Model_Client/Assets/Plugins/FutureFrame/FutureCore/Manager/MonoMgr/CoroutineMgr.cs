/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.12.22
*/

using System;
using System.Collections;
using UnityEngine;

namespace FutureCore
{
    public sealed class CoroutineMgr : BaseMonoMgr<CoroutineMgr>
    {
        public Coroutine StartRoutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }

        public void StopRoutine(Coroutine routine)
        {
            StopCoroutine(routine);
        }

        public void StopRoutine(IEnumerator routine)
        {
            StopCoroutine(routine);
        }

        public void StopAllRoutine()
        {
            StopAllCoroutines();
        }

        public void WaitForFrames(int frames, Action func)
        {
            StartCoroutine(OnWaitForFrames(frames, func));
        }

        private IEnumerator OnWaitForFrames(int frames, Action func)
        {
            for (int i = 0; i < frames; i++)
            {
                yield return YieldConst.WaitForNextFrame;
            }
            func();
        }

        public IEnumerator StartJobUntil(Func<bool> funtion, Action action)
        {
            yield return new WaitUntil(funtion);
            // 条件 true
            if (action != null)
            {
                action();
            }
        }

        public IEnumerator StartJobUntil<T>(Func<bool> funtion, Action<T> action, T obj)
        {
            yield return new WaitUntil(funtion);
            // 条件 true
            if (action != null)
            {
                action(obj);
            }
        }

        public IEnumerator StartJobWhile(Func<bool> funtion, Action action)
        {
            yield return new WaitUntil(funtion);
            // 条件 false
            if (action != null)
            {
                action();
            }
        }

        public IEnumerator StartJobWhile<T>(Func<bool> funtion, Action<T> action, T obj)
        {
            yield return new WaitWhile(funtion);
            // 条件 false
            if (action != null)
            {
                action(obj);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            StopAllCoroutines();
        }
    }
}