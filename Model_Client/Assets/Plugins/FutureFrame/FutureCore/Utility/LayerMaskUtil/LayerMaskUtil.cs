/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
using UnityEngine.Rendering;

namespace FutureCore
{
    public static class LayerMaskUtil
    {
        public static int Get(string layerName)
        {
            return 1 << LayerMask.NameToLayer(layerName);
        }

        public static int Reverse(string layerName)
        {
            return ~(1 << LayerMask.NameToLayer(layerName));
        }

        public static int Open(int mask, string layerName)
        {
            return mask |= 1 << LayerMask.NameToLayer(layerName);
        }

        public static int Close(int mask, string layerName)
        {
            return mask &= ~(1 << LayerMask.NameToLayer(layerName));
        }

        public static int Toggle(int mask, string layerName)
        {
            return mask ^= 1 << LayerMask.NameToLayer(layerName);
        }

        public static SortingGroup AddSortingGroup(GameObject gameObject)
        {
            return gameObject.AddComponent<SortingGroup>();
        }
    }
}