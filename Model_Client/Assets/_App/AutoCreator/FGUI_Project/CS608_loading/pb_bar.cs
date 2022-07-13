/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS608_loading
{
    public partial class pb_bar : GProgressBar
    {
        public Transition jump;
        public const string URL = "ui://9euvtldhpjc9p";

        public static pb_bar CreateInstance()
        {
            return (pb_bar)UIPackage.CreateObject("CS608_loading", "pb_bar");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            jump = GetTransitionAt(0);
        }
    }
}