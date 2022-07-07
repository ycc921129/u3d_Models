using System;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class CaptureScreenTool
    {
        [MenuItem("[FC Toolkit]/CaptureScreen/游戏截屏", false, 0)]
        public static void CaptureScreenshot()
        {
            string path = string.Format("_CaptureScreen/游戏截屏_{0}.png", DateTime.Now.ToString("yyyyMMddHHmmss"));
            ScreenCapture.CaptureScreenshot(path);
            Debug.Log("游戏截屏完成!");
        }
    }
}
