/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G006_gameSetting
{
    public partial class com_gameSetting : GComponent
    {
        public GImage bg;
        public GTextField text_title;
        public GButton btn_close;
        public GButton btn_MmTools;
        public btn_sound btn_sound;
        public btn_vibration btn_vibration;
        public GButton btn_restart;
        public GButton btn_help;
        public btn_language btn_language;
        public const string URL = "ui://sirjnrkkqj6m0";

        public static com_gameSetting CreateInstance()
        {
            return (com_gameSetting)UIPackage.CreateObject("G006_gameSetting", "com_gameSetting");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg = (GImage)GetChildAt(0);
            text_title = (GTextField)GetChildAt(1);
            btn_close = (GButton)GetChildAt(2);
            btn_MmTools = (GButton)GetChildAt(3);
            btn_sound = (btn_sound)GetChildAt(4);
            btn_vibration = (btn_vibration)GetChildAt(5);
            btn_restart = (GButton)GetChildAt(6);
            btn_help = (GButton)GetChildAt(7);
            btn_language = (btn_language)GetChildAt(8);
        }
    }
}