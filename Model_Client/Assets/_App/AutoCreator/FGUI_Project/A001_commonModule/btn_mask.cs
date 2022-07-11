/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.A001_commonModule
{
    public partial class btn_mask : GButton
    {
        public GGraph img_bg;
        public const string URL = "ui://9gpz896k8igg1";

        public static btn_mask CreateInstance()
        {
            return (btn_mask)UIPackage.CreateObject("A001_commonModule", "btn_mask");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            img_bg = (GGraph)GetChildAt(0);
        }
    }
}