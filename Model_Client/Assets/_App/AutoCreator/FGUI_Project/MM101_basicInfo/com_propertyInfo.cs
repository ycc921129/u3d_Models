/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM101_basicInfo
{
    public partial class com_propertyInfo : GComponent
    {
        public com_diamond com_diamond;
        public com_skinCurrency com_skinCurrency;
        public const string URL = "ui://94fbq9npd4qv1";

        public static com_propertyInfo CreateInstance()
        {
            return (com_propertyInfo)UIPackage.CreateObject("MM101_basicInfo", "com_propertyInfo");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            com_diamond = (com_diamond)GetChildAt(0);
            com_skinCurrency = (com_skinCurrency)GetChildAt(1);
        }
    }
}