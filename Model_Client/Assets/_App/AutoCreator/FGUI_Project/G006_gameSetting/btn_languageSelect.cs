/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G006_gameSetting
{
    public partial class btn_languageSelect : GButton
    {
        public GTextField txt;
        public const string URL = "ui://sirjnrkkqj6mp";

        public static btn_languageSelect CreateInstance()
        {
            return (btn_languageSelect)UIPackage.CreateObject("G006_gameSetting", "btn_languageSelect");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            txt = (GTextField)GetChildAt(1);
        }
    }
}