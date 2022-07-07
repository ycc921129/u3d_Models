/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace FuturePlugin
{
    /// <summary>
    /// GameView's Camera Add This Component
    /// </summary>
    public class SyncUnitySceneViewCamera : MonoBehaviour
    {
        private SceneView view = null;

        private void Awake()
        {
            view = SceneView.lastActiveSceneView;
        }

        #region 方法一
        private void Update()
        {
            Camera cameraMain = Camera.main;
            SceneView sceneView = SceneView.lastActiveSceneView;
            if (sceneView != null)
            {
                sceneView.cameraSettings.nearClip = cameraMain.nearClipPlane;
                sceneView.cameraSettings.fieldOfView = cameraMain.fieldOfView;
                sceneView.pivot = cameraMain.transform.position + cameraMain.transform.forward * sceneView.cameraDistance;
                sceneView.rotation = cameraMain.transform.rotation;
            }
        }
        #endregion 方法一

        #region 方法二
        private void LateUpdate()
        {
            if (view != null)
            {
                view.LookAt(transform.position, transform.rotation, 0f);
            }
        }

        private void OnDestroy()
        {
            if (view != null)
            {
                view.LookAt(transform.position, transform.rotation, 5f);
            }
        }
        #endregion 方法二
    }
}

#endif