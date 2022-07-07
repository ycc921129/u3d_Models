/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.3
*/

using UnityEngine;
using UnityEngine.UI;

namespace FutureCore
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