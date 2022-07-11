/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM101_basicInfo
{
    public partial class com_hand : GComponent
    {
        public Transition t0;
        public const string URL = "ui://94fbq9npht852t";

        public static com_hand CreateInstance()
        {
            return (com_hand)UIPackage.CreateObject("MM101_basicInfo", "com_hand");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            t0 = GetTransitionAt(0);
        }
    }
}