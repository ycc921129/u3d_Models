/*
 Author:du
 Time:2019.8.12
*/

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class BuildProjectEncryptTextureABTool
    {
        public class ABDecryptMap
        {
            public Dictionary<string, string> abDict = new Dictionary<string, string>();
            public Dictionary<string, string> pathMapDict = new Dictionary<string, string>();
        }

        private static string atlasPath = "Assets/_Res/Art/Atlas";
        private static string spritePath = "Assets/_Res/Art/Sprite";
        private static string uiPath = "Assets/_Res/Resources/UI";

        private static string encryptTextureABName = "ETexture" + EditorPathConst.ABExtension;
        private static string outABDir = "/Pak/";
        private static string outMapFile = "ETextureMap" + EditorPathConst.ABExtension;

        [MenuItem("[FC Release]/Build/加密纹理包/构建加密纹理包", false, 0)]
        private static void BuildEncryptTextureABMenuItem()
        {
            ClearAssetBundlesName();
            SetEncryptTextureAName(atlasPath);
            SetEncryptTextureAName(spritePath);
            SetEncryptTextureAName(uiPath);
            ABDecryptMap map = new ABDecryptMap();
            AssetBundleManifest manifest = BuildEncryptTextureAB();
            string outputPath = Application.streamingAssetsPath + outABDir;
            EncryptOffestAB(manifest, outputPath, map);
            CreateETextureMapFile(map);
            ClearAssetBundlesName();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("构建加密纹理包完成");
        }

        public static void ClearAssetBundlesName()
        {
            string[] abnames = AssetDatabase.GetAllAssetBundleNames();
            int length = abnames.Length;
            Debug.Log("[BuildProjectEncryptTextureABTool]ClearStart ABNames have " + length);
            string[] assetBundleNames = new string[length];
            for (int i = 0; i < length; i++)
            {
                assetBundleNames[i] = abnames[i];
            }

            for (int i = 0; i < assetBundleNames.Length; i++)
            {
                AssetDatabase.RemoveAssetBundleName(assetBundleNames[i], true);
                EditorUtility.DisplayCancelableProgressBar("清除AssetBundleName", "清除所有设置的AssetBundleName", (float)i / assetBundleNames.Length);
            }
            EditorUtility.ClearProgressBar();
            length = AssetDatabase.GetAllAssetBundleNames().Length;
            Debug.Log("[BuildProjectEncryptTextureABTool]ClearEnd ABNames have " + length);
        }

        private static void SetEncryptTextureAName(string path)
        {
            if (!Directory.Exists(path))
            {
                Debug.LogError("找不到路径:" + path);
                EditorUtility.ClearProgressBar();
                return;
            }

            DirectoryInfo folder = new DirectoryInfo(path);
            FileSystemInfo[] files = folder.GetFileSystemInfos();
            int length = files.Length;
            for (int i = 0; i < length; i++)
            {
                string fullName = files[i].FullName;
                if (!(files[i] is DirectoryInfo))
                {
                    if (files[i].Name.EndsWith(".meta")) continue;
                    if (files[i].Name.EndsWith(".DS_Store")) continue;
                    if (files[i].Name.EndsWith(".gitkeep")) continue;

                    string assetPath = "Assets" + fullName.Substring(Application.dataPath.Length);
                    AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
                    assetImporter.assetBundleName = encryptTextureABName;
                }
            }
        }

        private static AssetBundleManifest BuildEncryptTextureAB()
        {
            string outputPath = Application.streamingAssetsPath + outABDir;
            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }
            Directory.CreateDirectory(outputPath);

            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(outputPath,
                BuildAssetBundleOptions.ChunkBasedCompression
                | BuildAssetBundleOptions.DeterministicAssetBundle
                | BuildAssetBundleOptions.AppendHashToAssetBundleName,
                EditorUserBuildSettings.activeBuildTarget);
            return manifest;
        }

        /// <summary>
        /// 偏移加密
        /// 也可以在文件头加入一个小的AssetBundle
        /// </summary>
        private static void EncryptOffestAB(AssetBundleManifest manifest, string outputPath, ABDecryptMap map)
        {
            foreach (string bundleName in manifest.GetAllAssetBundles())
            {
                string abFilePath = outputPath + "/" + bundleName;
                string hashcode1 = manifest.GetAssetBundleHash(bundleName).ToString();
                string hashcode2 = hashcode1.GetHashCode().ToString();
                string hashcode = hashcode1 + "a" + hashcode2;
                byte[] hashBytes = Encoding.UTF8.GetBytes(hashcode);
                int hashOffsetLen = hashBytes.Length;
                if (hashOffsetLen > 0)
                {
                    byte[] abFileData = File.ReadAllBytes(abFilePath);
                    byte[] newBuffer = hashBytes.Concat(abFileData).ToArray();
                    int newFileLen = newBuffer.Length;
                    FileStream fs = File.OpenWrite(abFilePath);
                    fs.Write(newBuffer, 0, newFileLen);
                    fs.Close();

                    if (map != null)
                    {
                        map.abDict.Add(bundleName, hashcode);
                    }
                }
            }
        }

        private static void CreateETextureMapFile(ABDecryptMap map)
        {
            // 获取CRC
            //BuildPipeline.GetCRCForAssetBundle();

            // 获取Hash
            //AssetBundle ab = AssetBundle.LoadFromFile(@"D:\SectionAB\AB2");
            //AssetBundleManifest manifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            //Hash128 myHashCode = manifest.GetAssetBundleHash("section1.ab");

            // 添加映射
            AddETextureMapItem(atlasPath, map);
            AddETextureMapItem(spritePath, map);
            AddETextureMapItem(uiPath, map);
            string json = JsonConvert.SerializeObject(map, Formatting.Indented);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
            string tempPath = Application.dataPath + "/" + outMapFile;
            File.WriteAllBytes(tempPath, jsonBytes);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            string assetPath = "Assets" + tempPath.Substring(Application.dataPath.Length);
            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath);
            string abFilePath = Application.streamingAssetsPath + outABDir + outMapFile;
            bool isComplete = BuildPipeline.BuildAssetBundle(file, null, abFilePath,
                BuildAssetBundleOptions.ChunkBasedCompression
                | BuildAssetBundleOptions.DeterministicAssetBundle
                | BuildAssetBundleOptions.DisableLoadAssetByFileName,
                EditorUserBuildSettings.activeBuildTarget);

            if (isComplete)
            {
                File.Delete(tempPath);
            }

            byte[] abFileData = File.ReadAllBytes(abFilePath);
            byte[] hashBytes = new byte[111];
            byte[] newBuffer = hashBytes.Concat(abFileData).ToArray();
            int newFileLen = newBuffer.Length;
            FileStream fs = File.OpenWrite(abFilePath);
            fs.Write(newBuffer, 0, newFileLen);
            fs.Close();
        }

        private static void AddETextureMapItem(string path, ABDecryptMap map)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            FileSystemInfo[] files = folder.GetFileSystemInfos();
            int length = files.Length;
            for (int i = 0; i < length; i++)
            {
                string fullName = files[i].FullName;
                if (!(files[i] is DirectoryInfo))
                {
                    FileSystemInfo file = files[i];
                    if (file.Name.EndsWith(".meta")) continue;
                    if (file.Name.EndsWith(".DS_Store")) continue;
                    if (file.Name.EndsWith(".gitkeep")) continue;

                    string assetPath = "Assets" + fullName.Substring(Application.dataPath.Length);
                    string key = null;
                    assetPath = assetPath.Replace("\\", "/");
                    if (assetPath.StartsWith("Assets/_Res/Resources/"))
                    {
                        key = assetPath.Replace("Assets/_Res/Resources/", string.Empty).Replace(file.Extension, string.Empty); ;
                    }
                    else if (assetPath.StartsWith("Assets/_Res/Art/"))
                    {
                        key = assetPath.Replace("Assets/_Res/Art/", string.Empty).Replace(file.Extension, string.Empty); ;
                    }
                    map.pathMapDict.Add(key, assetPath);
                }
            }
        }
    }
}