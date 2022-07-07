/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
    [Flags]
    public enum Roles
    {
        [Description("描述")]
        Admin    = 1 << 0,
        [Description("描述")]
        Member   = 1 << 1,
        [Description("描述")]
        Manager  = 1 << 2,
        [Description("描述")]
        Operator = 1 << 3
    }
 */

using System;
using System.ComponentModel;
using System.Reflection;

namespace FutureCore
{
    public static class EnumUtil
    {
        /// <summary>
        /// 获取枚举特效描述值
        /// </summary>
        public static string GetDesc(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute != null)
            {
                return attribute.Description;
            }
            return null;
        }

        /// <summary>
        /// 解析枚举
        /// </summary>
        public static T Parse<T>(string enumName)
        {
            T style = (T)Enum.Parse(typeof(T), enumName);
            return style;
        }

        /// <summary>
        /// 获取枚举值的类型
        /// </summary>
        public static Type GetEnumValueType(Type enumType)
        {
            return Enum.GetUnderlyingType(enumType);
        }

        /// <summary>
        /// 获取枚举的所有名称
        /// </summary>
        public static string[] GetEnumAllName(Type enumType)
        {
            return Enum.GetNames(enumType);
        }
    }
}