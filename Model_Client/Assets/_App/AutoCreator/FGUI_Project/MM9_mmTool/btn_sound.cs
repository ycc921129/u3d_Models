/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM9_mmTool
{
    public partial class btn_sound : GButton
    {
        public Controller cont_switch;
        public GTextField on;
        public GTextField off;
        public const string URL = "ui://v5jd19r6fi6i2";

        public static btn_sound CreateInstance()
        {
            return (btn_sound)UIPackage.CreateObject("MM9_mmTool", "btn_sound");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_switch = GetControllerAt(1);
            on = (GTextField)GetChildAt(2);
            off = (GTextField)GetChildAt(3);
        }
    }
}