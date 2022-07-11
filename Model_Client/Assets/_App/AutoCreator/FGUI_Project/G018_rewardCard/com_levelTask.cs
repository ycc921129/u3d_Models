/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class com_levelTask : GComponent
    {
        public Controller cont_style;
        public GImage img_titleTop;
        public GImage img_title;
        public GImage img_tips0;
        public GImage img_level;
        public GTextField text_level;
        public com_taskReward com_frameReward;
        public btn_getMore btn_claim;
        public GButton btn_close;
        public GProgressBar pb_redeem;
        public GTextField text_pbTips;
        public GGraph caidaiPos;
        public Transition close;
        public Transition receive;
        public Transition t2;
        public const string URL = "ui://3ajwo69pmciwf4t";

        public static com_levelTask CreateInstance()
        {
            return (com_levelTask)UIPackage.CreateObject("G018_rewardCard", "com_levelTask");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_style = GetControllerAt(0);
            img_titleTop = (GImage)GetChildAt(0);
            img_title = (GImage)GetChildAt(1);
            img_tips0 = (GImage)GetChildAt(2);
            img_level = (GImage)GetChildAt(3);
            text_level = (GTextField)GetChildAt(5);
            com_frameReward = (com_taskReward)GetChildAt(6);
            btn_claim = (btn_getMore)GetChildAt(7);
            btn_close = (GButton)GetChildAt(8);
            pb_redeem = (GProgressBar)GetChildAt(9);
            text_pbTips = (GTextField)GetChildAt(10);
            caidaiPos = (GGraph)GetChildAt(11);
            close = GetTransitionAt(0);
            receive = GetTransitionAt(1);
            t2 = GetTransitionAt(2);
        }
    }
}