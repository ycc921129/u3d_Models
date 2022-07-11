/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM304_commonWindow
{
    public partial class com_commonWindow : GComponent
    {
        public Controller cont_layout;
        public GTextField text_title;
        public GTextField text_content;
        public GButton btn_positiveLarge;
        public GButton btn_negative;
        public GButton btn_positiveSmall;
        public GButton btn_close;
        public const string URL = "ui://zrdho07jotfn34";

        public static com_commonWindow CreateInstance()
        {
            return (com_commonWindow)UIPackage.CreateObject("MM304_commonWindow", "com_commonWindow");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_layout = GetControllerAt(0);
            text_title = (GTextField)GetChildAt(1);
            text_content = (GTextField)GetChildAt(2);
            btn_positiveLarge = (GButton)GetChildAt(3);
            btn_negative = (GButton)GetChildAt(4);
            btn_positiveSmall = (GButton)GetChildAt(5);
            btn_close = (GButton)GetChildAt(7);
        }
    }
}