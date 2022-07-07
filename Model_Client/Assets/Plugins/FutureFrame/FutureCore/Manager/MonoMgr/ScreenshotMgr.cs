/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections;
using UnityEngine;

namespace FutureCore
{
    public sealed class ScreenshotMgr : BaseMonoMgr<ScreenshotMgr>
    {
        private string fileName = "Screenshot.png";
        private int captureX = 0;
        private int captureY = 0;
        private int captureWidth = (int)ScreenConst.StandardWidth;
        private int captureHeight = (int)ScreenConst.StandardHeight;

        [ContextMenu("自定义截屏")]
        public void CustomCaptureScreenshot()
        {
            StartCoroutine(Internal_CustomCaptureScreenshot());
        }

        [ContextMenu("截屏")]
        public void CaptureScreenshot()
        {
            StartCoroutine(Internal_CaptureScreenshot());
        }

        private IEnumerator Internal_CustomCaptureScreenshot()
        {
            yield return new WaitForEndOfFrame();
            Texture2D texture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, true);
            texture.ReadPixels(new Rect(captureX, captureY, captureWidth, captureHeight), 0, 0, false);
            texture.Apply(); ;
            byte[] bytes = texture.EncodeToPNG();
            string path = Application.dataPath.Replace("Assets", string.Empty);
            System.IO.File.WriteAllBytes(path + "/" + fileName, bytes);
        }

        private IEnumerator Internal_CaptureScreenshot()
        {
            yield return new WaitForEndOfFrame();
            ScreenCapture.CaptureScreenshot(fileName);
        }
    }
}