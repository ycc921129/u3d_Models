/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using FutureCore;
using ProjectApp.Protocol;
using UnityEngine;

namespace ProjectApp
{
    /// <summary>
    /// preferences 数据准备类
    /// </summary>
    public class PreferencesDataReadyCtrl : BaseCtrl
    {
        LoginModel userModel;
        private List<uint> msgList = new List<uint>(); //做一个消息的缓存队列

        #region 生命周期
        protected override void OnInit()
        {
            userModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;
        }

        protected override void OnDispose()
        {        
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            ctrlDispatcher.AddListener(CtrlMsg.Preferences_InitComplete, PreferenceDataReady);
            ctrlDispatcher.AddListener(CtrlMsg.Game_StartBefore, SendCtrlMsg);
        }

        protected override void RemoveListener()
        {
            ctrlDispatcher.RemoveListener(CtrlMsg.Preferences_InitComplete, PreferenceDataReady);
            ctrlDispatcher.RemoveListener(CtrlMsg.Game_StartBefore, SendCtrlMsg);  
        }

        protected override void AddServerListener()
        {
        }

        protected override void RemoveServerListener()
        {
        }
        #endregion
        /// <summary>
        /// jian cha date
        /// </summary>
        public void PreferenceDataReady(object objs = null)
        {
            InspectionNewDay();

            //记录游戏启动次数
            PreferencesMgr.Instance.GameStartCount++;
        } 

        /// <summary>
        /// 处理日期检测
        /// </summary>
        public void InspectionNewDay()  
        {
            DateTime data = DateTimeMgr.Instance.GetDateTime(userModel.loginData.info.login_time);
            string dateStr = DateTimeMgr.Instance.DateTimeToYYYYMMDD(data);
            if (!PreferencesMgr.Instance.Date.Equals(dateStr))
            {
                PreferencesMgr.Instance.Date = dateStr;
                msgList.Add(CtrlMsg.NewDays);
                PreferencesMgr.Instance.LoginGameTodayTimes = 1;
                CommonConst.RaffleRedPiont = true;
            }
            else
            {
                PreferencesMgr.Instance.LoginGameTodayTimes++;
            }
            if (userModel.loginData == null) return;
            if (userModel.loginData.statis.online_day != PreferencesMgr.Instance.LastLoginDays) //服务器新的一天
            {
                PreferencesMgr.Instance.LastLoginDays = userModel.loginData.statis.online_day;  
                msgList.Add(CtrlMsg.ServerNewDays);
                //立刻派发出 清理每日Preferences的消息
                ctrlDispatcher.Dispatch(CtrlMsg.DisposeDailyPerferences);
            }
        }

        /// <summary>
        /// 派发数据消息
        /// </summary>
        public void SendCtrlMsg(object args = null)
        {
            for (int i = 0; i < msgList.Count; i++)
            {
                ctrlDispatcher.Dispatch(msgList[i]);
            }
            msgList.Clear();
        }
    }
}