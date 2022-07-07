#if UNITY_EDITOR

using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    [CustomPropertyDrawer(typeof(InspectorButton_Attribute))]
    public class InspectorButton_AttributeDrawer : PropertyDrawer
    {
        private const int height = 5;

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            return base.GetPropertyHeight(prop, label) + height;
        }

        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {
            InspectorButton_Attribute att = (InspectorButton_Attribute)attribute;
            if (GUI.Button(position, att.headerName))
            {
                string methodName = fieldInfo.Name.Replace("_TestBtn", string.Empty);
                Object cs = prop.serializedObject.targetObject;
                MethodInfo mi = cs.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (null != mi)
                {
                    mi.Invoke(cs, null);
                }
                else
                {
                    Debug.LogErrorFormat("[InspectorButton]找不到{0}的绑定方法", methodName);
                }
            }
        }
    }
}

#endif