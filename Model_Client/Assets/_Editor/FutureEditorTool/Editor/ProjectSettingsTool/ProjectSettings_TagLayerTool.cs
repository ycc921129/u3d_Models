using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using FutureCore;
using System.IO;
using System.Text;
using Newtonsoft.Json;

#if UNITY_EDITOR

using UnityEditor;
using UnityEditorInternal;

#endif

#if UNITY_EDITOR

namespace FutureEditor
{
    public static class ProjectSettings_TagLayerTool
    {
        [Serializable]
        private class ProjectSetting
        {
            public List<string> tags = new List<string>();
            public List<string> sortLayers = new List<string>();
            public List<string> layers = new List<string>();

            public ProjectSetting()
            {
                InitProjectSetting();
            }

            private void InitProjectSetting()
            {
                tags = InternalEditorUtility.tags.ToList();
                layers = InternalEditorUtility.layers.ToList();
                SortingLayer[] sortLayers = SortingLayer.layers;
                for (int i = 0; i < sortLayers.Length; i++)
                {
                    SortingLayer layer = sortLayers[i];
                    this.sortLayers.Add(layer.name);
                }
            }

            public void DeleteRepeat()
            {
                List<string> list = new List<string>();
                foreach (string tag in tags)
                {
                    if (!list.Contains(tag))
                        list.Add(tag);
                }
                tags = list;

                list = new List<string>();
                foreach (string layer in layers)
                {
                    if (!list.Contains(layer))
                        list.Add(layer);
                }
                layers = list;

                list = new List<string>();
                foreach (string layer in sortLayers)
                {
                    if (!list.Contains(layer))
                        list.Add(layer);
                }
                sortLayers = list;
                list = null;
            }
        }

        #region 模板字符串

        public const string ProjectSettingEnumTemp =
@"/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectApp
{
    public enum ProjectTag : int
    {
[Flag]ProjectTag
    }

    public enum ProjectSortLayer : int
    {
[Flag]ProjectSortLayer
    }

    public enum ProjectLayer : byte
    {
[Flag]ProjectLayer
    }
}";

        #endregion 模板字符串

        private static string folderPath = Application.dataPath + "/" + "Editor/ProjectSettings/";
        private static string jsonPath = folderPath + string.Format("{0}.json", "TagLayerSettings");
        private static string projectCodeFolder = Application.dataPath + "/" + "_App/AutoCreator/AutoRegister/ProjectSettings/";
        private static string projectSettingEnumPath = projectCodeFolder + "ProjectSettingsTagLayer_AutoCreator.cs";

        [MenuItem("[FC Project]/ProjectSettings/设置TagLayer", false, 0)]
        public static void Set()
        {
            try
            {
                if (!Directory.Exists(projectCodeFolder))
                {
                    Directory.CreateDirectory(projectCodeFolder);
                }

                if (!File.Exists(jsonPath))
                {
                    AutoCreateJson();
                }

                string jsonStr = File.ReadAllText(jsonPath);
                ProjectSetting setting = SerializeUtil.ToObject<ProjectSetting>(jsonStr);
                setting.DeleteRepeat();

                foreach (string tag in setting.tags)
                {
                    if (!InternalEditorUtility.tags.Contains(tag))
                    {
                        InternalEditorUtility.AddTag(tag);
                    }
                }

                foreach (string layer in setting.layers)
                {
                    AddLayer(layer);
                }

                foreach (string layer in setting.sortLayers)
                {
                    AddSortLayer(layer);
                }

                setting = new ProjectSetting();
                CreateProjectEnumFile(setting);

                Debug.Log("[ProjectSettings_TagLayerTool]设置TagLayer成功");
            }
            catch (Exception e)
            {
                LogUtil.LogError("[ProjectSettings_TagLayerTool]加载失败: " + e.ToString());
            }
        }

        private static void AutoCreateJson()
        {
            ProjectSetting setting = new ProjectSetting();
            string jsonStr = SerializeUtil.ToJson(setting);
            jsonStr = ConvertJsonString(jsonStr);
            File.WriteAllText(jsonPath, jsonStr);
        }

