/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G019_redeemActivite
{
    public partial class com_activiteFinish : GComponent
    {
        public GImage img_frame;
        public GButton btn_close;
        public GImage img_title;
        public GTextField text_title;
        public GComponent com_light;
        public GLoader load_reward;
        public GImage img_diamondBottom;
        public GLoader load_diamond;
        public GTextField text_diamondFinish;
        public GGraph pos;
        public GTextField text_tips0;
        public GButton btn_redeem;
        public const string URL = "ui://ayq1m1qumciw42";

        public static com_activiteFinish CreateInstance()
        {
            return (com_activiteFinish)UIPackage.CreateObject("G019_redeemActivite", "com_activiteFinish");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            img_frame = (GImage)GetChildAt(0);
            btn_close = (GButton)GetChildAt(1);
            img_title = (GImage)GetChildAt(2);
            text_title = (GTextField)GetChildAt(3);
            com_light = (GComponent)GetChildAt(4);
            load_reward = (GLoader)GetChildAt(5);
            img_diamondBottom = (GImage)GetChildAt(6);
            load_diamond = (GLoader)GetChildAt(7);
            text_diamondFinish = (GTextField)GetChildAt(8);
            pos = (GGraph)GetChildAt(9);
            text_tips0 = (GTextField)GetChildAt(10);
            btn_redeem = (GButton)GetChildAt(11);
        }
    }
}