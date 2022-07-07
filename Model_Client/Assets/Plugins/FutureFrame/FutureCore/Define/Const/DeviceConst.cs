/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace FutureCore
{
    public static class DeviceConst
    {
        private static Thread EngineMainThread = Thread.CurrentThread;

        /// <summary>
        /// 是否跑在主线程
        /// </summary>
        public static bool IsRunInMainThread
        {
            get
            {
                return Thread.CurrentThread == EngineMainThread;
            }
        }

        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public static string DeviceIdentifier
        {
            get
            {
                return SystemInfo.deviceUniqueIdentifier;
            }
        }

        /// <summary>
        /// 设备模式
        /// </summary>
        public static string DeviceModel
        {
            get
            {
#if UNITY_IOS && !UNITY_EDITOR
                return Device.generation.ToString();
#else
                return SystemInfo.deviceModel;
#endif
            }
        }

        /// <summary>
        /// GPU是否支持调用实例化
        /// </summary>
        public static bool IsSupportsInstancing
        {
            get
            {
                return SystemInfo.supportsInstancing;
            }
        }

        /// <summary>
        /// 图形设备名
        /// </summary>
        public static string GraphicsDeviceName
        {
            get
            {
                return SystemInfo.graphicsDeviceName;
            }
        }

        /// <summary>
        /// 图形设备类型
        /// </summary>
        public static GraphicsDeviceType GraphicsDeviceType
        {
            get
            {
                return SystemInfo.graphicsDeviceType;
            }
        }

        /// <summary>
        /// 网络是否可达的
        /// </summary>
        public static bool IsCanNetAvailable
        {
            get
            {
                return Application.internetReachability != NetworkReachability.NotReachable;
            }
        }

        /// <summary>
        /// 网络通过运营商数据网络是可达的
        /// </summary>
        public static bool IsCarrierDataNetwork
        {
            get
            {
                return Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork;
            }
        }

        /// <summary>
        /// 网络通过WiFi或有线网络是可达的
        /// </summary>
        public static bool IsLocalAreaNetwork
        {
            get
            {
                return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
            }
        }

        /// <summary>
        /// 是否支持OpenGL3
        /// </summary>
        public static bool IsSupportOpenGLES3
        {
            get
            {
                return SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3;
            }
        }

        /// <summary>
        /// 是否支持OpenGL2
        /// </summary>
        public static bool IsSupportOpenGLES2
        {
            get
            {
                return SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2;
            }
        }

        /// <summary>
        /// 是否是安卓模拟器
        /// </summary>
        public static bool IsAndroidSimulator
        {
            get
            {
                return SystemInfo.graphicsDeviceName.Contains("MuMu");
            }
        }
    }
}