/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G020_skinRedeem
{
    public partial class com_skin : GComponent
    {
        public Controller cont_skinType;
        public Controller cont_grade;
        public GLoader load_gameSkin;
        public GLoader load_grade;
        public btn_skinRedeem btn_redeem;
        public GProgressBar pb_skinChip;
        public GTextField text_tips0;
        public const string URL = "ui://a1jbnm9he31h3r";

        public static com_skin CreateInstance()
        {
            return (com_skin)UIPackage.CreateObject("G020_skinRedeem", "com_skin");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_skinType = GetControllerAt(0);
            cont_grade = GetControllerAt(1);
            load_gameSkin = (GLoader)GetChildAt(1);
            load_grade = (GLoader)GetChildAt(2);
            btn_redeem = (btn_skinRedeem)GetChildAt(4);
            pb_skinChip = (GProgressBar)GetChildAt(5);
            text_tips0 = (GTextField)GetChildAt(6);
        }
    }
}