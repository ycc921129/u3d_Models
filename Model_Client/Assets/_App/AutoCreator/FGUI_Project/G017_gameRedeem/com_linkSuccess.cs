/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_linkSuccess : GComponent
    {
        public GImage img_frame;
        public GButton btn_close;
        public com_iconCircle load_icon;
        public GTextField text_account;
        public GImage img_title;
        public GTextField text_title;
        public GTextField text_tips0;
        public btn_next btn_continue;
        public const string URL = "ui://rl7u9y2lmdwa18";

        public static com_linkSuccess CreateInstance()
        {
            return (com_linkSuccess)UIPackage.CreateObject("G017_gameRedeem", "com_linkSuccess");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            img_frame = (GImage)GetChildAt(0);
            btn_close = (GButton)GetChildAt(1);
            load_icon = (com_iconCircle)GetChildAt(2);
            text_account = (GTextField)GetChildAt(4);
            img_title = (GImage)GetChildAt(5);
            text_title = (GTextField)GetChildAt(6);
            text_tips0 = (GTextField)GetChildAt(7);
            btn_continue = (btn_next)GetChildAt(8);
        }
    }
}