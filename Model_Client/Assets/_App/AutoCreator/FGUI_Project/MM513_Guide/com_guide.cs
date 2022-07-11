/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM513_Guide
{
    public partial class com_guide : GComponent
    {
        public com_mask com_mask;
        public const string URL = "ui://usopam3mxewn0";

        public static com_guide CreateInstance()
        {
            return (com_guide)UIPackage.CreateObject("MM513_Guide", "com_guide");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            com_mask = (com_mask)GetChildAt(0);
        }
    }
}