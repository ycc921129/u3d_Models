/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class btn_next : GButton
    {
        public Controller cont_click;
        public const string URL = "ui://rl7u9y2lmdwa8";

        public static btn_next CreateInstance()
        {
            return (btn_next)UIPackage.CreateObject("G017_gameRedeem", "btn_next");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_click = GetControllerAt(0);
        }
    }
}