        /// <summary>
        /// 格式化Json
        /// </summary>
        private static string ConvertJsonString(string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }

        private static void CreateProjectEnumFile(ProjectSetting setting)
        {
            int idx = 0;
            string path = projectSettingEnumPath;
            StringBuilder sb = new StringBuilder(ProjectSettingEnumTemp);
            StringBuilder replace = new StringBuilder();

            idx = 0;
            foreach (string tag in setting.tags)
            {
                string str = tag.Replace(" ", "");
                if (tag.Equals(setting.tags[setting.tags.Count - 1]))
                    replace.Append(string.Format("        {0} = {1},", str, idx));
                else
                    replace.AppendLine(string.Format("        {0} = {1},", str, idx));
                idx++;
            }
            sb.Replace("[Flag]ProjectTag", replace.ToString());
            replace.Clear();

            idx = 0;
            foreach (string layer in setting.sortLayers)
            {
                string str = layer.Replace(" ", "");
                if (layer.Equals(setting.sortLayers[setting.sortLayers.Count - 1]))
                    replace.Append(string.Format("        {0} = {1},", str, idx));
                else
                    replace.AppendLine(string.Format("        {0} = {1},", str, idx));
                idx++;
            }
            sb.Replace("[Flag]ProjectSortLayer", replace.ToString());
            replace.Clear();

            idx = 0;
            foreach (string layer in setting.layers)
            {
                string str = layer.Replace(" ", "");
                int index = LayerMask.NameToLayer(layer);
                if (layer.Equals(setting.layers[setting.layers.Count - 1]))
                    replace.Append(string.Format("        {0} = {1},", str, index));
                else
                    replace.AppendLine(string.Format("        {0} = {1},", str, index));
                idx++;
            }
            sb.Replace("[Flag]ProjectLayer", replace.ToString());
            replace.Clear();

            File.WriteAllText(path, sb.ToString());
            AssetDatabase.Refresh();
        }

        private static void AddLayer(string name)
        {
            if (!InternalEditorUtility.layers.Contains(name))
            {
                SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/Tagmanager.asset"));
                SerializedProperty it = tagManager.GetIterator();
                while (it.NextVisible(true))
                {
                    if (it.name.Equals("layers"))
                    {
                        for (int i = 0; i < it.arraySize; i++)
                        {
                            if (i <= 7) continue;
                            SerializedProperty sp = it.GetArrayElementAtIndex(i);
                            if (string.IsNullOrEmpty(sp.stringValue))
                            {
                                sp.stringValue = name;
                                tagManager.ApplyModifiedProperties();
                                Debug.Log("[ProjectSettings_TagLayerTool]添加成功");
                                return;
                            }
                        }
                    }
                }
            }
        }

        private static void AddSortLayer(string layer)
        {
            if (!IsHasSortLayer(layer))
            {
                SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset"));
                SerializedProperty it = tagManager.GetIterator();
                while (it.NextVisible(true))
                {
                    if (it.name == "m_SortingLayers")
                    {
                        it.InsertArrayElementAtIndex(it.arraySize);
                        SerializedProperty dataPoint = it.GetArrayElementAtIndex(it.arraySize - 1);
                        while (dataPoint.NextVisible(true))
                        {
                            if (dataPoint.name == "name")
                            {
                                dataPoint.stringValue = layer;
                                tagManager.ApplyModifiedProperties();
                            }
                            if (dataPoint.name == "uniqueID")
                            {
                                dataPoint.intValue = it.arraySize;
                                tagManager.ApplyModifiedProperties();
                                return;
                            }
                        }
                    }
                }
            }
        }

        private static bool IsHasSortLayer(string name)
        {
            foreach (SortingLayer layer in SortingLayer.layers)
            {
                if (layer.name.Equals(name))
                    return true;
            }
            return false;
        }
    }
}

#endif