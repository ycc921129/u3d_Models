/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM101_basicInfo
{
    public partial class btn_mine : GButton
    {
        public GGraph pos;
        public GImage ic;
        public GGraph goldPos;
        public GProgressBar pb_mine;
        public Transition shakeRotation;
        public const string URL = "ui://94fbq9nppjbr2f";

        public static btn_mine CreateInstance()
        {
            return (btn_mine)UIPackage.CreateObject("MM101_basicInfo", "btn_mine");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            pos = (GGraph)GetChildAt(0);
            ic = (GImage)GetChildAt(1);
            goldPos = (GGraph)GetChildAt(2);
            pb_mine = (GProgressBar)GetChildAt(3);
            shakeRotation = GetTransitionAt(0);
        }
    }
}