/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM101_basicInfo
{
    public partial class btn_skin : GButton
    {
        public Controller cont_state;
        public GGroup shadow;
        public GGroup img;
        public GTextField text_level;
        public GGroup text;
        public com_hand hand;
        public GGroup skin;
        public Transition unlock;
        public const string URL = "ui://94fbq9nppjbr27";

        public static btn_skin CreateInstance()
        {
            return (btn_skin)UIPackage.CreateObject("MM101_basicInfo", "btn_skin");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_state = GetControllerAt(0);
            shadow = (GGroup)GetChildAt(9);
            img = (GGroup)GetChildAt(13);
            text_level = (GTextField)GetChildAt(15);
            text = (GGroup)GetChildAt(16);
            hand = (com_hand)GetChildAt(17);
            skin = (GGroup)GetChildAt(18);
            unlock = GetTransitionAt(0);
        }
    }
}