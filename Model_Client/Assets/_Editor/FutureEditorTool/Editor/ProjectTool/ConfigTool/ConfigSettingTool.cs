using FutureCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FutureEditor
{
    public static class ConfigSettingTool
    {
        /// <summary>
        /// 检查字段是否相同
        /// </summary>
        private abstract class CheckField
        {
            /// <summary>
            /// 代码字段名称
            /// </summary>
            public string name;

            public CheckField(string name)
            {
                this.name = name;
            }

            /// <summary>
            /// 是否需要替换
            /// </summary>
            public virtual bool isReplace(string line)
            {
                return IsField(line, name);
            }
        }

        /// <summary>
        /// 替换文本类
        /// </summary>
        private class ReplaceTextField : CheckField
        {
            public ReplaceTextField(string name) : base(name)
            {
            }

            public virtual void ReplaceJobject(JObject jobject, string line)
            {
                // 把 "" 去除
                line = line.Substring(1, line.Length - 2);
                jobject[name] = line;
            }
        }

        /// <summary>
        /// 指定名称替换字段
        /// </summary>
        private class ValueReplaceTextField : ReplaceTextField
        {
            public string jsonkeyName;

            public ValueReplaceTextField(string name, string jsonkeyName) : base(name)
            {
                this.jsonkeyName = jsonkeyName;
            }

            public override void ReplaceJobject(JObject jobject, string line)
            {
                line = line.Substring(1, line.Length - 2);
                jobject[jsonkeyName] = line;
            }
        }

        private class WebSocketUrlReplaceTextField : ReplaceTextField
        {
            private string releaseUrl;
            private string testUrl;

            public WebSocketUrlReplaceTextField(string name, string releaseUrl, string testUrl) : base(name)
            {
                this.releaseUrl = releaseUrl;
                this.testUrl = testUrl;
            }

            public override void ReplaceJobject(JObject jobject, string line)
            {
                string url = line.Substring(1, line.Length - 2);
                url = "https://" + url + "/";
                jobject[releaseUrl] = url;
                jobject[testUrl] = url;
            }
        }

        private class WebSocketPortReplaceTextField : ReplaceTextField
        {
            private string portName;

            public WebSocketPortReplaceTextField(string name, string portName) : base(name)
            {
                this.portName = portName;
            }

            public override void ReplaceJobject(JObject jobject, string line)
            {
                int cnt = line.Length;
                string str = line.Substring(1, cnt - 3);
                int index = str.LastIndexOf('/');
                int port = int.Parse(str.Substring(index + 1));
                JArray jr = JArray.FromObject(new string[] { port.ToString() });
                jobject[portName] = jr;
            }
        }

        private class ResetField : CheckField
        {
            public ResetField(string name) : base(name)
            {
            }

            public override bool isReplace(string line)
            {
                return line == name;
            }

            public virtual object ResetValue()
            {
                return null;
            }
        }

        private class ResetField<T> : ResetField
        {
            public ResetField(string name) : base(name)
            {
            }

            public override object ResetValue()
            {
                return default(T);
            }
        }

        //配置脚本所在的路径
        private static string ScriptPath = "Assets/_App/ProjectApp/App/AppFacade.cs";

        //配置真实路径
        private static string SaveSettingConfigPath = "../_Tool/ExcelTool/Setting/Setting.json";

        //字段替换器
        private static List<ReplaceTextField> replaceField = new List<ReplaceTextField>()
        {
            new ReplaceTextField(AppNameField),
            new ValueReplaceTextField(AppNameField,"AppFolderName"),
            new ReplaceTextField("AESKey"),
            new ReplaceTextField("AESIVector"),
            new WebSocketUrlReplaceTextField("Domain","ReleaseServerUrl","TestServerUrl"),
            new WebSocketPortReplaceTextField("WebSocketPort","ReleaseServerPorts"),
            new WebSocketPortReplaceTextField("WebSocketTestPort","TestServerPorts"),
        };

        private static List<ResetField> resetFields = new List<ResetField>(){
           new ResetField<int>("TestVerCode"),
           new ResetField<int>("ReleaseVerCode")
        };

        private const string AppNameField = "AppName";
        private static string[] Plattags = new string[] { "UNITY_ANDROID", "UNITY_IOS" };

        public static void Generate()
        {
            //检查文件是否存在
            if (!File.Exists(ScriptPath))
            {
                Debug.LogError("[ConfigSettingTool]生成失败: 不存在AppFacade.cs");
                return;
            }
            if (!File.Exists(SaveSettingConfigPath))
            {
                Debug.LogError("[ConfigSettingTool]生成失败: 不存在Setting.json");
                return;
            }

            FileStream saveConfigStream = null;
            FileStream scriptStream = null;
            try
            {
                saveConfigStream = new FileStream(SaveSettingConfigPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JArray jArray = ReadJsonConfig(saveConfigStream);
                if (jArray == null || jArray.Count == 0)
                {
                    Debug.LogError("[ConfigSettingTool]生成失败: 原始文件格式错误! 请检查！");
                    return;
                }
                JObject jobject = jArray[0] as JObject;

                //得到模板行
                //List<string> templateline = GetTemplate(readConfigStream);
                List<string> templateline = GetTemplates(saveConfigStream, 1)[0];

                scriptStream = new FileStream(ScriptPath, FileMode.Open, FileAccess.Read);
                //得到脚本
                List<string> appFacadeText = GetAllLine(scriptStream, true);
                List<string> source = new List<string>();
                source.Add("[{");
                //先把安卓搞定
                ReplaceConfig(appFacadeText, jobject, Plattags[0]);
                GetJObjectText(jobject, templateline, jArray, 0);
                source.AddRange(templateline);

                //检查IOS是否要生成配置
                //如果 ios 与 安卓 AppName 不一致 并且 ios.AppName != "solitaire"
                string iosAppName = ReadFieldValue(AppNameField, Plattags[1], appFacadeText);
                if (!string.IsNullOrEmpty(iosAppName) && iosAppName != "solitaire" && jobject[AppNameField].ToString() != iosAppName)
                {
                    source.Add("}");
                    source.Add(",");
                    source.Add("{");
                    ReplaceConfig(appFacadeText, jobject, Plattags[1]);
                    GetJObjectText(jobject, templateline, jArray, 1);
                    source.AddRange(templateline);
                }
                source.Add("}]");

                SaveFile(source, saveConfigStream);
                Debug.Log("[ConfigSettingTool]生成打表工具设置完成");
            }
            catch (Exception e)
            {
                Debug.LogError("[ConfigSettingTool]生成失败: 生成配置时引发异常:" + e.ToString());
            }
            finally
            {
                if (scriptStream != null)
                    scriptStream.Dispose();
                if (saveConfigStream != null)
                    saveConfigStream.Dispose();
            }
        }

        private static List<List<string>> GetTemplates(FileStream fl, int cnt = -1)
        {
            List<List<string>> result = new List<List<string>>();
            List<string> allLine = GetAllLine(fl, false);
            List<string> current = null;
            // [{  或者 { 开头
            // } 或者 }] 结尾
            int len = allLine.Count;
            for (int i = 0; i < len; i++)
            {
                string strline = allLine[i].Trim();
                if (strline == "[{" || strline == "{")//配置的开始
                {
                    current = new List<string>();
                    result.Add(current);
                    continue;
                }
                if (strline == "}" || strline == "}]")
                {
                    current = null;
                    if (cnt != -1 && result.Count >= cnt)
                        break;
                    continue;
                }
                if (current != null)
                    current.Add(allLine[i]);
            }
            return result;
        }

        private static List<string> GetTemplate(FileStream fl)
        {
            //测试首尾加空行
            List<string> result = GetAllLine(fl, false);
            //去除首尾空行 [{  }]
            int len = result.Count;
            List<int> delLine = new List<int>();
            for (int i = len - 1; i >= 0; i--)
            {
                delLine.Add(i);
                if (result[i].Contains("}]"))
                    break;
            }
            int insertIdx = delLine.Count;
            for (int i = 0; i < len; i++)
            {
                delLine.Insert(insertIdx, i);
                if (result[i].Contains("[{"))
                    break;
            }
            foreach (var item in delLine)
            {
                result.RemoveAt(item);
            }
            return result;
        }

        /// <summary>
        /// 遍历脚本列表
        /// </summary>
        private static void ForEachList(List<string> allScript, string beginPlatTag, Func<string, bool> action)
        {
            bool isForEachList = false;
            int len = allScript.Count;
            for (int i = 0; i < len; i++)
            {
                if (allScript[i].IndexOf(beginPlatTag) > -1)
                    isForEachList = true;
                else if (isForEachList)
                {
                    if (action(allScript[i]))
                    {
                        return;
                    }
                }
            }
        }

        private static void ReplaceConfig(List<string> allScript, JObject jObject, string PlatTag)
        {
            var tempReplaceFiled = new List<ReplaceTextField>(replaceField);
            Func<string, bool> action = (line) =>
            {
                //找到要替换的字段
                int cnt = tempReplaceFiled.Count;
                for (int j = cnt - 1; j >= 0; j--)
                {
                    if (tempReplaceFiled[j].isReplace(line))
                    {
                        tempReplaceFiled[j].ReplaceJobject(jObject, GetFieldValue(line));
                        tempReplaceFiled.RemoveAt(j);
                    }
                }
                if (tempReplaceFiled.Count <= 0)
                    return true;
                return false;
            };

            ForEachList(allScript, PlatTag, action);
        }

        /// <summary>
        /// 获取字段的值
        /// </summary>
        private static string GetFieldValue(string line)
        {
            string[] spl = line.Split('=');
            if (spl == null || spl.Length != 2)
                return null;
            spl[1] = spl[1].Trim();
            //把 ; 字符去除
            spl[1] = spl[1].Substring(0, spl[1].Length - 1);
            return spl[1];
        }

        /// <summary>
        /// 检查这一行是否是这个字段
        /// </summary>
        private static bool IsField(string line, string fieldName)
        {
            if (line.IndexOf(fieldName) < 0)
                return false;
            string str = line;
            int index = line.IndexOf('=');
            if (index < 0)
                return false;
            //得到等号左边的值
            line = line.Substring(0, index).Trim();
            index = line.LastIndexOf(' ');
            if (index < 0)
                return false;
            string tempName = line.Substring(index + 1);
            return tempName == fieldName;
        }

        /// <summary>
        /// 获取字段名和字段值，当这一行不是字段就返回 null 当字段值是字符串时，返回不带引号字符串
        /// </summary>
        private static System.Tuple<string, string> GetField(string line)
        {
            int index = line.IndexOf('=');
            if (index < 0)
                return null;
            //计算字段值
            string fieldValue = line.Substring(index + 1);
            fieldValue = fieldValue.Trim();
            fieldValue = fieldValue.Substring(0, fieldValue.Length - 1);    //去除 ;
                                                                            //计算字段名
            string fieldName = line.Substring(0, index);
            fieldName = fieldName.Trim();
            index = fieldName.LastIndexOf(' ');
            fieldName = fieldName.Substring(index + 1);
            return new System.Tuple<string, string>(fieldName, fieldValue);
        }

        /// <summary>
        /// 读取json配置
        /// </summary>
        /// <returns></returns>
        private static JArray ReadJsonConfig(FileStream fl)
        {
            string jsontext = null;
            fl.Position = 0;
            StreamReader sr = new StreamReader(fl);
            jsontext = sr.ReadToEnd();
            JArray ja = null;
            try
            {
                ja = SerializeUtil.ToObject<JArray>(jsontext);
            }
            catch
            {
                ja = null;
            }
            return ja;
        }

        /// <summary>
        /// 获取流的所有行
        /// </summary>
        private static List<string> GetAllLine(FileStream fl, bool delNote)
        {
            fl.Position = 0;
            List<string> result = new List<string>();
            StreamReader sr = new StreamReader(fl);
            while (true)
            {
                var line = sr.ReadLine();
                if (line == null)
                    break;
                if (delNote)
                {
                    line = line.Trim();
                    if (!string.IsNullOrEmpty(line))
                    {
                        //删除空行
                        result.Add(line);
                    }
                }
                else
                    result.Add(line);
            }
            return result;
        }

        private static string GetJTokenString(string name, JToken jToken)
        {
            string value = SerializeUtil.ToJson(jToken);
            return string.Format("\"{0}\": {1},", name, value);
        }

        //根据模板行生成文本
        private static void GetJObjectText(JObject jobject, List<string> templateline, JArray jArr, int index)
        {
            List<ResetField> ignorefields = new List<ResetField>(resetFields);
            if (index < jArr.Count)
            {
                jobject = jArr[index] as JObject;
                ignorefields.Clear();
            }

            int lineindex = 0;
            string itemkey = null;
            JToken itemvalue = null;
            foreach (var jobjItem in jobject)
            {
                while (true)
                {
                    if (lineindex >= templateline.Count)
                        break;

                    if (templateline[lineindex].IndexOf(jobjItem.Key) >= 0)
                    {
                        itemkey = jobjItem.Key;
                        itemvalue = jobjItem.Value;
                        //计算是否忽略
                        for (int i = 0; i < ignorefields.Count; i++)
                        {
                            if (ignorefields[i].isReplace(itemkey))
                            {
                                itemvalue = JToken.FromObject(ignorefields[i].ResetValue());
                                ignorefields.RemoveAt(i);
                                break;
                            }
                        }

                        //计算空格的数量
                        string temp = templateline[lineindex].TrimStart();
                        int cnt = templateline[lineindex].Length - temp.Length;
                        string jsontext = GetJTokenString(itemkey, itemvalue);//.ToString();
                        while (cnt-- > 0)
                        {
                            jsontext = " " + jsontext;
                        }
                        templateline[lineindex++] = jsontext;
                        break;
                    }
                    lineindex++;
                }
            }
            lineindex--;
            templateline[lineindex] = templateline[lineindex].Substring(0, templateline[lineindex].Length - 1);
        }

        /// <summary>
        /// 到脚本文本中查找一个字段值
        /// </summary>
        private static string ReadFieldValue(string fileName, string platTag, List<string> scriptText)
        {
            int cnt = scriptText.Count;
            string result = null;
            Func<string, bool> action = (line) =>
            {
                if (IsField(line, fileName))
                {
                    string str = line;
                    string[] strs = str.Split('=');
                    strs[1] = strs[1].Trim();
                    result = strs[1].Substring(1, strs[1].Length - 3);
                    return true;
                }
                return false;
            };

            ForEachList(scriptText, platTag, action);
            return result;
        }

        private static void SaveFile(List<string> allText, FileStream fl)
        {
            fl.Seek(0, SeekOrigin.Begin);
            fl.SetLength(0);
            StreamWriter sw = new StreamWriter(fl);
            foreach (var line in allText)
            {
                sw.WriteLine(line);
                sw.Flush();
            }
        }
    }
}