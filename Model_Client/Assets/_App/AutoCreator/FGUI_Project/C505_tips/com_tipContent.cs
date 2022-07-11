/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.C505_tips
{
    public partial class com_tipContent : GComponent
    {
        public Controller cont_iconCtrl;
        public GTextField text_content;
        public GLoader iconUrl;
        public Transition tra_rise;
        public const string URL = "ui://g7l3moc6anuhn";

        public static com_tipContent CreateInstance()
        {
            return (com_tipContent)UIPackage.CreateObject("C505_tips", "com_tipContent");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_iconCtrl = GetControllerAt(0);
            text_content = (GTextField)GetChildAt(1);
            iconUrl = (GLoader)GetChildAt(2);
            tra_rise = GetTransitionAt(0);
        }
    }
}