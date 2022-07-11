/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G019_redeemActivite
{
    public partial class com_activite : GComponent
    {
        public GImage img_frame;
        public GTextField text_title;
        public GComponent com_light;
        public GLoader load_reward;
        public GImage img_tiemBottom;
        public GTextField text_leftTips;
        public GTextField text_timeLeft;
        public GImage img_diamondBottom;
        public GTextField text_diamondValue;
        public GTextField text_timeShow;
        public GLoader load_diamond;
        public GGraph pos;
        public GTextField text_maxWin;
        public GProgressBar pb_activite;
        public GButton btn_confirm;
        public GTextField text_getTips;
        public const string URL = "ui://ayq1m1qumciw41";

        public static com_activite CreateInstance()
        {
            return (com_activite)UIPackage.CreateObject("G019_redeemActivite", "com_activite");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            img_frame = (GImage)GetChildAt(0);
            text_title = (GTextField)GetChildAt(2);
            com_light = (GComponent)GetChildAt(3);
            load_reward = (GLoader)GetChildAt(4);
            img_tiemBottom = (GImage)GetChildAt(5);
            text_leftTips = (GTextField)GetChildAt(6);
            text_timeLeft = (GTextField)GetChildAt(7);
            img_diamondBottom = (GImage)GetChildAt(8);
            text_diamondValue = (GTextField)GetChildAt(9);
            text_timeShow = (GTextField)GetChildAt(10);
            load_diamond = (GLoader)GetChildAt(11);
            pos = (GGraph)GetChildAt(12);
            text_maxWin = (GTextField)GetChildAt(13);
            pb_activite = (GProgressBar)GetChildAt(14);
            btn_confirm = (GButton)GetChildAt(15);
            text_getTips = (GTextField)GetChildAt(16);
        }
    }
}