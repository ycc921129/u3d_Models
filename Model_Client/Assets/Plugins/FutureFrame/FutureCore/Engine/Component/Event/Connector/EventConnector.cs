/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;

namespace FutureCore
{
    public class EventConnector : EventTrigger
    {
        [Header("关联事件的对象")]
        public EventConnector listener;

        public static EventConnector Get(GameObject go)
        {
            EventConnector connector = go.GetComponent<EventConnector>();
            if (connector == null)
                connector = go.AddComponent<EventConnector>();
            return connector;
        }

        public static EventConnector Get(Component component)
        {
            return Get(component.gameObject);
        }

        public override void OnPointerEnter(PointerEventData data)
        {
            if (listener != null)
                listener.OnPointerEnter(data);
        }
        public override void OnPointerExit(PointerEventData data)
        {
            if (listener != null)
                listener.OnPointerExit(data);
        }

        public override void OnPointerDown(PointerEventData data)
        {
            if (listener != null)
                listener.OnPointerDown(data);
        }
        public override void OnPointerUp(PointerEventData data)
        {
            if (listener != null)
                listener.OnPointerUp(data);
        }
        public override void OnPointerClick(PointerEventData data)
        {
            if (listener != null)
                listener.OnPointerClick(data);
        }

        public override void OnBeginDrag(PointerEventData data)
        {
            if (listener != null)
                listener.OnBeginDrag(data);
        }
        public override void OnDrag(PointerEventData data)
        {
            if (listener != null)
                listener.OnDrag(data);
        }
        public override void OnEndDrag(PointerEventData data)
        {
            if (listener != null)
                listener.OnEndDrag(data);
        }

        public override void OnScroll(PointerEventData data)
        {
            if (listener != null)
                listener.OnScroll(data);
        }

        public override void OnSubmit(BaseEventData data)
        {
            if (listener != null)
                listener.OnSubmit(data);
        }
        public override void OnSelect(BaseEventData data)
        {
            if (listener != null)
                listener.OnSelect(data);
        }
        public override void OnDeselect(BaseEventData data)
        {
            if (listener != null)
                listener.OnDeselect(data);
        }
        public override void OnUpdateSelected(BaseEventData data)
        {
            if (listener != null)
                listener.OnUpdateSelected(data);
        }
    }
}