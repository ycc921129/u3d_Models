/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FutureCore
{
    public static class EventUtil
    {
        #region Raycaster
        public static Physics2DRaycaster Add2DRaycaster(Camera camera)
        {
            Physics2DRaycaster com = camera.gameObject.GetComponent<Physics2DRaycaster>();
            if (com == null)
            {
                com = camera.gameObject.AddComponent<Physics2DRaycaster>();
            }
            return com;
        }

        public static PhysicsRaycaster Add3DRaycaster(Camera camera)
        {
            PhysicsRaycaster com = camera.gameObject.GetComponent<PhysicsRaycaster>();
            if (com == null)
            {
                com = camera.gameObject.AddComponent<PhysicsRaycaster>();
            }
            return com;
        }

        public static GraphicRaycaster AddUIRaycaster(Camera camera)
        {
            GraphicRaycaster com = camera.gameObject.GetComponent<GraphicRaycaster>();
            if (com == null)
            {
                com = camera.gameObject.AddComponent<GraphicRaycaster>();
            }
            return com;
        }

        public static Physics2DRaycaster Get2DRaycaster(Camera camera)
        {
            Physics2DRaycaster com = camera.gameObject.GetComponent<Physics2DRaycaster>();
            return com;
        }

        public static PhysicsRaycaster Get3DRaycaster(Camera camera)
        {
            PhysicsRaycaster com = camera.gameObject.GetComponent<PhysicsRaycaster>();
            return com;
        }

        public static GraphicRaycaster GetUIRaycaster(Camera camera)
        {
            GraphicRaycaster com = camera.gameObject.GetComponent<GraphicRaycaster>();
            return com;
        }

        public static void Set2DRaycasterEnabled(Physics2DRaycaster com, bool enabled)
        {
            if (com.enabled != enabled)
            {
                com.enabled = enabled;
            }
        }

        public static void Set3DRaycasterEnabled(PhysicsRaycaster com, bool enabled)
        {
            if (com.enabled != enabled)
            {
                com.enabled = enabled;
            }
        }

        public static void SetUIRaycasterEnabled(GraphicRaycaster com, bool enabled)
        {
            if (com.enabled != enabled)
            {
                com.enabled = enabled;
            }
        }

        public static void Set2DRaycasterEnabled(Camera camera, bool enabled)
        {
            Physics2DRaycaster com = Get2DRaycaster(camera);
            if (com.enabled != enabled)
            {
                com.enabled = enabled;
            }
        }

        public static void Set3DRaycasterEnabled(Camera camera, bool enabled)
        {
            PhysicsRaycaster com = Get3DRaycaster(camera);
            if (com.enabled != enabled)
            {
                com.enabled = enabled;
            }
        }

        public static void SetUIRaycasterEnabled(Camera camera, bool enabled)
        {
            GraphicRaycaster com = GetUIRaycaster(camera);
            if (com.enabled != enabled)
            {
                com.enabled = enabled;
            }
        }
        #endregion

        #region Execute
        public static void ExecuteClickEvent(GameObject gameObject)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(gameObject, eventData, ExecuteEvents.pointerClickHandler);
        }

        public static void PenetrateExecuteClick<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function, bool isPenetrateOnce) where T : IEventSystemHandler
        {
            List<RaycastResult> results = new List<RaycastResult>();
            GameObject current = data.pointerCurrentRaycast.gameObject;
            EventSystem.current.RaycastAll(data, results);
            for (int i = 0; i < results.Count; i++)
            {
                GameObject goItem = results[i].gameObject;
                if (current != goItem)
                {
                    PointerEventData eventData = new PointerEventData(EventSystem.current);
                    eventData.rawPointerPress = goItem;
                    eventData.pointerPress = goItem;
                    ExecuteEvents.Execute(goItem, eventData, function);

                    //RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
                    if (isPenetrateOnce) break;
                }
            }
        }

        public static void PenetrateExecuteDrag<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function, bool isPenetrateOnce) where T : IEventSystemHandler
        {
            List<RaycastResult> results = new List<RaycastResult>();
            GameObject current = data.pointerCurrentRaycast.gameObject;
            EventSystem.current.RaycastAll(data, results);
            for (int i = 0; i < results.Count; i++)
            {
                GameObject goItem = results[i].gameObject;
                if (current != goItem)
                {
                    PointerEventData eventData = new PointerEventData(EventSystem.current);
                    eventData.rawPointerPress = goItem;
                    eventData.pointerDrag = goItem;
                    ExecuteEvents.Execute(goItem, eventData, function);

                    //RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
                    if (isPenetrateOnce) break;
                }
            }
        }
        #endregion

        #region RaycastResult
        public static List<RaycastResult> GetPosRaycastResults(Vector2 screenPosition)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = new Vector2(screenPosition.x, screenPosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results;
        }

        public static bool HasRaycastResult()
        {
            List<RaycastResult> resList = null;
#if UNITY_STANDALONE || UNITY_EDITOR
            resList = GetPosRaycastResults(Input.mousePosition);
#else
            resList = GetPosRaycastResults(Input.GetTouch(0).position);
#endif
            return resList.Count == 0;
        }

        public static GameObject GetTopRaycastResult()
        {
            List<RaycastResult> resList = null;
#if UNITY_STANDALONE || UNITY_EDITOR
            resList = GetPosRaycastResults(Input.mousePosition);
#else
            resList = GetPosRaycastResults(Input.GetTouch(0).position);
#endif
            if (resList.Count > 0)
            {
                return resList[0].gameObject;
            }
            return null;
        }

        public static bool IsUguiCanClick(Graphic graphic, Camera camera, GraphicRaycaster graphicRaycaster)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = camera.WorldToScreenPoint(graphic.transform.position);
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, results);
            return results.Count > 0 && results[0].gameObject == graphic.gameObject;
        }
#endregion

        #region Pointer
        public static bool IsPointerOnEventObj()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            return EventSystem.current.IsPointerOverGameObject();
#else
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
#endif  
        }
        #endregion

        #region UGUI Event
        public static void TextRebuildRequested(Text text)
        {
            //text.OnRebuildRequested();
        }
        #endregion
    }
}