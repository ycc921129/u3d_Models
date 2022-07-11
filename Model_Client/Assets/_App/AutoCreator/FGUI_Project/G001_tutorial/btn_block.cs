/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G001_tutorial
{
    public partial class btn_block : GButton
    {
        public Controller cont_c1;
        public GLoader img_bg3;
        public const string URL = "ui://5tzgfbiwm9c22";

        public static btn_block CreateInstance()
        {
            return (btn_block)UIPackage.CreateObject("G001_tutorial", "btn_block");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_c1 = GetControllerAt(1);
            img_bg3 = (GLoader)GetChildAt(0);
        }
    }
}