/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class btn_card : GLabel
    {
        public Controller cont_fontState;
        public Controller cont_coinType;
        public GLoader load_logo;
        public GLoader iconImage;
        public GTextField rewardText;
        public btn_claim btn_get;
        public com_lightShine com_light;
        public GGraph pos;
        public Transition btnShow;
        public Transition light;
        public const string URL = "ui://3ajwo69pqj6mf4d";

        public static btn_card CreateInstance()
        {
            return (btn_card)UIPackage.CreateObject("G018_rewardCard", "btn_card");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_fontState = GetControllerAt(0);
            cont_coinType = GetControllerAt(1);
            load_logo = (GLoader)GetChildAt(1);
            iconImage = (GLoader)GetChildAt(3);
            rewardText = (GTextField)GetChildAt(4);
            btn_get = (btn_claim)GetChildAt(5);
            com_light = (com_lightShine)GetChildAt(6);
            pos = (GGraph)GetChildAt(7);
            btnShow = GetTransitionAt(0);
            light = GetTransitionAt(1);
        }
    }
}