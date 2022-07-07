/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS603_gameWindows
{
    public partial class com_btnStatus : GComponent
    {
        public Controller cont_status;
        public GButton btn_retry;
        public GButton btn_cancel;
        public const string URL = "ui://06ybtwdrum1vd";

        public static com_btnStatus CreateInstance()
        {
            return (com_btnStatus)UIPackage.CreateObject("CS603_gameWindows", "com_btnStatus");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_status = GetControllerAt(0);
            btn_retry = (GButton)GetChildAt(0);
            btn_cancel = (GButton)GetChildAt(1);
        }
    }
}