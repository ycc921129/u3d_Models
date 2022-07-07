/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;

namespace FutureCore
{
    public class EventPenetrater : EventTrigger
    {
        [Header("是否事件只渗透一次")]
        public bool isPenetrateOnce = true;

        public static EventPenetrater Get(GameObject go)
        {
            EventPenetrater penetrater = go.GetComponent<EventPenetrater>();
            if (penetrater == null)
                penetrater = go.AddComponent<EventPenetrater>();
            return penetrater;
        }

        public static EventPenetrater Get(Component component)
        {
            return Get(component.gameObject);
        }

        public static void Remove(GameObject go)
        {
            EventPenetrater penetrater = go.GetComponent<EventPenetrater>();
            if (penetrater != null)
                Destroy(penetrater);
        }

        public static void Remove(Component component)
        {
            Remove(component.gameObject);
        }

        public override void OnPointerDown(PointerEventData data)
        {
            EventUtil.PenetrateExecuteClick(data, ExecuteEvents.pointerDownHandler, isPenetrateOnce);
        }
        public override void OnPointerUp(PointerEventData data)
        {
            EventUtil.PenetrateExecuteClick(data, ExecuteEvents.pointerUpHandler, isPenetrateOnce);
        }
        public override void OnPointerClick(PointerEventData data)
        {
            EventUtil.PenetrateExecuteClick(data, ExecuteEvents.pointerClickHandler, isPenetrateOnce);
        }

        public override void OnBeginDrag(PointerEventData data)
        {
            EventUtil.PenetrateExecuteDrag(data, ExecuteEvents.beginDragHandler, isPenetrateOnce);
        }
        public override void OnDrag(PointerEventData data)
        {
            EventUtil.PenetrateExecuteDrag(data, ExecuteEvents.dragHandler, isPenetrateOnce);
        }
        public override void OnEndDrag(PointerEventData data)
        {
            EventUtil.PenetrateExecuteDrag(data, ExecuteEvents.endDragHandler, isPenetrateOnce);
        }

        public override void OnScroll(PointerEventData data)
        {
            EventUtil.PenetrateExecuteDrag(data, ExecuteEvents.scrollHandler, isPenetrateOnce);
        }
    }
}