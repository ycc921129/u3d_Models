/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public class CameraAdaptiveMB : MonoBehaviour
    {
        [HideInInspector]
        private bool isOrthographic;
        [HideInInspector]
        private float orthographicSize;

        private Camera cameraCom;

        public void DoAdaptive(bool isOrthographic, float orthographicSize)
        {
            this.isOrthographic = isOrthographic;
            this.orthographicSize = orthographicSize;
            this.cameraCom = GetComponent<Camera>();

            Adaptive();
        }

#if UNITY_EDITOR
        private float currScreenHeight;
        private float currScreenWidth;

        private void Update()
        {
            if (currScreenHeight != Screen.height || currScreenWidth != Screen.width)
            {
                currScreenHeight = Screen.height;
                currScreenWidth = Screen.width;
                ScreenConst.CurrAspectRatio = (float)Screen.height / Screen.width;
                Adaptive();
            }
        }
#endif

        private void Adaptive()
        {
            if (cameraCom == null) return;

            cameraCom.orthographic = isOrthographic;
            if (isOrthographic)
            {
                if (ScreenConst.CurrAspectRatio > ScreenConst.StandardAspectRatio)
                {
                    float computeHeight = Screen.width * ScreenConst.StandardAspectRatio;
                    float heightRatio = Screen.height / computeHeight;
                    cameraCom.orthographicSize = orthographicSize * heightRatio;
                }
                else
                {
                    cameraCom.orthographicSize = orthographicSize;
                }
            }
            else
            {
                float fov = Get3DFOV();
                cameraCom.fieldOfView = fov;
            }
        }

        private float Get3DFOV()
        {
            float defaultFOV = 60;
            float standardWidth = ScreenConst.StandardWidth;
            float standardHeight = ScreenConst.StandardHeight;
            float currWidth = Screen.width;
            float currHeight = Screen.height;
            float nowHeight;

            if (ScreenConst.CurrAspectRatio > ScreenConst.StandardAspectRatio)
            {
                nowHeight = Mathf.RoundToInt(standardWidth / currWidth * currHeight);
            }
            else
            {
                nowHeight = standardHeight;
            }
            float heightScale = nowHeight / standardHeight;
            float fov = defaultFOV * heightScale;
            return fov;
        }
    }
}