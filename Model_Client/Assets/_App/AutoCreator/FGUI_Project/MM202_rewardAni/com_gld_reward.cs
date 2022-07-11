/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM202_rewardAni
{
    public partial class com_gld_reward : GComponent
    {
        public GGraph pos;
        public GLoader gld_reward;
        public const string URL = "ui://msh2bq9ppe521";

        public static com_gld_reward CreateInstance()
        {
            return (com_gld_reward)UIPackage.CreateObject("MM202_rewardAni", "com_gld_reward");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            pos = (GGraph)GetChildAt(0);
            gld_reward = (GLoader)GetChildAt(1);
        }
    }
}