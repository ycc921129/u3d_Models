/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS603_gameWindows
{
    public partial class com_gameLoading : GComponent
    {
        public GImage img_loading;
        public Transition tra_loading;
        public const string URL = "ui://06ybtwdrb72qe";

        public static com_gameLoading CreateInstance()
        {
            return (com_gameLoading)UIPackage.CreateObject("CS603_gameWindows", "com_gameLoading");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            img_loading = (GImage)GetChildAt(0);
            tra_loading = GetTransitionAt(0);
        }
    }
}