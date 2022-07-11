/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS608_loading
{
    public partial class com_pbAni : GComponent
    {
        public Transition enter;
        public Transition jump;
        public Transition t2;
        public const string URL = "ui://9euvtldhe2l03e";

        public static com_pbAni CreateInstance()
        {
            return (com_pbAni)UIPackage.CreateObject("CS608_loading", "com_pbAni");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            enter = GetTransitionAt(0);
            jump = GetTransitionAt(1);
            t2 = GetTransitionAt(2);
        }
    }
}