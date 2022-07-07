/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;

namespace FuturePlugin
{
    public static class ProfilerOfflineDostorer
    {
        private static GameObject profilerGameObject;

        static ProfilerOfflineDostorer()
        {
            profilerGameObject = new GameObject("[ProfilerOfflineDostorer]");
            UnityEngine.Object.DontDestroyOnLoad(profilerGameObject);
        }

        public static void BeginRecord()
        {
            if (!profilerGameObject.GetComponent<ProfilerDostorer_InternalBehaviour>())
            {
                profilerGameObject.AddComponent<ProfilerDostorer_InternalBehaviour>();
            }
        }

        private class ProfilerDostorer_InternalBehaviour : MonoBehaviour
        {
            private string m_DebugInfo = string.Empty;

            private void OnGUI()
            {
                GUILayout.Label(string.Format("<size=50>{0}</size>", m_DebugInfo));
            }

            private IEnumerator Start()
            {
                for (int i = 5; i > 0; i--)
                {
                    m_DebugInfo = string.Format("<color=blue>{0}s后开始保存Profiler日志</color>", i);
                    yield return new WaitForSeconds(1);
                }
                string file = Application.persistentDataPath + "/Profiler/ProfilerDostorer_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".log";

                Profiler.logFile = file;
                Profiler.enabled = true;
                Profiler.enableBinaryLog = true;
                for (int i = 5; i > 0; i--)
                {
                    m_DebugInfo = string.Format("<color=red>{0}s后结束保存Profiler日志</color>", i);
                    yield return new WaitForSeconds(1);
                }
                Profiler.enableBinaryLog = false;

                m_DebugInfo = string.Format("保存完毕:{0}", file);
                yield return new WaitForSeconds(10);

                Destroy(this);
            }
        }
    }
}