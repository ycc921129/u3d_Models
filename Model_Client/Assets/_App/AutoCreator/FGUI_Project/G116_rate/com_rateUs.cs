/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G116_rate
{
    public partial class com_rateUs : GComponent
    {
        public GTextField text_title;
        public GTextField text_tips0;
        public GList list_star;
        public GButton btn_close;
        public GImage icon_hand;
        public Transition t0;
        public const string URL = "ui://7xruccmafntl0";

        public static com_rateUs CreateInstance()
        {
            return (com_rateUs)UIPackage.CreateObject("G116_rate", "com_rateUs");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_title = (GTextField)GetChildAt(1);
            text_tips0 = (GTextField)GetChildAt(2);
            list_star = (GList)GetChildAt(3);
            btn_close = (GButton)GetChildAt(4);
            icon_hand = (GImage)GetChildAt(6);
            t0 = GetTransitionAt(0);
        }
    }
}