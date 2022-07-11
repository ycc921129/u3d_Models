/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM101_basicInfo
{
    public partial class com_skinCurrency : GComponent
    {
        public btn_coin btn_coin;
        public GImage img_icon;
        public GTextField text_skinValue;
        public const string URL = "ui://94fbq9npdr4u1y";

        public static com_skinCurrency CreateInstance()
        {
            return (com_skinCurrency)UIPackage.CreateObject("MM101_basicInfo", "com_skinCurrency");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            btn_coin = (btn_coin)GetChildAt(0);
            img_icon = (GImage)GetChildAt(1);
            text_skinValue = (GTextField)GetChildAt(2);
        }
    }
}