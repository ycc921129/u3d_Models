/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS608_loading
{
    public partial class btn_choose : GButton
    {
        public Controller cont_select;
        public const string URL = "ui://9euvtldhc6nc3h";

        public static btn_choose CreateInstance()
        {
            return (btn_choose)UIPackage.CreateObject("CS608_loading", "btn_choose");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_select = GetControllerAt(0);
        }
    }
}