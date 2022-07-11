/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G001_tutorial
{
    public partial class com_tutorial : GComponent
    {
        public GTextField text_title;
        public GTextField text_tips0;
        public GButton btn_ok;
        public btn_nevershow btn_never;
        public GButton btn_close;
        public com_moveAni com_moveAni;
        public const string URL = "ui://5tzgfbiws5oq1q";

        public static com_tutorial CreateInstance()
        {
            return (com_tutorial)UIPackage.CreateObject("G001_tutorial", "com_tutorial");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_title = (GTextField)GetChildAt(2);
            text_tips0 = (GTextField)GetChildAt(4);
            btn_ok = (GButton)GetChildAt(5);
            btn_never = (btn_nevershow)GetChildAt(6);
            btn_close = (GButton)GetChildAt(7);
            com_moveAni = (com_moveAni)GetChildAt(8);
        }
    }
}