/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_changeAccount : GComponent
    {
        public Controller cont_guest;
        public GLoader load_gameFrame;
        public GTextField text_tips0;
        public GTextInput text_accouont;
        public btn_next btn_confirm;
        public GButton btn_guest;
        public GButton btn_close;
        public const string URL = "ui://rl7u9y2lmdwau";

        public static com_changeAccount CreateInstance()
        {
            return (com_changeAccount)UIPackage.CreateObject("G017_gameRedeem", "com_changeAccount");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_guest = GetControllerAt(0);
            load_gameFrame = (GLoader)GetChildAt(1);
            text_tips0 = (GTextField)GetChildAt(2);
            text_accouont = (GTextInput)GetChildAt(4);
            btn_confirm = (btn_next)GetChildAt(5);
            btn_guest = (GButton)GetChildAt(6);
            btn_close = (GButton)GetChildAt(7);
        }
    }
}