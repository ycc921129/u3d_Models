/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using JacobGames.SuperInvoke;

namespace FutureCore
{
    public static class SuperInvokeHelper
    {
        public static void Init()
        {
            SuperInvoke.Init(dontDestroyOnLoad: true);
        }

        public static string CreateTag()
        {
            return SuperInvoke.CreateTag();
        }

        public static IJob Run(float waitTime, Action action, string tag = null)
        {
            return SuperInvoke.Run(action, waitTime, tag);
        }

        public static IJob Run<T>(float waitTime, Action<T> action, T obj, string tag = null)
        {
            return SuperInvoke.Run(waitTime, () => action(obj));
        }

        public static void Run(int waitFrames, Action action)
        {
            SuperInvoke.SkipFrames(waitFrames, action);
        }

        public static void Pause(params string[] tag)
        {
            SuperInvoke.Pause(tag);
        }

        public static void Resume(params string[] tag)
        {
            SuperInvoke.Resume(tag);
        }

        public static void Kill(params string[] tag)
        {
            SuperInvoke.Kill(tag);
        }

        public static void KillAll()
        {
            SuperInvoke.KillAll();
        }

        public static ISuperInvokeSequence CreateSequence()
        {
            ISuperInvokeSequence sequence = SuperInvoke.CreateSequence();
            return sequence;
        }

        public static ISuperInvokeSequence AddMethod(ISuperInvokeSequence sequence, Action action)
        {
            return sequence.AddMethod(action);
        }

        public static ISuperInvokeSequence AddDelay(ISuperInvokeSequence sequence, float waitTime)
        {
            return sequence.AddDelay(waitTime);
        }

        public static IJob Run(ISuperInvokeSequence sequence, float waitTime = 0f)
        {
            return sequence.Run(waitTime);
        }
    }
}