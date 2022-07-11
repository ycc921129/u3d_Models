/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class com_flipCard : GComponent
    {
        public GTextField text_title;
        public GGraph pos;
        public GLoader load_reward;
        public GButton btn_close;
        public com_flipReward com_flipReward;
        public GProgressBar pb_redeem;
        public GTextField text_pbTips;
        public GGraph caidaiPos;
        public Transition close;
        public const string URL = "ui://3ajwo69pqj6mf48";

        public static com_flipCard CreateInstance()
        {
            return (com_flipCard)UIPackage.CreateObject("G018_rewardCard", "com_flipCard");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_title = (GTextField)GetChildAt(1);
            pos = (GGraph)GetChildAt(2);
            load_reward = (GLoader)GetChildAt(3);
            btn_close = (GButton)GetChildAt(4);
            com_flipReward = (com_flipReward)GetChildAt(5);
            pb_redeem = (GProgressBar)GetChildAt(6);
            text_pbTips = (GTextField)GetChildAt(7);
            caidaiPos = (GGraph)GetChildAt(8);
            close = GetTransitionAt(0);
        }
    }
}