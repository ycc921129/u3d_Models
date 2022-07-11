/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class com_flipReward : GComponent
    {
        public btn_card btn_card1;
        public btn_card btn_card2;
        public btn_card btn_card3;
        public Transition wash;
        public Transition light;
        public const string URL = "ui://3ajwo69pqj6mf4c";

        public static com_flipReward CreateInstance()
        {
            return (com_flipReward)UIPackage.CreateObject("G018_rewardCard", "com_flipReward");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            btn_card1 = (btn_card)GetChildAt(0);
            btn_card2 = (btn_card)GetChildAt(1);
            btn_card3 = (btn_card)GetChildAt(2);
            wash = GetTransitionAt(0);
            light = GetTransitionAt(1);
        }
    }
}