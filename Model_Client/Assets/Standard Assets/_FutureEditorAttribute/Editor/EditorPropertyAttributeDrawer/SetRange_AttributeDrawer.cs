#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    [CustomPropertyDrawer(typeof(SetRange_Attribute))]
    public class SetRange_AttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SetRange_Attribute myRange = attribute as SetRange_Attribute;
            label.text = myRange.headerName;
            if (property.propertyType == SerializedPropertyType.Integer)
            {
                EditorGUI.IntSlider(position, property, (int)myRange.min, (int)myRange.max, label);
            }
            else if (property.propertyType == SerializedPropertyType.Float)
            {
                EditorGUI.Slider(position, property, myRange.min, myRange.max, label);
            }
        }
    }
}

#endif