/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using Beebyte.Obfuscator;
using FutureCore;
using ProjectApp.Protocol;

namespace ProjectApp
{
    [Skip]
    public class EventCommonData
    {
        public string event_name;
        public Dictionary<string, object> properties;

        public EventCommonData()
        {
            event_name = "";
            properties = new Dictionary<string, object>();
        }

        public void Init(string _event_name, Dictionary<string, object> _properties)
        {
            event_name = _event_name;
            properties = _properties;
        }
    }

    public class UserSessionCtrl : BaseCtrl
    {
        public static UserSessionCtrl Instance { get; private set; }
        private static bool IsUseSdkApi = true;

        private float statusTime = 2.0f;
        private float eventsTime = 2.0f;
        private Dictionary<string, string> statusDict = new Dictionary<string, string>();
        private Dictionary<string, int> eventsDict = new Dictionary<string, int>();
        private TimerTask statusTimer = null;
        private TimerTask eventsTimer = null;
        private List<UserStatus> statusList = new List<UserStatus>();
        private List<UserEvent> eventsList = new List<UserEvent>();
        private ObjectPool<UserStatus> userStatusPool = new ObjectPool<UserStatus>();
        private ObjectPool<UserEvent> userEventPool = new ObjectPool<UserEvent>();
        private C2S_user_status c2s_user_Status = new C2S_user_status();
        private C2S_user_event c2S_user_Event = new C2S_user_event(); 
        private LoginModel loginModel = null;

        private EventCommonData eventCommonData = new EventCommonData();

        #region 生命周期
        protected override void OnInit()
        {
            Instance = this;
        }

        protected override void OnDispose()
        {
            //Instance = null;

            if (statusTimer != null)
            {
                statusTimer.Dispose();
            }
            if (eventsTimer != null)
            {
                eventsTimer.Dispose();
            }
            OnImmediateSend();
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            CtrlDispatcher.Instance.AddListener(CtrlMsg.Game_Start, OnGameStart);
            AppDispatcher.Instance.AddListener(AppMsg.App_Pause_True, OnImmediateSend);
        }
        protected override void RemoveListener()
        {
            CtrlDispatcher.Instance.RemoveListener(CtrlMsg.Game_Start, OnGameStart);
            AppDispatcher.Instance.RemoveListener(AppMsg.App_Pause_True, OnImmediateSend);
        }

        protected override void AddServerListener()
        {
        }
        protected override void RemoveServerListener()
        {
        }
        #endregion

        #region API
        /// <summary>
        /// 统计用户事件
        /// </summary>
        public void StatisticEvent(string key)
        {
            if (IsUseSdkApi)
            {
                Channel.Current.logUserEvent(key, 1);
            }
            else
            {
                if (eventsDict.ContainsKey(key))
                {
                    int times = eventsDict[key];
                    eventsDict[key] = ++times;
                }
                else
                {
                    eventsDict.Add(key, 1);
                }
            }
        }

        /// <summary>
        /// 统计用户状态
        /// </summary>
        public void StatisticState(string key, string value)
        {
            if (IsUseSdkApi)
            {
                Channel.Current.logUserStatus(key, value);
            }
            else
            {
                if (statusDict.ContainsKey(key))
                {
                    statusDict[key] = value;
                }
                else
                {
                    statusDict.Add(key, value);
                }
            }
        }

        /// <summary>
        /// 统计用户事件
        /// </summary>
        public void StatisticState(string key, int value)
        {
            StatisticState(key, value.ToString());
        }

        /// <summary>
        /// 通用统计
        /// </summary>
        public void StatisticNormal(string key, string value)
        {
            Channel.Current.logNormal(key, value);
        }

        /// <summary>
        /// 通用统计
        /// </summary>
        public void StatisticNormalDict(string key, Dictionary<string, object> dict)
        {
            Channel.Current.logNormalForJson(key, SerializeUtil.ToJson<Dictionary<string, object>>(dict));
        } 

        /// <summary>
        /// 通用统计：BI后台通用事件统计接口(通用事件只能用这个接口)
        /// </summary>
        /// <param name="_event_name">事件名</param>
        /// <param name="_properties">属性</param>
        public void StatisticCommonEvent(string _event_name, Dictionary<string, object> _properties)
        {
            eventCommonData.Init(_event_name, _properties);
            Channel.Current.logNormalForJson("newbyear_common_event", SerializeUtil.ToJson(eventCommonData));
        }

        /// <summary>
        /// 通用统计
        /// </summary>
        public void StatisticNormalTDict<T>(string key, Dictionary<string, T> dict)
        {
            Channel.Current.logNormalForJson(key, SerializeUtil.ToJson<Dictionary<string, T>>(dict));
        }        
        #endregion 

        private void OnGameStart(object obj)
        {
            if (IsUseSdkApi) return;

            statusTimer = TimerUtil.General.AddLoopTimer(statusTime, OnStatusTimer);
            eventsTimer = TimerUtil.General.AddLoopTimer(eventsTime, OnEventsTimer);

            loginModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;
            OnInitLogic();
        }

        private void OnInitLogic()
        {
            ReadCacheUserSessionData();
            if (AppGlobal.IsLoginSucceed)
            {
                SendStatus();
                SendEvents();
            }
        }

        private void OnImmediateSend(object obj = null)
        {
            if (IsUseSdkApi) return;

            if (AppGlobal.IsLoginSucceed)
            {
                if (!SendStatus())
                {
                    CacheUserStatusData();
                }
                if (!SendEvents())
                {
                    CacheUserEventsData();
                }
            }
            else
            {
                CacheUserStatusData();
                CacheUserEventsData();
            }
        }

        private void OnStatusTimer(TimerTask obj)
        {
            if (!AppGlobal.IsLoginSucceed) return;

            SendStatus();
        }

        private void OnEventsTimer(TimerTask obj)
        {
            if (!AppGlobal.IsLoginSucceed) return;

            SendEvents();
        }

        private bool SendStatus()
        {
            return true;
        }

        private bool SendEvents()
        {
            return true;
        }

        private void ReadCacheUserSessionData()
        {
            if (PrefsUtil.HasKey(PrefsKeyConst.UserSessionCtrl_status))
            {
                statusDict = PrefsUtil.ReadObject<Dictionary<string, string>>(PrefsKeyConst.UserSessionCtrl_status) as Dictionary<string, string>;
            }
            if (PrefsUtil.HasKey(PrefsKeyConst.UserSessionCtrl_events))
            {
                eventsDict = PrefsUtil.ReadObject<Dictionary<string, int>>(PrefsKeyConst.UserSessionCtrl_events) as Dictionary<string, int>;
            }
        }

        private void CacheUserEventsData()
        {
            if (statusDict.Count > 0)
            {
                PrefsUtil.WriteObject(PrefsKeyConst.UserSessionCtrl_status, statusDict);
            }
        }

        private void CacheUserStatusData()
        {
            if (eventsDict.Count > 0)
            {
                PrefsUtil.WriteObject(PrefsKeyConst.UserSessionCtrl_events, eventsDict);
            }
        }
    }
}