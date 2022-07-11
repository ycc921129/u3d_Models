/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace FutureCore
{
    public static class UIInfoConst
    {
        public static Color DefaultUIMaskColor = new Color(0, 0, 0, 0.9f);  
    }

    public class UIInfo
    {
        public string packageName = null;
        public string assetName = null;

        public UILayerType layerType = UILayerType.Normal;
        public UIGComType gComType = UIGComType.GComponent;
        public UIType uiType = UIType.NormalUI;

        public uint openUIMsgId = 0;
        public uint closeUIMsgId = 0;

        // 是否缓存
        public bool isCache = false;
        // 是否切换场景关闭UI
        public bool isSwitchSceneCloseUI = false;
        // 是否有Update函数
        public bool isTickUpdate = false;
        // 是否关闭空间射线
        public bool isClosetWorldRaycast = false;
        // 是否需要UI打开动画
        public bool isNeedOpenAnim = false;
        // 是否需要UI关闭动画
        public bool isNeedCloseAnim = false;
        // 是否需要UI底部遮罩
        public bool isNeedUIMask = false;
        // 是否UI底部遮罩绑定关闭UI事件
        public bool isNeedUIMaskCloseEvent = false;
        // UI底部遮罩自定义颜色
        public Color uiMaskCustomColor = UIInfoConst.DefaultUIMaskColor;
    }

    public enum UILayerType : int
    {
        None = -1,

        Background = 0,
        Bottom = 1,

        Normal = 2,
        Top = 3,

        FullScreen = 4,
        Popup = 5,

        Highest = 6,
        Animation = 7,
        Tips = 8,

        Loading = 9,
        System = 10,
    }

    public enum UIGComType : int
    {
        GComponent = 0,
        Window = 1,
    }

    public enum UIType : int
    {
        NormalUI = 0,
        FullScreenUI = 1,
        ConfirmationUI = 2,
    }

    /// <summary>
    /// EnumKey枚举优化写法
    /// Struct继承IEquatable来优化
    /// </summary>
    public class EnumComparer_UILayerType : IEqualityComparer<UILayerType>
    {
        public static EnumComparer_UILayerType Instance = new EnumComparer_UILayerType();

        public bool Equals(UILayerType x, UILayerType y)
        {
            return (int)x == (int)y;
        }

        public int GetHashCode(UILayerType obj)
        {
            return (int)obj;
        }
    }
}