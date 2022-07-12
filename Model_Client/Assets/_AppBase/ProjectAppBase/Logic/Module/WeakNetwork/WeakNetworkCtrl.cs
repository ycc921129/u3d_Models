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
            CtrlDispatcher.Instance.AddListener(CtrlMsg.WeakNetworkUI_Click, OnWeakNetworkUI_Click);
        }
        protected override void RemoveListener()
        {
            CtrlDispatcher.Instance.RemoveListener(CtrlMsg.WeakNetworkUI_Click, OnWeakNetworkUI_Click);
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

        public void InitCache()
        {
            InitSaveLoginCache();
            InitSavePreferencesCache();
        }

        private void InitSaveLoginCache()
        {
            if (!App.GetIsWeakNetwork()) return;            
        }

        private void InitSavePreferencesCache()
        {
            if (!App.GetIsWeakNetwork()) return;
            
            Preferences serverPreferences = new Preferences();
            LoginLocalCache.SaveServerPreferencesCache(serverPreferences);
            jCacheServerPreferences = SerializeUtil.GetJObjectByObject(serverPreferences);

            Preferences clientCachePreferences = LoginLocalCache.ReadLocalPreferencesCache();
            if (LoginLocalCache.IsExistLocalPreferencesCache() && clientCachePreferences != null)
            {
                LogUtil.LogFormat("[WeakNetworkCtrl]ServerPreferences.data_ver: {0} | ClientCachePreferences.data_ver: {1}", serverPreferences.data_ver, clientCachePreferences.data_ver);
                if (serverPreferences.data_ver > clientCachePreferences.data_ver)
                {
                    // 使用服务器为最新
                    LogUtil.Log("[WeakNetworkCtrl]使用服务器的Preferences为最新");
                    LoginLocalCache.SaveLocalPreferencesCache(serverPreferences);
                }
                else
                {
                    // 使用客户端为最新, 更新服务器的Preferences
                    LogUtil.Log("[WeakNetworkCtrl]使用客户端的Preferences为最新");
                    JObject jCacheClientPreferences = SerializeUtil.GetJObjectByObject(clientCachePreferences);
                    bool updateRes = ComparisonUpdateServerPreferences(jCacheServerPreferences, jCacheClientPreferences);
                    if (updateRes)
                    {
                        LogUtil.Log("[WeakNetworkCtrl]更新服务器的Preferences");
                        LoginLocalCache.SaveServerPreferencesCache(clientCachePreferences);
                        jCacheServerPreferences = jCacheClientPreferences;
                    }
                }
            }
            else
            {
                // 使用服务器为最新
                LogUtil.Log("[WeakNetworkCtrl]没有本地缓存Preferences, 使用服务器的Preferences为最新");
                LoginLocalCache.SaveLocalPreferencesCache(serverPreferences);
            }
        }

        public void UpdatePreferences()
        {
            Preferences newPreferences = PreferencesMgr.Instance.GetPreferences();
            LoginLocalCache.SaveLocalPreferencesCache(newPreferences);

            UpdateServerPreferences(newPreferences);
        }

        private void UpdateServerPreferences(Preferences newPreferences)
        {
            JObject jNewPreferences = SerializeUtil.GetJObjectByObject(newPreferences);
            bool updateRes = ComparisonUpdateServerPreferences(jCacheServerPreferences, jNewPreferences);
            if (updateRes)
            {
                LogUtil.Log("[WeakNetworkCtrl]更新服务器的Preferences");
                LoginLocalCache.SaveServerPreferencesCache(newPreferences);
                jCacheServerPreferences = jNewPreferences;
            }
        }

        private bool ComparisonUpdateServerPreferences(JObject oldData, JObject newData)
        {
            if (!AppGlobal.IsLoginSucceed) return false;

            //c2s_preferencesMsg.data.set.Clear();
            foreach (JProperty newItem in newData.Children())
            {
                string newKey = newItem.Name;
                JToken newValue = newItem.Value;
                JToken oldValue = oldData.GetValue(newKey);
                if (oldValue != null)
                {
                    if (newValue.Equals(oldValue))
                    {
                        continue;
                    }
                    else if (newValue.ToString(Formatting.None).Equals(oldValue.ToString(Formatting.None)))
                    {
                        continue;
                    }
                    else
                    {
                        // 修改
                        //c2s_preferencesMsg.data.set.Add(newKey, newValue);
                    }
                }
                else
                {
                    // 新增
                    //c2s_preferencesMsg.data.set.Add(newKey, newValue);
                }
            }
            return true;
        }

        public bool IsCanOfflineLogin()
        {
            //TODO 离线游戏
            return true; 

            bool isCanOfflineLogin = false;

            // 不启用弱联网
            if (!App.GetIsOfflineGame())
            {
                return isCanOfflineLogin;
            }
            // 是否已经登录服务器
            if (AppGlobal.IsLoginSucceed)
            {
                return isCanOfflineLogin;
            }
            // 是否已经处于弱联网模式
            if (IsInWeakNetworkMode)
            {
                isCanOfflineLogin = true;
                return isCanOfflineLogin;
            }            
            
            return isCanOfflineLogin;
        }

        public bool IsCanOfflineMode()
        {
            isCanOfflineMode = false;
                      
            // 是否已经处于弱联网模式
            if (IsInWeakNetworkMode)
            {
                isCanOfflineMode = true;
                return isCanOfflineMode;
            }
            
            return isCanOfflineMode;
        }

        public void SendOfflineLoginReq()
        {
            LoginCtrl.Instance.SendOfflineLoginReq();
        }

        public bool IsCanShowReconnectUI()
        {
            //TODO 弱联网
             return false;

            if (isNeedNetLogin)
            {
                return true;
            }
            if (!isCanOfflineMode)
            {
                return true;
            }
            return false;
        }         

        /// <summary>
        /// 开启保持网络在线
        /// </summary>
        public void EnableHoldNetOnline()
        {
            holdNetOnlineRefCount++;
            if (holdNetOnlineRefCount > 0)
            {
                isNeedNetLogin = true;         
            }
        }

        /// <summary>
        /// 结束保持网络在线
        /// </summary>
        public void EndHoldNetOnline()
        {
            holdNetOnlineRefCount--;
            if (holdNetOnlineRefCount <= 0)
            {
                holdNetOnlineRefCount = 0;
                isNeedNetLogin = false;
            }
        }        

        /// <summary>
        /// 联网通知UI点击
        /// </summary>
        private void OnWeakNetworkUI_Click(object param)
        {
            bool netWorkOnline = (bool)param;
            if (netWorkOnline)
            {

            }
        }
    }
}