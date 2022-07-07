/*
 Author:du
 Time:2017.11.7
*/

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FutureEditor
{
    public static class AssetBundleBuildTool
    {
        #region MenuItem
        [MenuItem("[FC Toolkit]/AssetBundle/标准流程生成AB资源", false, 100)]
        public static void StandardProcessBuildAssetBundle()
        {
            ClearAssetBundlesName();
            SetAssetBundlesName();
            BuildAssetBundle();
        }

        [MenuItem("[FC Toolkit]/AssetBundle/清除AB名", false, 201)]
        public static void ClearAssetBundlesName()
        {
            string[] abnames = AssetDatabase.GetAllAssetBundleNames();
            int length = abnames.Length;
            Debug.Log("[AssetBundleBuildTool]ClearStart ABNames have " + length);
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
            Debug.Log("[AssetBundleBuildTool]ClearEnd ABNames have " + length);
        }

        [MenuItem("[FC Toolkit]/AssetBundle/设置AB名", false, 202)]
        public static void SetAssetBundlesName()
        {
            EditorApplication.update = delegate ()
            {
                bool isCancel = EditorUtility.DisplayCancelableProgressBar("设置AssetBundleName", "...", 0);
                if (isCancel)
                {
                    EditorUtility.ClearProgressBar();
                    EditorApplication.update = null;
                    Debug.Log("[AssetBundleBuildTool]取消");
                }
            };

            ABAssetDic = GetABAssetDic();
            OneSharedPakDic = GetOneSharedPakDic();
            int index = 0;
            foreach (string key in ABAssetDic.Keys)
            {
                CurrDoingPath = PathTool.GetRawPath(key);
                SetAssetIntoPak(key, ABAssetDic[key]);
                index++;
                EditorUtility.DisplayCancelableProgressBar("设置AssetBundleName中", "...", (float)index / ABAssetDic.Keys.Count);
            }
            Dictionary<string, string> assignABPakDic = GetAssignABPakDic();
            foreach (string key in assignABPakDic.Keys)
            {
                SetFileToAssignABPak(key, assignABPakDic[key]);
            }

            EditorUtility.ClearProgressBar();
            EditorApplication.update = null;

            int length = AssetDatabase.GetAllAssetBundleNames().Length;
            Debug.Log("[AssetBundleBuildTool]Set ABNames Complete , ABNames have " + length);
        }

        [MenuItem("[FC Toolkit]/AssetBundle/直接生成AB资源", false, 203)]
        public static void BuildAssetBundle()
        {
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();

            string platformPath = PlatformTool.GetPlatformName(EditorUserBuildSettings.activeBuildTarget);
            string outputPath = Path.Combine(EditorPathConst.ABOutputPath, platformPath);
            if (Directory.Exists(outputPath))
                Directory.Delete(outputPath, true);
            Directory.CreateDirectory(outputPath);

            BuildPipeline.BuildAssetBundles(outputPath,
                BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.DeterministicAssetBundle,
                EditorUserBuildSettings.activeBuildTarget);

            CreateCheckFile(outputPath);

            AssetDatabase.Refresh();
            Debug.Log("[AssetBundleBuildTool]AB打包完成");
            EditorUtility.DisplayDialog("AB打包", "打包完成", "确定");
        }
        #endregion

        #region Path
        private static Dictionary<string, ABBuildPakType> ABAssetDic;
        private static Dictionary<string, string> OneSharedPakDic;
        private static string CurrDoingPath;

        private enum ABBuildPakType
        {
            // One
            OneShared,                      //公共包 (不同类型/目录)
                                            // Group
            Single_Group,                   //资源单组
            Multi_Group,                    //资源多组
                                            // Alone
            Alone_ObjPath,                  //单个资源以资源命名打包
            Alone_SecFolderPath,            //单个资源以二级文件夹命名打包
        }

        /// <summary>
        /// 获取AB资源路径
        /// 以源目录为标准
        /// </summary>
        private static Dictionary<string, ABBuildPakType> GetABAssetDic()
        {
            Dictionary<string, ABBuildPakType> dir = new Dictionary<string, ABBuildPakType>();
            // 加载资源
            dir.Add(EditorPathConst.AnimPath, ABBuildPakType.Multi_Group);
            dir.Add(EditorPathConst.AtlasPath, ABBuildPakType.Multi_Group);
            dir.Add(EditorPathConst.AudioPath, ABBuildPakType.Alone_ObjPath);
            dir.Add(EditorPathConst.DataPath, ABBuildPakType.Multi_Group);
            dir.Add(EditorPathConst.EffectPath, ABBuildPakType.Alone_SecFolderPath);
            dir.Add(EditorPathConst.FramesPath, ABBuildPakType.Alone_SecFolderPath);
            dir.Add(EditorPathConst.DynamicFontPath, ABBuildPakType.Single_Group);
            dir.Add(EditorPathConst.ModulePath, ABBuildPakType.Multi_Group);
            dir.Add(EditorPathConst.ScenePath, ABBuildPakType.Multi_Group);
            dir.Add(EditorPathConst.SkeletonPath, ABBuildPakType.Alone_SecFolderPath);
            dir.Add(EditorPathConst.SpritePath, ABBuildPakType.Alone_ObjPath);
            dir.Add(EditorPathConst.TexturePath, ABBuildPakType.Alone_ObjPath);
            dir.Add(EditorPathConst.VideoPath, ABBuildPakType.Alone_ObjPath);
            // 公共加载资源
            dir.Add(EditorPathConst.ShaderPath, ABBuildPakType.OneShared);
            dir.Add(EditorPathConst.MaterialPath, ABBuildPakType.OneShared);
            dir.Add(EditorPathConst.StaticFontPath, ABBuildPakType.OneShared);
            dir.Add(EditorPathConst.EffectLibPath, ABBuildPakType.OneShared);
            // 公共基础资源
            dir.Add(EditorPathConst.SkeletonShadersPath, ABBuildPakType.OneShared);
            dir.Add(EditorPathConst.SkeletonGraphicShadersPath, ABBuildPakType.OneShared);
            return dir;
        }

        private static Dictionary<string, string> GetOneSharedPakDic()
        {
            Dictionary<string, string> dir = new Dictionary<string, string>();
            // 静态公共包 (Shader、材质球、静态字体)
            dir.Add(EditorPathConst.ShaderPath, EP_ABSharedPakCosnt.StaticShared);
            dir.Add(EditorPathConst.MaterialPath, EP_ABSharedPakCosnt.StaticShared);
            dir.Add(EditorPathConst.StaticFontPath, EP_ABSharedPakCosnt.StaticShared);
            // 特效库公共包
            dir.Add(EditorPathConst.EffectLibPath, EP_ABSharedPakCosnt.EffectLibShared);
            // Skeleton库公共包
            dir.Add(EditorPathConst.SkeletonShadersPath, EP_ABSharedPakCosnt.SkeletonLibShared);
            dir.Add(EditorPathConst.SkeletonGraphicShadersPath, EP_ABSharedPakCosnt.SkeletonLibShared);
            return dir;
        }

        private static Dictionary<string, string> GetAssignABPakDic()
        {
            Dictionary<string, string> dir = new Dictionary<string, string>();
            // 预置静态基础字体文件 : PreMainFont
            dir.Add(EditorPathConst.PreStaticFontFile, EP_ABSharedPakCosnt.StaticShared);
            // 公共基础资源文件: SkeletonShader-Fill
            dir.Add(EditorPathConst.SkeletonSkeletonShaderFillFile, EP_ABSharedPakCosnt.SkeletonLibShared);
            // 公共基础资源文件: SkeletonEditorIcon
            dir.Add(EditorPathConst.SkeletonEditorGUIAtlasAssetIconFile, EP_ABSharedPakCosnt.SkeletonLibShared);
            dir.Add(EditorPathConst.SkeletonEditorGUISkeletonDataAssetIconFile, EP_ABSharedPakCosnt.SkeletonLibShared);
            return dir;
        }
        #endregion

        #region Pak

        #region OneSharedPak
        private static void SetOneSharedPak(string fullName)
        {
            fullName = PathTool.GetRawPath(fullName);
            foreach (string sharedPath in OneSharedPakDic.Keys)
            {
                if (fullName.StartsWith(sharedPath))
                {
                    string pakName = OneSharedPakDic[sharedPath];
                    SetOneSharedPak(fullName, pakName);
                    return;
                }
            }
        }

        private static void SetOneSharedPak(string fullName, string pakName, string[] extension = null)
        {
            if (extension != null)
            {
                bool isCan = HasExtension(fullName, extension);
                if (!isCan) return;
            }

            string assetPath = ChangeAssetRelativePath(fullName);
            AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
            assetImporter.assetBundleName = pakName + EditorPathConst.ABExtension;
        }
        #endregion

        #region SingleGroupPak
        private static void SetSingleGroupPak(string fullName)
        {
            fullName = PathTool.GetRawPath(fullName);
            string dataAssetPath = fullName.Substring(Application.dataPath.Length + 1);
            string assetName = dataAssetPath.Substring(dataAssetPath.IndexOf("/") + 1);
            assetName = assetName.Substring(assetName.IndexOf("/") + 1);
            assetName = assetName.Substring(0, assetName.LastIndexOf("/"));
            assetName = assetName + "/" + assetName;

            string assetPath = "Assets" + fullName.Substring(Application.dataPath.Length);
            AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
            assetImporter.assetBundleName = assetName + EditorPathConst.ABExtension;
        }
        #endregion

        #region MultiGroupPak
        private static void SetMultiGroupPak(string fullName)
        {
            fullName = PathTool.GetRawPath(fullName);
            string doingPath = CurrDoingPath.Substring(0, CurrDoingPath.LastIndexOf("/"));
            string typeName = CurrDoingPath.Substring(doingPath.LastIndexOf("/") + 1);
            string secLevel = fullName.Substring(CurrDoingPath.Length);
            secLevel = secLevel.Substring(0, secLevel.IndexOf("/"));

            string assetPath = "Assets" + fullName.Substring(Application.dataPath.Length);
            AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
            string assetName = typeName + secLevel;
            assetImporter.assetBundleName = assetName + EditorPathConst.ABExtension;
        }
        #endregion

        #region AlonePak
        private static void SetAloneObjPathPak(string fullName)
        {
            fullName = PathTool.GetRawPath(fullName);
            string dataAssetPath = fullName.Substring(Application.dataPath.Length + 1);
            string assetName = dataAssetPath.Substring(dataAssetPath.IndexOf("/") + 1);
            assetName = assetName.Substring(assetName.IndexOf("/") + 1);
            assetName = assetName.Replace(Path.GetExtension(assetName), string.Empty);

            string assetPath = "Assets" + fullName.Substring(Application.dataPath.Length);
            AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
            assetImporter.assetBundleName = assetName + EditorPathConst.ABExtension;
        }

        private static void SetAloneSecFolderPathPak(string fullName)
        {
            fullName = PathTool.GetRawPath(fullName);
            string doingPath = CurrDoingPath.Substring(0, CurrDoingPath.LastIndexOf("/"));
            string typeName = CurrDoingPath.Substring(doingPath.LastIndexOf("/") + 1);
            string secLevel = fullName.Substring(CurrDoingPath.Length);
            secLevel = secLevel.Substring(0, secLevel.IndexOf("/"));

            string assetPath = "Assets" + fullName.Substring(Application.dataPath.Length);
            AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
            string assetName = typeName + secLevel;
            assetImporter.assetBundleName = assetName + EditorPathConst.ABExtension;
        }
        #endregion

        #region AssignABPak
        private static void SetFileToAssignABPak(string fullFilePath, string abPakName)
        {
            string assetPath = "Assets" + fullFilePath.Substring(Application.dataPath.Length);
            AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
            string assetName = abPakName;
            assetImporter.assetBundleName = assetName + EditorPathConst.ABExtension;
        }
        #endregion

        #endregion

        #region Func
        private static void SetAssetIntoPak(string path, ABBuildPakType type)
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
                    switch (type)
                    {
                        // One
                        case ABBuildPakType.OneShared:
                            SetOneSharedPak(fullName);
                            break;
                        // Group
                        case ABBuildPakType.Single_Group:
                            SetSingleGroupPak(fullName);
                            break;
                        case ABBuildPakType.Multi_Group:
                            SetMultiGroupPak(fullName);
                            break;
                        // Alone
                        case ABBuildPakType.Alone_ObjPath:
                            SetAloneObjPathPak(fullName);
                            break;
                        case ABBuildPakType.Alone_SecFolderPath:
                            SetAloneSecFolderPathPak(fullName);
                            break;
                    }
                }
                else
                {
                    SetAssetIntoPak(fullName, type);
                }
            }
        }

        private static bool HasExtension(string fullName, string[] extension)
        {
            foreach (string ext in extension)
            {
                if (fullName.ToLower().EndsWith(ext))
                {
                    return true;
                }
            }
            return false;
        }

        private static string ChangeAssetRelativePath(string fullName)
        {
            return "Assets" + fullName.Substring(Application.dataPath.Length);
        }

        private static void CreateCheckFile(string path)
        {
            string newFilePath = path + "/abfiles.txt";
            if (File.Exists(newFilePath))
                File.Delete(newFilePath);

            List<string> files = new List<string>();
            files.Clear();
            PathTool.GetAllFilePath(path, files);

            string filePathReplace = path.Replace('\\', '/') + "/";
            FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            for (int i = 0; i < files.Count; i++)
            {
                string file = files[i];
                string ext = Path.GetExtension(file);
                if (ext.EndsWith(EditorPathConst.ABManifestExtension) || ext.EndsWith(".meta") || ext.Contains(".DS_Store"))
                    continue;

                string md5 = MD5EncryptUtil.GetFileMD5(file);
                string value = file.Replace(filePathReplace, string.Empty);
                FileInfo fileInfo = new FileInfo(file);
                sw.WriteLine(value + "=" + md5 + "=" + fileInfo.Length);
            }
            sw.Close();
            fs.Close();
        }
        #endregion
    }
}