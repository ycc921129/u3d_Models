#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    [CustomPropertyDrawer(typeof(EnumRename_Attribute))]
    public class EnumRename_AttributeDrawer : PropertyDrawer
    {
        private readonly List<string> m_displayNames = new List<string>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnumRename_Attribute att = (EnumRename_Attribute)attribute;
            Type type = property.serializedObject.targetObject.GetType();
            FieldInfo field = type.GetField(property.name);
            Type enumtype = field.FieldType;
            foreach (string enumName in property.enumNames)
            {
                FieldInfo enumfield = enumtype.GetField(enumName);
                object[] hds = enumfield.GetCustomAttributes(typeof(EnumRename_Attribute), false);
                m_displayNames.Add(hds.Length <= 0 ? enumName : ((EnumRename_Attribute)hds[0]).header);
            }
            EditorGUI.BeginChangeCheck();
            int value = EditorGUI.Popup(position, att.header, property.enumValueIndex, m_displayNames.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                property.enumValueIndex = value;
            }
        }
    }
}

#endif