using Newtonsoft.Json;
using System.Collections;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Rendering;

namespace FutureEditor
{
    public static class BuildClientTool
    {
        /// <summary>
        /// AotOptions:
        /// nrgctx-trampolines=8192 这是留给递归泛型使用的空间，默认是1024
        /// nimt-trampolines=8192 这是留给接口使用的空间，默认是128
        /// ntrampolines=4096 这是留给泛型方法调用使用的空间，默认是1024
        /// </summary>
        public const string AotOptions = "nrgctx-trampolines=8192,nimt-trampolines=8192,ntrampolines=4096";

        public static string AppInfoFolder = Application.dataPath + "/_App/PresetRes/Resources/Preset/VersionMgr/";

        public static void CreateAppInfo(Hashtable appInfo)
        {
            appInfo["BundleVersionCode"] = PlayerSettings.Android.bundleVersionCode;
            string configJson = JsonConvert.SerializeObject(appInfo, Formatting.None);
            FileTool.CreateFile(AppInfoFolder + "AppInfo.json", configJson);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static BuildReport BuildCppPlayer(string productName, string buildPath, BuildTarget buildTarget, BuildOptions buildOptions)
        {
            SetProjectSettings();
            Unity3dScriptTool.SetReleaseDefineSymbol();
            return Internal_BuildCppPlayer(productName, buildPath, buildTarget, buildOptions);
        }

        public static BuildReport BuildMonoPlayer(string productName, string buildPath, BuildTarget buildTarget, BuildOptions buildOptions)
        {
            SetProjectSettings();
            Unity3dScriptTool.SetReleaseDefineSymbol();
            return Internal_BuildMonoPlayer(productName, buildPath, buildTarget, buildOptions);
        }

        private static void SetProjectSettings()
        {
            // [ProjectSettings] 优化项勾选
            // [ProjectSettings] 全部选择安装到内部
            // [ProjectSettings Audio] DSPBufferSize: Best performance
            // [ProjectSettings Physics] EnableEnhancedDeteerminism: true
        }

        private static BuildReport Internal_BuildCppPlayer(string productName, string buildPath, BuildTarget buildTarget, BuildOptions buildOptions)
        {
            // 优化项
            PlayerSettings.MTRendering = true;
            PlayerSettings.gpuSkinning = true;
            PlayerSettings.graphicsJobs = false;
            PlayerSettings.graphicsJobMode = GraphicsJobMode.Legacy;
            //PlayerSettings.StaticBatching = true;
            //PlayerSettings.DynamicBatching = true;
            //PlayerSettings.OptimizeMeshData = true;
            //PlayerSettings.enableFrameTimingStats = true;

            // 通用项
            EditorUserBuildSettings.development = false;
            PlayerSettings.allowUnsafeCode = true;
            PlayerSettings.scriptingRuntimeVersion = ScriptingRuntimeVersion.Latest;
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
#if UNITY_2019
            PlayerSettings.gcIncremental = true;
#endif

            // 安卓项
            EditorUserBuildSettings.androidETC2Fallback = AndroidETC2Fallback.Quality32Bit;
            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
            EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
            PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new GraphicsDeviceType[2] { GraphicsDeviceType.OpenGLES3, GraphicsDeviceType.OpenGLES2 });
            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_4_6);
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.Android, Il2CppCompilerConfiguration.Master);

            // iOS项
            PlayerSettings.SetGraphicsAPIs(BuildTarget.iOS, new GraphicsDeviceType[2] { GraphicsDeviceType.OpenGLES3, GraphicsDeviceType.OpenGLES2 });
            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_4_6);
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.iOS, Il2CppCompilerConfiguration.Release);
            PlayerSettings.aotOptions = AotOptions;
#if UNITY_IOS
            PlayerSettings.MTRendering = false;
#endif

            // 项目项
            PlayerSettings.companyName = "games" + productName;
            PlayerSettings.productName = productName;
            string applicationIdentifier = string.Format("com.{0}.{1}", PlayerSettings.companyName, PlayerSettings.productName);
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, applicationIdentifier);
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, applicationIdentifier);
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, applicationIdentifier);

            BuildReport buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, buildPath, buildTarget, buildOptions);
            Debug.Log("[BuildClientTool]BuildCppPlayer:" + buildPath);
            return buildReport;
        }

        private static BuildReport Internal_BuildMonoPlayer(string productName, string buildPath, BuildTarget buildTarget, BuildOptions buildOptions)
        {
            // 优化项
            PlayerSettings.MTRendering = true;
            PlayerSettings.gpuSkinning = true;
            PlayerSettings.graphicsJobs = false;
            PlayerSettings.graphicsJobMode = GraphicsJobMode.Legacy;
            //PlayerSettings.StaticBatching = true;
            //PlayerSettings.DynamicBatching = true;
            //PlayerSettings.OptimizeMeshData = true;
            //PlayerSettings.enableFrameTimingStats = true;

            // 通用项
            EditorUserBuildSettings.development = false;
            PlayerSettings.allowUnsafeCode = true;
            PlayerSettings.scriptingRuntimeVersion = ScriptingRuntimeVersion.Latest;
#if UNITY_2019
            PlayerSettings.gcIncremental = true;
#endif

            // 安卓项
            EditorUserBuildSettings.androidETC2Fallback = AndroidETC2Fallback.Quality32Bit;
            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
            EditorUserBuildSettings.exportAsGoogleAndroidProject = false; //Change
            PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new GraphicsDeviceType[2] { GraphicsDeviceType.OpenGLES3, GraphicsDeviceType.OpenGLES2 });
            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_4_6);
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x); //Change
            PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.Android, Il2CppCompilerConfiguration.Master);

            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7; //Change

            // iOS项
            PlayerSettings.SetGraphicsAPIs(BuildTarget.iOS, new GraphicsDeviceType[2] { GraphicsDeviceType.OpenGLES3, GraphicsDeviceType.OpenGLES2 });
            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_4_6);
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.iOS, Il2CppCompilerConfiguration.Release);
            PlayerSettings.aotOptions = AotOptions;
#if UNITY_IOS
            PlayerSettings.MTRendering = false;
#endif

            // 项目项
            PlayerSettings.companyName = "games" + productName;
            PlayerSettings.productName = productName;
            string applicationIdentifier = string.Format("com.{0}.{1}mono", PlayerSettings.companyName, PlayerSettings.productName);
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, applicationIdentifier);
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, applicationIdentifier);
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, applicationIdentifier);

            BuildReport buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, buildPath, buildTarget, buildOptions);
            Debug.Log("[BuildClientTool]BuildMonoPlayer:" + buildPath);
            return buildReport;
        }
    }
}