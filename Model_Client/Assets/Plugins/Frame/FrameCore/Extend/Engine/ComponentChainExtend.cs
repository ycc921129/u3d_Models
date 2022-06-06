using UnityEngine;

namespace Frame
{
    public static class ComponentChainExtend
    {
        public static T Position<T>(this T thisComponent, Vector3 position) where T : Component
        {
            thisComponent.transform.position = position;
            return thisComponent;
        }

        public static T LocalPosition<T>(this T thisComponent, Vector3 position) where T : Component
        {
            thisComponent.transform.localPosition = position;
            return thisComponent;
        }

        public static T LocalScale<T>(this T thisComponent, Vector3 scale) where T : Component
        {
            thisComponent.transform.localScale = scale;
            return thisComponent;
        }

        public static T LocalScale<T>(this T thisComponent, float xyz) where T : Component
        {
            thisComponent.transform.localScale = Vector3.one * xyz;
            return thisComponent;
        }

        public static T Rotation<T>(this T thisComponent, Quaternion rotation) where T : Component
        {
            thisComponent.transform.rotation = rotation;
            return thisComponent;
        }

        public static T LocalRotation<T>(this T thisComponent, Quaternion localRotation) where T : Component
        {
            thisComponent.transform.localRotation = localRotation;
            return thisComponent;
        }

        public static T DOSetPositionAndRotation<T>(this T thisComponent, Vector3 position, Quaternion rotation) where T : Component
        {
            thisComponent.transform.SetPositionAndRotation(position, rotation);
            return thisComponent;
        }

        public static T DOSetParent<T>(this T thisComponent, Transform parent, bool worldPositionStays = false) where T : Component
        {
            thisComponent.transform.SetParent(parent, worldPositionStays);
            return thisComponent;
        }

        public static T DOSetAsFirstSibling<T>(this T thisComponent) where T : Component
        {
            thisComponent.transform.SetAsFirstSibling();
            return thisComponent;
        }

        public static T DOSetAsLastSibling<T>(this T thisComponent) where T : Component
        {
            thisComponent.transform.SetAsLastSibling();
            return thisComponent;
        }

        public static T DOSetSiblingIndex<T>(this T thisComponent, int index) where T : Component
        {
            thisComponent.transform.SetSiblingIndex(index);
            return thisComponent;
        }
    }
}