using UnityEngine;
using UnityEngine.UI;

namespace Frame
{
    public static class GraphicExtend
    {
        public static void SetColorAlpha(this Graphic graphic, float a)
        {
            Color tmp = graphic.color;
            tmp.a = a;
            graphic.color = tmp;
        }
    }
}