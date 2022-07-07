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
             * ����ö�ٸ�ѡ�� �� 0-Nothing��-1-Everything,������ö��֮��
             * ö��ֵ��2��x���ݣ���2��0����=1��2��1����=2��2��2����=4��8��16...
             */
            property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
        }
    }
}

#endif