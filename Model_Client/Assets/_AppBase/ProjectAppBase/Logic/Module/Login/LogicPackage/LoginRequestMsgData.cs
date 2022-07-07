/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using ProjectApp.Protocol;
using System.Collections.Generic;

namespace ProjectApp 
{
    public static class LoginRequestMsgData
    {
        public static void Init(C2S_reg_login_data reqData, List<string> C2S_infoExtraFields)
        {
            try
            {
                reqData.uid = LoginLocalCache.GetLocalUid();
                reqData.token = LoginLocalCache.GetToken();
#if UNITY_EDITOR
                reqData.ids = new Ids();
                if(Channel.Current.debug) reqData.pgtype = 1;
#else              
                if (Channel.Current.ids == string.Empty)
                {
                    LogUtil.LogError(StringUtil.Concat("[LoginRequestMsgData] Channel.Current.ids is null, channel = ", Channel.Current.ToString()));
                    reqData.ids = new Ids();
                }
                else
                    reqData.ids = SerializeUtil.ToObject<Dictionary<string, string>>(Channel.Current.ids);

                if (Channel.Current.buildType != FuturePlugin.AppBuildType.Release)
                    reqData.pgtype = (int)Channel.Current.buildType;
#endif
                reqData.pgname = Channel.Current.pg;    
                reqData.pgvercode = Channel.Current.ver_code;  
                reqData.pgverstring = Channel.Current.ver;
                reqData.pg_setting_version = LoginLocalCache.GetCacheOperationConfigVersion();

                reqData.device = new Device();
                reqData.device.tz = DateTimeMgr.Instance.GetCurrTimeZone();
                reqData.device.os = Channel.Current.os_ver;
                reqData.device.ver = Channel.Current.ver;
                reqData.device.fac = Channel.Current.dev_fac;
                reqData.device.lang = Channel.Current.lang;
                reqData.device.imsi = Channel.Current.imsi;
                reqData.device.model = Channel.Current.dev_model;
                reqData.device.network = Channel.Current.network;
                if (Channel.Current.isProxy)
                {
                    reqData.device.network_plugin[0] = "Proxy";
                }
                if (Channel.Current.isVPN)
                {
                    reqData.device.network_plugin[1] = "VPN";
                }
                reqData.device.is_root = Channel.Current.isRoot ? 1 : 0;
                reqData.device.resolution = ScreenConst.CurrResolutionInfo;

                bool isWss = WSNetMgr.Instance.IsAppWssUrl();
                if (isWss)
                {
                    LoginWssConfig.ReadLocalWssGameConfig(reqData);
                }
            }
            catch (System.Exception e)
            {
                LogUtil.LogError("[LoginRequestMsgData] uis or token Decrypt error. 解决方案：清理下缓存.");
            }            
        }
    }
}