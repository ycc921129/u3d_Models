/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G006_gameSetting
{
    public partial class btn_language : GComponent
    {
        public Controller cont_language;
        public GTextField txt;
        public GButton btn;
        public com_language com;
        public const string URL = "ui://sirjnrkkqj6mi";

        public static btn_language CreateInstance()
        {
            return (btn_language)UIPackage.CreateObject("G006_gameSetting", "btn_language");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_language = GetControllerAt(0);
            txt = (GTextField)GetChildAt(1);
            btn = (GButton)GetChildAt(2);
            com = (com_language)GetChildAt(3);
        }
    }
}