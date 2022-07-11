/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class btn_claim : GButton
    {
        public Controller cont_ad;
        public const string URL = "ui://3ajwo69pqj6mf4j";

        public static btn_claim CreateInstance()
        {
            return (btn_claim)UIPackage.CreateObject("G018_rewardCard", "btn_claim");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_ad = GetControllerAt(0);
        }
    }
}