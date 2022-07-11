/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM101_basicInfo
{
    public partial class btn_props : GLabel
    {
        public Controller cont_props;
        public GLoader load_props;
        public GTextField text_propNum;
        public const string URL = "ui://94fbq9nppjbr2c";

        public static btn_props CreateInstance()
        {
            return (btn_props)UIPackage.CreateObject("MM101_basicInfo", "btn_props");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_props = GetControllerAt(0);
            load_props = (GLoader)GetChildAt(0);
            text_propNum = (GTextField)GetChildAt(3);
        }
    }
}