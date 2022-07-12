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
        }

        protected override void RemoveListener()
        {
            ctrlDispatcher.RemoveListener(CtrlMsg.Preferences_InitComplete, PreferenceDataReady);  
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
            //记录游戏启动次数
            PreferencesMgr.Instance.GameStartCount++;
        } 
    }
}