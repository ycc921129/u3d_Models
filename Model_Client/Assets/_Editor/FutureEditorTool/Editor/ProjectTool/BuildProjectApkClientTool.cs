using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using FuturePlugin;
using ProjectApp;

/// <summary>
/// 适配旧版本Apk打包接口
/// </summary>
public static class BuildProjectApkClientTool
{
    public static void BuildUnitySyncAndroidStudio()
    {
        FutureEditor.BuildProjectApkClientTool.BuildProjectAndSyncAndroidStudio();
    }
}

namespace FutureEditor
{
    public static class BuildProjectApkClientTool
    {
        public enum ApkCmdType : int
        {
            Project = 0,
            All = 1,
        }

        //private const BuildOptions CompressBuildOption = BuildOptions.CompressWithLz4HC;
        //private const BuildOptions CompressBuildOption = BuildOptions.CompressWithLz4;
        private const BuildOptions CompressBuildOption = BuildOptions.None;

        private static ApkCmdType apkCmdType = ApkCmdType.Project;
        private static string Unity3dDir = Path.GetFullPath(Application.dataPath + "/..");
        private static string Unity3dBuildParentDir = Path.GetFullPath(Application.dataPath + "/../../UClient_Build/APK.apk");
        private static string AndroidProjectDir = Path.GetFullPath(Application.dataPath + "/../../../UGameAndroid");
        private static string AndroidProjectBuildDir = AndroidProjectDir + "/app/build";

        #region Release包国内服务器屏蔽  

        //HACK 不是Release才使用包国内服务器  
        private const string BUILD_DEBUGURL = "BUILD_DEBUGURL";

        private static void SetUrl()
        {
            string _url = BUILD_DEBUGURL;
            var url = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
            if (!url.Contains(_url))
            {
                if (!url.Trim().EndsWith(";")) url += ";";
                url += _url;
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, url);  
            }
        }    
          
