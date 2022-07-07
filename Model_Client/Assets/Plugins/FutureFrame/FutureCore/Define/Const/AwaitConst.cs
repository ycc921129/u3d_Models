/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.12
*/

using System;

namespace FutureCore
{
    public static class AwaitConst
    {
        public const float Time10ms = 0.01f;
        public const float Time100ms = 0.1f;
        public const float Time300ms = 0.3f;
        public const float Time500ms = 0.5f;
        public const float Time1s = 1f;
        public const float Time3s = 3f;
        public const float Time5s = 5f;

        public static TimeSpan WaitFor10ms = TimeSpan.FromMilliseconds(Time10ms);
        public static TimeSpan WaitFor100ms = TimeSpan.FromMilliseconds(Time100ms);
        public static TimeSpan WaitFor300ms = TimeSpan.FromMilliseconds(Time300ms);
        public static TimeSpan WaitFor500ms = TimeSpan.FromMilliseconds(Time500ms);
        public static TimeSpan WaitFor1s = TimeSpan.FromSeconds(Time1s);
        public static TimeSpan WaitFor3s = TimeSpan.FromSeconds(Time3s);
        public static TimeSpan WaitFor5s = TimeSpan.FromSeconds(Time5s);
    }
}