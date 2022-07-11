/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM101_basicInfo
{
    public partial class com_diamond : GComponent
    {
        public btn_coin btn_coin;
        public GLoader load_icon;
        public GTextField text_coin;
        public const string URL = "ui://94fbq9npmdwa1x";

        public static com_diamond CreateInstance()
        {
            return (com_diamond)UIPackage.CreateObject("MM101_basicInfo", "com_diamond");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            btn_coin = (btn_coin)GetChildAt(0);
            load_icon = (GLoader)GetChildAt(1);
            text_coin = (GTextField)GetChildAt(2);
        }
    }
}