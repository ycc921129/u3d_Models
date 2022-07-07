using UnityEditor;

namespace FutureEditor
{
    public static class BuildSettingTool
    {
        public static string FirstBundleVersion = "1.0.1";
        public static int FirstBundleVersionCode = 1;

        [MenuItem("[FC Release]/BuildSetting/重置客户端构建版本号", false, 0)]
        private static void ResetClientVersion()
        {
            PlayerSettings.bundleVersion = FirstBundleVersion;
            PlayerSettings.Android.bundleVersionCode = FirstBundleVersionCode;
        }
    }
}