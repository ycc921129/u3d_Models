/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_card : GComponent
    {
        public Controller cont_exchangeStatus;
        public Controller cont_redeemType;
        public GLoader load_bg;
        public GLoader load_reward;
        public GTextField text_value;
        public GLoader load_valueMain;
        public GTextField text_target;
        public GProgressBar pb_redeem;
        public GButton btn_exchange;
        public GTextField text_time;
        public GProgressBar pb_value;
        public GTextField text_adTime;
        public GTextField text_queueText;
        public GTextField text_queueMove;
        public GTextField text_finish;
        public GTextField text_finishWait;
        public GTextField text_successful;
        public Transition tra_putin;
        public Transition tra_putout;
        public const string URL = "ui://rl7u9y2lmdwae";

        public static com_card CreateInstance()
        {
            return (com_card)UIPackage.CreateObject("G017_gameRedeem", "com_card");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_exchangeStatus = GetControllerAt(0);
            cont_redeemType = GetControllerAt(1);
            load_bg = (GLoader)GetChildAt(0);
            load_reward = (GLoader)GetChildAt(1);
            text_value = (GTextField)GetChildAt(2);
            load_valueMain = (GLoader)GetChildAt(3);
            text_target = (GTextField)GetChildAt(4);
            pb_redeem = (GProgressBar)GetChildAt(5);
            btn_exchange = (GButton)GetChildAt(6);
            text_time = (GTextField)GetChildAt(8);
            pb_value = (GProgressBar)GetChildAt(9);
            text_adTime = (GTextField)GetChildAt(10);
            text_queueText = (GTextField)GetChildAt(11);
            text_queueMove = (GTextField)GetChildAt(12);
            text_finish = (GTextField)GetChildAt(13);
            text_finishWait = (GTextField)GetChildAt(14);
            text_successful = (GTextField)GetChildAt(16);
            tra_putin = GetTransitionAt(0);
            tra_putout = GetTransitionAt(1);
        }
    }
}