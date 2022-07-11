/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM507_commonMask
{
    public partial class btn_mask : GButton
    {
        public GGraph gg_bg;
        public const string URL = "ui://60h61hq2uq870";

        public static btn_mask CreateInstance()
        {
            return (btn_mask)UIPackage.CreateObject("MM507_commonMask", "btn_mask");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            gg_bg = (GGraph)GetChildAt(0);
        }
    }
}