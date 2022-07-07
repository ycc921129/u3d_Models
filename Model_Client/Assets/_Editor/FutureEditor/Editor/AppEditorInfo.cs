using System.IO;
using UnityEngine;

namespace FutureEditor
{
#if UNITY_ANDROID || UNITY_STANDALONE

    /// <summary>
    /// 编辑器配置信息
    /// </summary>
    public static class AppEditorInfo
    {
        /// <summary>
        /// 项目文件夹
        /// </summary>
        public static string ProjectFolder = Application.dataPath + "/../../";
        /// <summary>
        /// SVN文件夹
        /// </summary>
        public static string SVNFolder = "$SVN" + "_" + EditorAppConst.AppName + "";
        /// <summary>
        /// SVN目录
        /// </summary>
        public static string SVNPath = Path.GetFullPath(Application.dataPath + @"\..\..\" + SVNFolder + @"\");
        /// <summary>
        /// 配置表SVN目录
        /// </summary>
        public static string ExcelSVNPath = SVNPath + @"策划\配置表\";
        /// <summary>
        /// 界面SVN目录
        /// </summary>
        public static string UISVNPath = SVNPath + @"美术\FGUI\";
        /// <summary>
        /// 多语言翻译SVN目录
        /// </summary>
        public static string MultiLanguageSVNPath = SVNPath + @"翻译\";
        /// <summary>
        /// 入口场景名
        /// </summary>
        public const string MainSceneName = "0_Main";
        /// <summary>
        /// 入口场景
        /// </summary>
        public const string MainScenePath = "Assets/_AppBase/Scene/" + MainSceneName + ".unity";
        /// <summary>
        /// 测试场景
        /// </summary>
        public const string TestScenePath = "Assets/_AppBase/Scene/0_Test.unity";
    }

#elif UNITY_IOS

    /// <summary>
    /// 编辑器配置
    /// </summary>
    public static class AppEditorInfo
    {
        /// <summary>
        /// 项目文件夹
        /// </summary>
        public static string ProjectFolder = Application.dataPath + "/../../";
        /// <summary>
        /// SVN文件夹
        /// </summary>
        public static string SVNFolder = "$SVN" + "_" + EditorAppConst.AppName + "_iOS";
        /// <summary>
        /// SVN目录
        /// </summary>
        public static string SVNPath = Path.GetFullPath(Application.dataPath + @"\..\..\" + SVNFolder + @"\");
        /// <summary>
        /// 配置表SVN目录
        /// </summary>
        public static string ExcelSVNPath = SVNPath + @"策划\配置表\";
        /// <summary>
        /// 界面SVN目录
        /// </summary>
        public static string UISVNPath = SVNPath + @"美术\FGUI\";
        /// <summary>
        /// 多语言翻译SVN目录
        /// </summary>
        public static string MultiLanguageSVNPath = SVNPath + @"翻译\";
        /// <summary>
        /// 入口场景名
        /// </summary>
        public const string MainSceneName = "0_Main";
        /// <summary>
        /// 入口场景
        /// </summary>
        public const string MainScenePath = "Assets/_AppBase/Scene/" + MainSceneName + ".unity";
        /// <summary>
        /// 测试场景
        /// </summary>
        public const string TestScenePath = "Assets/_AppBase/Scene/0_Test.unity";
    }

#endif
}