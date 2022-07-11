/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G020_skinRedeem
{
    public partial class btn_skinRedeem : GButton
    {
        public GRichTextField title_value;
        public const string URL = "ui://a1jbnm9he31h3t";

        public static btn_skinRedeem CreateInstance()
        {
            return (btn_skinRedeem)UIPackage.CreateObject("G020_skinRedeem", "btn_skinRedeem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            title_value = (GRichTextField)GetChildAt(1);
        }
    }
}