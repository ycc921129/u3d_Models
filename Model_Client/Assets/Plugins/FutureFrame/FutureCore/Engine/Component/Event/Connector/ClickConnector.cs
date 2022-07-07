/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;

namespace FutureCore
{
    public class ClickConnector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [Header("关联事件的对象")]
        public ClickListener listener;

        public static ClickConnector Get(GameObject go)
        {
            ClickConnector connector = go.GetComponent<ClickConnector>();
            if (connector == null)
                connector = go.AddComponent<ClickConnector>();
            return connector;
        }

        public static ClickConnector Get(Component component)
        {
            return Get(component.gameObject);
        }

        public void OnPointerEnter(PointerEventData data)
        {
            if (listener != null)
                listener.OnPointerEnter(data);
        }
        public void OnPointerExit(PointerEventData data)
        {
            if (listener != null)
                listener.OnPointerExit(data);
        }

        public void OnPointerDown(PointerEventData data)
        {
            if (listener != null)
                listener.OnPointerDown(data);
        }
        public void OnPointerUp(PointerEventData data)
        {
            if (listener != null)
                listener.OnPointerUp(data);
        }
        public void OnPointerClick(PointerEventData data)
        {
            if (listener != null)
                listener.OnPointerClick(data);
        }
    }
}