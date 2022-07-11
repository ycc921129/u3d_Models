/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM9_mmTool
{
    public partial class com_mmTools : GComponent
    {
        public GImage bg;
        public btn_sound btn_adSetting;
        public btn_sound btn_open_exchange;
        public GButton btn_addGold1;
        public GButton btn_addGold2;
        public GButton btn_addLevel;
        public GButton btn_addVideo;
        public btn_language btn_language;
        public GButton btn_close;
        public const string URL = "ui://v5jd19r6fi6i1";

        public static com_mmTools CreateInstance()
        {
            return (com_mmTools)UIPackage.CreateObject("MM9_mmTool", "com_mmTools");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg = (GImage)GetChildAt(0);
            btn_adSetting = (btn_sound)GetChildAt(2);
            btn_open_exchange = (btn_sound)GetChildAt(3);
            btn_addGold1 = (GButton)GetChildAt(4);
            btn_addGold2 = (GButton)GetChildAt(5);
            btn_addLevel = (GButton)GetChildAt(6);
            btn_addVideo = (GButton)GetChildAt(7);
            btn_language = (btn_language)GetChildAt(8);
            btn_close = (GButton)GetChildAt(9);
        }
    }
}