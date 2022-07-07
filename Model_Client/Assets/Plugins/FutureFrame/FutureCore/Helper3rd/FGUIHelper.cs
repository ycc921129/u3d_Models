/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

namespace FutureCore
{
    public class EventObjWithParam
    {
        public Action<object> action = null;

        public EventObjWithParam(Action<object> action)
        {
            this.action = action;
        }
    }

    /// <summary>
    /// FGUI 帮助类
    /// · 支持相对目录导出: [资源目录]..\..\..\UClient_Xxx\Assets\_Res\Resources\UI [代码目录]..\..\..\UClient_Xxx\Assets\_App\AutoCreator\FGUI_Project
    /// · 支持层级目录导出: A\A 或者 A\{publish_file_name}
    /// · 支持编辑器的批处理Bat导出
    /// · 支持编辑器的插件开发
    ///
    /// -> URL格式: "ui://包名/图片名"
    /// -> 文本模板: 文本为“我的元宝：{jin=100}金{yin=200}银”，然后勾选“使用文本模板”即可。
    /// -> 当“{”字符不是用于模板时，可以使用"\{“这样的方式转义，注意，在编辑器输入时是直接输入”\“即可，在代码里需要用”\\“这样才能表达反斜杠。另外，只需转义”{"，不需要转义"}"。
    /// -> 文本模板优先于UBB解析，所以模板也可以在UBB中使用，例如文本为：“这个是可以变色的[color ={color=#FF0000}]文本[/color]”，可以方便的实现动态更改部分文本颜色的需求。
    ///
    /// a. 组件视图测试宏定义: FAIRYGUI_TEST
    /// b. AirTest自动化测试工具宏定义: FAIRYGUI_TEST
    /// c. 从右向左排版的阿拉伯语言文字显示宏定义: RTL_TEXT_SUPPORT (这个是全局设置，不适合做多语言)
    ///
    /// 1）TTF字体效果在Unity里面调整
    /// 2）水平填充完全可以用组件的溢出隐藏实现
    ///
    /// </summary>
    public static class FGUIHelper
    {
        /// <summary>
        /// 点击按下效果
        /// </summary>
        private const float ClickDownAnimEffectScale = 0.9f;

        /// <summary>
        /// 添加资源包
        /// </summary>
        public static UIPackage AddInitPackage(string pkgName)
        {
            if (UIPackage.GetByName(pkgName) == null)
            {
                return UIPackage.AddPackage("UI/" + pkgName);
            }
            return null;
        }

        /// <summary>
        /// 获取资源Url
        /// </summary>
        public static string GetItemURL(string pkgName, string resName)
        {
            string url = UIPackage.GetItemURL(pkgName, resName);
            return url;
        }

        /// <summary>
        /// 获取资源包
        /// </summary>
        public static PackageItem GetPakItemByURL(string url)
        {
            PackageItem pak = UIPackage.GetItemByURL(url);
            return pak;
        }

        /// <summary>
        /// 获取Fgui包内资源
        /// </summary>
        public static object GetItemAsset(string pkgName, string resName)
        {
            object pkg = UIPackage.GetItemAsset(pkgName, resName);
            return pkg;
        }

        /// <summary>
        /// 获取Fgui包内资源，通过Url
        /// </summary>
        public static object GetItemAssetByURL(string url)
        {
            object pkg = UIPackage.GetItemAssetByURL(url);
            return pkg;
        }

        /// <summary>
        /// 设置加载器Url
        /// </summary>
        public static string SetGLoaderUrl(GLoader gLoader, string pkgName, string resName)
        {
            string url = UIPackage.GetItemURL(pkgName, resName);
            if (url != null)
            {
                gLoader.url = url;
            }
            return url;
        }

        /// <summary>
        /// 动态创建装载器
        /// </summary>
        public static GLoader CreateGLoader(string url)
        {
            //gLoader.url = "ui://包名/图片名";
            GLoader gLoader = new GLoader();
            gLoader.SetSize(100, 100);
            gLoader.url = url;
            return gLoader;
        }

