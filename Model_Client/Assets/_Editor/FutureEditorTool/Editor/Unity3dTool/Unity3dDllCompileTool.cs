/*
 Author:du
 Time:2017.11.23
*/

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    [InitializeOnLoad]
    public static class Unity3dDllCompileTool
    {
        private const string s_CompiledKey = "UnityDllCompileTool_isCompiled";
        private const string s_CompileTimeTickKey = "UnityDllCompileTool_CompileTimeTick";
        private static bool isCompiled;

        static Unity3dDllCompileTool()
        {
            isCompiled = EditorPrefs.GetBool(s_CompiledKey, false);
            EditorApplication.update -= Update;
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            if (!isCompiled && EditorApplication.isCompiling)
            {
                isCompiled = true;
                EditorPrefs.SetBool(s_CompiledKey, isCompiled);
                DateTime dateTime = DateTime.Now;
                EditorPrefs.SetString(s_CompileTimeTickKey, dateTime.Ticks.ToString());
                Debug.Log(string.Format("[Unity3dDllCompileTool]Start: {0}", dateTime.ToString("yyyy-MM-dd HH:mm:ss")));
            }
            else if (isCompiled && !EditorApplication.isCompiling)
            {
                isCompiled = false;
                EditorPrefs.SetBool(s_CompiledKey, isCompiled);
                DateTime dateTime = DateTime.Now;
                long lastCompileTime = 0;
                if (EditorPrefs.HasKey(s_CompileTimeTickKey))
                {
                    string lastCompileTimeStr = EditorPrefs.GetString(s_CompileTimeTickKey);
                    lastCompileTime = long.Parse(lastCompileTimeStr);
                }
                else
                {
                    lastCompileTime = dateTime.Ticks;
                }
                TimeSpan tSpan = new TimeSpan(dateTime.Ticks - lastCompileTime);
                Debug.Log(string.Format("[Unity3dDllCompileTool]End: {0} Time: {1}s", dateTime.ToString("yyyy-MM-dd HH:mm:ss"), tSpan.TotalSeconds));
            }
            return;
        }

        private static void Unity3dPrecompiledAssemblies()
        {
            Type internalEditorUtilityType = typeof(Editor).Assembly.GetType("UnityEditorInternal.InternalEditorUtility");
            MethodInfo precompiledAssembliesMethod = internalEditorUtilityType.GetMethod("GetPrecompiledAssemblies", BindingFlags.Static | BindingFlags.NonPublic);
            precompiledAssembliesMethod.Invoke(null, null);
        }

        public static bool IsEditorCompiling()
        {
            return EditorApplication.isCompiling;
        }
    }
}