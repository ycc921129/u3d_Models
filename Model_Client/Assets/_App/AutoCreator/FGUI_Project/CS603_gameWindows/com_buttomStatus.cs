/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS603_gameWindows
{
    public partial class com_buttomStatus : GComponent
    {
        public Controller cont_showStatus;
        public GButton btn_cancel;
        public GButton btn_update;
        public const string URL = "ui://06ybtwdrfd20c";

        public static com_buttomStatus CreateInstance()
        {
            return (com_buttomStatus)UIPackage.CreateObject("CS603_gameWindows", "com_buttomStatus");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_showStatus = GetControllerAt(0);
            btn_cancel = (GButton)GetChildAt(1);
            btn_update = (GButton)GetChildAt(2);
        }
    }
}