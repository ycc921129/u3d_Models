/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class com_mineReward : GComponent
    {
        public GTextField text_title;
        public com_mineRewardItem com_frameReward;
        public btn_getMore btn_claim;
        public GButton btn_close;
        public GProgressBar pb_redeem;
        public GTextField text_pbTips;
        public GGraph caidaiPos;
        public Transition close;
        public Transition receive;
        public const string URL = "ui://3ajwo69pqj6mf4k";

        public static com_mineReward CreateInstance()
        {
            return (com_mineReward)UIPackage.CreateObject("G018_rewardCard", "com_mineReward");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_title = (GTextField)GetChildAt(1);
            com_frameReward = (com_mineRewardItem)GetChildAt(3);
            btn_claim = (btn_getMore)GetChildAt(4);
            btn_close = (GButton)GetChildAt(5);
            pb_redeem = (GProgressBar)GetChildAt(6);
            text_pbTips = (GTextField)GetChildAt(7);
            caidaiPos = (GGraph)GetChildAt(8);
            close = GetTransitionAt(0);
            receive = GetTransitionAt(1);
        }
    }
}