/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

#if UNITY_EDITOR

using System.Reflection;

namespace FuturePlugin
{
    public static class ReflectionDebugTester
    {
        public static object GetFieldValue(this object obj, string name)
        {
            FieldInfo info = obj.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            if (info != null)
            {
                return info.GetValue(obj);
            }
            return null;
        }

        public static object GetStaticFieldValue(this object obj, string name)
        {
            FieldInfo info = obj.GetType().GetField(name, BindingFlags.Static | BindingFlags.NonPublic);
            if (info != null)
            {
                return info.GetValue(obj);
            }
            return null;
        }

        public static FieldInfo SetFieldValue(this object obj, string name, object val)
        {
            FieldInfo info = obj.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            if (info != null)
            {
                info.SetValue(obj, val);
            }
            return info;
        }

        public static FieldInfo SetStaticFieldValue(this object obj, string name, object val)
        {
            FieldInfo info = obj.GetType().GetField(name, BindingFlags.Static | BindingFlags.NonPublic);
            if (info != null)
            {
                info.SetValue(obj, val);
            }
            return info;
        }

        public static PropertyInfo SetPropertyValue(this object obj, string name, object val)
        {
            PropertyInfo info = obj.GetType().GetProperty(name);
            if (info != null)
            {
                info.SetValue(obj, val, null);
            }
            return info;
        }

        public static MethodInfo ExecuteMethod(this object obj, string name, object[] parameters)
        {
            MethodInfo info = obj.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic);
            if (info != null)
            {
                info.Invoke(obj, parameters);
            }
            return info;
        }
    }
}

#endif