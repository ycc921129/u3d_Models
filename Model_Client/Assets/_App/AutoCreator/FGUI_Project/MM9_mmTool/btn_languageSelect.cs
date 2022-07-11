/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM9_mmTool
{
    public partial class btn_languageSelect : GButton
    {
        public GTextField txt;
        public const string URL = "ui://v5jd19r68d5zo";

        public static btn_languageSelect CreateInstance()
        {
            return (btn_languageSelect)UIPackage.CreateObject("MM9_mmTool", "btn_languageSelect");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            txt = (GTextField)GetChildAt(1);
        }
    }
}