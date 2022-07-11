/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS608_loading
{
    public partial class com_agreement : GComponent
    {
        public btn_confirm btn_yes;
        public btn_confirm btn_exit;
        public GTextField text_title;
        public GTextField text_tips;
        public const string URL = "ui://9euvtldhlh5y3i";

        public static com_agreement CreateInstance()
        {
            return (com_agreement)UIPackage.CreateObject("CS608_loading", "com_agreement");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            btn_yes = (btn_confirm)GetChildAt(1);
            btn_exit = (btn_confirm)GetChildAt(2);
            text_title = (GTextField)GetChildAt(3);
            text_tips = (GTextField)GetChildAt(4);
        }
    }
}