/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;

namespace FutureCore
{
    public class DragPenetrater : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("是否事件只渗透一次")]
        public bool isPenetrateOnce = true;

        public static DragPenetrater Add(GameObject go)
        {
            DragPenetrater penetrater = go.GetComponent<DragPenetrater>();
            if (penetrater == null)
                penetrater = go.AddComponent<DragPenetrater>();
            return penetrater;
        }

        public static DragPenetrater Get(Component component)
        {
            return Add(component.gameObject);
        }

        public static void Remove(GameObject go)
        {
            DragPenetrater penetrater = go.GetComponent<DragPenetrater>();
            if (penetrater != null)
                Destroy(penetrater);
        }

        public static void Remove(Component component)
        {
            Remove(component.gameObject);
        }

        public void OnBeginDrag(PointerEventData data)
        {
            EventUtil.PenetrateExecuteDrag(data, ExecuteEvents.beginDragHandler, isPenetrateOnce);
        }

        public void OnDrag(PointerEventData data)
        {
            EventUtil.PenetrateExecuteDrag(data, ExecuteEvents.dragHandler, isPenetrateOnce);
        }

        public void OnEndDrag(PointerEventData data)
        {
            EventUtil.PenetrateExecuteDrag(data, ExecuteEvents.endDragHandler, isPenetrateOnce);
        }
    }
}