/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS603_gameWindows
{
    public partial class com_login : GComponent
    {
        public GComponent btn_close;
        public GButton btn_facebookLogin;
        public GButton btn_googleLogin;
        public const string URL = "ui://06ybtwdrunhvf";

        public static com_login CreateInstance()
        {
            return (com_login)UIPackage.CreateObject("CS603_gameWindows", "com_login");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            btn_close = (GComponent)GetChildAt(4);
            btn_facebookLogin = (GButton)GetChildAt(5);
            btn_googleLogin = (GButton)GetChildAt(6);
        }
    }
}