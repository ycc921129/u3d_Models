/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class btn_getMore : GButton
    {
        public Controller cont_ad;
        public const string URL = "ui://3ajwo69pqj6mf3x";

        public static btn_getMore CreateInstance()
        {
            return (btn_getMore)UIPackage.CreateObject("G018_rewardCard", "btn_getMore");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_ad = GetControllerAt(0);
        }
    }
}