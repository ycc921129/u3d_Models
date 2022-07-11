/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_linkState : GComponent
    {
        public Controller cont_state;
        public GImage img_frame;
        public GImage img_title;
        public GTextField text_title;
        public GLoader load_gameIcon;
        public GTextField text_gameName;
        public GTextField text_tips0;
        public btn_next btn_confirm;
        public const string URL = "ui://rl7u9y2lmciw40";

        public static com_linkState CreateInstance()
        {
            return (com_linkState)UIPackage.CreateObject("G017_gameRedeem", "com_linkState");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_state = GetControllerAt(0);
            img_frame = (GImage)GetChildAt(0);
            img_title = (GImage)GetChildAt(1);
            text_title = (GTextField)GetChildAt(2);
            load_gameIcon = (GLoader)GetChildAt(3);
            text_gameName = (GTextField)GetChildAt(4);
            text_tips0 = (GTextField)GetChildAt(5);
            btn_confirm = (btn_next)GetChildAt(6);
        }
    }
}