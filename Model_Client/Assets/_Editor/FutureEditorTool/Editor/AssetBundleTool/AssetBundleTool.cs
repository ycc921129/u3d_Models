/*
 Author:du
 Time:2017.11.7
*/

/*
app build.gradle
android {
   ...
    aaptOptions {
        // 设置不压缩AB后缀格式 (压缩AB资源变小, 读取变慢)
        noCompress 'bundle', '.unity3d', '.ress', '.resource', '.obb', '.ab', '.bin', 'bytes', '.data' // or whatever extension you use
        // 设置目录忽略, 如果设置目录忽略, 会默认忽略_开头目录
        ignoreAssetsPattern = "!.svn:!.git:!.ds_store:!*.scc:.*:!CVS:!thumbs.db:!picasa.ini:!*~"
    }
}
*/

using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class AssetBundleTool
    {
        [MenuItem("[FC Release]/AssetBundle/标准生成资源包", false, 0)]
        private static void BuildReleaseABZip()
        {
            EditorApplication.isPlaying = false;
            AssetBundleBuildTool.StandardProcessBuildAssetBundle();
            ZipAB();
            CreateVersionFile();

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        [MenuItem("[FC Release]/AssetBundle/直接生成资源包", false, 1)]
        private static void BuildReleaseAB()
        {
            EditorApplication.isPlaying = false;
            AssetBundleBuildTool.BuildAssetBundle();
            ZipAB();
            CreateVersionFile();

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// 删除AB清单文件
        /// </summary>
        private static void DeleteABManifestFile()
        {
            DirectoryTool.DeleteTypeFile(EditorPathConst.ABOutputPath, EditorPathConst.ABManifestExtension);
        }

        private static void ZipAB()
        {
            EditorUtility.DisplayCancelableProgressBar("正在压缩AB包", "压缩中", 1);

            if (File.Exists(EditorPathConst.ABZipOutputFilePath))
            {
                File.Delete(EditorPathConst.ABZipOutputFilePath);
            }

            //HACK 压缩包插件删除
            //bool result = GZipHelper.ZipAll(EditorPathConst.ABOutputPath, EditorPathConst.ABZipOutputFilePath);
            //if (result)
            //{
            //    Debug.Log("BuildUitl: 压缩AB完成");
            //}
            //else
            //{
            //    Debug.Log("BuildUitl: 压缩AB失败");
            //}
            EditorUtility.ClearProgressBar();
        }

        private static void CreateVersionFile()
        {
            string path = EditorPathConst.ABVersionFilePath;
            string version = "version=1.0.1";
            if (File.Exists(path))
            {
                using (TxtDataParser parser = new TxtDataParser(path))
                {
                    string[] values = parser.GetValue("version");
                    version = values[0];
                    string[] versions = version.Split('.');
                    int abVersion = versions[2].ToInt();
                    abVersion++;
                    versions[2] = abVersion.ToString();
                    version = string.Format("{0}.{1}.{2}", versions[0], versions[1], versions[2]);
                    version = "version=" + version;
                }
                File.Delete(path);
            }
            FileTool.CreateFile(path, version);
        }
    }
}