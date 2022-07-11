/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM101_basicInfo
{
    public partial class com_mask : GComponent
    {
        public GGraph bg;
        public GGraph mask;
        public const string URL = "ui://94fbq9npht852s";

        public static com_mask CreateInstance()
        {
            return (com_mask)UIPackage.CreateObject("MM101_basicInfo", "com_mask");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg = (GGraph)GetChildAt(0);
            mask = (GGraph)GetChildAt(1);
        }
    }
}