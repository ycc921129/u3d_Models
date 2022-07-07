/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;

namespace FutureCore
{
    public class DragConnector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("关联事件的对象")]
        public DragConnector listener;

        public static DragConnector Get(GameObject go)
        {
            DragConnector connector = go.GetComponent<DragConnector>();
            if (connector == null)
                connector = go.AddComponent<DragConnector>();
            return connector;
        }

        public static DragConnector Get(Component component)
        {
            return Get(component.gameObject);
        }

        public void OnBeginDrag(PointerEventData data)
        {
            if (listener != null)
                listener.OnBeginDrag(data);
        }
        public void OnDrag(PointerEventData data)
        {
            if (listener != null)
                listener.OnDrag(data);
        }
        public void OnEndDrag(PointerEventData data)
        {
            if (listener != null)
                listener.OnEndDrag(data);
        }
    }
}