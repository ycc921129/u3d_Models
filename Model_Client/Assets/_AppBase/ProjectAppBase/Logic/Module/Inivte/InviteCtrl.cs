/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using Newtonsoft.Json.Linq;
using ProjectApp.Protocol;
using System;
using System.Collections.Generic;

namespace ProjectApp
{
    public class InviteCtrl : BaseCtrl
    {
        private const string TAG = "[InviteCtrl] ";

        public static InviteCtrl Instance { get; private set; }  
        private LoginModel loginModel;
        private TimerTask inviteTimer = null;
        private long mOnline_time = 0;
        private const int CheckInviteTime = 1;

        protected override void OnInit()
        {
            Instance = this;
            loginModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;
        }

        protected override void OnDispose()
        {
            Instance = null;

            if (inviteTimer != null)
            {
                inviteTimer.Dispose();
            }
        }

        protected override void AddListener()
        {
            CtrlDispatcher.Instance.AddListener(CtrlMsg.Game_Start, OnGamestart);

            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnVideoAdClosed, OnVideoAdClosed);
        }

        protected override void RemoveListener()
        {
            CtrlDispatcher.Instance.RemoveListener(CtrlMsg.Game_Start, OnGamestart);

            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnVideoAdClosed, OnVideoAdClosed);
        }

        protected override void AddServerListener()
        {
            wsNetDispatcher.AddPriorityListener(WSNetMsg.S2C_InvokeUpdateStatis, OnUpdataStatis);
        }


        protected override void RemoveServerListener()
        {
            wsNetDispatcher.RemovePriorityListener(WSNetMsg.S2C_InvokeUpdateStatis, OnUpdataStatis);
        }

        #region property
        /// <summary>
        /// 邀请码
        /// </summary>
        public string Invite_Code
        {
            get
            {
                if (loginModel == null
                    || loginModel.loginData == null
                    || loginModel.loginData.info == null) return "";

                return loginModel.loginData.info.invite_code;
            }
        }

        /// <summary>  
        /// 邀请链接
        /// </summary>
        public string Invite_url
        {
            get
            {
                if (loginModel == null
                    || loginModel.loginData == null
                    || loginModel.loginData.info == null) return "";

                return loginModel.loginData.info.invite_url;
            }
        }

        /// <summary>
        /// 邀请数量
        /// </summary>
        public int Invite_count
        {
            get
            {
                if (loginModel == null
                    || loginModel.loginData == null
                    || loginModel.loginData.statis == null) return 0;

                return loginModel.loginData.statis.invite_count;
            }
        }

        /// <summary>
        /// 邀请来源
        /// </summary>
        public string Invite_parent
        {
            get
            {
                if (loginModel == null
                    || loginModel.loginData == null
                    || loginModel.loginData.info == null) return null;

                return loginModel.loginData.info.invite_parent;
            }
        }

        /// <summary>
        /// 在线时间（秒）    
        /// </summary>
        public long Online_time
        {  
            get
            {
                if (loginModel == null
                    || loginModel.loginData == null
                    || loginModel.loginData.statis == null) return 0;
                  
                return loginModel.loginData.statis.online_time;
            }
        }
        #endregion

        #region EventCallBack 
          
        private void OnGamestart(object obj)
        {
            mOnline_time = Online_time;
            inviteTimer = TimerUtil.General.AddLoopTimer(CheckInviteTime, (timer) =>
            {
                CheckIsEffective();
            });
        }

        private void OnUpdataStatis(BaseS2CJsonProto respMsg)
        {
            try
            {
                S2C_InvokeUpdateStatis updateStatisResp = respMsg as S2C_InvokeUpdateStatis;
                // 异常处理
                if (updateStatisResp == null
                    || updateStatisResp.data == null
                    || !string.IsNullOrEmpty(updateStatisResp.err))
                {
                    LogUtil.LogError(updateStatisResp.err);
                    return;
                }
                JObject jObject = updateStatisResp.data as JObject;
                if (jObject["online_day"] != null)
                {
                    loginModel.loginData.statis.online_day = int.Parse(jObject["online_day"].ToString());
                }
                if (jObject["online_time"] != null)
                {
                    loginModel.loginData.statis.online_time = long.Parse(jObject["online_time"].ToString());
                }
                if (jObject["online_count"] != null)
                {
                    loginModel.loginData.statis.online_count = int.Parse(jObject["online_count"].ToString());
                }
                if (jObject["invite_count"] != null)
                {
                    loginModel.loginData.statis.invite_count = int.Parse(jObject["invite_count"].ToString());
                }
                if (jObject["attribute_count"] != null)
                {
                    loginModel.loginData.statis.attribute_count = long.Parse(jObject["attribute_count"].ToString());
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError(TAG + e.ToString());
            }
        }
          
        private void OnVideoAdClosed(object obj)
        {
            if (CheckIsEffective()) return;
            ++PreferencesMgr.Instance.VideoEffective_count;
        }  
          
        private bool CheckIsEffective()
        {
            if (PreferencesMgr.Instance.IsLogEffective) return true;

            ++mOnline_time;
            //成为有效用户 服务器验证条件：在线180秒并且视频>=3  
            if (mOnline_time > 180    
                && PreferencesMgr.Instance.VideoEffective_count >= 3)  
            {
                Channel.Current.logEffective();
                PreferencesMgr.Instance.IsLogEffective = true;
                return true; 
            }  

            return false; 
        }
        #endregion
    }
}