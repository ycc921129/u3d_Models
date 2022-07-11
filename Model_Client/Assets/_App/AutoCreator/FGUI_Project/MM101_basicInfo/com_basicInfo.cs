/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM101_basicInfo
{
    public partial class com_basicInfo : GComponent
    {
        public com_propertyInfo com_Info;
        public GButton btn_pause;
        public GGroup top;
        public btn_props btn_increase;
        public btn_props btn_hammer;
        public btn_mine btn_mine;
        public GButton btn_redeem;
        public btn_skin btn_skin;
        public GGroup bottom;
        public Transition unlock;
        public const string URL = "ui://94fbq9npd4qv0";

        public static com_basicInfo CreateInstance()
        {
            return (com_basicInfo)UIPackage.CreateObject("MM101_basicInfo", "com_basicInfo");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            com_Info = (com_propertyInfo)GetChildAt(0);
            btn_pause = (GButton)GetChildAt(1);
            top = (GGroup)GetChildAt(2);
            btn_increase = (btn_props)GetChildAt(4);
            btn_hammer = (btn_props)GetChildAt(5);
            btn_mine = (btn_mine)GetChildAt(6);
            btn_redeem = (GButton)GetChildAt(7);
            btn_skin = (btn_skin)GetChildAt(8);
            bottom = (GGroup)GetChildAt(9);
            unlock = GetTransitionAt(0);
        }
    }
}