        private static void CleanUrl()
        {
            string _url = BUILD_DEBUGURL;
            var url = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
            if (_url.Contains(_url))
            {
                _url = _url.Replace(_url, "");  
                _url = _url.Replace(";;", ";");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, _url);
            }
        }

        #endregion

        /// <summary>
        /// HACK 客户端写死判断，是否使用AES加密
        /// </summary>
        private static bool AES_USE = true;       
          
        #region 旧版本实现
        //[MenuItem("[FC Release]/Build/AndroidStudio命令/assembleDebug_v1", false, -9)]
        //private static void AndroidStudioDebugApk()
        //{
        //    AndroidStudioCmd(BuildType.Debug);
        //}

        //[MenuItem("[FC Release]/Build/AndroidStudio命令/assembleRelease_v1", false, -8)]
        //private static void AndroidStudioReleaseApk()
        //{
        //    AndroidStudioCmd(BuildType.Release);
        //}

        //[MenuItem("[FC Release]/Build/AndroidStudio命令/assemble_v1", false, -7)]
        //private static void AndroidStudioDebugAndReleaseApk()
        //{
        //    AndroidStudioCmd(BuildType.All);
        //}

        //[MenuItem("[FC Release]/Build/APK自动化构建/构建debug.apk_v1", false, -5)]
        //public static void AutoBuildDebugApk()
        //{
        //    apkCmdType = ApkCmdType.All;
        //    BuildApk(BuildType.Debug);
        //}

        //[MenuItem("[FC Release]/Build/APK自动化构建/构建release.apk_v1", false, -4)]
        //public static void AutoBuildReleaseApk()
        //{
        //    apkCmdType = ApkCmdType.All;
        //    BuildApk(BuildType.Release);
        //}

        //[MenuItem("[FC Release]/Build/APK自动化构建/构建debug.apk与release.apk_v1", false, -3)]
        //public static void AutoBuildDebugAndReleaseApk()
        //{
        //    apkCmdType = ApkCmdType.All;
        //    BuildApk(BuildType.All);
        //}
        #endregion

        [MenuItem("[FC Release]/当前构建版本: 2020-6-4 11:57:50", false, -100)]
        private static void Version_Menu()
        {
            UnityEngine.Debug.Log("[BuildProjectApkClientTool]AppFacade.IsUseUGameAndroid = " + AppFacade_Editor.IsUseUGameAndroid);
        }

        [MenuItem("[FC Release]/Build/构建项目/打开安卓工程目录", false, -23)]
        private static void OpenAndroidProjectDir_Menu()
        {
            Process process = Process.Start(AndroidProjectDir);
            process.WaitForExit();
            process.Close();
            UnityEngine.Debug.Log("[BuildProjectApkClientTool]打开安卓工程目录: " + AndroidProjectDir);
        }

        [MenuItem("[FC Release]/Build/构建项目/清理编译路径", false, -22)]
        private static void BuildPreCleanDir_Menu()
        {
            // 打包前清理编译路径
            CleanAndroidProjectMainSrcDir();
            CleanAndroidProjectBuildDir();
        }

        [MenuItem("[FC Release]/Build/构建项目/构建工程项目", false, -21)]
        private static void BuildProject_Menu()
        {
            BuildCppPlayer();
        }

        [MenuItem("[FC Release]/Build/构建项目/构建项目并同步到AndroidStudio", false, -20)]
        private static void BuildProjectAndSyncAndroidStudio_Menu()
        {
            CleanUrl();
            BuildProjectAndSyncAndroidStudio();
        }

        [MenuItem("[FC Release]/Build/构建项目/项目同步到AndroidStudio", false, -19)]
        private static void ProjectSyncAndroidStudio_Menu()
        {
            ProjectSyncAndroidStudio();
        }

        // 项目出包模式 v2
        [MenuItem("[FC Release]/Build/AndroidStudio命令/clean", false, -10)]
        private static void AndroidStudioCleanProject()
        {
            string cleanCMD = "clean";
            CleanAndroidProjectBuildDir();
            AssembleCmd(AndroidProjectDir, cleanCMD);
        }

        [MenuItem("[FC Release]/Build/AndroidStudio命令/assembleDebug(Debug测服)", false, -10)]
        private static void AndroidStudioDebugApkProject()
        {
            SetUrl();
            apkCmdType = ApkCmdType.Project;
            AndroidStudioCmd(AppBuildType.Debug);
        }

        [MenuItem("[FC Release]/Build/AndroidStudio命令/assembleDebugFormal(Debug正服)", false, -10)]
        private static void AndroidStudioDebugFormalApkProject()
        {
            SetUrl();
            apkCmdType = ApkCmdType.Project;
            AndroidStudioCmd(AppBuildType.DebugFormal);
        }

        [MenuItem("[FC Release]/Build/AndroidStudio命令/assembleRelease", false, -10)]
        private static void AndroidStudioReleaseApkProject()
        {
            CleanUrl();
            apkCmdType = ApkCmdType.Project;
            AndroidStudioCmd(AppBuildType.Release);
        }

        [MenuItem("[FC Release]/Build/AndroidStudio命令/assemble", false, -10)]
        private static void AndroidStudioDebugAndReleaseApkProject()
        {
            apkCmdType = ApkCmdType.Project;
            AndroidStudioCmd(AppBuildType.All);
        }

        [MenuItem("[FC Release]/Build/APK自动化构建/构建Debug.apk", false, -3)]
        public static void AutoBuildProjectDebugApk()
        {
            SetUrl();
            SetMetaDataLoader(false);
            apkCmdType = ApkCmdType.Project;
            BuildApk(AppBuildType.Debug);
        }
        [MenuItem("[FC Release]/Build/APK自动化构建/构建DebugFormal.apk", false, -2)]
        public static void AutoBuildProjectDebugFormalApk()
        {
            SetUrl();
            SetMetaDataLoader(false);
            apkCmdType = ApkCmdType.Project;
            BuildApk(AppBuildType.DebugFormal);
        }

        [MenuItem("[FC Release]/Build/APK自动化构建/构建Release.apk", false, -1)]
        public static void AutoBuildProjectReleaseApk()
        {
            CleanUrl();
            SetMetaDataLoader(false);  
            apkCmdType = ApkCmdType.Project;
            BuildApk(AppBuildType.Release);
        }

        [MenuItem("[FC Release]/Build/APK自动化构建/构建所有类型APK", false, 0)]
        public static void AutoBuildProjectDebugAndReleaseApk()
        {
            CleanUrl();
            SetMetaDataLoader(false);
            apkCmdType = ApkCmdType.Project;
            BuildApk(AppBuildType.All);
        }

        [MenuItem("[FC Release]/Build/安装、卸载、清理/安装debug.apk", false, 0)]
        public static void AutoInstallDebugApk()
        {
            AutoInstallApk(true);
        }
        [MenuItem("[FC Release]/Build/安装、卸载、清理/安装release.apk", false, 0)]
        public static void AutoInstallReleaseApk()
        {
            AutoInstallApk(false);
        }
        [MenuItem("[FC Release]/Build/安装、卸载、清理/卸载" + AppFacade_Editor.PackageName, false, 0)]
        public static void AutoDeleteApk()
        {
            _AutoUinstallApk();
        }
        [MenuItem("[FC Release]/Build/安装、卸载、清理/清理缓存" + AppFacade_Editor.PackageName, false, 0)]
        public static void AutoClearApk()
        {
            _AutoClearApk();
        }

        private static void AutoInstallApk(bool debug)
        {
            string buildApkDir = AndroidProjectBuildDir + "/outputs/apk/" + AppFacade_Editor.AppName + (debug ? "/debug/" : "/release/");
            string apkPath = buildApkDir;

            DirectoryInfo buildApkDirInfo = new DirectoryInfo(buildApkDir);
            foreach (FileInfo file in buildApkDirInfo.GetFiles())
                if (file.Name.EndsWith(".apk"))
                {
                    apkPath = apkPath + file.Name;
                    break;
                }

            if (!apkPath.EndsWith(".apk"))
            {
                UnityEngine.Debug.LogFormat("[BuildProjectApkClientTool] {0} 没有找到apk!", apkPath);
                return;
            }

            string args = "install -r " + apkPath;
            string runArgs = string.Format("shell monkey -p {0}  -c android.intent.category.LAUNCHER 1", AppFacade_Editor.PackageName);
#if UNITY_EDITOR_OSX
            GUIUtility.systemCopyBuffer = "adb " + args + ";adb " + runArgs;
            UnityEngine.Debug.Log("已复制命令到剪切板");
#endif
            Thread thread1 = new Thread(new ThreadStart(() =>
            {
                string output = processCMD(buildApkDir, "adb", args, "Success");

                if (output.Trim() == "Success")
                {
                    UnityEngine.Debug.LogFormat("[BuildProjectApkClientTool] adb 安装 Succeed!");
                    // 执行
                    string runOutput = processCMD(buildApkDir, "adb", runArgs, "Success");
                }
                else
                {
                    UnityEngine.Debug.LogFormat("[BuildProjectApkClientTool] adb 安装 Failed!");
                }
            }));
            thread1.Start();
        }

        private static void _AutoUinstallApk()
        {
            string args = "uninstall " + AppFacade_Editor.PackageName;
#if UNITY_EDITOR_OSX
            GUIUtility.systemCopyBuffer = "adb " + args;
            UnityEngine.Debug.Log("已复制命令到剪切板");
#endif
            Thread thread1 = new Thread(new ThreadStart(() =>
            {
                string output = processCMD(AndroidProjectDir, "adb", args, "Success");
                UnityEngine.Debug.LogFormat("[BuildProjectApkClientTool] 删除命令已经执行");
                //            if (output.Trim() == "Success")
                //            {
                //                UnityEngine.Debug.LogFormat("[BuildProjectApkClientTool] adb 卸载成功!");
                //            }
                //            else
                //            {
                //                UnityEngine.Debug.LogFormat("[BuildProjectApkClientTool] adb 卸载失败!");
                //            }
            }));
            thread1.Start();
        }

        private static void _AutoClearApk()
        {
            string args = "shell pm clear " + AppFacade_Editor.PackageName;
#if UNITY_EDITOR_OSX
            GUIUtility.systemCopyBuffer = "adb " + args;
            UnityEngine.Debug.Log("已复制命令到剪切板");
#endif
            Thread thread1 = new Thread(new ThreadStart(() =>
            {
                string output = processCMD(AndroidProjectDir, "adb", args, "Success");
                UnityEngine.Debug.LogFormat("[BuildProjectApkClientTool] 清理命令已经执行");
                //            if (output.Trim() == "Success")
                //            {
                //                UnityEngine.Debug.LogFormat("[BuildProjectApkClientTool] adb 清理成功!");
                //            }
                //            else
                //            {
                //                UnityEngine.Debug.LogFormat("[BuildProjectApkClientTool] adb 清理成功!");
                //            }
            }));
            thread1.Start();
        }

        public static string processCMD(string WorkingDirectory, string FileName, string Arguments, string defOutput)
        {
            UnityEngine.Debug.LogFormat("{0} {1}", FileName, Arguments);
            Process process = new Process();
#if UNITY_EDITOR_WIN
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
#elif UNITY_EDITOR_OSX
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.RedirectStandardError = false;
            process.StartInfo.RedirectStandardInput = false;
#endif
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.ErrorDialog = true;

            process.StartInfo.WorkingDirectory = WorkingDirectory;

#if UNITY_EDITOR_WIN
            process.StartInfo.FileName = FileName;
            process.StartInfo.Arguments = Arguments;
#elif UNITY_EDITOR_OSX
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = string.Format("{0} {1}", FileName, Arguments);
#endif

            string output = defOutput;
            process.Start();
#if UNITY_EDITOR_WIN
            output = process.StandardError.ReadToEnd();
            UnityEngine.Debug.Log("output=" + output);
#endif
            process.WaitForExit();
            process.Close();
            return output;
        }
    #region 混淆加密Build流程

        [MenuItem("[FC Release]/Build/构建项目/设置非加密MetaDataLoader", false, -3)]
        public static void SetNoEncptyionMetaDataLoader()
        {
            SetMetaDataLoader(false);
        }
        
        [MenuItem("[FC Release]/Build/构建项目/设置加密MetaDataLoader", false, -3)]
        public static void SetEncptyionMetaDataLoader()
        {
            SetMetaDataLoader();
        }
        [MenuItem("[FC Release]/Build/构建项目/AES加密MetaData文件", false, -3)]
        public static void EncptyionMetaData()
        {
           EncptyionMetaData();
        }

        [MenuItem("[FC Release]/Build/[混淆加密AES]APK自动化构建/构建Debug.apk", false, -3)]
        public static void Cryption_AutoBuildProjectDebugApk()
        {
            SetMetaDataLoader();
            apkCmdType = ApkCmdType.Project;
            BuildApk(AppBuildType.Debug,true);
        }

        [MenuItem("[FC Release]/Build/[混淆加密AES]APK自动化构建/构建DebugFormal.apk", false, -2)]
        public static void Cryption_AutoBuildProjectDebugFormalApk()
        {
            SetMetaDataLoader();
            apkCmdType = ApkCmdType.Project;
            BuildApk(AppBuildType.DebugFormal, true);
        }

        [MenuItem("[FC Release]/Build/[混淆加密AES]APK自动化构建/构建Release.apk", false, -1)]
        public static void Cryption_AutoBuildProjectReleaseApk()
        {
            SetMetaDataLoader();
            apkCmdType = ApkCmdType.Project;
            BuildApk(AppBuildType.Release,true);
        }

        [MenuItem("[FC Release]/Build/[混淆加密AES]APK自动化构建/构建所有类型APK", false, 0)]
        public static void Cryption_AutoBuildProjectDebugAndReleaseApk()
        {
            SetMetaDataLoader();
            apkCmdType = ApkCmdType.Project;
            BuildApk(AppBuildType.All,true);
        }


        public static string MetaLoaderFilePath =
            EditorApplication.applicationContentsPath+@"/il2cpp/libil2cpp/vm/MetadataLoader.cpp";

        public const string keySymbol = "{Key}";
        public const string IvectorSymbol = "{Ivector}";


        public static string metaDataPath = @"app\src\main\assets\bin\Data\Managed\Metadata/global-metadata.dat";

        /// <summary>
        /// 修改设置MetaDataLoader文件
        /// </summary>
        /// <param name="isEncryption">是否加密</param>
        public static void SetMetaDataLoader(bool isEncryption = true)
        {
            string loaderStr = "";
            string loaderPath =
                "Assets/_Editor/FutureEditorTool/Editor/ProjectTool/BuildProjectTool/MetaDataLoader/Cryption/MetadataLoader.cpp.txt";
            if (isEncryption)
            {
                if(AES_USE)
                    loaderPath =
                        "Assets/_Editor/FutureEditorTool/Editor/ProjectTool/BuildProjectTool/MetaDataLoader/Cryption/MetadataLoader.cpp.aes.txt";
                else
                    loaderPath =
                        "Assets/_Editor/FutureEditorTool/Editor/ProjectTool/BuildProjectTool/MetaDataLoader/Cryption/MetadataLoader.cpp.txt";
            }
            else
            {
                loaderPath =
                    "Assets/_Editor/FutureEditorTool/Editor/ProjectTool/BuildProjectTool/MetaDataLoader/Origin/MetadataLoader.cpp.txt";
            }
              
            loaderStr = AssetDatabase.LoadAssetAtPath<TextAsset>(loaderPath).text;
            LogUtil.Log(AppFacade.AESKey);

            if (isEncryption)
            {
                loaderStr = loaderStr.Replace(keySymbol, AppFacade.AESKey).Replace(IvectorSymbol, AppFacade.AESIVector);
            }
            LogUtil.Log(MetaLoaderFilePath);
            File.WriteAllText(MetaLoaderFilePath, loaderStr);
        }

        /// <summary>
        /// 加密MetaData文件
        /// </summary>
        public static void EncryptMetaData()
        {    
            if (!Directory.Exists(AndroidProjectDir))
            {
                LogUtil.LogError("安卓工程路径不存在!!");
                return;
            }
            string metaFile = Path.Combine(AndroidProjectDir, metaDataPath);
            LogUtil.Log(metaFile);
            if (!File.Exists(metaFile))  
            {
                LogUtil.LogError($"不存在文件MetaData:{metaFile}");
                return;
            }

            byte[] data = File.ReadAllBytes(metaFile);
            if(AES_USE)
                data = Encrypt(data, AppFacade.AESKey, AppFacade.AESIVector); //AES加密
            else
                data = Encrypt(data, AppFacade.AESKey); //普通加密->比AES快
            File.WriteAllBytes(metaFile, data);
        }        

        /// <summary>
        /// 运算字节矩阵
        /// </summary>
        private const int byteMatrixSize = 4 * 4;

        /// <summary>
        /// 补零
        /// </summary>
        private const string zeroString = "\0";

        /// <summary>
        /// 工程AES加密，Mode CBC，Padding PKCS7
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iVector"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, string key, string iVector)
        {
            byte[] toEncryptArray = null;
            if (data.Length % byteMatrixSize != 0)
            {
                long length = data.Length / byteMatrixSize;
                length++;
                toEncryptArray = new byte[length * byteMatrixSize];
                data.CopyTo(toEncryptArray, 0);
            }
            else
            {
                toEncryptArray = data;
            }

            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] ivArray = Encoding.UTF8.GetBytes(iVector);
            RijndaelManaged aes = new RijndaelManaged();
            aes.Key = keyArray;
            aes.IV = ivArray;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = aes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return resultArray;
        }

        /// 工程普通加密，Mode CBC，Padding PKCS7
        public static byte[] Encrypt(byte[] bytes, string key_str)
        {
            byte[] key_bytes = Encoding.Default.GetBytes(key_str);
            byte[] ret = new byte[bytes.Length];
            int nowindex = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                byte key = key_bytes[nowindex++];
                nowindex %= key_bytes.Length;
                ret[i] = (byte)(bytes[i] ^ key);  
            }  

            return ret;  
        }
        #endregion

        private static void BuildApk(AppBuildType buildType, bool needEncryption = false) 
        {
            BuildResult buildResult = BuildProjectAndSyncAndroidStudio();
            if (buildResult == BuildResult.Succeeded)
            {
                if (needEncryption)
                {
                    EncryptMetaData();
                }
                AndroidStudioCmd(buildType);
            }
        }  

        public static BuildResult BuildProjectAndSyncAndroidStudio()
        {
            BuildReport buildReport = BuildCppPlayer();
            if (buildReport == null)
            {
                return BuildResult.Failed;
            }

            if (buildReport.summary.result == BuildResult.Succeeded)
            {
                bool syncRes = ProjectSyncAndroidStudio();
                if (!syncRes)
                {
                    return BuildResult.Failed;
                }
            }
            else
            {
                ThrowException("[BuildProjectApkClientTool]Unity编译apk错误");
            }

            return buildReport.summary.result;
        }

        private static BuildReport BuildCppPlayer()
        {
#if !UNITY_ANDROID
            LogUtil.LogError("[BuildProjectIpaClientTool]切换到Android平台再打包");
            return null;
#endif

            DirectoryInfo dirInfo = new DirectoryInfo(Unity3dBuildParentDir);
            if (!dirInfo.Parent.Exists)
            {
                dirInfo.Parent.Create();
            }

            string buildDir = Unity3dBuildParentDir;
#if UNITY_2019_4
            buildDir = Unity3dBuildParentDir + "/" + AppFacade_Editor.AppName;
#endif

            // 打包前清理编译路径
            CleanAndroidProjectMainSrcDir();
            CleanAndroidProjectBuildDir();
            // 开始编译
            BuildReport buildReport = BuildClientTool.BuildCppPlayer(AppFacade_Editor.AppName, buildDir, BuildTarget.Android, CompressBuildOption);
            UnityEngine.Debug.Log("[BuildProjectApkClientTool]BuildResult: " + buildReport.summary.result.ToString());
            return buildReport;
        }

        private static bool ProjectSyncAndroidStudio()
        {
            // BuylyJar不再自动同步, 需要提交到安卓工程
            // 同步Jar
            //string buglyJar = BuildProjectDir + "/" + AppProjectInfo_Editor.AppName + "/libs/bugly.jar";
            //string buglyagentJar = BuildProjectDir + "/" + AppProjectInfo_Editor.AppName + "/libs/buglyagent.jar";
            //string copyToBuglyJar = AndroidProjectDir + "/app/libs/bugly.jar";
            //string copyToBuglyagentJar = AndroidProjectDir + "/app/libs/buglyagent.jar";
            //if (File.Exists(buglyJar) && File.Exists(buglyagentJar))
            //{
            //    if (File.Exists(copyToBuglyJar) && File.Exists(copyToBuglyagentJar))
            //    {
            //        File.Delete(copyToBuglyJar);
            //        File.Delete(copyToBuglyagentJar);
            //    }
            //    File.Copy(buglyJar, copyToBuglyJar);
            //    File.Copy(buglyagentJar, copyToBuglyagentJar);
            //    UnityEngine.Debug.Log("[BuildProjectApkClientTool]Sync Jar Succeed");
            //}

            // 同步资源和代码
            string buildProjectDir = Unity3dBuildParentDir + "/" + AppFacade_Editor.AppName;
            if (Directory.Exists(buildProjectDir))
            {
                string assets = "/src/main/assets";
                string jniLibs = "/src/main/jniLibs";

                string mainAssets = buildProjectDir + assets;
                string mainJniLibs = buildProjectDir + jniLibs;
#if UNITY_2019_4
                mainAssets = buildProjectDir + "/unityLibrary" + assets;
                mainJniLibs = buildProjectDir + "/unityLibrary" + jniLibs;
#endif
                string copyToMainAssets = AndroidProjectDir + "/app" + assets;
                string copyToMainJniLibs = AndroidProjectDir + "/app" + jniLibs;
                if (Directory.Exists(mainAssets) && Directory.Exists(mainJniLibs))
                {
                    DirectoryTool.CopyDir(mainAssets, copyToMainAssets);
                    DirectoryTool.CopyDir(mainJniLibs, copyToMainJniLibs);
                    UnityEngine.Debug.Log("[BuildProjectApkClientTool]Sync Assets And JniLibs Succeed");

                    // 同步Project打包资源
                    string projectAssetsDir = Unity3dDir + "/" + "_Project/Android/assets";
                    string copyToProjectAssetsDir = AndroidProjectDir + "/app" + assets;
                    SyncCopyProjectAssetsDir(projectAssetsDir, copyToProjectAssetsDir);
                    return true;
                }
                UnityEngine.Debug.LogError("[BuildProjectApkClientTool]Sync Assets And JniLibs Failure");
                return false;
            }
            else
            {
                UnityEngine.Debug.LogError("[BuildProjectApkClientTool]没有找到项目编译完成的目录: " + buildProjectDir);
                return false;
            }
        }

        private static void SyncCopyProjectAssetsDir(string sourceDir, string destDir)
        {
            if (!Directory.Exists(sourceDir)) return;

            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);

            DirectoryInfo sourceDirInfo = new DirectoryInfo(sourceDir);
            FileInfo[] files = sourceDirInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                string path = destDir + "/" + file.Name;
                file.CopyTo(path, true);
                UnityEngine.Debug.Log("[BuildProjectApkClientTool]SyncCopyProjectDir: " + file.Name);
            }

            DirectoryInfo[] dirs = sourceDirInfo.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                SyncCopyProjectAssetsDir(dir.FullName, destDir + "/" + dir.Name);
            }
        }

        private static void AndroidStudioCmd(AppBuildType buildType)
        {
            string buildApkDir = AndroidProjectBuildDir + "/outputs/apk";

            string cleanCMD = "clean";
            string assembleCMD = "assemble";
            string AppName = AppFacade_Editor.AppName[0].ToString().ToUpper() + AppFacade_Editor.AppName.Substring(1);
            if (apkCmdType == ApkCmdType.Project)
            {
                assembleCMD = "assemble" + AppName;
            }
            else
            {
                assembleCMD = "assemble";
            }

            string bundleReleaseCMD = "bundle" + AppName + "Release";

            switch (buildType)
            {
                case AppBuildType.Debug:
                    {
                        assembleCMD += "Debug";
                        Thread thread1 = new Thread(new ThreadStart(() =>
                        {
                            CleanAndroidProjectBuildDir();
                            if (!AssembleCmd(AndroidProjectDir, cleanCMD))
                                return;
                            if (!AssembleCmd(AndroidProjectDir, assembleCMD))
                                return;
                            OpenBuildApkDir(buildApkDir);
                        }));
                        thread1.Start();
                    }
                    break;
                case AppBuildType.DebugFormal:
                    {
                        assembleCMD += "DebugFormal";
                        Thread thread2 = new Thread(new ThreadStart(() =>
                        {
                            CleanAndroidProjectBuildDir();
                            if (!AssembleCmd(AndroidProjectDir, cleanCMD))
                                return;
                            if (!AssembleCmd(AndroidProjectDir, assembleCMD))
                                return;
                    //                  AssembleCmd(AndroidProjectDir, bundleReleaseCMD);
                    OpenBuildApkDir(buildApkDir);
                        }));
                        thread2.Start();
                    }
                    break;
                case AppBuildType.Release:
                    {
                        assembleCMD += "Release";
                        Thread thread2 = new Thread(new ThreadStart(() =>
                        {
                            CleanAndroidProjectBuildDir();
                            if (!AssembleCmd(AndroidProjectDir, cleanCMD))
                                return;
                            if (!AssembleCmd(AndroidProjectDir, assembleCMD))
                                return;
                    //                  AssembleCmd(AndroidProjectDir, bundleReleaseCMD);
                    OpenBuildApkDir(buildApkDir);
                        }));
                        thread2.Start();
                    }
                    break;
                case AppBuildType.All:
                    {
                        Thread thread3 = new Thread(new ThreadStart(() =>
                        {
                            CleanAndroidProjectBuildDir();
                            if (!AssembleCmd(AndroidProjectDir, cleanCMD))
                                return;
                            if (!AssembleCmd(AndroidProjectDir, assembleCMD + "Debug"))
                                return;
                            if (!AssembleCmd(AndroidProjectDir, assembleCMD + "DebugFormal"))
                                return;
                            if (!AssembleCmd(AndroidProjectDir, assembleCMD + "Release"))
                                return;
                    //                  AssembleCmd(AndroidProjectDir, bundleReleaseCMD);
                    OpenBuildApkDir(buildApkDir);
                        }));
                        thread3.Start();
                    }
                    break;
            }
        }

        private static void CleanAndroidProjectMainSrcDir()
        {
            string jniLibs = "/src/main/jniLibs";
            string assets = "/src/main/assets";

            string copyToMainJniLibs = AndroidProjectDir + "/app" + jniLibs;
            string copyToMainAssets = AndroidProjectDir + "/app" + assets;

            if (Directory.Exists(copyToMainJniLibs))
            {
                Directory.Delete(copyToMainJniLibs, true);
            }
            Directory.CreateDirectory(copyToMainJniLibs);

            if (Directory.Exists(copyToMainAssets))
            {
                Directory.Delete(copyToMainAssets, true);
            }
            Directory.CreateDirectory(copyToMainAssets);
        }

        private static void CleanAndroidProjectBuildDir()
        {
            if (Directory.Exists(AndroidProjectBuildDir))
            {
                Directory.Delete(AndroidProjectBuildDir, true);
            }
            Directory.CreateDirectory(AndroidProjectBuildDir);
        }

        //    private static void AssembleCmd(string andriodStudioDir, string cmd)
        //    {
        //        Process process = new Process();
        //        process.StartInfo.UseShellExecute = true;
        //        process.StartInfo.CreateNoWindow = false;
        //        process.StartInfo.ErrorDialog = true;
        //
        //        process.StartInfo.WorkingDirectory = andriodStudioDir;
        //#if UNITY_EDITOR_WIN
        //        process.StartInfo.FileName = GetBatFile();
        //        process.StartInfo.Arguments = cmd;
        //#elif UNITY_EDITOR_OSX
        //        process.StartInfo.FileName = "/bin/bash";
        //        process.StartInfo.Arguments = string.Format("{0}/{1} {2}", andriodStudioDir, GetBatFile(), cmd);
        //#endif
        //
        //        process.Start();
        //        process.WaitForExit();
        //        process.Close();
        //
        //        UnityEngine.Debug.LogFormat("[BuildProjectApkClientTool]Assemble {0} Succeed", cmd);
        //    }

        private static bool AssembleCmd(string andriodStudioDir, string cmd)
        {
            UnityEngine.Debug.LogFormat("[BuildProjectApkClientTool]Assemble {0} executing...", cmd);
            Process process = new Process();
            string output = "";
            //        process.StartInfo.UseShellExecute = true;
            //        process.StartInfo.CreateNoWindow = false;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.StartInfo.ErrorDialog = true;

            process.StartInfo.WorkingDirectory = andriodStudioDir;
#if UNITY_EDITOR_WIN
            process.StartInfo.FileName = Path.Combine(andriodStudioDir, GetBatFile());
            process.StartInfo.Arguments = cmd;
#elif UNITY_EDITOR_OSX
        process.StartInfo.FileName = "/bin/bash";
        process.StartInfo.Arguments = string.Format("{0}/{1} {2}", andriodStudioDir, GetBatFile(), cmd);
#endif

            process.Start();
            // var output = process.StandardOutput.ReadToEnd();
            // var error = process.StandardError.ReadToEnd();
            process.OutputDataReceived += (s, _e) =>
            {
                output += _e.Data + "\n";
                UnityEngine.Debug.LogFormat("[{0}] {1}\n ", cmd, _e.Data);
            };
            process.ErrorDataReceived += (s, _e) =>
            {
                output += _e.Data + "\n ";
            // UnityEngine.Debug.LogError(_e.Data);

        };
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            var exitCode = process.ExitCode;
            process.Close();
            if (exitCode == 0)
            {
                UnityEngine.Debug.LogFormat("[BuildProjectApkClientTool]Assemble {0} Succeed", cmd);
                return true;
            }
            else
            {
                // 失败
                UnityEngine.Debug.LogError(output);
                UnityEngine.Debug.LogErrorFormat("[BuildProjectApkClientTool]Assemble {0} Failed with code {1}", cmd, exitCode);
                return false;
            }
        }

        private static void OpenBuildApkDir(string buildApkDir)
        {
            if (Directory.Exists(buildApkDir))
            {
                Process process = Process.Start(buildApkDir);
                process.WaitForExit();
                process.Close();
                UnityEngine.Debug.LogFormat("<color=green>{0}</color>\n ", "[BuildProjectApkClientTool]Build Succeed, Open Build Apk Dir");
            }
            else
            {
                UnityEngine.Debug.LogError("[BuildProjectApkClientTool]AndroidProject Assemble BuildApk Failed");
            }
        }

        private static string GetBatFile()
        {
#if UNITY_EDITOR_WIN
            return "gradlew.bat";
#elif UNITY_EDITOR_OSX
            return "gradlew";
#else
            return null;
#endif
        }

        private static Encoding GetCmdEncoding()
        {
            Encoding encoding = Encoding.UTF8;
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                encoding = Encoding.GetEncoding("gb2312");
            }
            return encoding;
        }

        private static void ThrowException(string errorMsg)
        {
            UnityEngine.Debug.LogError(errorMsg);
            if (!Application.isBatchMode)
            {
                return;
            }
            throw new Exception(errorMsg);
        }
    }
}