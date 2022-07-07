 /****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using FairyGUI;

namespace FutureCore
{
    public abstract class BaseUI
    {
        #region Field
        protected UIMgr uiMgr;
        protected ModuleMgr moduleMgr;

        protected ModelDispatcher modelDispatcher;
        protected ViewDispatcher viewDispatcher;
        protected CtrlDispatcher ctrlDispatcher;
        protected UICtrlDispatcher uiCtrlDispatcher;
        protected DataDispatcher dataDispatcher;
        protected GameDispatcher gameDispatcher;
        protected WSNetDispatcher wsNetDispatcher;

        public string uiName;
        public string rawGameObjectName;
        public string gameObjectName;
        public UIInfo uiInfo;

        public uint uiOpenCumsumId;
        public int currLayer;
        public BaseUICtrl baseUICtrl;
        public object uiArgs;

        public GObject baseGObj;
        public GComponent baseUI;
        public Window windowUI;
        public GGraph uiMask;
        public List<SubUI> subUIs;

        public bool isOpen;
        public bool isVisible;
        public bool isClose;

        public GTweener openUiGTweener;
        public GTweener closeUiGTweener;

        private DynamicAssetLoader m_loader;
        protected DynamicAssetLoader loader
        {
            get
            {
                if (m_loader == null)
                {
                    m_loader = new DynamicAssetLoader();
                }
                return m_loader;
            }
        }
        #endregion

        #region Constructor
        public BaseUI()
        {
        }

        public BaseUI(BaseUICtrl baseUICtrl)
        {
            New(baseUICtrl);
        }

        public void New(BaseUICtrl baseUICtrl)
        {
            this.baseUICtrl = baseUICtrl;

            Assignment();
            OnNew();
            Process_Init();
        }

        protected virtual void Assignment()
        {
            uiMgr = UIMgr.Instance;
            moduleMgr = ModuleMgr.Instance;

            modelDispatcher = ModelDispatcher.Instance;
            viewDispatcher = ViewDispatcher.Instance;
            ctrlDispatcher = CtrlDispatcher.Instance;
            uiCtrlDispatcher = UICtrlDispatcher.Instance;
            dataDispatcher = DataDispatcher.Instance;
            gameDispatcher = GameDispatcher.Instance;
            wsNetDispatcher = WSNetDispatcher.Instance;
        }
        protected virtual void UnAssignment()
        {
            uiMgr = null;
            moduleMgr = null;

            modelDispatcher = null;
            viewDispatcher = null;
            ctrlDispatcher = null;
            uiCtrlDispatcher = null;
            dataDispatcher = null;
            gameDispatcher = null;
            wsNetDispatcher = null;
        }
        #endregion

        #region Interface: UI
        public void Open(object args = null)
        {
            uiArgs = args;
            uiMgr.Internal_OpenUI(this, args);
        }

        public void OpenUISequence(object args = null)
        {
            uiArgs = args;
            uiMgr.Internal_OpenUISequence(this, args);
        }

        public void Close()
        {
            if (isClose) return;
            uiMgr.Internal_CloseUI(this);
        }

        public void Hide()
        {
            uiMgr.Internal_HideUI(this);
        }

        public void Display(object args = null)
        {
            uiArgs = args;
            uiMgr.Internal_DisplayUI(this, args);
        }
        #endregion

        #region Interface: SubUI
        protected SubUI OpenSubUI(string subUiName, string assetName, bool needStandardResolution = false)
        {
            return OpenSubUI(subUiName, uiInfo.packageName, assetName, needStandardResolution);
        }

        protected SubUI OpenSubUI(string subUiName, string packageName, string assetName, bool needStandardResolution = false)
        {
            return uiMgr.OpenSubUI(this, subUiName, packageName, assetName, needStandardResolution);
        }

        protected void CloseSubUI(SubUI subUI)
        {
            uiMgr.CloseSubUI(this, subUI);
        }

        protected void CloseAllSubUI()
        {
            uiMgr.CloseAllSubUI(this);
        }
        #endregion

        #region Process
        private void Process_Init()
        {
            isOpen = false;
            isVisible = false;
            isClose = false;

            uiInfo = new UIInfo();

            SetUIInfo(uiInfo);
            PostProcess_UIInfo();
            OnInit();
        }
        private void PostProcess_UIInfo()
        {
        }

        public void Process_Bind()
        {
            OnBind();
        }
        public void Process_OpenBefore(object args)
        {
            OnOpenBefore(args);
        }
        public void Process_Open(object args)
        {
            OnOpen(args);
            AddListener();
            AddServerListener();

            isOpen = true;
        }
        public void Process_OpenUIAnimEnd()
        {
            OnOpenUIAnimEnd();
        }

        public void Process_Close()
        {
            RemoveListener();
            RemoveServerListener();
            OnClose();

            isClose = true;
        }
        public void Process_CloseUIAnimEnd()
        {
            OnCloseUIAnimEnd();
        }
        public void Process_Destroy()
        {
            if (m_loader != null)
            {
                m_loader.Release();
                m_loader.Dispose();
                m_loader = null;
            }

            OnDestroy();
            UnAssignment();

            isOpen = false;
            isVisible = false;
            isClose = true;

            baseGObj = null;
            baseUI = null;
            windowUI = null;
        }

        public void Process_Hide()
        {
            isVisible = false;
            baseUI.visible = isVisible;
            OnHide();
        }
        public void Process_Display(object args)
        {
            isVisible = true;
            baseUI.visible = isVisible;
            OnDisplay(args);
        }

        public void ProcessFunc_SwitchLanguage()
        {
            if (isClose) return;
            if (baseUI == null) return;
            if (baseUI.isDisposed) return;

            InternaProcesslFunc_GComponentSwitchLanguage(baseUI);
            OnSwitchLanguage();
        }
        private void InternaProcesslFunc_GComponentSwitchLanguage(GComponent switchCom)
        {
            if (switchCom == null) return;
            if (switchCom.isDisposed) return;

            for (int i = 0; i < switchCom.GetChildrenCount(); i++)
            {
                GObject gObject = switchCom.GetChildAt(i);
                if (gObject == null || gObject.isDisposed) continue;

                GComponent childCom = gObject.asCom;
                if (childCom != null)
                {
                    InternaProcesslFunc_GComponentSwitchLanguage(childCom);
                    continue;
                }
                else
                {
                    if (gObject.packageItem != null) continue;
                    if (gObject.parent == null) continue;

                    string text = null;
                    if (gObject is GTextField)
                    {
                        GTextField gTextField = gObject.asTextField;
                        if (gTextField == null) continue;
                        if (!gTextField.Ex_IsAutoMultiLang) continue;

                        text = gObject.Ex_GetMultiLangText();
                        if (text != null)
                        {
                            gTextField.text = text;
                            continue;
                        }
                    }
                }
            }
        }
        #endregion

        #region Virtual Logic
        protected virtual void OnNew() { }
        protected abstract void SetUIInfo(UIInfo uiInfo);
        protected virtual void OnInit() { }

        protected virtual void OnBind() { }
        protected virtual void OnOpenBefore(object args) { }
        protected virtual void OnOpen(object args) { }
        protected virtual void OnOpenUIAnimEnd() { }

        protected virtual void OnClose() { }
        protected virtual void OnCloseUIAnimEnd() { }
        protected virtual void OnDestroy() { }

        protected virtual void OnHide() { }
        protected virtual void OnDisplay(object args) { }

        public virtual void OnUpdate() { }
        public virtual void OnSwitchLanguage() { }

        protected virtual void AddListener() { }
        protected virtual void RemoveListener() { }
        protected virtual void AddServerListener() { }
        protected virtual void RemoveServerListener() { }
        #endregion

        #region Event
        protected void AutoBindButtonEvent(EventCallback1 callback)
        {
            AutoBindButtonEvent(baseUI, callback);
        }

        protected void AutoBindButtonEvent(GComponent gComponent, EventCallback1 callback)
        {
            GObject[] gObjects = gComponent.GetChildren();
            for (int i = 0; i < gObjects.Length; i++)
            {
                GObject gObject = gObjects[i];
                if (gObject.asButton != null)
                {
                    gObject.onClick.Remove(callback);
                    gObject.onClick.Add(callback);
                    continue;
                }
                GComponent otherGComponent = gObject.asCom;
                if (otherGComponent != null)
                {
                    AutoBindButtonEvent(otherGComponent, callback);
                    continue;
                }
            }
        }

        protected void BaseUIClickEvent(GComponent gComponent)
        {
        }
        #endregion

        #region Anim

        public void KillOpenUIAnim()
        {
            if (openUiGTweener != null)
            {
                if (!openUiGTweener.allCompleted)
                {
                    openUiGTweener.Kill(complete: false);
                }
                openUiGTweener = null;
            }
        }

        public void KillCloseUIAnim()
        {
            if (closeUiGTweener != null)
            {
                if (!closeUiGTweener.allCompleted)
                {
                    closeUiGTweener.Kill(complete: false);
                }
                closeUiGTweener = null;
            }
        }

        #endregion

        #region Func
        public uint GetOpenUIMsgId()
        {
            if (uiInfo.openUIMsgId == 0)
            {
                if (baseUICtrl == null) return 0;

                uiInfo.openUIMsgId = baseUICtrl.GetOpenUIMsg(uiName);
            }
            return uiInfo.openUIMsgId;
        }

        public uint GetCloseUIMsgId()
        {
            if (uiInfo.closeUIMsgId == 0)
            {
                if (baseUICtrl == null) return 0;

                uiInfo.closeUIMsgId = baseUICtrl.GetCloseUIMsg(uiName);
            }
            return uiInfo.closeUIMsgId;
        }

        public void CtrlCloseUI()
        {
            if (baseUICtrl == null) return;

            baseUICtrl.DispatchCloseUI(uiName);
        }

        public UILayerType GetCurrRenderLayer()
        {
            string uiLayer = baseUI.parent.name;
            switch (uiLayer)
            {
                case UILayerConst.Background:
                    return UILayerType.Background;
                case UILayerConst.Bottom:
                    return UILayerType.Bottom;

                case UILayerConst.Normal:
                    return UILayerType.Normal;
                case UILayerConst.Top:
                    return UILayerType.Top;

                case UILayerConst.FullScreen:
                    return UILayerType.FullScreen;
                case UILayerConst.Popup:
                    return UILayerType.Popup;

                case UILayerConst.Highest:
                    return UILayerType.Highest;
                case UILayerConst.Animation:
                    return UILayerType.Animation;
                case UILayerConst.Tips:
                    return UILayerType.Tips;
                case UILayerConst.Loading:
                    return UILayerType.Loading;
                case UILayerConst.System:
                    return UILayerType.System;

                default:
                    return UILayerType.None;
            }
        }

        public int GetCurrRenderQueueIdx()
        {
            return baseUI.parent.GetChildIndex(baseUI);
        }
        #endregion
    }
}