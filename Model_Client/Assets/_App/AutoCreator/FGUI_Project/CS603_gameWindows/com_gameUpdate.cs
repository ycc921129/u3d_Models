/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS603_gameWindows
{
    public partial class com_gameUpdate : GComponent
    {
        public GTextField text_title;
        public GTextField text_tips0;
        public com_buttomStatus com_buttomStatus;
        public const string URL = "ui://06ybtwdrwdfo0";

        public static com_gameUpdate CreateInstance()
        {
            return (com_gameUpdate)UIPackage.CreateObject("CS603_gameWindows", "com_gameUpdate");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_title = (GTextField)GetChildAt(1);
            text_tips0 = (GTextField)GetChildAt(2);
            com_buttomStatus = (com_buttomStatus)GetChildAt(4);
        }
    }
}