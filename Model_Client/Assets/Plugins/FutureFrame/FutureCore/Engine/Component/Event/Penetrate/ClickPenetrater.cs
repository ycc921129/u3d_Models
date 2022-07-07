/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;

namespace FutureCore
{
    public class ClickPenetrater : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [Header("是否事件只渗透一次")]
        public bool isPenetrateOnce = true;

        public static ClickPenetrater Add(GameObject go)
        {
            ClickPenetrater penetrater = go.GetComponent<ClickPenetrater>();
            if (penetrater == null)
                penetrater = go.AddComponent<ClickPenetrater>();
            return penetrater;
        }

        public static ClickPenetrater Add(Component component)
        {
            return Add(component.gameObject);
        }

        public static void Remove(GameObject go)
        {
            ClickPenetrater penetrater = go.GetComponent<ClickPenetrater>();
            if (penetrater != null)
                Destroy(penetrater);
        }

        public static void Remove(Component component)
        {
            Remove(component.gameObject);
        }

        public void OnPointerDown(PointerEventData data)
        {
            EventUtil.PenetrateExecuteClick(data, ExecuteEvents.pointerDownHandler, isPenetrateOnce);
        }
        public void OnPointerUp(PointerEventData data)
        {
            EventUtil.PenetrateExecuteClick(data, ExecuteEvents.pointerUpHandler, isPenetrateOnce);
        }
        public void OnPointerClick(PointerEventData data)
        {
            EventUtil.PenetrateExecuteClick(data, ExecuteEvents.pointerClickHandler, isPenetrateOnce);
        }
    }
}