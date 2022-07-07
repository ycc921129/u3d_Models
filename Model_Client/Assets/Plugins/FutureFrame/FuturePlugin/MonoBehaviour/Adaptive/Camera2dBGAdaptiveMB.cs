/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FuturePlugin
{
    public class Camera2dBGAdaptiveMB : MonoBehaviour
    {
        //基于iPhone4比例的单位高度
        private float devHeight = 9.6f;
        //基于iPhone4比例的单位宽度
        private float devWidth = 6.4f;

        private void Start()
        {
            float screenHeight = Screen.height;
            //摄像机的尺寸
            float orthographicSize = GetComponent<Camera>().orthographicSize;
            //宽高比
            float aspectRatio = Screen.width * 1.0f / Screen.height;
            //摄像机的单位宽度
            float cameraWidth = orthographicSize * 2 * aspectRatio;

            //如果设备的宽度大于摄像机的宽度的时候  调整摄像机的orthographicSize
            if (cameraWidth < devWidth)
            {
                orthographicSize = devWidth / (2 * aspectRatio);
                GetComponent<Camera>().orthographicSize = orthographicSize;
            }
        }
    }
}