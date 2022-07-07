/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using FairyGUI;
using FuturePlugin;
using System;
using Spine.Unity;
using Spine;

namespace ProjectApp
{
    public class LoadingUI : BaseUI
    {
        #region 注册需要的UI对象
        protected GTextField text_severStatus;
        protected GProgressBar pb_loading;
        #endregion

        //UI逻辑部分
        protected LoadingUICtrl ctrl;
        protected UI.CS608_loading.com_loading ui;

        protected int currValue;

        protected bool isUseSpine = false;
        protected string spinePath = "Prefab/loadingUISpine";
        protected string spineAnimName = "default";
        protected GGraph spineGGraph = null;

        public LoadingUI(LoadingUICtrl ctrl) : base(ctrl)
        {
            uiName = UIConst.LoadingUI;
            this.ctrl = ctrl;
        }

        protected override void SetUIInfo(UIInfo uiInfo)
        {
            uiInfo.packageName = "CS608_loading";
            uiInfo.assetName = "com_loading";
            uiInfo.layerType = UILayerType.Loading;
            uiInfo.isNeedOpenAnim = false;
            uiInfo.isNeedCloseAnim = false;
        }

        #region 生命周期
        protected override void OnInit()
        {
        }

        protected override void OnBind()
        {
            ui = baseUI as UI.CS608_loading.com_loading;
        }

        protected override void OnOpenBefore(object args)
        {
            ui.text_loading.text = "";
        }

        protected override void OnOpen(object args)
        {
            pb_loading.value = 0;

            if (isUseSpine && spineGGraph != null)
            {
                SkeletonAnimation spine = ResMgr.Instance.SyncLoadGameObject(spinePath).GetComponent<SkeletonAnimation>();
                if (AppConst.IsMultiLangue)
                {
                    if (spine != null)
                    {
                        string langCode = AppConst.CurrMultiLangue;
                        if (langCode.Contains("@"))
                        {
                            langCode = langCode.Replace("@", string.Empty);
                        }
                        Skin skin = spine.skeleton.Data.FindSkin(langCode);
                        if (skin != null)
                        {
                            spine.skeleton.SetSkin(skin);
                        }
                    }
                }
                FGUIHelper.ShowLoopSpineObject(spineGGraph, spine, spineAnimName);
            }
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
        }

        protected override void RemoveListener()
        {
        }
        #endregion

        #region Sever Status
        public void SetSeverStatus()
        {
            if (this.text_severStatus == null) return;

            switch (Channel.CurrType)
            {
                case ChannelType.LocalDebug:
                    this.ui.text_severStatus.text = "TestServer";
                    break;
                case ChannelType.NetCheck:
                case ChannelType.NetRelease:
                    this.ui.text_severStatus.text = string.Empty;
                    break;
            }
        }
        #endregion

        #region Loading Value
        public void SetLoadingValue(int value, float duration, Action _callback = null)
        {
            if (pb_loading == null) return;

            if (value <= currValue || value <= pb_loading.value)
            {
                if (_callback != null)
                {
                    _callback.Invoke();
                }
                return;
            }

            currValue = value;
            pb_loading.TweenValue(currValue, duration).OnComplete(() =>
            {
                // 暂不使用连贯分离逻辑载入
                //pb_loading.TweenValue(100, 0.1f);
                
                if (_callback != null)
                {
                    _callback.Invoke();
                }
            });
        }
        #endregion
    }
}