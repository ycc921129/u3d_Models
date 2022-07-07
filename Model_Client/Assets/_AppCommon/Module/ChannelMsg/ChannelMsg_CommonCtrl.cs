/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using FutureCore;
using FutureCore.Data;
using ProjectApp.Protocol;

namespace ProjectApp
{
    /// <summary>
    /// 通用逻辑中，对SDK的回调消息做统一的处理
    /// </summary>
    public class ChannelMsg_CommonCtrl : BaseCtrl
    {
        #region 生命周期
        protected override void OnInit()
        {

        }

        protected override void OnDispose()
        {
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            ChannelDispatcher.Instance.AddListener(ChannelMsg.OnLoginBindToken, LoginBindToken);

            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnAccountKitMobile, BindMobile);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnVideoAdRewarded, PlayVedio);
            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.ShareSuccess, OnShareSuccess);          
        }

        protected override void RemoveListener()
        {
            ChannelDispatcher.Instance.RemoveListener(ChannelMsg.OnLoginBindToken, LoginBindToken);

            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnAccountKitMobile, BindMobile);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnVideoAdRewarded, PlayVedio);
            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.ShareSuccess, OnShareSuccess);            
        }

        protected override void AddServerListener()
        {
        }

        protected override void RemoveServerListener()
        {

        }
        #endregion

        #region Sever
        public void RequsetC2S_set_personal()
        {
            //ODO FirebaseToken
            //C2S_set_personal msg = new C2S_set_personal();
            //msg.data = new C2S_set_personal_data();    
            //msg.SetData(infoModel.infoData.personal);
            //WSNetMgr.Instance.Send(msg);  
        }
        #endregion

        #region Event
        /// <summary>
        /// 绑定登陆信息
        /// </summary>
        /// <param name="obj"></param>
        public void LoginBindToken(object obj)
        {
            FirebaseUserInfo firebaseUserInfo = (FirebaseUserInfo)obj;
            LogUtil.Log("登陆绑定的Token   " + firebaseUserInfo);
            if (firebaseUserInfo.isSuccess)
            {
                // 修改 登陆的手机号信息不做保存，但是需要保留之前绑定过的手机号
                //TODO 绑定登录信息
                //string mob = infoModel.infoData.personal.mobile;
                //infoModel.infoData.personal = MapperUtil.MappingObject<Personal, FirebaseUserInfo>(firebaseUserInfo);
                //infoModel.infoData.personal.mobile = mob;
                RequsetC2S_set_personal();  
                ctrlDispatcher.Dispatch(CtrlMsg.BindLoginTokenFinish); 
            }
        }

        /// <summary>
        /// 监听视频播放
        /// </summary>
        /// <param name="args"></param>
        public void PlayVedio(object args = null)
        {
            //TaskAchievementGlobal.Instance.SaveTaskOffset(PreferencesTaskConst.Watch_N_videos);
        }

        /// <summary>
        /// 分享成功
        /// </summary>
        /// <param name="args"></param>
        public void OnShareSuccess(object args = null)
        {
            //TaskAchievementGlobal.Instance.SaveTaskOffset(PreferencesTaskConst.Have_N_share);
        }

        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <param name="mobile"></param>
        public void BindMobile(object mobile)
        {
            string mob = (string)mobile;
            if (!string.IsNullOrEmpty(mob))
            {
                //TODO 绑定手机号
                //infoModel.infoData.personal.mobile = (string)mobile;
                //RequsetC2S_set_personal();
                //ctrlDispatcher.Dispatch(CtrlMsg.BindMobileFinish);
            }
        }
        #endregion
    }
}