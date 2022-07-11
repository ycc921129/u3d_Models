/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_iconCircle : GComponent
    {
        public GLoader load_icon;
        public const string URL = "ui://rl7u9y2livu1ay";

        public static com_iconCircle CreateInstance()
        {
            return (com_iconCircle)UIPackage.CreateObject("G017_gameRedeem", "com_iconCircle");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            load_icon = (GLoader)GetChildAt(0);
        }
    }
}