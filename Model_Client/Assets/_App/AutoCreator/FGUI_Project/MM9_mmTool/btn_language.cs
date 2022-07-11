/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM9_mmTool
{
    public partial class btn_language : GComponent
    {
        public Controller cont_language;
        public GTextField txt;
        public GButton btn;
        public com_language com;
        public const string URL = "ui://v5jd19r68d5zh";

        public static btn_language CreateInstance()
        {
            return (btn_language)UIPackage.CreateObject("MM9_mmTool", "btn_language");
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