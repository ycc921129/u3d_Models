/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.13
*/

using FuturePlugin;
using UnityEngine;

namespace FutureCore
{
    public sealed class AssistDebugMgr : BaseMonoMgr<AssistDebugMgr>
    {
        private FC_Debuger.DebuggerConsoleMB debuggerConsole;
        private AssistAppProfilerMB assistAppProfiler;

        public override void Init()
        {
            base.Init();
            EnableDebugLog();
            PrintDeviceInfo();
            ProcessInitAssistSetting();
        }

        private void EnableDebugLog()
        {
            LogUtil.Log($"EnableDebugLog : {AppConst.IsEnabledLog}");  
        }

        private void PrintDeviceInfo()
        {
            string info = string.Empty;
            info += string.Format("[AssistDebugMgr]DeviceInfo\n", SystemInfo.batteryStatus);

            info += string.Format("CPU硬件:{0}\n", SystemInfo.processorType);
            info += string.Format("CPU核心数量:{0}\n", SystemInfo.processorCount);
            info += string.Format("CPU频率:{0}MHz\n", SystemInfo.processorFrequency);

            info += string.Format("GPU硬件:{0}\n", DeviceConst.GraphicsDeviceName);
            info += string.Format("GPU渲染模式:{0}\n", DeviceConst.GraphicsDeviceType);

            info += string.Format("屏幕分辨率:{0}x{1}\n", Screen.width, Screen.height);
            info += string.Format("屏幕分辨率安全区域:{0}\n", Screen.safeArea);
            info += string.Format("系统内存:{0}MB\n", SystemInfo.systemMemorySize);
            info += string.Format("是否是小端字节序:{0}\n", NetConst.IsLittleEndian);

            info += string.Format("电池电量:{0}\n", SystemInfo.batteryLevel);
            info += string.Format("电池状态:{0}", SystemInfo.batteryStatus);
            LogUtil.Log(info);
        }

        private void ProcessInitAssistSetting()
        {
            ProcessInitAssistConsole();
            ProcessInitAssistProfiler();
        }

        private void ProcessInitAssistConsole()
        {
            bool mode = AppConst.IsDebugMode;
            if (mode && AppConst.IsEnableAssistAppConsole)
            {
                InitAssistConsole();
            }
        }

        private void InitAssistConsole()
        {
            if (!debuggerConsole)
            {
                LogUtil.Log("[AssistDebugMgr]Init AssistConsole");
                GameObject debuggerConsoleGo = new GameObject("AssistConsole");
                debuggerConsoleGo.transform.SetParent(transform, false);
                debuggerConsole = debuggerConsoleGo.AddComponent<FC_Debuger.DebuggerConsoleMB>();
            }
        }

        private void ProcessInitAssistProfiler()
        {
            bool mode = AppConst.IsDebugMode;
            if (mode && AppConst.IsEnableAssistAppProfiler)
            {
                InitAssistProfiler();
            }
        }

        private void InitAssistProfiler()
        {
            if (!assistAppProfiler)
            {
                LogUtil.Log("[AssistDebugMgr]Init AssistAppProfiler");
                GameObject assistAppProfilerGo = new GameObject("AssistAppProfiler");
                assistAppProfilerGo.transform.SetParent(transform, false);
                assistAppProfilerGo.layer = LayerMaskConst.UI;
                assistAppProfiler = assistAppProfilerGo.AddComponent<AssistAppProfilerMB>();
            }
        }

        public void EnabledDebuggerConsole(bool enabled)
        {
            if (debuggerConsole)
            {
                debuggerConsole.enabled = enabled;
            }
            else
            {
                InitAssistConsole();
            }
        }

        public void EnabledAssistAppProfiler(bool enabled)
        {
            if (assistAppProfiler)
            {
                assistAppProfiler.enabled = enabled;
            }
            else
            {
                InitAssistProfiler();
            }
        }
    }
}