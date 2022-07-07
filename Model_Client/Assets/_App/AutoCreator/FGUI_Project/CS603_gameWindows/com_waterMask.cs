/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS603_gameWindows
{
    public partial class com_waterMask : GComponent
    {
        public GTextField text_code;
        public const string URL = "ui://06ybtwdrxeq4i";

        public static com_waterMask CreateInstance()
        {
            return (com_waterMask)UIPackage.CreateObject("CS603_gameWindows", "com_waterMask");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_code = (GTextField)GetChildAt(0);
        }
    }
}