/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G020_skinRedeem
{
    public partial class com_skinRedeem : GComponent
    {
        public Controller cont_isGuest;
        public GButton btn_home;
        public GImage jinbi;
        public GTextField text_skinValue;
        public GLoader load_gameIcon;
        public GButton btn_account;
        public GTextField text_account;
        public GTextField text_guestAccount;
        public GGroup top;
        public GGraph skin_mask;
        public GList list_skin;
        public const string URL = "ui://a1jbnm9he31h3u";

        public static com_skinRedeem CreateInstance()
        {
            return (com_skinRedeem)UIPackage.CreateObject("G020_skinRedeem", "com_skinRedeem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_isGuest = GetControllerAt(0);
            btn_home = (GButton)GetChildAt(2);
            jinbi = (GImage)GetChildAt(4);
            text_skinValue = (GTextField)GetChildAt(5);
            load_gameIcon = (GLoader)GetChildAt(6);
            btn_account = (GButton)GetChildAt(7);
            text_account = (GTextField)GetChildAt(8);
            text_guestAccount = (GTextField)GetChildAt(9);
            top = (GGroup)GetChildAt(10);
            skin_mask = (GGraph)GetChildAt(11);
            list_skin = (GList)GetChildAt(12);
        }
    }
}