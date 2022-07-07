/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using FuturePlugin;
using UnityEngine;
using UnityEngine.Profiling;

namespace FutureCore
{
    public class AssistAppProfilerMB : MonoBehaviour
    {
        public static float offsetX = 50;
        public static float offsetY = 10;
        private static int fontSize = (int)(20 * ScreenConst.HeightRatio);

        public bool allowDebugging = true;

        private GUIStyle guiStyle = new GUIStyle();
        private Color textColor = Color.green;
        private Vector2 textPos = new Vector2(offsetX, offsetY + fontSize);
        private string textInfo = null;

        private float deltaTime = 0f;
        private float updateTime = 1f;

        private void Start()
        {
            guiStyle.fontSize = fontSize;
            guiStyle.normal.textColor = textColor;

            if (allowDebugging)
            {
                Application.logMessageReceivedThreaded += OnLogHandler;
            }
        }

        private void OnDestory()
        {
            if (allowDebugging)
            {
                Application.logMessageReceivedThreaded -= OnLogHandler;
            }
        }

        private void OnLogHandler(string condition, string stackTrace, LogType type)
        {
            if (!enabled) return;

            if (type == LogType.Exception || type == LogType.Error)
            {
                textColor = Color.red;
            }
        }

        private void Update()
        {
            deltaTime += Time.deltaTime;
            if (deltaTime >= updateTime)
            {
                string fps = (1.0f / Time.smoothDeltaTime).ToString("0");

                textInfo =
                //帧数FPS
                "FPS:" + fps
                //总内存
                + " 总内存" + GetSize(Profiler.GetTotalReservedMemoryLong())
                //已占用内存
                + "-占用" + GetSize(Profiler.GetTotalAllocatedMemoryLong())
                //总Mono堆内存 (C#)
                + " 堆内存" + GetSize(Profiler.GetMonoHeapSizeLong())
                //已占用Mono堆内存 (C#)
                + "-占用" + GetSize(Profiler.GetMonoUsedSizeLong());

                deltaTime = 0f;
            }
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(textPos.x, Screen.height - textPos.y, Screen.width, 100), textInfo, guiStyle);
        }

        private string GetSize(long length)
        {
            float size = Mathf.Floor(length / MathConst.OneMBSize);
            string result = (int)size + "M";
            return result;
        }
    }
}