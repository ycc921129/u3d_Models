using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class CheckNamespaceTool
    {
        private static List<string> namespaceList = new List<string>
        {
            "FutureCore.Data",
            "FutureCore.Protocol",

            "ProjectApp.Data",
            "ProjectApp.Protocol",

            "GamePlay.Data",
            "GamePlay.Protocol",
        };

        [MenuItem("[FC Project]/Check/检查命名空间约束", false, 0)]
        public static bool CheckNamespace()
        {
            Debug.Log("[CheckNamespaceTool]开始检查: 命名空间约束");

            bool result = true;
            Assembly assembly = Assembly.Load("Assembly-CSharp");
            foreach (var item in assembly.GetTypes())
            {
                //如果是这命名空间下的
                if (namespaceList.Contains(item.Namespace))
                {
                    if (!CheckTypeInNamespace(item))
                    {
                        result = false;
                    }
                }
            }

            Debug.Log("[CheckNamespaceTool]检查结束: 命名空间约束");
            return result;
        }

        private static bool CheckTypeInNamespace(Type type)
        {
            bool result = true;
            foreach (var field in type.GetFields())
            {
                //if (field.IsDefined(typeof(Newtonsoft.Json.JsonIgnoreAttribute),true))
                //{
                //    continue;
                //}

                // 引用类型
                if (!field.FieldType.IsValueType)
                {
                    if (field.FieldType.Name == "String")
                        continue;
                    if (field.FieldType.Name == "Object")
                        continue;
                    if (field.FieldType.Name == "ArrayList")
                        continue;

                    // 跳过基础类型
                    if (field.FieldType.Namespace == "System")
                        continue;
                    if (field.FieldType.Namespace == "UnityEngine")
                        continue;
                    if (typeof(IDictionary).IsAssignableFrom(field.FieldType)) //Dictionary
                    {
                        if (field.FieldType.GetGenericArguments().Length != 2)
                        {
                            Debug.LogError("字典键值为空:" + type.Name);
                            continue;
                        }

                        Type type0 = field.FieldType.GetGenericArguments()[0];
                        Type type1 = field.FieldType.GetGenericArguments()[1];
                        if (!CheckTypeInNamespace(type0) || !CheckTypeInNamespace(type1))
                        {
                            Debug.LogErrorFormat("类名{0}.字段名{1}的类型为{2},不在约束的命名空间下", type.Name, field.Name, field.FieldType.Name);
                            result = false;
                        }
                    }

                    else if (field.FieldType.IsGenericType) //List
                    {
                        Type subType = field.FieldType.GetGenericArguments()[0];
                        if (!CheckTypeInNamespace(subType))
                        {
                            if (subType.Namespace == "UnityEngine")
                                continue;
                            Debug.LogErrorFormat("类名{0}.字段名{1}的类型为{2},不在约束的命名空间下", type.Name, field.Name, field.FieldType.Name);
                            result = false;
                        }
                    }
                    else //普通引用类型
                    {
                        if (field.FieldType.Namespace == "UnityEngine")
                            continue;
                        if (!namespaceList.Contains(field.FieldType.Namespace))//如果不是这命名空间下的
                        {
                            Debug.LogErrorFormat("类名{0}.字段名{1}的类型为{2},不在约束的命名空间下", type.Name, field.Name, field.FieldType.Name);
                            result = false;
                            CheckTypeInNamespace(field.FieldType);
                        }
                        else
                        {
                            //CheckTypeInNamespace(field.FieldType);
                        }
                    }
                }
                // 值类型
                else
                {
                    if (field.FieldType.Namespace == "UnityEngine")
                        continue;
                    if (field.FieldType.Namespace == "CodeStage.AntiCheat.ObscuredTypes")
                        continue;

                    if (field.FieldType.IsEnum) //枚举
                    {
                        if (!namespaceList.Contains(field.FieldType.Namespace))//如果不是这命名空间下的
                        {
                            Debug.LogErrorFormat("类名{0}.字段名{1}的类型为{2},不在约束的命名空间下", type.Name, field.Name, field.FieldType.Name);
                            result = false;
                        }
                    }
                    else if (!field.FieldType.IsPrimitive)//结构体
                    {
                        if (field.FieldType.Name.StartsWith("KeyValuePair")) //KeyValuePair
                        {
                            if (field.FieldType.GetGenericArguments().Length != 2)
                            {
                                Debug.LogError("字典键值为空:" + type.Name);
                                continue;
                            }

                            Type type0 = field.FieldType.GetGenericArguments()[0];
                            Type type1 = field.FieldType.GetGenericArguments()[1];
                            if (!CheckTypeInNamespace(type0) || !CheckTypeInNamespace(type1))
                            {
                                Debug.LogErrorFormat("类名{0}.字段名{1}的类型为{2},不在约束的命名空间下", type.Name, field.Name, field.FieldType.Name);
                                result = false;
                            }
                        }
                        else
                        if (!namespaceList.Contains(field.FieldType.Namespace))//如果不是这命名空间下的
                        {
                            Debug.LogErrorFormat("类名{0}.字段名{1}的类型为{2},不在约束的命名空间下", type.Name, field.Name, field.FieldType.Name);
                            result = false;
                            CheckTypeInNamespace(field.FieldType);
                        }
                        else
                        {
                            CheckTypeInNamespace(field.FieldType);
                        }
                    }
                }
            }
            return result;
        }
    }
}