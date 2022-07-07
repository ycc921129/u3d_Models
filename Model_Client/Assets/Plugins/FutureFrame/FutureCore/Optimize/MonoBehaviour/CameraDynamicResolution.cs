/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    /// <summary>
    /// 摄像机动态分辨率
    /// 1) 在ProjectSetting上必须勾选Enable Frame Timing Stats属
    /// 2) 给需要降低分辨率的摄像机打开allow Dynamic Resolution属性
    /// </summary>
    public class CameraDynamicResolution : MonoBehaviour
    {
        private string textInfo;

        FrameTiming[] frameTimings = new FrameTiming[3];

        public float maxResolutionWidthScale = 1.0f;
        public float maxResolutionHeightScale = 1.0f;
        public float minResolutionWidthScale = 0.1f;
        public float minResolutionHeightScale = 0.1f;
        public float scaleWidthIncrement = 0.1f;
        public float scaleHeightIncrement = 0.1f;

        float m_widthScale = 1.0f;
        float m_heightScale = 1.0f;

        // Variables for dynamic resolution algorithm that persist across frames
        uint m_frameCount = 0;

        const uint kNumFrameTimings = 2;

        double m_gpuFrameTime;
        double m_cpuFrameTime;

        private void Start()
        {
            int rezWidth = (int)Mathf.Ceil(ScalableBufferManager.widthScaleFactor * Screen.currentResolution.width);
            int rezHeight = (int)Mathf.Ceil(ScalableBufferManager.heightScaleFactor * Screen.currentResolution.height);
            textInfo = string.Format("Scale: {0:F3}x{1:F3}\nResolution: {2}x{3}\n",
                m_widthScale,
                m_heightScale,
                rezWidth,
                rezHeight);
        }

        private void OnGUI()
        {
            float oldWidthScale = m_widthScale;
            float oldHeightScale = m_heightScale;

            // One finger lowers the resolution
            if (GUILayout.Button("<size=100>--</size>"))
            {
                m_heightScale = Mathf.Max(minResolutionHeightScale, m_heightScale - scaleHeightIncrement);
                m_widthScale = Mathf.Max(minResolutionWidthScale, m_widthScale - scaleWidthIncrement);
            }

            // Two fingers raises the resolution
            if (GUILayout.Button("<size=100>++</size>"))
            {
                m_heightScale = Mathf.Min(maxResolutionHeightScale, m_heightScale + scaleHeightIncrement);
                m_widthScale = Mathf.Min(maxResolutionWidthScale, m_widthScale + scaleWidthIncrement);
            }

            if (m_widthScale != oldWidthScale || m_heightScale != oldHeightScale)
            {
                ModifiedResolution(m_widthScale, m_heightScale);
            }

            GUILayout.Label(string.Format("<size=100>{0}</size>", textInfo));
        }

        private void Update()
        {
            DetermineResolution();
            int rezWidth = (int)Mathf.Ceil(ScalableBufferManager.widthScaleFactor * Screen.currentResolution.width);
            int rezHeight = (int)Mathf.Ceil(ScalableBufferManager.heightScaleFactor * Screen.currentResolution.height);
            textInfo = string.Format("Scale: {0:F3}x{1:F3}\n��̬�ֱ���: {2}x{3}\nScaleFactor: {4:F3}x{5:F3}\nGPU: {6:F3} CPU: {7:F3}",
                m_widthScale,
                m_heightScale,
                rezWidth,
                rezHeight,
                ScalableBufferManager.widthScaleFactor,
                ScalableBufferManager.heightScaleFactor,
                m_gpuFrameTime,
                m_cpuFrameTime);
        }

        private void DetermineResolution()
        {
            ++m_frameCount;
            if (m_frameCount <= kNumFrameTimings)
            {
                return;
            }

            FrameTimingManager.CaptureFrameTimings();
            FrameTimingManager.GetLatestTimings(kNumFrameTimings, frameTimings);
            if (frameTimings.Length < kNumFrameTimings)
            {
                LogUtil.LogFormat("Skipping frame {0}, didn't get enough frame timings.", m_frameCount);
                return;
            }

            m_gpuFrameTime = frameTimings[0].gpuFrameTime;
            m_cpuFrameTime = frameTimings[0].cpuFrameTime;
        }

        public void ModifiedResolution(float widthScale, float heightScale)
        {
            ScalableBufferManager.ResizeBuffers(widthScale, heightScale);
        }
    }
}