/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_giftGuide : GComponent
    {
        public Controller cont_guide;
        public GGraph mask;
        public GTextField info;
        public const string URL = "ui://rl7u9y2lmdwa13";

        public static com_giftGuide CreateInstance()
        {
            return (com_giftGuide)UIPackage.CreateObject("G017_gameRedeem", "com_giftGuide");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_guide = GetControllerAt(0);
            mask = (GGraph)GetChildAt(0);
            info = (GTextField)GetChildAt(1);
        }
    }
}