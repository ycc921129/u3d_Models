#if UNITY_EDITOR

/*
 * 
 * [System.Flags]
 * public enum TestEnum
 * {
 *     One = 1,
 *     Two = 2,
 *     Three = 4,
 *     Four = 8,
 * }
 * 
 */

using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    [CustomPropertyDrawer(typeof(BitFieldFlags_Attribute))]
    public class BitFieldFlags_AttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            /*
             * 绘制枚举复选框 ， 0-Nothing，-1-Everything,其他是枚举之和
             * 枚举值（2的x次幂）：2的0次幂=1，2的1次幂=2，2的2次幂=4，8，16...
             */
            property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
        }
    }
}

#endif