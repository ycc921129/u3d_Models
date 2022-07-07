/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
如何使用按需渲染呢？

按需渲染的API包含在UnityEngine.Rendering命名空间中，由三种属性组成。

☞OnDemandRendering.renderFrameInterval
该API是最重要的部分。我们可以用它来取得或设置渲染帧间隔，确定帧率。

渲染帧间隔是确定Application.targetFrameRate及QualitySettings.vSyncCount间隔的要素。

打个比方，我们将Application.targetFrameRate设置为60，将OnDemandRendering.renderFrameInterval设置为2，则系统会每隔一帧渲染一次，最终帧率便为30fps。

☞OnDemandRendering.effectiveFrameRate
该属性会给出应用渲染帧率的估算值。

估算值根据OnDemandRendering.renderFrameInterval、Application.targetFrameRate
和QualitySetting.vSyncCount的数值，以及显示器刷新频率计算得出。请注意这只是个估算值，并不一定是实际表现；当CPU同时在处理其他脚本、物理模拟、网络传输等等进程时，效率难免会降低，应用的渲染速度也可能变慢。

☞OnDemandRendering.willThisFrameRender
该API会告知当前帧是否会被渲染到屏幕上。我们可以在不渲染的帧上执行一些吃CPU性能的进程，比如复杂的数学运算、加载资源或生成预制件。
 */

using UnityEngine;
using UnityEngine.Rendering;

namespace FutureCore
{
    /// <summary>
    /// 按需渲染
    /// </summary>
    public class OnDemandRendering : MonoBehaviour
    {
        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            // When the Menu starts, set the rendering to target 20fps
            //OnDemandRendering.renderFrameInterval = 3;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) || (Input.touchCount > 0))
            {
                // If the mouse button or touch detected render at 60 FPS (every frame).
                //OnDemandRendering.renderFrameInterval = 1;
            }
            else
            {
                // If there is no mouse and no touch input then we can go back to 20 FPS (every 3 frames).
                //OnDemandRendering.renderFrameInterval = 3;
            }
        }
    }
}