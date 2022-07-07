/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp
{
    public static class LoginStatistics
    {
        /// <summary>
        /// 是否是新用户
        /// </summary>
        public static bool IsNewInstall()
        {
            return ProjectApplication.Instance.IsNewInstall;
        }

        /// <summary>
        /// 初始化登录漏洞
        /// </summary>
        public static void InitFunnel()
        {
            int realtimeSinceStartupMS = (int)(UnityEngine.Time.realtimeSinceStartup * 1000);
            if (IsNewInstall())
            {
                ChannelMgr.Instance.SendStatisticEventWithTime(StatisticConst.dlld_newuser_framelaunch_1, realtimeSinceStartupMS);
                ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_newuser_connectlogin_2);
                ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_newuser_loginsucceed_3);
                ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_newuser_preferencesinitstart_4);
                ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_newuser_preferencesinitcomplete_5);
                ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_newuser_preferencesinitend_6);
                ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_newuser_configinit_7);
                ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_newuser_loadcomplete_8);
                ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_newuser_gamestart_9);
            }
            ChannelMgr.Instance.SendStatisticEventWithTime(StatisticConst.dlld_framelaunch_1, realtimeSinceStartupMS);
            ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_connectlogin_2);
            ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_loginsucceed_3);
            ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_preferencesinitstart_4);
            ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_preferencesinitcomplete_5);
            ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_preferencesinitend_6);
            ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_configinit_7);
            ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_loadcomplete_8);
            ChannelMgr.Instance.StartStatisticTimeEvent(StatisticConst.dlld_gamestart_9);
        }

        public static void AddFunnelData_connectlogin_2()
        {
            if (IsNewInstall())
            {
                ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_newuser_connectlogin_2);
            }
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_connectlogin_2);
        }
        public static void AddFunnelData_loginsucceed_3()
        {
            if (IsNewInstall())
            {
                ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_newuser_loginsucceed_3);
            }
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_loginsucceed_3);
        }
        public static void AddFunnelData_preferencesinitstart_4()
        {
            if (IsNewInstall())
            {
                ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_newuser_preferencesinitstart_4);
            }
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_preferencesinitstart_4);
        }
        public static void AddFunnelData_preferencesinitcomplete_5()
        {
            if (IsNewInstall())
            {
                ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_newuser_preferencesinitcomplete_5);
            }
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_preferencesinitcomplete_5);
        }
        public static void AddFunnelData_preferencesinitend_6()
        {
            if (IsNewInstall())
            {
                ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_newuser_preferencesinitend_6);
            }
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_preferencesinitend_6);
        }
        public static void AddFunnelData_configinit_7()
        {
            if (IsNewInstall())
            {
                ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_newuser_configinit_7);
            }
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_configinit_7);
        }
        public static void AddFunnelData_loadcomplete_8()
        {
            if (IsNewInstall())
            {
                ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_newuser_loadcomplete_8);
            }
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_loadcomplete_8);
        }
        public static void AddFunnelData_gamestart_9()
        {
            if (IsNewInstall())
            {
                ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_newuser_gamestart_9);
            }
            ChannelMgr.Instance.EndStatisticTimeEvent(StatisticConst.dlld_gamestart_9);
        }
    }
}