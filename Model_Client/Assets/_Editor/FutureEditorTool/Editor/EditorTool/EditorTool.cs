using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class EditorTool
    {
        public static void ExecuteMenuItem(string menuItem)
        {
            EditorApplication.ExecuteMenuItem(menuItem);
        }

        public static void ExecuteDelayFunc(Action func)
        {
            Func<Task> delayFunc = async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                func();
            };
            delayFunc();
        }

        public static void UpdateInputValueState()
        {
            GUIUtility.keyboardControl = 0;
        }
    }
}