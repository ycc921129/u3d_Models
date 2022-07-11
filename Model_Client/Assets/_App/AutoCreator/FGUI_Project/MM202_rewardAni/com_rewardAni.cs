/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM202_rewardAni
{
    public partial class com_rewardAni : GComponent
    {
        public com_gld_reward com_gld_reward1;
        public com_gld_reward com_gld_reward2;
        public com_gld_reward com_gld_reward3;
        public com_gld_reward com_gld_reward4;
        public com_gld_reward com_gld_reward5;
        public com_gld_reward com_gld_reward6;
        public com_gld_reward com_gld_reward7;
        public com_gld_reward com_gld_reward8;
        public com_gld_reward com_gld_reward9;
        public com_gld_reward com_gld_reward10;
        public com_gld_reward com_gld_reward11;
        public com_gld_reward com_gld_reward12;
        public com_gld_reward com_gld_reward13;
        public com_gld_reward com_gld_reward14;
        public com_gld_reward com_gld_reward15;
        public com_skin_reward com_skin_reward1;
        public com_skin_reward com_skin_reward2;
        public com_skin_reward com_skin_reward3;
        public com_skin_reward com_skin_reward4;
        public com_skin_reward com_skin_reward5;
        public com_skin_reward com_skin_reward6;
        public com_skin_reward com_skin_reward7;
        public com_skin_reward com_skin_reward8;
        public com_skin_reward com_skin_reward9;
        public com_skin_reward com_skin_reward10;
        public com_skin_reward com_skin_reward11;
        public com_skin_reward com_skin_reward12;
        public com_skin_reward com_skin_reward13;
        public com_skin_reward com_skin_reward14;
        public com_skin_reward com_skin_reward15;
        public Transition tra_rewardAni;
        public Transition tra_skinAni;
        public const string URL = "ui://msh2bq9pho5b2";

        public static com_rewardAni CreateInstance()
        {
            return (com_rewardAni)UIPackage.CreateObject("MM202_rewardAni", "com_rewardAni");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            com_gld_reward1 = (com_gld_reward)GetChildAt(0);
            com_gld_reward2 = (com_gld_reward)GetChildAt(1);
            com_gld_reward3 = (com_gld_reward)GetChildAt(2);
            com_gld_reward4 = (com_gld_reward)GetChildAt(3);
            com_gld_reward5 = (com_gld_reward)GetChildAt(4);
            com_gld_reward6 = (com_gld_reward)GetChildAt(5);
            com_gld_reward7 = (com_gld_reward)GetChildAt(6);
            com_gld_reward8 = (com_gld_reward)GetChildAt(7);
            com_gld_reward9 = (com_gld_reward)GetChildAt(8);
            com_gld_reward10 = (com_gld_reward)GetChildAt(9);
            com_gld_reward11 = (com_gld_reward)GetChildAt(10);
            com_gld_reward12 = (com_gld_reward)GetChildAt(11);
            com_gld_reward13 = (com_gld_reward)GetChildAt(12);
            com_gld_reward14 = (com_gld_reward)GetChildAt(13);
            com_gld_reward15 = (com_gld_reward)GetChildAt(14);
            com_skin_reward1 = (com_skin_reward)GetChildAt(15);
            com_skin_reward2 = (com_skin_reward)GetChildAt(16);
            com_skin_reward3 = (com_skin_reward)GetChildAt(17);
            com_skin_reward4 = (com_skin_reward)GetChildAt(18);
            com_skin_reward5 = (com_skin_reward)GetChildAt(19);
            com_skin_reward6 = (com_skin_reward)GetChildAt(20);
            com_skin_reward7 = (com_skin_reward)GetChildAt(21);
            com_skin_reward8 = (com_skin_reward)GetChildAt(22);
            com_skin_reward9 = (com_skin_reward)GetChildAt(23);
            com_skin_reward10 = (com_skin_reward)GetChildAt(24);
            com_skin_reward11 = (com_skin_reward)GetChildAt(25);
            com_skin_reward12 = (com_skin_reward)GetChildAt(26);
            com_skin_reward13 = (com_skin_reward)GetChildAt(27);
            com_skin_reward14 = (com_skin_reward)GetChildAt(28);
            com_skin_reward15 = (com_skin_reward)GetChildAt(29);
            tra_rewardAni = GetTransitionAt(0);
            tra_skinAni = GetTransitionAt(1);
        }
    }
}