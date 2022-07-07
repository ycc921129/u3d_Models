/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.12.19
*/

using FairyGUI;
using FuturePlugin;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FutureCore
{
    public class UISequenceInfo
    {
        public BaseUI ui;
        public object args;
        public bool isLaunch;
    }

    public class UIParentInfo
    {
        public GComponent parent;
        public int index;
    }

    public static class UIMgrConst
    {
        public static bool IsEnableOpenUIAnim = true;
        public static bool IsEnableCloseUIAnim = true;
        public static Vector2 OpenUIAnimEffectScale = new Vector2(0.65f, 0.65f);
        public static float OpenUIAnimEffectTime = 0.3f;
        public static float ClickDownAnimEffectScale = 0.9f;
    }

    public sealed class UIMgr : BaseMonoMgr<UIMgr>
    {
        #region Filed
        private const bool IsSyncLoadMode = true;
        private const bool IsUseSafeAreaAdaptive = false;
        private const bool IsSetButtonPivotCenter = true;

        private GameObject eventSystemGo;
        private EventSystem eventSystem;
        private StandaloneInputModule inputModule;

        private string uiDefaultFontName;
        private List<string> commonPackageList = new List<string>();
        private Queue<GGraph> uiMaskCacheQueue = new Queue<GGraph>();
        private Dictionary<GObject, UIParentInfo> tempGObjectParentDict = new Dictionary<GObject, UIParentInfo>();

        private Dictionary<int, Window> uiLayerWindowDict = new Dictionary<int, Window>();
        private List<BaseUI> existDynamicUIs = new List<BaseUI>();
        private List<BaseUI> tickUpdateUIs = new List<BaseUI>();
        private List<BaseUI> normalUIRecord = new List<BaseUI>();

        private List<UISequenceInfo> uiSequenceQueue = new List<UISequenceInfo>();
        private ObjectPool<UISequenceInfo> uiSequencePool = new ObjectPool<UISequenceInfo>();

        private Vector2 uiCenterPos;
        private uint currUIOpenCumsumId;
        private int closeWorldRaycastRefCount;
        #endregion

        #region UI
        public void RegisterCommonPackage(string commonPackage)
        {
            if (!commonPackageList.Contains(commonPackage))
            {
                commonPackageList.Add(commonPackage);
            }
        }

        public void RegisterCommonPackages(List<string> commonPackages)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                for (int i = 0; i < commonPackages.Count; i++)
                {
                    string pakName = commonPackages[i];
                    if (UIPackage.GetByName(pakName) == null)
                    {
                        string packagePath = GetPackageUIPath(pakName);
                        UIPackage.AddPackage(packagePath);
                    }
                }
                return;
            }
#endif
            for (int i = 0; i < commonPackages.Count; i++)
            {
                RegisterCommonPackage(commonPackages[i]);
            }
        }

        public void RegisterDefaultFont(string defaultFontName)
        {
            // 默认字体
            // 多个字体名称用英文逗号隔开，Unity会自动使用第一个能识别的字体名称，但必须有一个是能识别的。
            uiDefaultFontName = defaultFontName;

            Font defaultFont = ResourcesLoader.Instance.SyncLoadFont("DynamicFont/" + defaultFontName);
            if (defaultFont != null)
            {
                FontManager.RegisterFont(new DynamicFont(defaultFontName, defaultFont), defaultFontName);
            }
        }

        public void RegisterFont(params string[] otherFontNames)
        {
            // 其他字体
            for (int i = 0; i < otherFontNames.Length; i++)
            {
                string fontName = otherFontNames[i];
                Font font = ResourcesLoader.Instance.SyncLoadFont("DynamicFont/" + fontName);
                if (font != null)
                {
                    FontManager.RegisterFont(new DynamicFont(fontName, font), fontName);
                }
            }
        }

        public void RegisterReplaceFont(string rawFontName, string replaceFontName)
        {
            Font replaceFont = ResourcesLoader.Instance.SyncLoadFont("DynamicFont/" + replaceFontName);
            if (replaceFont != null)
            {
                FontManager.RegisterFont(new DynamicFont(replaceFontName, replaceFont), rawFontName);
            }
        }

        public void RegisterMapFont(string editorFontName, string replaceFontName)
        {
            BaseFont replaceFont = FontManager.GetFont(replaceFontName);
            if (replaceFont != null)
            {
                FontManager.RegisterFont(replaceFont, editorFontName);
            }
        }

        public BaseFont GetFont(string fontName)
        {
            return FontManager.GetFont(fontName);
        }

        #endregion

        #region Process
        public void CloseAllUI()
        {
            while (existDynamicUIs.Count != 0)
            {
                BaseUI ui = existDynamicUIs[existDynamicUIs.Count - 1];
                Internal_CloseUI(ui);
            }
        }

        public void DisposeAllUI()
        {
            while (existDynamicUIs.Count != 0)
            {
                BaseUI ui = existDynamicUIs[existDynamicUIs.Count - 1];
                Internal_CloseUI(ui, true);
            }
            UIPackage.RemoveAllPackages();
        }

        public void SwitchSceneCloseAllUI()
        {
            for (int i = existDynamicUIs.Count - 1; i >= 0; i--)
            {
                if (existDynamicUIs.Count == 0) return;

                if (i >= existDynamicUIs.Count)
                {
                    i = existDynamicUIs.Count;
                }

                BaseUI ui = existDynamicUIs[i];
                if (ui.uiInfo.isSwitchSceneCloseUI)
                {
                    Internal_CloseUI(ui);
                }
            }
        }

        public void Internal_OpenUI(BaseUI ui, object args = null)
        {
            if (!IsStartUp) return;

            LoadUI(ui, args, OpenUIProcess);
        }

        private void OpenUIProcess(BaseUI ui, object args)
        {
            existDynamicUIs.Add(ui);

            ui.Process_Bind();
            ui.Process_OpenBefore(args);
            ui.Process_Open(args);

            AddNormalBaseUI(ui);
            NotificationEvent(AppMsg.UIEvent_UIOpen, ui);

            if (ui.uiInfo.isTickUpdate)
            {
                tickUpdateUIs.Add(ui);
            }
            if (ui.uiInfo.isClosetWorldRaycast)
            {
                SetWorldRaycasterEnabled(false);
            }

            if (UIMgrConst.IsEnableOpenUIAnim && ui.uiInfo.isNeedOpenAnim)
            {
                ui.KillOpenUIAnim();
                OpenUIAnim(ui);
            }
        }

        public void Internal_CloseUI(BaseUI ui, bool isImmediatelyDispose = false)
        {
            if (existDynamicUIs.Contains(ui))
            {
                existDynamicUIs.Remove(ui);
                RemoveNormalBaseUI(ui);

                if (ui.uiInfo.isTickUpdate)
                {
                    tickUpdateUIs.Remove(ui);
                }

                ui.Process_Close();

                if (UIMgrConst.IsEnableOpenUIAnim && ui.uiInfo.isNeedOpenAnim)
                {
                    ui.KillOpenUIAnim();
                }
                if (UIMgrConst.IsEnableCloseUIAnim && ui.uiInfo.isNeedCloseAnim)
                {
                    ui.KillCloseUIAnim();
                    CloseUIAnim(ui, () => DestroyUI(ui, isImmediatelyDispose));
                }
                else
                {
                    DestroyUI(ui, isImmediatelyDispose);
                }
            }
        }

        public void Internal_HideUI(BaseUI ui)
        {
            ui.Process_Hide();
            NotificationEvent(AppMsg.UIEvent_UIHide, ui);
        }

        public void Internal_DisplayUI(BaseUI ui, object args = null)
        {
            ui.Process_Display(args);
            NotificationEvent(AppMsg.UIEvent_UIDisplay, ui);
        }
        #endregion

        #region Process Function
        /// <summary>
        /// 设置切换界面的语言
        /// </summary>
        public void SetSwitchLanguage(string switchLang)
        {
            if (AppConst.IsMultiLangue)
            {
                // 判断FGUI多语言的支持
                TextAsset multiLangueConfig = ResourcesLoader.Instance.SyncLoadTextAsset("Data/MultiLanguage/#MultiLanguageInfo");
                if (!multiLangueConfig) return;
                if (string.IsNullOrWhiteSpace(multiLangueConfig.text)) return;
                List<string> multiLangueConfigList = SerializeUtil.ToObject<List<string>>(multiLangueConfig.text);
                if (multiLangueConfigList == null || multiLangueConfigList.Count == 0) return;

                if (string.IsNullOrWhiteSpace(switchLang)) return;
                if (switchLang == Stage.inst.currLang && switchLang == AppConst.InternalLangue) return;
                if (!multiLangueConfigList.Contains(switchLang)) return;

                TextAsset switchLangXML = ResourcesLoader.Instance.SyncLoadTextAsset("Data/MultiLanguage/" + switchLang);
                if (!switchLangXML) return;
                if (string.IsNullOrWhiteSpace(switchLangXML.text)) return;

                // 设置FGUI多语言的配置
                FairyGUI.Utils.XML xml = new FairyGUI.Utils.XML(switchLangXML.text);
                UIPackage.SetStringsSource(xml);
                Stage.inst.currLang = switchLang;

                // 设置应用多语言
                AppConst.CurrMultiLangue = switchLang;
                PrefsUtil.WriteObject(PrefsKeyConst.UIMgr_switchLanguage, AppConst.CurrMultiLangue);
                AppDispatcher.Instance.Dispatch(AppMsg.App_SwitchLanguage);

                // 清除UIPackage的TranslatedFlag
                List<UIPackage> uiPackageList = UIPackage.GetPackages();
                for (int i = 0; i < uiPackageList.Count; i++)
                {
                    List<PackageItem> packageItemList = uiPackageList[i].GetItems();
                    for (int j = 0; j < packageItemList.Count; j++)
                    {
                        PackageItem packageItem = packageItemList[j];
                        if (packageItem.translated)
                        {
                            packageItem.translated = false;
                        }
                    }
                }

                // 所有UI切换语言流程
                for (int i = existDynamicUIs.Count - 1; i >= 0; i--)
                {
                    BaseUI ui = existDynamicUIs[i];
                    if (ui == null) continue;
                    if (ui.isClose) continue;

                    ui.ProcessFunc_SwitchLanguage();
                }
            }
        }
        #endregion

        #region Private
        private void Update()
        {
            if (tickUpdateUIs.Count <= 0) return;

            for (int i = tickUpdateUIs.Count - 1; i >= 0; i--)
            {
                BaseUI ui = tickUpdateUIs[i];
                if (ui == null) continue;
                if (ui.isClose) continue;
                if (!ui.uiInfo.isTickUpdate) continue;

                ui.OnUpdate();
            }
        }

        private void LoadUI(BaseUI ui, object args, Action<BaseUI, object> completeFunc)
        {
            AddUIPackage(ui.uiInfo.packageName);
            CreateUI(ui, args, completeFunc);
        }

        private void CreateUI(BaseUI ui, object args, Action<BaseUI, object> completeFunc)
        {
            if (string.IsNullOrEmpty(ui.uiName))
            {
                LogUtil.LogErrorFormat("[UIMgr]Create {0} {1}/{2} UI Name Is Null", ui.GetType(), ui.uiInfo.packageName, ui.uiInfo.assetName);
                return;
            }

            GObject gObject = UIPackage.CreateObject(ui.uiInfo.packageName, ui.uiInfo.assetName);

            string rawGoName = gObject.gameObjectName;
            string uiGoName = string.Format("({0}){1}", ui.uiName, rawGoName);
            gObject.gameObjectName = uiGoName;
            gObject.displayObject.name = uiGoName;

            ui.baseGObj = gObject;
            ui.baseUI = ui.baseGObj.asCom;
            // 启用深度自动调整合批
            ui.baseUI.fairyBatching = true;

            ui.baseUI.name = rawGoName;
            ui.rawGameObjectName = rawGoName;
            ui.gameObjectName = uiGoName;

            ui.baseUI.MakeFullScreen();
            ui.baseUI.SetSize(GRoot.inst.width, GRoot.inst.height, false);
            if (IsUseSafeAreaAdaptive)
            {
                Rect safeArea = Screen.safeArea;
                ui.baseUI.SetPivot(0.5f, 0.5f, false);
                ui.baseUI.SetSize(GRoot.inst.width, safeArea.height);
                ui.baseUI.y = GRoot.inst.height - safeArea.height;
            }

            SetButtonClickDownEffect(ui.baseUI);

            if (ui.uiInfo.gComType == UIGComType.Window)
            {
                Window gWindowUI = new Window();
                gWindowUI.contentPane = ui.baseUI;
                ui.windowUI = gWindowUI;
                ui.windowUI.fairyBatching = true;
                ui.windowUI.Show();
            }

            if (ui.uiInfo.isNeedUIMask)
            {
                GGraph uiMask = CreateUIMask(ui.uiInfo.uiMaskCustomColor);
                ui.uiMask = uiMask;
                ui.baseUI.AddChildAt(ui.uiMask, 0);
                if (ui.uiInfo.isNeedUIMaskCloseEvent)
                {
                    ui.uiMask.onClick.Add(ui.CtrlCloseUI);
                }
            }

            ui.uiOpenCumsumId = ++currUIOpenCumsumId;
            ui.currLayer = (int)ui.uiInfo.layerType;
            uiLayerWindowDict[ui.currLayer].AddChild(ui.baseUI);
            completeFunc(ui, args);
        }

        private void AddUIPackage(string packageName)
        {
            if (UIPackage.GetByName(packageName) == null)
            {
                ResMgr.Instance.AddFguiPackage(packageName, GetPackageUIPath(packageName));
            }
        }

        public void RemoveUIPackage(string packageName)
        {
            UIPackage.RemovePackage(packageName);
        }

        private string GetPackageUIPath(string packageName)
        {
            return string.Format("UI/{0}", packageName);
        }

        private void SetButtonClickDownEffect(GComponent gComponent)
        {
            GObject[] gObjects = gComponent.GetChildren();
            for (int i = 0; i < gObjects.Length; i++)
            {
                GObject gObject = gObjects[i];
                GButton gButton = gObject.asButton;
                if (gButton != null && gButton.mode == ButtonMode.Common)
                {
                    // OtherButton避开框架规则
                    if (!gButton.name.StartsWith("obtn_"))
                    {
                        if (IsSetButtonPivotCenter)
                        {
                            gButton.SetPivot(0.5f, 0.5f, false);
                        }
                        gButton.SetClickDownEffect(UIMgrConst.ClickDownAnimEffectScale);
                    }
                    continue;
                }

                GComponent otherGComponent = gObject.asCom;
                if (otherGComponent != null)
                {
                    SetButtonClickDownEffect(otherGComponent);
                    continue;
                }
            }
        }

        private GGraph CreateUIMask(Color color)
        {
            GGraph uiMask = null;
            if (uiMaskCacheQueue.Count > 0)
            {
                uiMask = GetUIMaskFormPool();
                uiMask.color = color;
                uiMask.visible = true;
            }
            else
            {
                uiMask = new GGraph();
                uiMask.gameObjectName = "UIMask";
                uiMask.name = uiMask.gameObjectName;

                uiMask.SetPivot(0.5f, 0.5f, true);
                uiMask.SetXY(uiCenterPos.x, uiCenterPos.y);
                uiMask.DrawRect(5000, 5000, 0, Color.black, color);
            }
            return uiMask;
        }

        private GGraph GetUIMaskFormPool()
        {
            if (uiMaskCacheQueue.Count == 0) return null;
            return uiMaskCacheQueue.Dequeue();
        }

        private void ReleaseUIMaskToPool(BaseUI ui)
        {
            if (ui.uiMask == null) return;

            GGraph uiMask = ui.uiMask;
            ui.baseUI.RemoveChild(uiMask);
            ui.uiMask = null;

            uiMask.onClick.Clear();
            uiMask.visible = false;
            uiMaskCacheQueue.Enqueue(uiMask);
        }

        private void DestroyUI(BaseUI ui, bool isImmediatelyDispose)
        {
            if (ui.uiInfo.isNeedUIMask)
            {
                ReleaseUIMaskToPool(ui);
            }
            if (ui.uiInfo.isClosetWorldRaycast)
            {
                SetWorldRaycasterEnabled(true);
            }

            CloseAllSubUI(ui);
            DisposeUI(ui);
            ui.Process_Destroy();
            NotificationEvent(AppMsg.UIEvent_UIClose, ui);

            QuitUISequence(ui);
        }

        private void DisposeUI(BaseUI ui)
        {
            uiLayerWindowDict[ui.currLayer].RemoveChild(ui.baseUI);
            if (ui.uiInfo.gComType == UIGComType.Window)
            {
                if (ui.windowUI != null)
                {
                    ui.windowUI.Dispose();
                    ui.windowUI = null;
                }
            }
            ui.baseUI.Dispose();
            ui.baseUI = null;
        }

        private void NotificationEvent(uint msgId, BaseUI ui)
        {
            AppDispatcher.Instance.Dispatch(msgId, ui);
        }

        private void OpenUIAnim(BaseUI ui)
        {
            GObject gObj = ui.baseUI;
            gObj.pivot = VectorConst.Half;
            gObj.SetScale(UIMgrConst.OpenUIAnimEffectScale.x, UIMgrConst.OpenUIAnimEffectScale.y);
            ui.openUiGTweener = gObj.TweenScale(Vector3.one, UIMgrConst.OpenUIAnimEffectTime).OnComplete(() =>
            {
                ui.openUiGTweener = null;
                ui.Process_OpenUIAnimEnd();
            }).SetIgnoreEngineTimeScale(true).SetEase(EaseType.BackOut);
        }

        private void CloseUIAnim(BaseUI ui, Action animEndCB)
        {
            GObject gObj = ui.baseUI;
            ui.closeUiGTweener = gObj.TweenScale(UIMgrConst.OpenUIAnimEffectScale, UIMgrConst.OpenUIAnimEffectTime).OnComplete(() =>
            {
                ui.Process_CloseUIAnimEnd();
                animEndCB();
            }).SetIgnoreEngineTimeScale(true).SetEase(EaseType.BackIn);
        }

        private void SetWorldRaycasterEnabled(bool enabled)
        {
            if (enabled)
            {
                closeWorldRaycastRefCount--;
            }
            else
            {
                closeWorldRaycastRefCount++;
            }

            if (closeWorldRaycastRefCount > 0)
            {
                CameraMgr.Instance.SetWorldRaycasterEnabled(false);
            }
            else
            {
                CameraMgr.Instance.SetWorldRaycasterEnabled(true);
            }
        }
        #endregion

        #region Public
        public BaseUI GetDynamicUI(string uiName)
        {
            foreach (BaseUI ui in existDynamicUIs)
            {
                if (ui.uiName == uiName)
                {
                    return ui;
                }
            }
            return null;
        }

        public bool IsExistUI(string uiName)
        {
            foreach (BaseUI ui in existDynamicUIs)
            {
                if (ui.uiName == uiName)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetExistDynamicUICount()
        {
            return existDynamicUIs.Count;
        }

        public void SetUILayerAddBottom(GComponent gComponent, UILayerType toLayer)
        {
            if (gComponent == null) return;
            uiLayerWindowDict[(int)toLayer].AddChildAt(gComponent, 0);
        }

        public void SetUILayerAddTop(GComponent gComponent, UILayerType toLayer)
        {
            if (gComponent == null) return;
            uiLayerWindowDict[(int)toLayer].AddChild(gComponent);
        }

        public void SetUILayerAddBottom(BaseUI ui, UILayerType toLayer)
        {
            if (ui == null) return;
            uiLayerWindowDict[(int)toLayer].AddChildAt(ui.baseGObj, 0);
        }

        public void SetUILayerAddTop(BaseUI ui, UILayerType toLayer)
        {
            if (ui == null) return;
            uiLayerWindowDict[(int)toLayer].AddChild(ui.baseGObj);
        }

        public void SetGObjectUILayer(UILayerType toLayer, params GObject[] objs)
        {
            foreach (GObject item in objs)
            {
                if (item.parent != null)
                {
                    GComponent defaultParent = item.parent;
                    int idx = defaultParent.GetChildIndex(item);

                    if (!tempGObjectParentDict.ContainsKey(item))
                    {
                        //只会保留第一个
                        tempGObjectParentDict.Add(item, new UIParentInfo()
                        {
                            parent = defaultParent,
                            index = idx,
                        });
                    }
                }

                uiLayerWindowDict[(int)toLayer].AddChild(item);
                //局部坐标转Root
                Vector2 pos = item.LocalToRoot(Vector2.zero, GRoot.inst);
                item.position = pos;
            }
        }

        public void ResetGObjectUILayer(params GObject[] objs)
        {
            foreach (GObject item in objs)
            {
                if (tempGObjectParentDict.ContainsKey(item))
                {
                    UIParentInfo parentInfo = tempGObjectParentDict[item];
                    int itemIndex = parentInfo.index;
                    int parentCount = parentInfo.parent.GetChildrenCount();
                    if (parentCount != 0 && parentCount >= itemIndex)
                    {
                        parentInfo.parent.AddChildAt(item, itemIndex);
                    }
                    else
                    {
                        parentInfo.parent.AddChild(item);
                    }

                    //Root坐标转局部
                    Vector2 pos = parentInfo.parent.RootToLocal(item.position, GRoot.inst);
                    item.position = pos;

                    tempGObjectParentDict.Remove(item);
                }
            }
        }

        public void EnableEventSystem(bool enable)
        {
            eventSystem.enabled = enable;
        }
        public void OpenBeforeNormalUI()
        {
            if (GetBeforeBaseUI() != null)
            {
                BaseUI beforeUI = GetBeforeBaseUI();
                CloseNowNormalUI();
                uint msgId = beforeUI.GetOpenUIMsgId();
                if (msgId == 0) return;
                UICtrlDispatcher.Instance.Dispatch(msgId);
            }
        }

        public void CloseNowNormalUI()
        {
            if (GetNowBaseUI() != null)
            {
                BaseUI nowUI = GetNowBaseUI();
                uint msgId = nowUI.GetCloseUIMsgId();
                if (msgId == 0) return;
                UICtrlDispatcher.Instance.Dispatch(msgId);
            }
        }

        public bool AddNormalBaseUI(BaseUI ui)
        {
            if (ui.uiInfo.layerType != UILayerType.Normal) return false;
            BaseUI nowUI = GetNowBaseUI();
            if (nowUI == null)
            {
                normalUIRecord.Add(ui);
                return true;
            }
            //如果是当前正在展示的界面，则直接返回false
            if (ui.uiInfo.packageName == nowUI.uiInfo.packageName && ui.uiInfo.assetName == nowUI.uiInfo.assetName) return false;

            normalUIRecord.Add(ui);

            return true;
        }

        public bool RemoveNormalBaseUI(BaseUI ui)
        {
            if (ui.uiInfo.layerType != UILayerType.Normal) return false;
            BaseUI nowUI = GetNowBaseUI();
            if (nowUI == null) return false;

            if (ui.uiInfo.packageName == nowUI.uiInfo.packageName && ui.uiInfo.assetName == nowUI.uiInfo.assetName)
            {
                normalUIRecord.Remove(ui);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取上一个展示的UI
        /// </summary>
        /// <returns></returns>
        public BaseUI GetBeforeBaseUI()
        {
            if (normalUIRecord.Count > 1)
            {
                return normalUIRecord[normalUIRecord.Count - 2];
            }
            return null;
        }

        /// <summary>
        /// 获取当前展示的UI
        /// </summary>
        /// <returns></returns>
        public BaseUI GetNowBaseUI()
        {
            if (normalUIRecord.Count > 0)
            {
                return normalUIRecord[normalUIRecord.Count - 1];
            }
            return null;
        }

        /// <summary>
        /// 获取当前UI自定义打开Id
        /// </summary>
        public uint GetCurrUIOpenCumsumId()
        {
            return currUIOpenCumsumId;
        }
        #endregion

        #region UISequence
        public void Internal_OpenUISequence(BaseUI ui, object args = null)
        {
            UISequenceInfo sequenceInfo = uiSequencePool.Get();
            sequenceInfo.ui = ui;
            sequenceInfo.args = args;
            sequenceInfo.isLaunch = false;
            uiSequenceQueue.Add(sequenceInfo);

            ExecuteOpenUISequence();
        }

        public void RemoveUISequence(BaseUI ui)
        {
            if (uiSequenceQueue.Count == 0) return;

            for (int i = 0; i < uiSequenceQueue.Count; i++)
            {
                UISequenceInfo sequenceInfo = uiSequenceQueue[0];
                if (sequenceInfo.ui == ui)
                {
                    uiSequenceQueue.RemoveAt(i);
                    uiSequencePool.Release(sequenceInfo);
                }
            }
        }

        public void ClearUISequence()
        {
            if (uiSequenceQueue.Count == 0) return;

            for (int i = 0; i < uiSequenceQueue.Count; i++)
            {
                UISequenceInfo sequenceInfo = uiSequenceQueue[0];
                uiSequencePool.Release(sequenceInfo);
            }
            uiSequenceQueue.Clear();
        }

        private void ExecuteOpenUISequence()
        {
            if (uiSequenceQueue.Count == 0) return;

            UISequenceInfo sequenceInfo = uiSequenceQueue[0];
            if (sequenceInfo.isLaunch) return;

            sequenceInfo.isLaunch = true;
            BaseUI ui = sequenceInfo.ui;
            ui.Open(sequenceInfo.args);
        }

        private void QuitUISequence(BaseUI ui)
        {
            if (uiSequenceQueue.Count == 0) return;

            UISequenceInfo sequenceInfo = uiSequenceQueue[0];
            if (sequenceInfo.ui != ui) return;

            uiSequenceQueue.Remove(sequenceInfo);
            uiSequencePool.Release(sequenceInfo);
            ExecuteOpenUISequence();
        }
        #endregion

        #region SubUI
        public SubUI OpenSubUI(BaseUI mainUI, string subUiName, string packageName, string assetName, bool needStandardResolution)
        {
            SubUI subUI = new SubUI(subUiName, packageName, assetName);

            AddUIPackage(packageName);
            GObject gObject = UIPackage.CreateObject(packageName, assetName);

            string rawGoName = gObject.gameObjectName;
            string uiGoName = string.Format("({0}){1}", subUI.uiName, rawGoName);
            gObject.gameObjectName = uiGoName;
            gObject.displayObject.name = uiGoName;

            subUI.baseGObj = gObject;
            subUI.baseUI = subUI.baseGObj.asCom;
            subUI.baseUI.fairyBatching = true;

            subUI.baseUI.name = rawGoName;
            subUI.rawGameObjectName = rawGoName;
            subUI.gameObjectName = uiGoName;

            if (needStandardResolution)
            {
                subUI.baseUI.SetSize(GRoot.inst.width, GRoot.inst.height, false);
            }

            if (mainUI.subUIs == null)
            {
                mainUI.subUIs = new List<SubUI>();
            }
            mainUI.subUIs.Add(subUI);
            mainUI.baseUI.AddChild(subUI.baseGObj);
            return subUI;
        }

        public void CloseSubUI(BaseUI mainUI, SubUI subUI)
        {
            mainUI.subUIs.Remove(subUI);
            mainUI.baseUI.RemoveChild(subUI.baseGObj);

            subUI.baseUI.Dispose();
            subUI.baseUI = null;
        }

        public void CloseAllSubUI(BaseUI mainUI)
        {
            List<SubUI> subUIs = mainUI.subUIs;
            if (subUIs == null) return;
            for (int i = subUIs.Count - 1; i >= 0; i--)
            {
                CloseSubUI(mainUI, subUIs[i]);
            }
            mainUI.subUIs.Clear();
            mainUI.subUIs = null;
        }
        #endregion

        #region Init
        private void InitUIMgr()
        {
            LogUtil.Log("[UIMgr]InitUIMgr");

            GameObject uiRootParent = new GameObject(AppObjConst.UIGoName);
            uiRootParent.layer = LayerMaskConst.UI;
            AppObjConst.UIGo = uiRootParent;
            AppObjConst.UIGo.SetParent(AppObjConst.FutureFrameGo);
            AppObjConst.UIGo.transform.position = new Vector3(CameraConst.UICameraPos.x, CameraConst.UICameraPos.y, 0);

            BindUIEventSystem();
            InitFguiConfig();
            InitFguiSettings();
            InitFguiLayers();
            InitFguiCommonPackages();
            InitFguiMultiLanguage();
        }

        private void BindUIEventSystem()
        {
            AppObjConst.EngineEventSystemGo = EngineEventSystem.Instance.eventObj.transform.parent.gameObject;
            AppObjConst.EngineEventSystemGo.name = AppObjConst.EngineEventSystemGoName;
            eventSystemGo = EngineEventSystem.Instance.eventObj;
            eventSystem = EngineEventSystem.Instance.eventSystem;
            inputModule = EngineEventSystem.Instance.inputModule;
        }

        private void InitFguiConfig()
        {
            /// 设置FGUI的分支
            UIPackage.branch = null;

            /// 设置FGUI的配置
            AppObjConst.UIGo.AddComponent<UIConfig>();
            // UI默认字体
            UIConfig.defaultFont = uiDefaultFontName;
            // UI字体八方向描边效果
            UIConfig.enhancedTextOutlineEffect = true;
            // 关闭Window点击自动排序功能
            UIConfig.bringWindowToFrontOnClick = false;
            // 设置动态窗口的背景颜色
            UIConfig.modalLayerColor = new Color(0f, 0f, 0f, (255f / 2f) / 255f);
            // 设置按钮音效大小
            UIConfig.buttonSoundVolumeScale = 1;
        }

        private void InitFguiSettings()
        {
            AppObjConst.UICacheGo = new GameObject(AppObjConst.UICacheGoName);
            AppObjConst.UICacheGo.SetParent(AppObjConst.FutureFrameGo);

            DisplayObject.CreateUICacheRoot(AppObjConst.UICacheGo.transform);
            Stage.Instantiate();
            Stage.inst.gameObject.transform.SetParent(AppObjConst.UIGo.transform, false);

            Vector2Int uiResolution = AppConst.UIResolution;
            GRoot.inst.SetContentScaleFactor(uiResolution.x, uiResolution.y, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
            uiCenterPos = new Vector2(GRoot.inst.width / 2f, GRoot.inst.height / 2f);
            LogUtil.Log("[UIMgr]StandardResolution: " + AppConst.StandardResolution);
        }

        private void InitFguiLayers()
        {
            GRoot.inst.fairyBatching = false;
            for (int i = 0; i < UILayerConst.AllUILayer.Length; i++)
            {
                Window uiLayerWindow = new Window();
                string name = UILayerConst.AllUILayer[i];
                uiLayerWindow.fairyBatching = false;
                uiLayerWindow.name = name;
                uiLayerWindow.displayObject.name = name;
                uiLayerWindow.gameObjectName = uiLayerWindow.name;
                uiLayerWindow.sortingOrder = i * 100;
                uiLayerWindow.Show();
                uiLayerWindow.fairyBatching = false;
                uiLayerWindowDict.Add(i, uiLayerWindow);
            }
        }

        private void InitFguiCommonPackages()
        {
            UIPackage.RemoveAllPackages();
            foreach (string commonPackage in commonPackageList)
            {
                ResMgr.Instance.AddFguiPackage(commonPackage, GetPackageUIPath(commonPackage));
            }
        }

        private void InitFguiMultiLanguage()
        {
            // 多语言开关
            if (AppConst.IsMultiLangue)
            {
                // 设置项目内置语言
                Stage.inst.currLang = AppConst.InternalLangue;

                // 判断FGUI多语言的支持
                TextAsset multiLangueConfig = ResourcesLoader.Instance.SyncLoadTextAsset("Data/MultiLanguage/#MultiLanguageInfo");
                if (!multiLangueConfig) return;
                if (string.IsNullOrWhiteSpace(multiLangueConfig.text)) return;
                List<string> multiLangueConfigList = SerializeUtil.ToObject<List<string>>(multiLangueConfig.text);
                if (multiLangueConfigList == null || multiLangueConfigList.Count == 0) return;

                string switchLang = PrefsUtil.ReadTObject<string>(PrefsKeyConst.UIMgr_switchLanguage);
                if (string.IsNullOrWhiteSpace(switchLang))
                {
                    switchLang = LangueUtil.GetCurrLangue(multiLangueConfigList);
                }
                if (string.IsNullOrWhiteSpace(switchLang)) return;
                if (switchLang == Stage.inst.currLang && switchLang == AppConst.InternalLangue) return;

                TextAsset switchLangXML = ResourcesLoader.Instance.SyncLoadTextAsset("Data/MultiLanguage/" + switchLang);
                if (!switchLangXML) return;
                if (string.IsNullOrWhiteSpace(switchLangXML.text)) return;

                // 设置FGUI多语言的配置
                FairyGUI.Utils.XML xml = new FairyGUI.Utils.XML(switchLangXML.text);
                UIPackage.SetStringsSource(xml);
                Stage.inst.currLang = switchLang;

                // 设置应用多语言
                AppConst.CurrMultiLangue = switchLang;
            }
        }
        #endregion

        #region FGUI API

        public GObject[] GetGRootAllUI()
        {
            int num = GRoot.inst.numChildren;
            GObject[] coms = new GObject[num];
            for (int i = 0; i < num - 1; i++)
            {
                coms[i] = GRoot.inst.GetChildAt(i);
            }
            return coms;
        }

        public Dictionary<string, string>[] GetUIPackageDependencies(UIPackage uiPak)
        {
            return uiPak.dependencies;
        }

        public int GetStageRenderOrder()
        {
            return Stage.inst.renderingOrder;
        }

        public float GetUIContentScaleFactor()
        {
            return UIContentScaler.scaleFactor;
        }

        public void SetGObjectPosCenter(GObject gObject)
        {
            gObject.SetXY((GRoot.inst.width - gObject.width) / 2, (GRoot.inst.height - gObject.height) / 2);
        }

        public void SetGObjectFullScreen(GObject gObject)
        {
            gObject.SetScale(GRoot.inst.width / gObject.width, GRoot.inst.height / gObject.height);
        }

        #endregion

        #region Mgr
        public override void Init()
        {
            base.Init();
            InitUIMgr();
        }

        public override void DisposeBefore()
        {
            base.DisposeBefore();
            DisposeAllUI();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            EngineUtil.Destroy(AppObjConst.UIGo);

            commonPackageList.Clear();
            existDynamicUIs.Clear();
            tickUpdateUIs.Clear();
            commonPackageList = null;
            existDynamicUIs = null;
            tickUpdateUIs = null;

            uiSequencePool.Dispose();
            uiSequenceQueue.Clear();
            uiSequencePool = null;
            uiSequenceQueue = null;
        }
        #endregion
    }
}