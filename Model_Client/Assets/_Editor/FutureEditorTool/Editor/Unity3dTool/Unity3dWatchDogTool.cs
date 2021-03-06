using UnityEngine;
using UnityEditor;

namespace FutureEditor
{
    [InitializeOnLoad]
    public static class Unity3dWatchDogTool
    {
        private static string[] Unity3dVersions = { "2018.3.11f1", "2018.4.14f1", "2019.1.8f1", "2019.2.7f2" };
        private const bool IsCompilingCloseGame = false;

        static Unity3dWatchDogTool()
        {
            CheckAllUnity3dVersion();

            EditorApplication.update -= Unity3dCompiling;
            EditorApplication.update += Unity3dCompiling;
        }

        private static void CheckAllUnity3dVersion()
        {
            if (EditorState.instance.IsDoCheckAllUnity3dVersion) return;
            EditorState.instance.IsDoCheckAllUnity3dVersion = true;

            bool isVersionRight = false;
            for (int i = 0; i < Unity3dVersions.Length; i++)
            {
                bool res = CheckUnity3dVersion(Unity3dVersions[i]);
                if (res)
                {
                    isVersionRight = true;
                    break;
                }
            }
            if (!isVersionRight)
            {
                ExitUnity3d();
            }
        }

        private static bool CheckUnity3dVersion(string unity3dVersion)
        {
            bool isRight = false;
#if UNITY_2018_3_11 || UNITY_2018_4_14 || UNITY_2019_1_8 || UNITY_2019_2_7
            if (Application.unityVersion == unity3dVersion)
            {
                isRight = true;
            }
#endif
            return isRight;
        }

        private static void ExitUnity3d()
        {
            if (EditorUtility.DisplayDialog("Unity3dWatchDogTool", "Please install the right version\n(" + string.Join(",", Unity3dVersions) + ")", "OK"))
            {
                // 为了方便开发，不强制退出引擎
                //EditorApplication.Exit(1);
            }
        }

        [InitializeOnLoadMethod]
        private static void InitializeOnLoadMethod()
        {
            EditorApplication.wantsToQuit -= Unity3dQuit;
            EditorApplication.wantsToQuit += Unity3dQuit;
        }

        private static void Unity3dCompiling()
        {
            if (EditorApplication.isPlaying && EditorApplication.isCompiling)
            {
                Debug.LogError("<color=red>[Unity3dWatchDogTool]你不能在项目运行时编译项目</color>");
                if (!IsCompilingCloseGame) return;

                EditorApplication.isPlaying = false;
            }
        }

        private static bool Unity3dQuit()
        {
            EditorApplication.isPlaying = false;
            AppTool.RefreshProject();

            return EditorUtility.DisplayDialog(string.Format("【{0}】Unity3dWatchDogTool", EditorAppConst.AppName),
                string.Format("是否关闭【{0}】项目?\n项目描述：【{1}】\n{2}", EditorAppConst.AppName, EditorAppConst.AppDesc, Application.dataPath)
                , "确认", "取消");
        }
    }
}