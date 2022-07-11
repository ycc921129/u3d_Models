/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_redeemConfirm : GComponent
    {
        public GImage frame;
        public GButton btn_close;
        public GComponent com_light;
        public GLoader load_reward;
        public GTextField text_tips0;
        public GGraph pos;
        public GTextField text_value;
        public GTextField text_account;
        public btn_next btn_confirm;
        public GButton btn_change;
        public const string URL = "ui://rl7u9y2lmciw3y";

        public static com_redeemConfirm CreateInstance()
        {
            return (com_redeemConfirm)UIPackage.CreateObject("G017_gameRedeem", "com_redeemConfirm");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            frame = (GImage)GetChildAt(0);
            btn_close = (GButton)GetChildAt(1);
            com_light = (GComponent)GetChildAt(2);
            load_reward = (GLoader)GetChildAt(3);
            text_tips0 = (GTextField)GetChildAt(4);
            pos = (GGraph)GetChildAt(5);
            text_value = (GTextField)GetChildAt(6);
            text_account = (GTextField)GetChildAt(8);
            btn_confirm = (btn_next)GetChildAt(9);
            btn_change = (GButton)GetChildAt(10);
        }
    }
}