        /// <summary>
        /// 构建图片对象
        /// </summary>
        public static NTexture NewNTexture(Sprite sprite, bool useAtlasRect)
        {
            if (sprite)
            {
                return new NTexture(sprite, useAtlasRect);
            }
            return null;
        }

        /// <summary>
        /// 获取UI适配系数
        /// </summary>
        public static float GetUIContentScaleFactor()
        {
            return UIContentScaler.scaleFactor;
        }

        /// <summary>
        /// 加载添加GobjectUI对象
        /// </summary>
        public static void LoadAddGObject(string pak, string com, GComponent addUI, Vector3 position, Action clickFunc)
        {
            if (UIPackage.GetByName(pak) == null)
            {
                UIPackage.AddPackage("UI/" + pak);
            }
            GObject gObject = UIPackage.CreateObject(pak, com);
            GButton gButton = gObject.asButton;
            if (gButton != null && gButton.mode == ButtonMode.Common)
            {
                // OtherButton避开框架规则
                if (!gButton.name.StartsWith("obtn_"))
                {
                    gButton.SetPivot(0.5f, 0.5f, false);
                    gButton.SetClickDownEffect(UIMgrConst.ClickDownAnimEffectScale);
                }
            }
            addUI.AddChild(gObject);
            gObject.AddRelation(addUI, RelationType.Center_Center);
            gObject.AddRelation(addUI, RelationType.Middle_Middle);
            gObject.position = position;
            gObject.onClick.Set(() => clickFunc());
        }

        /// <summary>
        /// 世界坐标转FGUI坐标
        /// </summary>
        public static Vector2 WorldToUIPos(GameObject gameObject)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            //原点位置转换
            screenPos.y = Screen.height - screenPos.y;
            Vector2 fguiPosition = GRoot.inst.GlobalToLocal(screenPos);
            return fguiPosition;
        }

        /// <summary>
        /// 设置不可点击触摸
        /// </summary>
        public static void SetCannotTouchable(GObject gObject)
        {
            gObject.touchable = false;
        }

        /// <summary>
        /// 设置组件点击空白可穿透
        /// </summary>
        public static void SetEmptyClickPenetrate(GComponent gComponent)
        {
            gComponent.opaque = false;
        }

        /// <summary>
        /// 显示弹出组件
        /// </summary>
        public static void ShowGObjectPopup(GObject gObj)
        {
            //弹出在当前鼠标位置
            GRoot.inst.ShowPopup(gObj);
        }

        /// <summary>
        /// 关闭弹出组件
        /// </summary>
        public static void HideGObjectPopup(GObject gObj)
        {
            GRoot.inst.HidePopup(gObj);
        }

        /// <summary>
        /// 设置监听监听移出舞台的事件
        /// </summary>
        public static void SetOnRemoveFromStageEvent(GComponent gComponent, EventCallback1 func)
        {
            gComponent.onRemovedFromStage.Set(func);
        }

