/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_chooseGame : GComponent
    {
        public GLoader load_reward;
        public GTextField text_title;
        public GGroup title;
        public GGraph pos;
        public GTextField text_tips0;
        public com_gameType list_gameType;
        public btn_next btn_confirm;
        public com_hand hand;
        public GTextField text_tips1;
        public Transition starAnim;
        public const string URL = "ui://rl7u9y2ll3r63j";

        public static com_chooseGame CreateInstance()
        {
            return (com_chooseGame)UIPackage.CreateObject("G017_gameRedeem", "com_chooseGame");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            load_reward = (GLoader)GetChildAt(2);
            text_title = (GTextField)GetChildAt(4);
            title = (GGroup)GetChildAt(5);
            pos = (GGraph)GetChildAt(6);
            text_tips0 = (GTextField)GetChildAt(7);
            list_gameType = (com_gameType)GetChildAt(8);
            btn_confirm = (btn_next)GetChildAt(9);
            hand = (com_hand)GetChildAt(10);
            text_tips1 = (GTextField)GetChildAt(11);
            starAnim = GetTransitionAt(0);
        }
    }
}