/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class com_mineRewardItem : GComponent
    {
        public Controller cont_rewardType;
        public GGraph pos_diamond;
        public GLoader load_diamondReward;
        public GTextField text_diamon;
        public GLoader load_diamond;
        public GGroup diamond;
        public GGraph pos_skin;
        public GLoader load_skinReward;
        public GTextField text_skin;
        public GLoader load_skin;
        public GGroup skin;
        public GGraph pos;
        public const string URL = "ui://3ajwo69ppspaf5o";

        public static com_mineRewardItem CreateInstance()
        {
            return (com_mineRewardItem)UIPackage.CreateObject("G018_rewardCard", "com_mineRewardItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_rewardType = GetControllerAt(0);
            pos_diamond = (GGraph)GetChildAt(0);
            load_diamondReward = (GLoader)GetChildAt(1);
            text_diamon = (GTextField)GetChildAt(2);
            load_diamond = (GLoader)GetChildAt(3);
            diamond = (GGroup)GetChildAt(4);
            pos_skin = (GGraph)GetChildAt(5);
            load_skinReward = (GLoader)GetChildAt(6);
            text_skin = (GTextField)GetChildAt(7);
            load_skin = (GLoader)GetChildAt(8);
            skin = (GGroup)GetChildAt(9);
            pos = (GGraph)GetChildAt(10);
        }
    }
}