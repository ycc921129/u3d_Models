/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM101_basicInfo
{
    public partial class com_unlockSkin : GComponent
    {
        public com_mask com_mask;
        public GGraph pos;
        public GTextField info;
        public GButton btn_confirm;
        public Transition confirmShow;
        public const string URL = "ui://94fbq9npw8q42p";

        public static com_unlockSkin CreateInstance()
        {
            return (com_unlockSkin)UIPackage.CreateObject("MM101_basicInfo", "com_unlockSkin");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            com_mask = (com_mask)GetChildAt(0);
            pos = (GGraph)GetChildAt(5);
            info = (GTextField)GetChildAt(6);
            btn_confirm = (GButton)GetChildAt(7);
            confirmShow = GetTransitionAt(0);
        }
    }
}