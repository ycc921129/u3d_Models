using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class RegisterSettingsAssetTool
    {
        private static readonly string Path = "Assets/Editor/AutoRegisterSettings";

        public static AutoRegisterSettings GetAutoRegisterSetting()
        {
            string path = string.Format("{0}/{1}.asset", Path, "AutoRegisterSettings");
            AutoRegisterSettings asset = AssetDatabase.LoadAssetAtPath<AutoRegisterSettings>(path);
            if (asset == null)
            {
                asset = CreateAutoRegisterSetting();
            }
            if (asset.autoRegisterPath == null
                || asset.autoRegisterPath.Count == 0
                || asset.autoRegisterPath.Contains(null))
            {
                AddAutoRegisterSettingDefaultPath(asset);
            }
            return asset;
        }

        private static AutoRegisterSettings CreateAutoRegisterSetting()
        {
            // 创建需要被序列化的XAsset
            AutoRegisterSettings m_Asset = ScriptableObject.CreateInstance<AutoRegisterSettings>();

            // 判断路径是否存在文件，如果存在就创建
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            // 带有asset文件名的储存路径
            string m_savePath = string.Format("{0}/{1}.asset", Path, "AutoRegisterSettings");
            // 创建asset文件
            AssetDatabase.CreateAsset(m_Asset, m_savePath);
            AddAutoRegisterSettingDefaultPath(m_Asset);

            Debug.Log("[CreateRegisterSettingAssetTool]创建自动注册目录完成 " + m_savePath);
            return m_Asset;
        }

        public static void SaveAutoRegisterSetting()
        {
            AutoRegisterSettings asset = GetAutoRegisterSetting();
            EditorUtility.SetDirty(asset);
        }

        private static void AddAutoRegisterSettingDefaultPath(AutoRegisterSettings asset)
        {
            // 添加默认目录
            asset.autoRegisterPath = new List<DefaultAsset>();
            AddAutoRegisterPath(asset, "Assets/_App");
            AddAutoRegisterPath(asset, "Assets/_AppCommon");
            AddAutoRegisterPath(asset, "Assets/_AppBase");
        }

        private static void AddAutoRegisterPath(AutoRegisterSettings asset, string path)
        {
            if (Directory.Exists(System.IO.Path.GetFullPath(path)))
            {
                asset.autoRegisterPath.Add(AssetDatabase.LoadAssetAtPath<DefaultAsset>(path));
            }
        }

        public static AutoRegisterVisualizationData GetAutoRegisterVisualizationData()
        {
            string path = string.Format("{0}/{1}.asset", Path, "AutoRegisterVisualizationData");
            AutoRegisterVisualizationData asset = AssetDatabase.LoadAssetAtPath<AutoRegisterVisualizationData>(path);
            if (asset == null)
            {
                asset = CreateAutoRegisterVisualizationData();
            }
            return asset;
        }

        private static AutoRegisterVisualizationData CreateAutoRegisterVisualizationData()
        {
            // 创建需要被序列化的XAsset
            AutoRegisterVisualizationData m_Asset = ScriptableObject.CreateInstance<AutoRegisterVisualizationData>();

            // 判断路径是否存在文件，如果存在就创建
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            // 带有asset文件名的储存路径
            string m_savePath = string.Format("{0}/{1}.asset", Path, "AutoRegisterVisualizationData");
            // 创建.asset文件
            AssetDatabase.CreateAsset(m_Asset, m_savePath);

            LogUtil.Log("[CreateRegisterSettingAssetTool]创建自动注册可视化Asset完成 " + m_savePath);
            return m_Asset;
        }

        public static void SaveAutoRegisterVisualizationData()
        {
            AutoRegisterVisualizationData asset = GetAutoRegisterVisualizationData();
            EditorUtility.SetDirty(asset);
        }
    }
}