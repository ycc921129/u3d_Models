/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G001_tutorial
{
    public partial class btn_nevershow : GButton
    {
        public Controller cont_select;
        public const string URL = "ui://5tzgfbiwm9c27";

        public static btn_nevershow CreateInstance()
        {
            return (btn_nevershow)UIPackage.CreateObject("G001_tutorial", "btn_nevershow");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_select = GetControllerAt(0);
        }
    }
}