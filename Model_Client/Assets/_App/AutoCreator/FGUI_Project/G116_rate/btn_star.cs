/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G116_rate
{
    public partial class btn_star : GComponent
    {
        public Controller cont_starStatus;
        public GImage icon_star;
        public const string URL = "ui://7xruccmafntl2";

        public static btn_star CreateInstance()
        {
            return (btn_star)UIPackage.CreateObject("G116_rate", "btn_star");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_starStatus = GetControllerAt(0);
            icon_star = (GImage)GetChildAt(0);
        }
    }
}