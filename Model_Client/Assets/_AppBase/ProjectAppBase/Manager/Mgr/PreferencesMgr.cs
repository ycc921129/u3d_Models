/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using CodeStage.AntiCheat.ObscuredTypes;
using FutureCore;
using Newtonsoft.Json.Linq;
using ProjectApp.Data;
using ProjectApp.Protocol;
using System.Collections;
using System.Collections.Generic;

namespace ProjectApp
{
    public sealed partial class PreferencesMgr : BaseMgr<PreferencesMgr>
    {
        public ObscuredInt credit;
        private Preferences preferences;
        private DataDispatcher dataDispatcher;

        private C2S_preferences c2s_preferencesMsg;

        private ObjectPool<KeyValue> keyValuePool;
        private List<KeyValue> autoSaveList;

        private TimerTask autoSaveTimer;  

        private CommonMsgData ppCardChangeMsgData = new CommonMsgData(CtrlCommonMsg.PPCardChange);

        public override void Init()
        {  
            base.Init();
            AddListener();              

            c2s_preferencesMsg = new C2S_preferences();
            c2s_preferencesMsg.data = new C2S_preferences_data();

            keyValuePool = new ObjectPool<KeyValue>();
            autoSaveList = new List<KeyValue>();
        }
           
        public override void StartUp()
        {
            base.StartUp();
            
            dataDispatcher = DataDispatcher.Instance;
            autoSaveTimer = TimerUtil.General.AddLoopTimer(PreferencesConst.AutoSaveTimeInterval, OnAutoDelaySave);   
        }

        public override void DisposeBefore()
        {
            base.DisposeBefore();
            ImmediateSendSave();
        }

        public override void Dispose()
        {
            base.Dispose();
            RemoveListener();

            autoSaveTimer.Dispose(); 
            keyValuePool.Dispose();
        }

        private void AddListener()
        {
            AppDispatcher.Instance.AddListener(AppMsg.App_Pause_True, ImmediateSendSave);
        }

        private void RemoveListener()
        {
            AppDispatcher.Instance.RemoveListener(AppMsg.App_Pause_True, ImmediateSendSave);
        }

        public void InitPreferences()
        {  
            LoginModel loginModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel; 
            
            preferences = loginModel.loginData.pref;    
            if (preferences == null)
            {
                preferences = new Preferences();
            }
            OnInitPreferences();  

            // 登录漏斗统计5
            LoginStatistics.AddFunnelData_preferencesinitcomplete_5();
        }

        private void OnAutoDelaySave(TimerTask timerInfo)
        {
            AutoSaveList(autoSaveList);
            PreferencesSendSave();
        }

        private void AutoSaveList(List<KeyValue> autolist)
        {
            if (autolist == null || autolist.Count == 0) return;
            foreach (KeyValue item in autolist)
            {
                Save(item.key, item.value);
                keyValuePool.Release(item);
            }
            autolist.Clear();
        }

        private void AddDataVer()
        {
            ++Data_ver;
        }

        /// <summary>
        /// 立即发送保存
        /// </summary>
        public void ImmediateSendSave(object arg = null)
        {
            AutoSaveList(autoSaveList);
            PreferencesSendSave();    
        }
        #region 远程存储

        #region 保存方法        
        private void AddToAutoDelaySaveList(string key, object value)
        {
            KeyValue item = keyValuePool.Get();
            item.key = key;
            item.value = value;
            autoSaveList.Add(item);
        }        

        private void Save<T>(string key, T data)
        {
            Dictionary<string, object> dic = c2s_preferencesMsg.data.set;
            if (!dic.ContainsKey(key))
            {
                dic.Add(key, data);
            }
            else
            {
                dic[key] = data;
            }
        }

        #endregion 保存方法
        private void ClearPreferencesDic() 
        {
            c2s_preferencesMsg.data.set.Clear();
        }

        private void PreferencesSendSave()
        {
            if (c2s_preferencesMsg == null || c2s_preferencesMsg.data.set.Count == 0) return;

            AddDataVer();
            if (WSNetMgr.Instance != null && !WSNetMgr.Instance.isConnected)
            {  
                WeakNetworkCtrl.Instance.UpdatePreferences();
                ClearPreferencesDic();
            }
            else
            {
                bool isSendSucceed = WSNetMgr.Instance.Send(c2s_preferencesMsg);
                if (isSendSucceed)
                {
                    ClearPreferencesDic();
                }
            }
        }

        #endregion 远程存储        

        #region 封装
        public Preferences GetPreferences()
        {
            return preferences;
        }
        #endregion 封装
    }
}