/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
using FutureCore;
using FuturePlugin;

namespace ProjectApp
{
    public class WaterMaskUI : BaseUI
    {
        private WaterMaskUICtrl ctrl;
        private UI.CS603_gameWindows.com_waterMask ui;

        private Color netStateColor = Color.white;

        public WaterMaskUI(WaterMaskUICtrl ctrl) : base(ctrl)
        {
            uiName = UIConst.WaterMaskUI;
            this.ctrl = ctrl;
        }

        protected override void SetUIInfo(UIInfo uiInfo)
        {
            uiInfo.packageName = "CS603_gameWindows";
            uiInfo.assetName = "com_waterMask";
            uiInfo.layerType = UILayerType.System;
        }

        #region 生命周期
        protected override void OnInit()
        {
        }

        protected override void OnBind()
        {
            ui = baseUI as UI.CS603_gameWindows.com_waterMask;
        }

        protected override void OnOpenBefore(object args)
        {
        }

        protected override void OnOpen(object args)
        {
            SetCode();
            SetNetState();            
        }

        protected override void OnClose()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnDisplay(object args)
        {
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            AppDispatcher.Instance.AddListener(AppMsg.WebSocketServer_StateUpdate, OnNetStateUpdate);
            //modelDispatcher.AddListener(ModelMsg.XXX, OnXXX);

        }

        protected override void RemoveListener()
        {
            AppDispatcher.Instance.RemoveListener(AppMsg.WebSocketServer_StateUpdate, OnNetStateUpdate);
            //modelDispatcher.RemoveListener(ModelMsg.XXX, OnXXX);
        }
        #endregion

        private void OnNetStateUpdate(object obj)
        {
            if (App.GetIsWeakNetwork())
            {
                SetNetState();
            }
        }

        public void SetCode()
        {
            if (ui == null) return;
            if (ui.text_code == null) return;

            switch (Channel.CurrType)
            {
                case ChannelType.LocalDebug:
                    ui.text_code.text = "TestServer_" + CommonGlobal.Instance.InvitedCode;
                    break;
                case ChannelType.NetCheck:
                case ChannelType.NetRelease:
                    ui.text_code.text = CommonGlobal.Instance.InvitedCode;
                    break;
            }
        }
        /// <summary>
        /// 设置
        /// </summary>
        private void SetNetState()
        {
            if (!NetConst.IsNetAvailable)
            {
                SetColor(Color.red);
                return;
            }

            switch (WSNetMgr.Instance.State)
            {
                case WSNetState.None:
                case WSNetState.NoNetwork:
                case WSNetState.Closed:
                case WSNetState.Exception:
                case WSNetState.LoginFailed_MustDelay:
                case WSNetState.SendLoginError:
                case WSNetState.PreferencesParseError:
                case WSNetState.ConfigParseError:
                case WSNetState.ConfigSerializeError:
                    SetColor(Color.red);
                    return;
                case WSNetState.Connecting:
                case WSNetState.Connected:
                case WSNetState.Logining:
                    SetColor(Color.yellow);
                    return;
                case WSNetState.LoginSuccess:
                    if (AppGlobal.IsLoginSucceed)
                    {
                        SetColor(Color.green);
                        return;
                    }
                    else
                    {
                        SetColor(Color.red);
                        return;
                    }
                default:
                    SetColor(Color.red);
                    return;
            }

            if (netStateColor == Color.white)
            {
                if (AppGlobal.IsLoginSucceed)
                {
                    SetColor(Color.green);
                    return;
                }
                else
                {
                    SetColor(Color.red);
                    return;
                }
            }
        }

        private void SetColor(Color color)
        {
            if (ui == null) return;

            netStateColor = color;
        }
    }
}