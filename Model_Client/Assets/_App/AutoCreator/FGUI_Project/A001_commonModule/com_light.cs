/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.A001_commonModule
{
    public partial class com_light : GComponent
    {
        public Transition light;
        public const string URL = "ui://9gpz896kivu1f5v";

        public static com_light CreateInstance()
        {
            return (com_light)UIPackage.CreateObject("A001_commonModule", "com_light");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            light = GetTransitionAt(0);
        }
    }
}