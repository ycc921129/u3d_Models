/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_hand : GComponent
    {
        public Transition t0;
        public const string URL = "ui://rl7u9y2ll3r63p";

        public static com_hand CreateInstance()
        {
            return (com_hand)UIPackage.CreateObject("G017_gameRedeem", "com_hand");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            t0 = GetTransitionAt(0);
        }
    }
}