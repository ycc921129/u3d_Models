/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.A001_commonModule
{
    public partial class btn_large : GButton
    {
        public Controller cont_icon;
        public Controller cont_style;
        public const string URL = "ui://9gpz896kqgal3b";

        public static btn_large CreateInstance()
        {
            return (btn_large)UIPackage.CreateObject("A001_commonModule", "btn_large");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_icon = GetControllerAt(0);
            cont_style = GetControllerAt(1);
        }
    }
}