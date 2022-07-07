using FutureCore;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldEventExample : MonoBehaviour
{
    public new Camera camera;
    public GameObject[] targetObjs;
    private float targetScreenZ;

    private void Awake()
    {
        //LogUtil.Log(1 >> 5);
        //LogUtil.Log(1 << 5);

        //EventUtil.Get2DRaycaster(camera);
        //foreach (GameObject item in targetObjs)
        //{
        //    ClickListener.GetForm2D(item).onClick = OnClick;
        //    ClickListener.GetForm2D(item).onPointerEnter = OnPointerEnter;
        //    ClickListener.GetForm2D(item).onPointerExit = onPointerExit;
        //    DragListener.GetForm2D(item).onBeginDrag = OnBeginDrag;
        //    DragListener.GetForm2D(item).onDrag = OnDrag;
        //}
    }

    private void OnClick(PointerEventData data)
    {
        Debug.Log("点击物体 " + data.pointerPress.name);
    }
    private void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("进入物体 " + data.pointerEnter.name);
    }
    private void onPointerExit(PointerEventData data)
    {
        Debug.Log("离开物体 " + data.pointerEnter.name);
    }

    private void OnBeginDrag(PointerEventData data)
    {
        ClickPenetrater.Add(data.pointerDrag);
    }

    private void OnDrag(PointerEventData eventData)
    {
        // 以鼠标位置为中心移动
        //float targetScreensZ = camera.WorldToScreenPoint(eventData.pointerDrag.transform.position).z;
        //eventData.pointerDrag.transform.position = camera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, targetScreensZ));

        // 以移动间距移动
        Vector3 itemPos = camera.WorldToScreenPoint(eventData.pointerDrag.transform.position);
        Vector2 delta = eventData.delta;
        eventData.pointerDrag.transform.position = camera.ScreenToWorldPoint(new Vector3(itemPos.x + delta.x, itemPos.y + delta.y, itemPos.z));
    }
}