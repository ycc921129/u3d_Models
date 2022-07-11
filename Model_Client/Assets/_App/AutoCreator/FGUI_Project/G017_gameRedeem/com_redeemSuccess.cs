/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_redeemSuccess : GComponent
    {
        public GComponent com_light;
        public GLoader load_reward;
        public GTextField text_title;
        public GButton btn_close;
        public GTextField text_tips0;
        public GGraph pos;
        public GTextField text_value;
        public btn_next btn_next;
        public const string URL = "ui://rl7u9y2lmdwa1f";

        public static com_redeemSuccess CreateInstance()
        {
            return (com_redeemSuccess)UIPackage.CreateObject("G017_gameRedeem", "com_redeemSuccess");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            com_light = (GComponent)GetChildAt(1);
            load_reward = (GLoader)GetChildAt(2);
            text_title = (GTextField)GetChildAt(4);
            btn_close = (GButton)GetChildAt(5);
            text_tips0 = (GTextField)GetChildAt(6);
            pos = (GGraph)GetChildAt(7);
            text_value = (GTextField)GetChildAt(8);
            btn_next = (btn_next)GetChildAt(9);
        }
    }
}