        /// <summary>
        /// GoWrapper挂载UGUI事件检查
        /// </summary>
        public static bool UGUIEvent_IsRaycastLocationValid(Vector2 sp, Camera eventCamera, GameObject uguiObj)
        {
            DisplayObject touchTarget = Stage.inst.touchTarget;
            if (touchTarget != null)
            {
                if (touchTarget is GoWrapper wrapper)
                {
                    if (wrapper.wrapTarget != uguiObj)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 是否没有点在UI上
        /// </summary>
        public static bool IsNoPointerUI(params string[] nullGObjectName)
        {
            if (!Stage.isTouchOnUI) return true;
            if (GRoot.inst.touchTarget == null) return true;

            for (int i = 0; i < nullGObjectName.Length; i++)
            {
                if (GRoot.inst.touchTarget.name.Equals(nullGObjectName[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否点击在UI
        /// </summary>
        public static bool IsPointerOnUI()
        {
            return Stage.isTouchOnUI;
        }

        /// <summary>
        /// 是否点击目标是这个GObject
        /// </summary>
        public static bool IsPointerOnGObject(string gObjectName)
        {
            if (!Stage.isTouchOnUI) return false;
            if (GRoot.inst.touchTarget == null) return false;

            return GRoot.inst.touchTarget.name.Equals(gObjectName);
        }

        /// <summary>
        /// 设置拖拽
        /// </summary>
        public static void SetDrop<T>(T gComponent, object param = null) where T : GComponent
        {
            gComponent.onDrop.Set((context) =>
            {
                EventObjWithParam obj = context.data as EventObjWithParam;
                if (obj == null) return;

                if (param != null)
                {
                    obj.action(param);
                }
                else
                {
                    obj.action(gComponent);
                }
            });
        }

        /// <summary>
        /// 设置动画帧时间
        /// </summary>
        public static void GetTransitionHookTime(Transition transition, string label)
        {
            transition.GetLabelTime(label);
        }

        /// <summary>
        /// 设置动画帧事件
        /// </summary>
        public static void SetTransitionHookEvent(Transition transition, string label, TransitionHook callback)
        {
            transition.SetHook(label, callback);
        }

        /// <summary>
        /// 设置动画帧内容
        /// </summary>
        public static void SetTransitionHookValue(Transition transition, string label, params object[] aParams)
        {
            transition.SetValue(label, aParams);
        }

        /// <summary>
        /// 设置动画帧对象
        /// </summary>
        public static void SetTransitionHookTarget(Transition transition, string label, GObject newTarget)
        {
            transition.SetTarget(label, newTarget);
        }

        /// <summary>
        /// 修改补间动画时间设置
        /// </summary>
        public static GTweener SetIgnoreTimeScale(GTweener gTweener, bool isIgnore)
        {
            return gTweener.SetIgnoreEngineTimeScale(isIgnore);
        }

        /// <summary>
        /// 播放点击音效
        /// </summary>
        public static void PlayClickSound()
        {
            if (UIConfig.buttonSound != null && UIConfig.buttonSound.nativeClip != null)
            {
                Stage.inst.PlayOneShotSound(UIConfig.buttonSound.nativeClip, UIConfig.buttonSoundVolumeScale);
            }
        }

        /// <summary>
        /// 设置文本模板
        /// </summary>
        public static void SetTextTemplateVars(GTextField textField, string value)
        {
            textField.SetVar("num", value).SetVar("count", value);
            textField.FlushVars();
        }

        /// <summary>
        /// 设置文本模板字典
        /// </summary>
        public static void SetTextTemplateVarsDict(GTextField textField, string value)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values["num"] = value;
            values["count"] = value;
            //注意，这种方式不需要再调用FlushVars()
            textField.templateVars = values;
        }

        /// <summary>
        /// 动态关闭文本模板功能
        /// </summary>
        public static void CloseTextTemplateVars(GTextField textField)
        {
            textField.templateVars = null;
        }

        /// <summary>
        /// 设置字体样式
        /// </summary>
        public static void SetTextStyleFormat(TextField textField)
        {
            TextFormat tf = textField.textFormat;
            tf.gradientColor = null;
            textField.textFormat = tf;
        }

        /// <summary>
        /// 设置字体
        /// </summary>
        public static void SetFont(TextField textField, string fontName)
        {
            TextFormat tf = textField.textFormat;
            tf.font = fontName;
            textField.textFormat = tf;
        }

        /// <summary>
        /// 设置颜色
        /// </summary>
        public static void SetColor(GImage image, Color color)
        {
            image.color = color;
        }

        /// <summary>
        /// 设置颜色
        /// </summary>
        public static void SetColor(GTextField textField, Color color)
        {
            textField.color = color;
        }

        /// <summary>
        /// 设置透明度
        /// </summary>
        public static void SetAlpha(GObject gObj, float alpha)
        {
            gObj.alpha = alpha;
        }

        /// <summary>
        /// 获取事件类型
        /// </summary>
        public static object GetEventContextType(EventContext context)
        {
            return context.type;
        }

        /// <summary>
        /// 获取事件数据
        /// </summary>
        public static object GetEventContextData(EventContext context)
        {
            return context.data;
        }

        /// <summary>
        /// 事件是否是双击
        /// </summary>
        public static bool EventContextIsDoubleClick(EventContext context)
        {
            return context.inputEvent.isDoubleClick;
        }

        /// <summary>
        /// 停止事件穿透响应
        /// </summary>
        public static void EventContextStopPropagation(EventContext context)
        {
            context.StopPropagation();
        }

        /// <summary>
        /// 显示点击动画
        /// </summary>
        public static GTweener ShowClickAnim(GObject gObj)
        {
            gObj.SetScale(0.9f, 0.9f);
            GTweener gTweener = gObj.TweenScale(Vector3.one, 0.3f).SetEase(EaseType.BackOut);
            return gTweener;
        }

        /// <summary>
        /// 设置组件点击动画
        /// </summary>
        public static void SetGComponentClickAnim(GComponent gComponent, float clickDownEffectValue = ClickDownAnimEffectScale)
        {
            gComponent.onTouchBegin.Set((context) =>
            {
                Stage.inst.AddTouchMonitor(context.inputEvent.touchId, gComponent);
                gComponent.SetScale(gComponent.scaleX * clickDownEffectValue, gComponent.scaleY * clickDownEffectValue);
            });
            gComponent.onTouchEnd.Set((context) =>
            {
                if (Stage.inst.HasTouchMonitor(gComponent))
                {
                    Stage.inst.RemoveTouchMonitor(gComponent);
                    gComponent.SetScale(gComponent.scaleX / clickDownEffectValue, gComponent.scaleX / clickDownEffectValue);
                }
            });
        }

        /// <summary>
        /// 组件添加关联
        /// </summary>
        public static void GObjectAddRelation(GObject curr, GObject target, RelationType relationType, bool usePercent)
        {
            curr.AddRelation(target, relationType, usePercent);
        }

        /// <summary>
        /// 组件移除关联
        /// </summary>
        public static void GObjectRemoveRelation(GObject curr, GObject target, RelationType relationType)
        {
            curr.RemoveRelation(target, relationType);
        }

        /// <summary>
        /// 设置是否启用深度自动调整合批
        /// </summary>
        public static void GObjectSetFairyBatching(GComponent gCom, bool isFairyBatching)
        {
            gCom.fairyBatching = isFairyBatching;
        }

        /// <summary>
        /// 组件手动刷新渲染批处理
        /// </summary>
        public static void GObjectInvalidateBatchingState(GObject gObj)
        {
            gObj.InvalidateBatchingState();
        }

        /// <summary>
        /// 动效手动刷新渲染批处理
        /// </summary>
        public static void TransitionTnvalidateBatchingEveryFrame(Transition transition)
        {
            transition.invalidateBatchingEveryFrame = true;
        }

        /// <summary>
        /// 动效是否忽略引擎的时间缩放
        /// </summary>
        public static void TransitionSetIgnoreEngineTimeScale(Transition transition, bool value)
        {
            transition.ignoreEngineTimeScale = value;
        }

        /// <summary>
        /// 获取事件发送者
        /// </summary>
        public static EventDispatcher GetEventSender(EventContext eventContext)
        {
            return eventContext.sender;
        }

        /// <summary>
        /// 模拟点击按钮
        /// </summary>
        public static void FireClickButton(GButton gButton)
        {
            gButton.FireClick(false, false);
        }

        /// <summary>
        /// 设置GoWrapper缩放
        /// </summary>
        public static GoWrapper SetGoWrapperScale(GoWrapper goWrapper, Vector2 scale)
        {
            goWrapper.scale = scale;
            return goWrapper;
        }

        /// <summary>
        /// 显示Unity对象
        /// </summary>
        public static GoWrapper ShowUnityObject(GGraph graph, GameObject go)
        {
            GoWrapper wrapper = new GoWrapper(go);
            wrapper.supportStencil = false;
            graph.SetNativeObject(wrapper);
            go.transform.localScale = go.transform.localScale.x * VectorConst.PPUOne;
            return wrapper;
        }

        /// <summary>
        /// 显示Spine对象
        /// </summary>
        public static GoWrapper ShowLoopSpineObject(GGraph graph, Spine.Unity.SkeletonAnimation spineAnim, string animName)
        {
            GoWrapper wrapper = new GoWrapper(spineAnim.gameObject);
            wrapper.supportStencil = false;
            graph.SetNativeObject(wrapper);
            spineAnim.gameObject.transform.localScale = spineAnim.gameObject.transform.localScale.x * VectorConst.PPUOne;
            SpineHelper.LoopPlayAnim(spineAnim, animName);
            return wrapper;
        }

        /// <summary>
        /// 显示Spine对象
        /// </summary>
        public static GoWrapper ShowSpineObject(GGraph graph, Spine.Unity.SkeletonAnimation spineAnim, string animName)
        {
            GoWrapper wrapper = new GoWrapper(spineAnim.gameObject);
            wrapper.supportStencil = false;
            graph.SetNativeObject(wrapper);
            spineAnim.gameObject.transform.localScale = spineAnim.gameObject.transform.localScale.x * VectorConst.PPUOne;
            SpineHelper.PlayAnim(spineAnim, animName);
            return wrapper;
        }

        /// <summary>
        /// 获取Spine缩放
        /// </summary>
        /// <param name="spineDesignResolutionHeight">Spine设计分辨率的高</param>
        public static Vector3 GetSpineScale(float spineDesignResolutionHeight = 1280f)
        {
            // 计算还原缩放值
            float scaleX = 1 / (GRoot.inst.scaleX * Stage.inst.scaleX);
            float scaleY = 1 / (GRoot.inst.scaleY * Stage.inst.scaleY);
            // 除以Spine的缩放比
            spineDesignResolutionHeight = (spineDesignResolutionHeight / AppConst.PixelsPerUnit) / (Stage.inst.GetRenderCamera().orthographicSize * 2);
            scaleX = scaleX / spineDesignResolutionHeight;
            scaleY = scaleY / spineDesignResolutionHeight;
            return new Vector3(scaleX, scaleY, 1);
        }

        /// <summary>
        /// 获取元件的运行时自定义参数
        /// </summary>
        public static object GetRuntimeParam(GObject gObject)
        {
            return gObject.rt_Param;
        }

        /// <summary>
        /// 获取组件用户自定义数据 (GComponent)
        /// </summary>
        public static string GetBaseUserCustomData(GComponent gComponent)
        {
            return gComponent.baseUserData;
        }

        /// <summary>
        /// 获取元件的自定义数据 (GObject)
        /// </summary>
        public static string GetCustomData(GObject gObject)
        {
            return gObject.customData;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        public static GObject GetChild(GComponent gComponent, string name)
        {
            return gComponent.GetChild(name);
        }

        /// <summary>
        /// 获取控制器
        /// </summary>
        public static Controller GetController(GComponent gComponent, string name)
        {
            return gComponent.GetController(name);
        }

        /// <summary>
        /// 获取动效
        /// </summary>
        public static Transition GetTransition(GComponent gComponent, string name)
        {
            return gComponent.GetTransition(name);
        }

        /// <summary>
        /// 关闭按钮所关联的控制器
        /// </summary>
        public static void RelieveGButtonRelatedController(GButton gButton)
        {
            gButton.relatedController = null;
        }

        /// <summary>
        /// 获取按钮是否选中
        /// </summary>
        public static bool GetGButtonSelected(GButton gButton)
        {
            return gButton.selected;
        }

        /// <summary>
        /// 获取按钮是否自动设置状态
        /// </summary>
        public static bool GetGButtonChangeStateOnClick(GButton gButton)
        {
            return gButton.changeStateOnClick;
        }

        /// <summary>
        /// 折叠GTree所有节点
        /// </summary>
        public static void GTreeCollapseAll(GTree gTree)
        {
            gTree.CollapseAll();
        }

        /// <summary>
        /// 设置列表是否点击移动对齐
        /// </summary>
        public static void SetListToViewOnClick(GList gList, bool value)
        {
            gList.scrollItemToViewOnClick = value;
        }

        /// <summary>
        /// 设置列表自动大小
        /// </summary>
        public static void SetListAutoResize(GList gList)
        {
            gList.foldInvisibleItems = true;
            gList.autoResizeItem = true;
        }

        /// <summary>
        /// 添加列表子项，通过资源名
        /// </summary>
        public static GObject AddListItem(GList gList, string pkgName, string resName)
        {
            string url = UIPackage.GetItemURL(pkgName, resName);
            GObject gObject = gList.AddItemFromPool(url);
            return gObject;
        }

        /// <summary>
        /// 添加列表子项，通过链接
        /// </summary>
        public static GObject AddListItemByURL(GList gList, string url)
        {
            GObject gObject = gList.AddItemFromPool(url);
            return gObject;
        }

        /// <summary>
        /// 添加列表默认子项
        /// </summary>
        public static GObject AddListDefaultItem(GList gList)
        {
            string url = UIPackage.NormalizeURL(gList.defaultItem);
            GObject gObject = gList.AddItemFromPool(url);
            return gObject;
        }

        /// <summary>
        /// 添加列表子项，通过资源名
        /// </summary>
        public static GObject AddNewListItem(GList gList, string pkgName, string resName)
        {
            if (UIPackage.GetByName(pkgName) == null)
            {
                UIPackage.AddPackage("UI/" + pkgName);
            }
            GObject gObject = UIPackage.CreateObject(pkgName, resName);
            return gList.AddChild(gObject);
        }

        /// <summary>
        /// 添加列表默认子项
        /// </summary>
        public static GObject AddNewListDefaultItem(GList gList)
        {
            string url = UIPackage.NormalizeURL(gList.defaultItem);
            GObject objItem = UIPackage.CreateObjectFromURL(url);
            return gList.AddChild(objItem);
        }

        /// <summary>
        /// 获取UI齿轮
        /// </summary>
        public static GearBase GetUIGear(GObject gObject)
        {
            GearBase gearBase = gObject.GetGear(0);
            return gearBase;
        }

        /// <summary>
        /// 文本动画
        /// </summary>
        public static void DoTextAnim(GTextField textField)
        {
            TypingEffect typingEffect = new TypingEffect(textField);
            typingEffect.Start();
            typingEffect.PrintAll(0.01f);
        }

        /// <summary>
        /// 添加FGUI的计时器
        /// </summary>
        public static void AddFguiTimer(float interval, int repeat, TimerCallback callback, object callbackParam)
        {
            FTimers.inst.Add(interval, repeat, callback, callbackParam);
        }  
          
        /// <summary>
        /// 重置旋转角度
        /// </summary>
        public static void ResetTweenRotate(GObject gObject)
        {
            while (gObject.rotation > 360f)
            {
                gObject.rotation -= 360f;
            }
        }

        /// <summary>
        /// 进行旋转
        /// </summary>
        public static GTweener DoTweenRotate(GObject gObject, float angle, int num, float time)
        {
            while (gObject.rotation > 360f)
            {
                gObject.rotation -= 360f;
            }
            GTweener gTweener = gObject.TweenRotate(angle + num * 360, time);
            gTweener.SetEase(EaseType.QuintOut);
            return gTweener;
        }
    }
}