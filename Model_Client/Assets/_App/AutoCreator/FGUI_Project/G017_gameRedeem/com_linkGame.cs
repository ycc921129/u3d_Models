/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_linkGame : GComponent
    {
        public GImage img_title;
        public GTextField text_title;
        public GLoader load_gameIcon;
        public GTextField text_gameName;
        public GTextField text_tips0;
        public btn_next btn_bind;
        public GButton btn_later;
        public const string URL = "ui://rl7u9y2lmciw3z";

        public static com_linkGame CreateInstance()
        {
            return (com_linkGame)UIPackage.CreateObject("G017_gameRedeem", "com_linkGame");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            img_title = (GImage)GetChildAt(1);
            text_title = (GTextField)GetChildAt(2);
            load_gameIcon = (GLoader)GetChildAt(3);
            text_gameName = (GTextField)GetChildAt(4);
            text_tips0 = (GTextField)GetChildAt(5);
            btn_bind = (btn_next)GetChildAt(6);
            btn_later = (GButton)GetChildAt(7);
        }
    }
}