/*
 Author:du
 Time:2019.12.27
*/

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FutureEditor
{
    [InitializeOnLoad]
    public static class CheckProgramRuntimeTool
    {
        static CheckProgramRuntimeTool()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (SceneManager.GetActiveScene().name == AppEditorInfo.MainSceneName)
            {
                if (state == PlayModeStateChange.ExitingEditMode)
                {
                    AppTool.RefreshProject();
                    DoCheck();
                }
            }
        }

        public static void DoCheck()
        {
            Debug.Log("[CheckProgramRuntimeTool]检查程序运行时正确性");
            bool isCorrect1 = CheckMsgDefineSameValueTool.CheckMsgDefineSameValue();
            bool isCorrect2 = CheckNamespaceTool.CheckNamespace();

            if (isCorrect1 && isCorrect2)
            {
                Debug.Log("<color=green>[CheckProgramRuntimeTool]检查程序运行时正确性: 检查完毕</color>");
            }
            else
            {
                EditorApplication.isPlaying = false;
                Debug.LogError("[CheckProgramRuntimeTool]检查程序运行时正确性: 发现错误!");
            }
        }
    }
}