/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G101_propWindow
{
    public partial class com_propWindow : GComponent
    {
        public Controller cont_prop;
        public GImage bg;
        public GTextField text_title;
        public GLoader load_prop;
        public GTextField text_tips0;
        public GButton btn_close;
        public GButton btn_video;
        public const string URL = "ui://fmr5mo8pg5fl1k";

        public static com_propWindow CreateInstance()
        {
            return (com_propWindow)UIPackage.CreateObject("G101_propWindow", "com_propWindow");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_prop = GetControllerAt(0);
            bg = (GImage)GetChildAt(0);
            text_title = (GTextField)GetChildAt(1);
            load_prop = (GLoader)GetChildAt(2);
            text_tips0 = (GTextField)GetChildAt(3);
            btn_close = (GButton)GetChildAt(4);
            btn_video = (GButton)GetChildAt(5);
        }
    }
}