/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G001_tutorial
{
    public partial class com_fingerAni : GComponent
    {
        public Transition tra_click;
        public const string URL = "ui://5tzgfbiwm9c24";

        public static com_fingerAni CreateInstance()
        {
            return (com_fingerAni)UIPackage.CreateObject("G001_tutorial", "com_fingerAni");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            tra_click = GetTransitionAt(0);
        }
    }
}