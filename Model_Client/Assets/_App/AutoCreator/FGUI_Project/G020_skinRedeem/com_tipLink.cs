/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G020_skinRedeem
{
    public partial class com_tipLink : GComponent
    {
        public GTextField text_title;
        public GButton btn_close;
        public GTextField text_tips0;
        public GTextField text_tips1;
        public GButton btn_confirm;
        public const string URL = "ui://a1jbnm9hmdwa1g";

        public static com_tipLink CreateInstance()
        {
            return (com_tipLink)UIPackage.CreateObject("G020_skinRedeem", "com_tipLink");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_title = (GTextField)GetChildAt(1);
            btn_close = (GButton)GetChildAt(2);
            text_tips0 = (GTextField)GetChildAt(3);
            text_tips1 = (GTextField)GetChildAt(4);
            btn_confirm = (GButton)GetChildAt(5);
        }
    }
}