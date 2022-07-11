/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_gameType : GComponent
    {
        public Transition starAnim;
        public const string URL = "ui://rl7u9y2ll3r63l";

        public static com_gameType CreateInstance()
        {
            return (com_gameType)UIPackage.CreateObject("G017_gameRedeem", "com_gameType");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            starAnim = GetTransitionAt(0);
        }
    }
}