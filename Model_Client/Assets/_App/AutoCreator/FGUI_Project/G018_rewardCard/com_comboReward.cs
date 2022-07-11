/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class com_comboReward : GComponent
    {
        public GTextField text_title;
        public com_comboRewardItem com_frameReward;
        public btn_getMore btn_claim;
        public GButton btn_close;
        public GProgressBar pb_redeem;
        public GTextField text_pbTips;
        public GGraph caidaiPos;
        public Transition close;
        public Transition receive;
        public const string URL = "ui://3ajwo69pmciwf4u";

        public static com_comboReward CreateInstance()
        {
            return (com_comboReward)UIPackage.CreateObject("G018_rewardCard", "com_comboReward");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_title = (GTextField)GetChildAt(1);
            com_frameReward = (com_comboRewardItem)GetChildAt(2);
            btn_claim = (btn_getMore)GetChildAt(3);
            btn_close = (GButton)GetChildAt(4);
            pb_redeem = (GProgressBar)GetChildAt(5);
            text_pbTips = (GTextField)GetChildAt(6);
            caidaiPos = (GGraph)GetChildAt(7);
            close = GetTransitionAt(0);
            receive = GetTransitionAt(1);
        }
    }
}