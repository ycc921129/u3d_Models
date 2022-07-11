/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class com_taskReward : GComponent
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
        public const string URL = "ui://3ajwo69pnavcf5q";

        public static com_taskReward CreateInstance()
        {
            return (com_taskReward)UIPackage.CreateObject("G018_rewardCard", "com_taskReward");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_rewardType = GetControllerAt(0);
            pos_diamond = (GGraph)GetChildAt(1);
            load_diamondReward = (GLoader)GetChildAt(2);
            text_diamon = (GTextField)GetChildAt(3);
            load_diamond = (GLoader)GetChildAt(4);
            diamond = (GGroup)GetChildAt(5);
            pos_skin = (GGraph)GetChildAt(7);
            load_skinReward = (GLoader)GetChildAt(8);
            text_skin = (GTextField)GetChildAt(9);
            load_skin = (GLoader)GetChildAt(10);
            skin = (GGroup)GetChildAt(11);
            pos = (GGraph)GetChildAt(12);
        }
    }
}