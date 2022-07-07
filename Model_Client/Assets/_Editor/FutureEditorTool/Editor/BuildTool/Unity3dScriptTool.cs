using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Callbacks;
using UnityEngine;

namespace FutureEditor
{
    public class Unity3dScriptTool : IActiveBuildTargetChanged
    {
        private static List<string> DevelopScriptDefineSymbol = new List<string>
        {
            "C_ENABLE_NORMAL_LOG",  // 开启普通日志
            "C_ENABLE_WARN_LOG",    // 开启警告日志
            "C_ENABLE_ERROR_LOG",   // 开启错误日志

            "FAIRYGUI_TEST",        // FGUI可视化测试
            //"RTL_TEXT_SUPPORT",   // FGUI从右向左排版的阿拉伯语言文字 (这个是全局设置，不适合做多语言，因此不开启)
        };

        private static List<string> ReleaseScriptDefineSymbol = new List<string>
        {
            "C_ENABLE_NORMAL_LOG",  // 开启普通日志
            "C_ENABLE_WARN_LOG",    // 开启警告日志
            "C_ENABLE_ERROR_LOG",   // 开启错误日志

            //"RTL_TEXT_SUPPORT",   // FGUI从右向左排版的阿拉伯语言文字 (这个是全局设置，不适合做多语言，因此不开启)
        };

        public int callbackOrder { get { return 0; } }

        [DidReloadScripts(0)]
        private static void OnUnity3dScriptReload()
        {
            Debug.Log("[Unity3dScriptTool]脚本编译完毕");
        }

        public void OnActiveBuildTargetChanged(BuildTarget previousTarget, BuildTarget newTarget)
        {
            PlatformTool.DynamicPlatformFolder();
            Debug.Log("[Unity3dScriptTool]编译平台切换完毕");
        }

        [MenuItem("[FC Release]/DefineSymbols/设置代码宏定义", false, -50)]
        private static void SetProjectDefineSymbol_MenuItem()
        {
            SetDevelopDefineSymbol();
        }

        public static void SetDevelopDefineSymbol()
        {
            PlatformTool.DynamicPlatformFolder();

            SetDefineSymbol(BuildTargetGroup.Standalone, false);
            SetDefineSymbol(BuildTargetGroup.Android, false);
            SetDefineSymbol(BuildTargetGroup.iOS, false);
        }

        public static void SetReleaseDefineSymbol()
        {
            PlatformTool.DynamicPlatformFolder();
            EditorUserBuildSettings.androidCreateSymbolsZip = true; 
            SetDefineSymbol(BuildTargetGroup.Standalone, true);
            SetDefineSymbol(BuildTargetGroup.Android, true);
            SetDefineSymbol(BuildTargetGroup.iOS, true);
        }

        private static void SetDefineSymbol(BuildTargetGroup target, bool isRelease)
        {
            string currDefineSymbol = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);
            string setDefineSymbol = currDefineSymbol;
            if (!isRelease)
            {
                for (int i = 0; i < DevelopScriptDefineSymbol.Count; i++)
                {
                    string item = DevelopScriptDefineSymbol[i];
                    if (!setDefineSymbol.Contains(item))
                    {
                        setDefineSymbol += (";" + item);
                    }
                }
            }
            else
            {
                for (int i = 0; i < DevelopScriptDefineSymbol.Count; i++)
                {
                    string item = DevelopScriptDefineSymbol[i];
                    if (setDefineSymbol.Contains((item + ";")))
                    {
                        setDefineSymbol = setDefineSymbol.Replace((item + ";"), string.Empty);
                    }
                    else if (setDefineSymbol.Contains(item))
                    {
                        setDefineSymbol = setDefineSymbol.Replace(item, string.Empty);
                    }
                }
                for (int i = 0; i < ReleaseScriptDefineSymbol.Count; i++)
                {
                    string item = ReleaseScriptDefineSymbol[i];
                    if (!setDefineSymbol.Contains(item))
                    {
                        setDefineSymbol += (";" + item);
                    }
                }
            }

            if (currDefineSymbol != setDefineSymbol)
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(target, setDefineSymbol);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private static void ClearDefineSymbol(BuildTargetGroup target)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(target, null);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void SwitchActiveBuildTarget(BuildTarget buildTarget)
        {
            EditorUserBuildSettings.activeBuildTargetChanged = delegate ()
            {
                if (EditorUserBuildSettings.activeBuildTarget == buildTarget)
                {
                    Debug.Log("[Unity3dScriptTool]编译平台切换完毕");
                }
            };
            EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);
        }
    }
}