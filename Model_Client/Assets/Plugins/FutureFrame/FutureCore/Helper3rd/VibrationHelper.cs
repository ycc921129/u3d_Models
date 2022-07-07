/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using MoreMountains.NiceVibrations;
using UnityEngine;

namespace FutureCore
{
    public static class VibrationHelper
    {
        private static bool isOpenVibration = true;

        public static bool IsOpenVibration
        {
            get
            {
                return isOpenVibration;
            }
            set
            {
                isOpenVibration = value;
                PrefsUtil.WriteBool(PrefsKeyConst.Common_isOpenVibration, isOpenVibration);
            }
        }

        /// <summary>
        /// SDK震动
        /// 轻震动的参数: 15, 1
        /// </summary>
        /// <param name="milliseconds">震动的持续时间</param>
        /// <param name="amplitude">震动强度 1-255</param>
        public static void PlatformVibrate(long milliseconds = 15, int amplitude = 1)
        {
            if (milliseconds == 0 || amplitude == 0)
            {
                return;
            }
            if (isOpenVibration)
            {
                Channel.Current.vibrate(milliseconds, amplitude);
            }
        }

        public static void PlatformCancelVibrate()
        {
            if (isOpenVibration)
                Channel.Current.virateCancel();
        }

        public static void UnityVibrate()
        {
#if !UNITY_STANDALONE
            if (isOpenVibration)
                Handheld.Vibrate();
#endif
        }

        /// <summary>
        /// 触发简单的震动
        /// </summary>
        public static void Vibrate()
        {
            if (isOpenVibration)
                MMVibrationManager.Vibrate();
        }

        /// <summary>
        /// 触发指定类型的触觉反馈
        /// </summary>
        public static void Haptic(HapticTypes hapticType)
        {
            if (isOpenVibration)
                MMVibrationManager.Haptic(hapticType);
        }

        public static void HapticLv0()
        {
            Haptic(HapticTypes.None);
        }

        public static void HapticLv1()
        {
            Haptic(HapticTypes.Selection);
        }

        public static void HapticLv2()
        {
            Haptic(HapticTypes.Success);
        }

        public static void HapticLv3()
        {
            Haptic(HapticTypes.Warning);
        }

        public static void HapticLv4()
        {
            Haptic(HapticTypes.Failure);
        }

        public static void HapticLv5()
        {
            Haptic(HapticTypes.LightImpact);
        }

        public static void HapticLv6()
        {
            Haptic(HapticTypes.MediumImpact);
        }

        public static void HapticLv7()
        {
            Haptic(HapticTypes.HeavyImpact);
        }
    }
}