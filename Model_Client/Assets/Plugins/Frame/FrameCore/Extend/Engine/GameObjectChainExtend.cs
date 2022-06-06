using UnityEngine;

namespace Frame
{
    public static class GameObjectChainExtend
    {
        public static GameObject Show(this GameObject thisObj)
        {
            thisObj.SetActive(true);
            return thisObj;
        }

        public static GameObject Hide(this GameObject thisObj)
        {
            thisObj.SetActive(false);
            return thisObj;
        }

        public static GameObject Name(this GameObject thisObj, string name)
        {
            thisObj.name = name;
            return thisObj;
        }

        public static GameObject Layer(this GameObject thisObj, string layerName)
        {
            Transform[] transArr = thisObj.GetComponentsInChildren<Transform>();
            for (int i = 0; i < transArr.Length; i++)
            {
                transArr[i].gameObject.layer = LayerMask.NameToLayer(layerName);
            }
            return thisObj;
        }

        public static GameObject SortingLayerName(this GameObject thisObj, string sortingLayerName)
        {
            Renderer[] renders = thisObj.GetComponentsInChildren<Renderer>();
            foreach (Renderer render in renders)
            {
                render.sortingLayerName = sortingLayerName;
            }
            return thisObj;
        }

        public static GameObject SortingOrder(this GameObject thisObj, int sortingOrder)
        {
            Renderer[] renders = thisObj.GetComponentsInChildren<Renderer>();
            foreach (Renderer render in renders)
            {
                render.sortingOrder = sortingOrder;
            }
            return thisObj;
        }

        public static GameObject DestroySelf(this GameObject selfObj, float time = 0f)
        {
            Object.Destroy(selfObj, time);
            selfObj = null;
            return selfObj;
        }
    }
}