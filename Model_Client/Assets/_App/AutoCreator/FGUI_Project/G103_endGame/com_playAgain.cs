/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G103_endGame
{
    public partial class com_playAgain : GComponent
    {
        public GTextField text_title;
        public GImage blockPos;
        public GTextField text_level;
        public GTextField text_biggestLevel;
        public GButton btn_close;
        public GButton btn_playAgain;
        public const string URL = "ui://tiosr750e9kl0";

        public static com_playAgain CreateInstance()
        {
            return (com_playAgain)UIPackage.CreateObject("G103_endGame", "com_playAgain");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_title = (GTextField)GetChildAt(2);
            blockPos = (GImage)GetChildAt(3);
            text_level = (GTextField)GetChildAt(4);
            text_biggestLevel = (GTextField)GetChildAt(6);
            btn_close = (GButton)GetChildAt(7);
            btn_playAgain = (GButton)GetChildAt(8);
        }
    }
}