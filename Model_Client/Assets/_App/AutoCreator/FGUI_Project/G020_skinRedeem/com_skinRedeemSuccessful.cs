/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G020_skinRedeem
{
    public partial class com_skinRedeemSuccessful : GComponent
    {
        public GImage frame;
        public GTextField text_title;
        public GLoader load_skin;
        public GButton btn_close;
        public GTextField text_tips1;
        public Transition light_size;
        public Transition light_transparency;
        public const string URL = "ui://a1jbnm9hmciw3x";

        public static com_skinRedeemSuccessful CreateInstance()
        {
            return (com_skinRedeemSuccessful)UIPackage.CreateObject("G020_skinRedeem", "com_skinRedeemSuccessful");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            frame = (GImage)GetChildAt(0);
            text_title = (GTextField)GetChildAt(2);
            load_skin = (GLoader)GetChildAt(5);
            btn_close = (GButton)GetChildAt(6);
            text_tips1 = (GTextField)GetChildAt(7);
            light_size = GetTransitionAt(0);
            light_transparency = GetTransitionAt(1);
        }
    }
}