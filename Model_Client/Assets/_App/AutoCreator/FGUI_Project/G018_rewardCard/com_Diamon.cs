/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class com_Diamon : GComponent
    {
        public GTextField text_title;
        public com_list list;
        public GGraph pos;
        public GLoader load_reward;
        public GTextField text_diamon;
        public GTextField text_rate;
        public GTextField text_mutiple;
        public GGroup mutipleGroup;
        public btn_getMore btn_coliect;
        public GButton btn_close;
        public GProgressBar pb_redeem;
        public GTextField text_pbTips;
        public GGraph caidaiPos;
        public Transition mutiple;
        public Transition close;
        public Transition receive;
        public const string URL = "ui://3ajwo69pqj6mf3u";

        public static com_Diamon CreateInstance()
        {
            return (com_Diamon)UIPackage.CreateObject("G018_rewardCard", "com_Diamon");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_title = (GTextField)GetChildAt(1);
            list = (com_list)GetChildAt(2);
            pos = (GGraph)GetChildAt(3);
            load_reward = (GLoader)GetChildAt(4);
            text_diamon = (GTextField)GetChildAt(5);
            text_rate = (GTextField)GetChildAt(7);
            text_mutiple = (GTextField)GetChildAt(8);
            mutipleGroup = (GGroup)GetChildAt(9);
            btn_coliect = (btn_getMore)GetChildAt(10);
            btn_close = (GButton)GetChildAt(11);
            pb_redeem = (GProgressBar)GetChildAt(12);
            text_pbTips = (GTextField)GetChildAt(13);
            caidaiPos = (GGraph)GetChildAt(14);
            mutiple = GetTransitionAt(0);
            close = GetTransitionAt(1);
            receive = GetTransitionAt(2);
        }
    }
}