/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class com_item : GComponent
    {
        public Controller cont_color;
        public GTextField text_value;
        public const string URL = "ui://3ajwo69pqj6mf42";

        public static com_item CreateInstance()
        {
            return (com_item)UIPackage.CreateObject("G018_rewardCard", "com_item");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont_color = GetControllerAt(0);
            text_value = (GTextField)GetChildAt(0);
        }
    }
}