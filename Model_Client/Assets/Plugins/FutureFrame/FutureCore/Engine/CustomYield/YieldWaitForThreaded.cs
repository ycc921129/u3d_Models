/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.12
*/

using System;
using System.Threading;

namespace FutureCore
{
    public class YieldWaitForThreaded : UnityEngine.CustomYieldInstruction
    {
        private Thread thread;
        private bool isRunning;

        public YieldWaitForThreaded(Action taskFunc, ThreadPriority priority = ThreadPriority.AboveNormal)
        {
            isRunning = true;

            thread = new Thread(() => {
                taskFunc();
                AbortThread();
            }) {
                IsBackground = true
            };

            thread.Start(priority);
        }

        private void AbortThread()
        {
            isRunning = false;
            if (thread != null)
            {
                thread.Abort();
            }
        }

        public override bool keepWaiting
        {
            get { return isRunning; }
        }
    }
}