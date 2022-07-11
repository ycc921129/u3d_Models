/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class btn_gameType : GButton
    {
        public Controller cont_state;
        public GLoader iconImg;
        public const string URL = "ui://rl7u9y2lntzg30";

        public static btn_gameType CreateInstance()
        {
            return (btn_gameType)UIPackage.CreateObject("G017_gameRedeem", "btn_gameType");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_state = GetControllerAt(0);
            iconImg = (GLoader)GetChildAt(0);
        }
    }
}