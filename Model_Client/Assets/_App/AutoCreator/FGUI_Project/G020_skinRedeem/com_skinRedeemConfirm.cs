/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G020_skinRedeem
{
    public partial class com_skinRedeemConfirm : GComponent
    {
        public GImage frame;
        public GButton btn_close;
        public GLoader load_skin;
        public GImage img_account;
        public GTextField text_account;
        public GTextField text_tips0;
        public GButton btn_confirm;
        public GButton btn_change;
        public Transition light_size;
        public Transition light_transparency;
        public const string URL = "ui://a1jbnm9hmciw3w";

        public static com_skinRedeemConfirm CreateInstance()
        {
            return (com_skinRedeemConfirm)UIPackage.CreateObject("G020_skinRedeem", "com_skinRedeemConfirm");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            frame = (GImage)GetChildAt(0);
            btn_close = (GButton)GetChildAt(1);
            load_skin = (GLoader)GetChildAt(4);
            img_account = (GImage)GetChildAt(5);
            text_account = (GTextField)GetChildAt(6);
            text_tips0 = (GTextField)GetChildAt(7);
            btn_confirm = (GButton)GetChildAt(8);
            btn_change = (GButton)GetChildAt(9);
            light_size = GetTransitionAt(0);
            light_transparency = GetTransitionAt(1);
        }
    }
}