/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.13
*/

using DG.Tweening;
using FairyGUI;
using FuturePlugin;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FutureCore
{
    public sealed class CameraMgr : BaseMgr<CameraMgr>
    {
        public Transform mainCameraRoot;
        public GameObject mainCameraGo;
        public Camera mainCamera;

        public Transform fguiCameraRoot;
        public GameObject fguiCameraGo;
        public Camera fguiCamera;

        public Transform uiCameraRoot;
        public GameObject uiCameraGo;
        public Camera uiCamera;

        public GameObject otherCameraGo;
        public Camera otherCamera;

        public bool isEnabledWorldRaycast;
        public Physics2DRaycaster physics2DRaycaster;
        public PhysicsRaycaster physics3DRaycaster;

        private bool isMainCameraShakeing;

        #region Coordinate
        public Vector3 CameraToCameraWorldPos(Camera cameraForm, Camera cameraTo, Vector3 worldPosition)
        {
            Vector3 screenPosition = cameraForm.WorldToScreenPoint(worldPosition);
            return cameraTo.ScreenToWorldPoint(screenPosition);
        }

        public Vector2 WorldPosToFGUIPos(Vector3 worldPos)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);
            // 原点位置转换
            screenPos.y = ScreenConst.CurrResolution.y - screenPos.y;
            Vector2 pt = GRoot.inst.GlobalToLocal(screenPos);
            return pt;
        }

        public Vector2 WorldPosToFGUILocalPos(Vector3 worldPos, GObject gObject)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);
            screenPos.y = Screen.height - screenPos.y;
            Vector2 pt = GRoot.inst.GlobalToLocal(screenPos);
            Vector2 logicScreenPos = gObject.RootToLocal(pt, GRoot.inst);
            return pt;
        }

        public Vector3 FGUIPosToWorldPos(Vector3 fguiPos)
        {
            Vector2 screenPos = GRoot.inst.LocalToGlobal(fguiPos);
            // 原点位置转换
            screenPos.y = ScreenConst.CurrResolution.y - screenPos.y;
            // 一般情况下，还需要提供距离摄像机视野正前方distance长度的参数作为screenPos.z(如果需要，将screenPos改为Vector3类型）
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            return worldPos;
        }

        public Vector3 ScreenPosToWorldPos(Vector3 screenPos)
        {
            Vector3 pos = mainCamera.ScreenToWorldPoint(screenPos);
            return pos;
        }

        public Vector2 ScreenPosToWorldPosV2(Vector3 screenPos)
        {
            Vector2 pos = mainCamera.ScreenToWorldPoint(screenPos);
            return pos;
        }

        public Vector3 UIWorldCameraPosToViewportPoint(Vector3 pos)
        {
            Vector3 point = uiCamera.WorldToViewportPoint(pos);
            return point;
        }
        #endregion

        #region Func
        public float GetCameraAdaptiveRatio()
        {
            float value = mainCamera.orthographicSize - (ScreenConst.StandardHeight / 2f / AppConst.PixelsPerUnit);
            return value;
        }

        public float GetHeightAdaptiveRatio()
        {
            float value = (Screen.height / Screen.width) - (ScreenConst.StandardResolution.y / ScreenConst.StandardResolution.x);
            return value;
        }

        public float GetCameraOrthographicSize(Camera camera)
        {
            return camera.orthographicSize;
        }

        public Vector2 GetCameraScreenHalfSize(Camera camera)
        {
            return new Vector2(camera.orthographicSize * camera.aspect, camera.orthographicSize);
        }

        public void BindWorldRaycaster(Physics2DRaycaster physics2DRaycaster)
        {
            this.physics2DRaycaster = physics2DRaycaster;
        }

        public void BindWorldRaycaster(PhysicsRaycaster physics3DRaycaster)
        {
            this.physics3DRaycaster = physics3DRaycaster;
        }

        public void SetWorldRaycasterEnabled(bool enabled)
        {
            isEnabledWorldRaycast = enabled;
            if (physics2DRaycaster != null)
            {
                EventUtil.Set2DRaycasterEnabled(physics2DRaycaster, isEnabledWorldRaycast);
            }
            if (physics3DRaycaster != null)
            {
                EventUtil.Set3DRaycasterEnabled(physics3DRaycaster, isEnabledWorldRaycast);
            }
            AppDispatcher.Instance.Dispatch(AppMsg.WorldRaycast_EnableChange, isEnabledWorldRaycast);
        }

        public void ShakeMainCamera()
        {
            if (isMainCameraShakeing) return;

            isMainCameraShakeing = true;
            Vector3 shakeInitPos = mainCamera.transform.localPosition;
            Tweener tweener = mainCamera.transform.DOShakePosition(1f, 0.2f, 100, 90, false, true);
            tweener.OnComplete(() =>
            {
                isMainCameraShakeing = false;
                mainCamera.transform.localPosition = shakeInitPos;
            });
        }

        public void PanningFarGamePlayCamera()
        {
            mainCamera.orthographicSize = ScreenConst.OrthographicSize_1280H;
        }

        public void PanningNearGamePlayCamera()
        {
            Tweener tweener = DOTween.To(() => mainCamera.orthographicSize,
                x => mainCamera.orthographicSize = x,
                ScreenConst.OrthographicSize_1280H, 0.8f);
            tweener.SetEase(Ease.InSine);
        }

        public bool IsPointInViewport(Camera camera, Vector3 pos)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(pos);
            if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1)
            {
                return false;
            }
            return true;
        }

        public bool IsRendererInCameraViewport(Camera camera, Renderer renderer)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }
        #endregion

        #region Camera
        public void SetCameraResolution(float width, float height)
        {
            ScalableBufferManager.ResizeBuffers(width, height);
        }

        public void CreateMainCamera()
        {
            if (mainCamera) return;

            string name = "MainCamera";
            mainCameraGo = new GameObject(name);
            mainCameraGo.tag = name;
            mainCameraGo.layer = LayerMaskConst.Default;
            mainCameraGo.transform.localPosition = Vector3.zero;
            int cullingMask = LayerMask.GetMask(LayerMaskConst.Default_Name);
            mainCamera = CreateCamera(mainCameraGo, cullingMask: cullingMask);
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
            // 默认不使用后效
            mainCamera.forceIntoRenderTexture = false;

            GameObject root = new GameObject(name + "Root");
            root.transform.position = CameraConst.MainCameraPos;
            root.SetParent(AppObjConst.CameraGo);
            mainCameraGo.SetParent(root);
            mainCameraRoot = root.transform;

            CameraAdaptiveMB adaptiveCom = mainCamera.gameObject.AddComponent<CameraAdaptiveMB>();
            adaptiveCom.DoAdaptive(isOrthographic: true, orthographicSize: ScreenConst.OrthographicSize_1280H);
        }

        public void CreateFGUICamera()
        {
            if (fguiCamera) return;

            StageCamera.CheckMainCamera();
            fguiCamera = StageCamera.main;
            fguiCamera.depth = CameraConst.UICameraDepth;
            // 默认不使用后效
            fguiCamera.forceIntoRenderTexture = false;
            fguiCameraGo = fguiCamera.gameObject;

            GameObject root = new GameObject("FGUICameraRoot");
            root.transform.position = CameraConst.UICameraPos;
            root.SetParent(AppObjConst.CameraGo);
            fguiCameraGo.SetParent(root);
            fguiCameraRoot = root.transform;
        }

        public void CreateUICamera()
        {
            if (uiCamera) return;

            string name = "UICamera";
            uiCameraGo = new GameObject(name);
            uiCameraGo.transform.localPosition = Vector3.zero;
            int cullingMask = LayerMask.GetMask(LayerMaskConst.UI_Name);
            uiCamera = CreateCamera(uiCameraGo, cullingMask: cullingMask);
            uiCamera.depth = CameraConst.UICameraDepth;
            // 默认不使用后效
            uiCamera.forceIntoRenderTexture = false;

            GameObject root = new GameObject(name + "Root");
            root.transform.position = CameraConst.UICameraPos;
            root.SetParent(AppObjConst.CameraGo);
            uiCameraGo.SetParent(root);
            uiCameraRoot = root.transform;

            CameraAdaptiveMB adaptiveCom = uiCamera.gameObject.AddComponent<CameraAdaptiveMB>();
            adaptiveCom.DoAdaptive(isOrthographic: true, orthographicSize: ScreenConst.OrthographicSize_1280H);
        }

        public Camera CreateOtherCamera(Vector3 position, Vector3 rotation, bool isOrthographic_param, float orthographicSize_param)
        {
            if (otherCamera) return otherCamera;

            string name = "OtherCamera";
            otherCameraGo = new GameObject(name);
            otherCameraGo.layer = LayerMaskConst.Default;
            otherCameraGo.transform.localPosition = Vector3.zero;
            int cullingMask = LayerMask.GetMask(LayerMaskConst.Default_Name);
            otherCamera = CreateCamera(otherCameraGo, cullingMask: cullingMask);
            // 默认不使用后效
            otherCamera.forceIntoRenderTexture = false;

            GameObject root = new GameObject(name + "Root");
            root.transform.position = position;
            root.transform.localEulerAngles = rotation;
            root.SetParent(AppObjConst.CameraGo);
            otherCameraGo.SetParent(root);

            CameraAdaptiveMB adaptiveCom = otherCamera.gameObject.AddComponent<CameraAdaptiveMB>();
            adaptiveCom.DoAdaptive(isOrthographic: isOrthographic_param, orthographicSize : orthographicSize_param);

            return otherCamera;
        }

        public Camera CreateCamera(GameObject cameraGo, int cullingMask)
        {
            Camera cameraCom = cameraGo.AddComponent<Camera>();
            cameraCom.clearFlags = CameraClearFlags.Depth;
            cameraCom.backgroundColor = Color.black;
            cameraCom.cullingMask = cullingMask;
            cameraCom.nearClipPlane = -30f;
            cameraCom.farClipPlane = 30f;
            cameraCom.rect = new Rect(0, 0, 1f, 1f);
            cameraCom.depth = CameraConst.MainDepth;
            cameraCom.renderingPath = RenderingPath.UsePlayerSettings;
            cameraCom.useOcclusionCulling = false;
            cameraCom.allowHDR = false;
            cameraCom.allowMSAA = false;
            // 默认不使用后效
            cameraCom.forceIntoRenderTexture = false;
            // 启用动态分辨率
            //cameraCom.allowDynamicResolution = true;
            return cameraCom;
        }
        #endregion

        #region Mgr
        public override void Init()
        {
            base.Init();
            InitCameraMgr();

            CreateMainCamera();
            CreateFGUICamera();
        }

        private void InitCameraMgr()
        {
            AppObjConst.CameraGo = new GameObject(AppObjConst.CameraGoName);
            AppObjConst.CameraGo.SetParent(AppObjConst.FutureFrameGo);
        }

        public override void Dispose()
        {
            base.Dispose();
            EngineUtil.Destroy(AppObjConst.CameraGo);
        }
        #endregion
    }
}