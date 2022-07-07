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
    public class InfoCtrl : BaseCtrl
    {
        public static InfoCtrl Instance { get; private set; }
        private LoginModel loginModel;

        private bool isInitResp;

        protected override void OnInit()
        {
            Instance = this;
            loginModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;
        }

        protected override void OnDispose()
        {
            Instance = null;
        }

        protected override void AddListener()
        {
        }

        protected override void RemoveListener()
        {
        }

        protected override void AddServerListener()
        {
            wsNetDispatcher.AddPriorityListener(WSNetMsg.S2C_InvokeInfo, OnS2C_info);
        }


        protected override void RemoveServerListener()
        {
            wsNetDispatcher.RemovePriorityListener(WSNetMsg.S2C_InvokeInfo, OnS2C_info);
        }

        public void SendFirebaseTokenToServer()
        {
            // 发送FirebaseToken给服务器  
            //string token = Channel.Current.getFirebaseToken();
            //C2S_set_personal setPersonalReq = new C2S_set_personal();
            //setPersonalReq.data = new C2S_set_personal_data();
            //setPersonalReq.data.firebase_token = token;
            //setPersonalReq.Send();
        }

        public void InitInfoData(S2C_reg_login loginMsg)
        {
            if (loginMsg != null
                && loginMsg.data != null
                && loginMsg.data.info != null)
            {
                CtrlDispatcher.Instance.Dispatch(CtrlMsg.Module_GiftSwitchChange);
            }

            if (isInitResp)
            {
                isInitResp = false;
                ctrlDispatcher.Dispatch(CtrlMsg.Info_InitComplete);
                ctrlDispatcher.Dispatch(CtrlMsg.Info_Updated);
            }
            else
            {
                ctrlDispatcher.Dispatch(CtrlMsg.Info_Updated);
            }
        }

        private void OnS2C_info(BaseS2CJsonProto respMsg)
        {
            try
            {
                S2C_InvokeInfo infoResp = respMsg as S2C_InvokeInfo;
                if (!string.IsNullOrEmpty(infoResp.err))
                {
                    LogUtil.LogError("[InfoCtrl] OnS2C_info is error, error = " + infoResp.err);
                    return;
                }
                if (infoResp.data == null)
                {
                    LogUtil.LogError("[InfoCtrl] infoResp.data is null,");
                    return;
                }

                if (loginModel.loginData == null)
                {
                    LogUtil.LogError("[InfoCtrl] loginModel.loginData is null.");
                    loginModel.loginData = new S2C_reg_login_data();
                }
                if (loginModel.loginData.info == null)
                {
                    LogUtil.LogError("[InfoCtrl] loginModel.loginData.info is null.");
                    loginModel.loginData.info = new Info();
                }

                JObject jObject = infoResp.data as JObject;
                if (jObject == null)
                {
                    LogUtil.LogError("[InfoCtrl] jObject is null.");
                    return;
                }
                if (jObject["channel"] != null)
                {
                    loginModel.loginData.info.channel = jObject["channel"].ToString();
                }
                if (jObject["invite_code"] != null)
                {
                    loginModel.loginData.info.invite_code = jObject["invite_code"].ToString();
                }
                if (jObject["invite_url"] != null)
                {
                    loginModel.loginData.info.invite_url = jObject["invite_url"].ToString();
                }
                if (jObject["invite_parent"] != null)
                {
                    loginModel.loginData.info.invite_parent = jObject["invite_parent"].ToString();
                }
                if (jObject["attribute_parent"] != null)
                {
                    loginModel.loginData.info.attribute_parent = jObject["attribute_parent"].ToString();
                }
                if (jObject["login_time"] != null)
                {
                    loginModel.loginData.info.login_time = long.Parse(jObject["login_time"].ToString());
                }
                if (jObject["reg_time"] != null)
                {
                    loginModel.loginData.info.reg_time = long.Parse(jObject["reg_time"].ToString());
                }
                if (jObject["reg_country"] != null)
                {
                    loginModel.loginData.info.reg_country = jObject["reg_country"].ToString();
                }
                if (jObject["is_open_exchange"] != null)
                {
                    loginModel.loginData.info.is_open_exchange = bool.Parse(jObject["is_open_exchange"].ToString());
                    CtrlDispatcher.Instance.Dispatch(CtrlMsg.Module_GiftSwitchChange);
                    OnGiftSwitchChange(loginModel.loginData.info.is_open_exchange);
                }
                if (jObject["is_open_sensfunc"] != null)
                {
                    loginModel.loginData.info.is_open_sensfunc = SerializeUtil.ToObject<List<string>>(jObject["is_open_sensfunc"].ToString());
                }
                if (jObject["is_grayscale"] != null)
                {
                    loginModel.loginData.info.is_grayscale = bool.Parse((jObject["is_grayscale"].ToString()));
                }
                if (jObject["logout_time"] != null)
                {
                    loginModel.loginData.info.logout_time = long.Parse((jObject["logout_time"].ToString()));
                }
            }
            catch (System.Exception e)
            {
                LogUtil.LogError(StringUtil.Concat("[InfoCtrl] ", e.ToString()));
            }

            if (isInitResp)
            {
                isInitResp = false;
                ctrlDispatcher.Dispatch(CtrlMsg.Info_InitComplete);
                ctrlDispatcher.Dispatch(CtrlMsg.Info_Updated);
            }
            else
            {
                ctrlDispatcher.Dispatch(CtrlMsg.Info_Updated);
            }
        }

        private void OnGiftSwitchChange(bool is_open_exchange)
        {
            try
            {
                if (!is_open_exchange) return;

                if (loginModel.loginData.pg_setting != null)
                {
                    JObject jobj = SerializeUtil.GetJObjectByObject(loginModel.loginData.pg_setting);
                    if (jobj == null) return;
                    if (jobj["net_opt_v1"] != null)
                        Channel.Current.setNetOptConfig(jobj["net_opt_v1"].ToString(), loginModel.loginData.uid.ToString());
                    else if (jobj["net_opt"] != null)
                        Channel.Current.setNetOptConfig(jobj["net_opt"].ToString(), loginModel.loginData.uid.ToString());
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError("[LoginCtrl] OnGiftSwitchChange Exception: " + e.ToString());
            }
        }
    }
}