/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G018_rewardCard
{
    public partial class com_list : GComponent
    {
        public GList list_multiple;
        public GImage icon_pointer;
        public const string URL = "ui://3ajwo69pqj6mf40";

        public static com_list CreateInstance()
        {
            return (com_list)UIPackage.CreateObject("G018_rewardCard", "com_list");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            list_multiple = (GList)GetChildAt(1);
            icon_pointer = (GImage)GetChildAt(2);
        }
    }
}