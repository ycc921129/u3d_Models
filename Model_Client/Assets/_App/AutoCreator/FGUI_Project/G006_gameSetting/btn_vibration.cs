/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G006_gameSetting
{
    public partial class btn_vibration : GButton
    {
        public Controller cont_switch;
        public GImage text_switch;
        public GLoader load_switch;
        public const string URL = "ui://sirjnrkku4s6t";

        public static btn_vibration CreateInstance()
        {
            return (btn_vibration)UIPackage.CreateObject("G006_gameSetting", "btn_vibration");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_switch = GetControllerAt(0);
            text_switch = (GImage)GetChildAt(2);
            load_switch = (GLoader)GetChildAt(4);
        }
    }
}