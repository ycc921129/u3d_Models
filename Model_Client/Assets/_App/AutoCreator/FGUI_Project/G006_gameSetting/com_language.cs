/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G006_gameSetting
{
    public partial class com_language : GComponent
    {
        public GList list;
        public Transition tra_language;
        public const string URL = "ui://sirjnrkkqj6mn";

        public static com_language CreateInstance()
        {
            return (com_language)UIPackage.CreateObject("G006_gameSetting", "com_language");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            list = (GList)GetChildAt(1);
            tra_language = GetTransitionAt(0);
        }
    }
}