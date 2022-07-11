/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_gameRedeem : GComponent
    {
        public Controller cont_isGuest;
        public GLoader load_bg;
        public GButton btn_home;
        public GTextField text_coinValue;
        public GLoader load_valueMain;
        public GLoader load_gameIcon;
        public GTextField text_account;
        public GTextField text_guestAccount;
        public GButton btn_account;
        public GGroup top;
        public GList list_card;
        public const string URL = "ui://rl7u9y2lmdwaz";

        public static com_gameRedeem CreateInstance()
        {
            return (com_gameRedeem)UIPackage.CreateObject("G017_gameRedeem", "com_gameRedeem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_isGuest = GetControllerAt(0);
            load_bg = (GLoader)GetChildAt(0);
            btn_home = (GButton)GetChildAt(2);
            text_coinValue = (GTextField)GetChildAt(4);
            load_valueMain = (GLoader)GetChildAt(5);
            load_gameIcon = (GLoader)GetChildAt(6);
            text_account = (GTextField)GetChildAt(7);
            text_guestAccount = (GTextField)GetChildAt(8);
            btn_account = (GButton)GetChildAt(9);
            top = (GGroup)GetChildAt(10);
            list_card = (GList)GetChildAt(11);
        }
    }
}