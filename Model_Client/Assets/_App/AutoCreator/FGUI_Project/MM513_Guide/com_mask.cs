/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM513_Guide
{
    public partial class com_mask : GComponent
    {
        public Controller cont_guide;
        public GGraph bg;
        public GGraph mask;
        public GTextField info;
        public const string URL = "ui://usopam3mxewn1";

        public static com_mask CreateInstance()
        {
            return (com_mask)UIPackage.CreateObject("MM513_Guide", "com_mask");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_guide = GetControllerAt(0);
            bg = (GGraph)GetChildAt(0);
            mask = (GGraph)GetChildAt(1);
            info = (GTextField)GetChildAt(2);
        }
    }
}