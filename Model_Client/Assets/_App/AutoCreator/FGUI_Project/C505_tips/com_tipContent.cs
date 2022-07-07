/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.C505_tips
{
    public partial class com_tipContent : GComponent
    {
        public GTextField text_content;
        public Transition tra_rise;
        public const string URL = "ui://g7l3moc6scoxn";

        public static com_tipContent CreateInstance()
        {
            return (com_tipContent)UIPackage.CreateObject("C505_tips", "com_tipContent");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_content = (GTextField)GetChildAt(1);
            tra_rise = GetTransitionAt(0);
        }
    }
}