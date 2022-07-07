#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    [CustomPropertyDrawer(typeof(RenameField_Attribute))]
    public class RenameField_AttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            RenameField_Attribute att = (RenameField_Attribute)attribute;
            label.text = att.header;
            EditorGUI.BeginDisabledGroup(!att.isDisplay);
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.EndDisabledGroup();
        }
    }
}

#endif