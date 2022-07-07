#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    [CustomPropertyDrawer(typeof(CustomLabel_Attribute))]
    public class CustomLabel_AttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string value;
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    value = property.intValue.ToString();
                    break;
                case SerializedPropertyType.Boolean:
                    value = property.boolValue.ToString();
                    break;
                case SerializedPropertyType.Float:
                    value = property.floatValue.ToString();
                    break;
                case SerializedPropertyType.String:
                    value = property.stringValue;
                    break;
                case SerializedPropertyType.Color:
                    value = property.colorValue.ToString();
                    break;
                case SerializedPropertyType.Vector3:
                    value = property.vector3Value.ToString();
                    break;
                case SerializedPropertyType.Rect:
                    value = property.rectValue.ToString();
                    break;
                default:
                    throw new System.Exception();
            }

            CustomLabel_Attribute thisAttribute = (CustomLabel_Attribute)attribute;

            Color tempColor = GUI.color;
            GUI.color = thisAttribute.textColor;
            EditorGUI.LabelField(position, value);
            GUI.color = tempColor;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }
    }
}

#endif