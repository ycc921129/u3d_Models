/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.C505_tips
{
    public partial class com_tips : GComponent
    {
        public com_tipContent com_tipContent;
        public const string URL = "ui://g7l3moc6anuho";

        public static com_tips CreateInstance()
        {
            return (com_tips)UIPackage.CreateObject("C505_tips", "com_tips");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            com_tipContent = (com_tipContent)GetChildAt(0);
        }
    }
}