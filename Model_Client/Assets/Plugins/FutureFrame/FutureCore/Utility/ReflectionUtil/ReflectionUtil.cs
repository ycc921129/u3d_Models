/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace FutureCore
{
    public static class ReflectionUtil
    {
        /// <summary>
        /// 获取所有特性
        /// </summary>
        public static List<Type> GetAllAttributesType(Type attributesType, string assemblyName)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Assembly assembly = null;
            for (int i = 0; i < assemblies.Length; i++)
            {
                Assembly assemblie = assemblies[i];
                if (assemblie.GetName(false).Name == assemblyName)
                {
                    assembly = assemblie;
                    break;
                }
            }

            if (assembly == null)
            {
                return null;
            }

            List<Type> getTypes = null;
            foreach (Type type in assembly.GetTypes())
            {
                object[] objects = type.GetCustomAttributes(attributesType, false);
                if (objects.Length == 0)
                {
                    continue;
                }
                if (getTypes == null)
                {
                    getTypes = new List<Type>();
                }
                getTypes.Add(type);
            }
            return getTypes;
        }

        /// <summary>
        /// 反射调用静态委托方法
        /// </summary>
        public static object InvokeMethod(Type methodInType, string methodName, object[] param = null)
        {
            FieldInfo methodField = methodInType.GetField(methodName, BindingFlags.Public | BindingFlags.Static);
            if (methodField == null) return null;

            object methodObject = methodField.GetValue(null);
            if (methodObject == null) return null;

            MethodInfo handlerMethod = methodObject.GetType().GetMethod("Invoke");
            if (handlerMethod == null) return null;

            return handlerMethod.Invoke(methodObject, param);
        }
    }
}