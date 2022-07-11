/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class com_lightShine : GComponent
    {
        public Transition light;
        public const string URL = "ui://3ajwo69pu4s6f4p";

        public static com_lightShine CreateInstance()
        {
            return (com_lightShine)UIPackage.CreateObject("G018_rewardCard", "com_lightShine");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            light = GetTransitionAt(0);
        }
    }
}