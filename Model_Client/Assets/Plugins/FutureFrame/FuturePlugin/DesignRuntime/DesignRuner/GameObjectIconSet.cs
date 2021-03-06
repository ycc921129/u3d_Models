using System;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR

namespace FuturePlugin
{
    public static class GameObjectIconSet
    {
        #region 数据定义
        private static GUIContent[] labelIcons;
        private static GUIContent[] largeIcons;

        /// <summary>
        /// Label类型icon 显示文字的
        /// </summary>
        public enum LabelIcon
        {
            Gray = 0,
            Blue,
            Teal,
            Green,
            Yellow,
            Orange,
            Red,
            Purple
        }

        /// <summary>
        /// 其他icon不显示文字
        /// </summary>
        public enum Icon
        {
            CircleGray = 0,
            CircleBlue,
            CircleTeal,
            CircleGreen,
            CircleYellow,
            CircleOrange,
            CircleRed,
            CirclePurple,
            DiamondGray,
            DiamondBlue,
            DiamondTeal,
            DiamondGreen,
            DiamondYellow,
            DiamondOrange,
            DiamondRed,
            DiamondPurple
        }
        #endregion

        #region 外部接口
        public static void SetIcon(GameObject goObj, LabelIcon icon)
        {
            if (labelIcons == null)
            {
                labelIcons = GetTextures("sv_label_", string.Empty, 0, 8);
            }
            SetIcon(goObj, labelIcons[(int)icon].image as Texture2D);
        }

        public static void SetIcon(GameObject goObj, Icon icon)
        {
            if (largeIcons == null)
            {
                largeIcons = GetTextures("sv_icon_dot", "_pix16_gizmo", 0, 16);
            }
            SetIcon(goObj, largeIcons[(int)icon].image as Texture2D);
        }

        private static void SetIcon(GameObject goObj, Texture2D texture)
        {
            var ty = typeof(EditorGUIUtility);
            var mi = ty.GetMethod("SetIconForObject", BindingFlags.NonPublic | BindingFlags.Static);
            mi.Invoke(null, new object[] { goObj, texture });
        }
        #endregion

        #region 内部
        private static GUIContent[] GetTextures(string baseName, string postFix, int startIndex, int count)
        {
            GUIContent[] guiContentArray = new GUIContent[count];

            var t = typeof(EditorGUIUtility);
            var mi = t.GetMethod("IconContent", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null);

            for (int index = 0; index < count; ++index)
            {
                guiContentArray[index] = mi.Invoke(null, new object[] { baseName + (object)(startIndex + index) + postFix }) as GUIContent;
            }
            return guiContentArray;
        }
        #endregion
    }
}

#endif