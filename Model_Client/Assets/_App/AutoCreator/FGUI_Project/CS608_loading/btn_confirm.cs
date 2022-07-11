/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS608_loading
{
    public partial class btn_confirm : GButton
    {
        public Controller cont_color;
        public const string URL = "ui://9euvtldhlh5y3j";

        public static btn_confirm CreateInstance()
        {
            return (btn_confirm)UIPackage.CreateObject("CS608_loading", "btn_confirm");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_color = GetControllerAt(0);
        }
    }
}