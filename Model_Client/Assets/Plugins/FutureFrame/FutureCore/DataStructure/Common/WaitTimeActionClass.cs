/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;

namespace FutureCore
{
    public class WaitTimeActionClass
    {
        public float waitTime;
        public Action action;

        public WaitTimeActionClass()
        {
        }

        public WaitTimeActionClass(float waitTime, Action action)
        {
            this.waitTime = waitTime;
            this.action = action;
        }
    }
}