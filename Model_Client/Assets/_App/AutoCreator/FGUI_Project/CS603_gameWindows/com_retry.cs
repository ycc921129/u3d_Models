/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS603_gameWindows
{
    public partial class com_retry : GComponent
    {
        public GTextField text_title;
        public com_btnStatus com_btnStatus;
        public GTextField text_reconnectStatus;
        public GTextField text_status;
        public GTextField text_time;
        public const string URL = "ui://06ybtwdrwdfo7";

        public static com_retry CreateInstance()
        {
            return (com_retry)UIPackage.CreateObject("CS603_gameWindows", "com_retry");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_title = (GTextField)GetChildAt(1);
            com_btnStatus = (com_btnStatus)GetChildAt(3);
            text_reconnectStatus = (GTextField)GetChildAt(4);
            text_status = (GTextField)GetChildAt(5);
            text_time = (GTextField)GetChildAt(6);
        }
    }
}