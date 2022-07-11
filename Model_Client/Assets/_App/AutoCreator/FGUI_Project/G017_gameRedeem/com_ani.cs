/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_ani : GComponent
    {
        public Transition jump;
        public const string URL = "ui://rl7u9y2lubt43f";

        public static com_ani CreateInstance()
        {
            return (com_ani)UIPackage.CreateObject("G017_gameRedeem", "com_ani");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            jump = GetTransitionAt(0);
        }
    }
}