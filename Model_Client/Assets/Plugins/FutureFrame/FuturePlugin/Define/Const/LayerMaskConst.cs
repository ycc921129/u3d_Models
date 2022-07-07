/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FuturePlugin
{
    public static class LayerMaskConst
    {
        public const string Everything_Name = "Everything";
        public const string Default_Name = "Default";
        public const string UI_Name = "UI";

        public readonly static int Everything = LayerMask.NameToLayer(Everything_Name);
        public readonly static int Default = LayerMask.NameToLayer(Default_Name);
        public readonly static int UI = LayerMask.NameToLayer(UI_Name);
    }
}