/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FutureCore;
using ProjectApp.Data;
using ProjectApp.Protocol;

namespace ProjectApp
{
    public class WeakNetworkCtrl : BaseCtrl
    {
        public static WeakNetworkCtrl Instance { get; private set; }

        private bool isCanOfflineMode;
        private bool isInWeakNetworkMode;
        private bool isNeedNetLogin;

        private LoginModel loginModel;
        private JObject jCacheServerPreferences;

        /// <summary>
        /// 保持网络在线引用计数
        /// </summary>
        private int holdNetOnlineRefCount = 0;

        protected override void OnInit()
        {
            Instance = this;
            loginModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;          
        }
        protected override void OnDispose()
        {
            //Instance = null;
        }

        protected override void AddListener()
        {
        }
        protected override void RemoveListener()
        {
        }

        protected override void AddServerListener()
        {
        }
        protected override void RemoveServerListener()
        {
        }

        /// <summary>
        /// 是否处于弱联网模式
        /// </summary>
        public bool IsInWeakNetworkMode
        {
            get
            {
                return isInWeakNetworkMode;
            }
            set
            {
                isInWeakNetworkMode = value;
                LogUtil.Log("[WeakNetworkCtrl]IsInWeakNetworkMode: " + isInWeakNetworkMode);
            }
        }       

        public void UpdatePreferences()
        {
            Preferences newPreferences = PreferencesMgr.Instance.GetPreferences();
            LoginLocalCache.SaveLocalPreferencesCache(newPreferences);            
        }   

        public bool IsCanOfflineLogin()
        {
            return true;             
        }

        public bool IsCanOfflineMode()
        {
            return true;
        }

        public void SendOfflineLoginReq()
        {
            LoginCtrl.Instance.SendOfflineLoginReq();
        }

        public bool IsCanShowReconnectUI()
        {
             return false;            
        }          
    }
}