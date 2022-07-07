/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace FutureCore
{
    /// <summary>
    /// Newtonsoft.Json特性:
    /// [JsonIgnoreAttribute] 忽略字段
    /// [JsonCustomSerializationAttribute] 自定义序列化
    /// [JsonDisplayAttribute] 显式字段
    /// [JsonConverter] 自定义转换器
    /// 
    /// Newtonsoft.Json:
    /// JObject 用于操作JSON对象
    /// JArray 用语操作JSON数组
    /// JValue 表示数组中的值
    /// JProperty 表示对象中的属性, 以"key/value"形式
    /// JToken 用于存放Linq to JSON查询后的结果
    /// 
    /// LitJson:
    /// JsonData 用于操作JSON数据
    /// </summary>
    public static class SerializeUtil
    {
        private static Type StringType = typeof(string);

        static SerializeUtil()
        {
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                return DefaultUseJsonSettings;
            });
        }

        private static JsonSerializerSettings DefaultUseJsonSettings = new JsonSerializerSettings
        {
            // 不缩进
            Formatting = Formatting.None,

            // 日期类型默认格式化处理
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            // 日期类型默认格式化处理
            DateFormatString = "yyyy/MM/dd hh:mm:ss",

            // 忽略其引用循环值
            //ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            // 忽略空值
            //NullValueHandling = NullValueHandling.Ignore,
            // 忽略默认值 (需同端设置)
            //DefaultValueHandling = DefaultValueHandling.Ignore,

            // 自定义转换器组
            //Converters = new List<JsonConverter>() { new JsonConverter_Vector2(), new JsonConverter_Vector3(), new JsonConverter_Vector4() },
        };

        private static JsonSerializerSettings JsonIndentedSettings = new JsonSerializerSettings
        {
            // 带缩进格式
            Formatting = Formatting.Indented,

            // 日期类型默认格式化处理
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            // 日期类型默认格式化处理
            DateFormatString = "yyyy/MM/dd hh:mm:ss",

            // 忽略其引用循环值
            //ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            // 忽略空值
            //NullValueHandling = NullValueHandling.Ignore,
            // 忽略默认值 (需同端设置)
            //DefaultValueHandling = DefaultValueHandling.Ignore,

            // 自定义转换器组
            //Converters = new List<JsonConverter>() { new JsonConverter_Vector2(), new JsonConverter_Vector3(), new JsonConverter_Vector4() },
        };

        private static JsonSerializer JObjectJsonSerializer = new JsonSerializer
        {
            // 不缩进
            Formatting = Formatting.None,

            // 日期类型默认格式化处理
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            // 日期类型默认格式化处理
            DateFormatString = "yyyy/MM/dd hh:mm:ss",

            // 忽略其引用循环值
            //ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            // 忽略空值
            //NullValueHandling = NullValueHandling.Ignore,
            // 忽略默认值 (需同端设置)
            //DefaultValueHandling = DefaultValueHandling.Ignore,
        };

        public static string ToRawJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, StringType, DefaultUseJsonSettings);
        }

        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, DefaultUseJsonSettings);
        }

        public static string ToJsonIndented(object obj)
        {
            return JsonConvert.SerializeObject(obj, JsonIndentedSettings);
        }

        public static string ToJson(object obj, Type type)
        {
            return JsonConvert.SerializeObject(obj, type, DefaultUseJsonSettings);
        }

        public static string ToJsonIndented(object obj, Type type)
        {
            return JsonConvert.SerializeObject(obj, type, JsonIndentedSettings);
        }

        public static string ToJson<T>(object obj)
        {
            return ToJson(obj, typeof(T));
        }

        public static string ToJsonIndented<T>(object obj)
        {
            return JsonConvert.SerializeObject(obj, typeof(T), JsonIndentedSettings);
        }

        public static Dictionary<string, object> ToDicLocal(string json)
        {
            JObject o_jobject = GetJObjectByJson(json);
            if (o_jobject == null || o_jobject["value"] == null)
            {
                LogUtil.LogError("[SerializeUtil] ToDicLocal is null.");
                return null;
            }
            string dicJson = o_jobject["value"].ToString();
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(dicJson);
        }
        public static Dictionary<string, object> ToDicClient(string json)
        {
            JObject o_jobject = GetJObjectByJson(json);
            if (o_jobject == null || o_jobject["version"] == null)
            {
                LogUtil.LogError("[SerializeUtil] ToDicClient is null.");
                return null;
            }
            string dicJson = o_jobject.ToString();
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(dicJson);
        }

        public static T ToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T ReplaceJobject<T>(T obj, JObject serverJson)
        {
            JObject o_jobject = GetJObjectByObject(obj);
            string newJson = ReplaceJObject(o_jobject, serverJson);
            return ToObject<T>(newJson);
        }

        private static string ReplaceJObject(JObject o_jobject, JObject serverJson)
        {
            foreach (JProperty newItem in o_jobject.Children())
            {
                string newKey = newItem.Name;
                JToken newValue = newItem.Value;
                if (serverJson[newKey] != null)
                {
                    newValue = serverJson[newKey];
                    newItem.Value = newValue;
                }
            }
            return o_jobject.ToString();
        }

        public static object ToObject(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        public static JObject GetJObjectByJson(string json)
        {
            return JObject.Parse(json);
        }

        public static JObject GetJObjectByObject(object obj)
        {
            return JObject.FromObject(obj, JObjectJsonSerializer);
        }

        public static string UnityToJson(object obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public static object UnityToObject(string json)
        {
            return JsonUtility.FromJson<object>(json);
        }
    }

    #region 自定义Json格式转换器
    public class JsonConverter_Vector2 : JsonConverter
    {
        private static Type CurrType = typeof(Vector2);

        public override bool CanConvert(Type objectType)
        {
            return objectType == CurrType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Vector2 v = (Vector2)value;
            serializer.Serialize(writer, "(" + v.x + "," + v.y + ")");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string[] vStr = reader.Value.ToString().Split(',');
            vStr[0] = vStr[0].Replace("(", string.Empty);
            vStr[1] = vStr[1].Replace(")", string.Empty);
            return new Vector2(float.Parse(vStr[0]), float.Parse(vStr[1]));
        }
    }

    public class JsonConverter_Vector3 : JsonConverter
    {
        private static Type CurrType = typeof(Vector3);

        public override bool CanConvert(Type objectType)
        {
            return objectType == CurrType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Vector3 v = (Vector3)value;
            serializer.Serialize(writer, "(" + v.x + "," + v.y + "," + v.z + ")");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string[] vStr = reader.Value.ToString().Split(',');
            vStr[0] = vStr[0].Replace("(", string.Empty);
            vStr[1] = vStr[1];
            vStr[2] = vStr[2].Replace(")", string.Empty);
            return new Vector3(float.Parse(vStr[0]), float.Parse(vStr[1]), float.Parse(vStr[2]));
        }
    }

    public class JsonConverter_Vector4 : JsonConverter
    {
        private static Type CurrType = typeof(Vector4);

        public override bool CanConvert(Type objectType)
        {
            return objectType == CurrType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Vector4 v = (Vector4)value;
            serializer.Serialize(writer, "(" + v.x + "," + v.y + "," + v.z + "," + v.w + ")");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string[] vStr = reader.Value.ToString().Split(',');
            vStr[0] = vStr[0].Replace("(", string.Empty);
            vStr[1] = vStr[1];
            vStr[2] = vStr[2];
            vStr[3] = vStr[3].Replace(")", string.Empty);
            return new Vector4(float.Parse(vStr[0]), float.Parse(vStr[1]), float.Parse(vStr[2]), float.Parse(vStr[3]));
        }
    }
    #endregion
}