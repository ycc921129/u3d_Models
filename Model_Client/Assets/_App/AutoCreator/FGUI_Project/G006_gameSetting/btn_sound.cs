/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G006_gameSetting
{
    public partial class btn_sound : GButton
    {
        public Controller cont_switch;
        public GLoader load_switch;
        public const string URL = "ui://sirjnrkkqj6m8";

        public static btn_sound CreateInstance()
        {
            return (btn_sound)UIPackage.CreateObject("G006_gameSetting", "btn_sound");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_switch = GetControllerAt(0);
            load_switch = (GLoader)GetChildAt(4);
        }
    }
}