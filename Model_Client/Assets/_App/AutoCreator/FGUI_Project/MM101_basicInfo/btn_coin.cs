/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM101_basicInfo
{
    public partial class btn_coin : GLabel
    {
        public Controller button;
        public const string URL = "ui://94fbq9npv10219";

        public static btn_coin CreateInstance()
        {
            return (btn_coin)UIPackage.CreateObject("MM101_basicInfo", "btn_coin");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
        }
    }
}